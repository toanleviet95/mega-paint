using System.Drawing;
using System.Windows.Forms;

namespace MegaPaint
{
    internal class EventSpecialShape:EventEllipse
    {
        public EventSpecialShape()
        {
            Cursor = new Cursor("../../Resources/Cursors/CustomShape.cur");
        }

        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            Point p = drawArea.BackTrackMouse(new Point(e.X, e.Y));
            AddNewObject(drawArea, new DrawSpecialShape(p.X, p.Y, 1, 1, drawArea.LineColor, drawArea.LineWidth, drawArea.PenType, drawArea.FillColor, drawArea.FillImage, drawArea.DrawFilled, drawArea.DrawGradient, drawArea.DrawHatch, drawArea.DrawTexture, drawArea.ShapeName));
        }
    }
}
