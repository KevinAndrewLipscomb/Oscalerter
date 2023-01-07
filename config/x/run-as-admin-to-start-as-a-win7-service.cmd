sc delete OscalertSvc_x
sc create OscalertSvc_x binpath= "C:\Program Files\Kalips'o Infogistics LLC\OscalertSvc_x\bin\OscalertSvc.exe" start= auto displayname= "OscalertSvc_x"
sc description OscalertSvc_x "OscalertSvc_x - automatic near-realtime cellphone notifications about certain VBRescue field situations"
sc start OscalertSvc_x
pause
