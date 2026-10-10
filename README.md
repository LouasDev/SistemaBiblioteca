# SUB — Sistema Único Bibliotecário

<div align="center">
  
  **Sistema de Gestão Bibliotecária para Escolas Públicas**
  
  *Modernização e organização do acervo bibliotecário escolar*
</div>

---

## 📋 Índice

- [Sobre o Sistema](#sobre-o-sistema)
- [Funcionalidades](#funcionalidades)
- [Requisitos do Sistema](#requisitos-do-sistema)
- [Instalação](#instalação)
- [Estrutura do Projeto](#estrutura-do-projeto)
- [Telas do Sistema](#telas-do-sistema)
- [Regras de Negócio](#regras-de-negócio)
- [Tecnologias Utilizadas](#tecnologias-utilizadas)
- [Segurança](#segurança)
- [Suporte](#suporte)

---

## 📖 Sobre o Sistema

O **Sistema Biblioteca** é uma aplicação desktop desenvolvida em C# (.NET Framework 4.8) para gestão completa de bibliotecas de escolas públicas. O sistema foi projetado para modernizar o controle bibliotecário, substituindo processos manuais por uma solução digital eficiente e segura.

### Objetivos

- **Digitalizar** o controle de empréstimo e devolução de livros
- **Organizar** o acervo bibliotecário com catalogação eficiente
- **Facilitar** o gerenciamento de usuários (alunos, professores, funcionários)
- **Rastrear** atrasos e aplicar regras de punição automaticamente
- **Gerar relatórios** gerenciais para tomada de decisão
- **Acessibilidade** para secretarias via funcionalidade de acesso remoto

---

## ⚙️ Funcionalidades

### 🔐 Autenticação e Segurança
- Login seguro com validação de credenciais
- Criptografia de senhas com BCrypt
- Sistema open source, livre para uso

### 👥 Gerenciamento de Usuários
| Tipo de Usuário | Permissões |
|-----------------|------------|
| **Administrador** | Controle total do sistema, cadastro de usuários |
| **Bibliotecário** | Gestão de empréstimos, devoluções, livros e relatórios |
| **Professor** | Empréstimos sem limite, acesso a relatórios básicos |
| **Aluno** | Empréstimo de 1 livro por vez |
| **Funcionário** | Empréstimos sem limite |

- Cadastro completo com validação de campos
- Edição e exclusão de usuários
- Mapeamento de turmas

### 📚 Gerenciamento de Livros
- Cadastro de novos livros com código de barras
- Campos: Nome, Autor, Gênero, Quantidade, Código de Barras
- Status de disponibilidade em tempo real
- Pesquisa por gênero literário
- Alteração de dados cadastrais
- Geração de etiquetas com código de barras (PDF)

### 📖 Empréstimos e Devoluções
| Funcionalidade | Descrição |
|----------------|-----------|
| **Empréstimo Padrão** | Registro completo com bibliotecário responsável |
| **Empréstimo Rápido** | Fluxo simplificado para professores |
| **Devolução Padrão** | Pesquisa por aluno ou livro |
| **Devolução Rápida** | Processo ágil para múltiplas devoluções |
| **Prorrogação** | Extensão do prazo em até 7 dias |

### 📊 Relatórios Gerenciais
- Empréstimos em aberto
- Livros com atraso (expirados)
- Livros mais emprestados
- Aluno com maior número de empréstimos
- Exportação para Excel (.xlsx)
- Geração de relatórios em PDF

### 🔄备份 e Sincronização
- Backup automático diário
- Sincronização com Google Drive
- Restauração de dados

---

## 💻 Requisitos do Sistema

### Hardware Mínimo
| Componente | Requisito |
|------------|-----------|
| Processador | 1 GHz ou superior |
| Memória RAM | 2 GB |
| Espaço em Disco | 500 MB |
| Resolução | 1024x768 ou superior |

### Software Necessário
- **Sistema Operacional**: Windows 10/11
- **Framework**: .NET Framework 4.8
- **Banco de Dados**: SQL Server Compact Edition 4.0
- **Microsoft Visual C++ 2010 Redistributable**

---

## 📦 Instalação

### Passos para Instalação

1. **Clone o repositório**
   ```bash
   git clone https://github.com/LouasDev/SistemaBiblioteca.git
   ```

2. **Abra a solução no Visual Studio**
   ```
   SistemaBiblioteca.sln
   ```

3. **Restaure os pacotes NuGet**
   - Ferramentas > Gerenciador de Pacotes NuGet > Restaurar Pacotes

4. **Compile o projeto**
   ```
   Build > Compilar Solução (Ctrl+Shift+B)
   ```

5. **Execute o aplicativo**
   ```
   Debug > Iniciar Sem Depuração (F5)
   ```

### Configuração do Banco de Dados

O banco SQL Server Compact do **Sistema Único Bibliotecário** é criado somente após a primeira configuração e fica em:
```
%LOCALAPPDATA%\SUB\SUB\sub.sdf
```

### Arquivos de Configuração

| Arquivo | Localização | Descrição |
|---------|-------------|------------|
| `credentials.json` | Pasta do executável | Credenciais Google Drive (backup) |
| `App.config` | Projeto | Configurações de conexão |

---

## 📁 Estrutura do Projeto

```
SistemaBiblioteca/
├── SUB/
│   ├── Data/                      # Dados e conexões (se houver)
│   ├── Elements/                  # Componentes customizados
│   │   ├── AnimatedToggle.cs      # Toggle animado
│   │   ├── GradientPanel.cs      # Painel com gradiente
│   │   ├── RoundedComboBox.cs     # ComboBox estilizado
│   │   ├── RoundedDatePicker.cs   # DatePicker estilizado
│   │   ├── RoundedMaskedTextBox.cs # TextBox com máscara
│   │   └── RoundedTextBox.cs      # TextBox arredondado
│   ├── Forms/
│   │   ├── Inicio/               # Telas iniciais
│   │   │   ├── MainForm.cs       # Formulário principal MDI
│   │   │   └── InicioForm.cs     # Tela inicial/dashboard
│   │   ├── Login/                # Autenticação
│   │   │   ├── LoginForm.cs      # Tela de login
│   │   │   ├── AboutForm.cs      # Sobre o sistema
│   │   │   └── EsqueceuSenhaForm.cs
│   │   ├── Livros/               # Gestão de livros
│   │   │   ├── LivrosForm.cs     # Lista de livros
│   │   │   ├── CadastroLivroForm.cs
│   │   │   ├── AlterarCadLivroForm.cs
│   │   │   ├── EmprestimoForm.cs
│   │   │   ├── EmprestimoRapidoForm.cs
│   │   │   ├── DevoluçãoForm.cs
│   │   │   ├── DevoluçãoRapidaForm.cs
│   │   │   ├── FichaAlunoForm.cs
│   │   │   └── ProrrogarDiasForm.cs
│   │   ├── Usuario/              # Gestão de usuários
│   │   │   ├── UsuarioForm.cs
│   │   │   ├── CadUsuario.cs
│   │   │   └── EditarUsuarioForm.cs
│   │   ├── Relatorio/           # Relatórios
│   │   │   └── RelForm.cs
│   │   └── Mapeamento/          # Mapeamento de turmas
│   │       └── MapeamentoDeTurmasWizardForm.cs
│   ├── Models/                   # Modelos de dados
│   │   ├── Usuarios.cs
│   │   ├── Livro.cs
│   │   ├── Emprestimo.cs
│   │   ├── Sessao.cs
│   │   └── MapeamentoModel.cs
│   ├── Properties/              # Recursos e configurações
│   ├── Resources/                # Imagens e ícones
│   └── Utils/                    # Utilitários
│       ├── AppPaths.cs           # Caminhos do sistema
│       └── BackupManager.cs      # Gerenciamento de backup
├── SUB.sln             # Solução Visual Studio
└── README.md                     # Este arquivo
```

---

## 🖥️ Telas do Sistema

### Tela de Login
<div align="center">
  <img src="docs/screenshots/login.png" alt="Tela de Login" width="400"/>
</div>

- Campo para e-mail do usuário
- Campo para senha (criptografada)
- Opção "Lembrar-me"
- Link para recuperação de senha

### Tela Principal (Dashboard)
<div align="center">
  <img src="docs/screenshots/dashboard.png" alt="Tela Principal" width="600"/>
</div>

- Menu lateral com ícones intuitivos
- Barra de título com informações do usuário
- Área central com dashboard/informações
- Indicadores visuais de livros emprestados/atrasados

### Menu de Navegação

```
📚 LIVROS
   ├── Cadastrar Livro
   ├── Listar/Editar Livros
   ├── Empréstimo
   ├── Empréstimo Rápido
   ├── Devolução
   └── Devolução Rápida

👥 USUÁRIOS
   ├── Cadastrar Usuário
   └── Listar/Editar Usuários

📊 RELATÓRIOS
   ├── Relatório Geral
   └── Mapeamento de Turmas

⚙️ SISTEMA
   ├── Sobre
   └── Sair
```

---

## 📜 Regras de Negócio

### Regras de Empréstimo

| Tipo de Usuário | Limite de Livros | Prazo Padrão | Prorrogável |
|-----------------|------------------|--------------|-------------|
| Aluno | 1 por vez | 7 dias | +7 dias |
| Professor | Ilimitado | 30 dias | +30 dias |
| Funcionário | Ilimitado | 30 dias | +30 dias |
| Administrador | Ilimitado | 30 dias | +30 dias |

### Regras de Atraso

| Tempo de Atraso | Ação |
|-----------------|------|
| **1-30 dias** | Status = "Atrasado" |
| **30-60 dias** | Notificação ao aluno |
| **Mais de 60 dias** | Bloqueio para novos empréstimos + carta de notificação |

### Penalidades
- Aluno com atraso não pode realizar novos empréstimos
- Após 2 meses de atraso: carta de notificação
- Bloqueio de documentos administrativos até regularização

---

## 🛠️ Tecnologias Utilizadas

### Frameworks e Bibliotecas

| Tecnologia | Versão | Finalidade |
|------------|--------|------------|
| **.NET Framework** | 4.8 | Framework principal |
| **Windows Forms** | - | Interface desktop |
| **MaterialSkin** | 0.2.1 | Design moderno (Material Design) |
| **BCrypt** | 1.8.9 | Criptografia de senhas |
| **Newtonsoft.Json** | 13.0.3 | Manipulação de JSON |
| **ClosedXML** | 0.105.0 | Exportação Excel |
| **PDFsharp** | 6.2.1 | Geração de PDFs |
| **iTextSharp** | 5.5.13 | Manipulação avançada de PDFs |
| **ZXing.Net** | 0.16.10 | Geração de códigos de barras |
| **Google APIs** | 1.70.0 | Integração Google Drive |

### Banco de Dados

| Tecnologia | Tipo | Finalidade |
|------------|------|------------|
| **SQL Server Compact** | Local | Armazenamento principal |
| **SQLite** | Local | Backup e cache |

### Outras Tecnologias
- **Google Drive API** - Backup em nuvem
- **BCrypt** - Hash de senhas
- **Barcode Scanning** - Código de barras

---

## 🔒 Segurança

### Medidas Implementadas

1. **Criptografia de Senhas**
   - Uso de BCrypt para hash de senhas
   - Salt único por usuário
   - Nunca armazenar senhas em texto puro

2. **Controle de Acesso**
   - Permissões baseadas em tipo de usuário
   - Sessões autenticadas
   - Logout automático

3. **Proteção de Dados**
   - Backup automático
   - Sincronização com nuvem
   - Integridade do banco de dados

### Boas Práticas de Segurança

- Não compartilha credenciais de backup
- Realize backups regulares
- Mantenha o sistema atualizado

---

## 📞 Suporte

### Canais de Suporte

- **Sistema**: SUB — Sistema Único Bibliotecário
- **GitHub Issues**: [Abrir Issue](https://github.com/LouasDev/SistemaBiblioteca/issues)
- **Documentação**: [Wiki do Projeto](https://github.com/LouasDev/SistemaBiblioteca/wiki)

### Documentação Adicional

- [Manual do Usuário](docs/manual-usuario.md)
- [Guia de Instalação](docs/guia-instalacao.md)
- [Especificação Técnica](docs/especificacao-tecnica.md)
- [Product Backlog](Product%20Backlog%20-%20Library%20Management%20System%20(1).markdown)

---

## 👥 Equipe de Desenvolvimento

| Membro | Função | Responsabilidade |
|--------|--------|------------------|
| **Ithalo Pimentel** | Gerente de Projeto | Coordenação e documentação |
| **Matheus Felipe** | Backend/Database | Banco de dados e Entity Framework |
| **Luis Gustavo** | Frontend/Backend | Login, livros, regras de negócio |
| **Renato Monteiro** | Frontend | Cadastro de usuários, empréstimos |
| **Bryan Emanuel** | Frontend/Testes | Empréstimo rápido, qualidade |

---

## 📄 Licença

Este projeto é distribuído sob a licença **MIT**. Sinta-se livre para usar, modificar e distribuir.

---

## 🙏 Agradecimentos

O SUB foi desenvolvido para apoiar a organização e a operação de bibliotecas escolares.

---

<div align="center">
  <p><strong>Sistema Biblioteca v1.0</strong></p>
  <p>Desenvolvido com 💙 para escolas públicas</p>
  <p>© 2024 - 2026 LouasDev</p>
</div>

