<div align="center">

<!-- Recomendação: Adicione a logo do Governo/SEE-MG ao lado da logo do sistema na pasta assets -->
<img src="docs/assets/logo-sub.png" alt="Logotipo do SUB" width="120">

# SUB · Sistema Único Bibliotecário

**Uma solução para a gestão integrada das bibliotecas escolares da rede estadual de ensino de Minas Gerais.**

![Versão](https://img.shields.io/badge/Versão-1.0-blue?style=flat-square)
![C#](https://img.shields.io/badge/C%23-.NET%20Framework%204.8-512BD4?style=flat-square)
![Windows Forms](https://img.shields.io/badge/Interface-Windows%20Forms-0078D4?style=flat-square)
![Banco](https://img.shields.io/badge/Banco-SQL%20Server%20Compact%204.0-CC2927?style=flat-square)
![Licença](https://img.shields.io/badge/Licença-MIT-green?style=flat-square)
![Iniciativa oficial](https://img.shields.io/badge/Iniciativa%20oficial-SEE%2FMG-1F6E54?style=flat-square)

[Documentação](docs/) · [Backlog do Produto](docs/backlog.md) · [Equipe](#equipe-do-projeto)

</div>

---

## 🏛 Sobre o Sistema

O SUB é um sistema unificado de gestão bibliotecária criado a partir da experiência de um sistema desenvolvido pelos alunos da Escola Estadual Professor Gastão Valle para a biblioteca da própria escola, em Bocaiuva, no Norte de Minas Gerais.

Após a apresentação do projeto original ao Governo de Minas Gerais, a equipe foi convidada a estruturar uma solução voltada às bibliotecas da rede estadual de ensino. O objetivo principal é padronizar os processos de cada unidade escolar, reunindo os fluxos essenciais da rotina bibliotecária em uma única plataforma.

## ⚙️ O que o SUB integra

A aplicação foi desenhada para cobrir de ponta a ponta a operação diária das bibliotecas:

* **Acervo, livros e consultas**: Cadastro, alteração e busca estruturada de títulos literários e didáticos, com suporte à leitura de código de barras.
* **Usuários, turmas e cadastros**: Gerenciamento completo de alunos, professores e funcionários, incluindo assistente de mapeamento de turmas e controle de acessos para bibliotecários (com senhas protegidas por *hash* e *salt*).
* **Empréstimos, devoluções e prorrogações**: Módulo de circulação otimizado para fluxo contínuo, contemplando operações rápidas e registro histórico de renovações.
* **Relatórios e recursos de apoio à operação**: Consulta, filtragem e exportação de dados operacionais para formatos Excel (XLSX) e PDF.

## 💻 Base Tecnológica

O sistema foi construído visando alta compatibilidade com o parque tecnológico das escolas estaduais, rodando localmente sem dependência estrita de servidores externos contínuos.

| Camada | Tecnologia |
| --- | --- |
| **Linguagem / Plataforma** | C# e .NET Framework 4.8 |
| **Interface (UI)** | Windows Forms com MaterialSkin |
| **Banco de Dados** | SQL Server Compact 4.0 (ADO.NET) |
| **Exportação de Dados** | ClosedXML (Excel), iTextSharp e PDFsharp (PDF) |
| **Leitura / Código de Barras**| ZXing.Net |

## 🚀 Executando Localmente

### Pré-requisitos do Ambiente

* Windows 10 ou superior.
* Visual Studio com a carga de trabalho **Desenvolvimento para desktop com .NET**.
* .NET Framework 4.8 Developer Pack.
* SQL Server Compact 4.0 SP1 (runtime) instalado na máquina host.
* Conexão à internet na primeira compilação para restauração dos pacotes NuGet.

### Instruções de Instalação

1. Clone o repositório oficial:
   ```bash
   git clone [https://github.com/LouasDev/SistemaBiblioteca.git](https://github.com/LouasDev/SistemaBiblioteca.git)
