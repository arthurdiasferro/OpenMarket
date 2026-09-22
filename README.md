# OpenMarket

> ⚠️ **Projeto em construção.** A API já expõe os módulos de **Categorias**, **Produtos** e **Estoque**. O cliente (front-end) ainda não foi iniciado.

OpenMarket é uma aplicação de marketplace composta por uma API em **ASP.NET Core (.NET 10)** e um cliente ainda a ser desenvolvido. O back-end é organizado seguindo os princípios de **Clean Architecture**, separando responsabilidades em camadas bem definidas.

## Índice

- [Status do projeto](#status-do-projeto)
- [Tecnologias](#tecnologias)
- [Arquitetura](#arquitetura)
- [Estrutura de pastas](#estrutura-de-pastas)
- [Regras de negócio](#regras-de-negócio)
- [Como executar](#como-executar)
- [Endpoints disponíveis](#endpoints-disponíveis)
- [Licença](#licença)

## Status do projeto

| Módulo                          | Situação           |
| ------------------------------- | ------------------ |
| Categorias (CRUD)               | ✅ Funcional        |
| Produtos (CRUD)                 | ✅ Funcional        |
| Estoque (criação e consulta)    | ✅ Funcional        |
| Cliente / Front-end             | ⏳ Não iniciado     |

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
- **Application** — regras de aplicação. Serviços, ViewModels, validações e perfis do AutoMapper.
- **Domain** — entidades de domínio (`Category`, `Product`, `Stock`) e contratos de repositório.
- **Core.Domain** — abstrações base compartilhadas (ex.: a classe `Entity` e a `DomainException`).
- **Infrastructure** — acesso a dados. `AppDbContext` (EF Core), repositórios, configurações de mapeamento e migrations.

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
│       ├── Application/    # Serviços, ViewModels, validações e mappings
│       ├── Domain/         # Entidades e interfaces de repositório
│       ├── Core.Domain/    # Abstrações base (Entity, DomainException)
│       └── Infrastructure/ # DbContext, repositórios, configurações e migrations
├── LICENSE
└── README.md
```

## Regras de negócio

Além do CRUD, alguns invariantes são garantidos pela camada de aplicação e traduzidos em respostas HTTP adequadas:

- **Exclusão de categoria** — uma categoria **não pode ser excluída** enquanto houver produtos vinculados a ela. Retorna `409 Conflict`.
- **Exclusão de produto** — um produto **não pode ser excluído** enquanto tiver estoque (`Quantity > 0`). É necessário zerar o estoque antes. Retorna `409 Conflict`. Ao excluir um produto com estoque zerado, o registro de estoque associado é removido junto.
- **Categoria do produto** — ao criar ou atualizar um produto, a categoria informada (`category_id`) deve existir. Caso contrário, retorna `409 Conflict`.
- **Validação de entrada** — campos obrigatórios e identificadores (`Guid`) não vazios são validados via _DataAnnotations_; requisições inválidas retornam `400 Bad Request`.
- **Preço** — o preço de um produto não pode ser negativo (validado na entidade de domínio).
- **Estoque por produto** — cada produto possui no máximo um registro de estoque (índice único em `ProductId`).

## Como executar

### Pré-requisitos

- [.NET SDK 10](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/) e Docker Compose
- Ferramenta de migrations do EF Core (`dotnet-ef`):

  ```bash
  dotnet tool install --global dotnet-ef
  ```

### 1. Subir o banco de dados

Na pasta `server`, inicie o container do PostgreSQL:

```bash
cd server
docker compose up -d
```

Isso disponibiliza um PostgreSQL em `localhost:5432` com as credenciais padrão de desenvolvimento (usuário/senha/banco: `openmarket`). A connection string está definida em `server/src/API/appsettings.json`.

### 2. Aplicar as migrations

A partir da pasta `server`, aplique as migrations para criar/atualizar o schema do banco:

```bash
dotnet ef database update \
  --project src/Infrastructure/Infrastructure.csproj \
  --startup-project src/API/API.csproj
```

> Sempre que uma entidade ou configuração do EF for alterada, gere uma nova migration com `dotnet ef migrations add <Nome>` (mesmos parâmetros `--project` / `--startup-project`) e aplique-a com o comando acima.

### 3. Executar a API

```bash
cd server/src/API
dotnet run
```

A API ficará disponível em:

- HTTP: `http://localhost:5092`
- HTTPS: `https://localhost:7272`

A documentação Swagger UI fica acessível em `/swagger` (ambiente de desenvolvimento).

## Endpoints disponíveis

### Categorias — `/api/categories`

| Método | Rota                              | Descrição                                      |
| ------ | --------------------------------- | ---------------------------------------------- |
| GET    | `/api/categories`                 | Lista todas as categorias                      |
| GET    | `/api/categories/{id}`            | Busca uma categoria por `id`                   |
| GET    | `/api/categories/by-name/{name}`  | Busca uma categoria pelo nome                  |
| POST   | `/api/categories`                 | Cria uma nova categoria                        |
| PUT    | `/api/categories/{id}`            | Atualiza nome e descrição de uma categoria     |
| DELETE | `/api/categories/{id}`            | Exclui uma categoria (se não houver produtos)  |

### Produtos — `/api/products`

| Método | Rota                                    | Descrição                                        |
| ------ | --------------------------------------- | ------------------------------------------------ |
| GET    | `/api/products`                         | Lista todos os produtos                          |
| GET    | `/api/products/{id}`                    | Busca um produto por `id`                        |
| GET    | `/api/products/by-category/{categoryId}`| Lista os produtos de uma categoria               |
| POST   | `/api/products`                         | Cria um novo produto                             |
| PUT    | `/api/products/{id}`                    | Atualiza nome, descrição, preço, categoria e status ativo |
| DELETE | `/api/products/{id}`                    | Exclui um produto (se não houver estoque)        |

### Estoque — `/api/stocks`

| Método | Rota                                  | Descrição                          |
| ------ | ------------------------------------- | ---------------------------------- |
| GET    | `/api/stocks`                         | Lista todos os registros de estoque |
| GET    | `/api/stocks/{id}`                    | Busca um estoque por `id`          |
| GET    | `/api/stocks/by-product/{productId}`  | Busca o estoque de um produto      |
| POST   | `/api/stocks`                         | Cria um registro de estoque        |

> Mais endpoints (autenticação, pedidos, etc.) serão adicionados conforme o projeto evolui.

## Licença

Distribuído sob a licença MIT. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.
