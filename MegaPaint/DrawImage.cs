using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Globalization;
using System.Runtime.Serialization;
using System;

namespace MegaPaint
{
    // Lớp DrawImage kế thừa từ DrawObject với mục đích là vẽ ra một ảnh có sẵn từ máy tính với kích thước tùy chỉnh 
    public class DrawImage : DrawObject
    {
        private Rectangle rectangle;
        private Bitmap _image;
        private Bitmap _originalImage;

        private const string entryRectangle = "Rect";
        private const string entryImage = "Image";
        private const string entryImageOriginal = "OriginalImage";

        public Bitmap TheImage
        {
            get { return _image; }
            set
            {
                _originalImage = value;
                ResizeImage(rectangle.Width, rectangle.Height);
            }
        }

        public override DrawObject Clone()
        {
            DrawImage drawImage = new DrawImage();
            drawImage._image = _image;
            drawImage._originalImage = _originalImage;
            drawImage.rectangle = rectangle;

            FillDrawObjectFields(drawImage);
            return drawImage;
        }

        protected Rectangle Rectangle
        {
            get { return rectangle; }
            set { rectangle = value; }
        }

        public DrawImage()
        {
            SetRectangle(0, 0, 1, 1);
            Initialize();
        }

        public DrawImage(int x, int y)
        {
            rectangle.X = x;
            rectangle.Y = y;
            rectangle.Width = 1;
            rectangle.Height = 1;
            Initialize();
        }

        public override void Draw(Graphics g)
        {
            if (Rotation != 0)
            {
                Matrix mSave = g.Transform;
                Matrix m = mSave.Clone();
                m.RotateAt(Rotation, new PointF(rectangle.Left + (rectangle.Width / 2), rectangle.Top + (rectangle.Height / 2)), MatrixOrder.Append);
                g.Transform = m;
            }
            if (_image != null)
            {
                if (rectangle.Width < 0)
                {
                    rectangle.Width = 0;
                }
                if (rectangle.Height < 0)
                {
                    rectangle.Height = 0;
                }
                g.DrawImage(_image, new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height));
            }
        }

        protected void SetRectangle(int x, int y, int width, int height)
        {
            rectangle.X = x;
            rectangle.Y = y;
            rectangle.Width = width;
            rectangle.Height = height;   
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
            switch (handleNumber)
            {
                case 1:
                    return Cursors.SizeNWSE;
                case 2:
                    return Cursors.SizeNS;
                case 3:
                    return Cursors.SizeNESW;
                case 4:
                    return Cursors.SizeWE;
                case 5:
                    return Cursors.SizeNWSE;
                case 6:
                    return Cursors.SizeNS;
                case 7:
                    return Cursors.SizeNESW;
                case 8:
                    return Cursors.SizeWE;
                default:
                    return Cursors.Default;
            }
        }

        
        public override void MoveHandleTo(Point point, int handleNumber)
        {
            int left = Rectangle.Left;
            int top = Rectangle.Top;
            int right = Rectangle.Right;
            int bottom = Rectangle.Bottom;

            switch (handleNumber)
            {
                case 1:
                    left = point.X;
                    top = point.Y;
                    break;
                case 2:
                    top = point.Y;
                    break;
                case 3:
                    right = point.X;
                    top = point.Y;
                    break;
                case 4:
                    right = point.X;
                    break;
                case 5:
                    right = point.X;
                    bottom = point.Y;
                    break;
                case 6:
                    bottom = point.Y;
                    break;
                case 7:
                    left = point.X;
                    bottom = point.Y;
                    break;
                case 8:
                    left = point.X;
                    break;
            }
            Dirty = true;
            SetRectangle(left, top, right - left, bottom - top);
            ResizeImage(rectangle.Width, rectangle.Height);
        }

        protected void ResizeImage(int width, int height)
        {
            if (_originalImage != null)
            {
                if (width < 5)
                {
                    if (width < 0)
                    {
                        width = width * (-1);
                    }
                    else
                    {
                        width = 5;
                    }
                }
                if(height < 5)
                {
                    if (height < 0)
                    {
                        height = height * (-1);
                    }
                    else
                    {
                        height = 5;
                    }
                }
                Bitmap b = new Bitmap(_originalImage, new Size(width, height));
                _image = (Bitmap)b.Clone();
                b.Dispose();
            }
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

       
        public override void Normalize()
        {
            rectangle = DrawRectangle.GetNormalizedRectangle(rectangle);
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
                              entryImage, orderNumber, objectIndex),
                _image);
            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryImageOriginal, orderNumber, objectIndex),
                _originalImage);

            base.SaveToStream(info, orderNumber, objectIndex);
        }

        public override void LoadFromStream(SerializationInfo info, int orderNumber, int objectIndex)
        {
            rectangle = (Rectangle)info.GetValue(
                                    String.Format(CultureInfo.InvariantCulture,
                                                  "{0}{1}-{2}",
                                                  entryRectangle, orderNumber, objectIndex),
                                    typeof(Rectangle));
            _image = (Bitmap)info.GetValue(
                                String.Format(CultureInfo.InvariantCulture,
                                              "{0}{1}-{2}",
                                              entryImage, orderNumber, objectIndex),
                                typeof(Bitmap));
            _originalImage = (Bitmap)info.GetValue(
                                        String.Format(CultureInfo.InvariantCulture,
                                                      "{0}{1}-{2}",
                                                      entryImageOriginal, orderNumber, objectIndex),
                                        typeof(Bitmap));

            base.LoadFromStream(info, orderNumber, objectIndex);
        }
    }
}
