use OrsnaDatabase
GO
ALTER TABLE dbo.Libranzas
	DROP COLUMN TipoFondoReparo
GO
ALTER TABLE dbo.Libranzas SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
GO
ALTER TABLE dbo.Libranzas ADD
	Sustituido bit NULL,
	NroEmbargo nvarchar(200) NULL,
	ResponsableEmbargo nvarchar(200) NULL,
	MontoEmbargo decimal(18, 2) NULL,
	SaldoEmbargo decimal(18, 2) NULL,
	RegistraCesion bit NULL,
	NroEscritura varchar(200) NULL,
	Beneficiario varchar(200) NULL,
	IdentificacionTributaria varchar(200) NULL
GO
ALTER TABLE dbo.Libranzas SET (LOCK_ESCALATION = TABLE)
GO