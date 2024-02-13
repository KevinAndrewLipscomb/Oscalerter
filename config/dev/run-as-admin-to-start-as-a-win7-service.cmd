sc delete OscalertConsoleApp_d
sc create OscalertConsoleApp_d binpath= "C:\Program Files\Kalips'o Infogistics LLC\OscalertConsoleApp\bin\OscalertConsoleApp.exe" start= auto displayname= "OscalertConsoleApp_d"
sc description OscalertConsoleApp_d "OscalertConsoleApp_d - automatic near-realtime cellphone notifications about certain VBRescue field situations"
sc start OscalertConsoleApp_d
pause
