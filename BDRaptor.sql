CREATE DATABASE DBRaptor;
USE DBRaptor;
-- DROP DATABASE DBRaptor;

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
CREATE TABLE tbAdm (
    IdAdm INT PRIMARY KEY AUTO_INCREMENT,
    NomeAdm VARCHAR(100) NOT NULL,
    EmailAdm ENUM('administradorn1@gmail.com','administradorn2@gmail.com'),
    SenhaAdm VARCHAR(30) NOT NULL
);

-- Tabela de Clientes
CREATE TABLE tbClientes (
    IdCliente INT PRIMARY KEY AUTO_INCREMENT,
    NomeCliente VARCHAR(100) NOT NULL,
    DataNascimento DATE NOT NULL,
    CPF CHAR(11) NOT NULL, -- Corrigido para CHAR(11) para CPF
    Telefone CHAR(11) NOT NULL, -- Corrigido para CHAR(11) também
    SenhaCliente VARCHAR(30) NOT NULL,
    EmailCliente VARCHAR(100) NOT NULL
);

-- Associação de Clientes com Endereços
CREATE TABLE tbClienteEnderecos (
    IdEndCliente INT PRIMARY KEY AUTO_INCREMENT,
    Fk_IdEndereco INT NOT NULL,
    FOREIGN KEY (Fk_IdEndereco) REFERENCES tbEnderecos(IdEndereco) ON DELETE CASCADE,
    Fk_IdCliente INT NOT NULL,
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
    PrecoProduto DECIMAL(6,2) NOT NULL,
    Qtd INT UNSIGNED NOT NULL,
    Descricao VARCHAR(200) NOT NULL,
    Tipo VARCHAR(50) NOT NULL,
    Desconto BOOLEAN NOT NULL,
    Tamanho TINYINT UNSIGNED NOT NULL,
    Fk_IdMarca INT NOT NULL,
    FOREIGN KEY (Fk_IdMarca) REFERENCES tbMarcaProduto(IdMarca) ON DELETE CASCADE
);

ALTER TABLE tbProdutos ADD COLUMN Image LONGBLOB;


-- Tabela de Favoritos do Cliente
CREATE TABLE tbClienteFav (
    IdClienteFav INT PRIMARY KEY AUTO_INCREMENT,
    IdCliente INT NOT NULL,
    FOREIGN KEY (IdCliente) REFERENCES tbClientes(IdCliente) ON DELETE CASCADE,
    IdProduto INT NOT NULL,
    FOREIGN KEY (IdProduto) REFERENCES tbProdutos(IdProduto) ON DELETE CASCADE,
    ativado BOOLEAN NOT NULL DEFAULT TRUE,
    CONSTRAINT unique_client_product UNIQUE (IdCliente, IdProduto)
);

-- Tabela de Carrinho
CREATE TABLE tbCarrinho (
    IdCarrinho INT PRIMARY KEY AUTO_INCREMENT,
    TipoProduto VARCHAR(50) NOT NULL,
    TotalVenda INT NOT NULL,
    Qtd INT UNSIGNED NOT NULL,
    DataVenda DATETIME NOT NULL,
    Fk_IdCliente INT NOT NULL,
    FOREIGN KEY (Fk_IdCliente) REFERENCES tbClientes(IdCliente)
);

-- Tabela de Pagamentos
CREATE TABLE tbPagamentos (
    IdPag INT PRIMARY KEY AUTO_INCREMENT,
    StatusPag ENUM('Pendente','Pago','Não Realizado'),
    MetodoPag VARCHAR(50)
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
    FOREIGN KEY (Fk_IdNota) REFERENCES tbNotaFiscal(IdNota),
    Fk_IdEndereco INT NOT NULL,
    FOREIGN KEY (Fk_IdEndereco) REFERENCES tbEnderecos(IdEndereco),
    Fk_IdPag INT NOT NULL,
    FOREIGN KEY (Fk_IdPag) REFERENCES tbPagamentos(IdPag),
    Fk_IdCliente INT NOT NULL,
    FOREIGN KEY (Fk_IdCliente) REFERENCES tbClientes(IdCliente),
    dataPed DATETIME NOT NULL,
    totalPedido DECIMAL(10,2) NOT NULL
);

-- Itens do Pedido
CREATE TABLE tbItemPedido (
    IdProdutoPedido INT PRIMARY KEY AUTO_INCREMENT,
    Fk_IdPedido INT NOT NULL,
    FOREIGN KEY (Fk_IdPedido) REFERENCES tbPedido(IdPedido),
    Fk_IdProduto INT NOT NULL,
    FOREIGN KEY (Fk_IdProduto) REFERENCES tbProdutos(IdProduto),
    PrecoUnitario DECIMAL(10,2) NOT NULL
);
