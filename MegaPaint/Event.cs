using System.Windows.Forms;

namespace MegaPaint
{
    // Base class cho các event
    internal abstract class Event
    {
        // 3 event phổ biến để vẽ
		public virtual void OnMouseDown(DrawArea drawArea, MouseEventArgs e) { }

        public virtual void OnMouseMove(DrawArea drawArea, MouseEventArgs e) { }
        
        public virtual void OnMouseUp(DrawArea drawArea, MouseEventArgs e) { }
    }
}
