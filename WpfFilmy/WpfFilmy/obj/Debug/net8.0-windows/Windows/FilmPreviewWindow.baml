   M S B A M L   `   `   `  ����C  ?WpfFilmy, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:WpfFilmy.WindowsWpfFilmy.Windows  5   	   (    "WpfFilmy.Windows.FilmPreviewWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:WpfFilmy.Windows     5   	   -   5          � Title$  Podgląd Filmu��5   	   $	��300��6   $	��400��6,    � WindowStartupLocation$ CenterOwner=�5	   	   .��5
      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��0��6   $��Tytuł:��6-   �� 5   
   -   5         ��Name$ titleTextBlock��5      $��0��6,   $��1��69   $��0,5,0,5q�6I   $
��Bold"�6Z   $
��Wrap=�6l   �� 5   
   $��1��6   $��0��6   $��Data Premiery:��6-   �� 5   
   -   5        $ releaseDateTextBlock��5      $��1��62   $��1��6?   $��0,5,0,5q�6O   �� 5   
   $��2��6   $��0��6   $��Opis:��6-   $	��Top=�6=   �� 5   
   -   5        $ descriptionTextBlock��5      $��2��62   $��1��6?   $��0,5,0,5q�6O   $
��Wrap=�6`   $	��Top=�6t   � 5    
   $��3��6   $��0��6   $��2��6,   �� 5"   
   -   5        $��4��5"      $��0��6   $��2��6.   $��Zamknij��6B   $��75��6T   $��Right=�6_    �� IsCancel$
 True��6�   5#      5$      