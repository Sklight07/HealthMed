using HealthMed.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Data.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurações adicionais
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        public DbSet<MedicoModel> Medico { get; set; }
        public DbSet<PacienteModel> Paciente { get; set; }
        public DbSet<HorariosDisponiveisMedicoModel> HorariosDisponiveisMedico { get; set; }
        public DbSet<AgendamentoModel> Agendamento { get; set; }
    }
}
