# Manual de autenticacao de treino

Este manual explica a camada de login criada em cima da base `BancoTreino`.

## Objetivo

Associar clientes existentes a usuarios de login.

Cada cliente recebeu um usuario.

As senhas nao foram salvas em texto puro. O banco guarda:

- `SenhaHash`
- `SenhaSalt`

## Script

Arquivo:

`D:\DevStack\sqlserver\scripts\02_criar_autenticacao_treino.sql`

Para recriar a camada de autenticacao:

```powershell
& "C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\170\Tools\Binn\SQLCMD.EXE" -S ".\SQLEXPRESS" -E -i "D:\DevStack\sqlserver\scripts\02_criar_autenticacao_treino.sql" -b
```

## Tabelas criadas

### Usuarios

Guarda os usuarios de acesso.

Campos importantes:

- `UsuarioId`
- `ClienteId`
- `Login`
- `Email`
- `SenhaHash`
- `SenhaSalt`
- `Ativo`
- `TentativasFalhas`
- `BloqueadoAte`
- `UltimoLogin`

### Perfis

Guarda os perfis de acesso.

Perfis criados:

- `Cliente`
- `Atendente`
- `Gerente`
- `Admin`

### UsuarioPerfis

Liga usuarios aos perfis.

### LoginAuditoria

Registra tentativas de login.

## Logins de clientes

Formato:

```text
cliente000001
cliente000002
cliente000003
...
```

Senha padrao de treino para clientes:

```text
Treino@123
```

Exemplo:

```text
Login: cliente000001
Senha: Treino@123
```

## Logins administrativos de treino

Admin:

```text
Login: admin.treino
Senha: Admin@123
```

Gerente:

```text
Login: gerente.treino
Senha: Gerente@123
```

Atendente:

```text
Login: atendente.treino
Senha: Atendente@123
```

## Testar login no SQL

```sql
USE BancoTreino;
GO

EXEC dbo.sp_ValidarLoginTreino
    @Login = 'cliente000001',
    @Senha = 'Treino@123';
GO
```

Login invalido:

```sql
EXEC dbo.sp_ValidarLoginTreino
    @Login = 'cliente000001',
    @Senha = 'senhaerrada';
GO
```

## Ver usuarios

```sql
SELECT TOP 50 *
FROM dbo.vw_UsuariosTreino
ORDER BY UsuarioId;
GO
```

## Ver auditoria de login

```sql
SELECT TOP 50 *
FROM dbo.LoginAuditoria
ORDER BY DataTentativa DESC;
GO
```

## Regras criadas

- Usuario inativo nao faz login.
- Senha incorreta aumenta `TentativasFalhas`.
- Depois de 5 erros, usuario fica bloqueado por 15 minutos.
- Login correto zera tentativas falhas.
- Login correto atualiza `UltimoLogin`.
- Toda tentativa gera registro em `LoginAuditoria`.

## Como a API vai usar isso depois

A API pode chamar a procedure:

```sql
EXEC dbo.sp_ValidarLoginTreino
    @Login = @login,
    @Senha = @senha;
```

Se retornar `Sucesso = 1`, a API cria um JWT com:

- `UsuarioId`
- `ClienteId`
- `Login`
- `Perfis`

Depois os endpoints usam esse token para autorizar o usuario.

## Observacao de seguranca

Esta estrutura e boa para treino, mas em producao a senha deve ser tratada na aplicacao com algoritmos proprios para senha, como PBKDF2, bcrypt ou Argon2.

Aqui usamos hash com salt para evitar senha pura no banco e permitir o treino antes da API ficar pronta.
