@echo off

call Clean
echo:
echo Make Module
call Script\Module\DeployInfra
call Script\Module\Make