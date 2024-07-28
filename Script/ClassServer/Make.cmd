@echo off

pushd ..\Class
call Script\Class\CleanModule
popd
echo:

call Script\ClassServer\CleanModule
echo:

pushd ..\Class
call Script\Tool\CleanBinary
popd
echo:

call Script\ClassServer\MakeModule
echo:

pushd ..\Class
echo Make Binary
call Script\Tool\Make ReferBinaryGen
call Script\Tool\Execute ReferBinaryGen
popd