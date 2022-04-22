using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Model;

namespace MVCControleRotas.Data
{
    public class MVCControleRotasContext : DbContext
    {
        public MVCControleRotasContext (DbContextOptions<MVCControleRotasContext> options)
            : base(options)
        {
        }

        public DbSet<Model.Cidade> Cidade { get; set; }

        public DbSet<Model.Pessoa> Pessoa { get; set; }

        public DbSet<Model.Equipe> Equipe { get; set; }

        public DbSet<Model.Usuario> Usuario { get; set; }
    }
}
