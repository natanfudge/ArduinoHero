@echo off
title Setting up Arduino enviroment
cd /d %~dp0
echo Copying libraries...
xcopy /s libs "C:\Program Files (x86)\Arduino\libraries"
echo Done copying libraries!
