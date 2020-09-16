using ApiTeste.Models.ObjectTypes;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ApiTeste.Models;

namespace ApiTeste.Models.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public DbSet<Ponto> Pontos { get; set; }
        public DbSet<Desenvolvedor> Desenvolvedores { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Telefone> Telefones { get; set; }
        public DbSet<Projeto> Projeto { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Desenvolvedor>()
                .HasOne(d => d.Telefone)
                .WithOne(t => t.Desenvolvedor)
                .HasForeignKey<Telefone>(t => t.DesenvolvedorId);

            builder.Entity<Desenvolvedor>()
                .HasOne(d => d.Endereco)
                .WithOne(e=>e.Desenvolvedor)
                .HasForeignKey<Endereco>(e=>e.DesenvolvedorId);

            builder.Entity<Ponto>()
                .HasOne(p=>p.Desenvolvedor)
                .WithMany(d=>d.ControleHoras)
                .IsRequired();

            builder.Entity<Ponto>()
                .HasOne(p => p.Projeto)
                .WithMany(proj => proj.ControleHoras)
                .IsRequired();

        }

    }
}
