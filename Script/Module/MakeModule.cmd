@echo off

echo Make Module
pushd Module\ClassHostExe
dotnet build -v quiet
popd