# Inventário de generalização do Sistema de Biblioteca

> **Inventário histórico, anterior às adaptações atuais.** As tabelas abaixo
> registram o levantamento original, não uma lista de remoções ainda a executar.
> Caminhos como `BibliotecaApp`, `SUB/` e referências a serviços removidos pertencem
> à estrutura daquela etapa e podem não existir neste repositório.
>
> Consulte o [contrato funcional atual](CONTRATO-FUNCIONAL-INSTALACAO-LOCAL.md),
> o [guia de desenvolvimento](DESENVOLVIMENTO.md) e o [backlog vigente](backlog.markdown).

## Situação após as adaptações

- Operação principal local, com SQL Server Compact e identidade institucional configurável.
- Primeiro uso, TXT de recuperação, login de bibliotecários e reset com dupla verificação.
- Busca externa opcional pela Open Library e atualizações assinadas pelo próprio repositório.
- Circulação e prorrogação com serviços transacionais e regras compartilhadas.
- Backups de atualização preservados como recurso local; não há sincronização em nuvem.
- Migrações internas do esquema são necessárias para preservar instalações existentes;
  a antiga orientação de iniciar uma base nova não se aplica a cada atualização.

---

## Levantamento original preservado

## Diretrizes confirmadas

- Aplicativo desktop local por instituição escolar.
- Uma base local independente por instituição.
- Primeira instalação com base nova, sem migração automática.
- Remover backup externo, Google Drive, e-mail, SMTP e demais integrações que exijam autenticação ou dependência externa.
- Manter somente fluxos autossuficientes no aplicativo.
- Manter a busca de livros por Google Books/OpenLibrary somente se ela não exigir login ou autenticação externa; deve ser opcional e não impedir o cadastro manual/offline.
- Manter os links externos atualmente exibidos na tela Sobre.

## Integrações e funcionalidades externas

| Área | Evidência | Ação planejada | Estado |
|---|---|---|---|
| Backup/Google Drive | `Utils/AppPaths.cs`, `credentials.json`, referências no `.csproj` | Remover tokens, credenciais, pastas e dependências de backup/sincronização | Remover |
| E-mail/API | `Utils/EmailService.cs`, `Utils/EmailRequest.cs`, `App.config`, chamadas nos Forms | Remover serviço, DTO, configurações, chamadas de envio e fluxos dependentes | Remover |
| Recuperação de senha | `Forms/Login/EsqueceuSenhaForm.cs` | Substituir por recuperação local administrada; não enviar códigos | Redesenhar |
| Busca de livros | `Forms/Livros/CadastroLivroForm.cs` | Manter apenas sem autenticação, com fallback manual e funcionamento offline | Avaliar implementação |
| Links da tela Sobre | `Forms/Login/AboutForm.cs` | Manter conforme orientação do usuário | Manter |

## Acoplamentos institucionais encontrados

| Local | Acoplamento | Ação planejada |
|---|---|---|
| `Forms/Login/LoginForm.cs` | Biblioteca Monteiro Lobato, secretaria, destinatário fixo, relatórios e e-mails | Remover fluxos de e-mail e tornar textos institucionais neutros/configuráveis |
| `Forms/Login/EsqueceuSenhaForm.cs` | Biblioteca Monteiro Lobato e e-mail de recuperação | Remover tela/fluxo de código por e-mail |
| `Forms/Inicio/InicioForm.cs` | Textos institucionais específicos | Usar SUB e dados da configuração local |
| `Forms/Livros/EmprestimoForm.cs` | Biblioteca Monteiro Lobato e secretaria em notificações | Remover e-mail e textos fixos |
| `Resources/*` e `Properties/Resources.resx` | Identidade visual institucional | Usar somente `sub-full.png` e `sub-200x200px.png` |
| `SUB.csproj` | Recursos específicos da distribuição | Manter a identidade SUB |
| `README.md` | Nome, regras, backup, Google Drive, e-mail e contatos específicos | Atualizar após a implementação |

## Configuração e dados

| Área | Situação atual | Necessidade |
|---|---|---|
| Banco | SQL Server Compact local em `%LOCALAPPDATA%\\SUB\\SUB\\sub.sdf` | Criar somente após a primeira configuração |
| Conexão | `Utils/Conexao.cs` monta caminho e senha a partir de `App.config` | Manter operação local e revisar proteção/gestão de segredo |
| Usuários | Modelo provisório, campos escolares e perfis misturados | Centralizar perfis, status, validação e autorização |
| Livros | Livro agregado com quantidade/disponibilidade | Avaliar necessidade de exemplares individuais e histórico |
| Empréstimos | Modelo simples, regras espalhadas nos Forms | Centralizar status, prazos, atrasos, limites e prorrogações |
| Turmas | `TurmasUtil` e regras fixas de turma | Tornar turmas e progressão configuráveis por instituição |
| Auditoria | Não identificada como camada formal | Adicionar registros locais para operações administrativas e circulação |

## Arquivos prioritários para a próxima etapa

1. `SUB/App.config`
2. `SUB/SUB.csproj`
3. `BibliotecaApp/packages.config`
4. `BibliotecaApp/Utils/AppPaths.cs`
5. `BibliotecaApp/Utils/EmailService.cs`
6. `BibliotecaApp/Utils/EmailRequest.cs`
7. `BibliotecaApp/Forms/Login/LoginForm.cs`
8. `BibliotecaApp/Forms/Login/EsqueceuSenhaForm.cs`
9. `BibliotecaApp/Forms/Inicio/InicioForm.cs`
10. `BibliotecaApp/Forms/Livros/CadastroLivroForm.cs`
11. `BibliotecaApp/Forms/Livros/EmprestimoForm.cs`
12. `BibliotecaApp/Models/*.cs`
13. `BibliotecaApp/Utils/Conexao.cs`

## Critérios para remoção

- Não remover referências chamadas “backup” que representem apenas cópia temporária de controles de UI, como `_acceptBackup`/`_cancelBackup`.
- Não remover links da tela Sobre.
- Não permitir que a busca externa de livros seja obrigatória para cadastrar um livro.
- Não deixar configurações, credenciais ou assemblies sem uso no projeto após a limpeza.
- Não alterar o banco atual nesta etapa; a primeira versão generalizada deverá inicializar uma base nova.
