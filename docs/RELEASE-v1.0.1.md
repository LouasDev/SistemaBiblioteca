# SUB 1.0.1 — teste do atualizador

Versão de teste para validar a atualização a partir da 1.0.0, com mudança visível.

## O que mudou

- A tela Início mostra uma faixa: **SUB 1.0.1 — atualização de teste instalada**.
- O campo de notas em Configurações também mostra as notas quando a versão instalada já é a mais recente.
- A versão do executável passa a ser **1.0.1.0**.

## Como validar

1. Abra a instalação existente da versão 1.0.0.
2. Em Configurações, clique em **Verificar atualizações**. Devem aparecer a versão 1.0.1, suas notas e o botão **Atualizar agora**.
3. Clique em **Atualizar agora**, confirme a senha e acompanhe o download.
4. Confirme a aplicação. O SUB deve fechar e reabrir; faça login novamente.
5. Em Início, confira a faixa de confirmação. Em Configurações, confira a versão **1.0.1.0**.
6. Verifique novamente: o sistema deve informar que está atualizado e manter as notas preenchidas.

Para testar o fluxo automático, use o botão do aplicativo; baixar e extrair o ZIP manualmente testa apenas a instalação manual.

Esta release foi marcada como Latest para ser encontrada pela instalação padrão.
Mantém as limitações funcionais documentadas na 1.0.0; não altera o esquema do banco.
