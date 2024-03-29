USE Shelf
/*
TRUNCATE TABLE Atendimento
TRUNCATE TABLE Versao
TRUNCATE TABLE Aplicativo
TRUNCATE TABLE Categoria
TRUNCATE TABLE Problema
TRUNCATE TABLE Item
TRUNCATE TABLE AtendimentoDetalhado
TRUNCATE TABLE SubCategoria
TRUNCATE TABLE Protocolo

DROP TABLE Atendimento
DROP TABLE Versao
DROP TABLE Aplicativo
DROP TABLE Categoria
DROP TABLE Problema
DROP TABLE Item
DROP TABLE AtendimentoDetalhado
DROP TABLE SubCategoria
DROP TABLE Protocolo
*/

CREATE TABLE Categoria
(
IDCategoria BIGINT IDENTITY(1,1) PRIMARY KEY,
Nome VARCHAR(80) NOT NULL UNIQUE
)

CREATE TABLE SubCategoria
(
IDSubCategoria BIGINT IDENTITY(1,1) PRIMARY KEY,
Nome VARCHAR(80) NOT NULL,
Categoria_ID BIGINT NOT NULL FOREIGN KEY
REFERENCES Categoria(IDCategoria)
)

CREATE TABLE Item
(
IDItem BIGINT IDENTITY(1,1) PRIMARY KEY,
Nome VARCHAR(80) NOT NULL,
SubCategoria_ID BIGINT NOT NULL FOREIGN KEY
REFERENCES SubCategoria(IDSubCategoria)
)

CREATE TABLE Aplicativo
(
IDAplicativo BIGINT IDENTITY(1,1) PRIMARY KEY,
Descricao VARCHAR(80) UNIQUE NOT NULL,

)

CREATE TABLE Versao
(
IDVersao BIGINT IDENTITY(1,1) PRIMARY KEY,
Aplicativo_ID BIGINT FOREIGN KEY
REFERENCES Aplicativo(IDAplicativo),
Versao VARCHAR(11)
)

CREATE TABLE Protocolo
(
IDProtocolo BIGINT IDENTITY(1,1) PRIMARY KEY,
Protocolo BIGINT,
Tipo INT
)

CREATE TABLE Atendimento
(
IDAtendimento BIGINT IDENTITY(1,1) PRIMARY KEY,
Problema VARCHAR(255),
Protocolo_ID BIGINT FOREIGN KEY
REFERENCES Protocolo(IDProtocolo),
Cliente_ID BIGINT NOT NULL FOREIGN KEY
REFERENCES Cliente(IDCliente)
)

CREATE TABLE Problema
(
IDProblema BIGINT IDENTITY(1,1) PRIMARY KEY,
Categoria_ID BIGINT NOT NULL FOREIGN KEY
REFERENCES Categoria(IDCategoria),
SubCategoria_ID BIGINT NOT NULL FOREIGN KEY
REFERENCES SubCategoria(IDSubCategoria),
Item_ID BIGINT NOT NULL FOREIGN KEY
REFERENCES Item(IDItem),
Aplicativo_ID BIGINT NOT NULL FOREIGN KEY
REFERENCES Aplicativo(IDAplicativo),
Versao_ID BIGINT NOT NULL FOREIGN KEY
REFERENCES Versao(IDVersao),
Atendimento_ID BIGINT NOT NULL FOREIGN KEY
REFERENCES Atendimento(IDAtendimento),
)

CREATE TABLE AtendimentoDetalhado
(
IDAtendimentoDetalhado BIGINT IDENTITY(1,1) PRIMARY KEY,
Atendimento_ID BIGINT NOT NULL FOREIGN KEY
REFERENCES Atendimento(IDAtendimento),
StatusAtendimento BIGINT NOT NULL,
Funcionario_ID BIGINT NOT NULL FOREIGN KEY
REFERENCES FuncionarioLogin(ID),
DataInicio DATETIME,
DataFinal DATETIME,
Solucao VARCHAR(255),
Contato VARCHAR(80),
)