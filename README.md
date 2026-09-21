# OpenMarket

> ⚠️ **Projeto em construção.** A API está nos estágios iniciais de desenvolvimento. Apenas o módulo de **Categorias** está funcional no momento, e o cliente (front-end) ainda não foi iniciado.

OpenMarket é uma aplicação de marketplace composta por uma API em **ASP.NET Core (.NET 10)** e um cliente ainda a ser desenvolvido. O back-end é organizado seguindo os princípios de **Clean Architecture**, separando responsabilidades em camadas bem definidas.

## Índice

- [Status do projeto](#status-do-projeto)
- [Tecnologias](#tecnologias)
- [Arquitetura](#arquitetura)
- [Estrutura de pastas](#estrutura-de-pastas)
- [Como executar](#como-executar)
- [Endpoints disponíveis](#endpoints-disponíveis)
- [Licença](#licença)

## Status do projeto

| Módulo                         | Situação          |
| ------------------------------ | ----------------- |
| Categorias (CRUD parcial)      | ✅ Em andamento    |
| Produtos (entidade criada)     | 🚧 Em construção   |
| Cliente / Front-end            | ⏳ Não iniciado    |

## Tecnologias

- [.NET 10 / ASP.NET Core](https://dotnet.microsoft.com/) — Web API
- [Entity Framework Core 10](https://learn.microsoft.com/ef/core/) — ORM
- [PostgreSQL 17](https://www.postgresql.org/) — banco de dados (via `Npgsql.EntityFrameworkCore.PostgreSQL`)
- [AutoMapper](https://automapper.org/) — mapeamento entre entidades e ViewModels
- [Swagger / OpenAPI](https://swagger.io/) — documentação da API
- [Docker Compose](https://docs.docker.com/compose/) — infraestrutura local (PostgreSQL)

## Arquitetura

O back-end segue o padrão de **Clean Architecture**, dividido nas seguintes camadas:

- **API** — camada de apresentação. Controllers, configuração de pipeline HTTP e OpenAPI.
- **Application** — regras de aplicação. Serviços, ViewModels e perfis do AutoMapper.
- **Domain** — entidades de domínio (`Category`, `Product`) e contratos de repositório.
- **Core.Domain** — abstrações base compartilhadas (ex.: a classe `Entity`).
- **Infrastructure** — acesso a dados. `AppDbContext` (EF Core), repositórios e configuração do PostgreSQL.

As dependências apontam sempre em direção ao domínio, mantendo as regras de negócio isoladas de detalhes de infraestrutura.

## Estrutura de pastas

```
OpenMarket/
├── client/                 # Front-end (ainda não iniciado)
├── server/
│   ├── docker-compose.yml  # PostgreSQL para desenvolvimento local
│   ├── OpenMarket.slnx
│   └── src/
│       ├── API/            # Controllers e configuração da Web API
│       ├── Application/    # Serviços, ViewModels e mappings
│       ├── Domain/         # Entidades e interfaces de repositório
│       ├── Core.Domain/    # Abstrações base (Entity)
│       └── Infrastructure/ # DbContext, repositórios e persistência
├── LICENSE
└── README.md
```

## Como executar

### Pré-requisitos

- [.NET SDK 10](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/) e Docker Compose

### 1. Subir o banco de dados

Na pasta `server`, inicie o container do PostgreSQL:

```bash
cd server
docker compose up -d
```

Isso disponibiliza um PostgreSQL em `localhost:5432` com as credenciais padrão de desenvolvimento (usuário/senha/banco: `openmarket`). A connection string está definida em `server/src/API/appsettings.json`.

### 2. Executar a API

```bash
cd server/src/API
dotnet run
```

A API ficará disponível em:

- HTTP: `http://localhost:5092`
- HTTPS: `https://localhost:7272`

A documentação Swagger UI fica acessível em `/swagger` (ambiente de desenvolvimento).

## Endpoints disponíveis

Base: `/api/categories`

| Método | Rota                              | Descrição                       |
| ------ | --------------------------------- | ------------------------------- |
| GET    | `/api/categories`                 | Lista todas as categorias       |
| GET    | `/api/categories/{id}`            | Busca uma categoria por `id`    |
| GET    | `/api/categories/by-name/{name}`  | Busca uma categoria pelo nome   |
| POST   | `/api/categories`                 | Cria uma nova categoria         |

> Mais endpoints (produtos, autenticação, etc.) serão adicionados conforme o projeto evolui.

## Licença

Distribuído sob a licença MIT. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.
