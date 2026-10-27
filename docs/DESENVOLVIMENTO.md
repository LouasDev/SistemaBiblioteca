# Guia de desenvolvimento do SUB

[Voltar ao README](../README.md)

## Ambiente necessário

- Windows.
- Visual Studio com a carga de trabalho **Desenvolvimento para desktop com .NET**, MSBuild e suporte a projetos SDK .NET.
- **.NET Framework 4.8 Developer Pack** para compilação.
- **SQL Server Compact 4.0**, incluindo o provedor gerenciado e as bibliotecas nativas compatíveis com a arquitetura de execução.
- NuGet para restauração dos pacotes da solução.

## Executar a partir do código-fonte

```powershell
git clone https://github.com/LouasDev/SistemaBiblioteca.git
cd SistemaBiblioteca
```

1. Abra `SUB-SEEMG\SUB-SEEMG.sln` no Visual Studio.
2. Restaure os pacotes NuGet da solução.
3. Selecione **Debug | Any CPU** e defina **SUB** como projeto de inicialização.
4. Compile a solução e execute o projeto.

O executável principal é gerado em `SUB-SEEMG\bin\Debug\SUB.exe`. O banco é criado automaticamente na área de dados local; não é necessário copiar uma base de outra instituição.

Como alternativa, após restaurar os pacotes, use o **Developer PowerShell do Visual Studio**:

```powershell
msbuild .\SUB-SEEMG\SUB-SEEMG.sln /t:Build /p:Configuration=Debug
```

Para distribuição, o projeto principal utiliza a configuração **`1.0`**, com saída em `bin\Release`. O atualizador deve ser compilado também em **Release**, conforme as verificações de `Ferramentas\PrepararRelease.ps1`.

## Tecnologias

| Componente | Tecnologia |
| --- | --- |
| Linguagem e plataforma | C# · .NET Framework 4.8 |
| Interface | Windows Forms · controles personalizados · MaterialSkin |
| Persistência | SQL Server Compact 4.0 · ADO.NET |
| Planilhas | ClosedXML |
| Documentos PDF | iTextSharp · PDFsharp |
| Códigos de barras | ZXing.Net |
| Configurações e manifestos | Newtonsoft.Json |
| Atualizador | Executável separado, `SUB-UPDATER` |

## Organização do repositório

```text
SistemaBiblioteca/
├── SUB-SEEMG/
│   ├── Forms/             Telas e arquivos do Designer
│   ├── Models/            Modelos de dados e sessão
│   ├── Utils/             Serviços, persistência e utilitários
│   ├── Resources/         Recursos visuais
│   ├── SUB-UPDATER/       Projeto do atualizador
│   ├── SUB.csproj         Projeto principal
│   └── SUB-SEEMG.sln      Solução do Visual Studio
├── Ferramentas/           Preparação de releases e testes
├── docs/                  Documentação técnica e funcional
└── README.md
```

## Verificações

Há verificações automatizadas e roteiros de validação em `Ferramentas\Testes`. Para executar os testes do reset, na raiz do repositório:

```powershell
.\Ferramentas\Testes\test-reset.ps1
```

O script exige o ambiente de compilação e o SQL Server Compact configurados. Os testes do reset utilizam dados fictícios em pastas próprias e geram prévias em `artifacts`.

Para testar manualmente com dados isolados, utilize outra conta do Windows ou uma máquina virtual. Copiar o executável para outra pasta mantém o uso da mesma área `%LOCALAPPDATA%\SUB`.

## Guias relacionados

- [Reset local e exclusão de dados](RESET-LOCAL.md)
- [Preparação e publicação de atualizações](PUBLICACAO-ATUALIZACOES.md)
- [Roteiro de validação da interface](../Ferramentas/Testes/roteiro-teste-etapaD.md)
- [Script de testes do reset](../Ferramentas/Testes/test-reset.ps1)

