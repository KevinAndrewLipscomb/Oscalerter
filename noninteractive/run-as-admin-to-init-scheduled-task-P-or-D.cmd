schtasks /end^
 /tn Oscalerter
::
schtasks /delete^
 /tn Oscalerter^
 /f
::
schtasks /create^
 /tn Oscalerter^
 /tr "C:\Users\KevinAndrewLipscomb\Documents\SANDBOX\vocational\kalipso-infogistics\OWNREPO\Oscalerter\bin\Oscalerter.exe"^
 /sc onstart^
 /np
::
schtasks /run^
 /tn Oscalerter
::
%windir%\system32\taskschd.msc
