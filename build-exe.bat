@echo off

setlocal enabledelayedexpansion

set "SCRIPT_DIR=%~dp0"
set "DIST_DIR=%SCRIPT_DIR%dist"
set "PUBLISH_ARGS=-c Release --self-contained true -p:PublishSingleFile=true -p:DebugType=None"

REM
if not exist "%DIST_DIR%" mkdir "%DIST_DIR%"

REM
goto :main

:publish
set "runtime=%~1"
set "output_name=%~2"
echo Publishing for %runtime%...
dotnet publish -r "%runtime%" %PUBLISH_ARGS%
set "exe_path=bin\Release\net9.0\%runtime%\publish\slimulator.exe"
if exist "%exe_path%" (
    copy "%exe_path%" "%DIST_DIR%\%output_name%"
    echo Created %DIST_DIR%\%output_name%
) else (
    echo Error: %exe_path% not found
    exit /b 1
)
goto :eof

:main

if "%1"=="" (
    call :publish "win-x64" "slimulator-x64.exe"
    call :publish "win-x86" "slimulator-x86.exe"
) else if "%1"=="--x64" (
    call :publish "win-x64" "slimulator-x64.exe"
) else if "%1"=="--x86" (
    call :publish "win-x86" "slimulator-x86.exe"
) else if "%1"=="--help" (
    goto :usage
) else if "%1"=="-h" (
    goto :usage
) else (
    echo Invalid argument: %1
    goto :usage
)

echo Build completed. Exe files are in %DIST_DIR%/
goto :eof

:usage
echo Usage: %0 [option]
echo.
echo Options:
echo   (no args)  Build for both architectures (default)
echo   --x64      Build for 64-bit Windows only
echo   --x86      Build for 32-bit Windows only
echo   --help     Show this help message
echo   -h         Show this help message
exit /b 1