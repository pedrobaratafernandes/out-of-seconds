# Out of Seconds

Jogo de plataforma 2D inspirado no filme *In Time (2011)*.

Nome do jogo *Out of Seconds*, o tempo e a vida são a mesma moeda. Cada segundo conta, o jogador tem de decidir quando avançar e quando esperar para não ficar sem tempo.

## Conceito

- Género: plataformas 2D com foco em movimento e gestão de tempo.
- Tema: sobrevivência num mundo onde o tempo é um recurso escasso.
- Inspiração: introduz o ambiente e a lógica do mundo apresentado no início do filme.

## Links Úteis

* [Trello](https://trello.com/b/nQKcDxmZ/intimethevideogame)
* [GDD - Versão 1](GDD//GDD%20IN%20TIME%202011%20v1.docx)


## Objetivo do Jogo

- Atravessar os níveis antes de o relógio chegar a zero.
- Recolher até 10 placas pelo cenário e terminar num tempo definido para realizar o fim bom.
- Evitar inimigos (5 segundos) ou amigos (1 segundo) que penalizam tempo.

## Game Loop

1. Comprar café para perceber que o tempo é moeda de troca
2. Explorar, saltar e superar desafios de plataformas.
3. Recolher as 10 placas e terminar num determinado tempo para ver fim bom.
4. Alcançar a saída do nível antes de o cronómetro chegar a zero.

## Mecânicas Principais

- Movimento lateral e saltar.
- Sistema de contagem decrescente como recurso principal.
- HUB para mostrar o tempo durante a partida e o tempo limite para terminar o nível.

## Controlos

- Mover: teclas A/D ou setas direcionais (teclado), joystick esquerdo ou d-pad (gamepad).
- Saltar: Barra de Espaço (teclado) ou botão A (gamepad).
- Mostrar tempo: tecla Q ou ALT (teclado) ou botão Y (gamepad).
- Ação: tecla E ou CTRL (teclado) ou botão X ou B (gamepad)
- Voltar ao menu principal: ESC ou Start (gamepad).

## Ficha Técnica do Projeto

- Motor: Unity 6000.3.8f1.
- Linguagem: C#.
- Pipeline gráfico: URP.
- Input: Unity Input System.

## Estrutura de Pastas

- Assets/Game: versão final do jogo.
- Assets/Prototype: versão protótipo do jogo.
- GDD: onde estão as diferentes versões de GDD.

## Como Abrir e Executar

1. Abrir o Unity Hub.
2. Adicionar a pasta raiz deste repositório.
3. Selecionar a versão Unity 6000.3.8f1.
4. Abrir o projeto e executar a scene Main Menu no Editor.

## Créditos

* César Carreira a22502766 - Ideia original
* Gabriel Almeida a22403021 - Artista
* Pedro Fernandes a22400473 - Programador
* Powered by Universidade Lusófona