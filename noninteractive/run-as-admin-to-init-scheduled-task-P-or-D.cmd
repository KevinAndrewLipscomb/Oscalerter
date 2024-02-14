schtasks /end^
 /tn Oscalert
::
schtasks /delete^
 /tn Oscalert^
 /f
::
schtasks /create^
 /tn Oscalert^
 /tr "C:\Users\KevinAndrewLipscomb\Documents\SANDBOX\vocational\kalipso-infogistics\OWNREPO\OscalertConsoleApp\bin\OscalertConsoleApp.exe"^
 /sc onstart^
 /np
::
schtasks /run^
 /tn Oscalert
::
%windir%\system32\taskschd.msc
