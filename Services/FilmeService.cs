using FilmesDatabaseChallenge.Data.Context;
using FilmesDatabaseChallenge.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmesDatabaseChallenge.Services;

public class FilmeService
{
    private readonly FilmeContext _context;

    public FilmeService(FilmeContext context)
    {
        _context = context;
    }

    // 1. Buscar o nome e ano dos filmes
    public async Task<List<Filme>> GetNomeAnoFilmes()
    {
        return await _context.Filmes
            .Select(f => new Filme { Nome = f.Nome, Ano = f.Ano })
            .ToListAsync();
    }

    // 2. Buscar o nome e ano dos filmes, ordenados por ordem crescente pelo ano
    public async Task<List<Filme>> GetNomeAnoFilmesOrdenados()
    {
        return await _context.Filmes
            .OrderBy(f => f.Ano)
            .Select(f => new Filme { Nome = f.Nome, Ano = f.Ano })
            .ToListAsync();
    }

    // 3. Buscar pelo filme de volta para o futuro, trazendo o nome, ano e a duração
    public async Task<Filme?> GetVoltaParaOFuturo()
    {
        return await _context.Filmes
            .Where(f => f.Nome == "De Volta para o Futuro")
            .Select(f => new Filme { Nome = f.Nome, Ano = f.Ano, Duracao = f.Duracao })
            .FirstOrDefaultAsync();
    }

    // 4. Buscar os filmes lançados em 1997
    public async Task<List<Filme>> GetFilmes1997()
    {
        return await _context.Filmes
            .Where(f => f.Ano == 1997)
            .ToListAsync();
    }

    // 5. Buscar os filmes lançados APÓS o ano 2000
    public async Task<List<Filme>> GetFilmesApos2000()
    {
        return await _context.Filmes
            .Where(f => f.Ano > 2000)
            .ToListAsync();
    }

    // 6. Buscar os filmes com duração maior que 100 e menor que 150, ordenando pela duração em ordem crescente
    public async Task<List<Filme>> GetFilmesPorDuracao()
    {
        return await _context.Filmes
            .Where(f => f.Duracao > 100 && f.Duracao < 150)
            .OrderBy(f => f.Duracao)
            .ToListAsync();
    }

    // 7. Buscar a quantidade de filmes lançadas no ano, agrupando por ano, ordenando pela quantidade em ordem decrescente
    public async Task<List<dynamic>> GetQuantidadeFilmesPorAno()
    {
        return await _context.Filmes
            .GroupBy(f => f.Ano)
            .Select(g => new { Ano = g.Key, Quantidade = g.Count() })
            .OrderByDescending(g => g.Quantidade)
            .ToListAsync<dynamic>();
    }

    // 8. Buscar os Atores do gênero masculino, retornando o PrimeiroNome, UltimoNome
    public async Task<List<Ator>> GetAtoresMasculinos()
    {
        return await _context.Atores
            .Where(a => a.Genero == "M")
            .Select(a => new Ator { PrimeiroNome = a.PrimeiroNome, UltimoNome = a.UltimoNome })
            .ToListAsync();
    }

    // 9. Buscar os Atores do gênero feminino, retornando o PrimeiroNome, UltimoNome, e ordenando pelo PrimeiroNome
    public async Task<List<Ator>> GetAtoresFemininos()
    {
        return await _context.Atores
            .Where(a => a.Genero == "F")
            .OrderBy(a => a.PrimeiroNome)
            .Select(a => new Ator { PrimeiroNome = a.PrimeiroNome, UltimoNome = a.UltimoNome })
            .ToListAsync();
    }

    // 10. Buscar o nome do filme e o gênero
    public async Task<List<dynamic>> GetFilmesComGeneros()
{
    return await _context.Filmes
        .Join(_context.FilmeGeneros,
            filme => filme.Id,
            filmeGenero => filmeGenero.IdFilme,
            (filme, filmeGenero) => new { filme, filmeGenero })
        .Join(_context.Generos,
            fg => fg.filmeGenero.IdGenero,
            genero => genero.Id,
            (fg, genero) => new { 
                FilmeNome = fg.filme.Nome, 
                GeneroNome = genero.Nome 
            })
        .ToListAsync<dynamic>();
}

    // 11. Buscar o nome do filme e o gênero do tipo "Mistério"
    public async Task<List<dynamic>> GetFilmesMisterio()
    {
        return await _context.Filmes
            .Join(_context.FilmeGeneros,
                filme => filme.Id,
                filmeGenero => filmeGenero.IdFilme,
                (filme, filmeGenero) => new { filme, filmeGenero })
            .Join(_context.Generos,
                fg => fg.filmeGenero.IdGenero,
                genero => genero.Id,
                (fg, genero) => new { FilmeNome = fg.filme.Nome, GeneroNome = genero.Nome })
            .Where(x => x.GeneroNome == "Mistério")
            .ToListAsync<dynamic>();
    }

    // 12. Buscar o nome do filme e os atores, trazendo o PrimeiroNome, UltimoNome e seu Papel
    public async Task<List<dynamic>> GetFilmesComAtores()
    {
        return await _context.Filmes
            .Join(_context.ElencoFilmes,
                filme => filme.Id,
                elenco => elenco.IdFilme,
                (filme, elenco) => new { filme, elenco })
            .Join(_context.Atores,
                fe => fe.elenco.IdAtor,
                ator => ator.Id,
                (fe, ator) => new { 
                    FilmeNome = fe.filme.Nome, 
                    PrimeiroNome = ator.PrimeiroNome, 
                    UltimoNome = ator.UltimoNome, 
                    Papel = fe.elenco.Papel 
                })
            .ToListAsync<dynamic>();
    }
}