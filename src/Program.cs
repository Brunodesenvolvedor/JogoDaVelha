// Criar uma branch para treinar e subir como está. Falta colocar opção de escolha em francês. Depois, alterar as variáveis para combinarem com os textos dos resources. 


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

            // Determina o idioma como francês (e o arquivo a ser lido o strings.fr.resx)
            CultureInfo.CurrentUICulture = new CultureInfo("fr-FR");

            // Instancia um ResourceManager. Ele acessa os recursos dos arquivos resx que eu criei.
            ResourceManager rm = new ResourceManager("JOGODAVELHA.Resources.Strings", typeof(Program).Assembly);

            Console.Clear();
            Console.WriteLine(rm.GetString("Titulo"));
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