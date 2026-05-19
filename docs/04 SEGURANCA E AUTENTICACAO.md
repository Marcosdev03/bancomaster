# Seguranca E Autenticacao

## Objetivo

Simular seguranca de uma aplicacao bancaria.

Mesmo sendo projeto de treino, a ideia e aprender o desenho correto.

## Autenticacao

Autenticacao responde:

```text
Quem e voce?
```

Primeira versao:

- Login com CPF e senha simulada.
- Geracao de JWT.
- Token enviado no header `Authorization`.

## Estrutura ja criada no banco

A base `BancoTreino` possui uma camada inicial de autenticacao criada pelo script:

```text
D:\DevStack\sqlserver\scripts\02_criar_autenticacao_treino.sql
```

Tabelas:

- `Usuarios`
- `Perfis`
- `UsuarioPerfis`
- `LoginAuditoria`

Procedure:

- `sp_ValidarLoginTreino`

View:

- `vw_UsuariosTreino`

Logins de cliente seguem o formato:

```text
cliente000001
cliente000002
cliente000003
```

Senha padrao de treino:

```text
Treino@123
```

Header:

```text
Authorization: Bearer <token>
```

## Autorizacao

Autorizacao responde:

```text
O que voce pode fazer?
```

Regras:

- Cliente so consulta suas proprias contas.
- Admin pode consultar clientes.
- Operacao de transferencia exige usuario autenticado.
- Operacao administrativa exige perfil especifico.

## Perfis

Perfis planejados:

- `Cliente`
- `Atendente`
- `Gerente`
- `Admin`

## Dados sensiveis

Dados que exigem cuidado:

- CPF.
- Email.
- Telefone.
- Saldo.
- Numero da conta.
- Token JWT.
- Senha.

## Regras de protecao

### Senha

Nunca salvar senha pura.

Quando criarmos tabela de usuarios, usar:

- Hash.
- Salt.
- Algoritmo seguro.

No .NET, podemos usar recursos do ASP.NET Core Identity ou uma estrategia simplificada para treino.

### Token

JWT deve ter:

- Identificador do usuario.
- Perfil.
- Tempo de expiracao.
- Assinatura.

### Logs

Nao logar:

- Senha.
- Token.
- CPF completo.
- Dados bancarios sensiveis completos.

### Erros

Nao devolver erro interno cru para o usuario.

Errado:

```text
SQL exception at line...
```

Certo:

```json
{
  "message": "Nao foi possivel processar a solicitacao."
}
```

## Endpoints protegidos

Publicos:

- `POST /auth/login`
- `GET /health`

Protegidos:

- `GET /clientes/me`
- `GET /contas`
- `GET /contas/{id}/extrato`
- `POST /transferencias`
- `GET /emprestimos`

Administrativos:

- `GET /clientes`
- `GET /auditoria`

## Checklist de seguranca

- JWT configurado.
- Senha fora do codigo.
- String de conexao fora do codigo.
- Endpoints sensiveis com autorizacao.
- Logs sem dados sensiveis.
- Transferencia com transacao SQL.
- Auditoria para operacoes importantes.
