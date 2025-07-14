@echo off

echo Make Module
pushd ClassHost\ClassHostExe
dotnet build -v quiet
popd