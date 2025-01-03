using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities; // Importando entidades (Jogo e Jogada)
using Microsoft.EntityFrameworkCore; 

namespace src.Context
{
    public class SaveGameContext: DbContext
    {
        // Construtor do contexto, recebendo as opções configuradas no Program.cs
        public SaveGameContext (DbContextOptions<SaveGameContext>options) : base(options)
        {

        }        
        
        // DbSet para as entidades
        public DbSet<Jogo> Jogos { get; set; } // Representa a tabela de Jogos
        public DbSet<Jogada> Jogadas { get; set; } // Representa a tabela de Jogadas
    }


}

