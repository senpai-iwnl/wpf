   M S B A M L   `   `   `  ����C  ?WpfFilmy, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:WpfFilmy.WindowsWpfFilmy.Windows  5   	   (    "WpfFilmy.Windows.FilmDetailsWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:WpfFilmy.Windows     5   	   -   5          � Title$  Szczegóły Filmu��5   	   $	��350��6!   $	��400��6.    � WindowStartupLocation$ CenterOwner=�5	   	   .��5
      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��0��6   $��Tytuł:��6-   �� 5   
   -   5         ��Name$ titleTextBox��5      $��0��6(   $��1��65   $��0,0,0,5q�6E   �� 5   
   $��1��6   $��0��6   $��Data Premiery:��6-   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ releaseDatePicker��5      $��1��60   $��1��6=   $��0,0,0,5q�6M   �� 5   
   $��2��6   $��0��6   $��Opis:��6-   $	��Top=�6=   �� 5   
   -   5        $ descriptionTextBox��5      $��2��6.   $��1��6;   $��0,0,0,5q�6K   $	��100��6\    �� TextWrapping$
 Wrap=�6i    �� AcceptsReturn$
 True��6}   " �� VerticalScrollBarVisibility$
 Auto=�6�   � 5!   
   $��4��6   $��0��6   $��2��6,   �� 5#   
   $��5��6   $��0��6"   $��2��62   	 �� Orientation$	 
Horizontal=�6F   $��Right=�6_   .+�5$      �� -   5        
 ��Name$
 okButton��5$      $��OK��6'   $��75��64   $��0,0,5,0q�6?    �� 	IsDefault$
 True��6g   �� 5%      -   5        $
 cancelButton��5%      $��Anuluj��6+   $��75��6<    �� IsCancel$
 True��6b   5&      5'      5(      