using System;
using System.Drawing;
using System.Windows.Forms;

namespace MegaPaint
{
    public partial class frmTextForm : Form
    {
        private string _text;
        private Font _font;
        private Color _color;
        private Color _background;

        public frmTextForm()
        {
            InitializeComponent();
        }

        public string TheText
        {
            get { return _text; }
            set { _text = value; }
        }

        public Font TheFont
        {
            get { return _font; }
            set { _font = value; }
        }

        public Color TheColor
        {
            get { return _color; }
            set { _color = value; }
        }

        public Color TheBackGround
        {
            get { return _background; }
            set { _background = value; }
        }

        private void frmTextForm_Load(object sender, EventArgs e)
        {
            CenterToScreen();
            _color = Color.Black;
            _font = txtTextInput.Font;
            _text = "";
        }

        private void btnEditFont_Click(object sender, EventArgs e)
        {
            dlgFont.AllowSimulations = true;
            dlgFont.AllowVectorFonts = true;
            dlgFont.AllowVerticalFonts = true;
            dlgFont.MaxSize = 72;
            dlgFont.MinSize = 16;
            dlgFont.ShowApply = false;
            dlgFont.ShowColor = false;
            dlgFont.ShowEffects = true;
            if (dlgFont.ShowDialog() == DialogResult.OK)
            {
                _font = dlgFont.Font;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            _text = txtTextInput.Text;
        }

        public Color ChooseFontColor()
        {
            ColorDialog dl = new ColorDialog();
            dl.AnyColor = true;
            dl.FullOpen = true;
            if (dl.ShowDialog() == DialogResult.OK)
            {
                _color = dl.Color;
                txtTextInput.ForeColor = _color;
                return dl.Color;
            }
            return Color.Black;
        }

        public Color ChooseBackground()
        {
            ColorDialog dl = new ColorDialog();
            dl.AnyColor = true;
            dl.FullOpen = true;
            if (dl.ShowDialog() == DialogResult.OK)
            {
                _background = dl.Color;
                txtTextInput.BackColor = _background;
                return dl.Color;
            }
            return Color.Black;
        }

        private void btnBackground_Click(object sender, EventArgs e)
        {
            btnBackground.BackColor = ChooseBackground();
        }

        private void btnFontColor_Click(object sender, EventArgs e)
        {
            btnFontColor.BackColor = ChooseFontColor();
        }
    }
}
