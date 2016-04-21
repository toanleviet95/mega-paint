using System;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;
using System.Runtime.Serialization;
using System.Drawing.Drawing2D;

namespace MegaPaint
{
    [Serializable]
    // DrawObject là Base Class cho các đối tượng vẽ
    public abstract class DrawObject : IComparable
    {
        /* --- CÁC THUỘC TÍNH --- */
        #region Members
        private bool selected;
        private bool gradient;
        private bool filled;
        private bool hatch;
        private bool texture;

        private Color color;
        private Color fillColor;
        private Image fillImage;
        private Pen drawpen;
        private TypeOfPen.PenType _penType;
        private int penWidth;

        private static Color lastUsedColor = Color.Black;
        private static int lastUsedPenWidth = 1;

        private bool dirty;
        private int _id;
        private int _zOrder;
        private int _rotation = 0;
        private Point _center;

        private SpecialShape.ShapeName _shapeName;

        // Các entry phục vụ cho việc mã hóa nhị phân để lưu đối tượng
        private const string entryColor = "Color";
        private const string entryPenWidth = "PenWidth";
        private const string entryPen = "PenType";
        private const string entryFillColor = "FillColor";
        private const string entryFillImage = "FillImage";
        private const string entryFilled = "Filled";
        private const string entryGradient = "Gradient";
        private const string entryHatch = "Hatch";
        private const string entryTexture = "Texture";
        private const string entryZOrder = "ZOrder";
        private const string entryRotation = "Rotation";
        #endregion Members

        #region Properties
        // Lấy tâm đối tượng
        public Point Center
        {
            get { return _center; }
            set { _center = value; }
        }

        // Xoay đối tượng theo độ. Âm là trái, dương là phải
        public int Rotation
        {
            get { return _rotation; }
            set
            {
                if (value > 360)
                    _rotation = value - 360;
                else if (value < -360)
                    _rotation = value + 360;
                else
                    _rotation = value;
            }
        }

        // ZOrder quyết định bề nổi của đối tượng
        public int ZOrder
        {
            get { return _zOrder; }
            set { _zOrder = value; }
        }

        // ID dùng cho Redo và Undo
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        // Dirty = true khi đối tượng có sự thay đổi
        public bool Dirty
        {
            get { return dirty; }
            set { dirty = value; }
        }

        // Kiểm tra đối tượng đã Fill chưa
        public bool Filled
        {
            get { return filled; }
            set { filled = value; }
        }

        // Kiểm tra đối tượng đã Gradient chưa
        public bool Gradient
        {
            get { return gradient; }
            set { gradient = value; }
        }

        // Kiểm tra đối tượng đã Hatch chưa
        public bool Hatch
        {
            get { return hatch; }
            set { hatch = value; }
        }

        // Kiểm tra đối tượng đã Hatch chưa
        public bool Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        // Kiểm tra đối tượng đã Select chưa
        public bool Selected
        {
            get { return selected; }
            set { selected = value; }
        }

        // Fill color
        public Color FillColor
        {
            get { return fillColor; }
            set { fillColor = value; }
        }

        // Fill Image
        public Image FillImage
        {
            get { return fillImage; }
            set { fillImage = value; }
        }

        // Border color
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        /// Pen width
        public int PenWidth
        {
            get { return penWidth; }
            set { penWidth = value; }
        }

        // Pen type
        public TypeOfPen.PenType PenType
        {
            get { return _penType; }
            set { _penType = value; }
        }

        // Pen được dùng để vẽ đối tượng
        public Pen DrawPen
        {
            get { return drawpen; }
            set { drawpen = value; }
        }

        public SpecialShape.ShapeName ShapeName
        {
            get { return _shapeName; }
            set { _shapeName = value; }
        }

        // Số lượng Handle 
        public virtual int HandleCount
        {
            get { return 0; }
        }

        // Số lượng Connection
        public virtual int ConnectionCount
        {
            get { return 0; }
        }
        #endregion Properties

        /* --- CÁC PHƯƠNG THỨC ẢO --- */
        #region Virtual Functions
        // Clone đối tượng
        public abstract DrawObject Clone();

        // Vẽ đối tượng
        public virtual void Draw(Graphics g)
        {
        }

        // Hàm xử lý Selection
        #region Selection handle methods
        // Handle điểm
        public virtual Point GetHandle(int handleNumber)
        {
            return new Point(0, 0);
        }

        // Handle hình chữ nhật anchor 4 góc
        public virtual Rectangle GetHandleRectangle(int handleNumber)
        {
            Point point = GetHandle(handleNumber);
            return new Rectangle(point.X - (penWidth + 3), point.Y - (penWidth + 3), 7 + penWidth, 7 + penWidth);
        }

        // Vẽ tracker lên đối tượng select
        public virtual void DrawTracker(Graphics g)
        {
            if (!Selected)
                return;
            SolidBrush brush = new SolidBrush(Color.Black);
            for (int i = 1; i <= HandleCount; i++)
            {
                g.FillRectangle(brush, GetHandleRectangle(i));
            }
            brush.Dispose();
        }
        #endregion Selection handle methods

        // Hàm connect các point
        #region Connection Point methods
        // Connect điểm
        public virtual Point GetConnection(int connectionNumber)
        {
            return new Point(0, 0);
        }
        #endregion Connection Point methods
        
        // Kiểm tra có nhấn chuột vào đối tượng vẽ không
        public virtual int HitTest(Point point)
        {
            return -1;
        }

        // Điểm có nằm trong đối tượng không
        protected virtual bool PointInObject(Point point)
        {
            return false;
        }


        // Lấy con trỏ xử lý
        public virtual Cursor GetHandleCursor(int handleNumber)
        {
            return Cursors.Default;
        }

        // Kiểm tra đối tượng có giao với hình chữ nhật
        public virtual bool IntersectsWith(Rectangle rectangle)
        {
            return false;
        }

        /* Di chuyển đối tượng
        Theo trục X: dương = phải, âm = trái
        Theo trục Y: dương = xuống, âm = lên
        */
        public virtual void Move(int deltaX, int deltaY){}

        // Di chuyển handle đến điểm
        public virtual void MoveHandleTo(Point point, int handleNumber){}

        // Normalize được gọi khi resize xong đối tượng
        public virtual void Normalize(){}

        #region Save / Load methods
        public virtual void SaveToStream(SerializationInfo info, int orderNumber, int objectIndex)
        {
            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryColor, orderNumber, objectIndex),
                Color.ToArgb());

            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryPenWidth, orderNumber, objectIndex),
                PenWidth);

            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryPen, orderNumber, objectIndex),
                PenType);

            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryFillColor, orderNumber, objectIndex),
                FillColor.ToArgb());

            info.AddValue(
               String.Format(CultureInfo.InvariantCulture,
                             "{0}{1}-{2}",
                             entryFillImage, orderNumber, objectIndex),
               FillImage);

            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryFilled, orderNumber, objectIndex),
                Filled);

            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryGradient, orderNumber, objectIndex),
                Gradient);

            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryHatch, orderNumber, objectIndex),
                Hatch);

            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryTexture, orderNumber, objectIndex),
                Texture);

            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryZOrder, orderNumber, objectIndex),
                ZOrder);

            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryRotation, orderNumber, objectIndex),
                Rotation);
        }

        public virtual void LoadFromStream(SerializationInfo info, int orderNumber, int objectData)
        {
            int n = info.GetInt32(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryColor, orderNumber, objectData));
            Color = Color.FromArgb(n);

            PenWidth = info.GetInt32(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryPenWidth, orderNumber, objectData));
 
            PenType = (TypeOfPen.PenType)info.GetValue(
                                            String.Format(CultureInfo.InvariantCulture,
                                                          "{0}{1}-{2}",
                                                          entryPen, orderNumber, objectData),
                                            typeof(TypeOfPen.PenType));

            n = info.GetInt32(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryFillColor, orderNumber, objectData));

            FillColor = Color.FromArgb(n);

            FillImage = (Bitmap)info.GetValue(
                                String.Format(CultureInfo.InvariantCulture,
                                              "{0}{1}-{2}",
                                              entryFillImage, orderNumber, objectData),
                                typeof(Bitmap));

            Filled = info.GetBoolean(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryFilled, orderNumber, objectData));

            Gradient = info.GetBoolean(
              String.Format(CultureInfo.InvariantCulture,
                            "{0}{1}-{2}",
                            entryGradient, orderNumber, objectData));

            Hatch = info.GetBoolean(
              String.Format(CultureInfo.InvariantCulture,
                            "{0}{1}-{2}",
                            entryHatch, orderNumber, objectData));

            Texture = info.GetBoolean(
              String.Format(CultureInfo.InvariantCulture,
                            "{0}{1}-{2}",
                            entryTexture, orderNumber, objectData));

            ZOrder = info.GetInt32(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryZOrder, orderNumber, objectData));

            Rotation = info.GetInt32(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryRotation, orderNumber, objectData));
            if (PenType != TypeOfPen.PenType.Normal)
                DrawPen = TypeOfPen.SetCurrentPen(PenType, Color, PenWidth);
        }
        #endregion Save/Load methods

        #endregion Virtual Functions

        /* --- CÁC PHƯƠNG THỨC KHÁC --- */
        #region Other functions
        protected void Initialize(){}

        // Đổ dữ liệu trong field từ đối tượng này đến đối tượng Clone
        protected void FillDrawObjectFields(DrawObject drawObject)
        {
            drawObject.selected = selected;
            drawObject.color = color;
            drawObject.penWidth = penWidth;
            drawObject.ID = ID;
            drawObject._penType = _penType;
            drawObject._shapeName = _shapeName;
            drawObject.drawpen = drawpen;
            drawObject.filled = filled;
            drawObject.gradient = gradient;
            drawObject.hatch = hatch;
            drawObject.texture = texture;
            drawObject.fillImage = fillImage;
            drawObject.fillColor = fillColor;
            drawObject._rotation = _rotation;
            drawObject._center = _center;
        }
        #endregion Other functions

        #region IComparable Members
        /* So sánh với ZOrder để xem đối tượng nào nổi lên
           Trả về -1: Nằm dưới
           Trả về 0: Nằm ngang nhau
           Trả về 1: Nằm trên
        */
        public int CompareTo(object obj)
        {
            DrawObject d = obj as DrawObject;
            int x = 0;
            if (d != null)
                if (d.ZOrder == ZOrder)
                    x = 0;
                else if (d.ZOrder > ZOrder)
                    x = -1;
                else
                    x = 1;
            return x;
        }
        #endregion IComparable Members
    }
}

