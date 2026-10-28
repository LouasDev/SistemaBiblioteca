# Checklist antes de dar release (1.0.0)

[Voltar à documentação](README.md)

Nenhuma release deve ser criada antes que os itens abaixo estejam marcados.
Até lá, o trabalho segue só em commits na `main`.

## Funcionalidade

- [ ] Fluxo completo testado: Primeiro Uso → login → cadastro de bibliotecário → empréstimo → devolução → relatórios
- [ ] Todos os formulários abrem sem erro em 1280×845 e 1366×768
- [ ] Tema claro e escuro revisados em todas as telas, inclusive as mais recentes (PrimeiroUsoForm, GerenciarTurmasForm, mapeamento de turmas)
- [ ] Sistema de atualização (SUB-UPDATER) testado de ponta a ponta — baixa, aplica, faz rollback se falhar

## Dados e segurança

- [ ] Banco `sub.sdf` não traz dados de teste em instalação limpa (sem instituições, usuários ou turmas fictícias)
- [ ] Backup/restauração de banco testado
- [ ] Reset do app (botão "Resetar aplicativo") testado sem deixar lixo em disco

## Qualidade

- [ ] Build Release (não só Debug) compila sem warnings relevantes
- [ ] Sem `TODO`/código morto relevante nos arquivos tocados recentemente
- [ ] Manual de uso (`docs/manual/index.html`) revisado e batendo com a UI atual

## Equipe

- [ ] Todos os PRs/commits pendentes de cada membro mesclados na `main`
- [ ] Ninguém com trabalho local não sincronizado com o remoto

## Distribuição

- [ ] Ícone, nome e versão do instalador definidos
- [ ] Contato de suporte (`subgvsuporte@gmail.com`) conferido nas telas de login, Sobre e Configurações
- [ ] Decisão em equipe: quem define o número da versão 1.0.0 e quando
