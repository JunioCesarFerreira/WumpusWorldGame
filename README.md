# Wumpus World

🌍 *[Português](README.md) ∙ [English](README_en.md)*

Wumpus World é um jogo baseado no famoso problema lógico em inteligência artificial. O jogador navega por um mundo de grade, tentando evitar perigos como poços e o terrível Wumpus enquanto busca um tesouro de ouro.

## Descrição

No Wumpus World, o jogador move um personagem através de um grid 4x4, onde cada célula pode conter um perigo ou tesouro. O objetivo é encontrar o ouro e retornar ao ponto de partida sem cair em poços ou ser comido pelo Wumpus. O jogo é um ótimo exemplo de aplicação de raciocínio lógico e tomada de decisão sob incerteza.

### Características

- Interface gráfica simples com botões para controlar o movimento e ações do jogador.
- Dicas sensoriais como "Breeze" e "Stench" para indicar a proximidade de perigos.
- Capacidade de atirar uma flecha para matar o Wumpus.

![img](print.png)

## Requisitos

- .NET 8.0
- SO Windows.

## Instalação

1. Clone o repositório para o seu computador local usando `git clone`.
2. Abra a solução `WumpusWorld.sln` no Visual Studio.
3. Compile o projeto.
4. Execute o arquivo executável gerado a partir da pasta `bin/Debug` ou `bin/Release`.

## Uso

- Use os botões de direção para mover o jogador pelo grid.
- O botão "Go" executa o movimento na direção atualmente selecionada.
- Use o botão "Get" para pegar o ouro se estiver na mesma célula.
- Use o botão "Arrow" para atirar uma flecha na direção atualmente selecionada.
- Pressione as teclas correspondentes para uma interação mais rápida:
  - Setas para mover.
  - Enter para "Go".
  - Espaço para "Get".
  - A para "Arrow".

## Matriz de probabilidades

![img2](print2.png)


## Sobre as Distribuições de Probabilidades

#### Definição de Adjacência de um Conjunto
Seja $C$ um subconjunto de células do tabuleiro $B$. O conjunto de adjacência $A = \text{adj}(C)$ é dado por todas as células adjacentes às células de $C$ nas direções acima, abaixo, à direita e à esquerda.

$$
A = \text{adj}(C) = \bigcup_{(i,j)\in C}\big\lbrace (i+1,j), (i-1,j), (i,j+1), (i,j-1)\big\rbrace
$$

#### Exemplo
Seja $C = \lbrace c_{1,1} \rbrace$, então $\text{adj}(C) = \lbrace c_{1,2}, c_{2,1} \rbrace$.

#### Definição de Subconjuntos
Definimos:
- $V$ o conjunto das células visitadas pelo jogador.
- $S$ o conjunto das células seguras por dedução.
- $S^c := B \setminus S$.
- $M$ o conjunto das células que indicam que há algum perigo na adjacência.
- $H := \big\lbrace H_{i,j} \in 2^B \mid H_{i,j} = \text{adj}(m_{i,j}) \cap S^c, \forall m_{i,j} \in M \big\rbrace$.

Note que $M \subset V \subset S$.

#### Probabilidades do Wumpus
$$
P(C_{i,j} = w \mid M, S) = 
\begin{cases} 
0, & \text{se } C_{i,j} \in S, \\
(||B|| - ||S||)^{-1}, & \text{se } H = \emptyset \text{ e } C_{i,j} \notin S, \\
||\bigcap H_{i,j}||^{-1}, & \text{se } H \neq \emptyset \text{ e } C_{i,j} \notin S.
\end{cases}
$$

A notação $||A||$ indica a cardinalidade do conjunto $A$.

E modelo é implementado na classe [WumpusProbabilityDistribution](WumpusProbabilityDistribution.cs). No entanto, esta foi substituida pela gernaralização a seguir.


### Generalização da Distribuição de Probabilidades

#### Definição
Denotamos por $\mathcal{C^S_n}$ o conjunto de todas as combinações possíveis de $n$ células de $S^c$ que podem conter $n$ perigos (sejam poços ou o Wumpus). Isto é,

$$
\mathcal{C}^S_n:=\big\lbrace C\subset S^c \mid ||C||=n \big\rbrace.
$$

Seja $M$ o conjunto das células já vizitadas com indicação de perigo. Definimos o conjunto das configurações válidas por:

$$
V(\mathcal{C}^S_n):=\big\lbrace \mathbf{C}\in\mathcal{C}^S_n \mid \text{adj}(\mathbf{C})^c\cap M = \emptyset \big\rbrace.
$$

Assim, cada configuração $\mathbf{C}\in V(\mathcal{C}^S_n)$ representa uma possível distribuição dos perigos indicados.

#### Probabilidade de Perigo

$$
P(C_{i,j}=p|M,S)=\begin{cases}
0, &\text{ se }C_{i,j}\in S,\\
\frac{||\lbrace\mathbf{C}\in V(\mathcal{C}^S_n) \mid C_{i,j}\in\mathbf{C}\rbrace||}{||V(\mathcal{C}^S_n)||}, &\text{ se }C_{i,j}\notin S.
\end{cases}
$$

Esta é distribuição que implementamos em [HazardProbabilityDistribution](HazardProbabilityDistribution.cs).