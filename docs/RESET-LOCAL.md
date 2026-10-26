# Reset local do SUB

O botão **Resetar aplicativo**, em Configurações, abre o popup `ResetTermoForm`.
Ele apresenta o termo de confirmação e a quantidade atual de usuários, títulos,
empréstimos, empréstimos rápidos e turmas que serão apagados. Também informa
a exclusão de configurações, relatórios internos, mapeamentos e backups.

O usuário precisa aceitar o termo e usar **Selecionar arquivo TXT** para
importar o arquivo de recuperação salvo no primeiro uso. O código é extraído
do arquivo e comparado ao hash da instalação, sem ser exibido na tela.
Arquivos vazios, inválidos, acima de 16 KiB, com múltiplos códigos ou de outra
instalação não liberam a próxima etapa. O arquivo é conferido novamente ao
continuar. A etapa seguinte reutiliza o `PasswordForm` para a senha da
bibliotecária autenticada. A autorização consulta o usuário pelo ID
da sessão, confirma o perfil de bibliotecário e rejeita uma conta inativa.
Cinco falhas consecutivas bloqueiam novas tentativas por cinco minutos na
execução atual, incluindo falhas na importação. Cancelar qualquer etapa não
agenda nem executa a limpeza.

## Popup e edição visual

Abra `Forms/Configuracoes/ResetTermoForm.cs` no Designer do Visual Studio para
editar posições, tamanhos, fontes e textos. Todos os controles e o seletor de
arquivo estão em `ResetTermoForm.Designer.cs`, com o `.resx` associado.
O code-behind não cria controles nem define o layout. Ele preenche os números,
valida o arquivo e gerencia o aceite.

Cabeçalho, botões e destaques acompanham a cor principal selecionada em
Configurações. O aviso de exclusão definitiva mantém a cor semântica de alerta.
Alterar o tema não substitui os textos nem as posições editados no Designer.

Após as duas verificações, uma confirmação final autoriza o apagamento
definitivo. O aplicativo grava um pedido protegido pelo Windows para a conta
atual, inicia uma nova instância e encerra a anterior. A nova instância aguarda
o término da anterior e obtém o bloqueio de instância única antes de apagar.
Assim, a limpeza acontece depois da liberação dos Forms e das conexões.

## Escopo

A limpeza remove o conteúdo de `%LOCALAPPDATA%\SUB`, incluindo banco, instituição,
turmas, relatórios internos, mapeamentos, downloads e backups locais. Não remove
executáveis nem arquivos exportados para outras pastas. O TXT antigo pode
continuar existindo fora da pasta de dados, mas não será aceito pela nova
instalação: o primeiro uso gera outro código mestre.

O código mestre não é regenerado a cada abertura. Nenhuma senha ou código
mestre é incluído no pedido de reset, nos argumentos do processo ou nos logs.

## Falhas e retomada

Um pedido ainda não iniciado expira após dez minutos. Pedidos adulterados não
autorizam limpeza. Atalhos e redirecionamentos de diretórios são rejeitados.
Todos os arquivos são verificados quanto a bloqueio e permissão antes da
primeira exclusão.

Se ocorrer uma falha depois de iniciar a exclusão, a operação não pode ser
desfeita. O pedido protegido permanece e o próximo início retoma a limpeza
autorizada antes de abrir login ou primeiro uso. O pedido só é removido após
a exclusão de todo o conteúdo. O assistente inicial abre somente após sucesso.

## Testes

`Ferramentas/Testes/TesteReset.cs` testa credenciais, bloqueios de arquivos,
pedidos inválidos ou expirados, retomada, proteção contra redirecionamentos,
importação do TXT, aceite, temas e carregamento no Designer, além de
preservação de arquivos externos. Usa apenas pastas temporárias fornecidas
explicitamente ao auxiliar; não executa o reset da instalação real.

Na raiz do projeto, execute `./Ferramentas/Testes/test-reset.ps1` para compilar
e rodar os testes. As prévias do layout ficam na pasta de testes em `artifacts`.

Configurações não tem rolagem. O botão de reset fica no rodapé e a seção de
atualização se ajusta à altura disponível; as notas têm rolagem própria.
O menu de Suporte permanece para uma etapa futura.
