using HealthMed.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthMed.Data.Configuration
{
    public class PacienteConfiguration : IEntityTypeConfiguration<PacienteModel>
    {
        public void Configure(EntityTypeBuilder<PacienteModel> builder)
        {
            builder.ToTable("HM_PACIENTE");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).HasColumnType("INT").UseIdentityColumn();
            builder.Property(u => u.Nome).HasColumnType("VARCHAR(100)").IsRequired();
            builder.Property(u => u.Permissao).HasConversion<int>().IsRequired();
            builder.Property(u => u.Senha).HasColumnType("VARCHAR(30)").IsRequired();
            builder.Property(u => u.CPF).HasColumnType("VARCHAR(11)").IsRequired();
            builder.Property(u => u.Email).HasColumnType("VARCHAR(100)").IsRequired();
        }
    }
}
