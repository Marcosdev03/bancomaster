# Kubernetes

## Papel do Kubernetes

Kubernetes sera usado para simular deploy corporativo.

No projeto, ele vai ensinar:

- Deployment.
- Service.
- ConfigMap.
- Secret.
- Variaveis de ambiente.
- Health checks.
- Escala.

## Componentes planejados

```text
k8s/
  api-deployment.yaml
  api-service.yaml
  api-configmap.yaml
  api-secret.yaml
  auditoria-consumer-deployment.yaml
```

## API

Deployment:

- Imagem da API.
- Porta 8080.
- Variaveis de ambiente.
- Health check.

Service:

- Expor API dentro do cluster.

## ConfigMap

Configuracoes nao sensiveis:

- Nome do ambiente.
- Endereco Kafka.
- Nome do banco.

## Secret

Configuracoes sensiveis:

- Senha do SQL Server.
- Chave JWT.

## Dependencia do Docker

Para rodar localmente com Kind, precisa Docker Desktop.

Comandos:

```powershell
D:\DevStack\kubernetes\kind.exe create cluster --name banco-digital
D:\DevStack\kubernetes\kubectl.exe get nodes
```

## Ordem futura

1. Criar API.
2. Criar Dockerfile.
3. Gerar imagem local.
4. Criar manifests Kubernetes.
5. Subir com Kind.
6. Testar endpoint `/health`.

