   M S B A M L   `   `   `  ���� O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  [ WSystem.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089Y@clr-namespace:System.Windows.Forms;assembly=System.Windows.FormsSystem.Windows.Forms D @MegaPaint, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:MegaPaint	MegaPaint %clr-namespace:MegaPaint	MegaPaint    MegaPaint.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     4s&clr-namespace:System;assembly=mscorlib     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     Owf@clr-namespace:System.Windows.Forms;assembly=System.Windows.Forms     )localclr-namespace:MegaPaint     'srcclr-namespace:MegaPaint     X TXceed.Wpf.Toolkit, Version=2.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  -     �Name$  MainForm�� � Title$ 
Mega Paint�� � WindowState$ 	Maximized=�$	��650��$	��650�� � 	Resources    
HatchBrush j� TileMode j� Viewport j� ViewportUnits j� Viewbox j� ViewboxUnits	 !� Visual�� %�   &	        !� $
 Tile=�$ 	0,0,15,15�$ Absolute=�$ 	0,0,15,15�$ Absolute=�	 � 
���   �.+�F� ����    ��� ����     	
�������F� ����        ���� ��� 	
�������.��� 
 � RowDefinitions
 �� $��20 ��� $��80 ��� .+�o� $��0��   New  Open  Save 
 SaveAs  Exit 
 Delete  	SelectAll  	DeleteAll 	 ResetRotate 
 Visible  MoveToFront  
MoveToBack��%�  &	       &	 *     &	 U     &	 �     &	 �     &	 �     &	     &	 1    &		 a    &	
 �    &	 �    &	 �    �� $$��../../Resources/Images/New.ico���� $%��../../Resources/Images/Open.ico���� $%��../../Resources/Images/Save.ico���� $'��!../../Resources/Images/SaveAs.ico���� $%��../../Resources/Images/Exit.ico���� $'��!../../Resources/Images/Delete.ico���� $&�� ../../Resources/Images/Check.ico���� $*��$../../Resources/Images/DeleteAll.ico���� $,��&../../Resources/Images/ResetRotate.ico���� $(��"../../Resources/Images/Visible.png���� $,��&../../Resources/Images/MoveToFront.png���� $+��%../../Resources/Images/MoveToBack.png��.@�m� -    m�Name$ menuFile��$��_File�� � ToolTip$6 0Click here to create, open and save your picture��.@�m� -   $ menuNew��$
��_New�� m� Icon# [ $ Create a new picture��m� -   $ menuOpen��$��_Open��# [ $0 *Open an existing picture to edit as Object��m� -   $ menuSave��$��_Save��# [ $( "Save the current picture as Object��m� -   $ 
menuSaveAs��$��Save _As��# [ $( "Save the current picture as Bitmap��m� -   $ menuExit��$��E_xit��# [ m� -   $ menuEdit��$��_Edit��$% Click here to edit your picture��.@�m� -	   $ menuSelectAll��$��Selec_t all��$ Select all drawing object��# [ m� -
   $ menuDeleteAll��$��D_elete all��$ Delete all drawing object��# [ m� -   $ 
menuDelete��$��_Delete��$ Delete all drawing object��# [ m� -   $ menuResetRotate��$��Reset _rotation��$ Reset rotated object��# [	 m� -   $ menuMoveToFront��$��Move to _front��$# Move selected object to front��# [ m� -   $ menuMoveToBack��$��Move to _back��$" Move selected object to back��# [ m� -   $ 	menuLayer��$��Default _Layer��$ Current layer��# [
 � -    �Name$ Grid_1��$��1�����	#993B3737 � ColumnDefinitions �� �� �� .+��� $��5,5,5,5q�
�������$��0��$��	Clipboard��.��  MegaPaint.ResponsivePanel  .+��� -    ��Name$ btnCut��$��5,5,5,5q�$��30��$��30��$��%��  HoverButton#���  $��Wingdings 2)�$��10(�
�������$	 Cut���� -   $ btnCopy��$��5,5,5,5q�$��30��$��30��$��2��#���  $��Webdings)�$��10(�
�������$
 Copy���� -   $ btnPaste��$��5,5,5,5q�$��30��$��30��$��¤��#���  $��Webdings)�$��10(�
�������$ Paste���� -   $ btnUndo��$��5,5,5,5q�$��30��$��30��$��Å��#���  $��	Wingdings)�$��10(�
�������$
 Undo���� -   $ btnRedo��$��5,5,5,5q�$��30��$��30��$��Æ��#���  $��	Wingdings)�$��10(�
�������$
 Redo���� $��5,5,5,5q�
�������$��1��$��Basic Shapes��.��  .+��� -   $ btnLine��$��5,5,5,5q�$��30��$��30��$	��—��#���  $��10(�
�������$
 Line���� -   $ btnRectangle��$��5,5,5,5q�$��30��$��30��$��¨��#���  $��	Wingdings)�$��10(�
�������$ 	Rectangle���� -   $ 
btnEllipse��$��5,5,5,5q�$��30��$��30��$��¡��#���  $��	Wingdings)�$��10(�
�������$ Ellipse���� $��5,5,5,5q�
�������$��2��$��Colors��.��� 
 �� ��  �� �� .+��� -    ��Name$ lblForeground��$��Center=�
�������$��0��$��0��$��
ForeGround���� -   $ lblBackground��$��Center=�
�������$��0��$��1��$��
BackGround��#  Xceed.Wpf.Toolkit.ColorPicker  -     Name$ lsbForeground��$��1��$��0��  -   $ lsbBackground��$��1��$��1��� -   $ Grid_2��$��2�����	#993B3737 �� $	��100 ��� �� $	��100 �.+�� $��0��
 �� $��2* ��� �� $��2* ��� $��60 �.+��� $��5,5,5,5q�
�������$��0��$��Tools��.��� 
 �� �� �� �� .+��� -   $ 	btnSelect��$��0��$��ö��#���  $��	Wingdings)�$��30(�
�������$ Select, Move or Resize���� -   $ 	btnPencil��$��1��$��!��#���  $��	Wingdings)�$��30(�
�������$ Pencil���� -    $ btnText��$��2��$��A��#���  $
��Bold"�$��30(�
�������$# Insert text with custom style���� -!   $ btnImage��$��3��$��°��#���  $��	Wingdings)�$��30(�
�������$# Insert image with custom size���� $��Rotate��
�������$��1��.��� 
 �� �� .+�  $��0��.+��� -"   $ btnRotateLeft��$��É��#���  $��	Wingdings)�$��30(�
�������$ Rotate Left���� -#   $ btnRotateRight��$��Ê��#���  $��	Wingdings)�$��30(�
�������$ Rotate Right��%  Xceed.Wpf.Toolkit.MaskedTextBox  -$     Name$ 	txtRotate��   
PromptChar$  ��4  .Xceed.Wpf.Toolkit.Primitives.ValueRangeTextBox   ValueDataType" ��   Mask$	 000��$��25�� �� TextAlignment$ Center=�$��1���� $��5,5,5,5q�
�������$��2��$
��Size��.��� 
 �� $��25 ��� �� $��25 �.+��� $��Center=�$	��Max��
�������$��0��$	 9px���� -%    ��Name$ 
sliderSize��$��1��  StyleSlider#���  $��Center=� �� TickPlacement$
 Both=� � Maximum$ 9Q� �� TickFrequency$ 3Q� �� Orientation$ Vertical=� �� IsSnapToTickEnabled . �� $��Center=�$	��Min��
�������$��2��$	 1px���� $��Theme Styles��
�������$��3��.��d� -&    d�Name$ btnTheme��  ToggleButtonStyle#���  ��  �� Orientation$ Vertical=�$��2��.+�9� 
�������  9� 
IsExpanded  .  $��Brushes��.���� $��Type of brush��.���� $ Vertical=�.+�	� -'   ! 	�Name$! 	cbxNormal��$��0,5,0,5q�
�������" 	� 	GroupName$" Brushes��.��Normal	� -(   $! cbxDot��$��0,5,0,5q�
�������$" Brushes��.��Dot	� -)   $! cbxDash��$��0,5,0,5q�
�������$" Brushes��.��Dash	� -*   $! cbxDoubleLine��$��0,5,0,5q�
�������$" Brushes��.��Double Line	� -+   $! cbxDashArrow��$��0,5,0,5q�
�������$" Brushes��.��
Dash Arrow�� $��5,5,5,5q�
�����_�.+��� -,   # ��Name$# chkFillBackGround��$��Center=�
�������$
��Bold"�.��Filled9� --   $ 9�Name$$ ExpFilledCustom��
�������  . $��Filled Custom��.���� $��Type of color��.��A� % A� Columns$% 2��.+��� -.   $ btnFilledSolid��
�������$
��Bold"�$��5,5,5,5q�$��30��$ Solid������ & �� Color$
& Blue���� -/   $ btnFilledGradient��
�������$
��Bold"�$��5,5,5,5q�$��30��$ Gradient������ ' �� 
StartPoint$	' 0,0&�( �� EndPoint$	( 1,1&�.��� ) � Color$) Yellow��* � Offset$	* 0.0Q�� $	) Red��$
* 0.25Q�� $
) Blue��$
* 0.75Q�� $) 	LimeGreen��$	* 1.0Q��� -0   $ btnFilledHatch��
�������$
��Bold"�$��5,5,5,5q�#��[  $��30��$ Hatch���� -1   $ btnFilledTexture��
�������$
��Bold"�$��5,5,5,5q�$��30��$ Image������ + �� ImageSource$,+ &../../Resources/Images/ImageBrush.jpeg��$ FlipXY=�$ 	0,0,25,25�$ Absolute=��� $��5,5,5,5q�$��11(�
�������$��Special Shapes��.��� 
 �� �� �� �� �� .+��� -2   $ btnTriangle��$��0��$��p��#���  $��Wingdings 3)�$��30(�
�������$ Triangle���� -3   $ btnRightTriangle��$��1��$��x��#���  $��Wingdings 3)�$��30(�
�������$ Right Triangle���� -4   $ btnRoundedRectangle��$��2��$��5��#���  $��Wingdings 2)�$��30(�
�������$ Rounded Rectangle���� -5   $ btnArrow��$��3��$��ð��#���  $��	Wingdings)�$��30(�
�������$ Arrow���� -6   $ btnStar��$��4��$��¶��#���  $��	Wingdings)�$��30(�
�������$
 Star��7  1System.Windows.Forms.Integration.WindowsFormsHost  ,   
Background
, �����$��10,10,10,10q�$��1��-   Child.-   MegaPaint.DrawArea    MegaPaint.DrawArea.   MouseUp$. DrawArea_MouseUp��-7   DrawArea��