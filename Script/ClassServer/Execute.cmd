@echo off

pushd Out\net8.0
dotnet ClassServer.Console-ExeCon.dll
echo Status: %errorlevel%
popd