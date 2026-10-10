# Sistema Único Bibliotecário   (SUB)

Repositório de desenvolvimento do **Sistema de Biblioteca (SUB)**, uma aplicação desktop para apoiar a gestão de bibliotecas escolares. O projeto centraliza o cadastro de acervo e usuários, empréstimos, devoluções e relatórios.

> Este repositório é destinado ao desenvolvimento do sistema. Regras operacionais e configurações de cada instalação devem ser validadas com a equipe responsável antes de serem alteradas.

## Visão geral

O SUB busca reduzir processos manuais na rotina da biblioteca e oferecer controle do acervo escolar.

Principais áreas do sistema:

- autenticação e controle de acesso;
- cadastro e manutenção de usuários;
- cadastro, consulta e atualização de livros;
- empréstimos, devoluções e prorrogações;
- relatórios e exportações;
- mapeamento de turmas e recursos de apoio à operação.

## Tecnologias

| Tecnologia | Uso no projeto |
| --- | --- |
| C# / .NET Framework 4.8 | Aplicação principal |
| Windows Forms | Interface desktop |
| SQL Server Compact | Persistência local |
| NuGet | Gerenciamento de dependências |
| MaterialSkin | Componentes visuais |

Outras bibliotecas presentes na solução apoiam exportação para Excel e PDF, geração de códigos de barras, criptografia e integrações.

## Estrutura do projeto

```text
SistemaBiblioteca/
├── BibliotecaApp/
│   ├── Elements/       # Componentes visuais reutilizáveis
│   ├── Forms/          # Telas da aplicação
│   ├── Models/         # Modelos de domínio
│   ├── Properties/     # Recursos e configurações do projeto
│   ├── Resources/      # Imagens e ícones
│   ├── Utils/          # Serviços e utilitários
│   ├── App.config      # Configurações da aplicação
│   └── BibliotecaApp.sln
├── Product Backlog - Library Management System (1).markdown
└── README.md
```

## Como preparar o ambiente

### Pré-requisitos

- Windows 10 ou superior;
- Visual Studio com suporte ao desenvolvimento .NET Desktop;
- .NET Framework 4.8 Developer Pack;
- acesso às dependências NuGet do projeto.

### Execução local

1. Clone o repositório:

   ```bash
   git clone https://github.com/LouasDev/SistemaBiblioteca.git
   ```

2. Abra a solução `BibliotecaApp/BibliotecaApp.sln` no Visual Studio.
3. Restaure os pacotes NuGet, se necessário.
4. Revise as configurações de banco em `BibliotecaApp/App.config` de acordo com o ambiente local.
5. Compile a solução e execute o projeto `BibliotecaApp`.

### Configurações locais

O arquivo `App.config` contém a configuração de conexão usada pela aplicação. Dados de acesso, chaves e demais informações sensíveis devem ser tratados como configuração local e nunca compartilhados em issues, commits ou documentação pública.

## Desenvolvimento

- Mantenha as alterações focadas em uma funcionalidade ou correção.
- Valide a compilação localmente antes de abrir um pull request.
- Descreva no pull request o comportamento alterado e como ele foi testado.
- Atualize este README apenas quando uma mudança afetar a preparação do ambiente, a arquitetura ou o fluxo de contribuição.

O backlog do produto está disponível em [Product Backlog - Library Management System (1).markdown](Product%20Backlog%20-%20Library%20Management%20System%20(1).markdown).

## Equipe de desenvolvimento

| Membro | Função | Responsabilidade |
| --- | --- | --- |
| **Ithalo Pimentel** | Gerente de Projeto | Coordenação e documentação |
| **Matheus Felipe** | Backend / Banco de Dados | Banco de dados e Entity Framework |
| **Luis Gustavo** | Front-end / Back-end | Login, livros e regras de negócio |
| **Renato Monteiro** | Front-end / Back-end | Cadastro de usuários e empréstimos |
| **Bryan Emanuel** | Front-end / Testes | Empréstimo rápido e qualidade |

## Suporte e acompanhamento

- [Issues do projeto](https://github.com/LouasDev/SistemaBiblioteca/issues)
- [Wiki do projeto](https://github.com/LouasDev/SistemaBiblioteca/wiki)

---

<div align="center">
  <p><strong>Sistema Único Bibliotecário   (SUB)</strong></p>
  <p>Desenvolvido para apoiar bibliotecas escolares.</p>
</div>
