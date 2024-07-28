@echo off

echo Clean Module
set DotNetModuleOutFold=..\Class\Out\net8.0
del /F /Q %DotNetModuleOutFold%\ClassServer.* 2>NUL
rmdir /S /Q %DotNetModuleOutFold%\ClassServer.Console.data 2>NUL