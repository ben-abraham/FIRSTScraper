   M S B A M L   `   `   `  ����	  FIRST    FIRST.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5        $
��Main��5   5     � Title$  
MainWindow��6	   $	��350��6   $	��525��6)    � 	Resources 5         ItemAnimation W� From	 W� To g� Duration �� TargetProperty �� 
TargetName g� 	BeginTime��%�  &	        �� 5   
   .��5	      W� $	 1.0X�6   $	 0.0X�6)   $ 0:0:0.5D�62   $ Scale�6E   $
 Main��6g   W� 5
      $	 0.0X�6   $	 1.0X�6)   $ 0:0:0.5D�62   $ 0:0:0.5X�6E   $ Scale�6Y   $
 Main��6{   W� 5      $	 1.0X�6   $	 0.0X�6)   $ 0:0:0.5D�62   $
 Fade�6E   $
 Main��6f   W� 5      $	 0.0X�6   $	 1.0X�6)   $ 0:0:0.5D�62   $ 0:0:0.5X�6E   $
 Fade�6Y   $
 Main��6z   5      5      .��5      �  � ColumnDefinitions 5   
   �� 5      $
��363* �6   �� 5      $
��146* �6   	 � RowDefinitions	 5   
   �� 5      $��20 �6   �� 5      $��* �6   .+�5   
   o� $��2��6   $��0��6#   .@�5      m� $��Events��6   .@�5      m� -   5        $��Select Event��5      61   5      m� 5      $��Teams��6   .@�5      m� $��Team Scraper��6   5      5       &   System.Windows.Controls.DataGrid  5"   
   -   5        $��Tester��5"      
   
IsReadOnly
 . $��2��63   $��1��6G      Columns 5#      0  *System.Windows.Controls.DataGridTextColumn  5$      ,  &System.Windows.Controls.DataGridColumn   Header$
 Name��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 63   �� *Name+5&      ��5(      �� 5)       �� 
TargetType)  #System.Windows.Controls.DataGridRow"  6   �5*      �� 5+       �� Property � z� �� Value 5,      �� 5-   "    �� ScaleY 61   ��  �� ElementName$
 Main�� �� Path$ Scale�6c   5/      �� 50       �@J�Opacity 64   �� $
 Main��$
 Fade�52      54      56      57      