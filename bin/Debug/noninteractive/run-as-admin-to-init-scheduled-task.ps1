#Requires -RunAsAdministrator
#
$taskName = "Oscalerter"
Stop-ScheduledTask -TaskName:$taskName |
  Unregister-ScheduledTask
#
$action = New-ScheduledTaskAction -Execute:"${PsScriptRoot}\..\bin\Debug\Oscalerter.exe"
$description = "Near real-time notifications for certain field situations"
$principal = New-ScheduledTaskPrincipal -LogonType:S4U -UserId:$env:UserName
$settings = New-ScheduledTaskSettingsSet -ExecutionTimeLimit:0 -RestartCount:([uint16]::MaxValue) -RestartInterval:00:05:00
$trigger = New-ScheduledTaskTrigger -AtStart
New-ScheduledTask -Action:$action -Description:$description -Principal:$principal -Settings:$settings -Trigger:$trigger |
  Register-ScheduledTask -Force -TaskName:$taskName |
  Start-ScheduledTask
#
Invoke-Item ${env:windir}\system32\taskschd.msc
