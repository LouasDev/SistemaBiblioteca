# SUB 1.0.0

Primeira release pública do Sistema Único Bibliotecário, aplicativo para Windows
com operação local por instituição escolar.

## Incluído nesta versão

- Assistente inicial com instituição, tema, bibliotecária, turmas e recuperação local de acesso.
- Cadastro e consulta de usuários e livros.
- Empréstimos, devoluções e circulação rápida de livros didáticos.
- Dashboard, relatórios e exportações em Excel e PDF.
- Etiquetas e códigos de barras.
- Painel de atualização corrigido, com resposta visível à consulta de versões.
- Atualizações assinadas distribuídas nas releases deste mesmo repositório.
- Dependências do SQL Server Compact para Windows de 32 e 64 bits no pacote.

## Como instalar

1. Baixe **SUB-1.0.0.zip** nos arquivos desta release.
2. Extraia todo o conteúdo para uma pasta com permissão de gravação.
3. Execute **SUB.exe** e conclua o assistente inicial.

É necessário Windows com **.NET Framework 4.8**. Não execute o aplicativo
diretamente dentro do ZIP. Guarde o código de recuperação solicitado no primeiro uso.

Os dados ficam em `%LOCALAPPDATA%\SUB`. A instalação em outra pasta não cria
uma base independente para o mesmo usuário do Windows.

## Atualizações

`version.json` e `version.json.sig` são arquivos usados pelo atualizador.
Para instalar manualmente, baixe o ZIP completo.

A versão exibida no aplicativo é **1.0.0.0**, equivalente à release **1.0.0**.
Uma instalação nessa mesma versão deve informar que já usa a versão mais recente.
A detecção de atualização exigirá uma futura versão superior.

## Limitações conhecidas

- A prorrogação de empréstimos em bases novas tem uma incompatibilidade entre
  o esquema de `LogProrrogacoes` e a gravação do histórico; a correção está pendente.
- O fluxo completo de substituição e reinício por uma release real ainda precisa
  ser validado com uma futura versão superior. Esta publicação é a versão inicial.

O código-fonte está disponível neste mesmo repositório.
