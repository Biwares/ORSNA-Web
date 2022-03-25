ALTER TABLE [UsuarioRol] DROP CONSTRAINT [FK_UsuarioRol_Usuarios_UsuariosId];

GO

DROP INDEX [IX_UsuarioRol_UsuariosId] ON [UsuarioRol];

GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[UsuarioRol]') AND [c].[name] = N'UsuariosId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [UsuarioRol] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [UsuarioRol] DROP COLUMN [UsuariosId];

GO

CREATE INDEX [IX_UsuarioRol_IdRol] ON [UsuarioRol] ([IdRol]);

GO

CREATE INDEX [IX_UsuarioRol_IdUsuario] ON [UsuarioRol] ([IdUsuario]);

GO

ALTER TABLE [UsuarioRol] ADD CONSTRAINT [FK_UsuarioRol_Rol_IdRol] FOREIGN KEY ([IdRol]) REFERENCES [Rol] ([Id]) ON DELETE CASCADE;

GO

ALTER TABLE [UsuarioRol] ADD CONSTRAINT [FK_UsuarioRol_Usuarios_IdUsuario] FOREIGN KEY ([IdUsuario]) REFERENCES [Usuarios] ([Id]) ON DELETE CASCADE;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190103165830_FKUsuarios', N'2.2.0-rtm-35687');

GO

