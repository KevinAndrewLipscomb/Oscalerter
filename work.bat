REM
REM $Id: work.bat 8004 2023-05-27 22:52:59Z kevinanlipscomb $
REM
cd "C:\Program Files\Kalips'o Infogistics LLC\OscalertSvc"
start /max explorer /e,/select,"C:\Program Files\Kalips'o Infogistics LLC\OscalertSvc\.svn"
start /max OscalertSvc.sln
IF EXIST "C:\Program Files\MySQL\MySQL Workbench\MySQLWorkbench.exe" (start "" /max "C:\Program Files\MySQL\MySQL Workbench\MySQLWorkbench.exe") ELSE start "" /max "C:\Program Files (x86)\MySQL\MySQL Workbench\MySQLWorkbench.exe"
