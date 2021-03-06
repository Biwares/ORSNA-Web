/*
   Saturday, December 15, 20189:45:39 AM
   User: rsaadmin
   Server: 10.230.20.21
   Database: OrsnaDatabaseDesa
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.LibranzasEstado SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Libranzas SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.LibranzaDetalleWorkflow
	(
	Id int NOT NULL IDENTITY (1, 1),
	IdLibranza int NOT NULL,
	Fecha datetime NOT NULL,
	IdUsuario int NOT NULL,
	Observaciones nvarchar(MAX) NOT NULL,
	IdEstadoAnterior int NULL,
	IdNuevoEstado int NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE dbo.LibranzaDetalleWorkflow ADD CONSTRAINT
	PK_LibranzaDetalleWorkflow PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.LibranzaDetalleWorkflow ADD CONSTRAINT
	FK_LibranzaDetalleWorkflow_Libranzas FOREIGN KEY
	(
	IdLibranza
	) REFERENCES dbo.Libranzas
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.LibranzaDetalleWorkflow ADD CONSTRAINT
	FK_LibranzaDetalleWorkflow_LibranzasEstado FOREIGN KEY
	(
	IdEstadoAnterior
	) REFERENCES dbo.LibranzasEstado
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.LibranzaDetalleWorkflow ADD CONSTRAINT
	FK_LibranzaDetalleWorkflow_LibranzasEstado1 FOREIGN KEY
	(
	IdNuevoEstado
	) REFERENCES dbo.LibranzasEstado
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.LibranzaDetalleWorkflow SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
