   M S B A M L   `   `   `  ���� C  ?RegUsers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:RegUsersRegUsers      RegUsers.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     (localclr-namespace:RegUsers     b ^MaterialDesignThemes.Wpf, Version=4.1.0.2556, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes    � Title$   ��$	��500��$	��700�� � WindowStartupLocation$ CenterScreen=�.��� 
��� ���.+��� $	��400��$��15q�
�������$��Center=� �� Padding$ 30q�$	��550�� �� CornerRadius$ 13�� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     
BlurRadius$ 30Q�   Color$ 	LightGray��   ShadowDepth$ 0Q�._��� .+��� $��Center=�$��TatBank��$��30(�$
��Bold"�$��0 0 0 0q��� -   $��Center=�$��
Войти��$��0 -3 0 5q�
����  �   MaterialDesignFlatButton#��[  �� -    ��Name$ textBoxLogin��)  #MaterialDesignThemes.Wpf.HintAssist	   Hint$*	 $Введите номер карты�� % !MaterialDesignFloatingHintTextBox#��[ G� -   
 G�Name$
 passBox��$!	 Введите пароль�� ) %MaterialDesignFloatingHintPasswordBox#��[ G� -   $
 	passBox_2��$%	 Повторите пароль��#��[ �� -   $ textBoxEmail��$	 Введите Email��#��[ �� -   $*��$Зарегистрироваться��$��0 25 0 0q�
����  �