# Azure AKS

## Objetivo

Subir a `BancoDigital.Api` no Azure Kubernetes Service usando Azure Container Registry.

Fluxo:

```text
GitHub
  -> Azure Container Registry
  -> imagem bancodigital-api:v1
  -> Azure Kubernetes Service
  -> Service publico
```

## Recursos Azure

Resource group:

```text
rg-banco-digital-dev
```

Regiao:

```text
Canada Central
```

Azure Container Registry:

```text
acrbancodigitaldev
```

AKS:

```text
aks-banco-digital-dev
```

Namespace Kubernetes:

```text
banco-digital
```

## Passo 1: preparar o GitHub

Antes de criar a imagem, commitar e enviar a versao atual da API:

```powershell
git status
git add .
git commit -m "Prepara API para deploy no AKS"
git push
```

## Passo 2: criar o ACR

No Azure Cloud Shell:

```bash
az acr create \
  --resource-group rg-banco-digital-dev \
  --name acrbancodigitaldev \
  --sku Basic \
  --location canadacentral
```

Se o nome nao estiver disponivel, usar outro nome globalmente unico e depois atualizar a imagem em `k8s/deployment.yaml`.

## Passo 3: criar o AKS

```bash
az aks create \
  --resource-group rg-banco-digital-dev \
  --name aks-banco-digital-dev \
  --location canadacentral \
  --node-count 1 \
  --node-vm-size Standard_B2s \
  --attach-acr acrbancodigitaldev \
  --generate-ssh-keys
```

Se a assinatura bloquear `Standard_B2s`, tentar:

```bash
az aks create \
  --resource-group rg-banco-digital-dev \
  --name aks-banco-digital-dev \
  --location canadacentral \
  --node-count 1 \
  --node-vm-size Standard_B2als_v2 \
  --attach-acr acrbancodigitaldev \
  --generate-ssh-keys
```

## Passo 4: conectar kubectl no AKS

```bash
az aks get-credentials \
  --resource-group rg-banco-digital-dev \
  --name aks-banco-digital-dev
```

Validar:

```bash
kubectl get nodes
```

## Passo 5: criar a imagem no ACR

No Cloud Shell, clonar o repositorio e entrar na pasta do projeto:

```bash
git clone git@github.com:Marcosdev03/bancomaster.git
cd bancomaster
```

Se o SSH nao estiver configurado no Cloud Shell, usar HTTPS:

```bash
git clone https://github.com/Marcosdev03/bancomaster.git
cd bancomaster
```

Build no ACR:

```bash
az acr build \
  --registry acrbancodigitaldev \
  --image bancodigital-api:v1 \
  .
```

## Passo 6: criar o Secret da API

O primeiro deploy precisa da chave JWT, porque a API nao inicia sem `Jwt:SigningKey`.

```bash
kubectl apply -f k8s/namespace.yaml

kubectl create secret generic bancodigital-api-secrets \
  --namespace banco-digital \
  --from-literal=Jwt__SigningKey='trocar-por-uma-chave-forte-com-32-ou-mais-caracteres'
```

Nao commitar senha, token ou connection string.

## Passo 7: subir a API

```bash
kubectl apply -f k8s/deployment.yaml
kubectl apply -f k8s/service.yaml
```

Verificar:

```bash
kubectl get pods -n banco-digital
kubectl get service -n banco-digital
```

Quando o service mostrar `EXTERNAL-IP`, testar:

```bash
curl http://EXTERNAL-IP/health/live
```

Resultado esperado:

```json
{"status":"healthy"}
```

## Passo 8: configurar banco depois

Depois do primeiro deploy, adicionar a connection string do Azure SQL no Secret:

```bash
kubectl create secret generic bancodigital-api-secrets \
  --namespace banco-digital \
  --from-literal=Jwt__SigningKey='trocar-por-uma-chave-forte-com-32-ou-mais-caracteres' \
  --from-literal=ConnectionStrings__BancoTreino='Server=tcp:sql-banco-digital-dev.database.windows.net,1433;Initial Catalog=BancoTreino;Persist Security Info=False;User ID=sqladmin;Password=<senha>;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;' \
  --dry-run=client \
  -o yaml | kubectl apply -f -
```

Reiniciar o pod para recarregar o Secret:

```bash
kubectl rollout restart deployment/bancodigital-api -n banco-digital
kubectl rollout status deployment/bancodigital-api -n banco-digital
```

Validar readiness:

```bash
curl http://EXTERNAL-IP/health/ready
```

## Comandos uteis

Ver logs:

```bash
kubectl logs -n banco-digital deployment/bancodigital-api
```

Descrever pod:

```bash
kubectl describe pod -n banco-digital -l app=bancodigital-api
```

Atualizar imagem depois de novo build:

```bash
az acr build \
  --registry acrbancodigitaldev \
  --image bancodigital-api:v2 \
  .

kubectl set image deployment/bancodigital-api \
  bancodigital-api=acrbancodigitaldev.azurecr.io/bancodigital-api:v2 \
  -n banco-digital
```
