using System;
using System.Text;
using Jogodavelha;
using System.Resources; // Acesso aos Resources
using System.Globalization; // Acesso ao "Culture info"

// Instalei duas extensões. Uma extensão para usar Recursos com o comando: dotnet add package System.Resources.Extensions ; a segunda para o usar o Globalization (Culture info) com o comando: dotnet add package System.Globalization

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

            // Instancia um ResourceManager. Ele acessa os recursos dos arquivos resx que eu criei.
            ResourceManager rm = new ResourceManager("JOGODAVELHA.Resources.Strings", typeof(Program).Assembly);

            Console.Clear();
            Console.WriteLine("JOGO DA VELHA / MORPION");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Você prefere jogar em português (1) ou em francês (2)?");
            Console.WriteLine("Préferéz-vous jouer en portugais (1) ou en français (2)?");
            
            int idiomaJogo = int.Parse(Console.ReadLine());
            bool escolhaLingua = true;

            while (escolhaLingua)
            {
                switch (idiomaJogo)
                {
                    case 1:
                    Console.Clear();
                    // Define o idioma como padrão (português)
                    CultureInfo.CurrentUICulture = new CultureInfo("pt-BR");
                    Console.WriteLine(rm.GetString("ValidaIdioma"));
                    escolhaLingua = false;
                    break;

                    case 2:
                    Console.Clear();
                    // Determina o idioma como francês (e o arquivo a ser lido o strings.fr.resx)
                    CultureInfo.CurrentUICulture = new CultureInfo("fr-FR");
                    Console.WriteLine(rm.GetString("ValidaIdioma"));
                    // Determina o idioma como francês (e o arquivo a ser lido o strings.fr.resx)
                    escolhaLingua = false;
                    break;

                    default:
                    Console.Clear();
                    Console.WriteLine("Entrada inválida. Você precisa escolher entre português (1) ou francês (2).");
                    Console.WriteLine("Entrée invalide. Vous devez choisir entre le portugais (1) et le français (2).");
                    break; 
                }
            }

            Console.WriteLine("");            
            Console.WriteLine(rm.GetString("Titulo"));
            // Escolha de ícone e lógica para atribuir o ícone do computador. Usei if ternário para reduzir o código (se jogador for 'O', computador recebe 'X', senão 'O').
            // Aqui foi preciso passar como parâmetro o Resource Manager
            iconeJogador = Tabuleiro.EscolhaIcone(rm);
            iconeComputador = iconeJogador == 'O' ? 'X' : 'O';
            Console.Clear();

            // Chamando o método para decidir quem joga primeiro e armazenei a resposta para determinar a ordem das jogadas.
            bool jogadorPrimeiro = Jogadas.QuemJogaPrimeiro(rm);
            // se false computador, se true jogador.
            // while verificavencedor false, looping de jogadas
 
            // A exclamação do começo nega o retorno false natural do método, ou seja, enquanto o valor for falso, o laço continua. 
            while (true)
            {
                if (jogadorPrimeiro)
                {
                    Tabuleiro.ExibirTabuleiro(tabuleiro);
                    Console.WriteLine("");
                    Jogadas.VezJogador(rm); // Jogada do jogador
                }
                else
                {
                    Tabuleiro.ExibirTabuleiro(tabuleiro);
                    Console.WriteLine("");
                    Jogadas.VezComputador(rm); // Jogada do computador
                }
                // Alterna a vez entre jogador e computador
                jogadorPrimeiro = !jogadorPrimeiro;
            }            
        }
    }
}