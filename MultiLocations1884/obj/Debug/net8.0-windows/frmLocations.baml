   M S B A M L   `   `   `  ����M  IMultiLocations1884, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:MultiLocations1884MultiLocations1884  5   	   %    MultiLocations1884.frmLocations   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:MultiLocations1884     5   	   -   5          �Name$  frmLocations1��5   	    � Title$ 	Locations��5   	   $	��650��6   $	��850��6(    � WindowStartupLocation$ CenterScreen=�64    � 
ResizeMode$ NoResize=�6w   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $	��100 �6   �� 5      $	��220 �6   �� 5      $	��250 �6   �� 5      $	��200 �6    � RowDefinitions 5   
   �� 5      $��40 �6   �� 5      $��70 �6   �� 5      $��50 �6   �� 5      $��50 �6   �� 5      $��50 �6   �� 5      $��50 �6   �� 5      $��50 �6   �� 5      $��50 �6   �� 5      $��50 �6   �� 5      $��50 �6   �� 5      $��50 �6   �� 5      $��50 �6   .+�5   
   �� -   5         ��Name$ lblNoLocation��5      $��2��6'   $��0��67   $��Numéro de location��6D   $
��22px(�6a   $��Center=�6q   $��Center=�6�   �� 5   
   -   5         ��Name$ txtNoLocation��5      $��2��6)   $��1��69   $��35��6F   $	��Top=�6R   $	��100��6j   $��Center=�6v   �� 5    
   -   5        $ lblIdClient��5       $��1��6%   $��3��65   $��Id du client :��6B   $
��15px(�6[   $��Right=�6k   $��Center=�6�   �� 5!   
   -   5        $ txtIdClient��5!      $��2��6'   $��3��67   $��30��6D    �� 
IsReadOnly$
 True��6P   �� 5"   
   -   5        $ lblNIV��5"      $��1��6    $��4��60   $��NIV du véhicule :��6=   $
��15px(�6Y   $��Right=�6i   $��Center=�6�   �� 5#   
   -   5        	 ��Name$	 lstNIV��5#      $��2��6#   $��4��63   $
��30px��6@   
 �� DisplayMemberPath$	
 NIV��6N    �� SelectedValuePath$	 NIV��6f   $y�Hidden=�6~   �� 5$   
   -   5        $ txtNIV��5$      $��2��6"   $��4��62   $
��30px��6?   $
 True��6M   �� 5%   
   -	   5        $ 
lblNoTerme��5%      $��1��6$   $��5��64   $��Numéro du terme :��6A   $
��15px(�6]   $��Right=�6m   $��Center=�6�   �� 5&   
   -
   5        $	 
lstNoTerme��5&      $��2��6'   $��5��67   $
��30px��6D   $
 NoTerme��6R   $ NoTerme��6n   ${�False��6�   �� 5'   
   -   5        $ lblDateDebutLoc��5'      $��1��6)   $��6��69   $��Début de location :��6F   $
��15px(�6d   $��Right=�6t   $��Center=�6�   (  "System.Windows.Controls.DatePicker  5(   
   -   5          Name$ txtDateDebutLoc��5(      $��2��6.   $��6��6>   $
��30px��6K   ${�False��6Y   �� 5)   
   -   5        $ lblDatePremPaiement��5)      $��1��6-   $��7��6=   $��Premier paiement :��6J   $
��15px(�6g   $��Right=�6w   $��Center=�6�     5*   
   -   5        $ txtDatePremPaiement��5*      $��2��62   $��7��6B   $
��30px��6O   ${�False��6]   �� 5+   
   -   5        $ lblMontantMensuel��5+      $��1��6+   $��8��6;   $��Montant mensuel :��6H   $
��15px(�6d   $��Right=�6t   $��Center=�6�   �� 5,   
   -   5        $ txtMontantMensuel��5,      $��2��6-   $��8��6=   $
��30px��6J   $
 True��6X   �� 5-   
   -   5        $ lblNbPaiementsMensuel��5-      $��1��6/   $��9��6?   $$��Nombre de paiements mensuels :��6L   $
��15px(�6u   $��Right=�6�   $��Center=�6�   �� 5.   
   -   5        $ txtNbPaiementsMensuel��5.      $��2��61   $��9��6A   $
��30px��6N   $
 True��6\   �� 5/   
   -   5        $ lblOdomètre��5/      $��1��6%   $��10��65   $��Odomètre au retour(km) :��6C   $
��15px(�6f   $��Right=�6v   $��Center=�6�   �� 50   
   -   5        $ txtOdomètre��50      $��2��6'   $��10��67   $
��30px��6E   $
 True��6S   �� 51   
   -   5         ��Name$ btnEnregistrer��51      $��Enregistrer��6)   $��2��6?   $��11��6O   $��80��6]   $��30��6h   $	��Top=�6t   $
��Left=�6�   $��10q�6�   6�   �� 52   
   -   5        $ 
btnAnnuler��52      $��Annuler��6%   $��2��67   $��11��6G   $��80��6U   $��30��6`   $	��Top=�6l   $��Right=�6�   $��10q�6�   6�   �� 53   
   -   5        $ btnRechercher��53      $��
Rechercher��6(   $��3��6=   $��1��6M   $
��Left=�6Z   $
��30px��6u   $
��80px��6�   $	��Top=�6�   $��5q�6�   6�   �� 54   
   -   5        $ 
btnNouveau��54      $��Nouveau��6%   $��3��67   $��1��6G   $��80��6T   $��30��6_   $��Right=�6k   $	��Top=�6�   $��5q�6�   6�   56      57      