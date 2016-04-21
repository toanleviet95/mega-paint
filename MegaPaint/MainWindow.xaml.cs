using Microsoft.Win32;
using System;
using System.Drawing;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MegaPaint
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            new LoadingScreen().ShowDialog();
        }

        private void CheckFilled()
        {
            if (btnFilledSolid.Content.ToString() == "C")
            {
                DrawArea.DrawFilled = true;
                DrawArea.DrawGradient = false;
                btnFilledGradient.Content = "";
                DrawArea.DrawHatch = false;
                btnFilledHatch.Content = "";
                DrawArea.DrawTexture = false;
                btnFilledTexture.Content = "";
            }
            if (btnFilledGradient.Content.ToString() == "C")
            {
                DrawArea.DrawGradient = true;
                DrawArea.DrawFilled = false;
                btnFilledSolid.Content = "";
                DrawArea.DrawHatch = false;
                btnFilledHatch.Content = "";
                DrawArea.DrawTexture = false;
                btnFilledTexture.Content = "";
            }
            if (btnFilledHatch.Content.ToString() == "C")
            {
                DrawArea.DrawHatch = true;
                DrawArea.DrawGradient = false;
                btnFilledGradient.Content = "";
                DrawArea.DrawFilled = false;
                btnFilledSolid.Content = "";
                DrawArea.DrawTexture = false;
                btnFilledTexture.Content = "";
            }
            if (btnFilledTexture.Content.ToString() == "C")
            {
                DrawArea.DrawTexture = true;
                DrawArea.DrawHatch = false;
                btnFilledHatch.Content = "";
                DrawArea.DrawGradient = false;
                btnFilledGradient.Content = "";
                DrawArea.DrawFilled = false;
                btnFilledSolid.Content = "";
            }
        }

        private void MainForm_Loaded(object sender, RoutedEventArgs e)
        {
            DrawArea.InitializeHelperObjects();
            DrawArea.Initialize();
            DrawArea.DrawFilled = true;
            DrawArea.LineWidth = 2;
            DrawArea.PenType = TypeOfPen.PenType.Normal;
            DrawArea.IsRotated = false;
            btnFilledSolid.Content = "C";
            chkFillBackGround.IsChecked = false;
            lsbForeground.SelectedColor = Colors.Black;
            lsbBackground.SelectedColor = Colors.CadetBlue;
            cbxNormal.IsChecked = true;
            ExpFilledCustom.IsEnabled = false;
            txtRotate.Text = "90";
            btnUndo.IsEnabled = false;
            btnRedo.IsEnabled = false;
            btnPaste.IsEnabled = false;
            txtRotate.IsEnabled = false;
            btnRotateLeft.IsEnabled = false;
            btnRotateRight.IsEnabled = false;
            menuResetRotate.IsEnabled = false;
            btnUndo.Foreground = new SolidColorBrush(Colors.Black);
            btnRedo.Foreground = new SolidColorBrush(Colors.Black);
            btnPaste.Foreground = new SolidColorBrush(Colors.Black);
            btnRotateLeft.Foreground = new SolidColorBrush(Colors.Black);
            btnRotateRight.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void btnLine_Click(object sender, RoutedEventArgs e)
        {
            DrawArea.ActiveTool = DrawArea.DrawToolType.Line;
            ResetColorButton();
            btnLine.Foreground = new SolidColorBrush(Colors.Yellow);
        }

        private void btnRectangle_Click(object sender, RoutedEventArgs e)
        {
            DrawArea.ActiveTool = DrawArea.DrawToolType.Rectangle;
            DrawArea.DrawFilled = false;
            DrawArea.DrawGradient = false;
            DrawArea.DrawHatch = false;
            DrawArea.DrawTexture = false;
            if (chkFillBackGround.IsChecked == true)
            {
                CheckFilled();
            }
            ResetColorButton();
            btnRectangle.Foreground = new SolidColorBrush(Colors.Yellow);
        }

        private void btnEllipse_Click(object sender, RoutedEventArgs e)
        {
            DrawArea.ActiveTool = DrawArea.DrawToolType.Ellipse;
            DrawArea.DrawFilled = false;
            DrawArea.DrawGradient = false;
            DrawArea.DrawHatch = false;
            DrawArea.DrawTexture = false;
            if (chkFillBackGround.IsChecked == true)
            {
                CheckFilled();
            }
            ResetColorButton();
            btnEllipse.Foreground = new SolidColorBrush(Colors.Yellow);
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            DrawArea.ActiveTool = DrawArea.DrawToolType.Pointer;
            ResetColorButton();
            btnSelect.Foreground = new SolidColorBrush(Colors.Yellow);
        }

        private void ResetColorButton()
        {
            btnSelect.Foreground = new SolidColorBrush(Colors.White);
            btnPencil.Foreground = new SolidColorBrush(Colors.White);
            btnText.Foreground = new SolidColorBrush(Colors.White);
            btnImage.Foreground = new SolidColorBrush(Colors.White);
            btnLine.Foreground = new SolidColorBrush(Colors.White);
            btnRectangle.Foreground = new SolidColorBrush(Colors.White);
            btnEllipse.Foreground = new SolidColorBrush(Colors.White);
            btnTriangle.Foreground = new SolidColorBrush(Colors.White);
            btnRightTriangle.Foreground = new SolidColorBrush(Colors.White);
            btnRoundedRectangle.Foreground = new SolidColorBrush(Colors.White);
            btnArrow.Foreground = new SolidColorBrush(Colors.White);
            btnStar.Foreground = new SolidColorBrush(Colors.White);
        }

        private void btnPencil_Click(object sender, RoutedEventArgs e)
        {
            DrawArea.ActiveTool = DrawArea.DrawToolType.Polygon;
            ResetColorButton();
            btnPencil.Foreground = new SolidColorBrush(Colors.Yellow);
        }

        private void menuExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void lsbForeground_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<System.Windows.Media.Color?> e)
        {
            System.Windows.Media.Color ForeColor = (System.Windows.Media.Color)lsbForeground.SelectedColor;
            DrawArea.LineColor = System.Drawing.Color.FromArgb(ForeColor.A, ForeColor.R, ForeColor.G, ForeColor.B);
            int x = DrawArea.TheLayers.ActiveLayerIndex;
            int n = DrawArea.TheLayers[x].Graphics.SelectionCount;
            if (n > 0)
            {
                n = DrawArea.TheLayers[x].Graphics.Count;
                for (int i = 0; i < n; i++)
                {
                    if (DrawArea.TheLayers[x].Graphics[i].Selected == true)
                    {
                        DrawArea.CurrentPen = TypeOfPen.SetCurrentPen(DrawArea.PenType, DrawArea.LineColor, DrawArea.LineWidth);
                        DrawObject obj = DrawArea.TheLayers[x].Graphics[i];
                        obj.Color = DrawArea.LineColor;
                        obj.PenType = DrawArea.PenType;
                        obj.DrawPen = DrawArea.CurrentPen;
                        DrawArea.TheLayers[x].Graphics.Replace(i, obj);
                    }
                }
                DrawArea.Refresh();
            }
        }

        private void lsbBackground_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<System.Windows.Media.Color?> e)
        {
            System.Windows.Media.Color BackColor = (System.Windows.Media.Color)lsbBackground.SelectedColor;
            DrawArea.FillColor = System.Drawing.Color.FromArgb(BackColor.A, BackColor.R, BackColor.G, BackColor.B);
            if (DrawArea.DrawFilled == true)
            {
                int x = DrawArea.TheLayers.ActiveLayerIndex;
                int n = DrawArea.TheLayers[x].Graphics.SelectionCount;
                if (n > 0)
                {
                    n = DrawArea.TheLayers[x].Graphics.Count;
                    for (int i = 0; i < n; i++)
                    {
                        if (DrawArea.TheLayers[x].Graphics[i].Selected == true)
                        {
                            DrawObject obj = DrawArea.TheLayers[x].Graphics[i];
                            obj.Gradient = false;
                            obj.FillColor = DrawArea.FillColor;
                            obj.Filled = true;
                            obj.Hatch = false;
                            obj.Texture = false;
                            DrawArea.TheLayers[x].Graphics.Replace(i, obj);
                        }
                    }
                    DrawArea.Refresh();
                }
            }
        }

        private void btnFilledSolid_Click(object sender, RoutedEventArgs e)
        {
            DrawArea.DrawFilled = true;
            btnFilledSolid.Content = "C";
            DrawArea.DrawGradient = false;
            btnFilledGradient.Content = "";
            DrawArea.DrawHatch = false;
            btnFilledHatch.Content = "";
            DrawArea.DrawTexture = false;
            btnFilledTexture.Content = "";
            int x = DrawArea.TheLayers.ActiveLayerIndex;
            int n = DrawArea.TheLayers[x].Graphics.SelectionCount;
            if (n > 0)
            {
                n = DrawArea.TheLayers[x].Graphics.Count;
                for (int i = 0; i < n; i++)
                {
                    if (DrawArea.TheLayers[x].Graphics[i].Selected == true)
                    {
                        DrawObject obj = DrawArea.TheLayers[x].Graphics[i];
                        obj.Gradient = false;
                        obj.Filled = true;
                        obj.Hatch = false;
                        obj.Texture = false;
                        obj.FillColor = DrawArea.FillColor;
                        DrawArea.TheLayers[x].Graphics.Replace(i, obj);
                    }
                }
                DrawArea.Refresh();
            }
        }

        private void btnFilledGradient_Click(object sender, RoutedEventArgs e)
        {
            DrawArea.DrawGradient = true;
            btnFilledGradient.Content = "C";
            DrawArea.DrawFilled = false;
            btnFilledSolid.Content = "";
            DrawArea.DrawHatch = false;
            btnFilledHatch.Content = "";
            DrawArea.DrawTexture = false;
            btnFilledTexture.Content = "";
            int x = DrawArea.TheLayers.ActiveLayerIndex;
            int n = DrawArea.TheLayers[x].Graphics.SelectionCount;
            if (n > 0)
            {
                n = DrawArea.TheLayers[x].Graphics.Count;
                for (int i = 0; i < n; i++)
                {
                    if (DrawArea.TheLayers[x].Graphics[i].Selected == true)
                    {
                        DrawObject obj = DrawArea.TheLayers[x].Graphics[i];
                        obj.Gradient = true;
                        obj.Filled = false;
                        obj.Hatch = false;
                        obj.Texture = false;
                        DrawArea.TheLayers[x].Graphics.Replace(i, obj);
                    }
                }
                DrawArea.Refresh();
            }
        }

        private void sliderSize_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int size = (int)sliderSize.Value;
            DrawArea.LineWidth = size;
            int x = DrawArea.TheLayers.ActiveLayerIndex;
            int n = DrawArea.TheLayers[x].Graphics.SelectionCount;
            if (n > 0)
            {
                n = DrawArea.TheLayers[x].Graphics.Count;
                for (int i = 0; i < n; i++)
                {
                    if (DrawArea.TheLayers[x].Graphics[i].Selected == true)
                    {
                        DrawArea.CurrentPen = TypeOfPen.SetCurrentPen(DrawArea.PenType, DrawArea.LineColor, DrawArea.LineWidth);
                        DrawObject obj = DrawArea.TheLayers[x].Graphics[i];
                        obj.PenType = DrawArea.PenType;
                        obj.DrawPen = DrawArea.CurrentPen;
                        obj.PenWidth = DrawArea.LineWidth;
                        DrawArea.TheLayers[x].Graphics.Replace(i, obj);
                    }
                }
                DrawArea.Refresh();
            }
        }

        private void cbxNormal_Checked(object sender, RoutedEventArgs e)
        {
            DrawArea.PenType = TypeOfPen.PenType.Normal;
            DrawArea.CurrentPen = TypeOfPen.SetCurrentPen(TypeOfPen.PenType.Normal, DrawArea.LineColor, DrawArea.LineWidth);
            int x = DrawArea.TheLayers.ActiveLayerIndex;
            int n = DrawArea.TheLayers[x].Graphics.SelectionCount;
            if (n > 0)
            {
                n = DrawArea.TheLayers[x].Graphics.Count;
                for (int i = 0; i < n; i++)
                {
                    if (DrawArea.TheLayers[x].Graphics[i].Selected == true)
                    {
                        DrawObject obj = DrawArea.TheLayers[x].Graphics[i];
                        obj.PenType = DrawArea.PenType;
                        obj.DrawPen = DrawArea.CurrentPen;
                        DrawArea.TheLayers[x].Graphics.Replace(i, obj);
                    }
                }
                DrawArea.Refresh();
            }
        }

        private void cbxDot_Checked(object sender, RoutedEventArgs e)
        {
            DrawArea.PenType = TypeOfPen.PenType.Dot;
            DrawArea.CurrentPen = TypeOfPen.SetCurrentPen(TypeOfPen.PenType.Dot, DrawArea.LineColor, DrawArea.LineWidth);
            int x = DrawArea.TheLayers.ActiveLayerIndex;
            int n = DrawArea.TheLayers[x].Graphics.SelectionCount;
            if (n > 0)
            {
                n = DrawArea.TheLayers[x].Graphics.Count;
                for (int i = 0; i < n; i++)
                {
                    if (DrawArea.TheLayers[x].Graphics[i].Selected == true)
                    {
                        DrawObject obj = DrawArea.TheLayers[x].Graphics[i];
                        obj.PenType = DrawArea.PenType;
                        obj.DrawPen = DrawArea.CurrentPen;
                        DrawArea.TheLayers[x].Graphics.Replace(i, obj);
                    }
                }
                DrawArea.Refresh();
            }
        }

        private void cbxDash_Checked(object sender, RoutedEventArgs e)
        {
            DrawArea.PenType = TypeOfPen.PenType.DotDash;
            DrawArea.CurrentPen = TypeOfPen.SetCurrentPen(TypeOfPen.PenType.DotDash, DrawArea.LineColor, DrawArea.LineWidth);
            int x = DrawArea.TheLayers.ActiveLayerIndex;
            int n = DrawArea.TheLayers[x].Graphics.SelectionCount;
            if (n > 0)
            {
                n = DrawArea.TheLayers[x].Graphics.Count;
                for (int i = 0; i < n; i++)
                {
                    if (DrawArea.TheLayers[x].Graphics[i].Selected == true)
                    {
                        DrawObject obj = DrawArea.TheLayers[x].Graphics[i];
                        obj.PenType = DrawArea.PenType;
                        obj.DrawPen = DrawArea.CurrentPen;
                        DrawArea.TheLayers[x].Graphics.Replace(i, obj);
                    }
                }
                DrawArea.Refresh();
            }
        }

        private void cbxDoubleLine_Checked(object sender, RoutedEventArgs e)
        {
            DrawArea.PenType = TypeOfPen.PenType.DoubleLine;
            DrawArea.CurrentPen = TypeOfPen.SetCurrentPen(TypeOfPen.PenType.DoubleLine, DrawArea.LineColor, DrawArea.LineWidth);
            int x = DrawArea.TheLayers.ActiveLayerIndex;
            int n = DrawArea.TheLayers[x].Graphics.SelectionCount;
            if (n > 0)
            {
                n = DrawArea.TheLayers[x].Graphics.Count;
                for (int i = 0; i < n; i++)
                {
                    if (DrawArea.TheLayers[x].Graphics[i].Selected == true)
                    {
                        DrawObject obj = DrawArea.TheLayers[x].Graphics[i];
                        obj.PenType = DrawArea.PenType;
                        obj.DrawPen = DrawArea.CurrentPen;
                        DrawArea.TheLayers[x].Graphics.Replace(i, obj);
                    }
                }
                DrawArea.Refresh();
            }
            
        }

        private void cbxDashArrow_Checked(object sender, RoutedEventArgs e)
        {
            DrawArea.PenType = TypeOfPen.PenType.DashArrow;
            DrawArea.CurrentPen = TypeOfPen.SetCurrentPen(TypeOfPen.PenType.DashArrow, DrawArea.LineColor, DrawArea.LineWidth);
            int x = DrawArea.TheLayers.ActiveLayerIndex;
            int n = DrawArea.TheLayers[x].Graphics.SelectionCount;
            if (n > 0)
            {
                n = DrawArea.TheLayers[x].Graphics.Count;
                for (int i = 0; i < n; i++)
                {
                    if (DrawArea.TheLayers[x].Graphics[i].Selected == true)
                    {
                        DrawObject obj = DrawArea.TheLayers[x].Graphics[i];
                        obj.PenType = TypeOfPen.PenType.DashArrow;
                        obj.DrawPen = DrawArea.CurrentPen;
                        DrawArea.TheLayers[x].Graphics.Replace(i, obj);
                    }
                }
                DrawArea.Refresh();
            }
        }

        private void btnRotateLeft_Click(object sender, RoutedEventArgs e)
        {
            if (txtRotate.Text.Trim() != "")
            {
                int radius = int.Parse(txtRotate.Text);
                if (radius >= 0 && radius <= 360)
                {
                    DrawArea.IsRotated = true;
                    if (DrawArea.IsRotated == true)
                    {
                        menuResetRotate.IsEnabled = true;
                    }
                    int al = DrawArea.TheLayers.ActiveLayerIndex;
                    if (DrawArea.TheLayers[al].Graphics.SelectionCount > 0)
                        DrawArea.RotateObject(-radius);
                    else
                        DrawArea.RotateDrawing(-radius);
                }
                else
                {
                    MessageBox.Show("Your input is wrong !", "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtRotate.Text = "90";
                    return;
                }
            }
            else
            {
                MessageBox.Show("Your input is wrong !", "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtRotate.Text = "90";
                return;
            }
        }

        private void btnRotateRight_Click(object sender, RoutedEventArgs e)
        {
            if (txtRotate.Text.Trim() != "")
            {
                int radius = int.Parse(txtRotate.Text);
                if (radius >= 0 && radius <= 360)
                {
                    DrawArea.IsRotated = true;  
                    if (DrawArea.IsRotated == true)
                    {
                        menuResetRotate.IsEnabled = true;
                    }
                    int al = DrawArea.TheLayers.ActiveLayerIndex;
                    if (DrawArea.TheLayers[al].Graphics.SelectionCount > 0)
                        DrawArea.RotateObject(radius);
                    else
                        DrawArea.RotateDrawing(radius);
                }
                else
                {
                    MessageBox.Show("Your input is wrong !", "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtRotate.Text = "90";
                    return;
                }
            }
            else
            {
                MessageBox.Show("Your input is wrong !", "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtRotate.Text = "90";
                return;
            }
        }

        private void menuSelectAll_Click(object sender, RoutedEventArgs e)
        {
            int x = DrawArea.TheLayers.ActiveLayerIndex;
            DrawArea.TheLayers[x].Graphics.SelectAll();
            DrawArea.Refresh();
        }

        private void menuDeleteAll_Click(object sender, RoutedEventArgs e)
        {
            int x = DrawArea.TheLayers.ActiveLayerIndex;
            CommandDeleteAll command = new CommandDeleteAll(DrawArea.TheLayers);

            if (DrawArea.TheLayers[x].Graphics.Clear())
            {
                DrawArea.AddCommandToHistory(command);
                if (DrawArea.CanUndo == true)
                    btnUndo.IsEnabled = true;
                else if (DrawArea.CanUndo == false)
                    btnUndo.IsEnabled = false;
                if (DrawArea.CanRedo == true)
                    btnRedo.IsEnabled = true;
                else if (DrawArea.CanRedo == false)
                    btnRedo.IsEnabled = false;
                DrawArea.Refresh();
            }
        }

        private void menuResetRotate_Click(object sender, RoutedEventArgs e)
        {
            int al = DrawArea.TheLayers.ActiveLayerIndex;
            if (DrawArea.TheLayers[al].Graphics.SelectionCount > 0)
                DrawArea.RotateObject(0);
            else
                DrawArea.RotateDrawing(0);
            menuResetRotate.IsEnabled = false;
            DrawArea.IsRotated = false;
        }

        private void btnFilledHatch_Click(object sender, RoutedEventArgs e)
        {
            DrawArea.DrawHatch = true;
            btnFilledHatch.Content = "C";
            DrawArea.DrawFilled = false;
            btnFilledSolid.Content = "";
            DrawArea.DrawGradient = false;
            btnFilledGradient.Content = "";
            DrawArea.DrawTexture = false;
            btnFilledTexture.Content = "";
            int x = DrawArea.TheLayers.ActiveLayerIndex;
            int n = DrawArea.TheLayers[x].Graphics.SelectionCount;
            if (n > 0)
            {
                n = DrawArea.TheLayers[x].Graphics.Count;
                for (int i = 0; i < n; i++)
                {
                    if (DrawArea.TheLayers[x].Graphics[i].Selected == true)
                    {
                        DrawObject obj = DrawArea.TheLayers[x].Graphics[i];
                        obj.Gradient = false;
                        obj.Filled = false;
                        obj.Hatch = true;
                        obj.Texture = false;
                        DrawArea.TheLayers[x].Graphics.Replace(i, obj);
                    }
                }
                DrawArea.Refresh();
            }
        }

        private void btnFilledTexture_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            DrawArea.DrawTexture = true;
            btnFilledTexture.Content = "C";
            DrawArea.DrawHatch = false;
            btnFilledHatch.Content = "";
            DrawArea.DrawFilled = false;
            btnFilledSolid.Content = "";
            DrawArea.DrawGradient = false;
            btnFilledGradient.Content = "";
            
            if (op.ShowDialog() == true)
            {
                DrawArea.FillImage = new Bitmap(op.FileName);
            }
            int x = DrawArea.TheLayers.ActiveLayerIndex;
            int n = DrawArea.TheLayers[x].Graphics.SelectionCount;
            if (n > 0)
            {
                n = DrawArea.TheLayers[x].Graphics.Count;
                for (int i = 0; i < n; i++)
                {
                    if (DrawArea.TheLayers[x].Graphics[i].Selected == true)
                    {
                        DrawObject obj = DrawArea.TheLayers[x].Graphics[i];
                        obj.Gradient = false;
                        obj.Filled = false;
                        obj.Hatch = false;
                        obj.Texture = true;
                        obj.FillImage = DrawArea.FillImage;
                        DrawArea.TheLayers[x].Graphics.Replace(i, obj);
                    }
                }
                DrawArea.Refresh();
            }
        }

        private void btnTheme_Click(object sender, RoutedEventArgs e)
        {
            if (btnTheme.IsChecked.Value == true)
            {
                BrushConverter bc = new BrushConverter();
                Grid_1.Background = (System.Windows.Media.Brush)bc.ConvertFrom("#38abcf");
                Grid_2.Background = (System.Windows.Media.Brush)bc.ConvertFrom("#38abcf");
            }
            else
            {
                BrushConverter bc = new BrushConverter();
                Grid_1.Background = (System.Windows.Media.Brush)bc.ConvertFrom("#993B3737");
                Grid_2.Background = (System.Windows.Media.Brush)bc.ConvertFrom("#993B3737");
            }
        }

        private void btnUndo_Click(object sender, RoutedEventArgs e)
        {
            DrawArea.Undo();
            if (DrawArea.CanRedo == true)
                btnRedo.IsEnabled = true;
            else if (DrawArea.CanRedo == false)
                btnRedo.IsEnabled = false;
            if (DrawArea.CanUndo == true)
                btnUndo.IsEnabled = true;
            else if (DrawArea.CanUndo == false)
                btnUndo.IsEnabled = false;
        }

        private void btnRedo_Click(object sender, RoutedEventArgs e)
        {
            DrawArea.Redo();
            if (DrawArea.CanUndo == true)
                btnUndo.IsEnabled = true;
            else if (DrawArea.CanUndo == false)
                btnUndo.IsEnabled = false;
            if (DrawArea.CanRedo == true)
                btnRedo.IsEnabled = true;
            else if (DrawArea.CanRedo == false)
                btnRedo.IsEnabled = false;
        }

        private void btnText_Click(object sender, RoutedEventArgs e)
        {
            DrawArea.ActiveTool = DrawArea.DrawToolType.Text;
            ResetColorButton();
            btnText.Foreground = new SolidColorBrush(Colors.Yellow);
        }

        private void btnUndo_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(btnUndo.IsEnabled)
            {
                btnUndo.Foreground = new SolidColorBrush(Colors.White);
            }
            else
            {
                btnUndo.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void btnRedo_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (btnRedo.IsEnabled)
            {
                btnRedo.Foreground = new SolidColorBrush(Colors.White);
            }
            else
            {
                btnRedo.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void DrawArea_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            int x = DrawArea.TheLayers.ActiveLayerIndex;
            if (DrawArea.CanUndo == true)
                btnUndo.IsEnabled = true;
            else if (DrawArea.CanUndo == false)
                btnUndo.IsEnabled = false;
            if (DrawArea.CanRedo == true)
                btnRedo.IsEnabled = true;
            else if (DrawArea.CanRedo == false)
                btnRedo.IsEnabled = false;
            if (DrawArea.TheLayers[x].Graphics.Count > 0)
            {
                menuSave.IsEnabled = true;
                menuSaveAs.IsEnabled = true;
                if (DrawArea.TheLayers[x].Graphics.SelectionCount > 0)
                {
                    menuDelete.IsEnabled = true;
                    menuMoveToFront.IsEnabled = true;
                    menuMoveToBack.IsEnabled = true;
                }
                else
                {
                    menuDelete.IsEnabled = false;
                    menuMoveToFront.IsEnabled = false;
                    menuMoveToBack.IsEnabled = false;
                    menuResetRotate.IsEnabled = false;
                }
                menuDeleteAll.IsEnabled = true;
                menuSelectAll.IsEnabled = true;
                btnRotateRight.IsEnabled = true;
                btnRotateLeft.IsEnabled = true;
                txtRotate.IsEnabled = true;
            }
            else
            {
                menuSave.IsEnabled = false;
                menuSaveAs.IsEnabled = false;
                menuDelete.IsEnabled = false;
                menuDeleteAll.IsEnabled = false;
                menuSelectAll.IsEnabled = false;
                menuMoveToFront.IsEnabled = false;
                menuMoveToBack.IsEnabled = false;
                menuResetRotate.IsEnabled = false;
                btnRotateRight.IsEnabled = false;
                btnRotateLeft.IsEnabled = false;
                txtRotate.IsEnabled = false;
            }
        }

        private void btnCut_Click(object sender, RoutedEventArgs e)
        {
            int x = DrawArea.TheLayers.ActiveLayerIndex;
            int n = DrawArea.TheLayers[x].Graphics.SelectionCount;
            DrawArea.ObjCopyList = new GraphicsList();
            for (int i = 0; i < n; i++)
            {
                DrawArea.ObjCopyList.Add(DrawArea.TheLayers[x].Graphics[i]);
            }
            CommandDelete command = new CommandDelete(DrawArea.TheLayers);
            if (DrawArea.TheLayers[x].Graphics.DeleteSelection())
            {
                DrawArea.AddCommandToHistory(command);
                DrawArea.Refresh();
            }
            if (btnCopy.IsEnabled == true)
                btnCopy.IsEnabled = false;
            if (btnPaste.IsEnabled == false)
                btnPaste.IsEnabled = true;
        }

        private void btnCopy_Click(object sender, RoutedEventArgs e)
        {
            int x = DrawArea.TheLayers.ActiveLayerIndex;
            int n = DrawArea.TheLayers[x].Graphics.SelectionCount;
            DrawArea.ObjCopyList = new GraphicsList();
            for (int i = 0; i < n; i++)
            {
                DrawArea.ObjCopyList.Add(DrawArea.TheLayers[x].Graphics[i]);
            }
            if (btnPaste.IsEnabled == false)
                btnPaste.IsEnabled = true;
        }

        private void btnPaste_Click(object sender, RoutedEventArgs e)
        {
            int x = DrawArea.TheLayers.ActiveLayerIndex;
            int n = DrawArea.ObjCopyList.Count;
            DrawArea.TheLayers[x].Graphics.UnselectAll();
            for (int i = 0; i < n; i++)
            {
                DrawArea.TheLayers[x].Graphics.Add(DrawArea.ObjCopyList[i]);
                DrawArea.TheLayers[x].Graphics[i].Move(30 + i, 30 + i);
            }
            DrawArea.Capture = true;
            DrawArea.Refresh();
            if (btnPaste.IsEnabled == false)
                btnPaste.IsEnabled = true;
            if (btnCopy.IsEnabled == false)
                btnCopy.IsEnabled = true;
            DrawArea.ActiveTool = DrawArea.DrawToolType.Pointer;
            btnPaste.IsEnabled = false;
        }

        private void btnPaste_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (btnPaste.IsEnabled)
            {
                btnPaste.Foreground = new SolidColorBrush(Colors.White);
            }
            else
            {
                btnPaste.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void btnCopy_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (btnCopy.IsEnabled)
            {
                btnCopy.Foreground = new SolidColorBrush(Colors.White);
            }
            else
            {
                btnCopy.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void menuSaveAs_Click(object sender, RoutedEventArgs e)
        {
            DrawArea.SaveAsImage();
        }

        private void menuOpen_Click(object sender, RoutedEventArgs e)
        {
            DrawArea.OpenMeg();
        }

        private void menuSave_Click(object sender, RoutedEventArgs e)
        {
            DrawArea.SaveMeg();
        }

        private void menuNew_Click(object sender, RoutedEventArgs e)
        {
            DrawArea.NewMeg();
            btnUndo.IsEnabled = false;
            btnRedo.IsEnabled = false;
            btnRotateRight.IsEnabled = false;
            btnRotateLeft.IsEnabled = false;
            menuSave.IsEnabled = false;
            menuSaveAs.IsEnabled = false;
            menuDelete.IsEnabled = false;
            menuDeleteAll.IsEnabled = false;
            menuMoveToFront.IsEnabled = false;
            menuMoveToBack.IsEnabled = false;
            menuSelectAll.IsEnabled = false;
            menuResetRotate.IsEnabled = false;
            btnRotateRight.IsEnabled = false;
            btnRotateLeft.IsEnabled = false;
            txtRotate.IsEnabled = false;
        }

        private void btnImage_Click(object sender, RoutedEventArgs e)
        {
            DrawArea.ActiveTool = DrawArea.DrawToolType.Image;
            ResetColorButton();
            btnImage.Foreground = new SolidColorBrush(Colors.Yellow);
        }

        private void btnRotateLeft_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(btnRotateLeft.IsEnabled)
            {
                btnRotateLeft.Foreground = new SolidColorBrush(Colors.White);
            }
            else
            {
                btnRotateLeft.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void btnRotateRight_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (btnRotateRight.IsEnabled)
            {
                btnRotateRight.Foreground = new SolidColorBrush(Colors.White);
            }
            else
            {
                btnRotateRight.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void chkFillBackGround_Checked(object sender, RoutedEventArgs e)
        {
            CheckFilled();
            ExpFilledCustom.IsEnabled = true;
            int x = DrawArea.TheLayers.ActiveLayerIndex;
            int n = DrawArea.TheLayers[x].Graphics.SelectionCount;
            if (n > 0)
            {
                n = DrawArea.TheLayers[x].Graphics.Count;
                for (int i = 0; i < n; i++)
                {
                    if (DrawArea.TheLayers[x].Graphics[i].Selected == true)
                    {
                        DrawObject obj = DrawArea.TheLayers[x].Graphics[i];
                        if (DrawArea.DrawFilled == true)
                        {
                            obj.Gradient = false;
                            obj.Filled = true;
                            obj.Hatch = false;
                            obj.Texture = false;
                            obj.FillColor = DrawArea.FillColor;
                            DrawArea.TheLayers[x].Graphics.Replace(i, obj);
                        }
                        else if(DrawArea.DrawGradient == true)
                        {
                            obj.Gradient = true;
                            obj.Filled = false;
                            obj.Hatch = false;
                            obj.Texture = false;
                            DrawArea.TheLayers[x].Graphics.Replace(i, obj);
                        }
                        else if (DrawArea.DrawHatch == true)
                        {
                            obj.Gradient = false;
                            obj.Filled = false;
                            obj.Hatch = true;
                            obj.Texture = false;
                            DrawArea.TheLayers[x].Graphics.Replace(i, obj);
                        }
                        else if (DrawArea.DrawTexture == true)
                        {
                            obj.Gradient = false;
                            obj.Filled = false;
                            obj.Hatch = false;
                            obj.Texture = true;
                            obj.FillImage = DrawArea.FillImage;
                            DrawArea.TheLayers[x].Graphics.Replace(i, obj);
                        }
                    }
                }
                DrawArea.Refresh();
            }
        }

        private void chkFillBackGround_Unchecked(object sender, RoutedEventArgs e)
        {
            DrawArea.DrawFilled = false;
            DrawArea.DrawGradient = false;
            DrawArea.DrawHatch = false;
            DrawArea.DrawTexture = false;
            ExpFilledCustom.IsEnabled = false;
            int x = DrawArea.TheLayers.ActiveLayerIndex;
            int n = DrawArea.TheLayers[x].Graphics.SelectionCount;
            if (n > 0)
            {
                n = DrawArea.TheLayers[x].Graphics.Count;
                for (int i = 0; i < n; i++)
                {
                    if (DrawArea.TheLayers[x].Graphics[i].Selected == true)
                    {
                        DrawObject obj = DrawArea.TheLayers[x].Graphics[i];
                        obj.Filled = false;
                        obj.Gradient = false;
                        obj.Hatch = false;
                        obj.Texture = false;
                        DrawArea.TheLayers[x].Graphics.Replace(i, obj);
                    }
                }
                DrawArea.Refresh();
            }
        }

        private void btnTriangle_Click(object sender, RoutedEventArgs e)
        {
            DrawArea.ActiveTool = DrawArea.DrawToolType.SpecialShape;
            DrawArea.ShapeName = SpecialShape.ShapeName.Triangle;
            DrawArea.DrawFilled = false;
            DrawArea.DrawGradient = false;
            DrawArea.DrawHatch = false;
            DrawArea.DrawTexture = false;
            if (chkFillBackGround.IsChecked == true)
            {
                CheckFilled();
            }
            ResetColorButton();
            btnTriangle.Foreground = new SolidColorBrush(Colors.Yellow);
        }

        private void btnRightTriangle_Click(object sender, RoutedEventArgs e)
        {
            DrawArea.ActiveTool = DrawArea.DrawToolType.SpecialShape;
            DrawArea.ShapeName = SpecialShape.ShapeName.RightTriangle;
            DrawArea.DrawFilled = false;
            DrawArea.DrawGradient = false;
            DrawArea.DrawHatch = false;
            DrawArea.DrawTexture = false;
            if (chkFillBackGround.IsChecked == true)
            {
                CheckFilled();
            }
            ResetColorButton();
            btnRightTriangle.Foreground = new SolidColorBrush(Colors.Yellow);
        }

        private void btnRoundedRectangle_Click(object sender, RoutedEventArgs e)
        {
            DrawArea.ActiveTool = DrawArea.DrawToolType.SpecialShape;
            DrawArea.ShapeName = SpecialShape.ShapeName.RoundedRectangle;
            DrawArea.DrawFilled = false;
            DrawArea.DrawGradient = false;
            DrawArea.DrawHatch = false;
            DrawArea.DrawTexture = false;
            if (chkFillBackGround.IsChecked == true)
            {
                CheckFilled();
            }
            ResetColorButton();
            btnRoundedRectangle.Foreground = new SolidColorBrush(Colors.Yellow);
        }

        private void btnArrow_Click(object sender, RoutedEventArgs e)
        {
            DrawArea.ActiveTool = DrawArea.DrawToolType.SpecialShape;
            DrawArea.ShapeName = SpecialShape.ShapeName.Arrow;
            DrawArea.DrawFilled = false;
            DrawArea.DrawGradient = false;
            DrawArea.DrawHatch = false;
            DrawArea.DrawTexture = false;
            if (chkFillBackGround.IsChecked == true)
            {
                CheckFilled();
            }
            ResetColorButton();
            btnArrow.Foreground = new SolidColorBrush(Colors.Yellow);
        }

        private void btnStar_Click(object sender, RoutedEventArgs e)
        {
            DrawArea.ActiveTool = DrawArea.DrawToolType.SpecialShape;
            DrawArea.ShapeName = SpecialShape.ShapeName.Star;
            DrawArea.DrawFilled = false;
            DrawArea.DrawGradient = false;
            DrawArea.DrawHatch = false;
            DrawArea.DrawTexture = false;
            if (chkFillBackGround.IsChecked == true)
            {
                CheckFilled();
            }
            ResetColorButton();
            btnStar.Foreground = new SolidColorBrush(Colors.Yellow);
        }

        private void menuLayer_Click(object sender, RoutedEventArgs e)
        {
            frmLayerForm ld = new frmLayerForm(DrawArea.TheLayers);
            ld.ShowDialog();
            bool Visible = false;
            for (int i = 0; i < ld.layerList.Count; i++)
            {
                if (ld.layerList[i].LayerNew)
                {
                    Layer layer = new Layer();
                    layer.LayerName = ld.layerList[i].LayerName;
                    layer.Graphics = new GraphicsList();
                    DrawArea.TheLayers.Add(layer);
                }
            }
            DrawArea.TheLayers.InactivateAllLayers();
            string ActiveLayerName = "";
            for (int i = 0; i < ld.layerList.Count; i++)
            {
                if (ld.layerList[i].LayerActive)
                {
                    DrawArea.TheLayers.SetActiveLayer(i);
                    ActiveLayerName = ld.layerList[i].LayerName;
                    if(ld.layerList[i].LayerVisible)
                    {
                        Visible = true;
                    }
                    else
                    {
                        Visible = false;
                    }
                }
                if (ld.layerList[i].LayerVisible)
                    DrawArea.TheLayers.MakeLayerVisible(i);
                else
                    DrawArea.TheLayers.MakeLayerInvisible(i);
                DrawArea.TheLayers[i].LayerName = ld.layerList[i].LayerName;
            }
            for (int i = 0; i < ld.layerList.Count; i++)
            {
                if (ld.layerList[i].LayerDelete)
                    DrawArea.TheLayers.RemoveLayer(i);
            }
            DrawArea.Invalidate();
            menuLayer.Header = ActiveLayerName;
            if (Visible)
            {
                menuLayer.Icon = new System.Windows.Controls.Image
                {
                    Source = new BitmapImage(new Uri("../../Resources/Images/Visible.png", UriKind.Relative))
                };
            }
            else
            {
                menuLayer.Icon = new System.Windows.Controls.Image
                {
                    Source = new BitmapImage(new Uri("../../Resources/Images/InVisible.png", UriKind.Relative))
                };
            }
        }

        private void menuDelete_Click(object sender, RoutedEventArgs e)
        {
            int x = DrawArea.TheLayers.ActiveLayerIndex;
            CommandDelete command = new CommandDelete(DrawArea.TheLayers);
            if (DrawArea.TheLayers[x].Graphics.DeleteSelection())
            {
                DrawArea.AddCommandToHistory(command);
                DrawArea.Refresh();
            }
        }

        private void menuMoveToFront_Click(object sender, RoutedEventArgs e)
        {
            int x = DrawArea.TheLayers.ActiveLayerIndex;
            if (DrawArea.TheLayers[x].Graphics.MoveSelectionToFront())
            {
                DrawArea.Refresh();
            }
        }

        private void menuMoveToBack_Click(object sender, RoutedEventArgs e)
        {
            int x = DrawArea.TheLayers.ActiveLayerIndex;
            if (DrawArea.TheLayers[x].Graphics.MoveSelectionToBack())
            {
                DrawArea.Refresh();
            }
        }

        private void menuResetRotate_Loaded(object sender, RoutedEventArgs e)
        {
            if (DrawArea.IsRotated)
            {
                menuResetRotate.IsEnabled = true;
            }
            else
            {
                menuResetRotate.IsEnabled = false;
            }
        }
    }
}
