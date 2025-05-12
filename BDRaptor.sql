CREATE DATABASE DBRaptor;
USE DBRaptor;

-- Tabela de Endereços
CREATE TABLE tbEnderecos (
    IdEndereco INT PRIMARY KEY AUTO_INCREMENT,
    CEP VARCHAR(10) NOT NULL,
    NumeroEndereco SMALLINT NOT NULL,
    Logradouro VARCHAR(200) NOT NULL,
    Complemento VARCHAR(100),
    Bairro VARCHAR(100) NOT NULL,
    Cidade VARCHAR(100) NOT NULL,
    Estado VARCHAR(100) NOT NULL
);

-- Tabela de Administradores
Create table tbAdm(
IdAdm int primary key auto_increment,
NomeAdm varchar (100) not null,
EmailAdm enum ('administradorn1@gmail.com','administradorn2@gmail.com'),
SenhaAdm varchar (30) not null
);

-- Tabela de Clientes
CREATE TABLE tbClientes (
    IdCliente INT PRIMARY KEY AUTO_INCREMENT,
    NomeCliente VARCHAR(100) NOT NULL,
    DataNascimento DATE NOT NULL,
    CPF CHAR(11) NOT NULL UNIQUE,
    Telefone CHAR(11) NOT NULL,
    SenhaCliente VARCHAR(100) NOT NULL,
    EmailCliente VARCHAR(100) NOT NULL UNIQUE
);

CREATE TABLE tbLogin(
IdLogin  int primary key auto_increment,
IdCliente int, 
foreign key (IdCliente) references tbClientes(IdCliente),
IdAdm int, 
foreign key (IdAdm) references tbAdm(IdAdm)
);

-- Associação de Clientes com Endereços
CREATE TABLE tbClienteEnderecos (
    IdEndCliente INT PRIMARY KEY AUTO_INCREMENT,
    Fk_IdEndereco INT NOT NULL,
    Fk_IdCliente INT NOT NULL,
    FOREIGN KEY (Fk_IdEndereco) REFERENCES tbEnderecos(IdEndereco) ON DELETE CASCADE,
    FOREIGN KEY (Fk_IdCliente) REFERENCES tbClientes(IdCliente) ON DELETE CASCADE
);

-- Tabela de Marcas
CREATE TABLE tbMarcaProduto (
    IdMarca INT PRIMARY KEY AUTO_INCREMENT,
    NomeMarca VARCHAR(50) NOT NULL
);

-- Tabela de Produtos
CREATE TABLE tbProdutos (
    IdProduto INT PRIMARY KEY AUTO_INCREMENT,
    NomeProduto VARCHAR(100) NOT NULL,
    PrecoProduto DECIMAL(10,2) NOT NULL,
    Qtd INT UNSIGNED NOT NULL,
    Descricao VARCHAR(500) NOT NULL,
    Tipo VARCHAR(50) NOT NULL,
    Desconto BOOLEAN NOT NULL DEFAULT FALSE,
    Tamanho INT NOT NULL, 
    Fk_IdMarca INT NOT NULL,
    QuantidadeProd int,
    ImagemProduto Varchar(300),
    FOREIGN KEY (Fk_IdMarca) REFERENCES tbMarcaProduto(IdMarca) ON DELETE CASCADE
);

/*ALTER TABLE tbProdutos
MODIFY COLUMN QuantidadeProd int;
*/

-- Tabela de Favoritos do Cliente
CREATE TABLE tbClienteFav (
    IdClienteFav INT PRIMARY KEY AUTO_INCREMENT,
    IdCliente INT NOT NULL,
    IdProduto INT NOT NULL,
    ativado BOOLEAN NOT NULL DEFAULT TRUE,
    CONSTRAINT unique_client_product UNIQUE (IdCliente, IdProduto),
    FOREIGN KEY (IdCliente) REFERENCES tbClientes(IdCliente) ON DELETE CASCADE,
    FOREIGN KEY (IdProduto) REFERENCES tbProdutos(IdProduto) ON DELETE CASCADE
);

-- Tabela de Pagamentos
CREATE TABLE tbPagamentos (
    IdPag INT PRIMARY KEY AUTO_INCREMENT,
    StatusPag ENUM('Pendente','Pago','Não Realizado') NOT NULL,
    MetodoPag VARCHAR(50) NOT NULL
);

-- Tabela de Nota Fiscal
CREATE TABLE tbNotaFiscal (
    IdNota INT PRIMARY KEY AUTO_INCREMENT,
    dataNf DATE NOT NULL,
    valorNF DECIMAL(10,2) NOT NULL
);

-- Tabela de Pedidos
CREATE TABLE tbPedido (
    IdPedido INT PRIMARY KEY AUTO_INCREMENT,
    Fk_IdNota INT NOT NULL,
    Fk_IdEndereco INT NOT NULL,
    Fk_IdPag INT NOT NULL,
    Fk_IdCliente INT NOT NULL,
    dataPed DATETIME NOT NULL,
    totalPedido DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (Fk_IdNota) REFERENCES tbNotaFiscal(IdNota),
    FOREIGN KEY (Fk_IdEndereco) REFERENCES tbEnderecos(IdEndereco),
    FOREIGN KEY (Fk_IdPag) REFERENCES tbPagamentos(IdPag),
    FOREIGN KEY (Fk_IdCliente) REFERENCES tbClientes(IdCliente)
);

-- Itens do Pedido
CREATE TABLE tbItemPedido (
    IdProdutoPedido INT PRIMARY KEY AUTO_INCREMENT,
    Fk_IdPedido INT NOT NULL,
    Fk_IdProduto INT NOT NULL,
    PrecoUnitario DECIMAL(10,2) NOT NULL,
    Quantidade INT UNSIGNED NOT NULL,
    FOREIGN KEY (Fk_IdPedido) REFERENCES tbPedido(IdPedido) ON DELETE CASCADE,
    FOREIGN KEY (Fk_IdProduto) REFERENCES tbProdutos(IdProduto) ON DELETE CASCADE
);

UPDATE `dbraptor`.`tbprodutos` SET `QuantidadeProd` = '300' WHERE (`IdProduto` = '1');
UPDATE `dbraptor`.`tbprodutos` SET `QuantidadeProd` = '300' WHERE (`IdProduto` = '2');
UPDATE `dbraptor`.`tbprodutos` SET `QuantidadeProd` = '300' WHERE (`IdProduto` = '3');
UPDATE `dbraptor`.`tbprodutos` SET `QuantidadeProd` = '300' WHERE (`IdProduto` = '4');
UPDATE `dbraptor`.`tbprodutos` SET `QuantidadeProd` = '300' WHERE (`IdProduto` = '5');
UPDATE `dbraptor`.`tbprodutos` SET `QuantidadeProd` = '300' WHERE (`IdProduto` = '6');
UPDATE `dbraptor`.`tbprodutos` SET `QuantidadeProd` = '300' WHERE (`IdProduto` = '7');
UPDATE `dbraptor`.`tbprodutos` SET `QuantidadeProd` = '300' WHERE (`IdProduto` = '8');
UPDATE `dbraptor`.`tbprodutos` SET `QuantidadeProd` = '300' WHERE (`IdProduto` = '9');
UPDATE `dbraptor`.`tbprodutos` SET `QuantidadeProd` = '300' WHERE (`IdProduto` = '10');

INSERT INTO `tbmarcaproduto` (`NomeMarca`) VALUES ('Nike');
INSERT INTO `tbmarcaproduto` (`NomeMarca`) VALUES ('Adidas');
INSERT INTO `tbmarcaproduto` (`NomeMarca`) VALUES ('Puma');
INSERT INTO `tbmarcaproduto` (`NomeMarca`) VALUES ('Mizuno');
INSERT INTO `tbmarcaproduto` (`NomeMarca`) VALUES ('Vans');

INSERT INTO tbProdutos (NomeProduto, PrecoProduto, Qtd, Descricao, Tipo, Desconto, Tamanho, Fk_IdMarca, QuantidadeProd) VALUES
('Air Jordan 4 Retro', 899.99, 50, 'Tênis de alta performance com design clássico da Nike.', 'Tênis', FALSE, 42, 1, 100),
('Adidas Ultraboost 22', 749.90, 30, 'Tênis confortável ideal para corridas de longa distância.', 'Tênis', TRUE, 41, 2, 100),
('Puma RS-X', 599.90, 20, 'Tênis esportivo com tecnologia de amortecimento Puma.', 'Tênis', FALSE, 43, 3, 100),
('Mizuno Wave Prophecy 11', 1099.99, 15, 'Tênis de alta resistência para treinos intensos.', 'Tênis', FALSE, 42, 4, 100),
('Vans Old Skool', 399.90, 40, 'Tênis casual icônico da Vans, ótimo para o dia a dia.', 'Tênis', TRUE, 40, 5, 100),
('Nike Air Force 1', 799.99, 60, 'Tênis clássico da Nike com visual atemporal.', 'Tênis', FALSE, 42, 1, 100),
('Adidas Forum Low', 699.90, 25, 'Tênis retrô da Adidas com design moderno.', 'Tênis', FALSE, 41, 2, 100),
('Puma Suede Classic', 349.99, 35, 'Tênis tradicional da Puma com acabamento em camurça.', 'Tênis', TRUE, 42, 3, 100),
('Mizuno Wave Sky 5', 949.90, 18, 'Tênis super amortecido para corredores exigentes.', 'Tênis', FALSE, 43, 4, 100),
('Vans Sk8-Hi', 449.90, 28, 'Tênis cano alto da Vans, estilo e conforto para o dia.', 'Tênis', TRUE, 41, 5, 100);
