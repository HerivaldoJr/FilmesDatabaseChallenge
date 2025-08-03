namespace FilmesDatabaseChallenge.Data.Models;

public class ElencoFilme
{
    public int Id { get; set; }
    public int IdFilme { get; set; }
    public int IdAtor { get; set; }
    public string Papel { get; set; } = null!;
    
    public Filme Filme { get; set; } = null!;
    public Ator Ator { get; set; } = null!;
}