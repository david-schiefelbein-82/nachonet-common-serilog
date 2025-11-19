@setlocal
@set VER=10.0.0.1
pushd Nachonet.Common.Serilog
dotnet pack /p:AssemblyVersion=%VER% /p:Version=%VER%

move ".\bin\Release\Nachonet.Common.Serilog.%VER%.nupkg" "%DEV_PACKAGE_DIR%"
popd
@endlocal