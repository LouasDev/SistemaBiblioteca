[CmdletBinding()]
param(
    [ValidateSet("Debug", "Release")]
    [string]$Configuration = "Debug"
)

$ErrorActionPreference = "Stop"
$repoRoot = Split-Path -Parent $PSScriptRoot

$testProject = Join-Path $PSScriptRoot "SUB.Tests.csproj"
$runSettings = Join-Path $PSScriptRoot "test.runsettings"
$results = Join-Path $PSScriptRoot "TestResults"

$msbuild = $null
$vswhere = Join-Path ${env:ProgramFiles(x86)} "Microsoft Visual Studio\Installer\vswhere.exe"
if (Test-Path $vswhere)
{
    $msbuild = & $vswhere -latest -products * -requires Microsoft.VisualStudio.Workload.ManagedDesktop -find "MSBuild\**\Bin\MSBuild.exe" |
        Select-Object -First 1
}

if ($msbuild)
{
    & $msbuild $testProject /restore /t:Build /p:Configuration=$Configuration /v:minimal /nologo
}
else
{
    throw "MSBuild do Visual Studio não encontrado. Instale o desenvolvimento desktop .NET para compilar o SUB e os testes."
}

if ($LASTEXITCODE -ne 0)
{
    exit $LASTEXITCODE
}

& dotnet test $testProject -c $Configuration --no-build --no-restore --settings $runSettings `
    --logger "trx;LogFileName=resultados.trx" --results-directory $results
exit $LASTEXITCODE