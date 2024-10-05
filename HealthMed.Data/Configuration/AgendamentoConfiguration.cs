using HealthMed.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Data.Configuration
{
    public class AgendamentoConfiguration : IEntityTypeConfiguration<AgendamentoModel>
    {
        public void Configure(EntityTypeBuilder<AgendamentoModel> builder)
        {
            builder.ToTable("HM_AGENDAMENTO");
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id).HasColumnType("INT").UseIdentityColumn();

            builder.Property(h => h.MedicoId)
           .IsRequired();

            builder.Property(h => h.PacienteId)
           .IsRequired();

            builder.Property(h => h.HorarioId)
           .IsRequired();

            builder.Property(c => c.DataConsulta)
                .IsRequired();

            builder.HasOne(c => c.Paciente)
                .WithMany(p => p.Consultas)
                .HasForeignKey(c => c.PacienteId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Medico)
                .WithMany(m => m.Consultas)
                .HasForeignKey(c => c.MedicoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.HorarioDisponivel)
                .WithMany()
                .HasForeignKey(c => c.HorarioId)
                .OnDelete(DeleteBehavior.Restrict); 

            builder.HasIndex(c => new { c.HorarioId })
                .IsUnique()
                .HasDatabaseName("IX_Consulta_HorarioId");
        }
    }
}
