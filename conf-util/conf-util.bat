@echo off

if exist "%~dp0conf-util.js" (
  cscript //nologo //e:JScript "%~dp0conf-util.js" %*
) else (
  cscript //nologo //e:JScript "%~dp0conf-util.js.src" %*
)