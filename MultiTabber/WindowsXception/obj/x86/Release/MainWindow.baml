   M S B A M L   `   `   `  ����   WindowsXception     WindowsXception.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -     � Title$  Universal Window��$	��389��$	��666�� � WindowStyle$ 
ToolWindow=� � ShowInTaskbar .   � DataContext ��  �� RelativeSource �� *Self+.��� .+��� -    ��Name$ 
rectangle1��$��44��$
��Left=�$��-28,0,0,-48q�$��Bottom=�$	��200�����	#E5274B79 �� StrokeDashCap$ Square=� J� RenderTransformOrigin$	 0,0&���Z� z�V� .~��� �� ��  �� Angle$ -51.605Q�T� �� -   	 ��Name$	 OK��$��Ok��$��23��$
��Left=�$��475,317,0,0q�$	��Top=�$��75���� -   $	 Cancel��$��Cancel��$��23��$
��Left=�$��556,317,0,0q�$	��Top=�$��75���� -   
 ��Name$
 tabControl1��$	��269��$
��Left=�$��	12,42,0,0q�$	��Top=�$	��620��.@��� -    ��Name$ tabItem1��$��Application ��.��� .+��� -    ��Name$ 	listView1��$	��221��$
��Left=�$��	10,10,0,0q�$	��Top=�$	��594������ *ApplicationCollection+ �� View �� .P���  �� Width$
 auto�� �� CellTemplate �� .R���  ��Name$	 row��-    d� IsThreeState .  
 � Tag ��  d� 	IsChecked ��  �� Path$ Value� �� Mode$ TwoWay=��� $��Application Window��$
 auto�� �� DisplayMemberBinding �� *
windowName+�� $��
Tab Number��$
 auto�� �� *screen+�� -	   $ tabItem2��$��Process��.��� .+��� -
   $ 	listView2��$	��225��$
��Left=�$��6,6,0,0q�$	��Top=�$	��598������ *ProcessCollection+ �� .P��� $
 auto�� �� .R��� $ 
processRow��-    .   ��  �� $ Value�$ TwoWay=��� $��Process��$
 auto�� �� *processName+�� $��Application��$
 auto�� �� *applicationName+�� -    ��Name$ label1��$��Universal Window��$��36��$
��Left=�$��
467,12,0,0q�$	��Top=�$	��165�����	#E5274B79$
��Bold"�$��18(�$��Normal$�$��Normal&�$��Vrinda)�