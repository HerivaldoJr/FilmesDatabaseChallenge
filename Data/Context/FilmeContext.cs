using FilmesDatabaseChallenge.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmesDatabaseChallenge.Data.Context;

public class FilmeContext : DbContext
{
    public FilmeContext(DbContextOptions<FilmeContext> options) : base(options)
    {
    }
    
    public DbSet<Filme> Filmes { get; set; }
    public DbSet<Ator> Atores { get; set; }
    public DbSet<Genero> Generos { get; set; }
    public DbSet<ElencoFilme> ElencoFilmes { get; set; }
    public DbSet<FilmeGenero> FilmeGeneros { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Filme>().ToTable("Filmes");
        modelBuilder.Entity<Ator>().ToTable("Atores");
        modelBuilder.Entity<Genero>().ToTable("Generos");
        modelBuilder.Entity<ElencoFilme>().ToTable("ElencoFilme");
        modelBuilder.Entity<FilmeGenero>().ToTable("FilmesGenero");
    }
}