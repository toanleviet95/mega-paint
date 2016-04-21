using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Globalization;
using System.Runtime.Serialization;
using System;

namespace MegaPaint
{
    public class DrawText : DrawObject
    {
        private Rectangle rectangle;
        private string _theText;
        private Font _font;

        private const string entryRectangle = "Rect";
        private const string entryText = "Text";
        private const string entryFontName = "FontName";
        private const string entryFontBold = "FontBold";
        private const string entryFontItalic = "FontItalic";
        private const string entryFontSize = "FontSize";
        private const string entryFontStrikeout = "FontStrikeout";
        private const string entryFontUnderline = "FontUnderline";

        protected Rectangle Rectangle
        {
            get { return rectangle; }
            set { rectangle = value; }
        }

        public DrawText()
        {
            _theText = "";
            Initialize();
        }

        public override DrawObject Clone()
        {
            DrawText drawText = new DrawText();

            drawText._font = _font;
            drawText._theText = _theText;
            drawText.rectangle = rectangle;

            FillDrawObjectFields(drawText);
            return drawText;
        }

        public DrawText(int x, int y, string textToDraw, Font textFont, Color textColor, Color textBackground)
        {
            rectangle.X = x;
            rectangle.Y = y;
            _theText = textToDraw;
            _font = textFont;
            Color = textColor;
            FillColor = textBackground;
            Initialize();
        }

        public override void Draw(Graphics g)
        {
            Brush b1 = new SolidBrush(Color);
            Brush b2 = new SolidBrush(FillColor);
            GraphicsPath gp1 = new GraphicsPath();
            GraphicsPath gp2 = new GraphicsPath();
            StringFormat format = StringFormat.GenericDefault; 
            gp1.AddString(_theText, _font.FontFamily, (int)_font.Style, _font.SizeInPoints, new PointF(Rectangle.X, Rectangle.Y), format);
            rectangle.Size = g.MeasureString(_theText, _font).ToSize();
            gp2.AddRectangle(rectangle);
            if (Rotation != 0)
            {
                RectangleF pathBounds = gp2.GetBounds();
                Matrix m = new Matrix();
                m.RotateAt(Rotation, new PointF(pathBounds.Left + (pathBounds.Width / 2), pathBounds.Top + (pathBounds.Height / 2)), MatrixOrder.Append);
                gp1.Transform(m);
                gp2.Transform(m);
            }
            g.FillPath(b2, gp2);
            g.FillPath(b1, gp1);
            b1.Dispose();
            b2.Dispose();
            gp1.Dispose();
            gp2.Dispose();
        }

        public override int HandleCount
        {
            get { return 8; }
        }

        public override Point GetHandle(int handleNumber)
        {
            int x, y, xCenter, yCenter;

            xCenter = rectangle.X + rectangle.Width / 2;
            yCenter = rectangle.Y + rectangle.Height / 2;
            x = rectangle.X;
            y = rectangle.Y;

            switch (handleNumber)
            {
                case 1:
                    x = rectangle.X;
                    y = rectangle.Y;
                    break;
                case 2:
                    x = xCenter;
                    y = rectangle.Y;
                    break;
                case 3:
                    x = rectangle.Right;
                    y = rectangle.Y;
                    break;
                case 4:
                    x = rectangle.Right;
                    y = yCenter;
                    break;
                case 5:
                    x = rectangle.Right;
                    y = rectangle.Bottom;
                    break;
                case 6:
                    x = xCenter;
                    y = rectangle.Bottom;
                    break;
                case 7:
                    x = rectangle.X;
                    y = rectangle.Bottom;
                    break;
                case 8:
                    x = rectangle.X;
                    y = yCenter;
                    break;
            }

            return new Point(x, y);
        }

        public override int HitTest(Point point)
        {
            if (Selected)
            {
                for (int i = 1; i <= HandleCount; i++)
                {
                    if (GetHandleRectangle(i).Contains(point))
                        return i;
                }
            }

            if (PointInObject(point))
                return 0;

            return -1;
        }


        protected override bool PointInObject(Point point)
        {
            return rectangle.Contains(point);
        }


        public override Cursor GetHandleCursor(int handleNumber)
        {
            return Cursors.Default;
        }

        public override bool IntersectsWith(Rectangle rectangle)
        {
            return Rectangle.IntersectsWith(rectangle);
        }

        public override void Move(int deltaX, int deltaY)
        {
            rectangle.X += deltaX;
            rectangle.Y += deltaY;
            Dirty = true;
        }

        public override void SaveToStream(SerializationInfo info, int orderNumber, int objectIndex)
        {
            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryRectangle, orderNumber, objectIndex),
                rectangle);
            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryText, orderNumber, objectIndex),
                _theText);
            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryFontName, orderNumber, objectIndex),
                _font.Name);
            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryFontBold, orderNumber, objectIndex),
                _font.Bold);
            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryFontItalic, orderNumber, objectIndex),
                _font.Italic);
            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryFontSize, orderNumber, objectIndex),
                _font.Size);
            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryFontStrikeout, orderNumber, objectIndex),
                _font.Strikeout);
            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryFontUnderline, orderNumber, objectIndex),
                _font.Underline);

            base.SaveToStream(info, orderNumber, objectIndex);
        }

        public override void LoadFromStream(SerializationInfo info, int orderNumber, int objectIndex)
        {
            rectangle = (Rectangle)info.GetValue(
                                    String.Format(CultureInfo.InvariantCulture,
                                                  "{0}{1}-{2}",
                                                  entryRectangle, orderNumber, objectIndex),
                                    typeof(Rectangle));
            _theText = info.GetString(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryText, orderNumber, objectIndex));
            string name = info.GetString(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryFontName, orderNumber, objectIndex));
            bool bold = info.GetBoolean(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryFontBold, orderNumber, objectIndex));
            bool italic = info.GetBoolean(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryFontItalic, orderNumber, objectIndex));
            float size = (float)info.GetValue(
                                    String.Format(CultureInfo.InvariantCulture,
                                                  "{0}{1}-{2}",
                                                  entryFontSize, orderNumber, objectIndex),
                                    typeof(float));
            bool strikeout = info.GetBoolean(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryFontStrikeout, orderNumber, objectIndex));
            bool underline = info.GetBoolean(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryFontUnderline, orderNumber, objectIndex));
            FontStyle fs = FontStyle.Regular;
            if (bold)
                fs |= FontStyle.Bold;
            if (italic)
                fs |= FontStyle.Italic;
            if (strikeout)
                fs |= FontStyle.Strikeout;
            if (underline)
                fs |= FontStyle.Underline;
            _font = new Font(name, size, fs);

            base.LoadFromStream(info, orderNumber, objectIndex);
        }
    }
}
