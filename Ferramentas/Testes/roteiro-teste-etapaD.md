# Roteiro manual — interface de atualização

[Documentação](../../docs/README.md) · [Publicação de pacotes](../../docs/PUBLICACAO-ATUALIZACOES.md)

Este roteiro descreve verificações a executar. Não registra que a simulação
completa pela interface já foi concluída.

## Preparação e isolamento

1. Utilize outra conta do Windows ou uma máquina virtual com dados fictícios.
   Apenas copiar os executáveis para outra pasta não isola `%LOCALAPPDATA%\SUB`.
2. Copie o pacote completo, com `SUB.exe`, `SUB.exe.config`, `SUB-UPDATER.exe` e
   dependências. Configure uma instituição e uma bibliotecária de teste.
3. Use um manifesto cuja versão corresponda ao executável real do pacote de
   destino e seja superior à instalação de origem. Não basta alterar a tag ou o JSON.
4. Para o fluxo oficial, siga o guia de publicação. Para um servidor local,
   utilize uma cópia separada do código e um par de chaves exclusivo de teste:
   a chave pública deve estar nessa compilação e a privada deve assinar os
   manifestos servidos. Uma chave aleatória não é aceita pelo executável oficial.
5. Guarde a chave privada de teste em caminho próprio e informe explicitamente
   `-ChavePrivada` ao script `Ferramentas\AssinarManifesto.ps1`. Não sobrescreva a
   chave oficial nem use `GerarChaveAssinatura.ps1 -Forcar` para preparar testes.
6. O servidor local pode ser `python -m http.server PORTA`, executado na pasta dos
   arquivos fictícios. Configure `ManifestoAtualizacaoUrl` em `SUB.exe.config`
   dessa cópia para `http://127.0.0.1:PORTA/version.json`. Sirva também a assinatura
   e o ZIP indicado no manifesto. HTTP de loopback é admitido para testes;
   URLs externas seguem as restrições do atualizador.

## Interface e operações básicas

1. Abra `ConfiguracaoForm` no Designer do Visual Studio. Confira os controles,
   a edição visual de posições e textos e o botão de reset no rodapé.
2. Confira a aplicação do tema institucional e a ausência de rolagem geral.
   As notas da atualização podem ter rolagem própria.
3. Altere nome e cor, salve, confirme a senha e confira a identidade na tela principal.
4. Confira a versão no formato de três números, como `1.0.2`.
5. Consulte um manifesto de versão igual: o sistema deve informar que está
   atualizado e manter as notas disponíveis, sem oferecer instalação.
6. Consulte uma versão superior: confira versão, data, notas e o botão de atualização.

## Falhas e cancelamentos

1. Sem conexão: conferir mensagem de falha de consulta, sem instalar um pacote.
2. Manifesto inexistente: conferir a informação de ausência de versão publicada
   quando a resposta é HTTP 404.
3. Assinatura inválida: alterar a assinatura fictícia; conferir bloqueio da atualização.
4. Senha incorreta ou cancelada: conferir que nenhum download é iniciado.
5. Confirmação recusada: conferir que a atualização não prossegue.
6. Cancelamento durante download: conferir ausência de arquivo `.part` abandonado
   na área de dados da conta de teste e retorno dos controles ao estado utilizável.
7. Fechamento da tela durante download: conferir cancelamento sem travamento.
8. Troca de tela durante consulta: retornar a Configurações e conferir estado consistente.
9. Pacote com hash ou tamanho incorreto: conferir recusa antes da aplicação.

## Atualização completa

1. Consulte a versão superior, confirme senha e instalação e acompanhe o download.
2. Confira a criação das cópias locais de atualização em `%LOCALAPPDATA%\SUB\backups`.
3. Confirme que o SUB fecha, o atualizador aplica o pacote e o aplicativo reabre.
4. Entre com a conta de teste, confira a versão efetiva e verifique novamente.
5. Confirme a preservação dos cadastros e a aplicação das migrações necessárias.
6. Em um cenário de falha preparado, consulte o log e o `RESTAURACAO.txt` na pasta
   `update\rollback-...`. O rollback pode ser removido após o primeiro início
   bem-sucedido; não espere encontrar o arquivo após toda atualização concluída.

Baixar e extrair o ZIP manualmente não valida a troca automática e o reinício.
Registre o resultado real de cada cenário, a versão testada e qualquer falha.
