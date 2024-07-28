@echo off

pushd ..\Class
call Script\Tool\Make ReferBinaryGen
call Script\Tool\Execute ReferBinaryGen
popd