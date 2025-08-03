using FilmesDatabaseChallenge.Data.Context;
using FilmesDatabaseChallenge.Data.Models;
using FilmesDatabaseChallenge.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

// Configuração
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

// Teste de conexão antes de prosseguir
try
{
    Console.WriteLine("Testando conexão com o banco...");
    using var testConn = new SqlConnection(configuration.GetConnectionString("FilmeConnection"));
    await testConn.OpenAsync();
    Console.WriteLine("Conexão bem-sucedida!");
    await testConn.CloseAsync();
}
catch (Exception ex)
{
    Console.WriteLine($"Falha na conexão: {ex.Message}");
    Console.WriteLine("Verifique:");
    Console.WriteLine("- Se o SQL Server (CDDMI01\\SQLEXPRESS) está online");
    Console.WriteLine("- Se o nome do servidor está correto no appsettings.json");
    Console.WriteLine("- Se o banco 'Filmes' existe no servidor");
    Console.WriteLine("- Sua rede corporativa (se aplicável)");
    return;
}

// Configurar DbContext
var optionsBuilder = new DbContextOptionsBuilder<FilmeContext>();
optionsBuilder.UseSqlServer(configuration.GetConnectionString("FilmeConnection"));

using var context = new FilmeContext(optionsBuilder.Options);
var filmeService = new FilmeService(context);

// Menu interativo
while (true)
{
    Console.WriteLine("\n=== MENU DE CONSULTAS ===");
    Console.WriteLine("1. Buscar o nome e ano dos filmes");
    Console.WriteLine("2. Buscar o nome e ano dos filmes ordenados por ano");
    Console.WriteLine("3. Buscar filme 'De Volta para o Futuro' (nome, ano, duração)");
    Console.WriteLine("4. Buscar filmes lançados em 1997");
    Console.WriteLine("5. Buscar filmes lançados após 2000");
    Console.WriteLine("6. Buscar filmes com duração entre 100 e 150 minutos");
    Console.WriteLine("7. Quantidade de filmes por ano (ordem decrescente)");
    Console.WriteLine("8. Atores do gênero masculino");
    Console.WriteLine("9. Atores do gênero feminino (ordenados por nome)");
    Console.WriteLine("10. Nome do filme e gênero");
    Console.WriteLine("11. Filmes do gênero 'Mistério'");
    Console.WriteLine("12. Filmes com atores e seus papéis");
    Console.WriteLine("0. Sair");
    Console.Write("\nEscolha uma opção (0-12): ");

    if (!int.TryParse(Console.ReadLine(), out var opcao))
    {
        Console.WriteLine("Opção inválida!");
        continue;
    }

    if (opcao == 0) break;

    Console.WriteLine();
    await ExecutarConsulta(opcao, filmeService);
}

async Task ExecutarConsulta(int opcao, FilmeService service)
{
    try
    {
        switch (opcao)
        {
            case 1:
                Console.WriteLine("1. Nome e ano dos filmes:");
                var filmes1 = await service.GetNomeAnoFilmes();
                ImprimirResultado(filmes1);
                break;

            case 2:
                Console.WriteLine("2. Nome e ano dos filmes ordenados por ano:");
                var filmes2 = await service.GetNomeAnoFilmesOrdenados();
                ImprimirResultado(filmes2);
                break;

            case 3:
                Console.WriteLine("3. Filme 'De Volta para o Futuro':");
                var voltaFuturo = await service.GetVoltaParaOFuturo();
                Console.WriteLine($"{voltaFuturo?.Nome} - {voltaFuturo?.Ano} - {voltaFuturo?.Duracao} min");
                break;

            case 4:
                Console.WriteLine("4. Filmes lançados em 1997:");
                var filmes1997 = await service.GetFilmes1997();
                ImprimirResultado(filmes1997);
                break;

            case 5:
                Console.WriteLine("5. Filmes lançados após 2000:");
                var filmesApos2000 = await service.GetFilmesApos2000();
                ImprimirResultado(filmesApos2000);
                break;

            case 6:
                Console.WriteLine("6. Filmes com duração entre 100 e 150 minutos:");
                var filmesPorDuracao = await service.GetFilmesPorDuracao();
                ImprimirResultado(filmesPorDuracao);
                break;

            case 7:
                Console.WriteLine("7. Quantidade de filmes por ano:");
                var filmesPorAno = await service.GetQuantidadeFilmesPorAno();
                foreach (var item in filmesPorAno)
                {
                    Console.WriteLine($"Ano: {item.Ano}, Quantidade: {item.Quantidade}");
                }
                break;

            case 8:
                Console.WriteLine("8. Atores do gênero masculino:");
                var atoresMasculinos = await service.GetAtoresMasculinos();
                ImprimirResultado(atoresMasculinos);
                break;

            case 9:
                Console.WriteLine("9. Atores do gênero feminino:");
                var atoresFemininos = await service.GetAtoresFemininos();
                ImprimirResultado(atoresFemininos);
                break;

            case 10:
                Console.WriteLine("10. Nome do filme e gênero:");
                var filmesGeneros = await service.GetFilmesComGeneros();
                foreach (var item in filmesGeneros)
                {
                    Console.WriteLine($"Filme: {item.FilmeNome}, Gênero: {item.GeneroNome}");
                }
                break;

            case 11:
                Console.WriteLine("11. Filmes do gênero 'Mistério':");
                var filmesMisterio = await service.GetFilmesMisterio();
                foreach (var item in filmesMisterio)
                {
                    Console.WriteLine($"Filme: {item.FilmeNome}, Gênero: {item.GeneroNome}");
                }
                break;

            case 12:
                Console.WriteLine("12. Filmes com atores e seus papéis:");
                var filmesAtores = await service.GetFilmesComAtores();
                foreach (var item in filmesAtores)
                {
                    Console.WriteLine($"Filme: {item.FilmeNome}, Ator: {item.PrimeiroNome} {item.UltimoNome}, Papel: {item.Papel}");
                }
                break;

            default:
                Console.WriteLine("Opção inválida!");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro ao executar consulta: {ex.Message}");
    }
}

void ImprimirResultado<T>(List<T> items)
{
    if (items == null || items.Count == 0)
    {
        Console.WriteLine("Nenhum resultado encontrado.");
        return;
    }

    if (typeof(T) == typeof(Filme))
    {
        foreach (var item in items.Cast<Filme>())
        {
            Console.WriteLine($"{item.Nome} - {item.Ano}{(item.Duracao > 0 ? $" - {item.Duracao} min" : "")}");
        }
        return;
    }

    if (typeof(T) == typeof(Ator))
    {
        foreach (var item in items.Cast<Ator>())
        {
            Console.WriteLine($"{item.PrimeiroNome} {item.UltimoNome}");
        }
        return;
    }

    foreach (var item in items)
    {
        Console.WriteLine(item?.ToString() ?? "null");
    }
}