using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Runtime.Serialization;

namespace MegaPaint
{
    [Serializable]
    public class GraphicsList
    {
        private ArrayList graphicsList;
        private bool _isDirty;

        private const string entryCount = "ObjectCount";
        private const string entryType = "ObjectType";

        public bool Dirty
        {
            get
            {
                if (_isDirty == false)
                {
                    foreach (DrawObject o in graphicsList)
                    {
                        if (o.Dirty)
                        {
                            _isDirty = true;
                            break;
                        }
                    }
                }
                return _isDirty;
            }
            set
            {
                foreach (DrawObject o in graphicsList)
                    o.Dirty = false;
                _isDirty = false;
            }
        }

        public IEnumerable<DrawObject> Selection
        {
            get
            {
                foreach (DrawObject o in graphicsList)
                {
                    if (o.Selected)
                    {
                        yield return o;
                    }
                }
            }
        }

        public GraphicsList()
        {
            graphicsList = new ArrayList();
        }
        
        public void Draw(Graphics g, Graphics gImage)
        {
            int numberObjects = graphicsList.Count;

            for (int i = numberObjects - 1; i >= 0; i--)
            {
                DrawObject o;
                o = (DrawObject)graphicsList[i];
                if (o.IntersectsWith(Rectangle.Round(g.ClipBounds)))
                {
                    o.Draw(g);
                    o.Draw(gImage);
                }

                if (o.Selected)
                    o.DrawTracker(g);
            }
        }

        public bool Clear()
        {
            bool result = (graphicsList.Count > 0);
            graphicsList.Clear();
            if (result)
                _isDirty = false;
            return result;
        }

        
        public int Count
        {
            get { return graphicsList.Count; }
        }

        public DrawObject this[int index]
        {
            get
            {
                if (index < 0 ||
                    index >= graphicsList.Count)
                    return null;

                return (DrawObject)graphicsList[index];
            }
        }

        
        public int SelectionCount
        {
            get
            {
                int n = 0;

                foreach (DrawObject o in graphicsList)
                {
                    if (o.Selected)
                        n++;
                }

                return n;
            }
        }

        public DrawObject GetSelectedObject(int index)
        {
            int n = -1;

            foreach (DrawObject o in graphicsList)
            {
                if (o.Selected)
                {
                    n++;

                    if (n == index)
                        return o;
                }
            }

            return null;
        }

        public void Add(DrawObject obj)
        {
            graphicsList.Sort();
            foreach (DrawObject o in graphicsList)
                o.ZOrder++;

            graphicsList.Insert(0, obj);
        }

        public void Append(DrawObject obj)
        {
            graphicsList.Add(obj);
        }

        public void SelectInRectangle(Rectangle rectangle)
        {
            UnselectAll();

            foreach (DrawObject o in graphicsList)
            {
                if (o.IntersectsWith(rectangle))
                    o.Selected = true;
            }
        }

        public void UnselectAll()
        {
            foreach (DrawObject o in graphicsList)
            {
                o.Selected = false;
            }
        }

        public void SelectAll()
        {
            foreach (DrawObject o in graphicsList)
            {
                o.Selected = true;
            }
        }

       
        public bool DeleteSelection()
        {
            bool result = false;

            int n = graphicsList.Count;

            for (int i = n - 1; i >= 0; i--)
            {
                if (((DrawObject)graphicsList[i]).Selected)
                {
                    graphicsList.RemoveAt(i);
                    result = true;
                }
            }
            if (result)
                _isDirty = true;
            return result;
        }

        public void DeleteLastAddedObject()
        {
            if (graphicsList.Count > 0)
            {
                graphicsList.RemoveAt(0);
            }
        }

        public void Replace(int index, DrawObject obj)
        {
            if (index >= 0 &&
                index < graphicsList.Count)
            {
                graphicsList.RemoveAt(index);
                graphicsList.Insert(index, obj);
            }
        }

        public void RemoveAt(int index)
        {
            graphicsList.RemoveAt(index);
        }

        public bool MoveSelectionToFront()
        {
            int n;
            int i;
            ArrayList tempList;

            tempList = new ArrayList();
            n = graphicsList.Count;

            for (i = n - 1; i >= 0; i--)
            {
                if (((DrawObject)graphicsList[i]).Selected)
                {
                    tempList.Add(graphicsList[i]);
                    graphicsList.RemoveAt(i);
                }
            }

            n = tempList.Count;

            for (i = 0; i < n; i++)
            {
                graphicsList.Insert(0, tempList[i]);
            }
            if (n > 0)
                _isDirty = true;
            return (n > 0);
        }

        public bool MoveSelectionToBack()
        {
            int n;
            int i;
            ArrayList tempList;

            tempList = new ArrayList();
            n = graphicsList.Count;

            for (i = n - 1; i >= 0; i--)
            {
                if (((DrawObject)graphicsList[i]).Selected)
                {
                    tempList.Add(graphicsList[i]);
                    graphicsList.RemoveAt(i);
                }
            }

            n = tempList.Count;

            for (i = n - 1; i >= 0; i--)
            {
                graphicsList.Add(tempList[i]);
            }
            if (n > 0)
                _isDirty = true;
            return (n > 0);
        }

        public void LoadFromStream(SerializationInfo info, int orderNumber)
        {
            graphicsList = new ArrayList();

            int numberObjects = info.GetInt32(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}",
                              entryCount, orderNumber));

            for (int i = 0; i < numberObjects; i++)
            {
                string typeName;
                typeName = info.GetString(
                    String.Format(CultureInfo.InvariantCulture,
                                  "{0}{1}",
                                  entryType, i));

                object drawObject;
                drawObject = Assembly.GetExecutingAssembly().CreateInstance(
                    typeName);

                ((DrawObject)drawObject).LoadFromStream(info, orderNumber, i);

                graphicsList.Add(drawObject);
            }
        }

        public void SaveToStream(SerializationInfo info, int orderNumber)
        {
            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}",
                              entryCount, orderNumber),
                graphicsList.Count);
            int i = 0;
            foreach (DrawObject o in graphicsList)
            {
                info.AddValue(
                    String.Format(CultureInfo.InvariantCulture,
                                  "{0}{1}",
                                  entryType, i),
                    o.GetType().FullName);
                o.SaveToStream(info, orderNumber, i);
                i++;
            }
        }
    }
}
