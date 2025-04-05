using Infodengue.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infodengue.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Solicitante> Solicitantes { get; set; }
        public DbSet<Relatorio> Relatorios { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Solicitante>(entity =>
            {
                entity.ToTable("Solicitantes");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.CPF)
                    .IsRequired()
                    .HasMaxLength(14);
            });

            modelBuilder.Entity<Relatorio>(entity =>
            {
                entity.ToTable("Relatorios");

                entity.HasKey(r => r.Id);

                entity.Property(r => r.SolicitanteId)
                    .IsRequired();

                entity.Property(r => r.DataSolicitacao)
                    .IsRequired();

                entity.Property(r => r.Arbovirose)
                    .HasMaxLength(100);

                entity.Property(r => r.SemanaInicio)
                    .HasMaxLength(10);

                entity.Property(r => r.SemanaTermino)
                    .HasMaxLength(10);

                entity.Property(r => r.CodigoIBGE)
                    .HasMaxLength(10);

                entity.Property(r => r.Municipio)
                    .HasMaxLength(150);

                entity.Property(r => r.TotalCasos);


            });

        }
    }
}