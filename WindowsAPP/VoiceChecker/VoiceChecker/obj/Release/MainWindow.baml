   M S B A M L   `   `   `  ���� H  DHear2Read_TTS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Hear2Read_TTS_AppHear2Read_TTS_App  "    Hear2Read_TTS_App.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     1localclr-namespace:Hear2Read_TTS_App       � Title$  Hear2Read TTS App��$��434.486��$	��700�� � 
ResizeMode$ CanMinimize=� � Icon$ h2r-32x32-win.ico��.��� $	��420�� � RowDefinitions �� $	��61* ��� $	��20* �.+�]� ��. $��0,0,-0.4,-0.2q�$��2��.+��� $	��Top=����#b3c6ff$��
0,0,-0.0,2q�$��51��.+�]� ��. $��57��$��	0,0,0.4,0q�.+��� -    ��Name$
 Logo��$��Right=����#b3c6ff$��0q�.���� -    ��Name$ H2RLogo��$!��hear2read-horizontal@2x.png��$��48��$	��216��$
��0 0 q� J� RenderTransformOrigin$ 0.488,0.329&��� $
��Left=����#b3c6ff$	��22 (�$	��310��$��20,17,20,6.8q����	#FF101FBB$ 0.5,0.5&�z�V� .~��� �� ��  �� Angle$ 0.276Q�T� Hear2Read Indic Language TTS�� $	��Top=�$��34��$��
-0.4,0,0,0q��� $	��top=����#00548b$��60��$��
0,0,-0.4,0q�.+�]� ��. $��54��$��
0,0,-0.4,0q�.+��� $
��Left=�.+��� $��20(�$
��Wrap=�
�������$��10 10q� �� TextAlignment$
 left=�75Hear2Read Has Installed these Voices On Your Computer�� $��Right=�.+��� -   $��40��$	��100�����	#FF101FBB$��10 10q�$ 0.475,0.244&�$��Refresh���� $��Right=��� $��Bottom=����#00548B.+�]� ��. .+��� -   $ 	GetVoices��$��Right=�$	��150��$��30��$
��90,2q����	#FF101FBB.��Get More Voices Here�� -   $ 
DemoVoices��$
��Left=�$	��150��$��30��$
��90,2q����	#FF101FBB.��Listen to a Demo Here�� �� $��Bottom=����#00548B.+��� $	��240��.���� -   	 ��Name$	 	VoiceList��$
��Auto=�