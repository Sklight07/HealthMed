using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthMed.Domain.Entities;

namespace HealthMed.Data.Configuration
{
    public class MedicoConfiguration : IEntityTypeConfiguration<MedicoModel>
    {
        public void Configure(EntityTypeBuilder<MedicoModel> builder)
        {
            builder.ToTable("HM_MEDICO");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).HasColumnType("INT").UseIdentityColumn();
            builder.Property(u => u.Nome).HasColumnType("VARCHAR(100)").IsRequired();
            builder.Property(u => u.Permissao).HasConversion<int>().IsRequired();
            builder.Property(u => u.Senha).HasColumnType("VARCHAR(30)").IsRequired();
            builder.Property(u => u.CPF).HasColumnType("VARCHAR(11)").IsRequired();
            builder.Property(u => u.NumeroCrm).HasColumnType("VARCHAR(15)").IsRequired();
            builder.Property(u => u.UfCrm).HasColumnType("VARCHAR(2)").IsRequired();
            builder.Property(u => u.Email).HasColumnType("VARCHAR(100)").IsRequired();
        }
    }
}
