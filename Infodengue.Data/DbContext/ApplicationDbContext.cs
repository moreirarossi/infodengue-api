using Infodengue.Domain.Entities;
using Infodengue.Domain.Model;
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
        public DbSet<IBGEDados> IBGEDados { get; set; }
        public DbSet<IBGEDadosRelatorio> IBGEDadosRelatorios { get; set; }
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

            modelBuilder.Entity<IBGEDados>(entity =>
            {
                entity.ToTable("IBGEDados");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.DataIniSE);

                entity.Property(e => e.SE);

                entity.Property(e => e.CasosEst);

                entity.Property(e => e.CasosEstMin);

                entity.Property(e => e.CasosEstMax);

                entity.Property(e => e.Casos);

                entity.Property(e => e.PRt1);

                entity.Property(e => e.PInc100k);

                entity.Property(e => e.LocalidadeId);

                entity.Property(e => e.Nivel);

                entity.Property(e => e.VersaoModelo);

                entity.Property(e => e.Tweet);

                entity.Property(e => e.Rt);

                entity.Property(e => e.Pop);

                entity.Property(e => e.TempMin);

                entity.Property(e => e.UmidMax);

                entity.Property(e => e.Receptivo);

                entity.Property(e => e.Transmissao);

                entity.Property(e => e.NivelInc);

                entity.Property(e => e.UmidMed);

                entity.Property(e => e.UmidMin);

                entity.Property(e => e.TempMed);

                entity.Property(e => e.TempMax);

                entity.Property(e => e.CasProv);

                entity.Property(e => e.CasProvEst);

                entity.Property(e => e.CasProvEstMin);

                entity.Property(e => e.CasProvEstMax);

                entity.Property(e => e.CasConf);

                entity.Property(e => e.NotifAccumYear);
            });

            modelBuilder.Entity<IBGEDadosRelatorio>(entity =>
            {
                entity.ToTable("IBGEDadosRelatorios");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .IsRequired();

                entity.Property(e => e.RelatorioId)
                    .IsRequired();

                entity.Property(e => e.IBGEDadosId)
                    .IsRequired();

            });
        }
    }
}