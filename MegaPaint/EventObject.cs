using System.Windows.Forms;

namespace MegaPaint
{
    // Base class cho các event dùng cho việc tạo mới đối tượng
    internal abstract class EventObject : Event
    {
        /* --- Thuộc tính --- */
        private Cursor cursor;

        protected Cursor Cursor
        {
            get { return cursor; }
            set { cursor = value; }
        }

        /* --- Phương thức --- */
        public override void OnMouseUp(DrawArea drawArea, MouseEventArgs e)
        {
            int al = drawArea.TheLayers.ActiveLayerIndex;
            if (drawArea.TheLayers[al].Graphics.Count > 0)
                drawArea.TheLayers[al].Graphics[0].Normalize();
            drawArea.Capture = false;
            drawArea.Refresh();
        }

        // Hàm này được gọi khi user click trái vào draw area và một tool được active
        public void AddNewObject(DrawArea drawArea, DrawObject o)
        {
            int al = drawArea.TheLayers.ActiveLayerIndex;
            drawArea.TheLayers[al].Graphics.UnselectAll();

            o.Selected = true;
            o.Dirty = true;
            int objectID = 0;
            // Lấy id của đối tượng
            for (int i = 0; i < drawArea.TheLayers.Count; i++)
            {
                objectID = +drawArea.TheLayers[i].Graphics.Count;
            }
            objectID++;
            o.ID = objectID;
            drawArea.TheLayers[al].Graphics.Add(o);

            drawArea.Capture = true;
            drawArea.Refresh();
        }
    }
}
