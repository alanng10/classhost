@echo off

set HostName=%~1
set HostPort=%~2

pushd Out\net8.0
ClassHost.Console-ExeCon "%HostName%" "%HostPort%"
echo Status: %errorlevel%
popd