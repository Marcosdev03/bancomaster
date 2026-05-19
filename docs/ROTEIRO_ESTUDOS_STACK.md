# Roteiro de estudos da stack

## SQL Server

1. Consultas `SELECT`, `WHERE`, `ORDER BY`, `TOP`.
2. Relacionamentos com `JOIN`.
3. Agrupamentos com `GROUP BY` e `HAVING`.
4. Subqueries, CTEs e views.
5. Procedures, indices e plano de execucao.
6. Transacoes com `BEGIN TRAN`, `COMMIT` e `ROLLBACK`.

## .NET 6, .NET 7 e .NET 8

1. Criar uma Web API em C#.
2. Conectar no SQL Server com Entity Framework Core.
3. Criar endpoints para clientes, contas e transacoes.
4. Separar camadas: API, dominio, infraestrutura.
5. Entender diferencas de LTS: .NET 6 e .NET 8 sao linhas LTS; .NET 7 foi linha STS.

## Apache Kafka

1. Conceitos: broker, topic, partition, consumer group.
2. Produzir evento de transacao bancaria.
3. Consumir evento e gravar log/auditoria.
4. Entender retry, offset e idempotencia.

## Kubernetes

1. Conceitos: pod, deployment, service, configmap, secret.
2. Rodar uma API .NET containerizada.
3. Expor a API via service.
4. Configurar variaveis de ambiente para conexao com SQL Server.

## Snowflake

Leitura inicial:

- O que e um data warehouse em nuvem.
- Diferenca entre database, schema, warehouse e stage.
- Como dados saem de sistemas transacionais e viram base analitica.
- Conceitos de ELT, clustering, roles e warehouses virtuais.

## z/OS Connect API

Leitura inicial:

- z/OS Connect expoe programas e dados de mainframe como APIs REST.
- COBOL/CICS pode ser encapsulado por uma camada de API.
- Conceitos importantes: service archive, API requester, API provider, OpenAPI e mapping.
- Objetivo pratico: entender como sistemas legados entram em arquiteturas modernas.
