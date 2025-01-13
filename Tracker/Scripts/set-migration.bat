@ECHO OFF

:: %1 = migration name
:: %2 = MySql | SqlServer
 
IF "%~1"=="" GOTO :eof

IF "%~2"=="" (
	CALL :dodotnet %1 MySql
	CALL :dodotnet %1 SqlServer
) ELSE (
	CALL :dodotnet %1 %2
)

GOTO :eof

:dodotnet
dotnet ef database update %1 --project .\Infrastructure.Store.%~2Migrations --startup-project .\WebApp -- --Store:Provider %~2
