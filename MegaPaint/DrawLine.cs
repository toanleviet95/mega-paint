using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Globalization;
using System.Runtime.Serialization;
using System;

namespace MegaPaint
{
    // Class DrawLine kế thừa từ DrawObject
    public class DrawLine : DrawObject
    {
        /* --- Thuộc tính --- */
        private Point startPoint;
        private Point endPoint;

        private GraphicsPath areaPath = null;
        private Pen areaPen = null;
        private Region areaRegion = null;

        private const string entryStart = "Start";
        private const string entryEnd = "End";

        /* --- Phương thức --- */
        protected GraphicsPath AreaPath
        {
            get { return areaPath; }
            set { areaPath = value; }
        }

        protected Pen AreaPen
        {
            get { return areaPen; }
            set { areaPen = value; }
        }

        protected Region AreaRegion
        {
            get { return areaRegion; }
            set { areaRegion = value; }
        }

        public DrawLine()
        {
            startPoint.X = 0;
            startPoint.Y = 0;
            endPoint.X = 1;
            endPoint.Y = 1;
            ZOrder = 0;

            Initialize();
        }

        public DrawLine(int x1, int y1, int x2, int y2, Color lineColor, int lineWidth, TypeOfPen.PenType p)
        {
            startPoint.X = x1;
            startPoint.Y = y1;
            endPoint.X = x2;
            endPoint.Y = y2;
            Color = lineColor;
            PenWidth = lineWidth;
            PenType = p;
            DrawPen = TypeOfPen.SetCurrentPen(PenType, Color, PenWidth);
            ZOrder = 0;
            Initialize();
        }

        public override void Draw(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Pen pen;
            if (DrawPen == null)
                pen = new Pen(Color, PenWidth);
            else
                pen = (Pen)DrawPen.Clone();
            GraphicsPath gp = new GraphicsPath();
            gp.AddLine(startPoint, endPoint);
            // Rotate path so với tâm
            if (Rotation != 0)
            {
                RectangleF pathBounds = gp.GetBounds();
                Matrix m = new Matrix();
                m.RotateAt(Rotation, new PointF(pathBounds.Left + (pathBounds.Width / 2), pathBounds.Top + (pathBounds.Height / 2)), MatrixOrder.Append);
                gp.Transform(m);
            }
            g.DrawPath(pen, gp);
            gp.Dispose();
            pen.Dispose();
        }

        public override DrawObject Clone()
        {
            DrawLine drawLine = new DrawLine();
            drawLine.startPoint = startPoint;
            drawLine.endPoint = endPoint;

            FillDrawObjectFields(drawLine);
            return drawLine;
        }

        // Số handle cho Line
        public override int HandleCount
        {
            get { return 2; }
        }

        // Xử lý nối các handle xác định
        public override Point GetHandle(int handleNumber)
        {
            GraphicsPath gp = new GraphicsPath();
            Matrix m = new Matrix();
            gp.AddLine(startPoint, endPoint);
            RectangleF pathBounds = gp.GetBounds();
            m.RotateAt(Rotation, new PointF(pathBounds.Left + (pathBounds.Width / 2), pathBounds.Top + (pathBounds.Height / 2)), MatrixOrder.Append);
            gp.Transform(m);
            Point start, end;
            start = Point.Truncate(gp.PathPoints[0]);
            end = Point.Truncate(gp.PathPoints[1]);
            gp.Dispose();
            m.Dispose();
            if (handleNumber == 1)
                return start;
            else
                return end;
        }

        /* Kiểm tra có nhấn vào path area của line không
        Trả về -1: Không nhấn
                0: Nhấn ở chỗ khác
                > 1: Số handle
        */
        public override int HitTest(Point point)
        {
            if (Selected)
            {
                for (int i = 1; i <= HandleCount; i++)
                {
                    GraphicsPath gp = new GraphicsPath();
                    gp.AddRectangle(GetHandleRectangle(i));
                    bool vis = gp.IsVisible(point);
                    gp.Dispose();
                    if (vis)
                        return i;
                }
            }
            if (PointInObject(point))
                return 0;
            return -1;
        }

        protected override bool PointInObject(Point point)
        {
            CreateObjects();
            return AreaRegion.IsVisible(point);
        }

        public override bool IntersectsWith(Rectangle rectangle)
        {
            CreateObjects();
            return AreaRegion.IsVisible(rectangle);
        }

        public override Cursor GetHandleCursor(int handleNumber)
        {
            switch (handleNumber)
            {
                case 1:
                case 2:
                    return Cursors.SizeAll;
                default:
                    return Cursors.Default;
            }
        }

        public override void MoveHandleTo(Point point, int handleNumber)
        {
            if (handleNumber == 1)
                startPoint = point;
            else
                endPoint = point;

            Dirty = true;
            Invalidate();
        }

        public override void Move(int deltaX, int deltaY)
        {
            startPoint.X += deltaX;
            startPoint.Y += deltaY;

            endPoint.X += deltaX;
            endPoint.Y += deltaY;
            Dirty = true;
            Invalidate();
        }

        public override void SaveToStream(SerializationInfo info, int orderNumber, int objectIndex)
        {
            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryStart, orderNumber, objectIndex),
                startPoint);

            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryEnd, orderNumber, objectIndex),
                endPoint);

            base.SaveToStream(info, orderNumber, objectIndex);
        }

        public override void LoadFromStream(SerializationInfo info, int orderNumber, int objectIndex)
        {
            startPoint = (Point)info.GetValue(
                                    String.Format(CultureInfo.InvariantCulture,
                                                  "{0}{1}-{2}",
                                                  entryStart, orderNumber, objectIndex),
                                    typeof(Point));

            endPoint = (Point)info.GetValue(
                                String.Format(CultureInfo.InvariantCulture,
                                              "{0}{1}-{2}",
                                              entryEnd, orderNumber, objectIndex),
                                typeof(Point));

            base.LoadFromStream(info, orderNumber, objectIndex);
        }

        // Khi đối tượng đã được invalidate thì các area được giải phóng
        protected void Invalidate()
        {
            if (AreaPath != null)
            {
                AreaPath.Dispose();
                AreaPath = null;
            }

            if (AreaPen != null)
            {
                AreaPen.Dispose();
                AreaPen = null;
            }

            if (AreaRegion != null)
            {
                AreaRegion.Dispose();
                AreaRegion = null;
            }
        }

        // Tạo ra các đối tượng area 
        protected virtual void CreateObjects()
        {
            if (AreaPath != null)
                return;

            // Tạo path để phục vụ selection
            AreaPath = new GraphicsPath();
            // Tạo pen với độ dày thực tế
            AreaPen = new Pen(Color.Black, PenWidth < 7 ? 7 : PenWidth);
            // Tránh lỗi khi startPoint bằng endPoint
            if (startPoint.Equals((Point)endPoint))
            {
                endPoint.X++;
                endPoint.Y++;
            }
            AreaPath.AddLine(startPoint.X, startPoint.Y, endPoint.X, endPoint.Y);
            AreaPath.Widen(AreaPen);
            // Rotate path so với tâm
            if (Rotation != 0)
            {
                RectangleF pathBounds = AreaPath.GetBounds();
                Matrix m = new Matrix();
                m.RotateAt(Rotation, new PointF(pathBounds.Left + (pathBounds.Width / 2), pathBounds.Top + (pathBounds.Height / 2)), MatrixOrder.Append);
                AreaPath.Transform(m);
                m.Dispose();
            }

            // Tạo region từ path
            AreaRegion = new Region(AreaPath);
        }  
    }
}
