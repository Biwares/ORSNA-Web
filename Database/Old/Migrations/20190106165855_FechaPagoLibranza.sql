ALTER TABLE [Libranzas] ADD [FechaPago] datetime2 NULL;

GO

ALTER TABLE [LibranzaDetalleWorkflow] ADD [FechaPago] datetime2 NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190106165855_FechaPagoLibranza', N'2.2.0-rtm-35687');

GO
