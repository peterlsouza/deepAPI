-- 1. Cria��o do banco de dados (se n�o existir)
IF NOT EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = 'ProfissionaisServicosDB')
BEGIN
    CREATE DATABASE ProfissionaisServicosDB;
    PRINT 'Banco de dados ProfissionaisServicosDB criado com sucesso.';
END
ELSE
BEGIN
    PRINT 'Banco de dados ProfissionaisServicosDB j� existe.';
END
GO

-- 2. Usar o banco de dados criado
USE ProfissionaisServicosDB;
GO

-- 3. Cria��o da tabela Profissionais
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Profissionais')
BEGIN
    CREATE TABLE Profissionais (
        Id INT PRIMARY KEY IDENTITY(1,1),
        Nome NVARCHAR(100) NOT NULL,
        Email NVARCHAR(100) NOT NULL,
        Telefone NVARCHAR(20),
        Especialidade NVARCHAR(100),
        DataCadastro DATETIME DEFAULT GETDATE(),
        Ativo BIT DEFAULT 1,
        
        CONSTRAINT UQ_Profissionais_Email UNIQUE (Email)
    );
    
    PRINT 'Tabela Profissionais criada com sucesso.';
END
ELSE
BEGIN
    PRINT 'Tabela Profissionais j� existe.';
END
GO

-- 4. Cria��o da tabela Servicos
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Servicos')
BEGIN
    CREATE TABLE Servicos (
        Id INT PRIMARY KEY IDENTITY(1,1),
        Nome NVARCHAR(100) NOT NULL,
        Descricao NVARCHAR(500),
        Preco DECIMAL(10, 2) NOT NULL,
        DuracaoMinutos INT NOT NULL,
        DataCadastro DATETIME DEFAULT GETDATE(),
        Ativo BIT DEFAULT 1,
        
        CONSTRAINT CK_Servicos_Preco CHECK (Preco >= 0),
        CONSTRAINT CK_Servicos_Duracao CHECK (DuracaoMinutos > 0)
    );
    
    PRINT 'Tabela Servicos criada com sucesso.';
END
ELSE
BEGIN
    PRINT 'Tabela Servicos j� existe.';
END
GO

-- 5. Cria��o de �ndices para melhor performance
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Profissionais_Nome')
BEGIN
    CREATE INDEX IX_Profissionais_Nome ON Profissionais(Nome);
END

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Servicos_Nome')
BEGIN
    CREATE INDEX IX_Servicos_Nome ON Servicos(Nome);
END
GO

---- 6. Inser��o de dados iniciais (opcional)
---- Profissionais
--IF NOT EXISTS (SELECT 1 FROM Profissionais)
--BEGIN
--    INSERT INTO Profissionais (Nome, Email, Telefone, Especialidade)
--    VALUES 
--        ('Jo�o Silva', 'joao@exemplo.com', '(11) 9999-8888', 'Cardiologia'),
--        ('Maria Santos', 'maria@exemplo.com', '(11) 9777-6666', 'Dermatologia'),
--        ('Carlos Oliveira', 'carlos@exemplo.com', '(11) 9555-4444', 'Ortopedia');
    
--    PRINT 'Dados iniciais de Profissionais inseridos.';
--END

---- Servicos
--IF NOT EXISTS (SELECT 1 FROM Servicos)
--BEGIN
--    INSERT INTO Servicos (Nome, Descricao, Preco, DuracaoMinutos)
--    VALUES 
--        ('Consulta B�sica', 'Consulta m�dica geral', 150.00, 30),
--        ('Exame Cardiol�gico', 'Avalia��o completa da sa�de card�aca', 350.00, 60),
--        ('Limpeza de Pele', 'Procedimento est�tico facial', 200.00, 45);
    
--    PRINT 'Dados iniciais de Servicos inseridos.';
--END
--GO

---- 7. Cria��o de uma stored procedure de exemplo
--IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetProfissionaisAtivos')
--BEGIN
--    EXEC('
--    CREATE PROCEDURE sp_GetProfissionaisAtivos
--    AS
--    BEGIN
--        SELECT Id, Nome, Email, Especialidade 
--        FROM Profissionais 
--        WHERE Ativo = 1
--        ORDER BY Nome;
--    END
--    ');
--    PRINT 'Stored procedure sp_GetProfissionaisAtivos criada.';
--END
--GO

PRINT 'Script executado com sucesso!';
GO