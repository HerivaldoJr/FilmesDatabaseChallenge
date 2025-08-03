namespace FilmesDatabaseChallenge.Data.Models;

public class Genero
{
    public int Id { get; set; }
    public string Nome { get; set; } = null!;
    
    public ICollection<FilmeGenero> FilmeGeneros { get; set; } = null!;
}