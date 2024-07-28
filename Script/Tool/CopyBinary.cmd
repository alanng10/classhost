@echo off

set DotNetModuleOutFold=.\Out\net8.0
set BinaryFold=..\Class\Out\net8.0
mkdir %DotNetModuleOutFold% 1>NUL 2>NUL

copy /Y %BinaryFold%\*.ref %DotNetModuleOutFold% 1>NUL 2>NUL