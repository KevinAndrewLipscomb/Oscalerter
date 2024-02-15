schtasks /end^
 /tn Oscalerter_x
::
schtasks /delete^
 /tn Oscalerter_x^
 /f
::
schtasks /create^
 /tn Oscalerter_x^
 /tr "C:\Users\KevinAndrewLipscomb\Documents\SANDBOX\vocational\kalipso-infogistics\OWNREPO\Oscalerter\bin\Oscalerter.exe"^
 /sc onstart^
 /np
::
schtasks /run^
 /tn Oscalerter_x
::
%windir%\system32\taskschd.msc
