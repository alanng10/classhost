@echo off

echo Deploy Infra

set AvalonOutFold=.\Out\net8.0
set InfraDeployFold=..\Class\Out\InfraDeploy

mkdir %AvalonOutFold% 1>NUL 2>NUL

xcopy /S /E /Y "%InfraDeployFold%" "%AvalonOutFold%" 1>NUL 2>NUL