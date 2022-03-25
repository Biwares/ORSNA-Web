ALTER TABLE [Modulos] ADD [Icono] nvarchar(max) NULL;

GO

ALTER TABLE [Modulos] ADD [RouterLink] nvarchar(max) NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190106130232_ModuloCaracteristicas', N'2.2.0-rtm-35687');

GO

update [dbo].[modulos] set routerlink = 'libranzas', icono = 'fa fa-money' where nombre = 'Libranzas de Pago'

GO

update [dbo].[modulos] set routerlink = 'beneficiarios', icono = 'fa fa-fw  fa-plus-square' where nombre = 'Beneficiarios'

GO

update [dbo].[modulos] set routerlink = 'cuentas', icono = 'fa fa-users' where nombre = 'Cuentas'

GO

update [dbo].[modulos] set routerlink = 'proyectos', icono = 'fa fa-folder-open' where nombre = 'Proyectos'

GO

update [dbo].[modulos] set routerlink = 'areas', icono = 'fa fa-download' where nombre = 'Áreas'

GO

update [dbo].[modulos] set routerlink = 'audit', icono = 'fa fa-file-text-o' where nombre = 'Auditoría'

GO

update [dbo].[modulos] set routerlink = 'usuarios', icono = 'fa fa-user-circle-o' where nombre = 'Usuarios'

GO

update [dbo].[modulos] set routerlink = 'roles', icono = 'fa fa-user-circle-o' where nombre = 'Roles'

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190106130254_ModuloCaracteristicasInsert', N'2.2.0-rtm-35687');

GO

