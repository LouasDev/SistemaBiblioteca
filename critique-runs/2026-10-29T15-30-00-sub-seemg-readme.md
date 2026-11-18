# Critique archive

- Target: published `README.md` in `renato0x/SUB-SEEMG` on `main`
- Mode: independent critic
- Timestamp: 2026-10-29T15:30:00-03:00

## Critique (Claude Opus)

**6 findings: 0 showstoppers, 2 gaps, 2 inconsistencies, 0 underspecified areas, 2 suggestions.**

[Gap] O painel ainda depende de um único serviço externo sem alternativa  
O card de métricas vem de `github-stats-extended.vercel.app`. A versão anterior do widget já falhou porque o deploy original foi pausado; se esse novo host tiver indisponibilidade, rate limit ou mudança de API, a parte central do registro volta a quebrar visualmente.  
Scenario: O serviço retorna 503 durante uma visita ao repositório e o README passa a exibir um ícone quebrado justamente no trecho que deveria comprovar a participação.

[Gap] A afirmação institucional mais forte não tem fonte no próprio texto  
O aviso declara que o SUB é “propriedade do Estado de Minas Gerais” e está sob responsabilidade da SEE/MG, mas a fonte oficial fica distante, no rodapé, e a matéria linkada parece demonstrar reconhecimento/divulgação, não necessariamente titularidade jurídica.  
Scenario: Um visitante interpreta o aviso como uma declaração jurídica formal e pergunta qual instrumento oficial comprova a transferência de propriedade; o README não oferece esse vínculo.

[Inconsistency] “Registro público” e cards externos têm pesos narrativos diferentes  
O primeiro card mede a contribuição individual no repositório `LouasDev/SistemaBiblioteca`; o segundo só apresenta metadados públicos do repositório. Visualmente eles têm o mesmo peso, mas apenas o primeiro sustenta a proposta de registrar a participação de Renato.  
Scenario: Um recrutador lê os dois cards como duas evidências equivalentes de autoria/contribuição, embora o card do projeto não demonstre nenhuma participação individual.

[Inconsistency] O README promete minimalismo, mas repete a mesma história em três lugares  
A trajetória aparece no parágrafo de abertura da seção, novamente na tabela 01/02/03 e ainda no texto final de contexto institucional. Não é excessivo em volume, mas gera a sensação de que a página precisa se justificar repetidamente.  
Scenario: Em uma leitura rápida, o visitante entende “origem → reconhecimento → continuidade” já no texto e encontra a mesma sequência na tabela logo abaixo, sem informação nova.

[Suggestion] O painel pode ser mais informativo sem recorrer a métricas globais  
O card de contribuições está corretamente filtrado pelo repositório e mostra commits, mas o segundo card não acrescenta uma dimensão da participação. Uma composição mais coerente seria manter o card individual como foco e usar o espaço complementar para uma métrica de atividade do repositório realmente relacionada ao período, ou retirar o segundo card.  
Scenario: O número de commits continua útil, enquanto a parte complementar mostra ritmo de evolução do sistema, em vez de apenas nome, descrição e linguagem do repositório.

[Suggestion] As telas do manual dependem da branch `main` de outro repositório  
As imagens estão carregadas por URLs `raw.githubusercontent.com/LouasDev/SistemaBiblioteca/main/...`. Isso torna o registro vulnerável a renomes, reorganizações de documentação ou alterações de imagem no repositório de desenvolvimento.  
Scenario: Uma reorganização em `docs/manual/img` transforma as duas telas em imagens quebradas no repositório histórico, mesmo sem editar este README.

What looks good

- A estrutura realmente caiu para dois títulos principais; a leitura ficou bem mais direta que as versões anteriores.
- O card de contribuição usa `repo=LouasDev/SistemaBiblioteca` e oculta estrelas, PRs, issues e contribuições globais; o dado central está, portanto, no escopo certo.
- A distinção entre repositório histórico e repositório de desenvolvimento está explícita.
- A linguagem em primeira pessoa está moderada: registra participação sem inventar responsabilidades técnicas individuais.
