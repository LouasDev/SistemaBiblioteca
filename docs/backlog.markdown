# Backlog do SUB

[Documentação](README.md) · [README do projeto](../README.md)

Este backlog separa comportamento implementado, validação pendente e propostas.
Não representa um calendário de entrega. As estimativas e atribuições do
planejamento original foram preservadas no [backlog histórico de 2025](historico/BACKLOG-2025.md).

## Base implementada

| Área | Situação |
| --- | --- |
| Plataforma | Windows Forms, .NET Framework 4.8, SQL Server Compact e ADO.NET. |
| Primeiro uso | Identidade institucional, primeira bibliotecária, turmas e TXT de recuperação. |
| Acesso | Login local de bibliotecários e recuperação com código mestre; hashes PBKDF2. |
| Operação | Acervo, usuários, turmas, circulação normal e rápida, relatórios e exportações. |
| Atualização | Manifesto assinado, verificação do pacote e versões no padrão `0.0.0`. |
| Reset | Termo, TXT da instalação, senha da bibliotecária e confirmação final. |

A implementação desses recursos não significa que todos os seus cenários de
interface já tenham sido validados. O [contrato funcional](CONTRATO-FUNCIONAL-INSTALACAO-LOCAL.md)
detalha o escopo e as regras atuais.

## Sequência de correções acordada

| Item | Situação | Resultado ou próximo passo |
| --- | --- | --- |
| 1. Prorrogações | Implementado, com testes específicos | Migração do histórico, gravação conjunta e bloqueio de empréstimos encerrados. |
| 2. Circulação | Implementado, com testes específicos | Estoque e empréstimos na mesma transação; saldo parcial protegido; painel semanal sem exclusão. |
| 3. Regras compartilhadas | Implementado, com testes específicos | Prazos, quantidades, limites e situações compartilhados entre telas e serviços. |
| 4. Documentação | Revisada nesta etapa | Contrato atualizado, referências históricas identificadas e instruções de teste corrigidas. |
| 5. Suporte | Pendente de definição dos canais | Submenu com contato, sugestões e relatos de problema; preservar Configurações sem rolagem. |
| 6. Licença | Pendente de formalização | Conferir direitos e dependências e alinhar `LICENSE`, README, painel Sobre e distribuição. |
| 7. Simulação completa de uso | Pendente, após os demais itens | Percorrer os fluxos pela interface em ambiente isolado, registrar evidências e corrigir falhas. |

Os scripts existentes são apresentados no [guia de desenvolvimento](DESENVOLVIMENTO.md).
Teste específico aprovado não substitui a simulação completa pela interface.

## Suporte: decisões pendentes

- Definir contato e canais que serão exibidos, sem inventar endereços.
- Confirmar o destino das sugestões e dos relatos de bugs.
- Apresentar o plano e o fluxo antes de implementar o submenu.
- Usar formulário editável no Designer e integrado ao tema.
- Não enviar mensagens, anexos ou dados automaticamente.

## Licenciamento: decisões pendentes

A aplicação menciona MIT, mas ainda não existe `LICENSE` formalizado no
repositório. É preciso confirmar os titulares e a licença e revisar as condições
das dependências antes de apresentar a distribuição como regularizada.

## Propostas que não são funcionalidades entregues

As propostas abaixo não estão autorizadas como parte automática das correções:

- Portal ou acesso remoto da secretaria e login por múltiplos perfis.
- Editor de limites e prazos por instituição nas configurações.
- Controle de exemplares individuais em substituição ao estoque agregado.
- Auditoria administrativa unificada para todos os módulos.
- Novas punições por atraso, bloqueios administrativos ou regras universais de 30 dias.
- Rotação do código mestre por um comando de configuração.

MySQL, Entity Framework e BCrypt constavam do planejamento antigo e não descrevem
a implementação atual. Sincronização em nuvem, envio de e-mail e recuperação por
código enviado não integram o escopo local vigente.

## Critérios para encerrar esta rodada

- Correções previstas implementadas e verificações específicas aprovadas.
- Documentação coerente com o código e links locais válidos.
- Suporte e licença tratados conforme as decisões dos responsáveis.
- Simulação final concluída em ambiente isolado, com registro das falhas e resultados.
- Publicação de pacote somente quando a versão for considerada pronta; um commit
  não equivale a uma release para usuários.
