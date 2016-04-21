using System.Drawing;
using System.Windows.Forms;

namespace MegaPaint
{
    // LineEvent để vẽ line
    internal class EventLine : EventObject
    {
        public EventLine()
        {
            Cursor = new Cursor("../../Resources/Cursors/Line.cur");    
        }

        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            Point p = drawArea.BackTrackMouse(new Point(e.X, e.Y));
            AddNewObject(drawArea, new DrawLine(p.X, p.Y, p.X + 1, p.Y + 1, drawArea.LineColor, drawArea.LineWidth, drawArea.PenType));
        }

        public override void OnMouseMove(DrawArea drawArea, MouseEventArgs e)
        {
            drawArea.Cursor = Cursor;
            int al = drawArea.TheLayers.ActiveLayerIndex;
            if (e.Button == MouseButtons.Left && drawArea.TheLayers[al].Graphics.Count > 0)
            {
                Point point = drawArea.BackTrackMouse(new Point(e.X, e.Y));
                drawArea.TheLayers[al].Graphics[0].MoveHandleTo(point, 2);
                drawArea.Refresh();
            }
        }
    }
}
