# Visao Geral

## Ideia do projeto

Vamos criar uma aplicacao que simula um banco digital.

A base de dados ja existe no SQL Server com clientes, contas, cartoes, PIX, transacoes e emprestimos. Agora vamos construir um ecossistema em volta dela para treinar como uma stack corporativa funciona.

## O que a aplicacao deve simular

O usuario entra na aplicacao, autentica, consulta dados bancarios e executa operacoes simuladas.

Exemplos:

- Login.
- Ver perfil do cliente.
- Ver contas.
- Ver saldo.
- Ver extrato.
- Simular PIX ou transferencia.
- Consultar emprestimos.
- Gerar eventos de auditoria.
- Publicar eventos no Kafka.
- Preparar deploy em Kubernetes.

## Por que esse projeto e bom para treino

Ele mistura assuntos que aparecem muito em empresas:

- SQL em banco relacional.
- APIs REST.
- Autenticacao.
- Autorizacao.
- Transacoes bancarias.
- Mensageria/eventos.
- Deploy.
- Logs.
- Observabilidade.
- Legado.
- Analytics.

## Stacks cobertas

### SQL Server

Banco transacional principal.

Usado para:

- Clientes.
- Contas.
- Transacoes.
- Emprestimos.
- Auditoria.

### DBeaver

Editor SQL usado para consultar, explorar e testar comandos.

Usado para:

- Rodar `SELECT`.
- Criar consultas.
- Ver tabelas.
- Entender relacionamentos.

### .NET

Camada de aplicacao.

Usado para:

- Criar API.
- Fazer regras de negocio.
- Conectar no SQL Server.
- Publicar eventos no Kafka.

### Kafka

Camada de eventos.

Usado para:

- Evento de transferencia criada.
- Evento de login realizado.
- Evento de transacao aprovada.
- Evento de auditoria.

### Kubernetes

Camada de deploy.

Usado para:

- Rodar API em container.
- Configurar variaveis.
- Expor servico.
- Simular ambiente corporativo.

### DB2

Banco paralelo para estudar stack usada em ambiente legado/mainframe.

Usado para:

- Comparar SQL Server com DB2.
- Treinar sintaxe DB2.
- Entender sistemas bancarios legados.

### Snowflake

Camada conceitual de analytics.

Usado para entender:

- Data warehouse.
- Dados historicos.
- BI.
- ETL/ELT.

### z/OS Connect API

Camada conceitual de integracao com mainframe.

Usado para entender:

- COBOL expondo API.
- Mainframe integrado com REST.
- Modernizacao de legado.

## Resultado esperado

Ao final, voce tera:

- Uma base bancaria.
- Uma API bancaria.
- Um fluxo com Kafka.
- Documentacao de arquitetura.
- Scripts de deploy.
- Material para explicar o projeto em entrevista ou no trabalho.

