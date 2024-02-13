sc delete OscalertConsoleApp_x
sc create OscalertConsoleApp_x binpath= "C:\Program Files\Kalips'o Infogistics LLC\OscalertConsoleApp_x\bin\OscalertConsoleApp.exe" start= manual displayname= "OscalertConsoleApp_x"
sc description OscalertConsoleApp_x "OscalertConsoleApp_x - automatic near-realtime cellphone notifications about certain VBRescue field situations"
sc start OscalertConsoleApp_x
pause
