Labirinto
===============

Labirinto(jogo) desenvolvido para um trabalho da faculdade de Engenharia da Computação da UNISOCIESC, para a cadeira de Análise de Algorítmos 2. Utiliza teoria dos grafos e algoritmo de busca em largura(BFS).

	Este código estava anteriormente em um repositório Mercurial. Por este motivo, para consultar o histórico de commits, consultar o antigo repositório. Link: https://bitbucket.org/claudiosteuernagel/labirinto

Requisitos do trabalho
===============

-Fazer um ratinho sair de um labirinto.

-Dado um array de 2 dimensões contendo os seguintes caracteres:


	'.' = espaço vazio
	'#' = parede
	'S' = posição inicial (6,1)
	'E' = saída (0,30) 
    
-Por exemplo:
	
	  0         1         2         3
	  0123456789012345678901234567890123456
	0 #################################E###
	1 ###....#.....#......###...#...#...#.#
	2 #...##...###...####.....#..##.#.##..#
	3 #.#########.###....##.####.##.#.#..##
	4 #.............##.######..#......#...#
	5 #####.#####.#..........#.##########.#
	6 #S..........###.#.#.##..............#
	7 #####################################

-Observação: O array apresentado é apenas um exemplo. O arquivo contendo o array deve ser lido de um arquivo txt.

-A aplicação deve responder:
   1. É possível chegar a uma saída? (Sim ou Não)
   2. Quantos passos são necessários para chegar a saída seguindo o caminho mais curto?
   3. Imprimir o caminho que foi seguido para chegar a saída
   4. Apresentar o labirinto e demais objetos de cena graficamente 

-O problema deve ser tradado como um grafo onde os elementos 'S', 'E' e '.' devem ser tratados como vértices. Cada vertice adjacente é conectado por uma aresta. O problema deve ser resolvido usado busca em largura (breadth-first search ou BFS)
