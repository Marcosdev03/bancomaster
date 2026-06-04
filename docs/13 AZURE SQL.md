# Azure SQL

## Objetivo

Subir a base de treino `BancoTreino` para o Azure SQL Database, mantendo a estrutura e os dados do banco local.

Fluxo usado:

```text
SQL Server local
BancoTreino
   -> exportacao .bacpac
Azure Storage Account
   -> upload do .bacpac
Azure SQL Database
   -> importacao do banco
DBeaver
   -> validacao dos dados
```

## Recursos criados no Azure

Resource group:

```text
rg-banco-digital-dev
```

Servidor Azure SQL:

```text
sql-banco-digital-dev.database.windows.net
```

Banco de dados:

```text
BancoTreino
```

Regiao:

```text
Canada Central
```

Storage Account usado para importacao:

```text
stbancodigitaldev_1780515959538
```

Container:

```text
bacpac
```

Arquivo importado:

```text
basedados.bacpac
```

## Configuracao usada

Banco Azure SQL:

```text
Camada: Basico
Armazenamento: 2 GB
Autenticacao: SQL Server Authentication
Usuario administrador: sqladmin
```

Storage Account:

```text
Regiao: Canada Central
Desempenho: Standard
Redundancia: LRS
Acesso anonimo ao blob: desabilitado
TLS minimo: 1.2
```

Firewall do servidor SQL:

```text
Acesso publico: redes selecionadas
IP do cliente local: liberado
Permitir acesso de servicos Azure: habilitado temporariamente para importacao
```

## Conexao pelo DBeaver

Tipo de banco:

```text
SQL Server
```

Configuracao:

```text
Host: sql-banco-digital-dev.database.windows.net
Porta: 1433
Banco de dados: BancoTreino
Autenticacao: SQL Server Authentication
Usuario: sqladmin
Senha: senha do administrador do Azure SQL
```

Se houver erro de SSL/TLS, conferir as propriedades do driver:

```text
encrypt=true
trustServerCertificate=false
hostNameInCertificate=*.database.windows.net
```

## Validacoes executadas

Total de clientes:

```sql
SELECT COUNT(*) AS TotalClientes
FROM dbo.Clientes;
```

Resultado esperado:

```text
1200
```

Amostra de clientes:

```sql
SELECT TOP 10
    ClienteId,
    Nome,
    Cidade,
    Estado,
    StatusCliente
FROM dbo.Clientes
ORDER BY ClienteId;
```

Resultado validado:

```text
Clientes 1 a 10 retornados corretamente.
```

## Cuidados

- Nao salvar senha do Azure SQL no Git.
- Nao colocar connection string com senha em `appsettings.json`.
- Usar User Secrets no desenvolvimento local.
- Usar Secret no Kubernetes.
- Manter firewall liberado apenas para IPs necessarios.
- Desabilitar acesso amplo de servicos Azure quando nao for necessario.
- Monitorar custo do banco e do Storage Account.

## Proximo passo

Configurar a API para acessar o Azure SQL usando User Secrets.

Exemplo de chave esperada pela API:

```text
ConnectionStrings:BancoTreino
```

Exemplo do formato da connection string, sem salvar em arquivo versionado:

```text
Server=tcp:sql-banco-digital-dev.database.windows.net,1433;Initial Catalog=BancoTreino;Persist Security Info=False;User ID=sqladmin;Password=<senha>;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;
```
