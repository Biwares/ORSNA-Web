IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Adjuntos] (
    [Id] int NOT NULL IDENTITY,
    [Modulo] nvarchar(50) NULL,
    [FechaAlta] datetime NOT NULL,
    [NombreArchivo] nvarchar(500) NULL,
    [Estado] bit NULL DEFAULT (((1))),
    [TipoAnexo] nvarchar(50) NULL,
    CONSTRAINT [PK_Adjuntos] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [AeropuertosGrupo] (
    [Id] int NOT NULL IDENTITY,
    [Nombre] nvarchar(100) NULL,
    [Estado] bit NULL DEFAULT (((1))),
    CONSTRAINT [PK_AeropuertosGrupo] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Areas] (
    [Id] int NOT NULL IDENTITY,
    [Nombre] nvarchar(500) NULL,
    [Codigo] nvarchar(500) NULL,
    [Estado] bit NULL DEFAULT (((1))),
    CONSTRAINT [PK_Areas] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Beneficiarios] (
    [Id] int NOT NULL IDENTITY,
    [RazonSocial] nvarchar(max) NULL,
    [Descripcion] nvarchar(max) NULL,
    [Cuit] nvarchar(50) NULL,
    [FechaAlta] datetime NOT NULL,
    [NacionalExtranjero] varchar(10) NULL,
    [Email] nvarchar(100) NULL,
    [Estado] bit NULL DEFAULT (((1))),
    [Telefono] nvarchar(100) NULL,
    CONSTRAINT [PK_Beneficiarios] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [LibranzasEstado] (
    [Id] int NOT NULL IDENTITY,
    [Nombre] nvarchar(50) NULL,
    [Estado] bit NOT NULL DEFAULT (((1))),
    CONSTRAINT [PK_LibranzasEstado] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [LibranzaTipo] (
    [Id] int NOT NULL IDENTITY,
    [Nombre] nvarchar(50) NULL,
    [Estado] bit NULL DEFAULT (((1))),
    CONSTRAINT [PK_LibranzaTipo] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Log] (
    [Id] int NOT NULL IDENTITY,
    [Ubicacion] nvarchar(50) NOT NULL,
    [Mensaje] nvarchar(500) NOT NULL,
    [Detalle] nvarchar(max) NULL,
    [Fecha] datetime NOT NULL,
    CONSTRAINT [PK_Log] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Modulos] (
    [Id] int NOT NULL IDENTITY,
    [Nombre] nvarchar(50) NULL,
    [Estado] bit NULL DEFAULT (((1))),
    CONSTRAINT [PK_Modulos] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Permisos] (
    [Id] int NOT NULL,
    [Nombre] nvarchar(200) NULL,
    [Key] nvarchar(200) NULL,
    [Estado] bit NULL DEFAULT (((1))),
    CONSTRAINT [PK_Permisos] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Provincias] (
    [Id] int NOT NULL IDENTITY,
    [Nombre] nvarchar(500) NULL,
    [Estado] bit NULL DEFAULT (((1))),
    CONSTRAINT [PK_Provincias] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [ProyectosEstado] (
    [Id] int NOT NULL IDENTITY,
    [Nombre] nvarchar(50) NULL,
    [Estado] bit NULL DEFAULT (((1))),
    CONSTRAINT [PK_ProyectosEstado] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Usuarios] (
    [Id] int NOT NULL IDENTITY,
    [email] nvarchar(200) NULL,
    [password] nvarchar(200) NULL,
    [Estado] bit NULL DEFAULT (((1))),
    CONSTRAINT [PK_Usuarios] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [BeneficiarioAdjuntos] (
    [Id] int NOT NULL IDENTITY,
    [IdBeneficiario] int NULL,
    [IdAdjunto] int NULL,
    [Estado] bit NULL DEFAULT (((1))),
    CONSTRAINT [PK_BeneficiarioAdjuntos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_BeneficiarioAdjuntos_Adjuntos] FOREIGN KEY ([IdAdjunto]) REFERENCES [Adjuntos] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_BeneficiarioAdjuntos_Beneficiarios] FOREIGN KEY ([IdBeneficiario]) REFERENCES [Beneficiarios] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [BeneficiarioBancos] (
    [Id] int NOT NULL IDENTITY,
    [IdBeneficiario] int NULL,
    [TipoCuenta] nvarchar(500) NOT NULL,
    [NroCuenta] nvarchar(500) NULL,
    [NombreBanco] nvarchar(500) NULL,
    [Sucursal] nvarchar(500) NULL,
    [CBU] nvarchar(100) NULL,
    [Cuit] nvarchar(50) NULL,
    [Titular] nvarchar(max) NULL,
    [FechaVigencia] datetime NOT NULL,
    [Estado] bit NULL DEFAULT (((1))),
    [Direccion] nvarchar(500) NULL,
    [BicSwift] nvarchar(100) NULL,
    [EsNacional] float NULL,
    CONSTRAINT [PK_BeneficiarioBancos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_BancoProveedores_Proveedores] FOREIGN KEY ([IdBeneficiario]) REFERENCES [Beneficiarios] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Cuentas] (
    [Id] int NOT NULL IDENTITY,
    [NroCuenta] nvarchar(100) NULL,
    [Nombre] nvarchar(500) NULL,
    [Descripcion] nvarchar(max) NULL,
    [FechaVigencia] datetime NULL,
    [FechaCreacion] datetime NULL,
    [IdLibranzaTipo] int NULL,
    [IdAeropuertosGrupo] int NULL,
    [Estado] bit NULL DEFAULT (((1))),
    CONSTRAINT [PK_Cuentas] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Cuentas_AeropuertosGrupo] FOREIGN KEY ([IdAeropuertosGrupo]) REFERENCES [AeropuertosGrupo] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Cuentas_LibranzaTipo] FOREIGN KEY ([IdLibranzaTipo]) REFERENCES [LibranzaTipo] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [AreasModulos] (
    [Id] int NOT NULL IDENTITY,
    [IdArea] int NOT NULL,
    [IdModulo] int NOT NULL,
    [Estado] bit NOT NULL DEFAULT (((1))),
    CONSTRAINT [PK_AreasModulos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AreasModulos_Areas] FOREIGN KEY ([IdArea]) REFERENCES [Areas] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_AreasModulos_Modulos] FOREIGN KEY ([IdModulo]) REFERENCES [Modulos] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Aeropuertos] (
    [Id] int NOT NULL IDENTITY,
    [Nombre] nvarchar(200) NULL,
    [IdProvincia] int NULL,
    [FechaInicio] datetime NULL,
    [Fechafin] datetime NULL,
    [IdAeropuertosGrupo] int NULL,
    [NombreCorto] nvarchar(500) NULL,
    [CodIata] nvarchar(500) NULL,
    [Estado] bit NULL,
    CONSTRAINT [PK_Aeropuertos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Aeropuertos_AeropuertosGrupo] FOREIGN KEY ([IdAeropuertosGrupo]) REFERENCES [AeropuertosGrupo] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Aeropuertos_Provincias] FOREIGN KEY ([IdProvincia]) REFERENCES [Provincias] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [UsuariosAreas] (
    [Id] int NOT NULL IDENTITY,
    [IdUsuario] int NULL,
    [IdArea] int NULL,
    [Estado] bit NULL DEFAULT (((1))),
    CONSTRAINT [PK_UsuariosAreas] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UsuariosAreas_Areas] FOREIGN KEY ([IdArea]) REFERENCES [Areas] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_UsuariosAreas_Usuarios] FOREIGN KEY ([IdUsuario]) REFERENCES [Usuarios] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Proyectos] (
    [Id] int NOT NULL IDENTITY,
    [Nombre] nvarchar(500) NULL,
    [Descripcion] nvarchar(max) NULL,
    [IdCuenta] int NULL,
    [IdProyecto] nvarchar(500) NULL,
    [IdArea] int NULL,
    [NroContratacion] nvarchar(50) NULL,
    [NroObra] nvarchar(50) NULL,
    [CodObra] nvarchar(500) NULL,
    [NroPago] nvarchar(50) NULL,
    [NormaLegal] nvarchar(500) NULL,
    [Objeto] nvarchar(max) NULL,
    [MontoTotal] decimal(18,2) NULL,
    [FechaCreacion] datetime NOT NULL,
    [TipoEstado] nvarchar(100) NULL,
    [Estado] bit NULL DEFAULT (((1))),
    [IdProyectosEstado] int NULL,
    CONSTRAINT [PK_Proyectos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Proyectos_Areas] FOREIGN KEY ([IdArea]) REFERENCES [Areas] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Proyectos_Cuentas] FOREIGN KEY ([IdCuenta]) REFERENCES [Cuentas] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Proyectos_ProyectosEstado] FOREIGN KEY ([IdProyectosEstado]) REFERENCES [ProyectosEstado] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [AreasModulosPermisos] (
    [Id] int NOT NULL IDENTITY,
    [IdAreaModulo] int NULL,
    [IdPermiso] int NULL,
    [Estado] bit NULL DEFAULT (((1))),
    CONSTRAINT [PK_AreasModulosPermisos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AreasModulosPermisos_AreasModulos] FOREIGN KEY ([IdAreaModulo]) REFERENCES [AreasModulos] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_AreasModulosPermisos_Permisos] FOREIGN KEY ([IdPermiso]) REFERENCES [Permisos] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Libranzas] (
    [Id] int NOT NULL IDENTITY,
    [IdLibranzaTipo] int NULL,
    [NroLibranza] nvarchar(100) NULL,
    [IdProvincia] int NULL,
    [IdProyecto] int NULL,
    [IdLibranzaEstados] int NULL,
    [MontoFondoReparo] decimal(18,2) NULL,
    [Multa] decimal(18,2) NULL,
    [Mora] decimal(18,2) NULL,
    [MontoRestante] decimal(18,2) NULL,
    [Fecha] datetime NOT NULL,
    [Estado] bit NULL DEFAULT (((1))),
    [Sustituido] bit NULL,
    [NroEmbargo] nvarchar(200) NULL,
    [ResponsableEmbargo] nvarchar(200) NULL,
    [MontoEmbargo] decimal(18,2) NULL,
    [SaldoEmbargo] decimal(18,2) NULL,
    [RegistraCesion] bit NULL,
    [NroEscritura] varchar(200) NULL,
    [Beneficiario] varchar(200) NULL,
    [IdentificacionTributaria] varchar(200) NULL,
    CONSTRAINT [PK_Libranzas] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Libranzas_EstadoLibranzas] FOREIGN KEY ([IdLibranzaEstados]) REFERENCES [LibranzasEstado] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Libranzas_Proyectos] FOREIGN KEY ([IdProyecto]) REFERENCES [Proyectos] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [ProyectoAdjuntos] (
    [Id] int NOT NULL IDENTITY,
    [IdProyecto] int NULL,
    [IdAdjunto] int NULL,
    [Estado] bit NULL DEFAULT (((1))),
    CONSTRAINT [PK_ProyectoAdjuntos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProyectoAdjuntos_Adjuntos] FOREIGN KEY ([IdAdjunto]) REFERENCES [Adjuntos] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ProyectoAdjuntos_Proyectos] FOREIGN KEY ([IdProyecto]) REFERENCES [Proyectos] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [ProyectoAeropuertos] (
    [Id] int NOT NULL IDENTITY,
    [IdProyecto] int NULL,
    [IdAeropuerto] int NULL,
    [Estado] bit NULL DEFAULT (((1))),
    CONSTRAINT [PK_ProyectoAeropuertos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProyectoAeropuertos_Proyectos] FOREIGN KEY ([IdProyecto]) REFERENCES [Proyectos] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [ProyectoBeneficiarios] (
    [Id] int NOT NULL IDENTITY,
    [IdProyecto] int NOT NULL,
    [IdBeneficiario] int NULL,
    [Estado] bit NULL DEFAULT (((1))),
    CONSTRAINT [PK_ProyectoBeneficiarios] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProyectoProveedores_Proveedores] FOREIGN KEY ([IdBeneficiario]) REFERENCES [Beneficiarios] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ProyectoProveedores_Proyectos] FOREIGN KEY ([IdProyecto]) REFERENCES [Proyectos] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [ProyectoProvincias] (
    [Id] int NOT NULL IDENTITY,
    [IdProyecto] int NULL,
    [IdProvincia] int NULL,
    [Estado] bit NULL DEFAULT (((1))),
    CONSTRAINT [PK_ProyectoProvincias] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProyectoProvincias_Provincias] FOREIGN KEY ([IdProvincia]) REFERENCES [Provincias] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ProyectoProvincias_Proyectos] FOREIGN KEY ([IdProyecto]) REFERENCES [Proyectos] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [LibranzaAeropuertos] (
    [Id] int NOT NULL IDENTITY,
    [IdLibranza] int NULL,
    [IdAeropuerto] int NULL,
    [Estado] bit NULL DEFAULT (((1))),
    CONSTRAINT [PK_LibranzaAeropuertos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_LibranzaAeropuertos_Aeropuertos] FOREIGN KEY ([IdAeropuerto]) REFERENCES [Aeropuertos] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LibranzaAeropuertos_Libranzas] FOREIGN KEY ([IdLibranza]) REFERENCES [Libranzas] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [LibranzaBeneficiarios] (
    [Id] int NOT NULL IDENTITY,
    [IdLibranzas] int NOT NULL,
    [IdBeneficiario] int NOT NULL,
    [IdBeneficiarioBancos] int NOT NULL,
    [Estado] bit NOT NULL DEFAULT (((1))),
    CONSTRAINT [PK_LibranzaBeneficiarios] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_LibranzaBeneficiarios_Beneficiarios] FOREIGN KEY ([IdBeneficiario]) REFERENCES [Beneficiarios] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LibranzaBeneficiarios_BeneficiarioBancos] FOREIGN KEY ([IdBeneficiarioBancos]) REFERENCES [BeneficiarioBancos] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LibranzaBeneficiarios_Libranzas] FOREIGN KEY ([IdLibranzas]) REFERENCES [Libranzas] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [LibranzaCesiones] (
    [Id] int NOT NULL IDENTITY,
    [IdLibranza] int NULL,
    [Beneficiario] nvarchar(100) NULL,
    [Cuit] nvarchar(50) NULL,
    [RegistraCesion] nvarchar(50) NULL,
    [NroEscritura] nvarchar(50) NULL,
    [Estado] bit NULL DEFAULT (((1))),
    CONSTRAINT [PK_LibranzaCesiones] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_LibranzaCesiones_Libranzas] FOREIGN KEY ([IdLibranza]) REFERENCES [Libranzas] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [LibranzaDetalleWorkflow] (
    [Id] int NOT NULL IDENTITY,
    [IdLibranza] int NOT NULL,
    [Fecha] datetime NOT NULL,
    [IdUsuario] int NOT NULL,
    [Observaciones] nvarchar(max) NOT NULL,
    [IdEstadoAnterior] int NULL,
    [IdNuevoEstado] int NULL,
    CONSTRAINT [PK_LibranzaDetalleWorkflow] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_LibranzaDetalleWorkflow_LibranzasEstado] FOREIGN KEY ([IdEstadoAnterior]) REFERENCES [LibranzasEstado] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LibranzaDetalleWorkflow_Libranzas] FOREIGN KEY ([IdLibranza]) REFERENCES [Libranzas] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LibranzaDetalleWorkflow_LibranzasEstado1] FOREIGN KEY ([IdNuevoEstado]) REFERENCES [LibranzasEstado] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [LibranzaEmbargos] (
    [Id] int NOT NULL IDENTITY,
    [IdLibranza] int NULL,
    [NroEmbargo] nvarchar(50) NULL,
    [Responsable] nvarchar(100) NULL,
    [Monto] decimal(18,2) NULL,
    [Saldo] decimal(18,2) NULL,
    [Estado] bit NOT NULL DEFAULT (((1))),
    CONSTRAINT [PK_LibranzaEmbargos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_LibranzaEmbargos_Libranzas] FOREIGN KEY ([IdLibranza]) REFERENCES [Libranzas] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [LibranzaFacturas] (
    [Id] int NOT NULL IDENTITY,
    [IdLibranza] int NULL,
    [Tipo] nvarchar(50) NULL,
    [Nro] nvarchar(50) NULL,
    [Fecha] datetime NOT NULL,
    [Monto] decimal(18,2) NULL,
    [Iva] decimal(18,2) NULL,
    [Estado] bit NULL DEFAULT (((1))),
    [Ibb] decimal(18,2) NULL,
    CONSTRAINT [PK_LibranzaFacturas] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_LibranzaFacturas_Libranzas] FOREIGN KEY ([IdLibranza]) REFERENCES [Libranzas] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_Aeropuertos_IdAeropuertosGrupo] ON [Aeropuertos] ([IdAeropuertosGrupo]);

GO

CREATE INDEX [IX_Aeropuertos_IdProvincia] ON [Aeropuertos] ([IdProvincia]);

GO

CREATE INDEX [IX_AreasModulos_IdArea] ON [AreasModulos] ([IdArea]);

GO

CREATE INDEX [IX_AreasModulos_IdModulo] ON [AreasModulos] ([IdModulo]);

GO

CREATE INDEX [IX_AreasModulosPermisos_IdAreaModulo] ON [AreasModulosPermisos] ([IdAreaModulo]);

GO

CREATE INDEX [IX_AreasModulosPermisos_IdPermiso] ON [AreasModulosPermisos] ([IdPermiso]);

GO

CREATE INDEX [IX_BeneficiarioAdjuntos_IdAdjunto] ON [BeneficiarioAdjuntos] ([IdAdjunto]);

GO

CREATE INDEX [IX_BeneficiarioAdjuntos_IdBeneficiario] ON [BeneficiarioAdjuntos] ([IdBeneficiario]);

GO

CREATE INDEX [IX_BeneficiarioBancos_IdBeneficiario] ON [BeneficiarioBancos] ([IdBeneficiario]);

GO

CREATE INDEX [IX_Cuentas_IdAeropuertosGrupo] ON [Cuentas] ([IdAeropuertosGrupo]);

GO

CREATE INDEX [IX_Cuentas_IdLibranzaTipo] ON [Cuentas] ([IdLibranzaTipo]);

GO

CREATE INDEX [IX_LibranzaAeropuertos_IdAeropuerto] ON [LibranzaAeropuertos] ([IdAeropuerto]);

GO

CREATE INDEX [IX_LibranzaAeropuertos_IdLibranza] ON [LibranzaAeropuertos] ([IdLibranza]);

GO

CREATE INDEX [IX_LibranzaBeneficiarios_IdBeneficiario] ON [LibranzaBeneficiarios] ([IdBeneficiario]);

GO

CREATE INDEX [IX_LibranzaBeneficiarios_IdBeneficiarioBancos] ON [LibranzaBeneficiarios] ([IdBeneficiarioBancos]);

GO

CREATE INDEX [IX_LibranzaBeneficiarios_IdLibranzas] ON [LibranzaBeneficiarios] ([IdLibranzas]);

GO

CREATE INDEX [IX_LibranzaCesiones_IdLibranza] ON [LibranzaCesiones] ([IdLibranza]);

GO

CREATE INDEX [IX_LibranzaDetalleWorkflow_IdEstadoAnterior] ON [LibranzaDetalleWorkflow] ([IdEstadoAnterior]);

GO

CREATE INDEX [IX_LibranzaDetalleWorkflow_IdLibranza] ON [LibranzaDetalleWorkflow] ([IdLibranza]);

GO

CREATE INDEX [IX_LibranzaDetalleWorkflow_IdNuevoEstado] ON [LibranzaDetalleWorkflow] ([IdNuevoEstado]);

GO

CREATE INDEX [IX_LibranzaEmbargos_IdLibranza] ON [LibranzaEmbargos] ([IdLibranza]);

GO

CREATE INDEX [IX_LibranzaFacturas_IdLibranza] ON [LibranzaFacturas] ([IdLibranza]);

GO

CREATE INDEX [IX_Libranzas_IdLibranzaEstados] ON [Libranzas] ([IdLibranzaEstados]);

GO

CREATE INDEX [IX_Libranzas_IdProyecto] ON [Libranzas] ([IdProyecto]);

GO

CREATE INDEX [IX_ProyectoAdjuntos_IdAdjunto] ON [ProyectoAdjuntos] ([IdAdjunto]);

GO

CREATE INDEX [IX_ProyectoAdjuntos_IdProyecto] ON [ProyectoAdjuntos] ([IdProyecto]);

GO

CREATE INDEX [IX_ProyectoAeropuertos_IdProyecto] ON [ProyectoAeropuertos] ([IdProyecto]);

GO

CREATE INDEX [IX_ProyectoBeneficiarios_IdBeneficiario] ON [ProyectoBeneficiarios] ([IdBeneficiario]);

GO

CREATE INDEX [IX_ProyectoBeneficiarios_IdProyecto] ON [ProyectoBeneficiarios] ([IdProyecto]);

GO

CREATE INDEX [IX_ProyectoProvincias_IdProvincia] ON [ProyectoProvincias] ([IdProvincia]);

GO

CREATE INDEX [IX_ProyectoProvincias_IdProyecto] ON [ProyectoProvincias] ([IdProyecto]);

GO

CREATE INDEX [IX_Proyectos_IdArea] ON [Proyectos] ([IdArea]);

GO

CREATE INDEX [IX_Proyectos_IdCuenta] ON [Proyectos] ([IdCuenta]);

GO

CREATE INDEX [IX_Proyectos_IdProyectosEstado] ON [Proyectos] ([IdProyectosEstado]);

GO

CREATE INDEX [IX_UsuariosAreas_IdArea] ON [UsuariosAreas] ([IdArea]);

GO

CREATE INDEX [IX_UsuariosAreas_IdUsuario] ON [UsuariosAreas] ([IdUsuario]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20181217150836_InitialCreate', N'2.2.0-rtm-35687');

GO