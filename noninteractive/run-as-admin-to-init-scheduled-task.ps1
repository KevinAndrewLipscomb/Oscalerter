$taskName = "Oscalerter"
Stop-ScheduledTask -TaskName $taskName |
  Unregister-ScheduledTask
#
$action = New-ScheduledTaskAction -Execute "${PsScriptRoot}\..\bin\Oscalerter.exe"
$description = "Near real-time notifications for certain field situations"
$trigger = New-ScheduledTaskTrigger -AtStart
$principal = New-ScheduledTaskPrincipal -LogonType S4U -UserId $env:UserName
New-ScheduledTask -Action $action -Description $description -Principal $principal -Trigger $trigger |
  Register-ScheduledTask -Force -TaskName $taskName |
  Start-ScheduledTask
#
Invoke-Item ${env:windir}\system32\taskschd.msc
