using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Entities
{
    public class Jogada
    {
        public int Id { get; set; } // Chave primária
        public int JogoId { get; set; } // Chave estrangeira para Jogo

        public Jogo Jogo { get; set; } // Propriedade de navegação para o jogo. Essa propriedade é uma instancia da classe jogo dentro de jogada, sendo uma forma de acessar a classe com ela estando dentro da classe jogada. Quando você carrega uma Jogada do banco de dados, a propriedade de navegação Jogo será automaticamente populada com os dados do jogo correspondente, graças ao Entity Framework. Ou seja, essa instância será preenchida com o jogo (as jogadas serão alocadas aqui), sendo que a classe jogo ficará como o "container lógico" do jogo. 
        public string Jogador { get; set; } // "Jogador" ou "Computador". TRANSFORMAR EM ENUM DEPOIS.  
        public int Linha { get; set; } // Linha da jogada (0, 1, 2)
        public int Coluna { get; set; } // Coluna da jogada (0, 1, 2)
        public int Ordem { get; set; } // Ordem da jogada no jogo
    }
}