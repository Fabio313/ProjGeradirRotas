using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Model;

namespace APIEquipe.Data
{
    public class APIEquipeContext : DbContext
    {
        public APIEquipeContext (DbContextOptions<APIEquipeContext> options)
            : base(options)
        {
        }

        public DbSet<Model.Equipe> Equipe { get; set; }
    }
}
