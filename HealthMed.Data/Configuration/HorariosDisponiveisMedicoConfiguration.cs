using HealthMed.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Data.Configuration
{
    public class HorariosDisponiveisMedicoConfiguration : IEntityTypeConfiguration<HorariosDisponiveisMedicoModel>
    {
        public void Configure(EntityTypeBuilder<HorariosDisponiveisMedicoModel> builder)
        {
            builder.ToTable("HM_HORARIOS_DISPONIVEIS");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).HasColumnType("INT").UseIdentityColumn();
            builder.Property(h => h.MedicoId)
           .IsRequired();

            builder.Property(h => h.DiaSemana)
                .IsRequired()
                .HasConversion<int>(); // Converte o enum para inteiro no banco

            builder.Property(h => h.HorarioInicio)
                .IsRequired()
                .HasColumnType("time"); // Define como tipo "time" no SQL

            builder.Property(h => h.HorarioFim)
                .IsRequired()
                .HasColumnType("time");

            builder.Property(h => h.Disponivel)
                .IsRequired();

            builder.Property(h => h.Data)
           .IsRequired()
           .HasColumnType("date");

            // Relacionamento com o Médico
            builder.HasOne(h => h.Medico)
                .WithMany(m => m.HorariosDisponiveis)
                .HasForeignKey(h => h.MedicoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Índice para busca rápida nos horários disponíveis
            builder.HasIndex(h => new { h.MedicoId, h.DiaSemana, h.HorarioInicio })
                .IsUnique()
                .HasDatabaseName("IX_Horarios_Medico_Dia_Horario");
        }
    }
}
