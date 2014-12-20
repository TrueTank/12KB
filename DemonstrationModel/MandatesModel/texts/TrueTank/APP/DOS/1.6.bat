@echo off
for /f "tokens=1,* delims==" %%a in ('set') do (
echo %%b
echo ------
)
