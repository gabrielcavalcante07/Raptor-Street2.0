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

-- Marcas
INSERT INTO `tbmarcaproduto` (`NomeMarca`) VALUES ('Nike');
INSERT INTO `tbmarcaproduto` (`NomeMarca`) VALUES ('Adidas');
INSERT INTO `tbmarcaproduto` (`NomeMarca`) VALUES ('Puma');
INSERT INTO `tbmarcaproduto` (`NomeMarca`) VALUES ('Mizuno');
INSERT INTO `tbmarcaproduto` (`NomeMarca`) VALUES ('Vans');

-- Produtos
INSERT INTO tbProdutos (ImagemProduto, NomeProduto, PrecoProduto, Descricao, Fk_IdMarca, QuantidadeProd) VALUES
-- Adidas
('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/ADIDAS-ADI2000.png', 'Adidas ADI2000', 649.90, 'Tênis com design retrô e visual robusto, ideal para quem busca estilo e conforto no dia a dia.', 2, 20),
('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/ADIDAS-ADIFOM.png', 'Adidas Adifom', 719.90, 'Modelo futurista da Adidas com cabedal em espuma moldada, oferecendo leveza e impacto visual único.', 2, 20),
('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/ADIDAS-CAMPUS-00s-PV.png', 'Adidas Campus 00s PV', 579.90, 'Edição especial do clássico Campus com detalhes modernos e materiais premium.', 2, 20),
('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/ADIDAS-CAMPUS00s.png', 'Adidas Campus 00s', 549.90, 'Releitura dos anos 2000 com design robusto e solado reforçado, mantendo o conforto clássico da linha.', 2, 20),
('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/ADIDAS-FORUM.png', 'Adidas Forum', 599.90, 'Tênis icônico da Adidas, mistura perfeita entre herança do basquete e estilo urbano.', 2, 20),
('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/ADIDAS-JAPAN.png', 'Adidas Japan', 499.90, 'Silhueta inspirada no estilo japonês, com linhas limpas e acabamento refinado.', 2, 20),
('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/ADIDAS-OG.png', 'Adidas OG', 519.90, 'Modelo clássico original com toques modernos e materiais duráveis.', 2, 20),
('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/ADIDAS-SAMBAOG.png', 'Adidas Samba OG', 449.90, 'Tênis atemporal da Adidas com visual esportivo retrô e conforto excepcional.', 2, 20),
('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/ADIDAS-SAMBAXLG.png', 'Adidas Samba XLG', 479.90, 'Versão modernizada do Samba com entressola elevada e toque urbano.', 2, 20),
('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/ADIDAS-SUPERSTAR.png', 'Adidas Superstar', 599.90, 'Ícone global com biqueira shell-toe e visual inconfundível, ideal para todos os estilos.', 2, 20),

-- Mizuno
('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/MIZUNO-CONTENDER-AZL.png', 'Mizuno Contender Azul', 489.90, 'Modelo leve e versátil com visual retrô running, ideal para uso casual.', 4, 20),
('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/MIZUNO-CONTENDER.png', 'Mizuno Contender', 459.90, 'Tênis casual com inspiração nos modelos clássicos de corrida da Mizuno.', 4, 20),
('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/MIZUNO-MUJIN.png', 'Mizuno Mujin', 749.90, 'Tênis robusto de trilha, com tração reforçada e amortecimento para aventuras extremas.', 4, 20),
('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/MIZUNO-MXR.png', 'Mizuno MXR', 699.90, 'Design esportivo com tecnologias de amortecimento avançadas para o máximo desempenho.', 4, 20),
('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/MIZUNO-PROPHECY-DOU.png', 'Mizuno Prophecy Dourado', 1199.90, 'Edição sofisticada do Prophecy com visual marcante e sistema Infinity Wave.', 4, 20),
('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/MIZUNO-PROPHECYLS-CNZ.png', 'Mizuno Prophecy LS Cinza', 1149.90, 'Tênis premium com design moderno e construção resistente para longos treinos.', 4, 20),
('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/MIZUNO-PROPHECYLS-VRD.png', 'Mizuno Prophecy LS Verde', 1149.90, 'Visual arrojado com cabedal respirável e amortecimento de alto nível.', 4, 20),
('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/MIZUNO-PROPHECYLS.png', 'Mizuno Prophecy LS', 1129.90, 'Modelo de alta performance com tecnologia Wave para máximo conforto e estabilidade.', 4, 20),
('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/MIZUNO-WAVERIDER-YOKAI.png', 'Mizuno Wave Rider Yokai', 899.90, 'Edição especial inspirada em lendas japonesas, combinando leveza e velocidade.', 4, 20),
('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/MIZUNO-WAVERIDER.png', 'Mizuno Wave Rider', 869.90, 'Tênis versátil para corrida com ótimo suporte e responsividade.', 4, 20),

-- Nike
('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/NIKE-AIR-ZOOM-SPIRIDON.png', 'Nike Air Zoom Spiridon', 749.90, 'Design inovador com amortecimento responsivo e estilo esportivo.', 1, 20),
('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/NIKE-AIRFORCE1.png', 'Nike Air Force 1', 699.90, 'Ícone do streetwear com silhueta clássica e durabilidade incomparável.', 1, 20),
('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/NIKE-DN-RS.png', 'Nike Downshifter RS', 499.90, 'Tênis com foco em leveza e suporte, ideal para corridas leves e treinos.', 1, 20),
('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/NIKE-DN-VRD.png', 'Nike Downshifter Verde', 499.90, 'Modelo casual com toque esportivo e cores vibrantes.', 1, 20),
('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/NIKE-DN.png', 'Nike Downshifter', 489.90, 'Tênis versátil com cabedal respirável e ótimo custo-benefício.', 1, 20),
('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/NIKE-DN8.png', 'Nike Downshifter 8', 519.90, 'Atualização do modelo clássico com amortecimento melhorado.', 1, 20),
('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/NIKE-SHOX4.png', 'Nike Shox 4', 899.90, 'Tênis com tecnologia Shox de molas que oferece impulso e estilo futurista.', 1, 20),
('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/NIKE-TN.png', 'Nike TN', 1149.90, 'Silhueta ousada e marcante, com amortecimento Tuned Air.', 1, 20),
('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/NIKE-UPTEMPO.png', 'Nike Uptempo', 1129.90, 'Tênis imponente com visual anos 90 e máximo conforto.', 1, 20),
('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/NIKE-VOMERO.png', 'Nike Vomero', 999.90, 'Modelo de alta performance com foco em amortecimento para corridas longas.', 1, 20),
('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/Nike-Air-Max-Tailwind-IV.png', 'Nike Air Max Tailwind IV', 949.90, 'Modelo com cápsulas de ar visíveis e estética agressiva dos anos 2000.', 1, 20),

-- Puma
('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/PUMA EXTOS.png', 'Puma Extos', 499.90, 'Tênis de visual urbano com construção leve e acabamento moderno.', 3, 20),
('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/PUMA-180.png', 'Puma 180', 489.90, 'Design robusto com influência dos anos 90 e toque contemporâneo.', 3, 20),
('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/PUMA-INHALE.png', 'Puma Inhale', 599.90, 'Modelo com linhas futuristas e ótima respirabilidade para uso diário.', 3, 20),
('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/PUMA-LAFRANCE.png', 'Puma La France', 639.90, 'Tênis com estilo europeu e detalhes refinados no acabamento.', 3, 20),
('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/PUMA-RSX.png', 'Puma RS-X', 679.90, 'Silhueta chunky com entressola confortável e visual marcante.', 3, 20),
('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/PUMA-SPEEDCAT.png', 'Puma Speedcat', 469.90, 'Modelo inspirado nas pistas de corrida com perfil baixo e ajuste preciso.', 3, 20),
('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/PUMA-SUEDE.png', 'Puma Suede', 449.90, 'Clássico da marca com cabedal de camurça e visual atemporal.', 3, 20),
('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/PUMA-VERITANA.png', 'Puma Veritana', 519.90, 'Tênis moderno com design clean e solado robusto para o dia a dia.', 3, 20),
('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/PUMA2.png', 'Puma Modelo 2', 489.90, 'Design casual com cabedal flexível e ótimo conforto.', 3, 20),
('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/PUMA3LD.png', 'Puma 3LD', 499.90, 'Tênis estiloso com mix de materiais e detalhes exclusivos.', 3, 20),

-- Vans
('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/VANS-HYLANE.png', 'Vans Hylane', 469.90, 'Tênis de skate com solado vulcanizado e design minimalista.', 5, 20),
('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/VANS-KNU.png', 'Vans Knu', 499.90, 'Modelo robusto com detalhes retrô e identidade forte.', 5, 20),
('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/VANS-MID.png', 'Vans Mid', 459.90, 'Versão cano médio do clássico Vans, oferecendo suporte e estilo.', 5, 20),
('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/VANS-OLD.png', 'Vans Old Skool', 429.90, 'Tênis clássico da Vans com listra lateral icônica e sola waffle.', 5, 20),
('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/VANS-PLATFORM.png', 'Vans Platform', 479.90, 'Silhueta elevada com base reforçada e visual moderno.', 5, 20),
('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/VANS-SK8.png', 'Vans Sk8-Hi', 499.90, 'Tênis de cano alto ideal para skate e estilo urbano.', 5, 20),
('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/VANS-SKOOL.png', 'Vans Skool', 429.90, 'Versão casual com design inspirado no Old Skool e conforto garantido.', 5, 20),
('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/VANS-ULTRARANGE.png', 'Vans UltraRange', 519.90, 'Tênis leve e resistente para aventuras urbanas e caminhadas leves.', 5, 20),
('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/VANS-Ua.png', 'Vans UA', 449.90, 'Modelo versátil da Vans com design moderno e ótimo ajuste.', 5, 20),
('https://raw.githubusercontent.com/gabrielcavalcante07/tenisRaptor/refs/heads/main/VANSLD.png', 'Vans LD', 439.90, 'Tênis casual com visual limpo e solado vulcanizado.', 5, 20);
	