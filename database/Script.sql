USE Infodengue;
GO

/*
drop table IBGEDadosRelatorios
drop table Relatorios
drop table Solicitantes
drop table IBGEDados
*/

IF NOT EXISTS( SELECT * FROM sys.sysobjects o WHERE o.name = 'Solicitantes' ) 
	CREATE TABLE Solicitantes
	(
		[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
		[Nome] VARCHAR(200) NOT NULL,
		[CPF] VARCHAR(11) NOT NULL
	);
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


IF NOT EXISTS( SELECT * FROM sys.sysobjects o WHERE o.name = 'IBGEDados' ) 
	CREATE TABLE IBGEDados (
		Id BIGINT NOT NULL PRIMARY KEY,
		DataIniSE BIGINT NULL,
		SE INT NULL,
		CasosEst FLOAT NULL,
		CasosEstMin INT NULL,
		CasosEstMax INT NULL,
		Casos INT NULL,
		PRt1 FLOAT NULL,
		PInc100k FLOAT NULL,
		LocalidadeId INT NULL,
		Nivel INT NULL,
		VersaoModelo NVARCHAR(MAX) NULL,
		Tweet FLOAT NULL,
		Rt FLOAT NULL,
		Pop FLOAT NULL,
		TempMin FLOAT NULL,
		UmidMax FLOAT NULL,
		Receptivo INT NULL,
		Transmissao INT NULL,
		NivelInc INT NULL,
		UmidMed FLOAT NULL,
		UmidMin FLOAT NULL,
		TempMed FLOAT NULL,
		TempMax FLOAT NULL,
		CasProv INT NULL,
		CasProvEst INT NULL,
		CasProvEstMin INT NULL,
		CasProvEstMax INT NULL,
		CasConf INT NULL,
		NotifAccumYear INT NULL
	);
GO

IF NOT EXISTS( SELECT * FROM sys.sysobjects o WHERE o.name = 'IBGEDadosRelatorios' ) 
	CREATE TABLE IBGEDadosRelatorios (
		Id BIGINT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
		RelatorioId INT NOT NULL,
		IBGEDadosId BIGINT NOT NULL,
		CONSTRAINT IBGEDadosRelatoriosRelatoriosId FOREIGN KEY (RelatorioId) REFERENCES Relatorios(Id),
		CONSTRAINT IBGEDadosRelatoriosIBGEDadosdId FOREIGN KEY (IBGEDadosId) REFERENCES IBGEDados(Id)
		)



select * from Solicitantes order by id desc

select * from Relatorios order by id desc

select * from IBGEDados order by id desc

select * from IBGEDadosRelatorios order by id desc


