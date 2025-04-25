create database DBRaptor;
use DBRaptor;

CREATE TABLE tbEnderecos(
IdEndereco int PRIMARY KEY auto_increment,
CEP varchar (10) not null,
NumeroEndereco smallint not null,
Logradouro varchar(200) not null,
Complemento varchar (100),
Bairro varchar(100) not null,
Cidade varchar(100) not null,
Estado varchar(100) not null
);

Create table tbAdm(
IdAdm int primary key auto_increment,
NomeAdm varchar (100) not null,
EmailAdm enum ('administradorn1@gmail.com','administradorn2@gmail.com'),
SenhaAdm varchar (30) not null
);

CREATE TABLE tbClientes(
IdCliente int PRIMARY KEY auto_increment,
NomeCliente varchar(100) not null,
DataNascimento date not null,
CPF Int not null,
Telefone numeric (11,0) not null,
SenhaCliente varchar (30) not null,
EmailCliente varchar (100) not null
);

CREATE TABLE tbClienteEnderecos(
IdEndCliente int primary key auto_increment,
IdEnd int not null,
foreign key (IdEndCliente) references tbEnderecos(IdEndereco) on delete cascade,
Fk_IdCliente int not null,
foreign key (Fk_IdCliente) references tbClientes(IdCliente) on delete cascade
);

CREATE TABLE tbMarcaProduto(
IdMarca int primary key auto_increment,
NomeMarca varchar (50) not null
);

CREATE TABLE tbProdutos (
NomeProduto Varchar(100) not null,
PrecoProduto Decimal(6,2) not null,
Qtd int unsigned not null,
Descricao Varchar(200) not null,
IdProduto int primary key auto_increment,
Tipo varchar(50) not null,
Desconto bool not null,
Tamanho tinyint unsigned not null,
Fk_IdMarca int not null,
foreign key (Fk_IdMarca) references tbMarcaProduto(IdMarca) on delete cascade
);

CREATE TABLE tbClienteFav(
IdClienteFav int primary key auto_increment,
IdCliente int not null,
foreign key (IdCliente) references tbClientes(IdCliente) on delete cascade,
IdProduto int not null,
foreign key (IdProduto) references tbProdutos(IdProduto) on delete cascade,
ativado boolean not null default true,
constraint unique_client_product unique (IdCliente, IdProduto)
);

CREATE TABLE tbCarrinho (
TipoProduto varchar(50) not null,
TotalVenda Int not null,
Qtd int unsigned not null,
IdVenda int PRIMARY KEY auto_increment,
DataVenda Datetime not null,
Fk_IdCliente Int not null,
FOREIGN KEY (Fk_IdCliente) REFERENCES tbClientes (IdCliente)
);

CREATE TABLE tbPagamentos (
IdPag Int primary key auto_increment,
StatusPag enum('Pendente','Pago','Não Realizado'),
MetodoPag varchar(50)
);

CREATE TABLE tbNotaFiscal(
IdNota int primary key auto_increment,
dataNf date not null,
valorNF decimal(10,2) not null
);

CREATE TABLE tbPedido (
IdPedido Int PRIMARY KEY auto_increment,
Fk_IdNota int not null,
foreign key (Fk_IdNota) references tbNotaFiscal(IdNota),
Fk_IdEndereco int not null,
foreign key (Fk_IdEndereco) references tbEnderecos(IdEndereco),
Fk_IdPag int not null,
foreign key (Fk_IdPag) references tbPagamentos(IdPag),
Fk_IdCliente int not null,
foreign key (Fk_IdCliente) references tbClientes (IdCliente),
dataPed datetime not null,
totalPedido decimal (10,2) not null
);

CREATE TABLE tbItemPedido(
IdProdutoPedido int primary key auto_increment,
Fk_IdPedido int not null,
foreign key (Fk_IdPedido) references tbPedido(IdPedido),
Fk_IdProduto int not null,
foreign key (Fk_IdProduto) references tbProdutos(IdProduto),
PrecoUnitario Decimal (10,2) not null
); 





