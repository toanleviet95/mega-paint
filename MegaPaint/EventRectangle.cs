using System.Drawing;
using System.Windows.Forms;

namespace MegaPaint
{
    internal class EventRectangle : EventObject
    {
        public EventRectangle()
        {
            Cursor = new Cursor("../../Resources/Cursors/Rectangle.cur");
        }

        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            Point p = drawArea.BackTrackMouse(new Point(e.X, e.Y));
            AddNewObject(drawArea, new DrawRectangle(p.X, p.Y, 1, 1, drawArea.LineColor, drawArea.LineWidth, drawArea.PenType, drawArea.FillColor, drawArea.FillImage, drawArea.DrawFilled, drawArea.DrawGradient, drawArea.DrawHatch, drawArea.DrawTexture));
        }

        public override void OnMouseMove(DrawArea drawArea, MouseEventArgs e)
        {
            drawArea.Cursor = Cursor;
            int al = drawArea.TheLayers.ActiveLayerIndex;
            if (e.Button == MouseButtons.Left && drawArea.TheLayers[al].Graphics.Count > 0)
            {
                Point point = drawArea.BackTrackMouse(new Point(e.X, e.Y));
                drawArea.TheLayers[al].Graphics[0].MoveHandleTo(point, 5);
                drawArea.Refresh();
            }
        }
    }
}
