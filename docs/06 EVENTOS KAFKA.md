# Eventos Kafka

## Papel do Kafka

Kafka sera usado para simular comunicacao por eventos.

Em vez de a API fazer tudo sozinha, ela publica eventos para outros processos reagirem.

## Topicos planejados

### `cliente.autenticado`

Publicado quando um cliente faz login.

Exemplo:

```json
{
  "eventoId": "uuid",
  "clienteId": 1,
  "dataEvento": "2026-05-19T12:00:00Z",
  "canal": "API"
}
```

### `conta.consultada`

Publicado quando uma conta e consultada.

Exemplo:

```json
{
  "eventoId": "uuid",
  "clienteId": 1,
  "contaId": 1,
  "dataEvento": "2026-05-19T12:01:00Z"
}
```

### `transferencia.solicitada`

Publicado quando uma transferencia e solicitada.

### `transferencia.concluida`

Publicado quando a transferencia termina com sucesso.

Exemplo:

```json
{
  "eventoId": "uuid",
  "contaOrigemId": 1,
  "contaDestinoId": 2,
  "valor": 100.50,
  "status": "Concluida",
  "dataEvento": "2026-05-19T12:02:00Z"
}
```

### `auditoria.registrada`

Publicado quando algo sensivel e auditado.

## Consumers planejados

### Auditoria Consumer

Le eventos e grava em `LogAuditoria`.

### Notificacao Consumer

Simula envio de notificacao.

No inicio, pode apenas escrever no console.

## Regras de evento

- Evento deve ter identificador unico.
- Evento deve ter data.
- Evento deve ter versao.
- Evento nao deve carregar senha.
- Evento nao deve carregar token.
- Evento deve evitar CPF completo.

## Comandos Kafka

Iniciar Kafka:

```powershell
powershell -ExecutionPolicy Bypass -File D:\DevStack\kafka\iniciar_kafka.ps1
```

Criar topico:

```powershell
$env:JAVA_HOME = "D:\DevStack\tools\jdk-17.0.19+10"
D:\DevStack\kafka\kafka_2.13-4.1.1\bin\windows\kafka-topics.bat --bootstrap-server localhost:9092 --create --topic transferencia.concluida --partitions 3 --replication-factor 1
```

Consumir:

```powershell
$env:JAVA_HOME = "D:\DevStack\tools\jdk-17.0.19+10"
D:\DevStack\kafka\kafka_2.13-4.1.1\bin\windows\kafka-console-consumer.bat --bootstrap-server localhost:9092 --topic transferencia.concluida --from-beginning
```

