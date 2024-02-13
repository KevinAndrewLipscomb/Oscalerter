sc delete OscalertConsoleApp
sc create OscalertConsoleApp binpath= "C:\Program Files\Kalips'o Infogistics LLC\OscalertConsoleApp\bin\OscalertConsoleApp.exe" start= auto displayname= "OscalertConsoleApp"
sc description OscalertConsoleApp "OscalertConsoleApp - automatic near-realtime cellphone notifications about certain VBRescue field situations"
sc start OscalertConsoleApp
pause
