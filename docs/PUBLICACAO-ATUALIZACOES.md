# Publicação de atualizações

[Voltar ao README](../README.md) · [Guia de desenvolvimento](DESENVOLVIMENTO.md)

O código-fonte e os pacotes do SUB compartilham o repositório público
`LouasDev/SistemaBiblioteca`. Não é necessário criar um repositório separado.

O aplicativo consulta:

`https://github.com/LouasDev/SistemaBiblioteca/releases/latest/download/version.json`

Enquanto não houver uma release com esse arquivo, a consulta deve informar HTTP 404.
A versão instalada não precisa ter sido publicada anteriormente no GitHub.

## Padrão obrigatório de versão

Use sempre **MAJOR.MINOR.PATCH**, com três números, por exemplo `1.0.2`.
O campo `versao` do manifesto não aceita quarto componente, prefixo `v`,
sufixos de teste ou metadados de build. A tag do GitHub usa `v1.0.2` como
identificador da release; o nome do pacote será `SUB-1.0.2.zip`.

Telas, mensagens e registros de atualização usam a mesma representação.
O Windows e o CLR podem representar a versão binária internamente com um
quarto zero; o aplicativo normaliza esses metadados sem mudar a versão pública.
Uma revisão binária diferente de zero não é descartada silenciosamente.

As notas das releases já publicadas registram o comportamento daquela época.

## Preparação do pacote

1. Em `SUB-SEEMG/Properties/AssemblyInfo.cs`, atualize `AssemblyFileVersion` e
   `AssemblyInformationalVersion`, por exemplo de `1.0.1` para `1.0.2`. Atualize
   também `ApplicationVersion` em `SUB.csproj` e as versões em
   `SUB-UPDATER/SUB-UPDATER.csproj`. Mantenha a identidade `AssemblyVersion`
   conforme a orientação do arquivo.
2. Compile a solução com a configuração de distribuição `1.0` e compile
   explicitamente o `SUB-UPDATER` em `Release`: na solução, a configuração `1.0`
   do atualizador ainda está mapeada para `Debug`.
3. Prepare um ZIP contendo `SUB.exe`, `SUB.exe.config`, `SUB-UPDATER.exe` e todas as
   dependências necessárias. Os executáveis devem ficar na raiz do ZIP.
4. Não inclua banco, `instituicao.json`, chave privada nem pastas de dados locais.
5. Calcule o SHA-256 e o tamanho em bytes do ZIP. O limite aceito é 300 MiB.

No Developer PowerShell do Visual Studio, a partir da raiz do repositório:

```powershell
msbuild .\SUB-SEEMG\SUB-SEEMG.sln /t:Build /p:Configuration=1.0
msbuild .\SUB-SEEMG\SUB-UPDATER\SUB-UPDATER.csproj /t:Build /p:Configuration=Release
```

Após compilar e conferir as versões, `Ferramentas\PrepararRelease.ps1` pode montar
o pacote, calcular hash e tamanho e assinar o manifesto. Por exemplo, para uma
versão de destino `1.0.2` já aplicada aos executáveis:

```powershell
.\Ferramentas\PrepararRelease.ps1 -Versao 1.0.2 -Notas 'Descreva as mudanças efetivas desta versão.'
```

Esse comando exige a chave privada correspondente à pública do aplicativo,
gera arquivos em `artifacts\releases\v1.0.2` e não publica no GitHub.
Ele recusa uma pasta de destino já existente e divergências de versão ou chave.
Use o fluxo manual abaixo como alternativa; um manifesto gerado e assinado pelo
script não precisa ser recriado manualmente.

## Manifesto e assinatura

Crie `version.json` com os valores reais do pacote. O exemplo abaixo precisa ter
`sha256` e `tamanho` substituídos antes da assinatura:

```json
{
  "versao": "1.0.2",
  "data": "26/10/2026",
  "url": "https://github.com/LouasDev/SistemaBiblioteca/releases/download/v1.0.2/SUB-1.0.2.zip",
  "sha256": "SUBSTITUIR_PELO_SHA256_DO_ZIP",
  "tamanho": 0,
  "notas": "Correções e melhorias desta versão."
}
```

Na raiz do projeto, assine com:

```powershell
.\Ferramentas\AssinarManifesto.ps1 -Manifesto .\version.json
```

A chave privada deve corresponder à chave pública embutida em
`SUB-SEEMG/Utils/AtualizacaoService.cs`. A assinatura permanece necessária mesmo
com o código aberto: ela identifica os pacotes oficiais. A chave privada fica
fora do repositório e não deve ser anexada à release. Não gere outra chave para
cada versão; isso invalida a confiança dos aplicativos já distribuídos.

Não edite o JSON depois de assiná-lo. Se precisar alterá-lo, assine novamente.

## Publicação e teste

Publique uma release com a tag usada na URL do manifesto e anexe:

- `SUB-1.0.2.zip`
- `version.json`
- `version.json.sig`

Para o endereço padrão do aplicativo, use uma release publicada como Latest.
Para testar uma pré-release, altere `ManifestoAtualizacaoUrl` somente no
`SUB.exe.config` da cópia de teste para a URL específica da tag:

`https://github.com/LouasDev/SistemaBiblioteca/releases/download/v1.0.2/version.json`

Teste com uma versão instalada inferior à publicada: verifique a detecção,
o download, o reinício e a nova versão exibida em Configurações. A tag, sozinha,
não altera a versão do executável.

Use outro usuário do Windows ou uma máquina virtual para isolar também os dados.
Copiar o aplicativo para outra pasta não isola `%LOCALAPPDATA%/SUB`.

Para testes manuais, consulte o [roteiro da interface do atualizador](../Ferramentas/Testes/roteiro-teste-etapaD.md).
