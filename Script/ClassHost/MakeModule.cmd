@echo off

echo Make Module
pushd ClassServer\ClassServerExe
dotnet build -v quiet
popd