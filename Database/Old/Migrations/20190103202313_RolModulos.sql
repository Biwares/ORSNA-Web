INSERT INTO [dbo].[Modulos]([Nombre],[Estado],[RolModuloId])VALUES('Libranzas de Pago',1,null)

GO

INSERT INTO [dbo].[Modulos]([Nombre],[Estado],[RolModuloId])VALUES('Beneficiarios',1,null)

GO

INSERT INTO [dbo].[Modulos]([Nombre],[Estado],[RolModuloId])VALUES('Cuentas',1,null)

GO

INSERT INTO [dbo].[Modulos]([Nombre],[Estado],[RolModuloId])VALUES('Proyectos',1,null)

GO

INSERT INTO [dbo].[Modulos]([Nombre],[Estado],[RolModuloId])VALUES('Áreas',1,null)

GO

INSERT INTO [dbo].[Modulos]([Nombre],[Estado],[RolModuloId])VALUES('Auditoría',1,null)

GO

INSERT INTO [dbo].[Modulos]([Nombre],[Estado],[RolModuloId])VALUES('Usuarios',1,null)

GO

INSERT INTO [dbo].[Modulos]([Nombre],[Estado],[RolModuloId])VALUES('Roles',1,null)

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190103184002_InsertModulos', N'2.2.0-rtm-35687');

GO

ALTER TABLE [Modulos] DROP CONSTRAINT [FK_Modulos_RolModulo_RolModuloId];

GO

ALTER TABLE [Rol] DROP CONSTRAINT [FK_Rol_RolModulo_RolModuloId];

GO

DROP INDEX [IX_Rol_RolModuloId] ON [Rol];

GO

DROP INDEX [IX_Modulos_RolModuloId] ON [Modulos];

GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Rol]') AND [c].[name] = N'RolModuloId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Rol] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Rol] DROP COLUMN [RolModuloId];

GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Modulos]') AND [c].[name] = N'RolModuloId');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Modulos] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Modulos] DROP COLUMN [RolModuloId];

GO

CREATE INDEX [IX_RolModulo_IdModulo] ON [RolModulo] ([IdModulo]);

GO

CREATE INDEX [IX_RolModulo_IdRol] ON [RolModulo] ([IdRol]);

GO

ALTER TABLE [RolModulo] ADD CONSTRAINT [FK_RolModulo_Modulos_IdModulo] FOREIGN KEY ([IdModulo]) REFERENCES [Modulos] ([Id]) ON DELETE CASCADE;

GO

ALTER TABLE [RolModulo] ADD CONSTRAINT [FK_RolModulo_Rol_IdRol] FOREIGN KEY ([IdRol]) REFERENCES [Rol] ([Id]) ON DELETE CASCADE;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190103202313_RolModulos', N'2.2.0-rtm-35687');

GO

