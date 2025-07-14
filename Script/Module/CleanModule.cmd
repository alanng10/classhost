@echo off

echo Clean Module
set DotNetModuleOutFold=Out\net8.0
del /F /Q %DotNetModuleOutFold%\ClassHost.* 2>NUL
rmdir /S /Q %DotNetModuleOutFold%\ClassHost.Console.data 2>NUL