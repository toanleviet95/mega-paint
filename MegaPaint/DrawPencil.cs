using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Globalization;
using System.Runtime.Serialization;
using System;

namespace MegaPaint
{
    // Class DrawPencil kế thừa từ class DrawLine
    class DrawPencil : DrawLine
    {
        private ArrayList pointArray;
        private Cursor handleCursor;

        private const string entryLength = "Length";
        private const string entryPoint = "Point";

        public DrawPencil()
        {
            pointArray = new ArrayList();
            LoadCursor();
            Initialize();
        }

        public DrawPencil(int x1, int y1, int x2, int y2, Color lineColor, int lineWidth, TypeOfPen.PenType p)
        {
            pointArray = new ArrayList();
            pointArray.Add(new Point(x1, y1));
            pointArray.Add(new Point(x2, y2));
            PenType = p;
            Color = lineColor;
            PenWidth = lineWidth;
            DrawPen = TypeOfPen.SetCurrentPen(PenType, Color, PenWidth);
            LoadCursor();
            Initialize();
        }
    
        // Vẽ tracker lên đối tượng select
        public override void DrawTracker(Graphics g)
        {
            if (!Selected)
                return;
            SolidBrush brush = new SolidBrush(Color.Black);
            g.FillRectangle(brush, GetHandleRectangle(1));
            g.FillRectangle(brush, GetHandleRectangle(HandleCount));
            brush.Dispose();
        }

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

        public override DrawObject Clone()
        {
            DrawPencil drawPolygon = new DrawPencil();

            foreach (Point p in pointArray)
            {
                drawPolygon.pointArray.Add(p);
            }

            FillDrawObjectFields(drawPolygon);
            return drawPolygon;
        }

        public override void Draw(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Pen pen;

            if (DrawPen == null)
                pen = new Pen(Color, PenWidth);
            else
                pen = DrawPen.Clone() as Pen;

            Point[] pts = new Point[pointArray.Count];
            for (int i = 0; i < pointArray.Count; i++)
            {
                Point px = (Point)pointArray[i];
                pts[i] = px;
            }
            byte[] types = new byte[pointArray.Count];
            for (int i = 0; i < pointArray.Count; i++)
                types[i] = (byte)PathPointType.Line;
            GraphicsPath gp = new GraphicsPath(pts, types);

            if (Rotation != 0)
            {
                RectangleF pathBounds = gp.GetBounds();
                Matrix m = new Matrix();
                m.RotateAt(Rotation, new PointF(pathBounds.Left + (pathBounds.Width / 2), pathBounds.Top + (pathBounds.Height / 2)), MatrixOrder.Append);
                gp.Transform(m);
            }
            g.DrawPath(pen, gp);
            gp.Dispose();
            if (pen != null)
                pen.Dispose();
        }

        public void AddPoint(Point point)
        {
            pointArray.Add(point);
        }

        public override int HandleCount
        {
            get { return pointArray.Count; }
        }

        public override Point GetHandle(int handleNumber)
        {
            if (handleNumber < 1)
                handleNumber = 1;

            if (handleNumber > pointArray.Count)
                handleNumber = pointArray.Count;

            return ((Point)pointArray[handleNumber - 1]);
        }

        public override Cursor GetHandleCursor(int handleNumber)
        {
            return handleCursor;
        }

        public override void MoveHandleTo(Point point, int handleNumber)
        {
            if (handleNumber < 1)
                handleNumber = 1;

            if (handleNumber > pointArray.Count)
                handleNumber = pointArray.Count;

            pointArray[handleNumber - 1] = point;
            Dirty = true;
            Invalidate();
        }

        public override void Move(int deltaX, int deltaY)
        {
            int n = pointArray.Count;

            for (int i = 0; i < n; i++)
            {
                Point point;
                point = new Point(((Point)pointArray[i]).X + deltaX, ((Point)pointArray[i]).Y + deltaY);

                pointArray[i] = point;
            }
            Dirty = true;
            Invalidate();
        }

        protected override void CreateObjects()
        {
            if (AreaPath != null)
                return;

            AreaPath = new GraphicsPath();

            int x1 = 0, y1 = 0; 

            IEnumerator enumerator = pointArray.GetEnumerator();

            if (enumerator.MoveNext())
            {
                x1 = ((Point)enumerator.Current).X;
                y1 = ((Point)enumerator.Current).Y;
            }

            while (enumerator.MoveNext())
            {
                int x2, y2; 
                x2 = ((Point)enumerator.Current).X;
                y2 = ((Point)enumerator.Current).Y;

                AreaPath.AddLine(x1, y1, x2, y2);

                x1 = x2;
                y1 = y2;
            }

            AreaPath.CloseFigure();
            AreaRegion = new Region(AreaPath);
        }

        private void LoadCursor()
        {
            handleCursor = new Cursor("../../Resources/Cursors/Pencil.cur");
        }

        public override void SaveToStream(SerializationInfo info, int orderNumber, int objectIndex)
        {
            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryLength, orderNumber, objectIndex),
                pointArray.Count);

            int i = 0;
            foreach (Point p in pointArray)
            {
                info.AddValue(
                    String.Format(CultureInfo.InvariantCulture,
                                  "{0}{1}-{2}-{3}",
                                  new object[] { entryPoint, orderNumber, objectIndex, i++ }),
                    p);
            }
            base.SaveToStream(info, orderNumber, objectIndex);
        }

        public override void LoadFromStream(SerializationInfo info, int orderNumber, int objectIndex)
        {
            int n = info.GetInt32(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryLength, orderNumber, objectIndex));

            for (int i = 0; i < n; i++)
            {
                Point point;
                point = (Point)info.GetValue(
                                   String.Format(CultureInfo.InvariantCulture,
                                                 "{0}{1}-{2}-{3}",
                                                 new object[] { entryPoint, orderNumber, objectIndex, i }),
                                   typeof(Point));

                pointArray.Add(point);
            }
            base.LoadFromStream(info, orderNumber, objectIndex);
        }
    }
}
