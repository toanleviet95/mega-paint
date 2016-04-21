using System.Drawing;
using System.Windows.Forms;

namespace MegaPaint
{
    internal class EventText : EventObject
    {
        public EventText()
        {
            Cursor = new Cursor("../../Resources/Cursors/TextTool.cur");
        }

        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            Point p = drawArea.BackTrackMouse(new Point(e.X, e.Y));
            frmTextForm td = new frmTextForm();
            if (td.ShowDialog() == DialogResult.OK)
            {
                string t = td.TheText;
                Color c = td.TheColor;
                Color b = td.TheBackGround;
                Font f = td.TheFont;
                AddNewObject(drawArea, new DrawText(p.X, p.Y, t, f, c, b));
                drawArea.ActiveTool = DrawArea.DrawToolType.Pointer;
            }
            else
            {
                drawArea.ActiveTool = DrawArea.DrawToolType.Pointer;
            }
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
