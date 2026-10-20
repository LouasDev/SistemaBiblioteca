# Harness da Etapa D - interface de atualizacao no ConfiguracaoForm.
# Compila a solucao, compila o auxiliar TesteFormD.cs com csc do framework
# e executa os testes automatizaveis. Sem NuGet, sem tocar em dados reais.
$ErrorActionPreference = 'Stop'

$raiz = (Resolve-Path (Join-Path $PSScriptRoot '..\..')).Path
$sln = Join-Path $raiz 'SUB-SEEMG\SUB-SEEMG.sln'
$bin = Join-Path $raiz 'SUB-SEEMG\bin\Debug'
$subExe = Join-Path $bin 'SUB.exe'

$msbuildCandidates = @(
    (Join-Path ${env:ProgramFiles} 'Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe'),
    (Join-Path ${env:ProgramFiles} 'Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe'),
    (Join-Path ${env:ProgramFiles(x86)} 'Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe')
)
$msbuild = $msbuildCandidates | Where-Object { Test-Path $_ } | Select-Object -First 1
if (-not $msbuild) { Write-Host 'FALHOU msbuild nao encontrado'; exit 2 }

Write-Host '== Build da solucao =='
& $msbuild $sln /t:Build /p:Configuration=Debug /v:quiet /nologo
if ($LASTEXITCODE -ne 0) { Write-Host 'FALHOU build da solucao'; exit 1 }
if (-not (Test-Path $subExe)) { Write-Host "FALHOU SUB.exe ausente em $bin"; exit 1 }

$trabalho = Join-Path $env:TEMP 'sub-etapaD'
New-Item -ItemType Directory -Force $trabalho | Out-Null
$exe = Join-Path $trabalho 'TesteFormD.exe'
$fonte = Join-Path $PSScriptRoot 'TesteFormD.cs'
$csc = Join-Path $env:WINDIR 'Microsoft.NET\Framework64\v4.0.30319\csc.exe'
if (-not (Test-Path $csc)) { $csc = Join-Path $env:WINDIR 'Microsoft.NET\Framework\v4.0.30319\csc.exe' }

Write-Host '== Compilando auxiliar de teste =='
& $csc /nologo /platform:anycpu `
    /r:"$bin\SUB.exe" /r:System.Windows.Forms.dll /r:System.Drawing.dll `
    /out:$exe $fonte
if ($LASTEXITCODE -ne 0) { Write-Host 'FALHOU compilacao do auxiliar'; exit 1 }

Write-Host '== Executando testes =='
Push-Location $bin
try { & $exe $subExe; $codigo = $LASTEXITCODE }
finally { Pop-Location }

Write-Host ''
if ($codigo -eq 0) { Write-Host 'ETAPA D: TODOS OS TESTES AUTOMATIZAVEIS PASSARAM' }
else { Write-Host "ETAPA D: $codigo TESTE(S) FALHARAM" }
exit $codigo
