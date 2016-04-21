using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization;
using System.Security;

namespace MegaPaint
{
    public partial class DrawArea : UserControl
    {
        
        #region Constructor
        public DrawArea()
        {
            // Tạo ra danh sách layer với một layer mặc định visible, khởi tạo chỉ có một layer
            _layers = new Layers();
            _layers.CreateNewLayer("Default Layer");

            // PanX và PanY để zoom theo chiều dọc
            _panX = 0;
            _panY = 0;
            _panning = false;

            // Lăn Mouse Wheel để zoom hình ảnh
            MouseWheel += new MouseEventHandler(DrawArea_MouseWheel);

            InitializeComponent();
        }
        #endregion Constructor

        // Các enum phân biệt tool vẽ
        #region Enumerations
        public enum DrawToolType
        {
            Pointer,
            Rectangle,
            Ellipse,
            Line,
            Polygon,
            Text,
            Image,
            SpecialShape,
            NumberOfDrawTools
        };
        #endregion Enumerations

        /* CÁC THUỘC TÍNH */
        #region Members
        // Zoom
        private float _zoom = 1.0f;
        private int _panX;
        private int _panY;
        private bool _panning;

        // Resize DrawArea
        private bool _resizing;
        internal bool MouseIsInRightEdge { get; set; }
        internal bool MouseIsInBottomEdge { get; set; }
        private Size _currentControlStartSize;
        private Point _cursorStartPoint;

        // Rotate
        private float _rotation = 0f;

        // Tool vẽ
        private Event[] tools;
        private DrawToolType activeTool;
        private Color _lineColor = Color.Black;
        private Color _fillColor = Color.White;
        private bool _drawGradient = false;
        private bool _drawFilled = false;
        private bool _drawHatch = false;
        private bool _drawTexture = false;
        private Image _fillImage = new Bitmap(1, 1, PixelFormat.Format24bppRgb);
        private int _lineWidth = 2;
        private Pen _currentPen;
        private TypeOfPen.PenType _penType;
        private Image _image;
        private SpecialShape.ShapeName _shapeName;

        // Layer
        private Layers _layers;

        // Anchor select
        private Rectangle netRectangle;
        private bool drawNetRectangle = false;
        private Point lastPoint;

        // Undo và Redo
        private Undo_Redo undoManager;
        private GraphicsList objCopyList;
        
        // Save và Open
        private Save_Open docManager;
        
        // Dùng phím keyboard
        private bool _controlKey = false;
        #endregion Members

        /* KHỞI TẠO CHO CÁC THUỘC TÍNH */
        #region Properties
        public TypeOfPen.PenType PenType
        {
            get { return _penType; }
            set { _penType = value; }
        }

        public SpecialShape.ShapeName ShapeName
        {
            get { return _shapeName; }
            set { _shapeName = value; }
        }

        public GraphicsList ObjCopyList
        {
            get { return objCopyList; }
            set { objCopyList = value; }
        }

        public Save_Open DocManager
        {
            get { return docManager; }
            set { docManager = value; }
        }

        public Pen CurrentPen
        {
            get { return _currentPen; }
            set { _currentPen = value; }
        }

        public int LineWidth
        {
            get { return _lineWidth; }
            set { _lineWidth = value; }
        }

        public bool DrawFilled
        {
            get { return _drawFilled; }
            set { _drawFilled = value; }
        }

        public bool DrawGradient
        {
            get { return _drawGradient; }
            set { _drawGradient = value; }
        }

        public bool DrawHatch
        {
            get { return _drawHatch; }
            set { _drawHatch = value; }
        }

        public bool DrawTexture
        {
            get { return _drawTexture; }
            set { _drawTexture = value; }
        }

        public Color FillColor
        {
            get { return _fillColor; }
            set { _fillColor = value; }
        }

        public Image FillImage
        {
            get { return _fillImage; }
            set { _fillImage = value; }
        }

        public Color LineColor
        {
            get { return _lineColor; }
            set { _lineColor = value; }
        }

        public float Rotation
        {
            get { return _rotation; }
            set { _rotation = value; }
        }

        public int PanX
        {
            get { return _panY; }
            set { _panY = value; }
        }

        public int PanY
        {
            get { return _panY; }
            set { _panY = value; }
        }

        public float Zoom
        {
            get { return _zoom; }
            set { _zoom = value; }
        }

        public Rectangle NetRectangle
        {
            get { return netRectangle; }
            set { netRectangle = value; }
        }

        public bool DrawNetRectangle
        {
            get { return drawNetRectangle; }
            set { drawNetRectangle = value; }
        }

        public DrawToolType ActiveTool
        {
            get { return activeTool; }
            set { activeTool = value; }
        }

        public Layers TheLayers
        {
            get { return _layers; }
            set { _layers = value; }
        }

        public bool IsRotated
        {
            get;
            set;
        }
        #endregion

        /* PHƯƠNG THỨC */
        #region Event Handlers

        // Truy vết Mouse để làm Anchor point
        public Point BackTrackMouse(Point p)
        {
            Point[] pts = new Point[] { p };
            Matrix mx = new Matrix();
            mx.Translate(-ClientSize.Width / 2f, -ClientSize.Height / 2f, MatrixOrder.Append);
            mx.Rotate(_rotation, MatrixOrder.Append);
            mx.Translate(ClientSize.Width / 2f + _panX, ClientSize.Height / 2f + _panY, MatrixOrder.Append);
            mx.Scale(_zoom, _zoom, MatrixOrder.Append);
            mx.Invert();
            mx.TransformPoints(pts);
            return pts[0];
        }
        #endregion

        #region Other Functions
        public void Initialize()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                 ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            Invalidate();

            activeTool = DrawToolType.Pointer;

            DocManager = docManager;

            undoManager = new Undo_Redo(_layers);

            _image = new Bitmap(this.Width, this.Height);

            _resizing = false;
            _cursorStartPoint = Point.Empty;
            MouseIsInRightEdge = false;
            MouseIsInBottomEdge = false;
            this.MouseDown += (sender, e) => StartMovingOrResizing(e);
            this.MouseUp += (sender, e) => StopDragOrResizing();
            this.MouseMove += (sender, e) => MoveControl(e);

            tools = new Event[(int)DrawToolType.NumberOfDrawTools];
            tools[(int)DrawToolType.Pointer] = new EventPointer();
            tools[(int)DrawToolType.Rectangle] = new EventRectangle();
            tools[(int)DrawToolType.Ellipse] = new EventEllipse();
            tools[(int)DrawToolType.Line] = new EventLine();
            tools[(int)DrawToolType.Polygon] = new EventPencil();
            tools[(int)DrawToolType.Text] = new EventText();
            tools[(int)DrawToolType.Image] = new EventImage();
            tools[(int)DrawToolType.SpecialShape] = new EventSpecialShape();
            LineColor = Color.Black;
            FillColor = Color.White;
            LineWidth = 2;
        }

        // Kiểm tra có Undo được không
        public bool CanUndo
        {
            get
            {
                if (undoManager != null)
                {
                    return undoManager.CanUndo;
                }

                return false;
            }
        }

        // Kiểm tra có Redo được không
        public bool CanRedo
        {
            get
            {
                if (undoManager != null)
                {
                    return undoManager.CanRedo;
                }

                return false;
            }
        }

        // Thêm Command vào History để phục vụ Undo và Redo
        public void AddCommandToHistory(Command command)
        {
            undoManager.AddCommandToHistory(command);
        }

        // Xóa History
        public void ClearHistory()
        {
            undoManager.ClearHistory();
        }

        // Hàm Undo
        public void Undo()
        {
            undoManager.Undo();
            Refresh();
        }

        // Hàm Redo
        public void Redo()
        {
            undoManager.Redo();
            Refresh();
        }

        // Vẽ các khối liên kết Anchor point
        public void DrawNetSelection(Graphics g)
        {
            if (!DrawNetRectangle)
                return;

            ControlPaint.DrawFocusRectangle(g, NetRectangle, Color.Black, Color.Transparent);
        }

        // Xử lý cho click phải chuột
        private void OnContextMenu(MouseEventArgs e)
        {
            Point point = BackTrackMouse(new Point(e.X, e.Y));
            Point menuPoint = new Point(e.X, e.Y);
            int al = _layers.ActiveLayerIndex;
            int n = _layers[al].Graphics.Count;
            DrawObject o = null;

            for (int i = 0; i < n; i++)
            {
                if (_layers[al].Graphics[i].HitTest(point) == 0)
                {
                    o = _layers[al].Graphics[i];
                    break;
                }
            }

            if (o != null)
            {
                if (!o.Selected)
                    _layers[al].Graphics.UnselectAll();
                o.Selected = true;
            }
            else
            {
                _layers[al].Graphics.UnselectAll();
            }

            Refresh();
            ctxtMenu.Show(this, menuPoint);
        }
        #endregion

        // Paint của DrawArea
        private void DrawArea_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = Graphics.FromImage(_image);
            Matrix mx = new Matrix();
            mx.Translate(-ClientSize.Width / 2f, -ClientSize.Height / 2f, MatrixOrder.Append);
            mx.Rotate(_rotation, MatrixOrder.Append);
            mx.Translate(ClientSize.Width / 2f + _panX, ClientSize.Height / 2f + _panY, MatrixOrder.Append);
            mx.Scale(_zoom, _zoom, MatrixOrder.Append);
            e.Graphics.Transform = mx;
            g.Transform = mx;
            Point centerRectangle = new Point();
            centerRectangle.X = ClientRectangle.Left + ClientRectangle.Width / 2;
            centerRectangle.Y = ClientRectangle.Top + ClientRectangle.Height / 2;
            centerRectangle = BackTrackMouse(centerRectangle);

            SolidBrush brush = new SolidBrush(Color.FromArgb(255, 255, 255));
            e.Graphics.FillRectangle(brush, ClientRectangle);
            g.FillRectangle(brush, ClientRectangle);

            //TheLayers.Draw(e.Graphics, g);
            if (_layers != null)
            {
                int lc = _layers.Count;
                for (int i = 0; i < lc; i++)
                {
                    if (_layers[i].IsVisible)
                    {
                        if (_layers[i].Graphics != null)
                            _layers[i].Graphics.Draw(e.Graphics,g);
                    }
                }
            }

            DrawNetSelection(e.Graphics); 
            brush.Dispose();
        }

        /* XỬ LÝ SỰ KIỆN DRAWAREA */
        private void DrawArea_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = BackTrackMouse(e.Location);
            if (e.Button == MouseButtons.Left)
                tools[(int)activeTool].OnMouseDown(this, e);
            else if (e.Button ==
                 MouseButtons.Right)
            {
                if (_panning)
                    _panning = false;
                ActiveTool = DrawToolType.Pointer;
                OnContextMenu(e);
            }
        }

        private void DrawArea_MouseMove(object sender, MouseEventArgs e)
        {
            Point curLoc = BackTrackMouse(e.Location);
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.None)
                if (e.Button == MouseButtons.Left && _panning)
                {
                    if (curLoc.X !=
                      lastPoint.X)
                        _panX += curLoc.X - lastPoint.X;
                    if (curLoc.Y !=
                      lastPoint.Y)
                        _panY += curLoc.Y - lastPoint.Y;
                    Invalidate();
                }
                else
                   tools[(int)activeTool].OnMouseMove(this, e);
            else
                Cursor = Cursors.Default;
            lastPoint = BackTrackMouse(e.Location);
        }

        private void DrawArea_MouseUp(object sender, MouseEventArgs e)
        {
            lastPoint = BackTrackMouse(e.Location);
            int al = TheLayers.ActiveLayerIndex;
            if (e.Button == MouseButtons.Left)
            {
                if(TheLayers[al].Dirty && TheLayers[al].Graphics[0]!=null)
                    AddCommandToHistory(new CommandAdd(TheLayers[al].Graphics[0]));
                tools[(int)activeTool].OnMouseUp(this, e);
            }
        }

        /* XOAY ĐỐI TƯỢNG */
        public void RotateObject(int p)
        {
            int al = TheLayers.ActiveLayerIndex;
            for (int i = 0; i < TheLayers[al].Graphics.Count; i++)
            {
                if (TheLayers[al].Graphics[i].Selected)
                    if (p == 0)
                        TheLayers[al].Graphics[i].Rotation = 0;
                    else
                        TheLayers[al].Graphics[i].Rotation += p;
            }
            Invalidate();
        }

        public void RotateDrawing(int p)
        {
            if (p == 0)
                Rotation = 0;
            else
            {
                Rotation += p;
                if (p < 0) 
                {
                    if (Rotation < -360)
                        Rotation = 0;
                }
                else
                {
                    if (Rotation > 360)
                        Rotation = 0;
                }
            }
            Invalidate();
        }

        /* XỬ LÝ SỰ KIỆN HỘP THOẠI SAVE/OPEN DOCMANAGER */
        #region DocManager Event Handlers
        // Exception mở không được file .meg
        private void HandleLoadException(Exception ex, SerializationEventArgs e)
        {
            MessageBox.Show(this,
                            "Open File operation failed. File name: " + e.FileName + "\n" +
                            "Reason: " + ex.Message,
                            Application.ProductName);

            e.Error = true;
        }

        // Exception save không được file .meg
        private void HandleSaveException(Exception ex, SerializationEventArgs e)
        {
            MessageBox.Show(this,
                            "Save File operation failed. File name: " + e.FileName + "\n" +
                            "Reason: " + ex.Message,
                            Application.ProductName);

            e.Error = true;
        }

        private void docManager_LoadEvent(object sender, SerializationEventArgs e)
        {
            try
            {
                TheLayers = (Layers)e.Formatter.Deserialize(e.SerializationStream);
            }
            catch (ArgumentNullException ex)
            {
                HandleLoadException(ex, e);
            }
            catch (SerializationException ex)
            {
                HandleLoadException(ex, e);
            }
            catch (SecurityException ex)
            {
                HandleLoadException(ex, e);
            }
        }

        private void docManager_SaveEvent(object sender, SerializationEventArgs e)
        {
            try
            {
                e.Formatter.Serialize(e.SerializationStream, TheLayers);
            }
            catch (ArgumentNullException ex)
            {
                HandleSaveException(ex, e);
            }
            catch (SerializationException ex)
            {
                HandleSaveException(ex, e);
            }
            catch (SecurityException ex)
            {
                HandleSaveException(ex, e);
            }
        }
        #endregion

        // Khởi tạo DocManger để quản lý save/open file .meg
        public void InitializeHelperObjects()
        {
            Save_Open_Data data = new Save_Open_Data();
            data.FormOwner = this;
            data.UpdateTitle = true;
            data.FileDialogFilter = "Mega files (*.meg)|*.meg|All Files (*.*)|*.*";
            data.NewDocName = "Untitled.meg";

            docManager = new Save_Open(data);
            docManager.RegisterFileType("meg", "megfile", "Mega File");

            docManager.SaveEvent += docManager_SaveEvent;
            docManager.LoadEvent += docManager_LoadEvent;

            docManager.DocChangedEvent += delegate
            {
                Refresh();
                ClearHistory();
            };

            docManager.ClearEvent += delegate
            {
                bool haveObjects = false;
                for (int i = 0; i < TheLayers.Count; i++)
                {
                    if (TheLayers[i].Graphics.Count > 1)
                    {
                        haveObjects = true;
                        break;
                    }
                }
                if (haveObjects)
                {
                    TheLayers.Clear();
                    ClearHistory();
                    Refresh();
                }
            };

            docManager.NewDocument();
        }

        // Save đối tượng lại theo cấu trúc nhị phân dưới dạng file .meg
        public void SaveMeg()
        {
            DocManager.SaveDocument(Save_Open.SaveType.SaveAs);
        }

        // Mở file .meg và thao tác tiếp với đối tượng đã save
        public void OpenMeg()
        {
            DocManager.OpenDocument("");
        }

        // New file .meg
        public void NewMeg()
        {
            int x = TheLayers.ActiveLayerIndex;
            CommandDeleteAll command = new CommandDeleteAll(TheLayers);
            if (TheLayers[x].Graphics.Clear())
            {
                Refresh();
            }
            ClearHistory();
            DocManager.NewDocument();
        }

        // Save As được sử dụng để lưu thành hình ảnh hoàn chỉnh khi đã vẽ xong (.jpg, .png, .bmp) khác với lưu đối tượng
        public void SaveAsImage()
        {
            if (_image != null)
            {
                SaveFileDialog dlgSave = new SaveFileDialog();
                dlgSave.Filter = "Image files (*.jpg,*.png,*.bmp)|*.jpg;*.png;*.bmp|" +
                             "JPG files (*.jpg)|*.jpg|PNG files (*.png)|*.png|BMP files (*.bmp)|*.bmp";

                dlgSave.DefaultExt = "jpg";
                dlgSave.RestoreDirectory = true;

                if (dlgSave.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        switch (Path.GetExtension(dlgSave.FileName).ToLower())
                        {
                            case ".bmp":
                                _image.Save(dlgSave.FileName, ImageFormat.Bmp);
                                break;
                            case ".png":
                                _image.Save(dlgSave.FileName, ImageFormat.Png);
                                break;
                            case ".jpg":
                                _image.Save(dlgSave.FileName, ImageFormat.Jpeg);
                                break;
                            default:
                                MessageBox.Show("Unsupported image format was specified", "Save File",
                                    MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                                return;
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Failed to save image file ", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        /* XỬ LÝ CLICK PHẢI MOUSE */
        // Thực hiện xóa được khi click phải mouse chọn Delete
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int x = TheLayers.ActiveLayerIndex;
            CommandDelete command = new CommandDelete(TheLayers);
            if (TheLayers[x].Graphics.DeleteSelection())
            {
                AddCommandToHistory(command);
                Refresh();
            }
        }

        private void ctxtMenu_Opened(object sender, EventArgs e)
        {
            int x = TheLayers.ActiveLayerIndex;
            if (TheLayers[x].Graphics.SelectionCount > 0)
            {
                deleteToolStripMenuItem.Enabled = true;
                if (IsRotated == true)
                {
                    resetRotationToolStripMenuItem.Enabled = true;
                }
                else
                {
                    resetRotationToolStripMenuItem.Enabled = false;
                }
                moveToFrontToolStripMenuItem.Enabled = true;
                moveToBackToolStripMenuItem.Enabled = true;
            }
            else
            {
                deleteToolStripMenuItem.Enabled = false;
                resetRotationToolStripMenuItem.Enabled = false;
                moveToFrontToolStripMenuItem.Enabled = false;
                moveToBackToolStripMenuItem.Enabled = false;
            }
        }

        /* XỬ LÝ ZOOM ĐỐI TƯỢNG */
        private void AdjustZoom(float _amount)
        {
            Zoom += _amount;
            if (Zoom < .1f)
                Zoom = .1f;
            if (Zoom > 10)
                Zoom = 10f;
            Invalidate();
        }

        // Lăn Mouse Wheel để zoom đối tượng
        private void DrawArea_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta != 0)
            {
                if (_controlKey)
                {
                    if (e.Delta > 0)
                        PanY += 10;
                    else
                        PanY -= 10;
                    Invalidate();
                }
                else
                {
                    if (e.Delta > 0)
                        AdjustZoom(.1f);
                    else
                        AdjustZoom(-.1f);
                }
            }
                return;
            }

        /* PHÍM TẮT KEYBOARD */
        #region Keyboard Functions
        
        // Ấn phím DEL trên bàn phím để xóa một đối tượng đã được chọn
        private void DrawArea_KeyDown(object sender, KeyEventArgs e)
        {
            int al = TheLayers.ActiveLayerIndex;
            switch (e.KeyCode)
            {
                case Keys.Delete:
                    {
                        int x = TheLayers.ActiveLayerIndex;
                        CommandDelete command = new CommandDelete(TheLayers);
                        if (TheLayers[x].Graphics.DeleteSelection())
                        {
                            AddCommandToHistory(command);
                            Refresh();
                        }
                        break;
                    }
                case Keys.ControlKey:
                    _controlKey = true;
                    break;
                default:
                    break;
            }
        }

        private void DrawArea_KeyUp(object sender, KeyEventArgs e)
        {
            _controlKey = false;
        }
        #endregion Keyboard Functions
        private void UpdateMouseEdgeProperties(Point mouseLocationInControl)
        {
            MouseIsInRightEdge = Math.Abs(mouseLocationInControl.X - this.Width) <= 10;
            MouseIsInBottomEdge = Math.Abs(mouseLocationInControl.Y - this.Height) <= 10;
        }

        private void UpdateMouseCursor()
        {
            if (MouseIsInRightEdge && this.ActiveTool == DrawArea.DrawToolType.Pointer)
            {
                if (MouseIsInBottomEdge && this.ActiveTool == DrawArea.DrawToolType.Pointer)
                {
                    this.Cursor = Cursors.SizeNWSE;
                }
                else
                {
                    this.Cursor = Cursors.SizeWE;
                }
            }
            else if (MouseIsInBottomEdge && this.ActiveTool == DrawArea.DrawToolType.Pointer)
            {
                this.Cursor = Cursors.SizeNS;
            }
        }

        private void StartMovingOrResizing(MouseEventArgs e)
        {
            if (_resizing)
            {
                return;
            }
            if (MouseIsInRightEdge || MouseIsInBottomEdge)
            {
                _resizing = true;
                _currentControlStartSize = this.Size;
            }
            _cursorStartPoint = new Point(e.X, e.Y);
            this.Capture = true;
        }

        private void MoveControl(MouseEventArgs e)
        {
            if (!_resizing)
            {
                UpdateMouseEdgeProperties(new Point(e.X, e.Y));
                UpdateMouseCursor();
            }
            else
            {
                if (MouseIsInRightEdge)
                {
                    if (MouseIsInBottomEdge)
                    {
                        this.Width = (e.X - _cursorStartPoint.X) + _currentControlStartSize.Width;
                        this.Height = (e.Y - _cursorStartPoint.Y) + _currentControlStartSize.Height;
                    }
                    else
                    {
                        this.Width = (e.X - _cursorStartPoint.X) + _currentControlStartSize.Width;
                    }
                }
                else if (MouseIsInBottomEdge)
                {
                    this.Height = (e.Y - _cursorStartPoint.Y) + _currentControlStartSize.Height;
                }
                else
                {
                    StopDragOrResizing();
                }
                _image = new Bitmap(this.Width, this.Height);
            }
        }

        private void StopDragOrResizing()
        {
            _resizing = false;
            this.Capture = false;
            UpdateMouseCursor();
        }

        private void moveToFrontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int x = TheLayers.ActiveLayerIndex;
            if (TheLayers[x].Graphics.MoveSelectionToFront())
            {
                Refresh();
            }
        }

        private void moveToBackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int x = TheLayers.ActiveLayerIndex;
            if (TheLayers[x].Graphics.MoveSelectionToBack())
            {
                Refresh();
            }
        }

        private void resetRotationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int x = TheLayers.ActiveLayerIndex;
            if (TheLayers[x].Graphics.SelectionCount > 0)
            {
                resetRotationToolStripMenuItem.Enabled = false;
                IsRotated = false;
                RotateObject(0);
            }
            else
                RotateDrawing(0);
        }
    }
}

