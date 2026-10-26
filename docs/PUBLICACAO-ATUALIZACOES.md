# Publicação de atualizações

O código-fonte e os pacotes do SUB compartilham o repositório público
`LouasDev/SistemaBiblioteca`. Não é necessário criar um repositório separado.

O aplicativo consulta:

`https://github.com/LouasDev/SistemaBiblioteca/releases/latest/download/version.json`

Enquanto não houver uma release com esse arquivo, a consulta deve informar HTTP 404.
A versão instalada não precisa ter sido publicada anteriormente no GitHub.

## Preparação do pacote

1. Em `SUB-SEEMG/Properties/AssemblyInfo.cs`, aumente `AssemblyFileVersion`, por exemplo,
   de `1.0.0.0` para `1.0.0.1`. Mantenha `AssemblyVersion` conforme a orientação do arquivo.
2. Compile a solução com a configuração de distribuição `1.0`.
3. Prepare um ZIP contendo `SUB.exe`, `SUB.exe.config`, `SUB-UPDATER.exe` e todas as
   dependências necessárias. Os executáveis devem ficar na raiz do ZIP.
4. Não inclua banco, `instituicao.json`, chave privada nem pastas de dados locais.
5. Calcule o SHA-256 e o tamanho em bytes do ZIP. O limite aceito é 300 MiB.

## Manifesto e assinatura

Crie `version.json` com os valores reais do pacote. O exemplo abaixo precisa ter
`sha256` e `tamanho` substituídos antes da assinatura:

```json
{
  "versao": "1.0.0.1",
  "data": "26/10/2026",
  "url": "https://github.com/LouasDev/SistemaBiblioteca/releases/download/v1.0.0.1/SUB-1.0.0.1.zip",
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

- `SUB-1.0.0.1.zip`
- `version.json`
- `version.json.sig`

Para o endereço padrão do aplicativo, use uma release publicada como Latest.
Para testar uma pré-release, altere `ManifestoAtualizacaoUrl` somente no
`SUB.exe.config` da cópia de teste para a URL específica da tag:

`https://github.com/LouasDev/SistemaBiblioteca/releases/download/v1.0.0.1/version.json`

Teste com uma versão instalada inferior à publicada: verifique a detecção,
o download, o reinício e a nova versão exibida em Configurações. A tag, sozinha,
não altera a versão do executável.

Use outro usuário do Windows ou uma máquina virtual para isolar também os dados.
Copiar o aplicativo para outra pasta não isola `%LOCALAPPDATA%/SUB`.
