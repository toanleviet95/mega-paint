﻿using System;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Runtime.Serialization;

namespace MegaPaint
{
    // Layer là bề mặt chứa GraphicsList
    public class Layer
    {
        /* --- Thuộc tính --- */
        private string _name; // Tên layer
        private bool _isDirty;
        private bool _visible; // Có thể nhìn thấy không
        private bool _active; // Có được active không
        private GraphicsList _graphicsList; // Danh sách các graphic

        private const string entryLayerName = "LayerName";
        private const string entryLayerVisible = "LayerVisible";
        private const string entryLayerActive = "LayerActive";
        private const string entryObjectType = "ObjectType";
        private const string entryGraphicsCount = "GraphicsCount";

        public string LayerName
        {
            get { return _name; }
            set { _name = value; }
        }

        public GraphicsList Graphics
        {
            get { return _graphicsList; }
            set { _graphicsList = value; }
        }

        public bool IsVisible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        public bool IsActive
        {
            get { return _active; }
            set { _active = value; }
        }

        public bool Dirty
        {
            get
            {
                if (_isDirty == false)
                    _isDirty = _graphicsList.Dirty;
                return _isDirty;
            }
            set
            {
                _graphicsList.Dirty = false;
                _isDirty = false;
            }
        }

        public void SaveToStream(SerializationInfo info, int orderNumber)
        {
            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}",
                              entryLayerName, orderNumber),
                _name);

            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}",
                              entryLayerVisible, orderNumber),
                _visible);

            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}",
                              entryLayerActive, orderNumber),
                _active);

            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}",
                              entryGraphicsCount, orderNumber),
                _graphicsList.Count);

            for (int i = 0; i < _graphicsList.Count; i++)
            {
                object o = _graphicsList[i];
                info.AddValue(
                    String.Format(CultureInfo.InvariantCulture,
                                  "{0}{1}-{2}",
                                  entryObjectType, orderNumber, i),
                    o.GetType().FullName);

                ((DrawObject)o).SaveToStream(info, orderNumber, i);
            }
        }

        public void LoadFromStream(SerializationInfo info, int orderNumber)
        {
            _graphicsList = new GraphicsList();

            _name = info.GetString(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}",
                              entryLayerName, orderNumber));

            _visible = info.GetBoolean(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}",
                              entryLayerVisible, orderNumber));

            _active = info.GetBoolean(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}",
                              entryLayerActive, orderNumber));

            int n = info.GetInt32(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}",
                              entryGraphicsCount, orderNumber));

            for (int i = 0; i < n; i++)
            {
                string typeName;
                typeName = info.GetString(
                    String.Format(CultureInfo.InvariantCulture,
                                  "{0}{1}-{2}",
                                  entryObjectType, orderNumber, i));

                object drawObject;
                drawObject = Assembly.GetExecutingAssembly().CreateInstance(typeName);

                ((DrawObject)drawObject).LoadFromStream(info, orderNumber, i);

                _graphicsList.Append((DrawObject)drawObject);
            }
        }

        /*internal void Draw(Graphics g, Graphics gImage)
        {
            _graphicsList.Draw(g, gImage);
        }*/
    }
}
