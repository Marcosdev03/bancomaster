# Roadmap

## Fase 1: Documentacao

Status: em andamento.

Entregas:

- Visao geral.
- Arquitetura.
- Banco de dados.
- Seguranca.
- APIs.
- Kafka.
- Kubernetes.
- DB2.
- Snowflake.
- z/OS Connect.

## Fase 2: API minima

Criar projeto .NET 8.

Entregas:

- `GET /health`
- Swagger.
- Conexao com SQL Server.
- Endpoint de clientes.
- Endpoint de contas.
- Endpoint de extrato.

## Fase 3: Autenticacao

Entregas:

- Login simulado.
- JWT.
- Protecao de endpoints.
- Perfis.

## Fase 4: Operacoes bancarias

Entregas:

- Transferencia simulada.
- Validacao de saldo.
- Transacao SQL.
- Registro em `Transacoes`.
- Registro em `LogAuditoria`.

## Fase 5: Kafka

Entregas:

- Producer na API.
- Topicos de eventos.
- Consumer de auditoria.
- Consumer de notificacao simulada.

## Fase 6: Kubernetes

Entregas:

- Dockerfile.
- Manifests.
- ConfigMap.
- Secret.
- Deploy local com Kind.

## Fase 7: DB2

Entregas:

- Subir DB2 com Docker.
- Criar base DB2.
- Comparar consultas SQL Server e DB2.
- Simular legado.

## Fase 8: Snowflake e z/OS Connect

Entregas:

- Documentar fluxo analitico.
- Documentar integracao legado.
- Criar simulacao simples de API legada.

## Ordem recomendada agora

1. Revisar documentacao.
2. Criar API .NET 8.
3. Conectar API no SQL Server.
4. Criar endpoints de leitura.
5. Adicionar autenticacao.
6. Adicionar transferencia.
7. Adicionar Kafka.
8. Preparar Kubernetes.

