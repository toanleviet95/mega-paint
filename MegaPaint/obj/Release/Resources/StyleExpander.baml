   M S B A M L   `   `   `  ���� D  @MegaPaint, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:MegaPaint.ResourcesMegaPaint.Resources  W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 %  !GroupBoxExpanderToggleButtonStyle  �� 
TargetType �� Property �� Value �� 
TargetType J� SnapsToDevicePixels A�Name  ButtonNormalBackgroundFill  ExpanderShadowFill F�Name �� Triggers P� Property	 P� Value
 �� 
TargetName  GroupBoxBorderBrush �� Color  GroupBoxHeaderBrush  BorderGapMaskConverter  GroupBoxExpander � ColumnDefinitions � RowDefinitions �� CornerRadius ��Name �� Padding d� 	IsChecked �� Path �� Mode �� RelativeSource ��Name J� OpacityMask `� 	Converter `� ConverterParameter �� ElementName�� F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     +local!clr-namespace:MegaPaint.Resources  %�  &	        &	 K    &	 ^    &	 q    &	 v    7  7   7  7  �� "  d�.���  � �� �� " d�.R��  .  
������ .+�A� $ circle��$��Center=�$��Center=�$��15��$��15��#���  
�������A� $ shadow��$y�Hidden=�$��Center=�$��Center=�$��13��$��13��#���  F� $ arrow�� .  $��Center=�$��Center=����#666$��2��"����@B @B � 	=  	= ���j @B 	 P�  �@d�	IsChecked$
	 trueY�. ��� $
 arrow�� � ��" ��@B  	= � 	= @B ���j  	= 	P�  �@J�IsMouseOver	 . . ��� $
 circle�� � �� �#666�� $
 arrow�� � �� �#222�� $
 shadow�� � y�$ Visible=��� $ #D0D0BF���� $ #0046D5���� �� "  9�.���  � ��8   ��  � ��$ 1q���  � �� �� " 9�.R��  .  �� $��6 ��� $
��Auto ��� $��* ��� $��6 � �� $
��Auto ��� $
��Auto ��� $��* ��� $��6 �.+��� $ 4��$��1��$��3��$��0��$��4��#��z��
������ #��z���� $ Header��$��1��$��0��$��2��$ 3,0,3,0q�._��  .  
������ $��1��$��0��$��2�� �� �� .+�d� $��HeaderToggle��$��0��$��0��$��0��8��  �� $ 
IsExpanded�$ TwoWay=� �� *TemplatedParent+�� $��Header����. 8�� # z $��1��$��Center=�$
��Left=�$��3,0,0,0q��� $ 
ExpandSite��$y�	Collapsed=�$��2��$��1��$��2��#��z��# z �� $��1��$��3��$��4��#��z��#��z��$ 4�� `� 8  $ 7��.1��� $ ActualWidth�$ Header���� $ ActualWidth� �� *Self+�� $ ActualHeight� �� *Self+ P�  �@9�
IsExpanded	 . . ��� $
 
ExpandSite�� � y�$ Visible=�P�  � {�	 .  . ��� # � �� � ��