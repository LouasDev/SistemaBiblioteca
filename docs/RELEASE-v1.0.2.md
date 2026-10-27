# SUB 1.0.2 — correções de segurança

Versão de correção a partir da 1.0.1.

## O que mudou

- **Login:** contas de bibliotecário desativadas não conseguem mais entrar. Cadastros antigos sem o campo `Ativo` preenchido continuam tratados como ativos.
- **Atualizações:** o download do pacote aceita somente HTTPS em hosts do GitHub. A exceção para `http://127.0.0.1`/`localhost` passa a valer apenas nos testes automatizados.
- A versão do executável passa a ser **1.0.2**.

## Como validar

1. Com uma conta de bibliotecário ativa, entre normalmente.
2. Desative essa conta (ou outra de bibliotecário) em Usuários e tente entrar com ela: deve aparecer "E-mail ou senha não conferem".
3. Em Configurações, confira a versão **1.0.2**.
4. Rode `.\Ferramentas\Testes\test-etapaD.ps1`: as verificações `loopback-bloqueado-por-padrao` e `loopback-permitido-em-teste` devem passar.

Não altera o esquema do banco.
