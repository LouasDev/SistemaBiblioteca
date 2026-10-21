# Roteiro de teste manual — Etapa D (interface de atualização no ConfiguracaoForm)

Executar em **pasta de teste** (nunca na instalação real), com `SUB.exe`, `SUB-UPDATER.exe`
e dependências copiados para a pasta de teste. Servidor local = pasta servida por
`python -m http.server` ou IIS Express apontando para os arquivos de teste.

## Preparação
1. Gerar chave de teste e manifesto com versão nova (99.0.0.0) usando
   `Ferramentas\GerarChaveAssinatura.ps1` e `Ferramentas\AssinarManifesto.ps1`.
2. No `App.config` da pasta de teste, apontar `ManifestoAtualizacaoUrl` para o
   servidor local (`http://127.0.0.1:PORTA/version.json`).
3. Fazer login com usuário bibliotecária.

## Roteiro
1. **Designer**: abrir `ConfiguracaoForm` no Visual Studio — o form deve abrir sem
   erro, mostrando as seções "Instituição" e "Atualização" com todos os controles
   no layout. O layout é **plano** (sem TableLayoutPanel/FlowLayoutPanel): cada
   controle tem Location/Size próprios e âncoras, então dá para arrastar,
   redimensionar e estilizar individualmente. Mexer em tamanho/posição e desfazer
   (Ctrl+Z).
2. **Tema**: entrar em Configurações — labels, botões Salvar/Cancelar e os botões
   da seção de atualização devem usar a cor institucional salva.
3. **Nome/cor/salvar**: alterar nome e cor, Salvar → pede senha → salva → volta ao
   dashboard com o novo tema aplicado. Repetir sem alterar nada → igual.
4. **Versão atual**: confirmar que "Versão atual:" mostra o FileVersion do exe.
5. **Verificar — já atual**: manifesto com a versão igual → mensagem "você já está
   usando a versão mais recente"; "Atualizar agora" continua oculto.
6. **Verificar — versão nova**: manifesto 99.0.0.0 → status com versão+data,
   notas no campo multilinho (texto puro), "Atualizar agora" visível.
7. **Verificar — sem internet**: desligar a rede/servidor → mensagem de sem conexão.
8. **Verificar — HTTP 404**: apontar a URL para um caminho inexistente (sem
   `version.json`) → mensagem amigável "não possui uma versão publicada
   (HTTP 404)" em vez do erro genérico.
9. **Verificar — assinatura inválida**: servir `.sig` corrompido → mensagem de
   assinatura inválida; "Atualizar agora" oculto.
10. **Atualizar — senha errada**: "Atualizar agora" → PasswordForm → errar a senha →
    nada baixa, controles voltam ao normal.
11. **Atualizar — senha cancelada**: cancelar o PasswordForm → nada muda.
12. **Atualizar — confirmação**: com senha correta, "Não" na confirmação → nada
    baixa.
13. **Download cancelado**: confirmar "Sim" → durante o download clicar em
    "Cancelar download" → status "Download cancelado", sem `.part` em
    `%LOCALAPPDATA%\SUB\update`, controles reabilitados.
14. **Fechar durante download**: repetir o passo 13 fechando o form (Cancelar/Esc)
    em vez do botão → sem exceção, sem `.part` solto, sem travamento.
15. **Atualização completa**: "Atualizar agora" → senha → "Sim" → download com
    barra → backup em `%LOCALAPPDATA%\SUB\backups\` → confirmação final → o SUB
    fecha e o updater aplica → o SUB reabre sozinho na versão 99.0.0.0 (ou a
    versão do pacote); conferir `update\RESTAURACAO.txt` e log do updater.
16. **Reentrada/troca de tela**: durante uma verificação, clicar em Início no
    menu e voltar para Configurações → estado consistente, sem erro.
