using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JogodaVelha;

namespace Jogodavelha
{
    class Jogadas
    {  
        // Usei if ternário (se for par true, senão false)
        public static bool QuemJogaPrimeiro()
        {
            Random random = new Random();
            int JogaPrimeiro = random.Next(1, 11);

            Console.WriteLine($"Neste jogo, é escolhido aleatoriamente quem inicia. Desta vez, {(JogaPrimeiro % 2 == 0 ? "você" : "o computador")} começa.");

            // Essa expressão já retorna um booleano, então não preciso de um if 
            return JogaPrimeiro % 2 == 0;
        }
  
        public static void ValidarJogada(int jogadaAValidar, char icone)
        {
            while (true) 
            {
                // Verifica se a jogada está fora do intervalo permitido ou se a posição já está ocupada.
                if (jogadaAValidar < 1 || jogadaAValidar > 9 || Program.tabuleiro[jogadaAValidar - 1] == 'O' || Program.tabuleiro[jogadaAValidar - 1] == 'X')
                {
                    Console.WriteLine("Essa não é uma jogada válida. Escolha um número válido (1 a 9) que esteja livre:");

                    // Solicita uma nova entrada e valida novamente.
                    if (int.TryParse(Console.ReadLine(), out int novaJogada))
                    {
                        jogadaAValidar = novaJogada; // Atualiza para a nova entrada.
                    }
                    else
                    {
                        Console.WriteLine("Entrada inválida. Insira um número entre 1 e 9.");
                    }
                }
                else
                {
                    // Se a jogada for válida, atualiza o tabuleiro e sai do loop.
                    Program.tabuleiro[jogadaAValidar - 1] = icone;
                    break;
                }
            }
        }

        public static bool VerificaVencedor(char icone)
        {
            // Verifica combinações vencedoras
            if (
                (Program.tabuleiro[0] == icone && Program.tabuleiro[1] == icone && Program.tabuleiro[2] == icone) || // Linha superior
                (Program.tabuleiro[3] == icone && Program.tabuleiro[4] == icone && Program.tabuleiro[5] == icone) || // Linha do meio
                (Program.tabuleiro[6] == icone && Program.tabuleiro[7] == icone && Program.tabuleiro[8] == icone) || // Linha inferior
                (Program.tabuleiro[0] == icone && Program.tabuleiro[3] == icone && Program.tabuleiro[6] == icone) || // Coluna esquerda
                (Program.tabuleiro[1] == icone && Program.tabuleiro[4] == icone && Program.tabuleiro[7] == icone) || // Coluna do meio
                (Program.tabuleiro[2] == icone && Program.tabuleiro[5] == icone && Program.tabuleiro[8] == icone) || // Coluna direita
                (Program.tabuleiro[0] == icone && Program.tabuleiro[4] == icone && Program.tabuleiro[8] == icone) || // Diagonal principal
                (Program.tabuleiro[2] == icone && Program.tabuleiro[4] == icone && Program.tabuleiro[6] == icone)    // Diagonal secundária
                )
            {
                if (icone == Program.iconeJogador)
                {
                    Tabuleiro.ExibirTabuleiro(Program.tabuleiro);
                    Console.WriteLine("O jogo encerrou. Parabéns, você venceu\nPressione enter para encerrar");
                }
                else
                {
                    Tabuleiro.ExibirTabuleiro(Program.tabuleiro);
                    Console.WriteLine("O jogo encerrou. O computador venceu\nPressione enter para encerrar");
                }
                Console.ReadLine();
                Environment.Exit(0);
            }

            // Verifica empate (tabuleiro cheio e nenhuma vitória)
            if (Program.tabuleiro.All(c => c == 'O' || c == 'X'))
            {
                Tabuleiro.ExibirTabuleiro(Program.tabuleiro);
                Console.WriteLine("O jogo empatou\nPressione enter para encerrar");
                Console.ReadLine();
                return true;
            }
            // Nenhum vencedor ou empate
            return false;
        }

        public static void VezComputador()
        {
            Random random = new Random();

            // Gera um int aleatório entre 1-9, confere se não há um ícone na posição. Se não houver, encerra o looping e joga esse int e o ícone do PC no método ValidarJogada. 
            while (true)
            {
                int jogadadoComputador = random.Next(1, 10); 

                if (Program.tabuleiro[jogadadoComputador - 1] != Program.iconeJogador && Program.tabuleiro[jogadadoComputador - 1] != Program.iconeComputador)
                {
                Jogadas.ValidarJogada(jogadadoComputador, Program.iconeComputador);

                Console.Clear();
                Console.WriteLine("Na vez do computador, ele jogou na posição {0}", jogadadoComputador);

                Jogadas.VerificaVencedor(Program.iconeComputador);
                break;
                }
            }
        }

        // Método que acolhe e registra a vez do jogador, depois valida a jogada e registra (por outro método), por fim, verifica se resultou em fim do jogo (outro método) 
        public static void VezJogador()
        {
            Console.WriteLine("Sua vez: escolha o número da posição em que deseja jogar.");

            int jogada = int.Parse(Console.ReadLine());

            ValidarJogada(jogada,Program.iconeJogador);

            Console.Clear();
            Console.WriteLine("Você jogou na posição {0}", jogada);
            
            Jogadas.VerificaVencedor(Program.iconeJogador);
        }
    }
}

