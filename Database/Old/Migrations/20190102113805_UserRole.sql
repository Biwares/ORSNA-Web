ALTER TABLE [Modulos] ADD [RolModuloId] int NULL;

GO

CREATE TABLE [RolModulo] (
    [Id] int NOT NULL IDENTITY,
    [IdRol] int NOT NULL,
    [IdModulo] int NOT NULL,
    [Ver] bit NOT NULL,
    [Editar] bit NOT NULL,
    [Eliminar] bit NOT NULL,
    CONSTRAINT [PK_RolModulo] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [UsuarioRol] (
    [Id] int NOT NULL IDENTITY,
    [IdUsuario] int NOT NULL,
    [IdRol] int NOT NULL,
    [UsuariosId] int NULL,
    CONSTRAINT [PK_UsuarioRol] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UsuarioRol_Usuarios_UsuariosId] FOREIGN KEY ([UsuariosId]) REFERENCES [Usuarios] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Rol] (
    [Id] int NOT NULL IDENTITY,
    [Nombre] nvarchar(max) NULL,
    [Estado] bit NULL,
    [RolModuloId] int NULL,
    CONSTRAINT [PK_Rol] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Rol_RolModulo_RolModuloId] FOREIGN KEY ([RolModuloId]) REFERENCES [RolModulo] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_Modulos_RolModuloId] ON [Modulos] ([RolModuloId]);

GO

CREATE INDEX [IX_Rol_RolModuloId] ON [Rol] ([RolModuloId]);

GO

CREATE INDEX [IX_UsuarioRol_UsuariosId] ON [UsuarioRol] ([UsuariosId]);

GO

ALTER TABLE [Modulos] ADD CONSTRAINT [FK_Modulos_RolModulo_RolModuloId] FOREIGN KEY ([RolModuloId]) REFERENCES [RolModulo] ([Id]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190102113805_UserRole', N'2.2.0-rtm-35687');

GO

