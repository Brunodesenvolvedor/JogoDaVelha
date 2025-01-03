using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Entities
{
    public class Jogo
    {
        public int Id { get; set; } // Chave primária
        public DateTime Data { get; set; } // Data do jogo
        public string Vencedor { get; set; } // Nome do vencedor (Jogador ou Computador)
        public ICollection<Jogada> Jogadas { get; set; } // Relação com as jogadas

    }
}


  