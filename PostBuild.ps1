$thisScript = "~\$($(Get-ChildItem $PSCommandPath).Name)"
$target = "C:\ProgramData\Ki\Oscalerter\Sensitive\connectionStrings.config"
if (Test-Path $target)
  {
  Write-Host "$thisScript assumes this is a local build because it found connectionString.config in the app's ProgramData\~\Sensitive folder. Copying it to the bin\Debug\Config\Sensitive folder..."
  Copy-Item -Path:$target -Destination:"Config\Sensitive" # relative to the project's output folder
  }
else
  {
  Write-Host "WARN: $thisScript did not find connectionStrings.config in the app's ProgramData\Sensitive folder. It must be provided manually. If it had been found there at build-time, this script would've copied it to the bin\Debug\Config\Sensitive folder."
  }
