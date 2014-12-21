@echo off
pushd %1
for /R %1 %%i in (.) do ( 
pushd %%i
del /Q *.*
popd)

