   M S B A M L   `   `   `  ���� D  @MergePDFs, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:MergePDFs	MergePDFs      MergePDFs.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     )localclr-namespace:MergePDFs     -     � Title$  PDF-Merge-Tool��$	��500��$	��800�� � 	Resources  �� 
TargetType �� Property �� Value&   System.Windows.Controls.DataGrid*  $System.Windows.Controls.DataGridCell �� ElementName �� Path �� Triggers P� Property	 P� Value=  7System.Windows.Controls.Primitives.DataGridColumnHeader��%  '��       '  �     '  �    '  h    �� " �����  � ��$ 20����  � ��$	 100����  � ��$ 50����  � ��
 �������  � ��$ Normal$���  � ��$ 15(���  � ��$ DemiBold"���  � ��$ 10q��� "  ���  � ��
 �������  �@ 
IsReadOnly$
 True���� % �@��IsSynchronizedWithCurrentItem$
 TrueY���  �@ CanUserSortColumns$ False����  �@ AutoGenerateColumns$ False����  � ��$ 50,25,25,40q��� "  ���  � ��$ 30����  � ��$ 15(���  � �� �� $ column1��$ Width���  � ��$ Center=���  � ��$ Center=� P�  �@J�IsMouseOver$
	 True��. ���  � ��
 ��  ���  � ��
 �����P�  �@ 
IsSelected$
	 True��. ���  � ��
 ��  ���  � ��
 �����P�  �@ 
IsSelected$	 False��. ���  � ��
 �������  � ��
 �   ��� "  ���  � ��$ 40����  � ��$ 20(���  � ��$ DemiBold"���  � ��$ Stretch=���  � ��$ Center=�.��� 
 � ColumnDefinitions
 �� $��3* ��� $��1* �.+�  -     Name$	 dg1��$��0������ $ 
AllPDFData�   Columns 0  *System.Windows.Controls.DataGridTextColumn  -   column1��,  &System.Windows.Controls.DataGridColumn   Header$ 	Filenames��   Width5  /System.Windows.Controls.DataGridLengthConverter$ 1* 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� $ Filename�4  .System.Windows.Controls.DataGridTemplateColumn     
Visibility$ Hidden=� J� InputBindings d�  d� Gesture$ LeftDoubleClicka� �� Command �� *LeftDoubleClickCommand+ �� CommandParameter �� $	 dg1��$ SelectedItem���  �� Orientation$ Vertical=�$��1��$��10q�.+��� -    ��Name$ btn_add��$��Add PDF������ *
AddCommand+�� -   $ btn_up��$��Move Up������ *MoveUpCommand+���� $	 dg1��$ SelectedIndex��� -   $ btn_down��$��	Move Down������ *MoveDownCommand+���� $	 dg1��$ SelectedIndex��� -   $ btn_del��$��Delete������ *DeleteCommand+���� $	 dg1��$ SelectedItem��� -   $ btn_del_all��$��
Delete All������ *ResetCommand+�� -	   $ 	btn_merge��$��
Merge PDFs������ *MergeCommand+