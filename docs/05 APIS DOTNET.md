# APIs .NET

## Versao principal

Vamos usar .NET 8 como versao principal da API.

Motivo:

- E LTS.
- E atual.
- E adequada para novos projetos.

.NET 6 e .NET 7 ficam instalados para comparacao e compatibilidade.

## Projeto principal

Nome:

```text
BancoDigital.Api
```

## Estrutura planejada

```text
src/
  BancoDigital.Api/
    Controllers/
    Endpoints/
    Auth/
    Clientes/
    Contas/
    Transferencias/
    Emprestimos/
    Auditoria/
    Kafka/
    Data/
```

## Endpoints iniciais

### Health

```http
GET /health
```

Retorna se a API esta viva.

### Login

```http
POST /auth/login
```

Body:

```json
{
  "cpf": "70000000001",
  "senha": "123456"
}
```

Resposta:

```json
{
  "accessToken": "...",
  "expiresIn": 3600
}
```

### Meu perfil

```http
GET /clientes/me
```

Retorna dados do cliente autenticado.

### Minhas contas

```http
GET /contas
```

Retorna contas do cliente autenticado.

### Extrato

```http
GET /contas/{contaId}/extrato
```

Retorna transacoes da conta.

### Transferencia

```http
POST /transferencias
```

Body:

```json
{
  "contaOrigemId": 1,
  "contaDestinoId": 2,
  "valor": 100.50,
  "descricao": "Treino de transferencia"
}
```

## Padrao de resposta

Sucesso:

```json
{
  "data": {},
  "success": true
}
```

Erro:

```json
{
  "success": false,
  "message": "Mensagem amigavel"
}
```

## Bibliotecas planejadas

- `Microsoft.Data.SqlClient`
- `Dapper` ou `EntityFrameworkCore`
- `Microsoft.AspNetCore.Authentication.JwtBearer`
- `Confluent.Kafka`
- `Swashbuckle.AspNetCore`

## Escolha inicial de acesso a dados

Para aprender SQL de verdade, a primeira versao pode usar Dapper ou `Microsoft.Data.SqlClient`.

Motivo:

- Voce ve o SQL.
- Fica mais claro o que esta acontecendo.
- Ajuda no treino com DBeaver.

Depois podemos criar uma versao com Entity Framework Core.

