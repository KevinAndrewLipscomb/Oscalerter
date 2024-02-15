REM
REM $Id$
REM
cd "C:\Program Files\Kalips'o Infogistics LLC\Oscalerter"
start /max explorer /e,/select,"C:\Program Files\Kalips'o Infogistics LLC\Oscalerter\.git"
start /max Oscalerter.sln
IF EXIST "C:\Program Files\MySQL\MySQL Workbench\MySQLWorkbench.exe" (start "" /max "C:\Program Files\MySQL\MySQL Workbench\MySQLWorkbench.exe") ELSE start "" /max "C:\Program Files (x86)\MySQL\MySQL Workbench\MySQLWorkbench.exe"
