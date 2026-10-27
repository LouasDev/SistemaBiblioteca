$ErrorActionPreference = 'Stop'
$raiz = (Resolve-Path (Join-Path $PSScriptRoot '..\..')).Path
$candidatos = @(
    (Join-Path $env:ProgramFiles 'Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe'),
    (Join-Path $env:ProgramFiles 'Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe')
)
$msbuild = $candidatos | Where-Object { Test-Path $_ } | Select-Object -First 1
if (!$msbuild) { throw 'MSBuild não encontrado. Instale o desenvolvimento desktop .NET no Visual Studio.' }
& $msbuild (Join-Path $raiz 'SUB-SEEMG\SUB-SEEMG.sln') /t:Build /p:Configuration=Debug /v:quiet /nologo
if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }
$bin = Join-Path $raiz 'SUB-SEEMG\bin\Debug'
$sqlCe = Join-Path $env:ProgramFiles 'Microsoft SQL Server Compact Edition\v4.0\Desktop\System.Data.SqlServerCe.dll'
$csc = Join-Path $env:WINDIR 'Microsoft.NET\Framework64\v4.0.30319\csc.exe'
if (!(Test-Path $csc)) { $csc = Join-Path $env:WINDIR 'Microsoft.NET\Framework\v4.0.30319\csc.exe' }
$exe = Join-Path $env:TEMP ('SUB-TesteProrrogacao-' + [Guid]::NewGuid().ToString('N') + '.exe')
& $csc /nologo /platform:anycpu "/r:$bin\SUB.exe" "/r:$sqlCe" /r:System.Data.dll `
    "/out:$exe" (Join-Path $PSScriptRoot 'TesteProrrogacao.cs') `
    (Join-Path $raiz 'SUB-SEEMG\Utils\ProrrogacaoService.cs') `
    (Join-Path $raiz 'SUB-SEEMG\Utils\RegrasCirculacao.cs')
if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }
$pastaTeste = Join-Path $raiz ('artifacts\testes-prorrogacao-' + [Guid]::NewGuid().ToString('N'))
Write-Output "Bancos fictícios: $pastaTeste"
& $exe $bin $pastaTeste
exit $LASTEXITCODE
