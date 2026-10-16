<div align="center">

<img src="docs/assets/logo-sub.png" alt="Logotipo do SUB" width="140">

# SUB · Sistema Único Bibliotecário

**Plataforma unificada de gestão para bibliotecas escolares da rede estadual de ensino de Minas Gerais.**[cite: 1]

[![Versão](https://img.shields.io/badge/Versão-1.0.0--beta-blue.svg?style=for-the-badge)](https://github.com/LouasDev/SistemaBiblioteca/releases)
[![C#](https://img.shields.io/badge/C%23-NET_Framework_4.8-512BD4?style=for-the-badge&logo=csharp)](https://dotnet.microsoft.com/)
[![Windows Forms](https://img.shields.io/badge/UI-Windows_Forms-0078D4?style=for-the-badge&logo=windows)](https://docs.microsoft.com/en-us/dotnet/desktop/winforms/)
[![Banco de Dados](https://img.shields.io/badge/Database-SQL_Compact_4.0-CC2927?style=for-the-badge&logo=microsoftsqlserver)](#)
[![Licença](https://img.shields.io/badge/Licença-MIT-brightgreen.svg?style=for-the-badge)](LICENSE)

---

O **SUB** centraliza, padroniza e automatiza a operação diária das bibliotecas, desde a catalogação do acervo literário até a gestão de circulação e métricas de leitura dos alunos.[cite: 1]

[Funcionalidades](#-funcionalidades-principais) •
[Arquitetura](#-arquitetura-e-tecnologias) •
[Instalação](#-guia-de-instalação) •
[Contribuição](#-guia-de-contribuição) •
[Equipe](#-equipe)

</div>

---

## 🏛 Contexto Institucional

O projeto nasceu de uma iniciativa acadêmica na **Escola Estadual Professor Gastão Valle**, no município de Bocaiuva, região Norte de Minas Gerais[cite: 1]. Identificando a ausência de uma ferramenta padronizada para a gestão do acervo local, a equipe desenvolveu a primeira iteração do sistema[cite: 1]. 

Com a validação do modelo, o software foi reestruturado sob diretrizes do Governo de Minas Gerais para escalabilidade, visando atender a totalidade das bibliotecas da rede estadual de ensino, integrando fluxos essenciais de operação em uma interface única e local[cite: 1].

## ✨ Funcionalidades Principais

O sistema é dividido em quatro módulos de negócio principais:

### 1. Catalogação e Acervo[cite: 1]
* **Gestão de Títulos:** Cadastro completo de obras literárias, didáticas e periódicos.
* **Classificação Dinâmica:** Sistema de autocompletar para gêneros literários (Ficção Científica, Romance, Poesia) e áreas do conhecimento (Exatas, Informática, História).
* **Automação por Código de Barras:** Suporte nativo a leitores de código de barras (ISBN/SKU) para entrada e saída rápida de exemplares.

### 2. Gestão de Usuários e Acessos[cite: 1]
* **Perfis Híbridos:** Controle de cadastros para Alunos, Professores e Equipe Escolar.
* **Mapeamento Escolar:** Assistente inteligente para progressão e gerenciamento de Turmas e Turnos.
* **Segurança de Acesso:** Autenticação de bibliotecários com senhas criptografadas localmente (algoritmos de *Hash* e *Salt*).

### 3. Circulação e Empréstimos[cite: 1]
* **PDV Bibliotecário:** Interface otimizada (Checkout/Check-in) para empréstimos e devoluções em massa.
* **Regras de Negócio:** Aplicação automática de prazos, bloqueios por atraso e limites de renovação (prorrogações).
* **Histórico de Rastreabilidade:** Auditoria completa de quem retirou qual exemplar e quando.

### 4. Inteligência e Relatórios[cite: 1]
* **Geração de Documentos:** Exportação de declarações e recibos em PDF (*iTextSharp / PDFsharp*).
* **Análise de Dados:** Extração de relatórios operacionais (livros mais lidos, turmas mais ativas, inadimplência) nativamente para planilhas Excel (*ClosedXML*).

---

## 🏗 Arquitetura e Tecnologias

A aplicação foi projetada no padrão **Monolito Desktop (Rich Client)**, garantindo resiliência em escolas com conectividade intermitente à internet. Os dados são persistidos localmente no host.

| Camada | Stack Tecnológico | Propósito |
| :--- | :--- | :--- |
| **Plataforma** | C# 10 / .NET Framework 4.8 | Base sólida com amplo suporte no parque de máquinas do estado. |
| **Apresentação** | Windows Forms + MaterialSkin | UI responsiva baseada nos padrões do Material Design do Google. |
| **Acesso a Dados** | ADO.NET | Comunicação de baixo nível e alta performance com o banco. |
| **Banco de Dados** | SQL Server Compact 4.0 | SGDB relacional embarcado (não exige instalação de servidor local). |
| **Utilitários** | ZXing.Net | Processamento e decodificação de códigos de barras bidimensionais. |

---

## 🚀 Guia de Instalação e Execução

### Pré-requisitos
Certifique-se de que o ambiente de desenvolvimento possua:
* OS: Windows 10 (Build 19041) ou superior.
* IDE: [Visual Studio 2022](https://visualstudio.microsoft.com/) com a workload `Desenvolvimento para desktop com .NET`.
* Runtime: `Microsoft SQL Server Compact 4.0 SP1`.

### Passos para Build Local

1. **Clone o repositório**
   ```bash
   git clone [https://github.com/LouasDev/SistemaBiblioteca.git](https://github.com/LouasDev/SistemaBiblioteca.git)
   cd SistemaBiblioteca
