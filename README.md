<div align="center">

<img src="docs/assets/logo-sub.png" alt="Logotipo do SUB" width="120">

# SUB · Sistema Único Bibliotecário

Sistema de gestão de bibliotecas escolares da rede estadual de ensino de Minas Gerais.

![C#](https://img.shields.io/badge/C%23-.NET%20Framework%204.8-512BD4?style=flat-square)
![Windows Forms](https://img.shields.io/badge/Interface-Windows%20Forms-0078D4?style=flat-square)
![SQL Server Compact](https://img.shields.io/badge/Banco-SQL%20Server%20Compact%204.0-CC2927?style=flat-square)
![Iniciativa oficial](https://img.shields.io/badge/Iniciativa%20oficial-SEE%2FMG-1F6E54?style=flat-square)

[Documentação](docs/) · [Backlog do produto](docs/backlog.md) · [Equipe](#equipe)

</div>

---

## Sobre o projeto

O SUB é uma aplicação desktop que reúne, em um único sistema, a rotina de uma biblioteca escolar: cadastro de acervo e de usuários, empréstimos, devoluções, prorrogações de prazo e emissão de relatórios.

O sistema é fruto da refatoração do projeto Biblioteca Gastão Valle e foi reestruturado para atender a rede estadual de ensino, padronizando os processos de cada unidade. <!-- Informar escolas piloto ou abrangência atual, se houver. -->

## Funcionalidades

- **Acesso:** login de bibliotecários(as) com senha protegida por hash e salt, e recuperação de senha
- **Usuários:** cadastro de alunos(as), professores(as), bibliotecários(as) e outros perfis, com edição e listagem
- **Turmas:** gerenciamento de turmas e assistente de mapeamento
- **Acervo:** cadastro, consulta e alteração de livros, com código de barras
- **Circulação:** empréstimo, empréstimo rápido, devolução, devolução rápida e prorrogação de prazo, com registro de prorrogações
- **Relatórios:** consulta e exportação para Excel e PDF
- **Primeiro uso:** configuração inicial da instituição e criação automática do banco de dados

## Status

Em desenvolvimento. <!-- Ajustar: piloto, produção ou versão atual. -->

## Tecnologias

| Camada | Tecnologia |
| --- | --- |
| Linguagem e plataforma | C# e .NET Framework 4.8 |
| Interface | Windows Forms, MaterialSkin |
| Banco de dados | SQL Server Compact 4.0 (ADO.NET) |
| Exportação | ClosedXML (Excel), iTextSharp e PDFsharp (PDF) |
| Código de barras | ZXing.Net |

## Executando localmente

### Requisitos

- Windows 10 ou superior
- Visual Studio com a carga de trabalho **Desenvolvimento para desktop com .NET**
- .NET Framework 4.8 Developer Pack
- SQL Server Compact 4.0 SP1 (runtime), caso não esteja instalado na máquina
- Acesso à internet na primeira compilação, para restaurar os pacotes NuGet

### Passos

1. Clone o repositório:
```bash
   git clone https://github.com/LouasDev/SistemaBiblioteca.git
```
2. Abra `SUB-SEEMG/SUB-SEEMG.sln` no Visual Studio.
3. Restaure os pacotes NuGet e compile a solução.
4. Copie `App.config.example` para `App.config` e ajuste as configurações locais.
5. Execute o projeto `SUB`.

No primeiro uso, o sistema solicita a configuração da instituição e cria o banco de dados automaticamente. Os dados ficam em `%AppData%\SUB\`.

> Configurações de conexão e senhas são locais de cada instalação e não devem ser publicadas em issues, commits ou documentação.

## Estrutura do repositório

```text
SistemaBiblioteca/
├── .github/            Configurações do GitHub
├── docs/               Documentação (backlog e demais documentos)
├── SUB-SEEMG/
│   ├── Elements/       Componentes visuais reutilizáveis
│   ├── Forms/          Telas, organizadas por módulo
│   ├── Models/         Modelos de domínio
│   ├── Properties/     Metadados e recursos do projeto
│   ├── Resources/      Imagens e ícones
│   └── Utils/          Serviços e utilitários
└── README.md
```

## Documentação

- [Backlog do produto](docs/backlog.md)

## Contribuindo

Mantenha cada alteração focada em uma única funcionalidade ou correção, valide a compilação antes de abrir o pull request e descreva o que mudou e como foi testado. As mensagens de commit seguem o formato `tipo: descrição` (`feat`, `fix`, `style`, `docs`, `chore`).

## Equipe

| Nome | Função |
| --- | --- |
| Ithalo Pimentel | Gerência de projeto e documentação |
| Matheus Felipe | Banco de dados e back-end |
| Luis Gustavo | Front-end e back-end |
| Renato Monteiro | Front-end e back-end |
| Bryan Emanuel | Front-end e testes |

## Licença

<!-- Definir com a equipe e a instituição responsável e adicionar o arquivo LICENSE. -->
