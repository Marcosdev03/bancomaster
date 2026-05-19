# Manual SQL com significados

Este manual explica os principais comandos e simbolos de SQL em linguagem simples.

Use junto com o banco `BancoTreino` no SQL Server.

## SELECT

`SELECT` serve para fazer projecao.

Projecao significa escolher quais colunas voce quer enxergar no resultado.

Exemplo:

```sql
SELECT Nome, CPF, RendaMensal
FROM dbo.Clientes;
```

Significado:

- `SELECT Nome, CPF, RendaMensal`: mostre somente essas colunas.
- `FROM dbo.Clientes`: busque esses dados na tabela `Clientes`.

Se a tabela tiver 20 colunas, mas voce colocou 3 no `SELECT`, o resultado mostra apenas essas 3.

## FROM

`FROM` indica de onde os dados vao sair.

Normalmente ele aponta para uma tabela, view ou resultado temporario.

Exemplo:

```sql
SELECT *
FROM dbo.Clientes;
```

Significado:

- `SELECT *`: mostre todas as colunas.
- `FROM dbo.Clientes`: da tabela `Clientes`.

Pense no `FROM` como a origem da consulta.

## Asterisco: *

`*` significa todas as colunas.

Exemplo:

```sql
SELECT *
FROM dbo.Contas;
```

Significado:

Mostre todas as colunas da tabela `Contas`.

Use `*` quando estiver explorando a tabela. Em sistemas reais, prefira escrever as colunas:

```sql
SELECT ContaId, NumeroConta, SaldoAtual
FROM dbo.Contas;
```

Isso deixa a consulta mais clara e evita trazer dados desnecessarios.

## WHERE

`WHERE` serve para filtrar linhas.

Exemplo:

```sql
SELECT Nome, ScoreCredito
FROM dbo.Clientes
WHERE ScoreCredito >= 700;
```

Significado:

Mostre nome e score somente dos clientes com score maior ou igual a 700.

`SELECT` escolhe colunas.

`WHERE` escolhe linhas.

## ORDER BY

`ORDER BY` serve para ordenar o resultado.

Exemplo:

```sql
SELECT Nome, RendaMensal
FROM dbo.Clientes
ORDER BY RendaMensal DESC;
```

Significado:

Mostre os clientes ordenando pela maior renda mensal.

Tipos de ordenacao:

- `ASC`: crescente.
- `DESC`: decrescente.

## TOP

`TOP` limita a quantidade de linhas no SQL Server.

Exemplo:

```sql
SELECT TOP 10 Nome, RendaMensal
FROM dbo.Clientes
ORDER BY RendaMensal DESC;
```

Significado:

Mostre apenas os 10 clientes com maior renda.

## DISTINCT

`DISTINCT` remove repeticoes do resultado.

Exemplo:

```sql
SELECT DISTINCT Estado
FROM dbo.Clientes;
```

Significado:

Mostre cada estado apenas uma vez.

Sem `DISTINCT`, o estado `SP` poderia aparecer centenas de vezes.

## AS

`AS` cria um apelido para coluna ou tabela.

Exemplo:

```sql
SELECT
    Nome AS Cliente,
    RendaMensal AS Renda
FROM dbo.Clientes;
```

Significado:

No resultado, a coluna `Nome` aparece como `Cliente`, e `RendaMensal` aparece como `Renda`.

## JOIN

`JOIN` junta dados de tabelas relacionadas.

Exemplo:

```sql
SELECT
    c.Nome,
    co.NumeroConta,
    co.SaldoAtual
FROM dbo.Clientes c
JOIN dbo.Contas co ON co.ClienteId = c.ClienteId;
```

Significado:

Junte clientes com contas usando o campo `ClienteId`.

Aqui:

- `c` e apelido de `Clientes`.
- `co` e apelido de `Contas`.
- `ON co.ClienteId = c.ClienteId` diz qual campo liga uma tabela na outra.

## INNER JOIN

`INNER JOIN` traz apenas registros que existem dos dois lados.

Exemplo:

```sql
SELECT c.Nome, co.NumeroConta
FROM dbo.Clientes c
INNER JOIN dbo.Contas co ON co.ClienteId = c.ClienteId;
```

Significado:

Mostre somente clientes que possuem conta.

`JOIN` sozinho normalmente significa `INNER JOIN`.

## LEFT JOIN

`LEFT JOIN` traz tudo da tabela da esquerda, mesmo que nao exista correspondente na direita.

Exemplo:

```sql
SELECT c.Nome, e.EmprestimoId
FROM dbo.Clientes c
LEFT JOIN dbo.Emprestimos e ON e.ClienteId = c.ClienteId;
```

Significado:

Mostre todos os clientes, mesmo os que nao possuem emprestimo.

Quando nao existir emprestimo, as colunas da tabela `Emprestimos` aparecem como `NULL`.

## NULL

`NULL` significa ausencia de valor.

Nao e zero.

Nao e texto vazio.

E valor desconhecido ou inexistente.

Exemplo:

```sql
SELECT *
FROM dbo.ParcelasEmprestimo
WHERE DataPagamento IS NULL;
```

Significado:

Mostre parcelas que ainda nao possuem data de pagamento.

Para comparar `NULL`, use:

```sql
IS NULL
```

ou:

```sql
IS NOT NULL
```

Nao use `= NULL`.

## AND

`AND` exige que duas condicoes sejam verdadeiras ao mesmo tempo.

Exemplo:

```sql
SELECT Nome, RendaMensal, ScoreCredito
FROM dbo.Clientes
WHERE RendaMensal >= 8000
  AND ScoreCredito >= 700;
```

Significado:

Mostre clientes com renda alta e score alto.

As duas condicoes precisam ser verdadeiras.

## OR

`OR` aceita uma condicao ou outra.

Exemplo:

```sql
SELECT Nome, Estado, ScoreCredito
FROM dbo.Clientes
WHERE Estado = 'SP'
   OR ScoreCredito >= 800;
```

Significado:

Mostre clientes de SP ou clientes com score maior ou igual a 800.

Se qualquer uma das condicoes for verdadeira, a linha aparece.

## IN

`IN` verifica se um valor esta dentro de uma lista.

Exemplo:

```sql
SELECT Nome, Estado
FROM dbo.Clientes
WHERE Estado IN ('SP', 'RJ', 'MG');
```

Significado:

Mostre clientes cujo estado seja SP, RJ ou MG.

Isso evita escrever varios `OR`.

## BETWEEN

`BETWEEN` filtra valores dentro de um intervalo.

Exemplo:

```sql
SELECT Nome, RendaMensal
FROM dbo.Clientes
WHERE RendaMensal BETWEEN 3000 AND 8000;
```

Significado:

Mostre clientes com renda entre 3000 e 8000.

O `BETWEEN` inclui as duas pontas do intervalo.

## LIKE

`LIKE` busca padroes em texto.

Exemplo:

```sql
SELECT Nome
FROM dbo.Clientes
WHERE Nome LIKE 'Ana%';
```

Significado:

Mostre nomes que comecam com `Ana`.

O simbolo `%` significa qualquer quantidade de caracteres.

Exemplos:

```sql
LIKE 'Ana%'
```

Comeca com Ana.

```sql
LIKE '%Silva'
```

Termina com Silva.

```sql
LIKE '%Oliveira%'
```

Contem Oliveira em qualquer posicao.

## COUNT

`COUNT` conta linhas.

Exemplo:

```sql
SELECT COUNT(*) AS TotalClientes
FROM dbo.Clientes;
```

Significado:

Conte quantos clientes existem na tabela.

## SUM

`SUM` soma valores.

Exemplo:

```sql
SELECT SUM(SaldoAtual) AS SaldoTotal
FROM dbo.Contas;
```

Significado:

Some o saldo de todas as contas.

## AVG

`AVG` calcula media.

Exemplo:

```sql
SELECT AVG(RendaMensal) AS RendaMedia
FROM dbo.Clientes;
```

Significado:

Calcule a renda media dos clientes.

## MIN

`MIN` pega o menor valor.

Exemplo:

```sql
SELECT MIN(RendaMensal) AS MenorRenda
FROM dbo.Clientes;
```

Significado:

Mostre a menor renda mensal cadastrada.

## MAX

`MAX` pega o maior valor.

Exemplo:

```sql
SELECT MAX(RendaMensal) AS MaiorRenda
FROM dbo.Clientes;
```

Significado:

Mostre a maior renda mensal cadastrada.

## GROUP BY

`GROUP BY` agrupa linhas para calcular totais por categoria.

Exemplo:

```sql
SELECT
    Estado,
    COUNT(*) AS TotalClientes
FROM dbo.Clientes
GROUP BY Estado;
```

Significado:

Agrupe os clientes por estado e conte quantos existem em cada estado.

Sempre que voce mistura uma coluna comum com uma agregacao, a coluna comum geralmente precisa estar no `GROUP BY`.

## HAVING

`HAVING` filtra grupos.

Exemplo:

```sql
SELECT
    Estado,
    COUNT(*) AS TotalClientes
FROM dbo.Clientes
GROUP BY Estado
HAVING COUNT(*) > 100;
```

Significado:

Agrupe clientes por estado, mas mostre apenas estados com mais de 100 clientes.

Diferenca:

- `WHERE` filtra linhas antes do agrupamento.
- `HAVING` filtra grupos depois do agrupamento.

## UNION

`UNION` junta o resultado de duas consultas e remove duplicados.

Exemplo:

```sql
SELECT Cidade
FROM dbo.Clientes
WHERE Estado = 'SP'

UNION

SELECT Cidade
FROM dbo.Agencias
WHERE Estado = 'SP';
```

Significado:

Junte cidades de clientes e cidades de agencias em uma unica lista, sem repetir valores iguais.

Regras importantes:

- As duas consultas precisam ter a mesma quantidade de colunas.
- As colunas precisam ter tipos compativeis.
- O nome final das colunas vem da primeira consulta.

## UNION ALL

`UNION ALL` junta o resultado de duas consultas sem remover duplicados.

Exemplo:

```sql
SELECT Estado
FROM dbo.Clientes

UNION ALL

SELECT Estado
FROM dbo.Agencias;
```

Significado:

Junte tudo, mantendo repeticoes.

`UNION ALL` costuma ser mais rapido que `UNION`, porque nao precisa comparar e remover duplicados.

## INSERT

`INSERT` insere dados em uma tabela.

Exemplo:

```sql
INSERT dbo.Agencias (Codigo, Nome, Cidade, Estado, DataAbertura)
VALUES ('0099', 'Agencia Teste', 'Sao Paulo', 'SP', '2026-01-01');
```

Significado:

Crie uma nova agencia.

## UPDATE

`UPDATE` altera dados existentes.

Exemplo:

```sql
UPDATE dbo.Clientes
SET StatusCliente = 'Inativo'
WHERE ClienteId = 10;
```

Significado:

Altere o status do cliente 10 para inativo.

Cuidado: `UPDATE` sem `WHERE` altera a tabela inteira.

## DELETE

`DELETE` apaga linhas de uma tabela.

Exemplo:

```sql
DELETE FROM dbo.LogAuditoria
WHERE LogId = 1;
```

Significado:

Apague o registro de log com `LogId = 1`.

Cuidado: `DELETE` sem `WHERE` apaga todas as linhas da tabela.

## BEGIN TRAN

`BEGIN TRAN` inicia uma transacao.

Transacao e um bloco de comandos que pode ser confirmado ou desfeito.

Exemplo:

```sql
BEGIN TRAN;

UPDATE dbo.Contas
SET SaldoAtual = SaldoAtual - 100
WHERE ContaId = 1;
```

## COMMIT

`COMMIT` confirma uma transacao.

Exemplo:

```sql
COMMIT;
```

Significado:

Grave definitivamente as alteracoes feitas dentro da transacao.

## ROLLBACK

`ROLLBACK` desfaz uma transacao.

Exemplo:

```sql
ROLLBACK;
```

Significado:

Desfaca as alteracoes feitas desde o `BEGIN TRAN`.

## CREATE TABLE

`CREATE TABLE` cria uma tabela.

Exemplo:

```sql
CREATE TABLE dbo.Teste (
    Id int,
    Nome varchar(100)
);
```

Significado:

Crie uma tabela chamada `Teste` com as colunas `Id` e `Nome`.

## PRIMARY KEY

`PRIMARY KEY` identifica cada linha de forma unica.

Exemplo:

```sql
ClienteId int PRIMARY KEY
```

Significado:

`ClienteId` nao pode repetir e identifica o cliente.

## FOREIGN KEY

`FOREIGN KEY` cria relacionamento entre tabelas.

Exemplo:

```sql
ClienteId int REFERENCES dbo.Clientes(ClienteId)
```

Significado:

A coluna `ClienteId` desta tabela aponta para um cliente existente na tabela `Clientes`.

## INDEX

`INDEX` ajuda o banco a encontrar dados mais rapido.

Exemplo:

```sql
CREATE INDEX IX_Clientes_CPF
ON dbo.Clientes (CPF);
```

Significado:

Crie um indice para acelerar buscas por CPF.

Indice melhora leitura, mas pode deixar escrita um pouco mais pesada.

## Resumo mental rapido

```sql
SELECT colunas
FROM tabela
WHERE filtro de linhas
GROUP BY agrupamento
HAVING filtro de grupos
ORDER BY ordenacao;
```

Ordem de leitura humana:

1. `FROM`: de onde vem.
2. `WHERE`: quais linhas entram.
3. `GROUP BY`: como agrupa.
4. `HAVING`: quais grupos ficam.
5. `SELECT`: quais colunas aparecem.
6. `ORDER BY`: em qual ordem aparece.

## Exercicio completo

```sql
SELECT
    c.Estado,
    COUNT(*) AS TotalClientes,
    AVG(c.RendaMensal) AS RendaMedia,
    SUM(co.SaldoAtual) AS SaldoTotal
FROM dbo.Clientes c
JOIN dbo.Contas co ON co.ClienteId = c.ClienteId
WHERE c.StatusCliente = 'Ativo'
GROUP BY c.Estado
HAVING COUNT(*) >= 50
ORDER BY SaldoTotal DESC;
```

Significado:

1. Busque clientes e contas.
2. Junte as tabelas pelo `ClienteId`.
3. Considere apenas clientes ativos.
4. Agrupe por estado.
5. Conte clientes, calcule renda media e saldo total.
6. Mostre apenas estados com pelo menos 50 clientes.
7. Ordene pelo maior saldo total.
