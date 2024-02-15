schtasks /end^
 /tn Oscalerter
::
schtasks /delete^
 /tn Oscalerter^
 /f
::
schtasks /create^
 /tn Oscalerter^
 /tr "%~dp0..\bin\Oscalerter.exe"^
 /sc onstart^
 /np
::
schtasks /run^
 /tn Oscalerter
::
%windir%\system32\taskschd.msc
