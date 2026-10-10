# Contrato funcional da instalação local

## 1. Escopo da instalação

Cada instituição escolar terá uma instalação independente do aplicativo e uma base de dados local própria. A aplicação deve funcionar sem internet para autenticação, cadastros, circulação, relatórios locais, exportações e configuração.

A internet não pode ser requisito para:

- iniciar o sistema;
- fazer login;
- cadastrar ou editar livros;
- cadastrar ou editar usuários;
- registrar empréstimos e devoluções;
- consultar atrasos e relatórios locais;
- exportar PDF, Excel e etiquetas.

A busca automática de livros será apenas um facilitador opcional. Se estiver indisponível ou exigir autenticação, o usuário continuará podendo cadastrar o livro manualmente.

## 2. Primeiro uso

Na primeira execução, antes do login, o aplicativo deve abrir um assistente obrigatório com estas etapas:

1. Nome da instituição.
2. Logo opcional da instituição.
3. Cor primária do aplicativo.
4. Código mestre para recuperação local de senhas.
5. Criação do primeiro usuário administrador.
6. Confirmação e finalização da configuração.

A configuração só será concluída quando os campos obrigatórios forem válidos. Depois da conclusão, o assistente não deve reaparecer, salvo por ação explícita de um administrador.

O nome e a cor primária devem substituir textos e cores fixos onde houver identidade institucional. O logo deve ser armazenado de modo local e usado em telas e relatórios quando disponível.

## 3. Recuperação local de senha

Não haverá recuperação por e-mail, API, SMS ou qualquer serviço externo.

O administrador poderá iniciar a redefinição de senha de um usuário. Como procedimento de contingência, a tela de login permitirá o uso do código mestre configurado no primeiro uso para autorizar a redefinição local.

Requisitos do código mestre:

- nunca exibir ou armazenar em texto puro;
- armazenar somente hash seguro;
- exigir confirmação antes da redefinição;
- limitar tentativas consecutivas;
- registrar localmente a operação e o administrador, quando houver sessão administrativa;
- permitir alteração do código mestre por um administrador autenticado;
- não permitir que o código mestre seja usado como senha normal de usuário.

A redefinição deve criar uma nova senha com hash BCrypt e invalidar qualquer senha anterior. O código mestre não deve ser enviado, exportado ou incluído em relatórios.

## 4. Perfis iniciais

Os perfis existentes serão mantidos como ponto de partida, com permissões centralizadas:

| Perfil | Responsabilidades |
|---|---|
| Administrador | Configuração da instituição, usuários, parâmetros, auditoria e todas as operações |
| Bibliotecário | Livros, exemplares, usuários conforme permissão, empréstimos, devoluções e relatórios |
| Professor | Operações de circulação permitidas pela instituição, sem administração do sistema |
| Funcionário | Operações de circulação permitidas pela instituição |
| Aluno | Consulta e operações permitidas pelo fluxo adotado; sem administração |

O perfil não deve ser usado como texto livre para autorizar operações. As telas e serviços devem consultar permissões definidas em um ponto central.

## 5. Regras padrão de circulação

Os valores atuais serão tratados como padrão inicial, não como regra universal imutável:

- Aluno: limite de 1 empréstimo simultâneo e prazo padrão de 7 dias.
- Professor: sem limite padrão e prazo padrão de 30 dias.
- Funcionário: sem limite padrão e prazo padrão de 30 dias.
- Administrador: sem limite padrão e prazo padrão de 30 dias.

O administrador poderá ajustar limites, prazos, prorrogação e bloqueio por atraso na configuração local. O sistema deve validar esses valores em uma camada de negócio, sem depender de mensagens ou condições específicas nos Forms.

## 6. Funcionalidades mantidas

- Login local.
- Cadastro, edição, ativação e desativação de usuários.
- Cadastro e edição de livros.
- Busca manual por título, autor, gênero e código de barras.
- Busca externa de metadados apenas como recurso opcional sem autenticação.
- Empréstimo padrão e rápido.
- Devolução padrão e rápida.
- Prorrogação conforme parâmetros locais.
- Mapeamento anual de turmas, se habilitado pela instituição.
- Relatórios de circulação e atrasos.
- Exportação local em PDF e Excel.
- Geração e impressão local de etiquetas/códigos de barras.

## 7. Funcionalidades removidas

- Backup automático.
- Sincronização com Google Drive.
- Restauração via nuvem.
- Token e credencial Google.
- Envio de e-mail e relatório semanal por e-mail.
- Notificação externa de disponibilidade ou atraso.
- Recuperação de senha por código enviado.
- Dependência de internet para operação principal.

## 8. Notificações internas

Quando uma ação anteriormente dependia de e-mail, o aplicativo poderá manter apenas:

- indicador visual de livro disponível;
- lista interna de pendências e atrasos;
- relatório local para consulta e impressão/exportação;
- confirmação visual da operação.

Nenhum registro deve ser marcado como “enviado” se não houver envio externo. Campos e tabelas que só existirem para controlar e-mail devem ser removidos ou convertidos em pendências internas.

## 9. Identidade visual

A configuração mínima da instituição será:

- nome da instituição;
- logo opcional;
- cor primária.

O aplicativo deve usar uma identidade neutra quando o logo não for informado. Referências a instituições, escolas, secretarias, destinatários e nomes de bibliotecas específicas não podem permanecer em regras, relatórios ou mensagens.

## 10. Critérios de aceite do contrato

- Uma instalação limpa inicia o assistente sem internet.
- O assistente cria a identidade institucional e o primeiro administrador.
- O login funciona sem e-mail ou serviço externo.
- O código mestre permite redefinir uma senha local sem transmitir dados.
- Cadastros, circulação e relatórios funcionam offline.
- Uma falha na busca externa de livros não impede o cadastro manual.
- Nenhuma rotina tenta enviar e-mail, acessar Google Drive ou criar tokens externos.
- O nome, logo e cor configurados aparecem nas áreas previstas do aplicativo.
