using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jogodavelha
{
    // Como não criarei instâncias dessa classe e a usarei sempre da mesma forma, eu a fiz static.
    static class Tabuleiro
    {
        public static void ExibirTabuleiro (char[] tabuleiro)
        {
            Console.WriteLine ("");
            Console.WriteLine ("TABULEIRO:");
            Console.WriteLine ("");
            Console.WriteLine("{0} - {1} - {2}\n{3} - {4} - {5}\n{6} - {7} - {8}", tabuleiro[0], tabuleiro[1], tabuleiro[2], tabuleiro[3], tabuleiro[4], tabuleiro[5], tabuleiro[6], tabuleiro[7], tabuleiro[8]);
        }

        // Método que recebe o ícone escolhido pelo jogador, exibe um while até que o ícone seja válido. O return encerra o while, retornando o ícone escolhido pelo jogador.
        public static char EscolhaIcone ()
        {
            while (true)
            {
                Console.WriteLine("Você prefere jogar com o 'O' (1) ou com o 'X' (2)?");
                string escolha = Console.ReadLine();

                switch (escolha)
                {
                    case "1":
                        Console.WriteLine("Muito bem, você será o 'O' e o computador será o 'X'");
                        return 'O';

                    case "2":
                        Console.WriteLine("Muito bem, você será o 'X' e o computador será o 'O'");
                        return 'X';
                    
                    default:
                        Console.WriteLine("Entrada inválida. Você precisa escolher 'O' (1) ou 'X' (2).");
                        break; // Volta ao início do loop
                }
            }
        }
    }
}