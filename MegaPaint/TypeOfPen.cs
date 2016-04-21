using System.Drawing;
using System.Drawing.Drawing2D;

namespace MegaPaint
{
    // Các kiểu cho Pen
    public class TypeOfPen
    {
        // Khai báo tên hằng các kiểu của Pen và số lượng Pen
        #region Enums
        public enum PenType
        {
            Normal,
            Dot,
            DotDash,
            DoubleLine,
            DashArrow
        }
        #endregion Enums
        
        // Trả về kiểu Pen mong muốn
        public static Pen SetCurrentPen(PenType _penType, Color _lineColor, int _lineWidth)
        {
            Pen pen;
            switch (_penType)
            {
                case PenType.Normal:
                    pen = null;
                    break;
                case PenType.DotDash:
                    pen = DotDashPen(_lineColor, _lineWidth);
                    break;
                case PenType.Dot:
                    pen = DotPen(_lineColor, _lineWidth);
                    break;
                case PenType.DoubleLine:
                    pen = DoubleLinePen(_lineColor, _lineWidth);
                    break;
                case PenType.DashArrow:
                    pen = DashArrowPen(_lineColor, _lineWidth);
                    break;
                default:
                    pen = null;
                    break;
            }
            return pen;
        }

        // Trả về DotPen
        private static Pen DotPen(Color _lineColor, int _lineWidth)
        {
            Pen p = new Pen(_lineColor);
            p.LineJoin = LineJoin.Round;
            p.DashStyle = DashStyle.Dot;
            p.Width = _lineWidth;
            return p;
        }

        // Trả về DotDashPen 
        private static Pen DotDashPen(Color _lineColor, int _lineWidth)
        {
            Pen p = new Pen(_lineColor);
            p.LineJoin = LineJoin.Round;
            p.Width = _lineWidth;
            p.DashStyle = DashStyle.DashDot;
            p.DashCap = DashCap.Round;
            return p;
        }

        // Trả về DoubleLinePen
        private static Pen DoubleLinePen(Color _lineColor, int _lineWidth)
        {
            Pen p = new Pen(_lineColor);
            p.CompoundArray = new float[] { 0.0f, 0.1f, 0.2f, 0.3f, 0.7f, 0.8f, 0.9f, 1.0f };
            p.LineJoin = LineJoin.Round;
            p.Width = (float)(_lineWidth + 10);
            return p;
        }

        // Trả về DashArrowPen
        private static Pen DashArrowPen(Color _lineColor, int _lineWidth)
        {
            Pen p = new Pen(_lineColor);
            p.LineJoin = LineJoin.Round;
            p.Width = _lineWidth;
            p.DashStyle = DashStyle.Dash;
            p.EndCap = LineCap.ArrowAnchor;
            p.DashCap = DashCap.Flat;
            return p;
        }
    }
}
