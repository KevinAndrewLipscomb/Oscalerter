echo ""
echo "${0}: Launching Oscalerter as a daemon..."
nohup /cygdrive/c/PROJ/Oscalerter/bin/Oscalerter.exe &
echo "${0}: Done."
echo "${0}: Make sure its entry is in your crontab so that it will run as a cron job upon @reboot."
echo ""
