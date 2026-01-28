using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SistemaEstacionamento.Main.Models;

namespace SistemaEstacionamento.Main.Data
{
    public class SistemaEstacionamentoContext : DbContext
    {
        public SistemaEstacionamentoContext (DbContextOptions<SistemaEstacionamentoContext> options)
            : base(options)
        {
        }

        public DbSet<SistemaEstacionamento.Main.Models.Estacionamento> Estacionamento { get; set; } = default!;
    }
}
