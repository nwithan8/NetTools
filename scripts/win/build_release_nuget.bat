:: This script will build a .NET project in Release mode and package the DLLs into a NuGet package.
:: This script also handles pre-run cleanup (will delete old DLLs and NuGet package files)

:: Requirements:
:: - NuGet is installed on the machine and is accessible everywhere (added to PATH)
:: - dotnet is installed on the machine and is accessible everywhere (added to PATH) (might be in C:\Program Files\dotnet)

@ECHO OFF

:: Parse command line arguments
SET projectName=%1
SET buildMode=%5

:: Delete old files
CALL "scripts\delete_old_assemblies.bat"

:: Restore dependencies and build solution
CALL "scripts\build_project.bat" %buildMode% || GOTO :commandFailed

:: Package the DLLs in a NuGet package (will fail if DLLs are missing)
CALL "scripts\pack_nuget.bat" %projectName% || GOTO :commandFailed

SET nugetFileName=
FOR /R %%F IN (*.nupkg) DO (
    SET nugetFileName="%%F"
)
IF [%nugetFileName%]==[] (
    ECHO Could not find NuGet package.
    GOTO :exitWithError
)

:: Present final information
ECHO:
ECHO NuGet file %nugetFileName% is ready.

GOTO :eof


:usage
@ECHO:
@ECHO Usage: %0 <PROJECT_NAME> <BUILD_MODE>
GOTO :exitWithError

:commandFailed
@ECHO Step failed.
GOTO :exitWithError

:exitWithError
@ECHO Exiting...
EXIT /B 1
