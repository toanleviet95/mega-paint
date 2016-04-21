using System;
using System.Collections;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Windows;

namespace MegaPaint
{
    [Serializable]
    // Layers gồm nhiều Layer
    public class Layers : ISerializable
    {
        /* --- Thuộc tính --- */
        private ArrayList layerList; // Danh sách các layer

        //private bool _isDirty;

        private const string entryCount = "LayerCount";
        private const string entryLayer = "LayerType";

        /*public bool Dirty
        {
            get
            {
                if (_isDirty == false)
                {
                    foreach (Layer l in layerList)
                    {
                        if (l.Dirty)
                        {
                            _isDirty = true;
                            break;
                        }
                    }
                }
                return _isDirty;
            }
        }*/

        // Trả về index của layer được active
        public int ActiveLayerIndex
        {
            get
            {
                int i = 0;
                foreach (Layer l in layerList)
                {
                    if (l.IsActive)
                        break;
                    i++;
                }
                return i;
            }
        }

        public int Count
        {
            get { return layerList.Count; }
        }

        public Layer this[int index]
        {
            get
            {
                if (index < 0 ||
                    index >= layerList.Count)
                    return null;
                return (Layer)layerList[index];
            }
        }

        /* --- Phương thức --- */
        public Layers()
        {
            layerList = new ArrayList();
        }

        /*public void Draw(Graphics g, Graphics gImage)
        {
            foreach (Layer l in layerList)
            {
                if (l.IsVisible)
                    l.Draw(g, gImage);
            }
        }*/

        public void Add(Layer obj)
        {
            layerList.Add(obj);
        }

        public void CreateNewLayer(string theName)
        {
            if (layerList.Count > 0)
                ((Layer)layerList[ActiveLayerIndex]).IsActive = false;
            Layer l = new Layer();
            l.IsVisible = true;
            l.IsActive = true;
            l.LayerName = theName;
            l.Graphics = new GraphicsList();
            Add(l);
        }

        public bool Clear()
        {
            bool result = (layerList.Count > 0);
            foreach (Layer l in layerList)
                l.Graphics.Clear();

            layerList.Clear();
            CreateNewLayer("Default");

            /*if (result)
                _isDirty = false;*/
            return result;
        }

        public void InactivateAllLayers()
        {
            foreach (Layer l in layerList)
            {
                l.IsActive = false;
                if (l.Graphics != null)
                    l.Graphics.UnselectAll();
            }
        }

        public void MakeLayerVisible(int p)
        {
            if (p > -1 &&
                p < layerList.Count)
                ((Layer)layerList[p]).IsVisible = true;
        }

        public void MakeLayerInvisible(int p)
        {
            if (p > -1 && p < layerList.Count)
                ((Layer)layerList[p]).IsVisible = false;
        }

        public void SetActiveLayer(int p)
        {
            if (p > -1 && p < layerList.Count)
            {
                ((Layer)layerList[p]).IsActive = true;
                ((Layer)layerList[p]).IsVisible = true;
            }
        }

        public void RemoveLayer(int p)
        {
            if (ActiveLayerIndex == p)
            {
                MessageBox.Show("Cannot remove the Active Layer", "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (layerList.Count == 1)
            {
                MessageBox.Show("There must be at least ONE Layer in this drawing! You cannot remove ALL Layer!", "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (p > -1 && p < layerList.Count)
            {
                ((Layer)layerList[p]).Graphics.Clear();
                layerList.RemoveAt(p);
            }
        }

        protected Layers(SerializationInfo info, StreamingContext context)
        {
            layerList = new ArrayList();

            int n = info.GetInt32(entryCount);

            for (int i = 0; i < n; i++)
            {
                string typeName;
                typeName = info.GetString(
                    String.Format(CultureInfo.InvariantCulture,
                                  "{0}{1}",
                                  entryLayer, i));

                object _layer;
                _layer = Assembly.GetExecutingAssembly().CreateInstance(typeName);
                ((Layer)_layer).LoadFromStream(info, i);
                layerList.Add(_layer);
            }
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(entryCount, layerList.Count);

            int i = 0;

            foreach (Layer l in layerList)
            {
                info.AddValue(
                    String.Format(CultureInfo.InvariantCulture,
                                  "{0}{1}",
                                  entryLayer, i),
                    l.GetType().FullName);

                l.SaveToStream(info, i);
                i++;
            }
        }
    }
}
