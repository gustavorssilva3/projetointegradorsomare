CREATE DATABASE sistemasomare;

CREATE TABLE cadastro(
id INT AUTO_INCREMENT PRIMARY KEY,
email VARCHAR(100) UNIQUE NOT NULL,
nome VARCHAR(100) NOT NULL,
senha VARCHAR(50) NOT NULL,
pergunta_rec_1 VARCHAR(50) NOT NULL,
pergunta_rec_2 VARCHAR(50) NOT NULL,
resposta_rec_1 VARCHAR(50) NOT NULL,
resposta_rec_2 VARCHAR(50) NOT NULL,
data_criacao DATETIME DEFAULT CURRENT_TIMESTAMP, 
data_atualizacao DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
);

INSERT INTO cadastro(email, nome, senha, pergunta_rec_1, pergunta_rec_2, resposta_rec_1, resposta_rec_2)
VALUES
("biancafernandes222@gmail.com", "Bianca","0348546", "Qual é seu animal preferido?", "Qual é a sua cor preferida?", "Gato" ,"Roxo"),
("lucas.silva@gmail.com", "Lucas Silva", "senha123", "Qual é o nome do seu melhor amigo?", "Qual cidade você nasceu?", "Carlos", "São Paulo"),
("ana.pereira@hotmail.com", "Ana Pereira", "abcd1234", "Qual é o nome da sua mãe?", "Qual é o nome do seu cachorro?", "Maria", "Max"),
("marcoantonio_01@gmail.com", "Marco Antônio", "senhaSegura2025", "Qual é a sua comida favorita?", "Qual é o nome da sua escola?", "Pizza", "Colégio XYZ");


SELECT * 
FROM cadastro
WHERE email = "biancafernandes222@gmail.com"
  AND pergunta_rec_1 = "Qual é seu animal preferido?" 
  AND pergunta_rec_2 = "Qual é a sua cor preferida?"
  AND resposta_rec_1 = "Gato"
  AND resposta_rec_2 = "Roxo";


/* Atualização da data de criação de conta */
UPDATE cadastro
SET senha = "TESTE242424"
WHERE email = "biancafernandes222@gmail.com";

UPDATE cadastro
SET pergunta_rec_2 = "Qual é a sua cor preferida?"
WHERE email = "biancafernandes222@gmail.com";

CREATE TABLE conta (
id_conta INT AUTO_INCREMENT PRIMARY KEY,
nome_cliente VARCHAR(100) NOT NULL,
data_vencimento DATE NOT NULL, 
nome_conta VARCHAR(50) NOT NULL,
valor DECIMAL(8,2) NOT NULL,
tipo VARCHAR(20) NOT NULL,
categoria VARCHAR(50),
descricao VARCHAR(200),
situacao VARCHAR(20) NOT NULL,
criacao DATETIME DEFAULT CURRENT_TIMESTAMP, 
atualizacao DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
id_cadastro INT NOT NULL,
FOREIGN KEY (id_cadastro) REFERENCES cadastro(id)
);


INSERT INTO conta( nome_cliente, data_vencimento, nome_conta, valor, tipo, categoria ,descricao, situacao)
VALUES
("Enel","2025-04-05","Conta de Luz", 250.99,"Conta a pagar","Energia", "" , "Pendente"),
("Sabesp","2025-04-10","Conta de Água", 120.45,"Conta a Pagar", " Água", "Desconto por consumo abaixo da média","Atrasada"),
("Vivo","2025-04-12","Conta de Internet", 150.00, "Conta a Pagar", "Internet", "Plano de 300MB", "Pendente"),
( "Oi","2025-04-15", "Conta de Telefone", 80.99, "Conta a Pagar","Telefone", "Inclui ligações ilimitadas", "Paga"),
("System", "2025-04-20", "Venda de Produto", 500.00, "Conta a Receber", "Vendas", "", "Paga"),
("System", "2025-04-30", "Venda de Produto", 300.00, "Conta a Receber", "Vendas", "", "Pendente");


/* Atualizando dados na tabela conta */
UPDATE conta
SET data_vencimento = "2025-03-28"
WHERE nome_cliente IN ("Sabesp", "Vivo");

/* Somando todas as contas */
SELECT SUM(valor) AS soma_valor FROM conta;

/*Select de todas as colunas da página 24*/
SELECT * 
FROM conta
WHERE nome_cliente = "Oi"
  AND data_vencimento = "2025-04-15" 
  AND nome_conta = "Conta de Telefone"
  AND valor = 80.99
  AND tipo = "Conta a Pagar"
  AND categoria = "Telefone"
  AND descricao = "Inclui ligações ilimitadas"
  AND situacao = "Paga";


/*Select de todas as colunas da página 25*/
SELECT * 
FROM conta
WHERE nome_cliente = "Oi"
  AND data_vencimento = "2025-04-15" 
  AND nome_conta = "Conta de Telefone"
  AND valor = 80.99
  AND tipo = "Conta a Pagar"
  AND categoria = "Telefone"
  AND descricao = "Inclui ligações ilimitadas"

/*Realizar Select de todas as contas a pagar dentro de um mês.*/
SELECT * FROM conta
WHERE situacao = "Pendente"
  AND MONTH(data_vencimento) = 4
  AND YEAR(data_vencimento) = 2025;

/*Realizar Select de todas as contas atrasadas dentro de um mês.*/
SELECT * FROM conta
WHERE situacao = "Atrasada";

/*Realizar Select de todas as contas pagas, recebimento dentro de um ano*/
SELECT * FROM conta
WHERE tipo = "Conta a Receber"
  AND situacao = "Paga"
  AND YEAR(data_vencimento) = 2025;

/*Realizar Select de todas as contas, saída dentro de um ano.*/
SELECT * FROM conta
WHERE tipo = "Conta a Pagar"
  AND situacao = "Paga"
  AND YEAR(data_vencimento) = 2025;


/*Alterando status de uma conta*/
UPDATE conta
SET situacao = "Paga"
WHERE id_conta = 1;


/*Deletando o id da tabela conta*/
DELETE FROM conta  WHERE id_conta = 5;
