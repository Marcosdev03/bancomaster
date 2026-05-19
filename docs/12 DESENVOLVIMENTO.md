# Desenvolvimento

## Status atual

Primeira entrega criada:

- Repositorio Git local.
- Solution `BancoDigital.sln`.
- Projeto C# `.NET 8` `BancoDigital.Api`.
- Projeto de testes `BancoDigital.Tests`.
- Swagger configurado.
- Health check da API.
- Health check do banco SQL Server.

## Pasta do projeto

```text
D:\DevStack\projetos\banco-digital
```

## Rodar a API

```powershell
cd D:\DevStack\projetos\banco-digital
D:\DevStack\dotnet\dotnet.exe run --project src\BancoDigital.Api
```

## Swagger

Quando a API estiver rodando, abra o endereco mostrado no terminal e acesse:

```text
/swagger
```

## Endpoints iniciais

```http
GET /health
```

Verifica se a API esta viva.

```http
GET /health/database
```

Verifica se a API consegue acessar o SQL Server e contar clientes na base `BancoTreino`.

## Build

```powershell
cd D:\DevStack\projetos\banco-digital
D:\DevStack\dotnet\dotnet.exe build
```

## Testes

```powershell
cd D:\DevStack\projetos\banco-digital
D:\DevStack\dotnet\dotnet.exe test
```

## Configuracao local

Durante o treino, a string de conexao esta em:

```text
src\BancoDigital.Api\appsettings.Development.json
```

Em ambiente real, senha e chave JWT devem ir para variaveis de ambiente ou Kubernetes Secret.

## Proximos passos

1. Criar modulo de autenticacao.
2. Criar `POST /auth/login`.
3. Gerar JWT.
4. Proteger endpoints.
5. Criar endpoints de clientes e contas.
