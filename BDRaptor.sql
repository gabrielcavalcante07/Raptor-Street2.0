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
INSERT INTO tbadm (`NomeAdm`, `EmailAdm`, `SenhaAdm`) VALUES ('Cavalcante', 'administradorn1@gmail.com', '2396');


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
INSERT INTO `dbraptor`.`tbClientes` (`NomeCliente`, `DataNascimento`, `CPF`, `Telefone`, `SenhaCliente`, `EmailCliente`) VALUES ('Gabriel', '20070814', '55779862818', '11941741429', '1447', 'gcsantos@gmail.com');

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
    IdEnd INT,
    FOREIGN KEY (IdEnd) REFERENCES tbEnderecos(IdEndereco) ON DELETE CASCADE,
    Fk_IdCliente INT,
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
    Descricao VARCHAR(500) NOT NULL,
    Fk_IdMarca INT NOT NULL,
    QuantidadeProd int,
    Tamanho int,
    ImagemProduto Varchar(300),
    FOREIGN KEY (Fk_IdMarca) REFERENCES tbMarcaProduto(IdMarca) ON DELETE CASCADE
);

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
INSERT INTO `dbraptor`.`tbpagamentos` (`MetodoPag`) VALUES ('Cartão');
INSERT INTO `dbraptor`.`tbpagamentos` (`MetodoPag`) VALUES ('Pix');
INSERT INTO `dbraptor`.`tbpagamentos` (`MetodoPag`) VALUES ('Boleto');

-- Tabela de Pedidos
CREATE TABLE tbPedido (
	IdPedido INT PRIMARY KEY AUTO_INCREMENT,
    ImagemProduto Varchar(300),
    dataPed DATETIME NOT NULL,
    Fk_IdEndereco INT NOT NULL,
    totalPedido DECIMAL(10,2) NOT NULL,
    Fk_IdPag INT NOT NULL,
    Fk_IdCliente INT NOT NULL,
    QuantidadeProd int,
    FOREIGN KEY (Fk_IdEndereco) REFERENCES tbEnderecos(IdEndereco),
    FOREIGN KEY (Fk_IdPag) REFERENCES tbPagamentos(IdPag),
    FOREIGN KEY (Fk_IdCliente) REFERENCES tbClientes(IdCliente)
);

-- Itens do Pedido
CREATE TABLE tbItemPedido (
    IdProdutoPedido INT PRIMARY KEY AUTO_INCREMENT,
	ImagemProduto Varchar(300), 
    NomeProduto VARCHAR(100) NOT NULL,
    PrecoUnitario DECIMAL(10,2) NOT NULL,
	TamanhoItem INT,
	QuantidadeItem INT,
    MarcaProduto varchar(10),
    Fk_IdPedido INT NOT NULL,
    Fk_IdProduto INT NOT NULL,
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

INSERT INTO tbProdutos (ImagemProduto, NomeProduto, PrecoProduto, Descricao, Fk_IdMarca, QuantidadeProd) VALUES
('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/ADIDAS-ADI2000.jpeg', 
'Adidas Adi2000', 499.99, 'Tênis retrô com estilo dos anos 2000.', 2, 10),

('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/ADIDAS-ADIFOM(2).webp', 
'Adidas Adifom', 599.99, 'Tênis futurista com espuma moldada.', 2, 10),

('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/ADIDAS-CAMPUS00s.webp', 
'Adidas Campus 00s', 549.99, 'Clássico retrabalhado com estética dos anos 2000.', 2, 10),

('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/ADIDAS-FORUM.webp', 
'Adidas Forum', 599.99, 'Tênis robusto inspirado no basquete.', 2, 10),

('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/ADIDAS-JAPAN.webp', 
'Adidas Japan', 499.99, 'Modelo inspirado nas Olimpíadas de Tóquio 1964.', 2, 10),

('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/ADIDAS-OG.webp', 
'Adidas OG', 449.99, 'Versão original de um clássico da Adidas.', 2, 10),

('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/ADIDAS-SAMBAOG.webp', 
'Adidas Samba OG', 479.99, 'Ícone da moda urbana e esportiva.', 2, 10),

('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/ADIDAS-SAMBAXLG.webp', 
'Adidas Samba XLG', 499.99, 'Versão moderna com silhueta ampliada.', 2, 10),

('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/ADIDAS-SUPERSTAR(2).webp', 
'Adidas Superstar', 529.99, 'Famoso pelo bico de concha e estilo icônico.', 2, 10),

('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/MIZUNO-CONTENDER.webp', 
'Mizuno Contender', 459.99, 'Tênis retrô com conforto e estilo casual.', 4, 10),

('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/MIZUNO-MUJIN(1).webp', 
'Mizuno Mujin', 699.99, 'Tênis robusto para trilhas e terrenos difíceis.', 4, 10),

('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/MIZUNO-MXR.webp', 
'Mizuno MXR', 499.99, 'Versátil e confortável para diversas atividades.', 4, 10),

('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/MIZUNO-PROPHECYLS(2).webp', 
'Mizuno Prophecy', 899.99, 'Amortecimento com placas e design marcante.', 4, 10),

('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/MIZUNO-WAVERIDER.webp', 
'Mizuno Wave Rider', 649.99, 'Ideal para corrida com amortecimento eficiente.', 4, 10),

('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/MIZUNO-WAVERIDER-YOKAI(2).webp', 
'Mizuno Wave Rider Yokai', 679.99, 'Edição especial inspirada na mitologia japonesa.', 4, 10),

('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/NIKE-AIR-ZOOM-SPIRIDON.webp', 
'Nike Air Zoom Spiridon', 699.99, 'Tênis de corrida com amortecimento responsivo.', 1, 10),

('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/NIKE-AIRFORCE1(2).webp', 
'Nike Air Force 1', 599.99, 'Clássico versátil e atemporal da Nike.', 1, 10);
