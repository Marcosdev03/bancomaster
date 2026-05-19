# DB2 Legado

## Papel do DB2

DB2 entra no projeto como estudo de ambiente legado/corporativo.

Muitos bancos usam tecnologias IBM, mainframe, COBOL e DB2. Mesmo quando a API moderna esta em .NET, pode existir um sistema legado por tras.

## Como vamos usar no treino

DB2 tera uma base parecida com a do SQL Server.

Objetivo:

- Comparar sintaxe SQL Server vs DB2.
- Entender limitacoes e diferencas.
- Simular consulta a legado.

## Possiveis cenarios

### Cenario 1: consulta direta

API consulta SQL Server como principal e DB2 como legado.

### Cenario 2: sincronizacao

SQL Server publica evento no Kafka, e um consumer atualiza DB2.

### Cenario 3: legado via API

API moderna chama uma API legada que representa z/OS Connect.

## Pasta DB2

```text
D:\DevStack\db2
```

Arquivos:

- `manual_comandos_db2.md`
- `docker-compose.yml`
- `scripts\01_criar_banco_treino_bancario_db2.sql`
- `scripts\02_consultas_treino_db2.sql`

## Observacao

DB2 depende de Docker Desktop/WSL para subir via container neste computador.

