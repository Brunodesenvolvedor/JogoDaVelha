-- Tabela de Partidas
CREATE TABLE Partidas (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Vencedor NVARCHAR(50),
    DataHora DATETIME
);

-- Tabela de Jogadas
CREATE TABLE Jogadas (
    Id INT PRIMARY KEY IDENTITY(1,1),
    PartidaId INT,
    Jogador NVARCHAR(50),
    Posicao INT,
    Ordem INT,
    FOREIGN KEY (PartidaId) REFERENCES Partidas(Id)
);
