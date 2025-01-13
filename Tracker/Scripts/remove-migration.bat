@ECHO OFF

:: %1 = MySql | SqlServer

IF "%~1"=="" (
	CALL :dodotnet MySql
	CALL :dodotnet SqlServer
) ELSE (
	CALL :dodotnet %1
)

GOTO :eof

:dodotnet
dotnet ef migrations remove --project .\Infrastructure.Store.%~1Migrations --startup-project .\WebApp -- --Store:Provider %~1 --force
