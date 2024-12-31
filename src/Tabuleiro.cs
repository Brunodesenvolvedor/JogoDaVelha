using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Resources; // Acesso aos Resources

namespace Jogodavelha
{
    // Como não criarei instâncias dessa classe e a usarei sempre da mesma forma, eu a fiz static.
    static class Tabuleiro
    {
        public static void ExibirTabuleiro (char[] tabuleiro, ResourceManager rm)
        {
            Console.WriteLine ("");
            Console.WriteLine(rm.GetString("Tabuleiro"));
            Console.WriteLine ("");
            Console.WriteLine("{0} - {1} - {2}\n{3} - {4} - {5}\n{6} - {7} - {8}", tabuleiro[0], tabuleiro[1], tabuleiro[2], tabuleiro[3], tabuleiro[4], tabuleiro[5], tabuleiro[6], tabuleiro[7], tabuleiro[8]);
        }

        // Método que recebe o ícone escolhido pelo jogador, exibe um while até que o ícone seja válido. O return encerra o while, retornando o ícone escolhido pelo jogador.
        // Passei como parâmetro o Resource Manager.
        public static char EscolhaIcone (ResourceManager rm)
        {
            while (true)
            {
                Console.WriteLine(rm.GetString("EscolhaIcone"));
                string escolha = Console.ReadLine();

                switch (escolha)
                {
                    case "1":
                        Console.WriteLine(rm.GetString("EscolhaIconeO"));
                        Console.WriteLine("");
                        return 'O';

                    case "2":
                        Console.WriteLine(rm.GetString("EscolhaIconeX"));
                        Console.WriteLine("");
                        return 'X';
                    
                    default:
                        Console.WriteLine("");
                        Console.WriteLine(rm.GetString("EscolhaInvalida"));
                        break; // Volta ao início do loop
                }
            }
        }
    }
}

