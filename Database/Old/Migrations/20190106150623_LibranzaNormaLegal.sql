ALTER TABLE [Libranzas] ADD [NormaLegal] nvarchar(max) NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190106150623_LibranzaNormaLegal', N'2.2.0-rtm-35687');

GO

