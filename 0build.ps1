# Derived from KiZeroBuild/0build.ps1
#
# Remove unneeded lines.
#
Write-Host ""
Write-Host "*"
Write-Host "* Make sure any instance of Visual Studio that has touched Oscalerter.sln has been shut down."
Write-Host "*"
Pause
Write-Host ""
Write-Host "OPERATING AT LOCAL ENVIRONMENT LEVEL..."
Write-Host ""
Write-Host "-- Running dotnet nuget locals all --clear"
Write-Host ""
& dotnet nuget locals all --clear
Write-Host ""
Write-Host "OPERATING AT SOLUTION LEVEL..."
Write-Host ""
$removalTargets =
  "*.*proj.user",
  ".vs",
  "bin",
  "Debug", # can exist outside of bin/, especially in Setup projects
  "node_modules",
  "obj",
  "package-lock.json",
  "packages",
  "Release" # can exist outside of bin/, especially in Setup projects
$derivedItems = Get-ChildItem -Recurse -Force | Where-Object Name -in $removalTargets
if ($derivedItems.Length -eq 0)
  {
  Write-Host "No derived items to remove."
  }
else
  {
  Write-Host "-- Removing derived items..."
  $derivedItems |
    Get-Item -ErrorAction:SilentlyContinue | # absorbs errors like when the pipeline calls for removing /bin followed by /bin/Debug
    Remove-Item -Recurse -Force -Verbose
  }
Write-Host ""
Write-Host "When you are ready to manually remove the configuration/runtime element from App.config,"
Pause
Invoke-Item App.config
Write-Host ""
Write-Host "Confirm that you have removed the configuration/runtime element from App.config,"
Pause
if (Test-Path package.json)
  {
  Write-Host ""
  Write-Host "-- Running npm install..."
  Write-Host ""
  npm install --no-fund
  }
Write-Host ""
Write-Host "-- Running nuget restore..."
Write-Host ""
nuget restore
Write-Host ""
Write-Host "*"
Write-Host "* Oscalerter.sln will now open in Visual Studio. Manually launch a Build, then perform the recommended runtime"
Write-Host "* assemblyBinding redirect procedure, if any."
Write-Host "*"
Pause
Invoke-Item Oscalerter.sln
Write-Host ""
Write-Host "*"
Write-Host "* Put any configuration/runtime element changes into controlled instances of App.config as applicable."
Write-Host "*"
Pause
