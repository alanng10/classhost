@echo off

call Script\ClassServer\CleanModule
echo:
call Script\ClassServer\MakeModule
echo:
call Script\Tool\CopyBinary