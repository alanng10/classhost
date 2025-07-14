@echo off

call Clean
echo:
echo Make ClassHost
call Script\Module\DeployInfra
call Script\Module\Make