   M S B A M L   `   `   `  ����E  ATestWpfApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:TestWpfApp
TestWpfApp  5   	       TestWpfApp.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   _ [MaterialDesignThemes.Wpf, Version=4.8.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:TestWpfApp     5   	     � Title$  
MainWindow��5	   	   $	��450��6   $	��800��6)   .��5
      �  � 	Resources 5   
      MyCustomStyleComboBox �� 
TargetType  MaterialDesignComboBox �� BasedOn �� Triggers �� Binding �� ElementName �� Value �� Property	 �� Value  MyCustomStyleButton # MaterialDesignRaisedLightButton��%l  &	        7  &	 �     7  �� 5      " ��62   8    5      �� 5       6"   �� *Items.Count+$ 	MyListBox�� 06Y   .a�5      ��  � {�	 .  66   5      5      �� 5      " ��60   8    5      �� 5       6"   �� *Items.Count+$ 	MyListBox�� 06Y   .a�5      ��  � {�	 .  66   5      5      5      .+�5   
   �� $��	0,0,465,0q�6   .+�5      �� $4��.Считать данные с помощью:��6   $��20(�68   �� 5      $-��'Регулярных выражений��6   #��[ 6\   $
��Left=�6�   $��
10 10 0 10q�6�   ���	#FF3AA5B7���	#FF3AA5B7$	��204��6�   ��64   �� *ReadXmlRegexCommand+6�   �� 5      $��Модели данных��6   #��[ 6Y   $
��Left=�6�   $��10 0 0 0q�6�   ���	#FF3AA5B7���	#FF3AA5B7$	��204��6�   
 � Cursor$

 Hand��6�   ��6-   �� *ReadXmlDataModelCommand+6�   �� 5      $5��/Фильтрация по категориям:��6   $��0 10 0 0q�68   $��20(�6J   �� 5       #��[  6   $
��Left=�6�   $	��204��6�   $��
10 10 0 10q�6�   ���	#FF3AA5B7��6F   �� *
Categories+ �� SelectedItem 6i   �� *SelectedCategory+6�   �� 5!      $)��#Сортировка по дате:��6   $��0 10 0 0q�62   $��20(�6D   �� 5"      #��[ 6   $ ��Отсортировать��6h   $
��Left=�6�   $��
10 10 0 10q�6�   ���	#FF3AA5B7���	#FF3AA5B7$	��204��6�   $

 Hand��6�   ��6B   �� *SortByDateCommand+6�   �� 5#      $?��9Запись данных, выберите формат:��6   $��20(�6>   �� 5$      #��[  6   $
��Left=�6�   $	��204��6�   $��	10 10 0 0q�6�   ���	#FF3AA5B7��6F   �� *	Formats+ 6f   �� *SelectedFormat+6�   5%      � 5&   
   $��	335,0,0,0q�6   .+�5'      �� $��Новости��6   $��Center=�6&   $��0,10,0,0q�6C   $	��Top=�6U   $��22(�6m   �� 5(      -   5         ��Name$ 	MyListBox��5(      $ �� IsSynchronizedWithCurrentItem$
 TrueY�6Z   $��10,52,10,10q�6�   $��16(�6�   ��6   �� *Items+ �� UpdateSourceTrigger$ PropertyChanged=���5)      �� 5*      .R�5+      �� $��3q�6%   .+�5,      �� $
��Wrap=�6(   $��16(�6<   ��6J   ��  �� Path$ Title��� 5-      $
��Wrap=�6(   $��12(�6<   ��6J   �� $ Category��� 5.      $
��Wrap=�6(   $��12(�6<   ��6J   �� $
 Link��� 5/      $
��Wrap=�6(   $��12(�6<   ��6J   �� $ PubDate��� 50      $
��Wrap=�6(   $��10(�6<   ��6J   �� $ Description�51      52      54      55      56      57      