param(
    [Parameter(Mandatory = $true)]
    [string]$Manifesto,

    [string]$ChavePrivada = (Join-Path $env:USERPROFILE '.sub-releases\chave-privada.xml')
)

if (-not (Test-Path $Manifesto)) {
    Write-Error "Manifesto nao encontrado: $Manifesto"
    exit 1
}

if (-not (Test-Path $ChavePrivada)) {
    Write-Error "Chave privada nao encontrada: $ChavePrivada (rode GerarChaveAssinatura.ps1 antes)."
    exit 1
}

$bytes = [IO.File]::ReadAllBytes((Resolve-Path $Manifesto))

$rsa = [System.Security.Cryptography.RSA]::Create()
$rsa.FromXmlString([IO.File]::ReadAllText($ChavePrivada))

$assinatura = $rsa.SignData(
    $bytes,
    [System.Security.Cryptography.HashAlgorithmName]::SHA256,
    [System.Security.Cryptography.RSASignaturePadding]::Pkcs1)

$caminhoSig = "$Manifesto.sig"
[IO.File]::WriteAllText($caminhoSig, [Convert]::ToBase64String($assinatura), (New-Object Text.UTF8Encoding $false))

Write-Output "Assinatura gravada em: $caminhoSig"
Write-Output "Publique version.json e version.json.sig juntos, com os bytes do JSON exatamente como estao agora."
