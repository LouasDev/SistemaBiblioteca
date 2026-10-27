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

Para verificar a migração do histórico e a gravação das prorrogações:

```powershell
.\Ferramentas\Testes\test-prorrogacao.ps1
```

Esses testes criam bancos SQL Server Compact fictícios em `artifacts`, sem acessar
o banco da instalação. Cobrem bases novas e os formatos antigos do histórico,
preservação de registros, bloqueio de empréstimos devolvidos, confirmação com
prazo desatualizado e reversão da operação quando a gravação do histórico falha.

Na primeira abertura da versão corrigida, a inicialização aplica a versão 5 do
esquema. O histórico mantém `DataHora` e `DataDaAcao`, preenchendo datas ausentes
a partir da informação existente. Novas prorrogações preenchem ambas, além do
novo prazo e do nome da bibliotecária. Informações antigas desconhecidas não são
inventadas. A regra de somar de 1 a 14 dias ao vencimento atual é preservada.

Para verificar empréstimos, devoluções e o painel semanal:

```powershell
.\Ferramentas\Testes\test-circulacao.ps1
```

Os testes usam exclusivamente bancos fictícios e cobrem os fluxos normal e rápido,
estoque insuficiente, última unidade, devoluções totais e parciais, confirmações
repetidas ou desatualizadas, falhas intermediárias e duas conexões disputando o
mesmo exemplar. Também verificam o filtro semanal sem exclusão de histórico.

O fluxo rápido permanece destinado a professores e livros do gênero `Didático`.
O painel considera a semana de segunda-feira a domingo: devolvidos integralmente
antes da segunda-feira atual ficam ocultos; ativos, faltantes e registros com
informações incompletas continuam visíveis. Os relatórios mantêm acesso ao histórico.
A rotina de exclusão semanal e a reinicialização de identificadores foram removidas;
o antigo arquivo `limpeza.txt` não é mais utilizado. Registros excluídos por versões
anteriores não podem ser recuperados por essa correção.

Cada retirada ou devolução grava o empréstimo e a movimentação de estoque na mesma
transação. Uma devolução parcial repõe apenas o recebido e mantém `Quantidade`
como saldo pendente no empréstimo rápido. O limite existente de duas pendências
bloqueia novas retiradas, mas não impede regularizar uma pendência já marcada como
`Faltando`. Informar zero recebidos mantém todo o saldo como faltante, após a
confirmação do operador, sem repor exemplares.

Para testar manualmente com dados isolados, utilize outra conta do Windows ou uma máquina virtual. Copiar o executável para outra pasta mantém o uso da mesma área `%LOCALAPPDATA%\SUB`.

## Guias relacionados

### Regras compartilhadas de circulação

`SUB-SEEMG/Utils/RegrasCirculacao.cs` concentra as políticas usadas pelos
formulários e pelos serviços `CirculacaoService` e `ProrrogacaoService`, sem
dependências de banco ou interface. Os serviços mantêm as transações e as
condições SQL que protegem a gravação contra dados desatualizados.

- Prazo sugerido de sete dias e comparação das datas informadas, incluindo horário.
- Personalização obrigatória de prazo para o perfil `Professor(a)`, conforme o fluxo existente.
- Limite existente de um empréstimo com status `Ativo` para `Aluno(a)`; a contagem
  de atrasados não foi alterada nesta reorganização.
- Limite de duas pendências rápidas, mantendo a regularização das já existentes.
- Quantidades válidas para retirada, saldo após devolução e situações que permitem movimentação.
- Prorrogação de 1 a 14 dias sobre o prazo atual, com a mesma previsão na tela e
  na gravação, inclusive proteção contra datas fora do intervalo representável.
- Categoria `Didático` compartilhada entre sugestões, leitura por código de
  barras e gravação do empréstimo rápido.

A validação de prazo também ocorre no serviço. O formulário fornece as mesmas
datas usadas na validação visual; a data de registro no banco continua sendo o
momento da operação, como no comportamento anterior. A centralização não adiciona
uma migração de banco nem novas configurações de prazo.

Os testes das regras estão incluídos em `test-circulacao.ps1`. A simulação completa
pela interface será realizada após as demais correções, em ambiente isolado.

### Referências

- [Reset local e exclusão de dados](RESET-LOCAL.md)
- [Preparação e publicação de atualizações](PUBLICACAO-ATUALIZACOES.md)
- [Roteiro de validação da interface](../Ferramentas/Testes/roteiro-teste-etapaD.md)
- [Script de testes do reset](../Ferramentas/Testes/test-reset.ps1)
- [Script de testes das prorrogações](../Ferramentas/Testes/test-prorrogacao.ps1)
- [Script de testes da circulação](../Ferramentas/Testes/test-circulacao.ps1)

