   M S B A M L   `   `   `  ����   Cube    Cube.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  
MainWindow��$
��1024��$	��768��.��� -   $��Grid1��
�������
 �� Top$ 0�� �� Left$ 0��.+�]� -   |�. .+�'� -   $��
myViewport��}�. $	��600��$	��400��|�.  '� Camera =� -   myPerspectiveCamera�� � FarPlaneDistance$ 15Q� � LookDirection$ 0,0,15� � UpDirection$ 0,1,05� � NearPlaneDistance$ 1Q� � Position$ 0,0,-31�	 =� FieldOfView$	 50Q�.x�g� -   topModelVisual3D��2�g� 
 g� Content
 ��  �� Color$ #333333��g� 
 t� $ #FFFFFF�� t� 	Direction$ -0.612372,-0.5,-0.6123725�g� 
 t� $ #FFFFFF��$ 0.612372,-0.5,-0.6123725�g� -   side1ModelVisual��
 � -   side1GeometryModel3D��   
CubeSide01 � Geometry# [    BranchesMaterial � Material# [   InsideMaterial � BackMaterial# [ g� -   side2ModelVisual��
 � -	   side2GeometryModel3D��  
CubeSide02# [   FlowersMaterial# [ # [ g� -
   side3ModelVisual��
 � -   side3GeometryModel3D��  
CubeSide03# [   BerriesMaterial# [ # [ g� -   side4ModelVisual��
 � -   side4GeometryModel3D��  
CubeSide04# [   LeavesMaterial1# [ # [ g� -   side5ModelVisual��
 � -   side5GeometryModel3D�� 	 
CubeSide05# [	  
 RocksMaterial# [
 # [ g� -   side6ModelVisual��
 � -   side6GeometryModel3D��  
CubeSide06# [   SunsetMaterial# [ # [  g� 	Transform Y� ��� -   myHorizontalRTransform�� �� Rotation �� -   myHorizontalRotation�� �� Angle$ 0Q� �� Axis$ 0 1 05��� -   myVerticalRTransform�� �� -   myVerticalRotation��$ 0Q�$ 1 0 05�S� -   myTranslateTransform�� S� OffsetX$ 0Q� S� OffsetY$ 0Q� S� OffsetZ$ 0Q� � Triggers :�  :� RoutedEvent$ Viewport3D.Loaded��.[��� .���� -    ��Name$ rotationStoryboard��.��W�  �� 
TargetName$ myVerticalRotation�� �� TargetProperty$ Angle� W� From$ 0X�	 W� To$	 360X� g� Duration$ 0:0:0D� g� RepeatBehavior$ Forever��W� $ myHorizontalRotation��$ Angle�$ 0X�$	 360X�$ 0:0:0D�$ Forever���� -   $��stackPanel1��$	��133��$	��200��.+��� -   $��label1��$��28��$��camera position���� -   $��textBox1��$��23��$	��120���� -   $��textBox2��$��23��$	��120���� -   $��textBox3��$��23��$	��120���� -   $��textBox7��$��23��$	��120���� -   $��stackPanel2��$	��138��$	��200��.+��� -   $��label2��$��
viewport3d��$��28���� -    $��textBox4��$��23��$	��120���� -!   $��textBox5��$��23��$	��120���� -"   $��textBox6��$��23��$	��120���� -#   $��button1��$��Render��$��23��$��75��