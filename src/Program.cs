using System;
using System.Text;
using Jogodavelha;

namespace JogodaVelha
{
    class Program
    {
        public static char[] tabuleiro = new char[9] {'1', '2', '3', '4', '5', '6', '7', '8', '9'};
        public static char iconeJogador, iconeComputador;

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            Console.Clear();
            Console.WriteLine("JOGO DA VELHA");
            Console.WriteLine("");

            // Escolha de ícone e lógica para atribuir o ícone do computador. Usei if ternário para reduzir o código (se jogador for 'O', computador recebe 'X', senão 'O').
            iconeJogador = Tabuleiro.EscolhaIcone();
            iconeComputador = iconeJogador == 'O' ? 'X' : 'O';
            Console.Clear();

            // Chamando o método para decidir quem joga primeiro e armazenei a resposta para determinar a ordem das jogadas.
            bool jogadorPrimeiro = Jogadas.QuemJogaPrimeiro();
            // se false computador, se true jogador.
            // while verificavencedor false, looping de jogadas
 
            // A exclamação do começo nega o retorno false natural do método, ou seja, enquanto o valor for falso, o laço continua. 
            while (true)
            {
                if (jogadorPrimeiro)
                {
                    Tabuleiro.ExibirTabuleiro(tabuleiro);
                    Console.WriteLine("");
                    Jogadas.VezJogador(); // Jogada do jogador
                }
                else
                {
                    Tabuleiro.ExibirTabuleiro(tabuleiro);
                    Console.WriteLine("");
                    Jogadas.VezComputador(); // Jogada do computador
                }
                // Alterna a vez entre jogador e computador
                jogadorPrimeiro = !jogadorPrimeiro;
            }            
        }
    }
}




