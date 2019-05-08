@echo off

set FILE=%~f1

if "%FILE%"=="" (
  (
    echo Usage:
    echo   %~nx0 ^<filename^>
  ) 1>&2
  exit
)
if not exist "%FILE%" (
  (
    echo File "%FILE%" not found!
  ) 1>&2
  exit
)


del /f /q "%FILE%.check" >nul 2>&1

call "%~dp0conf-util.bat" check %*

if exist "%FILE%.check" (
  echo %FILE%.check
)
