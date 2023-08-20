sc delete OscalertSvc
sc create OscalertSvc binpath= "C:\Program Files\Kalips'o Infogistics LLC\OscalertSvc\bin\OscalertSvc.exe" start= auto displayname= "OscalertSvc"
sc description OscalertSvc "OscalertSvc - automatic near-realtime cellphone notifications about certain VBRescue field situations"
sc start OscalertSvc
pause
