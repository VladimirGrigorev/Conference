@echo off

set DIR=%~f1

if "%DIR%"=="" (
  (
    echo Usage:
    echo   %~nx0 ^<dirname^>
  ) 1>&2
  exit
)
if not exist "%DIR%" (
  (
    echo Directory "%DIR%" not found!
  ) 1>&2
  exit
)


for /f "delims=" %%F in ('dir /b /s "%DIR%\*.docx" "%DIR%\*.doc"') do (
  echo Processing "%%~F" ...
  cmd /c "%~dp0check.bat "%%~F"
)
