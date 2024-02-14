schtasks /end^
 /tn Oscalert_x
::
schtasks /delete^
 /tn Oscalert_x^
 /f
::
schtasks /create^
 /tn Oscalert_x^
 /tr "C:\Users\KevinAndrewLipscomb\Documents\SANDBOX\vocational\kalipso-infogistics\OWNREPO\OscalertConsoleApp\bin\OscalertConsoleApp.exe"^
 /sc onstart^
 /np
::
schtasks /run^
 /tn Oscalert_x
::
%windir%\system32\taskschd.msc
