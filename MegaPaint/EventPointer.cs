using System.Drawing;
using System.Windows.Forms;

namespace MegaPaint
{
    // Class PointerEvent cho con trỏ chuột phục vụ cho select, move và resize
    internal class EventPointer : Event
    {
        /* --- Thuộc tính --- */
        // Hằng số cho kiểu select
        private enum SelectionMode
        {
            None,
            NetSelection, // Group selection
            Move, 
            Size
        }

        // Mặc định selection mode là none
        private SelectionMode selectMode = SelectionMode.None;

        private DrawObject resizedObject;
        private int resizedObjectHandle;

        // Giá trị trước và sau của point (Dùng để resize và move)
        private Point lastPoint = new Point(0, 0);
        private Point startPoint = new Point(0, 0);
        private CommandChangeState commandChangeState;
        private bool wasMove = false;

        /* --- Phương thức --- */
        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            commandChangeState = null;
            wasMove = false;

            selectMode = SelectionMode.None;
            Point point = drawArea.BackTrackMouse(new Point(e.X, e.Y));

            // Kiểm tra có phải là đang resize 
            int al = drawArea.TheLayers.ActiveLayerIndex;
            int n = drawArea.TheLayers[al].Graphics.SelectionCount;

            for (int i = 0; i < n; i++)
            {
                DrawObject o = drawArea.TheLayers[al].Graphics.GetSelectedObject(i);
                int handleNumber = o.HitTest(point);

                if (handleNumber > 0)
                {
                    selectMode = SelectionMode.Size;
                    // Giữ đối tượng cần resize
                    resizedObject = o;
                    resizedObjectHandle = handleNumber;
                    // Khi resize một đối tượng phải unselect các đối tượng khác
                    drawArea.TheLayers[al].Graphics.UnselectAll();
                    o.Selected = true;
                    commandChangeState = new CommandChangeState(drawArea.TheLayers);
                    break;
                }
            }

            // Kiểm tra có phải là đang move
            if (selectMode == SelectionMode.None)
            {
                int n1 = drawArea.TheLayers[al].Graphics.Count;
                DrawObject o = null;

                for (int i = 0; i < n1; i++)
                {
                    if (drawArea.TheLayers[al].Graphics[i].HitTest(point) == 0)
                    {
                        o = drawArea.TheLayers[al].Graphics[i];
                        break;
                    }
                }

                if (o != null)
                {
                    selectMode = SelectionMode.Move;

                    // Unselect tất cả nếu Ctrl chưa nhấn và một đối tượng chưa được chọn
                    if ((Control.ModifierKeys & Keys.Control) == 0 &&
                        !o.Selected)
                        drawArea.TheLayers[al].Graphics.UnselectAll();

                    o.Selected = true;
                    commandChangeState = new CommandChangeState(drawArea.TheLayers);
                    drawArea.Cursor = Cursors.SizeAll;
                }
            }

            // Kiểm tra có phải là đang group selection
            if (selectMode == SelectionMode.None)
            {
                if ((Control.ModifierKeys & Keys.Control) == 0)
                    drawArea.TheLayers[al].Graphics.UnselectAll();

                selectMode = SelectionMode.NetSelection;
                drawArea.DrawNetRectangle = true;
            }

            lastPoint.X = point.X;
            lastPoint.Y = point.Y;
            startPoint.X = point.X;
            startPoint.Y = point.Y;

            drawArea.Capture = true;
            drawArea.NetRectangle = DrawRectangle.GetNormalizedRectangle(startPoint, lastPoint);
            drawArea.Refresh();
        }

        public override void OnMouseMove(DrawArea drawArea, MouseEventArgs e)
        {
            Point point = drawArea.BackTrackMouse(new Point(e.X, e.Y));
            int al = drawArea.TheLayers.ActiveLayerIndex;
            wasMove = true;
            // Set con trỏ khi chưa nhấn chuột
            if (e.Button == MouseButtons.None)
            {
                Cursor cursor = null;

                if (drawArea.TheLayers[al].Graphics != null)
                {
                    for (int i = 0; i < drawArea.TheLayers[al].Graphics.Count; i++)
                    {
                        int n = drawArea.TheLayers[al].Graphics[i].HitTest(point);
                        if (n > 0)
                        {
                            cursor = drawArea.TheLayers[al].Graphics[i].GetHandleCursor(n);
                            break;
                        }
                    }
                }

                if (cursor == null)
                    cursor = Cursors.Default;

                drawArea.Cursor = cursor;
                return;
            }

            if (e.Button != MouseButtons.Left)
                return;

            // Khi nhấn trái chuột
            // Tìm khoảng cách giữa hai điểm trước và sau
            int dx = point.X - lastPoint.X;
            int dy = point.Y - lastPoint.Y;

            lastPoint.X = point.X;
            lastPoint.Y = point.Y;

            // Resize
            if (selectMode == SelectionMode.Size)
            {
                if (resizedObject != null)
                {
                    resizedObject.MoveHandleTo(point, resizedObjectHandle);
                    drawArea.Refresh();
                }
            }

            // Move
            if (selectMode == SelectionMode.Move)
            {
                int n = drawArea.TheLayers[al].Graphics.SelectionCount;

                for (int i = 0; i < n; i++)
                {
                    drawArea.TheLayers[al].Graphics.GetSelectedObject(i).Move(dx, dy);
                }

                drawArea.Cursor = Cursors.SizeAll;
                drawArea.Refresh();
            }

            // Group selection
            if (selectMode == SelectionMode.NetSelection)
            {
                drawArea.NetRectangle = DrawRectangle.GetNormalizedRectangle(startPoint, lastPoint);
                drawArea.Refresh();
                return;
            }
        }

        public override void OnMouseUp(DrawArea drawArea, MouseEventArgs e)
        {
            int al = drawArea.TheLayers.ActiveLayerIndex;
            if (selectMode == SelectionMode.NetSelection)
            {
                // Group selection
                drawArea.TheLayers[al].Graphics.SelectInRectangle(drawArea.NetRectangle);

                selectMode = SelectionMode.None;
                drawArea.DrawNetRectangle = false;
            }

            if (resizedObject != null)
            {
                // Sau khi resize
                resizedObject.Normalize();
                resizedObject = null;
            }

            drawArea.Capture = false;
            drawArea.Refresh();

            if (commandChangeState != null && wasMove)
            {
                commandChangeState.NewState(drawArea.TheLayers);
                drawArea.AddCommandToHistory(commandChangeState);
                commandChangeState = null;
            }

            lastPoint = drawArea.BackTrackMouse(e.Location);
        }
    }
}
