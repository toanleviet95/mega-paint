   M S B A M L   `   `   `  ����D  @MegaPaint, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:MegaPaint.ResourcesMegaPaint.Resources  5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 %  !GroupBoxExpanderToggleButtonStyle  �� 
TargetType �� Property �� Value �� 
TargetType J� SnapsToDevicePixels A�Name  ButtonNormalBackgroundFill  ExpanderShadowFill F�Name �� Triggers P� Property	 P� Value
 �� 
TargetName  GroupBoxBorderBrush �� Color  GroupBoxHeaderBrush  BorderGapMaskConverter  GroupBoxExpander � ColumnDefinitions � RowDefinitions �� CornerRadius ��Name �� Padding d� 	IsChecked �� Path �� Mode �� RelativeSource ��Name J� OpacityMask `� 	Converter `� ConverterParameter �� ElementName�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     5      +local!clr-namespace:MegaPaint.Resources  5      %  &	        &	 �    &	 �    &	 �    &	 �    7  7   7  7  �� 5      "  d�66   .�5   
   ��  � �� 5      �� 5      " d�6"   .R�5      �  .  
������ .+�5	      A� $ circle��6?   $��Center=�6"   $��Center=�6O   $��15��6j   $��15��6u   #���  6�   
�������6�   A� 5
      $ shadow��6S   $y�Hidden=�6"   $��Center=�66   $��Center=�6c   $��13��6~   $��13��6�   #���  6�   F� 5      $ arrow��6;    .  $��Center=�6J   $��Center=�6e   ���#666$��2��6�   "����@B @B � 	=  	= ���j @B 	6�   5       5      P� 5       �@d�	IsChecked$
	 trueY�67   . �5      �� $
 arrow��65    � ��" ��@B  	= � 	= @B ���j  	= 	6H   5      P� 5       �@J�IsMouseOver	 . . �5      �� $
 circle��67    � �� �#6666K   �� 5      $
 arrow��67    � �� �#2226J   �� 5      $
 shadow��6;    � y�$ Visible=�6O   5      5      5      5      �� 5      $ #D0D0BF��62   �� 5      $ #0046D5��62   �� 5      6   �� 5!      "  9�6%   .�5"   
   ��  � ��8   6(   �� 5#   
    � ��$ 1q�6,   �� 5$   
    � �� 5%      �� 5&      " 9�6"   .R�5'      �  .  5(      �� 5)      $��6 �6/   �� 5*      $
��Auto �6/   �� 5+      $��* �6/   �� 5,      $��6 �6/    5.      �� 5/      $
��Auto �6,   �� 50      $
��Auto �6,   �� 51      $��* �6,   �� 52      $��6 �6,   .+�54      �� $ 4��6!   $��1��62   $��3��6?   $��0��6P   $��4��6`   #��z��6t   
������ #��z��6�   �� 55      $ Header��6!   $��1��61   $��0��6A   $��2��6N   $ 3,0,3,0q�6_   ._�56      �  .  
������ $��1��6X   $��0��6h   $��2��6u    57   "   �� 58   &   �� 59   &   .+�5;   "   d� $��HeaderToggle��5<   %   $��0��5;   /   $��0��6?   $��0��6M   8��  5=   %   �� $ 
IsExpanded�$ TwoWay=� �� *TemplatedParent+5>   #   �� 5?   "   $��Header��63   ��. 8�� # z 5A   %   $��1��6a   $��Center=�6q   $
��Left=�6�   $��3,0,0,0q�6�   5B      5C      �� 5E      $ 
ExpandSite��6+   $y�	Collapsed=�6?   $��2��6V   $��1��6c   $��2��6s   #��z��6�   # z 6�   �� 5G      $��1��6!   $��3��6.   $��4��6?   #��z��6S   #��z��6�   $ 4��6�    5H      `� 5I   "   8  $ 7��6c   .1�5J   &   �� $ ActualWidth�6.   $ Header��6A   �� 5K   &   $ ActualWidth�6.    6A   �� *Self+�� 5L   &   $ ActualHeight�6.    6B   �� *Self+5M   #   5O      5Q       5R      P� 5S       �@9�
IsExpanded	 . . �5T      �� $
 
ExpandSite��6;    � y�$ Visible=�6S   5U      P� 5V       � {�	 .  . �5W      �� # � ��6;    � ��5X      5[      5]      5^      5_      