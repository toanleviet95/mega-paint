using System.Drawing;
using System.Windows.Forms;

namespace MegaPaint
{
    internal class EventEllipse : EventRectangle
    {
        public EventEllipse()
        {
            Cursor = new Cursor("../../Resources/Cursors/Ellipse.cur");
        }

        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            Point p = drawArea.BackTrackMouse(new Point(e.X, e.Y));
            AddNewObject(drawArea, new DrawEllipse(p.X, p.Y, 1, 1, drawArea.LineColor, drawArea.LineWidth, drawArea.PenType, drawArea.FillColor, drawArea.FillImage, drawArea.DrawFilled, drawArea.DrawGradient, drawArea.DrawHatch, drawArea.DrawTexture));
        }
    }
}
