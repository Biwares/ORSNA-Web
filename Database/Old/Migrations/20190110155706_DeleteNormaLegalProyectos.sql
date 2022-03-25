DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Proyectos]') AND [c].[name] = N'NormaLegal');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Proyectos] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Proyectos] DROP COLUMN [NormaLegal];

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190110155706_DeleteNormaLegalProyectos', N'2.2.0-rtm-35687');

GO