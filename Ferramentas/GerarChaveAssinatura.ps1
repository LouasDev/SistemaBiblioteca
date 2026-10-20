param(
    [switch]$Forcar
)

$pasta = Join-Path $env:USERPROFILE '.sub-releases'
$privada = Join-Path $pasta 'chave-privada.xml'

New-Item -ItemType Directory -Force -Path $pasta | Out-Null

if ((Test-Path $privada) -and -not $Forcar) {
    Write-Error "Ja existe chave privada em $privada. Use -Forcar para sobrescrever (quebra a verificacao das assinaturas antigas)."
    exit 1
}

$rsa = New-Object System.Security.Cryptography.RSACng
$rsa.KeySize = 2048

$xmlPrivada = $rsa.ToXmlString($true)
$xmlPublica = $rsa.ToXmlString($false)

[IO.File]::WriteAllText($privada, $xmlPrivada, (New-Object Text.UTF8Encoding $false))

Write-Output "Chave privada gravada em: $privada"
Write-Output "IMPORTANTE: esta pasta NUNCA vai para o repositorio nem para os releases."
Write-Output "Guarda tambem uma copia em cofre/gestor de senhas."
Write-Output ""
Write-Output "Chave publica (embutir no const ChavePublicaAssinaturaXml em Utils\AtualizacaoService.cs):"
Write-Output $xmlPublica
