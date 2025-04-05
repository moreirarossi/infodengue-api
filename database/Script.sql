USE Infodengue;
GO

IF NOT EXISTS( SELECT * FROM sys.sysobjects o WHERE o.name = 'Relatorios' ) 
	CREATE TABLE Relatorios
	(
		[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
		[SolicitanteId] INT NOT NULL,
		[DataSolicitacao] DATETIME NOT NULL DEFAULT(GETDATE()),
		[Arbovirose] VARCHAR(MAX) NULL,
		[SemanaInicio] DATETIME NULL,
		[SemanaTermino] DATETIME NULL,
		[CodigoIBGE] VARCHAR(MAX) NULL,
		[Municipio] VARCHAR(MAX) NULL,
		[TotalCasos] INT NULL,
		CONSTRAINT FKRelatoriosSolicitante FOREIGN KEY (SolicitanteId) REFERENCES Solicitantes(Id)
	);
GO

IF NOT EXISTS( SELECT * FROM sys.sysobjects o WHERE o.name = 'Solicitantes' ) 
	CREATE TABLE Solicitantes
	(
		[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
		[Nome] VARCHAR(200) NOT NULL,
		[CPF] VARCHAR(11) NOT NULL
	);
GO

select * from Solicitantes

select * from Relatorios

