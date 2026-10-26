param(
    [ValidatePattern('\A[0-9]+\.[0-9]+\.[0-9]+\z')]
    [string]$Versao = '1.0.1',
    [string]$Notas = 'Primeira release pública do SUB. Operação local, configuração da instituição, acervo, usuários, circulação, relatórios e painel de atualização corrigido.',
    [string]$ChavePrivada = (Join-Path $env:USERPROFILE '.sub-releases\chave-privada.xml')
)

$ErrorActionPreference = 'Stop'
$raiz = (Resolve-Path (Join-Path $PSScriptRoot '..')).Path
$executavel = Join-Path $raiz 'SUB-SEEMG\bin\Release\SUB.exe'
$updater = Join-Path $raiz 'SUB-SEEMG\bin\Release\SUB-UPDATER.exe'
$metadados = [Diagnostics.FileVersionInfo]::GetVersionInfo($executavel)
$versaoBinaria = [version]$metadados.FileVersion
$versaoArquivo = $versaoBinaria.ToString(3)
if ($versaoBinaria.Revision -gt 0 -or $versaoArquivo -ne $Versao -or $metadados.ProductVersion -ne $Versao) {
    throw "A versão do executável ($versaoArquivo) não corresponde à release $Versao."
}
if (!(Test-Path $updater)) { throw 'Compile também o SUB-UPDATER em Release.' }
$metadadosAtualizador = [Diagnostics.FileVersionInfo]::GetVersionInfo($updater)
$versaoAtualizador = [version]$metadadosAtualizador.FileVersion
if ($versaoAtualizador.Revision -gt 0 -or $versaoAtualizador.ToString(3) -ne $Versao -or $metadadosAtualizador.ProductVersion -ne $Versao) {
    throw "A versão do atualizador não corresponde à release $Versao."
}

# Confere a identidade da assinatura antes de gerar qualquer pacote.
$rsa = [Security.Cryptography.RSA]::Create()
try {
    $rsa.FromXmlString([IO.File]::ReadAllText($ChavePrivada))
    $servico = [IO.File]::ReadAllText((Join-Path $raiz 'SUB-SEEMG\Utils\AtualizacaoService.cs'))
    $publica = [regex]::Match($servico, '<RSAKeyValue>.*?</RSAKeyValue>').Value
    if ($rsa.ToXmlString($false) -ne $publica) {
        throw 'A chave privada não corresponde à chave pública do aplicativo.'
    }
} finally { $rsa.Dispose() }

$destino = Join-Path $raiz ('artifacts\releases\v' + $Versao)
if (Test-Path $destino) { throw "A pasta já existe: $destino. Preserve ou mova o pacote anterior." }
$pacote = Join-Path $destino 'pacote'
New-Item -ItemType Directory -Path $pacote -Force | Out-Null
$bin = Split-Path $executavel
Get-ChildItem $bin -File | Where-Object { $_.Extension -in '.exe', '.dll', '.config' } |
    Copy-Item -Destination $pacote

# Distribuição privada completa do SQL CE, para Windows de 32 e 64 bits.
$sqlCe = Join-Path $env:ProgramFiles 'Microsoft SQL Server Compact Edition\v4.0\Private'
if (!(Test-Path $sqlCe)) { throw 'SQL Server Compact 4.0 SP1 não encontrado para empacotamento.' }
Copy-Item (Join-Path $sqlCe 'System.Data.SqlServerCe.dll') -Destination $pacote -Force
foreach ($pasta in 'x86', 'amd64', 'PT') {
    Copy-Item (Join-Path $sqlCe $pasta) -Destination $pacote -Recurse
}
$licencas = Join-Path $pacote 'licencas-sqlce'
New-Item -ItemType Directory -Path $licencas | Out-Null
foreach ($arquivo in 'EULA_PTB.rtf', 'REDIST_PTB.TXT') {
    Copy-Item (Join-Path (Split-Path $sqlCe) $arquivo) -Destination $licencas
}

$instrucoes = @"
SUB $Versao - Sistema Único Bibliotecário

Requisitos: Windows e .NET Framework 4.8.
Extraia todo o ZIP para uma pasta com permissão de gravação e execute SUB.exe.
Não execute diretamente de dentro do ZIP.
O primeiro uso configura a instituição, a bibliotecária e as turmas.
Guarde o código de recuperação solicitado pelo assistente em local seguro.
Os dados ficam em %LOCALAPPDATA%\SUB, separados dos arquivos do aplicativo.

Código-fonte e releases: https://github.com/LouasDev/SistemaBiblioteca

$Notas
A versão $versaoArquivo exibida em Configurações corresponde à release $Versao.
Uma instalação nessa mesma versão não indicará nova atualização disponível.
"@
[IO.File]::WriteAllText((Join-Path $pacote 'LEIA-ME.txt'), $instrucoes, [Text.UTF8Encoding]::new($false))

$prefixo = $pacote.TrimEnd('\') + '\'
$arquivos = @(Get-ChildItem $pacote -File -Recurse |
    ForEach-Object { $_.FullName.Substring($prefixo.Length) } | Sort-Object)
$arquivos += 'arquivos.txt'
[IO.File]::WriteAllLines((Join-Path $pacote 'arquivos.txt'), $arquivos, [Text.UTF8Encoding]::new($false))

$zip = Join-Path $destino ('SUB-' + $Versao + '.zip')
Add-Type -AssemblyName System.IO.Compression.FileSystem
[IO.Compression.ZipFile]::CreateFromDirectory($pacote, $zip, [IO.Compression.CompressionLevel]::Optimal, $false)
$manifesto = [ordered]@{
    versao = $Versao
    data = (Get-Date).ToString('dd/MM/yyyy')
    url = "https://github.com/LouasDev/SistemaBiblioteca/releases/download/v$Versao/SUB-$Versao.zip"
    sha256 = (Get-FileHash -LiteralPath $zip -Algorithm SHA256).Hash.ToLowerInvariant()
    tamanho = (Get-Item -LiteralPath $zip).Length
    notas = $Notas
}
$json = Join-Path $destino 'version.json'
[IO.File]::WriteAllText($json, ($manifesto | ConvertTo-Json), [Text.UTF8Encoding]::new($false))
& (Join-Path $PSScriptRoot 'AssinarManifesto.ps1') -Manifesto $json -ChavePrivada $ChavePrivada
if ($LASTEXITCODE -and $LASTEXITCODE -ne 0) { throw 'Falha ao assinar o manifesto.' }
Write-Output "Pacote preparado: $zip"
