using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MegaPaint
{
    [Serializable]
    // Class DrawEllipse kế thừa từ class DrawRectangle
    public class DrawEllipse : DrawRectangle
    {
        public DrawEllipse()
        {
            SetRectangle(0, 0, 1, 1);
            Initialize();
        }

        public override DrawObject Clone()
        {
            DrawEllipse drawEllipse = new DrawEllipse();
            drawEllipse.Rectangle = Rectangle;

            FillDrawObjectFields(drawEllipse);
            return drawEllipse;
        }

        public DrawEllipse(int x, int y, int width, int height, Color lineColor, int lineWidth, TypeOfPen.PenType pType, Color fillColor, Image fillImage, bool filled, bool gradient, bool hatch, bool texture)
        {
            Rectangle = new Rectangle(x, y, width, height);
            Center = new Point(x + (width / 2), y + (height / 2));
            PenType = pType;
            Color = lineColor;
            PenWidth = lineWidth;
            PenType = pType;
            DrawPen = TypeOfPen.SetCurrentPen(PenType, Color, PenWidth);
            FillColor = fillColor;
            FillImage = fillImage;
            Filled = filled;
            Gradient = gradient;
            Hatch = hatch;
            Texture = texture;
            Initialize();
        }

        public override void Draw(Graphics g)
        {
            Pen pen;
            Brush b = new SolidBrush(FillColor);
            Brush b1 = new LinearGradientBrush(new Point(0, 10), new Point(200, 10), Color.FromArgb(255, 255, 0, 0), Color.FromArgb(255, 0, 0, 255));
            Brush b2 = new HatchBrush(HatchStyle.Cross, Color.Azure);
            Brush b3 = new TextureBrush(FillImage);

            if (DrawPen == null)
                pen = new Pen(Color, PenWidth);
            else
                pen = (Pen)DrawPen.Clone();
            GraphicsPath gp = new GraphicsPath();
            gp.AddEllipse(GetNormalizedRectangle(Rectangle));
            if (Rotation != 0)
            {
                RectangleF pathBounds = gp.GetBounds();
                Matrix m = new Matrix();
                m.RotateAt(Rotation, new PointF(pathBounds.Left + (pathBounds.Width / 2), pathBounds.Top + (pathBounds.Height / 2)), MatrixOrder.Append);
                gp.Transform(m);
            }
            g.DrawPath(pen, gp);
            if (Filled)
                g.FillPath(b, gp);
            if (Gradient)
                g.FillPath(b1, gp);
            if (Hatch)
                g.FillPath(b2, gp);
            if (Texture)
                g.FillPath(b3, gp);
            gp.Dispose();
            pen.Dispose();
            b.Dispose();
            b1.Dispose();
            b2.Dispose();
            b3.Dispose();
        }
    }
}
