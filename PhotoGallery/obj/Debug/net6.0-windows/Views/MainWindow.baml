   M S B A M L   `   `   `  ����G  CPhotoGallery, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:PhotoGalleryPhotoGallery  5   	   D @AdonisUI, Version=1.17.1.0, Culture=neutral, PublicKeyToken=nullG1clr-namespace:AdonisUI.Controls;assembly=AdonisUIAdonisUI.Controls 5   	       PhotoGallery.MainWindow   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:PhotoGallery     5   	   Fcontrols1clr-namespace:AdonisUI.Controls;assembly=AdonisUI     5   	   $	��400��5
   	     � WindowState$  	Maximized=�5   	    � Title$ mddn's Photo Gallery��5   	   $	��450��6&   $	��800��63    � Icon$ /Resources/logo.ico��5   	   ��5      �� 5   
    �� 
TargetType" �6    �� BasedOn# [B�6$    � 	Resources 5         LeftArrowImage  RightArrowImage��%�   &	        &	 8     �� 5   
   $��/Resources/arrow-left.png��6'   6L   �� 5   
   $ ��/Resources/arrow-right.png��6(   6N   5      .��5      �  � RowDefinitions 5   
   �� 5      $
��389* �6   6,   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,    � ColumnDefinitions 5   
   �� 5      $��* �6   6+   �� 5      $��* �6   6+   �� 5      $��* �6   6+   .+�5    
   o� $��3��6   $��20��6#   $	��Top=�6/   .@�5!      m� $
��File��6   .@�5"      m� $
��Save��6    m� Command 6)   �� *SaveCommand+m� 5#      $��
Save As...��6    6/   �� *SaveAsCommand+5$      5%      �� 5&   
   -   5        $��ImageBox��5&      $��Center=�5(      $
��Auto��5)      $��0 20 0 0q�5*      $	��Top=�5+      $��0��5,      $��0��5-      $��3��5.      ��5'      �� *CurrentImage+5.      �� 5/   
   #��[ 6   $��22(�50      $��2��52      $��2��53      $��50��54      $��50��55      $��0 0 0 10q�56      ��51      �� *NextCommand+56      �� 57   
   -   5        	 ��Name$	 Previous��57      #��[  58      $��22(�59      $��2��5;      $��50��5<      $��50��5=      $��0 0 0 10q�5>      ��5:      �� *PreviousCommand+5>      �� 5?   
   $��-��6   $��Right=�5@      $��22(�5A      $
��Auto��5B      $��1��5D      $��2��5E      $��	0 10 0 10q�5F      ��5C      �� *RemoveCommand+5F      �� 5G   
   $��+��6   $
��Left=�5H      $��22(�5I      $
��Auto��5J      $��2��5L      $��1��5M      $��	0 10 0 10q�5N      ��5K      �� *
AddCommand+5N      �� 5O   
   $��Center=�6   $��0,20,0,0q�5P      
 �� TextWrapping$

 Wrap=�5Q      $	��Top=�5T      $
��Auto��5U      $��30��5V      $��1��5W      $��1��5X      ��5R      �� *Description+ �� UpdateSourceTrigger$ PropertyChanged=�{�5S      �� *IsTextBoxEnabled+5X      5Y      5Z      