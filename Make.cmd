@echo off

call Clean
echo:
echo Make ClassServer
call Script\ClassServer\MakeModule
call Script\Tool\CopyBinary