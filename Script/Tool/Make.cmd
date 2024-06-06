@echo off

set ToolName=%~1
set ModuleName=Z.Tool.%ToolName%
pushd Tool\%ModuleName%
dotnet build -v quiet
popd