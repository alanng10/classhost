@echo off

set ToolName=%~1
set ModuleName=Z.Tool.%ToolName%
pushd Out\net8.0
dotnet %ModuleName%.dll
echo Status: %errorlevel%
popd