@echo off

echo Make Module
set DotNetModuleOutFold=..\Class\Out\net8.0
pushd ClassServer\ClassServerExe
dotnet build -v quiet
popd