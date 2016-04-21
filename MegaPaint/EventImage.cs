using System;
using System.Drawing;
using System.Windows.Forms;

namespace MegaPaint
{
    internal class EventImage : EventObject
    {
        public EventImage()
        {
            Cursor = new Cursor("../../Resources/Cursors/Rectangle.cur");
        }

        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            Point p = drawArea.BackTrackMouse(new Point(e.X, e.Y));
            AddNewObject(drawArea, new DrawImage(p.X, p.Y));
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

        public override void OnMouseUp(DrawArea drawArea, MouseEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select an Image to insert";
            ofd.Filter = "Image files (*.jpg,*.png,*.bmp)|*.jpg;*.png;*.bmp|" +
                             "JPG files (*.jpg)|*.jpg|PNG files (*.png)|*.png|BMP files (*.bmp)|*.bmp";
            ofd.InitialDirectory = Environment.SpecialFolder.MyPictures.ToString();
            int al = drawArea.TheLayers.ActiveLayerIndex;
            if (ofd.ShowDialog() == DialogResult.OK)
                ((DrawImage)drawArea.TheLayers[al].Graphics[0]).TheImage = (Bitmap)Bitmap.FromFile(ofd.FileName);
            ofd.Dispose();
            base.OnMouseUp(drawArea, e);
        }
    }
}
