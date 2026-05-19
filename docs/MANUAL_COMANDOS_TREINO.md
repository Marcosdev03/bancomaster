# Manual de comandos para treino

Este manual fica junto do seu ambiente de estudos em `D:\DevStack`.

## 1. Abrir o ambiente

Abra o PowerShell e rode:

```powershell
powershell -ExecutionPolicy Bypass -File D:\DevStack\abrir_ambiente.ps1
```

Esse comando carrega os caminhos do .NET, Java, Kafka e Kubernetes para o terminal atual.

## 2. Entrar no SQL Server pelo terminal

Conectar usando login do Windows:

```powershell
& "C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\170\Tools\Binn\SQLCMD.EXE" -S ".\SQLEXPRESS" -E
```

Conectar usando usuario `sa`:

```powershell
& "C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\170\Tools\Binn\SQLCMD.EXE" -S ".\SQLEXPRESS" -U sa -P "Treino@123456"
```

Conectar direto no banco `BancoTreino`:

```powershell
& "C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\170\Tools\Binn\SQLCMD.EXE" -S ".\SQLEXPRESS" -U sa -P "Treino@123456" -d BancoTreino
```

Dentro do `sqlcmd`, todo comando SQL precisa terminar com:

```sql
GO
```

Para sair:

```sql
EXIT
```

## 3. Conferir se o banco esta online

```sql
SELECT name, state_desc
FROM sys.databases
WHERE name = 'BancoTreino';
GO
```

## 4. Ver quantidade de dados

```sql
USE BancoTreino;
GO

SELECT 'Clientes' AS Entidade, COUNT(*) AS Total FROM dbo.Clientes
UNION ALL SELECT 'Contas', COUNT(*) FROM dbo.Contas
UNION ALL SELECT 'Cartoes', COUNT(*) FROM dbo.Cartoes
UNION ALL SELECT 'ChavesPix', COUNT(*) FROM dbo.ChavesPix
UNION ALL SELECT 'Transacoes', COUNT(*) FROM dbo.Transacoes
UNION ALL SELECT 'Emprestimos', COUNT(*) FROM dbo.Emprestimos
UNION ALL SELECT 'ParcelasEmprestimo', COUNT(*) FROM dbo.ParcelasEmprestimo;
GO
```

## 5. Consultas basicas para treinar

Listar clientes:

```sql
SELECT TOP 20
    ClienteId,
    Nome,
    CPF,
    Email,
    RendaMensal,
    ScoreCredito,
    StatusCliente
FROM dbo.Clientes
ORDER BY ClienteId;
GO
```

Filtrar clientes ativos:

```sql
SELECT TOP 50
    Nome,
    RendaMensal,
    ScoreCredito,
    Cidade,
    Estado
FROM dbo.Clientes
WHERE StatusCliente = 'Ativo'
ORDER BY ScoreCredito DESC;
GO
```

Buscar clientes com renda maior:

```sql
SELECT
    Nome,
    RendaMensal,
    ScoreCredito
FROM dbo.Clientes
WHERE RendaMensal >= 10000
ORDER BY RendaMensal DESC;
GO
```

## 6. Treinar JOIN

Clientes com contas:

```sql
SELECT TOP 50
    c.ClienteId,
    c.Nome,
    co.NumeroConta,
    co.TipoConta,
    co.SaldoAtual,
    co.StatusConta
FROM dbo.Clientes c
JOIN dbo.Contas co ON co.ClienteId = c.ClienteId
ORDER BY c.ClienteId;
GO
```

Clientes, contas e agencias:

```sql
SELECT TOP 50
    c.Nome AS Cliente,
    co.NumeroConta,
    co.TipoConta,
    a.Nome AS Agencia,
    a.Cidade,
    a.Estado
FROM dbo.Clientes c
JOIN dbo.Contas co ON co.ClienteId = c.ClienteId
JOIN dbo.Agencias a ON a.AgenciaId = co.AgenciaId
ORDER BY c.Nome;
GO
```

## 7. Treinar GROUP BY

Total de clientes por estado:

```sql
SELECT
    Estado,
    COUNT(*) AS TotalClientes
FROM dbo.Clientes
GROUP BY Estado
ORDER BY TotalClientes DESC;
GO
```

Saldo total por tipo de conta:

```sql
SELECT
    TipoConta,
    COUNT(*) AS TotalContas,
    SUM(SaldoAtual) AS SaldoTotal,
    AVG(SaldoAtual) AS SaldoMedio
FROM dbo.Contas
GROUP BY TipoConta
ORDER BY SaldoTotal DESC;
GO
```

Movimentacao por tipo de transacao:

```sql
SELECT
    TipoTransacao,
    Sinal,
    COUNT(*) AS TotalTransacoes,
    SUM(Valor) AS ValorTotal
FROM dbo.Transacoes
GROUP BY TipoTransacao, Sinal
ORDER BY ValorTotal DESC;
GO
```

## 8. Treinar HAVING

Clientes com mais de uma conta:

```sql
SELECT
    c.ClienteId,
    c.Nome,
    COUNT(co.ContaId) AS TotalContas
FROM dbo.Clientes c
JOIN dbo.Contas co ON co.ClienteId = c.ClienteId
GROUP BY c.ClienteId, c.Nome
HAVING COUNT(co.ContaId) > 1
ORDER BY TotalContas DESC;
GO
```

## 9. Treinar datas

Transacoes dos ultimos 30 dias:

```sql
SELECT TOP 100
    ContaId,
    DataTransacao,
    TipoTransacao,
    Canal,
    Valor,
    Sinal,
    StatusTransacao
FROM dbo.Transacoes
WHERE DataTransacao >= DATEADD(day, -30, SYSDATETIME())
ORDER BY DataTransacao DESC;
GO
```

Clientes cadastrados por mes:

```sql
SELECT
    YEAR(DataCadastro) AS Ano,
    MONTH(DataCadastro) AS Mes,
    COUNT(*) AS TotalClientes
FROM dbo.Clientes
GROUP BY YEAR(DataCadastro), MONTH(DataCadastro)
ORDER BY Ano DESC, Mes DESC;
GO
```

## 10. Usar a view pronta

```sql
SELECT TOP 50
    ClienteId,
    Nome,
    ScoreCredito,
    TotalContas,
    SaldoTotal,
    TotalCartoes,
    TotalEmprestimos
FROM dbo.vw_ResumoCliente
ORDER BY SaldoTotal DESC;
GO
```

## 11. Usar a procedure de extrato

Extrato completo da conta 1:

```sql
EXEC dbo.sp_ExtratoConta @ContaId = 1;
GO
```

Extrato por periodo:

```sql
EXEC dbo.sp_ExtratoConta
    @ContaId = 1,
    @DataInicio = '2026-01-01',
    @DataFim = '2026-12-31';
GO
```

## 12. Treinar transacao com ROLLBACK

Este treino altera dados e desfaz no final.

```sql
BEGIN TRAN;

UPDATE dbo.Contas
SET SaldoAtual = SaldoAtual - 100
WHERE ContaId = 1;

SELECT ContaId, SaldoAtual
FROM dbo.Contas
WHERE ContaId = 1;

ROLLBACK;

SELECT ContaId, SaldoAtual
FROM dbo.Contas
WHERE ContaId = 1;
GO
```

## 13. Treinar transacao com COMMIT

Este treino grava de verdade. Use apenas quando quiser manter a alteracao.

```sql
BEGIN TRAN;

UPDATE dbo.Contas
SET SaldoAtual = SaldoAtual + 50
WHERE ContaId = 1;

COMMIT;

SELECT ContaId, SaldoAtual
FROM dbo.Contas
WHERE ContaId = 1;
GO
```

## 14. Ver indices criados

```sql
SELECT
    t.name AS Tabela,
    i.name AS Indice,
    i.type_desc AS Tipo
FROM sys.indexes i
JOIN sys.tables t ON t.object_id = i.object_id
WHERE t.is_ms_shipped = 0
ORDER BY t.name, i.name;
GO
```

## 15. Ver tamanho dos arquivos do banco

```sql
SELECT
    DB_NAME(database_id) AS Banco,
    name AS ArquivoLogico,
    physical_name AS ArquivoFisico,
    size * 8 / 1024 AS TamanhoMB
FROM sys.master_files
WHERE DB_NAME(database_id) = 'BancoTreino';
GO
```

## 16. Refazer a base do zero

Use quando quiser resetar todos os dados.

```powershell
& "C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\170\Tools\Binn\SQLCMD.EXE" -S ".\SQLEXPRESS" -E -i "D:\DevStack\sqlserver\scripts\01_criar_banco_treino_bancario.sql" -b
```

## 17. Criar projeto .NET 8 Web API

```powershell
cd D:\DevStack
mkdir projetos
cd projetos
D:\DevStack\dotnet\dotnet.exe new webapi -n BancoTreino.Api -f net8.0
cd BancoTreino.Api
D:\DevStack\dotnet\dotnet.exe run
```

## 18. Criar projeto .NET 6 Web API

```powershell
cd D:\DevStack\projetos
D:\DevStack\dotnet\dotnet.exe new webapi -n BancoTreino.Net6.Api -f net6.0
cd BancoTreino.Net6.Api
D:\DevStack\dotnet\dotnet.exe run
```

## 19. Adicionar pacote SQL Server no .NET

Dentro da pasta do projeto:

```powershell
D:\DevStack\dotnet\dotnet.exe add package Microsoft.Data.SqlClient
```

Para Entity Framework Core com SQL Server:

```powershell
D:\DevStack\dotnet\dotnet.exe add package Microsoft.EntityFrameworkCore.SqlServer
D:\DevStack\dotnet\dotnet.exe add package Microsoft.EntityFrameworkCore.Design
```

String de conexao para usar nos estudos:

```text
Server=localhost\SQLEXPRESS;Database=BancoTreino;User Id=sa;Password=Treino@123456;TrustServerCertificate=True;
```

## 20. Ver versoes do .NET

```powershell
D:\DevStack\dotnet\dotnet.exe --list-sdks
```

## 21. Ver versao do Java

```powershell
D:\DevStack\tools\jdk-17.0.19+10\bin\java.exe -version
```

## 22. Ver versao do Kafka

```powershell
$env:JAVA_HOME = "D:\DevStack\tools\jdk-17.0.19+10"
D:\DevStack\kafka\kafka_2.13-4.1.1\bin\windows\kafka-topics.bat --version
```

## 23. Iniciar Kafka local

```powershell
powershell -ExecutionPolicy Bypass -File D:\DevStack\kafka\iniciar_kafka.ps1
```

Deixe esse terminal aberto enquanto estiver usando o Kafka.

## 24. Criar topico no Kafka

Abra outro PowerShell e rode:

```powershell
$env:JAVA_HOME = "D:\DevStack\tools\jdk-17.0.19+10"
D:\DevStack\kafka\kafka_2.13-4.1.1\bin\windows\kafka-topics.bat --bootstrap-server localhost:9092 --create --topic transacoes-bancarias --partitions 3 --replication-factor 1
```

## 25. Listar topicos Kafka

```powershell
$env:JAVA_HOME = "D:\DevStack\tools\jdk-17.0.19+10"
D:\DevStack\kafka\kafka_2.13-4.1.1\bin\windows\kafka-topics.bat --bootstrap-server localhost:9092 --list
```

## 26. Produzir mensagens Kafka

```powershell
$env:JAVA_HOME = "D:\DevStack\tools\jdk-17.0.19+10"
D:\DevStack\kafka\kafka_2.13-4.1.1\bin\windows\kafka-console-producer.bat --bootstrap-server localhost:9092 --topic transacoes-bancarias
```

Depois digite mensagens como:

```json
{"contaId":1,"tipo":"PIX","valor":150.75}
{"contaId":2,"tipo":"TED","valor":500.00}
```

Para sair do produtor, pressione `Ctrl+C`.

## 27. Consumir mensagens Kafka

```powershell
$env:JAVA_HOME = "D:\DevStack\tools\jdk-17.0.19+10"
D:\DevStack\kafka\kafka_2.13-4.1.1\bin\windows\kafka-console-consumer.bat --bootstrap-server localhost:9092 --topic transacoes-bancarias --from-beginning
```

## 28. Ver versao do Kubernetes

```powershell
D:\DevStack\kubernetes\kubectl.exe version --client=true
```

## 29. Ver versao do Kind

```powershell
D:\DevStack\kubernetes\kind.exe version
```

## 30. Criar cluster Kubernetes com Kind

Este comando so funciona se o Docker Desktop estiver instalado e rodando.

```powershell
D:\DevStack\kubernetes\kind.exe create cluster --name treino
```

Ver clusters:

```powershell
D:\DevStack\kubernetes\kind.exe get clusters
```

Ver nodes:

```powershell
D:\DevStack\kubernetes\kubectl.exe get nodes
```

Excluir cluster:

```powershell
D:\DevStack\kubernetes\kind.exe delete cluster --name treino
```

## 31. Comandos uteis no SQL Server Management Studio

Abra o SSMS e use:

```text
Server name: localhost\SQLEXPRESS
Authentication: SQL Server Authentication
Login: sa
Password: Treino@123456
```

Depois selecione o banco `BancoTreino` e rode as consultas deste manual.

## 32. Abrir o DBeaver

```powershell
powershell -ExecutionPolicy Bypass -File D:\DevStack\abrir_dbeaver.ps1
```

Ou abra direto:

```powershell
D:\DevStack\dbeaver\dbeaver\dbeaver.exe
```

## 33. Criar conexao SQL Server no DBeaver

No DBeaver:

1. Clique em `New Database Connection`.
2. Escolha `SQL Server`.
3. Preencha:

```text
Host: localhost
Port: 1433
Database: BancoTreino
User: sa
Password: Treino@123456
```

Se aparecer campo de instancia, use:

```text
SQLEXPRESS
```

Se pedir para baixar driver, aceite.

Depois clique em `Test Connection`.

## 34. Abrir editor SQL no DBeaver

Depois de conectar:

1. Clique com o botao direito na conexao.
2. Escolha `SQL Editor`.
3. Escolha `Open SQL Script`.
4. Escreva e execute comandos como:

```sql
SELECT TOP 20 *
FROM dbo.Clientes;
```

Para executar, use o botao de executar no topo do editor ou o atalho mostrado pelo proprio DBeaver.

## 35. Sugestao de treino diario

Dia 1:

- `SELECT`, `WHERE`, `ORDER BY`, `TOP`

Dia 2:

- `JOIN` entre clientes, contas e agencias

Dia 3:

- `GROUP BY`, `HAVING`, agregacoes

Dia 4:

- Procedures, views e datas

Dia 5:

- Transacoes com `BEGIN TRAN`, `COMMIT`, `ROLLBACK`

Dia 6:

- Criar API .NET conectando no SQL Server

Dia 7:

- Kafka: criar topico, produzir e consumir mensagens

Dia 8:

- Kubernetes: ler comandos, criar cluster com Kind se Docker estiver pronto
