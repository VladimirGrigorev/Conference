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


"%~dp0conf-util.bat" upd-macro %*
