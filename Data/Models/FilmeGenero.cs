namespace FilmesDatabaseChallenge.Data.Models;

public class FilmeGenero
{
    public int Id { get; set; }
    public int IdFilme { get; set; }
    public int IdGenero { get; set; }
    
    public Filme Filme { get; set; } = null!;
    public Genero Genero { get; set; } = null!;
}