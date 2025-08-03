namespace FilmesDatabaseChallenge.Data.Models;

public class Filme
{
    public int Id { get; set; }
    public string Nome { get; set; } = null!;
    public int Ano { get; set; }
    public int Duracao { get; set; }
    
    public ICollection<ElencoFilme> ElencoFilmes { get; set; } = null!;
    public ICollection<FilmeGenero> FilmeGeneros { get; set; } = null!;
}