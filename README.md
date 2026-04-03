# DealershipAI

API web cujo **foco principal** é a **busca de veículos em linguagem natural**: o usuário descreve o que procura (marca, modelo, versão, etc.) e o sistema usa um modelo de linguagem para extrair critérios estruturados e consultar o catálogo.

A **persistência em banco** existe sobretudo para **cadastrar carros e alimentar testes** do fluxo de busca, não como produto central do projeto.

## Objetivo

Explorar um assistente de estoque em português: interpretar a mensagem do usuário, traduzir em filtros e devolver veículos compatíveis. Uma **interface estática** em `wwwroot` (chat simples) é servida pelo mesmo host da API para experimentar tudo no navegador, na mesma origem.

## Arquitetura (visão geral)

O backend está organizado em **camadas conceituais** dentro de um único projeto ASP.NET Core:

| Camada | Papel |
|--------|--------|
| **Controllers** | Expõe HTTP em `/cars` e `/chat` (prefixo `api` nas rotas), validação básica de entrada e delegação aos casos de uso. |
| **Use cases** | O núcleo é a **busca** que combina **IA + consulta ao catálogo**; há também criação de veículo com validação para apoiar testes. |
| **Repositórios** | Acesso a dados com **Entity Framework Core** e **MySQL**; repositórios read/write e **Unit of Work** onde faz sentido. |
| **Modelos** | Entidades, DTOs de request/response e contratos de repositório. |

Fluxo da busca: a mensagem vai ao **Google Gemini** com prompt que força JSON (`brand`, `model`, `version`). Esse JSON vira filtros; a consulta ao banco usa critérios parciais e retorna os carros encontrados.

## Estrutura do repositório

- **`DealershipAI.slnx`** — solução que referencia o projeto da API.
- **`src/DearlershipAI.API/`** — projeto Web único (API + arquivos estáticos).

Pastas principais dentro da API (sem listar cada arquivo):

- `Controllers/` — endpoints REST.
- `Services/UseCases/` — casos de uso (busca com IA, criação de carro para testes).
- `Repositories/DataAccess/` — `DbContext`, repositórios e unit of work.
- `Models/` — entidades, DTOs e interfaces de repositório.
- `Services/AutoMapper/` — mapeamento entre DTOs e entidades.
- `wwwroot/` — front-end estático do chat.


## Tecnologias

- .NET 10  
- ASP.NET Core (controllers, arquivos estáticos, fallback para a página do chat)  
- Entity Framework Core 10  
- MySQL (MySql.EntityFrameworkCore)  
- Google GenAI / Gemini (extração JSON a partir do texto do usuário)  
- AutoMapper  
- FluentValidation (cadastro de veículos)  
- Swashbuckle / Swagger UI e OpenAPI (desenvolvimento)

## Configuração

1. **MySQL** — banco referenciado na connection string (ex.: `dealershipdb`).
2. **`appsettings.Development.json`** (ou variáveis de ambiente / User Secrets em produção):
   - `ConnectionStrings:Connection` — conexão MySQL.
   - `ConnectionStrings:API_KEY` — chave da API Google para o Gemini.

Não commite chaves reais; use User Secrets ou variáveis de ambiente em ambientes compartilhados.

## Como executar

Na raiz do repositório (SDK .NET 10):

```bash
dotnet run --project src/DearlershipAI.API/DearlershipAI.API.csproj
```

Em desenvolvimento há OpenAPI/Swagger; o chat fica na raiz do site (arquivos estáticos).

## Endpoints

- **`/cars`** — cadastro de veículos para popular o catálogo nos testes.  
- **`/chat`** — busca em linguagem natural (fluxo principal).

Rotas em minúsculas (`RouteOptions.LowercaseUrls`).
