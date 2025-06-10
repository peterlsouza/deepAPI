CREATE TABLE Agendamentos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ProfissionalId INT NOT NULL,
    ServicoId INT NOT NULL,
    DataHora DATETIME NOT NULL,
    Observacoes NVARCHAR(500),
    Status NVARCHAR(20) DEFAULT 'Agendado',
    DataCriacao DATETIME DEFAULT GETDATE(),
    
    FOREIGN KEY (ProfissionalId) REFERENCES Profissionais(Id),
    FOREIGN KEY (ServicoId) REFERENCES Servicos(Id),
    
    CONSTRAINT CHK_Status CHECK (Status IN ('Agendado', 'Cancelado', 'Concluído'))
);

CREATE INDEX IX_Agendamentos_ProfissionalData ON Agendamentos(ProfissionalId, DataHora);