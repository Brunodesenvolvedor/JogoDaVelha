using System;
using System.Text;
using Jogodavelha;
using System.Resources; // Acesso aos Resources
using System.Globalization; // Acesso ao "Culture info"
using System.Data.SqlClient; // Para usar comandos SQL

namespace JogodaVelha
{
    public class Program
    {
        public static char[] tabuleiro = new char[9] {'1', '2', '3', '4', '5', '6', '7', '8', '9'};
        public static char iconeJogador, iconeComputador;

        // Conexão com o banco de dados (tornei uma variável global e estática, mas poderia passá-la como parâmetro para as classes que a usam)
        public static string connectionString = "Server=localhost\\SQLEXPRESS;Database=JogoDaVelha;Trusted_Connection=True;";

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            // Testa a conexão com o banco de dados
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    Console.WriteLine("Conexão com o banco de dados foi bem-sucedida!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao conectar ao banco de dados: {ex.Message}");
                return; // Encerra o programa se a conexão falhar
            }

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
            Console.WriteLine("");
            // Escolha de ícone e lógica para atribuir o ícone do computador. Usei if ternário para reduzir o código (se jogador for 'O', computador recebe 'X', senão 'O'). 
            // Aqui foi preciso passar como parâmetro o Resource Manager
            iconeJogador = Tabuleiro.EscolhaIcone(rm);
            iconeComputador = iconeJogador == 'O' ? 'X' : 'O';
            Console.Clear();

            // Chamando o método para decidir quem joga primeiro e armazenei a resposta para determinar a ordem das jogadas.
            bool jogadorPrimeiro = Jogadas.QuemJogaPrimeiro(rm);
            // se false computador, se true jogador.
            // while verificavencedor false, looping de jogadas
            while (true)
            {
                if (jogadorPrimeiro)
                {
                    Tabuleiro.ExibirTabuleiro(tabuleiro, rm);
                    Console.WriteLine("");
                    Jogadas.VezJogador(rm); // Jogada do jogador
                }
                else
                {
                    Tabuleiro.ExibirTabuleiro(tabuleiro, rm);
                    Console.WriteLine("");
                    Jogadas.VezComputador(rm); // Jogada do computador
                }
                // Alterna a vez entre jogador e computador
                jogadorPrimeiro = !jogadorPrimeiro;
            }
        }

        public static void SalvarPartida(string connectionString, string vencedor, DateTime dataHora, string jogador, int posicao, int ordem)
        {
            // Comando SQL para inserir os dados na tabela Partidas
            string queryPartida = @"
                INSERT INTO Partidas (Vencedor, DataHora)
                VALUES (@Vencedor, @DataHora);
                SELECT SCOPE_IDENTITY();"; // Retorna o ID da última partida inserida
            
            // Conexão com o banco
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand commandPartida = new SqlCommand(queryPartida, connection))
                {
                    // Adiciona os parâmetros ao comando para a partida
                    commandPartida.Parameters.AddWithValue("@Vencedor", vencedor);
                    commandPartida.Parameters.AddWithValue("@DataHora", dataHora);

                    // Executa o comando e obtém o ID da partida recém-inserida
                    int partidaId = Convert.ToInt32(commandPartida.ExecuteScalar());

                    // Agora insere as jogadas
                    string queryJogadas = @"
                        INSERT INTO Jogadas (PartidaId, Jogador, Posicao, ordem)
                        VALUES (@PartidaId, @Jogador, @Posicao, @ordem)";

                    using (SqlCommand commandJogadas = new SqlCommand(queryJogadas, connection))
                    {
                        // Adiciona os parâmetros ao comando para as jogadas
                        commandJogadas.Parameters.AddWithValue("@PartidaId", partidaId);
                        commandJogadas.Parameters.AddWithValue("@Jogador", jogador);
                        commandJogadas.Parameters.AddWithValue("@Posicao", posicao != -1 ? (object)posicao : DBNull.Value); // Tratamento para posição nula
                        commandJogadas.Parameters.AddWithValue("@ordem", ordem);

                        // Executa o comando para inserir as jogadas
                        commandJogadas.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
