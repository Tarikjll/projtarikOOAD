   M S B A M L   `   `   `  ����  
WpfCompany    WpfCompany.YearReportPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Aav2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6E     N� Title$  Jaarrapporten��5      .��5      ]� $��10q�6   .+�5	   
   �� $	��Top=�6    �� Orientation$ 
Horizontal=�5
      $��Center=�5      $��0,0,0,10q�5      .+�5      �� $��Filter op jaar:��6   $��Center=�6/   �� 5      -   5         ��Name$ cbYears��5      $	��100��5      $��5,0,0,0q�5      5      �� 5      -   5        $��Ververs��5      $��10,0,0,0q�5      5      5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ 	dgReports��5         AutoGenerateColumns$ False��5         CanUserAddRows$ False��5         
IsReadOnly$
 True��5         Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6%   	   Width5  /System.Windows.Controls.DataGridLengthConverter$	 50 5      1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 5      �� *Id+5        5       $
 Jaar��6%   $	 80 5"      
 5!      �� *Year+5"        5#      $	 FTE��6%   $	 80 5%      
 5$      �� *Fte+5%        5&      $ Datum laatste wijziging��6%   $		 150 5(      
 5'      �� *LastModified+ �� StringFormat$ d��5(      5*      5+      5,      