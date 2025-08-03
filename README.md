## Desafio de Banco de Dados com C# e .NET 9
Projeto desenvolvido como parte do desafio de Banco de Dados com C# e Entity Framework Core.

## 🗃️ Descrição do Projeto
Sistema que realiza consultas em banco de dados de filmes, demonstrando:

- Operações CRUD

- Relacionamentos entre tabelas

- Consultas LINQ complexas

- Entity Framework Core

## ⚙️ Funcionalidades
- 12 consultas diferentes em banco de filmes

- Modelagem de entidades relacionais

- Operações com relacionamentos 1:N e N:N

- Agregações e agrupamentos

## 🛠️ Tecnologias
- .NET 9.0

- C#

- Entity Framework Core

- SQL Server

## 📂 Estrutura do Projeto
```/FilmesDatabaseChallenge
│   Program.cs
│   FilmesDatabaseChallenge.csproj
│   README.md
│   appsettings.json
│
└───/Data
│   └───/Context
│   │   │   FilmeContext.cs
│   │
│   └───/Models
│       │   Ator.cs
│       │   ElencoFilme.cs
│       │   Filme.cs
│       │   FilmeGenero.cs
│       │   Genero.cs
│
└───/Services
    │   FilmeService.cs
```
## 🚀 Como Executar
```
dotnet restore
dotnet build
dotnet run
```
## 📝 Licença
Este projeto está sob a licença MIT.
