:: Derived from KiZeroBuild/0build.cmd
::
@echo off
set bash=C:\cygwin\bin\bash
set cyg=/cygdrive/c/cygwin/bin
echo.
echo *
echo * Make sure any instance of Visual Studio that has touched Oscalerter.sln has been shut down.
echo *
pause
echo.
echo OPERATING AT LOCAL ENVIRONMENT LEVEL...
echo.
echo -- Running dotnet nuget locals all --clear
echo.
dotnet nuget locals all --clear
echo.
set CommonCommandStartForFolders=%bash% -c "%cyg%/find -depth -name
set CommonCommandEndForFolders=-type d -exec %cyg%/rm --recursive {} \;"
echo.
echo OPERATING AT SOLUTION LEVEL...
echo.
echo -- Removing solution's .vs folder(s)...
%CommonCommandStartForFolders% .vs %CommonCommandEndForFolders%
echo -- Removing solution's bin folder(s)...
%CommonCommandStartForFolders% bin %CommonCommandEndForFolders%
echo -- Removing solution's obj folder(s)...
%CommonCommandStartForFolders% obj %CommonCommandEndForFolders%
echo -- Removing solution's packages folder(s)...
%CommonCommandStartForFolders% packages %CommonCommandEndForFolders%
echo -- Removing solution's *.*proj.user file(s)...
%bash% -c "%cyg%/find -name *.*proj.user -type f -exec %cyg%/rm {} \;"
echo.
echo When you are ready to manually remove the configuration/runtime element from App.config,
pause
App.config
echo.
echo Confirm that you have removed the configuration/runtime element from App.config,
pause
echo.
echo -- Running nuget restore...
echo.
nuget restore
echo.
echo *
echo * Oscalerter.sln will now open in Visual Studio.  Manually launch a Build, then perform the recommended runtime assemblyBinding
echo * redirect procedure.
echo *
pause
Oscalerter.sln
echo.
echo *
echo * Put any configuration/runtime element changes into controlled instances of App.config as applicable.
echo *
pause
