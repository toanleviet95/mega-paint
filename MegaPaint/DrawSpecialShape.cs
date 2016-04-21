using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Globalization;
using System.Runtime.Serialization;
using System;

namespace MegaPaint
{
    [Serializable]
    public class DrawSpecialShape : DrawEllipse
    {
        private const string entryShapeName = "ShapeName";
        public DrawSpecialShape()
        {
            SetRectangle(0, 0, 1, 1);
            Initialize();
        }

        public override DrawObject Clone()
        {
            DrawSpecialShape drawSpecialShapes = new DrawSpecialShape();
            drawSpecialShapes.Rectangle = Rectangle;

            FillDrawObjectFields(drawSpecialShapes);
            return drawSpecialShapes;
        }

        public DrawSpecialShape(int x, int y, int width, int height, Color lineColor, int lineWidth, TypeOfPen.PenType pType, Color fillColor, Image fillImage, bool filled, bool gradient, bool hatch, bool texture, SpecialShape.ShapeName shapeName)
        {
            Rectangle = new Rectangle(x, y, width, height);
            Center = new Point(x + (width / 2), y + (height / 2));
            PenType = pType;
            Color = lineColor;
            PenWidth = lineWidth;
            PenType = pType;
            ShapeName = shapeName;
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
            GraphicsPath gp = new GraphicsPath();
            Brush b = new SolidBrush(FillColor);
            Brush b1 = new LinearGradientBrush(new Point(0, 10), new Point(200, 10), Color.FromArgb(255, 255, 0, 0), Color.FromArgb(255, 0, 0, 255));
            Brush b2 = new HatchBrush(HatchStyle.Cross, Color.Azure);
            Brush b3 = new TextureBrush(FillImage);

            if (DrawPen == null)
                pen = new Pen(Color, PenWidth);
            else
                pen = (Pen)DrawPen.Clone();

            Rectangle rect = DrawRectangle.GetNormalizedRectangle(Rectangle);

            if (ShapeName == SpecialShape.ShapeName.Triangle)
            {
                PointF[] ListPoint =
                {
                     new PointF(rect.X + rect.Width/2, rect.Y),
                     new PointF(rect.X+rect.Width,rect.Y+rect.Height),
                     new PointF(rect.X,rect.Y+rect.Height),
                     new PointF(rect.X + rect.Width/2, rect.Y)
                };
                gp.AddLines(ListPoint);
            }
            else if (ShapeName == SpecialShape.ShapeName.RightTriangle)
            {
                PointF[] ListPoint =
                {
                         new PointF(rect.X, rect.Y),
                         new PointF(rect.X+rect.Width,rect.Y+rect.Height),
                         new PointF(rect.X,rect.Y+rect.Height),
                         new PointF(rect.X, rect.Y)
                };
                gp.AddLines(ListPoint);
            }
            else if (ShapeName == SpecialShape.ShapeName.RoundedRectangle)
            {
                int rad = 60;
                gp.AddArc(rect.X, rect.Y, rad, rad, 180, 90);
                gp.AddArc(rect.X + rect.Width - rad, rect.Y, rad, rad, 270, 90);
                gp.AddArc(rect.X + rect.Width - rad, rect.Y + rect.Height - rad, rad, rad, 0, 90);
                gp.AddArc(rect.X, rect.Y + rect.Height - rad, rad, rad, 90, 90);
                gp.AddLine(rect.X, rect.Y + rect.Height - rad, rect.X, rect.Y + rad / 2);
            }
            else if (ShapeName == SpecialShape.ShapeName.Arrow)
            {
                PointF[] ListPoint =
                {
                     new PointF(rect.X+rect.Width/2, rect.Y),
                     new PointF(rect.X+rect.Width, rect.Y+rect.Height/2),
                     new PointF(rect.X+rect.Width/2, rect.Y+rect.Height),
                     new PointF(rect.X+rect.Width/2, rect.Y+rect.Height*3/4),
                     new PointF(rect.X, rect.Y+rect.Height*3/4),
                     new PointF(rect.X, rect.Y+rect.Height/4),
                     new PointF(rect.X+rect.Width/2, rect.Y+rect.Height/4),
                     new PointF(rect.X+rect.Width/2, rect.Y)
                };
                gp.AddLines(ListPoint);
            }
            else if (ShapeName == SpecialShape.ShapeName.Star)
            {
                PointF[] ListPoint =
                {
                     new PointF(rect.X+rect.Width/2, rect.Y),
                     new PointF(rect.X+rect.Width*6/10, rect.Y+rect.Height*4/10),
                     new PointF(rect.X+rect.Width, rect.Y+rect.Height*4/10),
                     new PointF(rect.X+rect.Width*65/100, rect.Y+rect.Height*65/100),
                     new PointF(rect.X+rect.Width*3/4, rect.Y+rect.Height),
                     new PointF(rect.X+rect.Width/2, rect.Y+rect.Height*78/100),
                     new PointF(rect.X+rect.Width/4, rect.Y+rect.Height),
                     new PointF(rect.X+rect.Width*35/100, rect.Y+rect.Height*65/100),
                     new PointF(rect.X, rect.Y+rect.Height*4/10),
                     new PointF(rect.X+rect.Width*4/10, rect.Y+rect.Height*4/10),
                     new PointF(rect.X+rect.Width/2, rect.Y)
                };
                gp.AddLines(ListPoint);
            }
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

        public override void SaveToStream(SerializationInfo info, int orderNumber, int objectIndex)
        {
            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryShapeName, orderNumber, objectIndex),
                ShapeName);

            base.SaveToStream(info, orderNumber, objectIndex);
        }

        public override void LoadFromStream(SerializationInfo info, int orderNumber, int objectIndex)
        {
            ShapeName = (SpecialShape.ShapeName)info.GetValue(
                                    String.Format(CultureInfo.InvariantCulture,
                                                  "{0}{1}-{2}",
                                                  entryShapeName, orderNumber, objectIndex),
                                    typeof(SpecialShape.ShapeName));

            base.LoadFromStream(info, orderNumber, objectIndex);
        }

    }
}

