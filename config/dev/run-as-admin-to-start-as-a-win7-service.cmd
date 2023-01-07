sc delete OscalertSvc_d
sc create OscalertSvc_d binpath= "C:\Program Files\Kalips'o Infogistics LLC\OscalertSvc\bin\OscalertSvc.exe" start= auto displayname= "OscalertSvc_d"
sc description OscalertSvc_d "OscalertSvc_d - automatic near-realtime cellphone notifications about certain VBRescue field situations"
sc start OscalertSvc_d
pause
