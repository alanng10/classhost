@echo off

set HostName=%~1
set ServerPort=%~2

pushd Out\net8.0
ClassServer.Console-ExeCon "%HostName%" "%ServerPort%"
echo Status: %errorlevel%
popd