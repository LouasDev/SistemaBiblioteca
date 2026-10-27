# Contrato funcional da instalação local

[Documentação](README.md) · [README do projeto](../README.md)

Este documento descreve o comportamento implementado, incluindo as correções de
prorrogação, circulação e regras compartilhadas. Propostas ainda não implementadas
ficam no [backlog](backlog.markdown). A existência de código ou de testes específicos
não significa que a simulação completa pela interface já tenha sido concluída.

## 1. Instalação e funcionamento local

O SUB é uma aplicação Windows Forms em C# para .NET Framework 4.8. Usa SQL Server
Compact 4.0 por ADO.NET, sem exigir servidor de banco de dados separado.

Autenticação, cadastros, circulação, relatórios e exportações locais não dependem
de internet. A busca opcional de metadados por ISBN utiliza a Open Library;
atualizações e abertura de links externos utilizam conexão. O cadastro manual de
livros permanece disponível se a busca externa falhar.

O banco fica em `%LOCALAPPDATA%\SUB\SUB\sub.sdf`, junto de uma área de configuração,
relatórios internos, mapeamentos, atualizações e backups em `%LOCALAPPDATA%\SUB`.
Essa área pertence à conta do Windows. Copiar os executáveis para outra pasta não
cria uma instituição ou base independente para a mesma conta.

## 2. Primeiro uso e identidade institucional

Sem a configuração institucional, o aplicativo abre o assistente antes do login.
Ele reúne nome da instituição, logotipo opcional, tema, primeira conta de
bibliotecário, turmas iniciais e geração/exportação do código mestre de recuperação.
A finalização depende das validações do assistente e do TXT de recuperação salvo.

A identidade configurada é utilizada nas áreas previstas da interface e nos
documentos que a incorporam. Os temas acompanham a cor principal escolhida.
Configurações não possui rolagem geral; as notas de atualização têm rolagem própria.
Os formulários de Configurações e do termo de reset são editáveis no Designer.

## 3. Acesso e recuperação de senha

O painel é acessado por contas do perfil `Bibliotecário(a)`, com e-mail e senha
verificados localmente. O e-mail identifica a conta; o login não exige envio de
mensagem, confirmação online nem acesso à caixa de e-mail.

Alunos, professores e demais participantes são cadastros da circulação. Não há um
portal de aluno, acesso remoto da secretaria ou matriz de permissões administrativa
por múltiplos perfis de login implementados no fluxo atual.

A recuperação no login solicita o e-mail da bibliotecária, o código mestre salvo
no primeiro uso e uma nova senha. Não envia códigos por e-mail, API ou SMS.
Senhas e código mestre são armazenados como hash e salt usando PBKDF2, conforme
`CriptografiaSenha.cs`, e não BCrypt.

O código mestre não é gerado novamente a cada abertura. O TXT contém o código em
texto legível e deve ficar em local seguro. A configuração institucional armazena
seu hash e salt. Não há uma função geral de rotação desse código nas configurações.
O reset concluído inicia uma nova configuração, com outro código.

## 4. Circulação normal

- O formulário sugere sete dias para devolução. A opção de data personalizada
  permite ajustar o prazo; para `Professor(a)`, ela fica habilitada obrigatoriamente.
- A devolução prevista não pode ser anterior à data de empréstimo informada,
  incluindo horário. A tela e o serviço usam a mesma validação.
- A data de registro salva continua sendo o momento da operação; a data exibida
  para validação não foi convertida em um recurso de lançamento retroativo.
- O limite existente para `Aluno(a)` considera um empréstimo com status `Ativo`.
  A contagem não inclui automaticamente todos os registros `Atrasado`.
- A retirada exige estoque suficiente no banco no momento da gravação.
- A devolução só conclui empréstimos em aberto e não pode repor estoque duas vezes.

Não há prazo automático de 30 dias por perfil nem editor de regras de circulação
nas configurações. Os valores atuais são definidos no código compartilhado.

## 5. Prorrogação

A prorrogação acrescenta de 1 a 14 dias ao vencimento atual, após seleção e
confirmação. Não é calculada automaticamente a partir da data de hoje.
Empréstimos devolvidos ou encerrados não podem ser prorrogados. Se o prazo mudou
desde a consulta, o operador precisa atualizar a lista e confirmar novamente.

O novo prazo e o histórico são gravados na mesma transação. O histórico registra
a data da ação, o novo vencimento e a bibliotecária. O esquema versão 5 preserva
os formatos antigos de `LogProrrogacoes`, incluindo `DataHora` e `DataDaAcao`.
Informações antigas desconhecidas não são inventadas durante a migração.

## 6. Circulação rápida de livros didáticos

O fluxo rápido é separado do empréstimo normal e atende professores retirando
vários exemplares de livros do gênero `Didático`, com turma e bibliotecária.

A devolução pode ser total ou parcial. `Quantidade` passa a representar o saldo
pendente: somente os exemplares recebidos voltam ao estoque. Zero recebidos pode
ser confirmado como falta de toda a quantidade, sem reposição de estoque.

Duas pendências `Faltando` impedem novas retiradas rápidas. A regularização de um
registro que já estava faltando continua permitida. Um empréstimo totalmente
devolvido não pode ser contabilizado novamente.

O painel considera a semana de segunda a domingo. Devolvidos integralmente antes
da segunda-feira atual ficam ocultos; ativos, faltantes e registros incompletos
permanecem visíveis para conferência. Não há exclusão semanal nem reinício de IDs.
O histórico permanece disponível aos relatórios. Registros apagados pelas versões
anteriores não são recuperados por essa alteração.

## 7. Consistência das operações

Retirada, devolução e movimentação de estoque são gravadas juntas em transações.
Falhas intermediárias desfazem as alterações daquela operação. As condições de
quantidade e situação são conferidas no banco, sem depender apenas da informação
carregada na tela. Confirmações e eventos de sucesso ocorrem após a gravação.

`RegrasCirculacao` compartilha políticas entre telas e serviços;
`CirculacaoService` e `ProrrogacaoService` executam as operações protegidas.
Isso não equivale a uma refatoração completa de todos os módulos do aplicativo.

## 8. Reset da instalação

O reset exige aceite do termo, importação do TXT correspondente à instalação,
senha da bibliotecária autenticada e confirmação final. A limpeza ocorre após
encerrar a instância anterior. Remove o conteúdo de `%LOCALAPPDATA%\SUB`, inclusive
backups locais, e retorna ao primeiro uso. Exportações em outras pastas permanecem.

O fluxo de reset bloqueia novas tentativas por cinco minutos após cinco falhas
consecutivas na execução atual. Essa regra não deve ser atribuída automaticamente
a todos os fluxos de recuperação de senha.

Detalhes, proteção do pedido e retomada de falhas: [reset local](RESET-LOCAL.md).

## 9. Atualizações, relatórios e limites do escopo

As versões públicas usam três componentes, como `1.0.2`. O aplicativo consulta
releases do próprio repositório, verifica a assinatura do manifesto e o SHA-256
do pacote. Commit e tag isolados não distribuem uma atualização.

Há relatórios locais, exportações Excel, documentos e etiquetas PDF e mapeamento
de turmas. Não há sincronização com Google Drive, backup automático em nuvem,
envio de notificações por e-mail ou acesso remoto da secretaria. Os backups
feitos pelo fluxo de atualização não constituem uma rotina geral de backup.

Suporte e formalização da licença permanecem no backlog.

## 10. Verificação

As verificações específicas de circulação e prorrogação passaram com bancos
fictícios, incluindo migração, falhas intermediárias e disputa de estoque. Os
scripts e os procedimentos estão no [guia de desenvolvimento](DESENVOLVIMENTO.md).
A simulação completa como usuário fica para depois das demais correções e deve
ser realizada em outra conta do Windows ou máquina virtual, com dados fictícios.
