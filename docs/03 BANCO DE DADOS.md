# Banco De Dados

## Banco principal

SQL Server:

```text
Servidor: localhost\SQLEXPRESS
Banco: BancoTreino
```

## Tabelas existentes

- `Agencias`
- `Clientes`
- `Funcionarios`
- `Contas`
- `Cartoes`
- `ChavesPix`
- `Transacoes`
- `Emprestimos`
- `ParcelasEmprestimo`
- `LogAuditoria`

## Papel de cada tabela

### Clientes

Guarda dados cadastrais.

Uso na aplicacao:

- Perfil do usuario.
- Validacao de titularidade.
- Consulta de dados basicos.

### Contas

Guarda contas bancarias.

Uso na aplicacao:

- Saldo.
- Tipo de conta.
- Status da conta.
- Origem/destino de transferencias.

### Transacoes

Guarda movimentacoes.

Uso na aplicacao:

- Extrato.
- Historico.
- Comprovante.
- Auditoria de operacoes.

### Cartoes

Guarda cartoes por conta.

Uso futuro:

- Limite.
- Status.
- Compras.

### ChavesPix

Guarda chaves PIX.

Uso futuro:

- Transferencia por chave.
- Consulta de chave.

### Emprestimos

Guarda contratos de emprestimo.

Uso futuro:

- Consulta de emprestimos.
- Parcelas.
- Situacao de pagamento.

### LogAuditoria

Guarda eventos de auditoria.

Uso na aplicacao:

- Registrar login.
- Registrar consulta de conta.
- Registrar transferencia.
- Registrar erro importante.

## Acesso pelo DBeaver

Conexao:

```text
Tipo: SQL Server
Host: localhost
Porta: 1433
Database: BancoTreino
User: sa
Password: Treino@123456
Instancia: SQLEXPRESS
```

## Acesso pela API

String de conexao:

```text
Server=localhost\SQLEXPRESS;Database=BancoTreino;User Id=sa;Password=Treino@123456;TrustServerCertificate=True;
```

## Consultas base

Clientes:

```sql
SELECT TOP 20 *
FROM dbo.Clientes;
```

Contas de um cliente:

```sql
SELECT *
FROM dbo.Contas
WHERE ClienteId = 1;
```

Extrato:

```sql
EXEC dbo.sp_ExtratoConta @ContaId = 1;
```

Resumo:

```sql
SELECT TOP 20 *
FROM dbo.vw_ResumoCliente
ORDER BY SaldoTotal DESC;
```

## Cuidados

- Nao salvar senha em codigo.
- Usar variavel de ambiente ou Secret no Kubernetes.
- Usar transacao SQL para transferencia.
- Evitar expor CPF completo em resposta publica.
- Nao logar senha ou token.

