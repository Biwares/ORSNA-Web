USE [master]
USE [OrsnaDatabase]
GO
/****** Object:  Table [dbo].[Adjuntos]    Script Date: 11/25/2018 11:36:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Adjuntos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Modulo] [nvarchar](50) NULL,
	[FechaAlta] [datetime] NOT NULL,
	[NombreArchivo] [nvarchar](500) NULL,
	[Estado] [bit] NULL,
	[TipoAnexo] [nvarchar](50) NULL,
 CONSTRAINT [PK_Adjuntos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Aeropuertos]    Script Date: 11/25/2018 11:36:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Aeropuertos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](200) NULL,
	[IdProvincia] [int] NULL,
	[FechaInicio] [datetime] NULL,
	[Fechafin] [datetime] NULL,
	[IdAeropuertosGrupo] [int] NULL,
	[NombreCorto] [nvarchar](500) NULL,
	[CodIata] [nvarchar](500) NULL,
	[Estado] [bit] NULL,
 CONSTRAINT [PK_Aeropuertos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AeropuertosGrupo]    Script Date: 11/25/2018 11:36:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AeropuertosGrupo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](100) NULL,
	[Estado] [bit] NULL,
 CONSTRAINT [PK_AeropuertosGrupo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Areas]    Script Date: 11/25/2018 11:36:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Areas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](500) NULL,
	[Codigo] [nvarchar](500) NULL,
	[Estado] [bit] NULL,
 CONSTRAINT [PK_Areas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AreasModulos]    Script Date: 11/25/2018 11:36:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AreasModulos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdArea] [int] NOT NULL,
	[IdModulo] [int] NOT NULL,
	[Estado] [bit] NOT NULL,
 CONSTRAINT [PK_AreasModulos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AreasModulosPermisos]    Script Date: 11/25/2018 11:36:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AreasModulosPermisos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdAreaModulo] [int] NULL,
	[IdPermiso] [int] NULL,
	[Estado] [bit] NULL,
 CONSTRAINT [PK_AreasModulosPermisos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BeneficiarioAdjuntos]    Script Date: 11/25/2018 11:36:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BeneficiarioAdjuntos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdBeneficiario] [int] NULL,
	[IdAdjunto] [int] NULL,
	[Estado] [bit] NULL,
 CONSTRAINT [PK_AdjuntoProveedores] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BeneficiarioBancos]    Script Date: 11/25/2018 11:36:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BeneficiarioBancos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdBeneficiario] [int] NULL,
	[TipoCuenta] [nvarchar](500) NOT NULL,
	[NroCuenta] [nvarchar](500) NULL,
	[NombreBanco] [nvarchar](500) NULL,
	[Sucursal] [nvarchar](500) NULL,
	[CBU] [nvarchar](100) NULL,
	[Titular] [nvarchar](max) NULL,
	[FechaVigencia] [datetime] NOT NULL,
	[Estado] [bit] NULL,
	[Direccion] [nvarchar](500) NULL,
	[BicSwift] [nvarchar](100) NULL,
	[EsNacional] [float] NULL,
 CONSTRAINT [PK_BancoProveedores] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Beneficiarios]    Script Date: 11/25/2018 11:36:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Beneficiarios](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RazonSocial] [nvarchar](max) NULL,
	[Descripcion] [nvarchar](max) NULL,
	[Cuit] [nvarchar](50) NULL,
	[FechaAlta] [datetime] NOT NULL,
	[NacionalExtranjero] [char](10) NULL,
	[Email] [nvarchar](100) NULL,
	[Estado] [bit] NULL,
	[Telefono] [nvarchar](100) NULL,
 CONSTRAINT [PK_Proveedores] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cuentas]    Script Date: 11/25/2018 11:36:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cuentas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NroCuenta] [nvarchar](100) NULL,
	[Nombre] [nvarchar](500) NULL,
	[Descripcion] [nvarchar](max) NULL,
	[FechaVigencia] [datetime] NULL,
	[FechaCreacion] [datetime] NULL,
	[IdLibranzaTipo] [int] NULL,
	[IdAeropuertosGrupo] [int] NULL,
	[Estado] [bit] NULL,
 CONSTRAINT [PK_Cuentas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LibranzaAeropuertos]    Script Date: 11/25/2018 11:36:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LibranzaAeropuertos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdLibranza] [int] NULL,
	[IdAeropuerto] [int] NULL,
	[Estado] [bit] NULL,
 CONSTRAINT [PK_LibranzaAeropuertos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LibranzaBeneficiarios]    Script Date: 11/25/2018 11:36:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LibranzaBeneficiarios](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdLibranzas] [int] NOT NULL,
	[IdBeneficiario] [int] NOT NULL,
	[IdBeneficiarioBancos] [int] NOT NULL,
	[Estado] [bit] NOT NULL,
 CONSTRAINT [PK_LibranzaBeneficiarios] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LibranzaCesiones]    Script Date: 11/25/2018 11:36:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LibranzaCesiones](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdLibranza] [int] NULL,
	[Beneficiario] [nvarchar](100) NULL,
	[Cuit] [nvarchar](50) NULL,
	[RegistraCesion] [nvarchar](50) NULL,
	[NroEscritura] [nvarchar](50) NULL,
	[Estado] [bit] NULL,
 CONSTRAINT [PK_LibranzaCesiones] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LibranzaEmbargos]    Script Date: 11/25/2018 11:36:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LibranzaEmbargos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdLibranza] [int] NULL,
	[NroEmbargo] [nvarchar](50) NULL,
	[Responsable] [nvarchar](100) NULL,
	[Monto] [decimal](18, 2) NULL,
	[Saldo] [decimal](18, 2) NULL,
	[Estado] [bit] NOT NULL,
 CONSTRAINT [PK_LibranzaEmbargos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LibranzaFacturas]    Script Date: 11/25/2018 11:36:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LibranzaFacturas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdLibranza] [int] NULL,
	[Tipo] [nvarchar](50) NULL,
	[Nro] [nvarchar](50) NULL,
	[Fecha] [datetime] NOT NULL,
	[Monto] [decimal](18, 2) NULL,
	[Iva] [decimal](18, 2) NULL,
	[Estado] [bit] NULL,
	[Ibb] [decimal](18, 2) NULL,
 CONSTRAINT [PK_LibranzaFacturas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Libranzas]    Script Date: 11/25/2018 11:36:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Libranzas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdLibranzaTipo] [int] NULL,
	[NroLibranza] [nvarchar](100) NULL,
	[IdProvincia] [int] NULL,
	[IdProyecto] [int] NULL,
	[IdLibranzaEstados] [int] NULL,
	[MontoFondoReparo] [decimal](18, 2) NULL,
	[Multa] [decimal](18, 2) NULL,
	[Mora] [decimal](18, 2) NULL,
	[MontoRestante] [decimal](18, 2) NULL,
	[Fecha] [datetime] NOT NULL,
	[Estado] [bit] NULL,
	[Sustituido] [bit] NULL,
	[NroEmbargo] [nvarchar](200) NULL,
	[ResponsableEmbargo] [nvarchar](200) NULL,
	[MontoEmbargo] [decimal](18, 2) NULL,
	[SaldoEmbargo] [decimal](18, 2) NULL,
	[RegistraCesion] [bit] NULL,
	[NroEscritura] [varchar](200) NULL,
	[Beneficiario] [varchar](200) NULL,
	[IdentificacionTributaria] [varchar](200) NULL,
 CONSTRAINT [PK_Libranzas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LibranzasEstado]    Script Date: 11/25/2018 11:36:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LibranzasEstado](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NULL,
	[Estado] [bit] NOT NULL,
 CONSTRAINT [PK_EstadoLibranzas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LibranzaTipo]    Script Date: 11/25/2018 11:36:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LibranzaTipo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NULL,
	[Estado] [bit] NULL,
 CONSTRAINT [PK_LibranzaTipo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Log]    Script Date: 11/25/2018 11:36:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Log](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Ubicacion] [nvarchar](50) NOT NULL,
	[Mensaje] [nvarchar](500) NOT NULL,
	[Detalle] [nvarchar](max) NULL,
	[Fecha] [datetime] NOT NULL,
 CONSTRAINT [PK_Log] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Modulos]    Script Date: 11/25/2018 11:36:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Modulos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NULL,
	[Estado] [bit] NULL,
 CONSTRAINT [PK_Modulos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Permisos]    Script Date: 11/25/2018 11:36:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permisos](
	[Id] [int] NOT NULL,
	[Nombre] [nvarchar](200) NULL,
	[Key] [nvarchar](200) NULL,
	[Estado] [bit] NULL,
 CONSTRAINT [PK_Permisos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Provincias]    Script Date: 11/25/2018 11:36:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Provincias](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](500) NULL,
	[Estado] [bit] NULL,
 CONSTRAINT [PK_Provincias] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProyectoAdjuntos]    Script Date: 11/25/2018 11:36:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProyectoAdjuntos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdProyecto] [int] NULL,
	[IdAdjunto] [int] NULL,
	[Estado] [bit] NULL,
 CONSTRAINT [PK_ProyectoAdjuntos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProyectoAeropuertos]    Script Date: 11/25/2018 11:36:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProyectoAeropuertos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdProyecto] [int] NULL,
	[IdAeropuerto] [int] NULL,
	[Estado] [bit] NULL,
 CONSTRAINT [PK_ProyectoAeropuertos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProyectoBeneficiarios]    Script Date: 11/25/2018 11:36:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProyectoBeneficiarios](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdProyecto] [int] NOT NULL,
	[IdBeneficiario] [int] NULL,
	[Estado] [bit] NULL,
 CONSTRAINT [PK_ProyectoProveedores] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProyectoProvincias]    Script Date: 11/25/2018 11:36:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProyectoProvincias](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdProyecto] [int] NULL,
	[IdProvincia] [int] NULL,
	[Estado] [bit] NULL,
 CONSTRAINT [PK_ProyectoProvincias] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Proyectos]    Script Date: 11/25/2018 11:36:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Proyectos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](500) NULL,
	[Descripcion] [nvarchar](max) NULL,
	[IdCuenta] [int] NULL,
	[IdProyecto] [nvarchar](500) NULL,
	[IdArea] [int] NULL,
	[NroContratacion] [nvarchar](50) NULL,
	[NroObra] [nvarchar](50) NULL,
	[CodObra] [nvarchar](500) NULL,
	[NroPago] [nvarchar](50) NULL,
	[NormaLegal] [nvarchar](500) NULL,
	[Objeto] [nvarchar](max) NULL,
	[MontoTotal] [decimal](18, 2) NULL,
	[FechaCreacion] [datetime] NOT NULL,
	[TipoEstado] [nvarchar](100) NULL,
	[Estado] [bit] NULL,
	[IdProyectosEstado] [int] NULL,
 CONSTRAINT [PK_Proyectos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProyectosEstado]    Script Date: 11/25/2018 11:36:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProyectosEstado](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NULL,
	[Estado] [bit] NULL,
 CONSTRAINT [PK_ProyectosGrupo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 11/25/2018 11:36:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[email] [nvarchar](200) NULL,
	[password] [nvarchar](200) NULL,
	[Estado] [bit] NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsuariosAreas]    Script Date: 11/25/2018 11:36:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsuariosAreas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdUsuario] [int] NULL,
	[IdArea] [int] NULL,
	[Estado] [bit] NULL,
 CONSTRAINT [PK_UsuariosAreas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Adjuntos] ON 

INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (12, N'Beneficiario', CAST(N'2018-11-06T14:10:20.890' AS DateTime), N'AsistenciaFirmas_curso nuevo asd  Fecha de inicio_ 07_06_2018 Horario_ Tardes Instructor_ Wladimir Stelo  _ Nombre instructor Apellido_2018_8_9 (1).xls', 1, NULL)
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (13, N'Beneficiario', CAST(N'2018-11-06T15:55:07.373' AS DateTime), N'AutorizacionCapacitacion_Administrador._2018_8_10.pdf', 1, NULL)
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (14, N'Beneficiario', CAST(N'2018-11-06T16:59:36.497' AS DateTime), N'924af13664568cba4b56d40562a9bf82.jpg', 1, NULL)
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (15, N'Beneficiario', CAST(N'2018-11-06T17:51:17.320' AS DateTime), N'comprobante-operacion-113922688 (1).pdf', 1, NULL)
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (16, N'Beneficiario', CAST(N'2018-11-06T17:51:50.587' AS DateTime), N'CURRICULO BETSA.doc', 1, NULL)
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (17, N'Beneficiario', CAST(N'2018-11-09T14:00:32.917' AS DateTime), N'libranzas-pago.png', 1, NULL)
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (18, N'Proyecto', CAST(N'2018-11-15T12:48:56.230' AS DateTime), N'cod-moz.pdf', 1, N'2')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (19, N'Proyecto', CAST(N'2018-11-15T12:56:55.990' AS DateTime), N'datos de .config', 1, N'2')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (20, N'Proyecto', CAST(N'2018-11-15T13:03:52.553' AS DateTime), N'minuta de reunion.docx', 1, N'2')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (21, N'Proyecto', CAST(N'2018-11-15T13:09:43.090' AS DateTime), N'c2d816f8-ba4e-4735-9309-cc154859c43f.jpg', 1, N'2')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (22, N'Proyecto', CAST(N'2018-11-15T13:33:29.683' AS DateTime), N'imprimircertificado.do.pdf', 1, N'2')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (23, N'Proyecto', CAST(N'2018-11-15T13:49:26.153' AS DateTime), N'manual de instructivo.docx', 1, N'2')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (24, N'Proyecto', CAST(N'2018-11-15T14:47:11.583' AS DateTime), N'cef_100_percent.pak', 1, N'1')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (25, N'Proyecto', CAST(N'2018-11-15T16:16:22.027' AS DateTime), N'debug.log', 1, N'3')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (26, N'Proyecto', CAST(N'2018-11-15T17:41:44.157' AS DateTime), N'libGLESv2.dll', 1, N'3')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (27, N'Proyecto', CAST(N'2018-11-15T21:16:07.127' AS DateTime), N'icudtl.dat', 1, N'2')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (28, N'Proyecto', CAST(N'2018-11-15T21:32:12.027' AS DateTime), N'cef_100_percent.pak', 1, N'2')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (29, N'Proyecto', CAST(N'2018-11-15T21:32:53.143' AS DateTime), N'chrome_elf.dll', 1, N'1')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (30, N'Proyecto', CAST(N'2018-11-15T21:33:20.810' AS DateTime), N'd3dcompiler_43.dll', 1, N'3')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (31, N'Proyecto', CAST(N'2018-11-15T21:48:37.013' AS DateTime), N'prefs', 1, N'1')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (32, N'Proyecto', CAST(N'2018-11-16T14:26:08.723' AS DateTime), N'datos de .config', 1, N'2')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (33, N'Proyecto', CAST(N'2018-11-16T14:30:23.410' AS DateTime), N'Log.txt', 1, N'1')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (34, N'Proyecto', CAST(N'2018-11-16T15:18:21.177' AS DateTime), N'Maquetado de Acciones Masivas (1).docx', 1, N'2')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (35, N'Proyecto', CAST(N'2018-11-16T15:22:15.353' AS DateTime), N'bapro.PNG', 1, N'2')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (36, N'Proyecto', CAST(N'2018-11-16T18:17:33.700' AS DateTime), N'Cycle Dump.txt', 1, N'2')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (37, N'Proyecto', CAST(N'2018-11-16T18:18:43.543' AS DateTime), N'Log.txt', 1, N'2')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (38, N'Proyecto', CAST(N'2018-11-16T18:28:53.487' AS DateTime), N'Cycle Dump.txt', 1, N'2')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (39, N'Proyecto', CAST(N'2018-11-16T18:50:02.423' AS DateTime), N'crash_reporter.cfg', 1, N'2')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (40, N'Proyecto', CAST(N'2018-11-16T19:33:39.313' AS DateTime), N'chrome_elf.dll', 1, N'2')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (41, N'Proyecto', CAST(N'2018-11-16T20:42:51.490' AS DateTime), N'chrome_elf.dll', 1, N'1')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (42, N'Proyecto', CAST(N'2018-11-16T20:49:30.467' AS DateTime), N'libGLESv2.dll', 1, N'3')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (43, N'Proyecto', CAST(N'2018-11-16T20:53:15.727' AS DateTime), N'cef_200_percent.pak', 1, N'3')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (44, N'Proyecto', CAST(N'2018-11-16T20:55:56.017' AS DateTime), N'cef_200_percent.pak', 1, N'3')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (45, N'Proyecto', CAST(N'2018-11-16T20:56:22.213' AS DateTime), N'debug.log', 1, N'3')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (46, N'Proyecto', CAST(N'2018-11-16T20:57:31.397' AS DateTime), N'chrome_elf.dll', 1, N'2')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (47, N'Proyecto', CAST(N'2018-11-16T20:58:46.087' AS DateTime), N'chrome_elf.dll', 1, N'1')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (48, N'Proyecto', CAST(N'2018-11-16T20:59:06.123' AS DateTime), N'libEGL.dll', 0, N'3')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (49, N'Proyecto', CAST(N'2018-11-18T13:18:14.100' AS DateTime), N'cef_extensions.pak', 0, N'3')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (50, N'Proyecto', CAST(N'2018-11-18T13:24:16.677' AS DateTime), N'd3dcompiler_43.dll', 1, N'1')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (51, N'Proyecto', CAST(N'2018-11-18T13:24:53.200' AS DateTime), N'icudtl.dat', 1, N'1')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (52, N'Proyecto', CAST(N'2018-11-18T13:25:08.160' AS DateTime), N'libGLESv2.dll', 0, N'3')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (53, N'Proyecto', CAST(N'2018-11-18T13:25:49.880' AS DateTime), N'natives_blob.bin', 1, N'2')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (54, N'Proyecto', CAST(N'2018-11-20T09:49:55.203' AS DateTime), N'pista.jpg', 1, N'2')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (55, N'Proyecto', CAST(N'2018-11-20T10:01:38.993' AS DateTime), N'pista.jpg', 1, N'2')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (56, N'Proyecto', CAST(N'2018-11-20T10:13:44.530' AS DateTime), N'pista.jpg', 1, N'2')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (57, N'Proyecto', CAST(N'2018-11-20T10:16:54.313' AS DateTime), N'pista.jpg', 0, N'3')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (58, N'Proyecto', CAST(N'2018-11-20T10:17:31.833' AS DateTime), N'pista.jpg', 1, N'1')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (59, N'Proyecto', CAST(N'2018-11-20T10:17:53.013' AS DateTime), N'pista.jpg', 0, N'3')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (60, N'Proyecto', CAST(N'2018-11-20T10:18:09.147' AS DateTime), N'pista.jpg', 0, N'3')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (61, N'Proyecto', CAST(N'2018-11-20T10:19:51.100' AS DateTime), N'aeroparquepista.jpg', 0, N'3')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (62, N'Proyecto', CAST(N'2018-11-20T11:16:31.500' AS DateTime), N'fe67e9582a8146b9a598d7452940ceda.jpg', 0, N'3')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (63, N'Proyecto', CAST(N'2018-11-20T16:53:04.490' AS DateTime), N'Cycle Dump.txt', 1, N'2')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (64, N'Proyecto', CAST(N'2018-11-20T16:59:57.973' AS DateTime), N'Plane Maker.exe', 0, N'3')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (65, N'Proyecto', CAST(N'2018-11-20T17:05:00.847' AS DateTime), N'Boeing Mid (high subsonic).afl', 0, N'3')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (66, N'Proyecto', CAST(N'2018-11-20T17:07:02.553' AS DateTime), N'NACA 2412 (popular) cuffed.afl', 0, N'3')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (67, N'Proyecto', CAST(N'2018-11-20T17:18:35.217' AS DateTime), N'How to install.txt', 0, N'3')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (68, N'Proyecto', CAST(N'2018-11-20T17:22:09.540' AS DateTime), N'b738.acf', 0, N'3')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (69, N'Proyecto', CAST(N'2018-11-20T17:35:08.410' AS DateTime), N'fms-0.fml', 0, N'3')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (70, N'Proyecto', CAST(N'2018-11-20T17:49:03.803' AS DateTime), N'Cycle Dump.txt', 0, N'3')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (71, N'Proyecto', CAST(N'2018-11-20T18:20:56.603' AS DateTime), N'Readme.txt', 0, N'3')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (72, N'Proyecto', CAST(N'2018-11-20T18:22:51.173' AS DateTime), N'Readme.txt', 0, N'3')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (73, N'Proyecto', CAST(N'2018-11-20T18:27:40.830' AS DateTime), N'Log.txt', 1, N'3')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (74, N'Proyecto', CAST(N'2018-11-20T18:28:04.480' AS DateTime), N'How to install.txt', 1, N'3')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (75, N'Proyecto', CAST(N'2018-11-20T18:32:32.147' AS DateTime), N'Log.txt', 0, N'3')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (76, N'Proyecto', CAST(N'2018-11-20T19:25:20.883' AS DateTime), N'Cycle Dump.txt', 1, N'2')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (77, N'Proyecto', CAST(N'2018-11-20T19:27:30.213' AS DateTime), N'How to install.txt', 0, N'3')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (78, N'Proyecto', CAST(N'2018-11-20T19:27:44.847' AS DateTime), N'Plane Maker.exe', 1, N'3')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (79, N'Proyecto', CAST(N'2018-11-21T16:56:41.200' AS DateTime), N'RETOQUE TEMPLATE Con Modif. Solicitadas - revisado por Marcelo Griego.docx', 0, N'3')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (80, N'Proyecto', CAST(N'2018-11-21T16:56:54.207' AS DateTime), N'sanjusto.sql', 1, N'3')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (81, N'Proyecto', CAST(N'2018-11-22T14:02:17.207' AS DateTime), N'cef_100_percent.pak', 1, N'3')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (82, N'Beneficiario', CAST(N'2018-11-23T11:19:17.903' AS DateTime), N'fe67e9582a8146b9a598d7452940ceda.jpg', 1, NULL)
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (83, N'Beneficiario', CAST(N'2018-11-23T11:51:00.987' AS DateTime), N'CARGAR.docx', 1, NULL)
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (84, N'Beneficiario', CAST(N'2018-11-23T12:10:45.480' AS DateTime), N'CARGAR.docx', 1, NULL)
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (85, N'Proyecto', CAST(N'2018-11-23T13:15:28.223' AS DateTime), N'CARGAR.docx', 1, N'2')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (86, N'Proyecto', CAST(N'2018-11-23T13:28:10.973' AS DateTime), N'CARGAR.docx', 1, N'2')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (87, N'Proyecto', CAST(N'2018-11-23T14:04:10.677' AS DateTime), N'PERMISOS.docx', 1, N'1')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (88, N'Proyecto', CAST(N'2018-11-25T09:03:51.970' AS DateTime), N'PERMISOS.docx', 1, N'2')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (89, N'Proyecto', CAST(N'2018-11-25T09:26:43.237' AS DateTime), N'Prueba Anexo II.docx', 1, N'2')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (90, N'Proyecto', CAST(N'2018-11-25T09:31:30.157' AS DateTime), N'Prueba Anexo II.docx', 1, N'2')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (91, N'Proyecto', CAST(N'2018-11-25T09:36:06.437' AS DateTime), N'Prueba Anexo I.docx', 1, N'1')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (92, N'Proyecto', CAST(N'2018-11-25T09:37:08.727' AS DateTime), N'Prueba Anexo II.docx', 1, N'1')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (93, N'Proyecto', CAST(N'2018-11-25T09:44:49.787' AS DateTime), N'Prueba Anexo II.docx', 1, N'2')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (94, N'Proyecto', CAST(N'2018-11-25T09:48:48.237' AS DateTime), N'Prueba Anexo I.docx', 1, N'1')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (95, N'Proyecto', CAST(N'2018-11-25T09:51:23.777' AS DateTime), N'Prueba otro Anexo I.docx', 1, N'1')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (96, N'Proyecto', CAST(N'2018-11-25T10:18:19.760' AS DateTime), N'Prueba.txt', 0, N'3')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (97, N'Proyecto', CAST(N'2018-11-25T10:21:08.177' AS DateTime), N'Prueba.txt', 0, N'3')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (98, N'Proyecto', CAST(N'2018-11-25T10:22:01.133' AS DateTime), N'Prueba.txt', 0, N'3')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (99, N'Proyecto', CAST(N'2018-11-25T10:22:15.420' AS DateTime), N'Prueba.txt', 0, N'3')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (100, N'Proyecto', CAST(N'2018-11-25T10:22:32.607' AS DateTime), N'Prueba.txt', 1, N'3')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (101, N'Proyecto', CAST(N'2018-11-25T10:22:34.137' AS DateTime), N'Prueba.txt', 1, N'3')
INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (102, N'Beneficiario', CAST(N'2018-11-25T11:22:55.227' AS DateTime), N'DB.csproj.nuget.cache', 1, NULL)
SET IDENTITY_INSERT [dbo].[Adjuntos] OFF
SET IDENTITY_INSERT [dbo].[Aeropuertos] ON 

INSERT [dbo].[Aeropuertos] ([Id], [Nombre], [IdProvincia], [FechaInicio], [Fechafin], [IdAeropuertosGrupo], [NombreCorto], [CodIata], [Estado]) VALUES (1, N'Ezeiza', 1, CAST(N'2018-10-02T00:00:00.000' AS DateTime), CAST(N'2018-10-02T00:00:00.000' AS DateTime), 1, N'EZE', N'EZP123', 1)
INSERT [dbo].[Aeropuertos] ([Id], [Nombre], [IdProvincia], [FechaInicio], [Fechafin], [IdAeropuertosGrupo], [NombreCorto], [CodIata], [Estado]) VALUES (2, N'Ezpeleta', 1, CAST(N'2018-10-02T00:00:00.000' AS DateTime), CAST(N'2018-10-02T00:00:00.000' AS DateTime), 1, N'EZP', N'EZP123', 1)
INSERT [dbo].[Aeropuertos] ([Id], [Nombre], [IdProvincia], [FechaInicio], [Fechafin], [IdAeropuertosGrupo], [NombreCorto], [CodIata], [Estado]) VALUES (3, N'Iguazu', 1, CAST(N'2018-10-02T00:00:00.000' AS DateTime), CAST(N'2018-10-02T00:00:00.000' AS DateTime), 1, N'IGU', N'IGU123', 1)
SET IDENTITY_INSERT [dbo].[Aeropuertos] OFF
SET IDENTITY_INSERT [dbo].[AeropuertosGrupo] ON 

INSERT [dbo].[AeropuertosGrupo] ([Id], [Nombre], [Estado]) VALUES (1, N'GrupoA', 1)
INSERT [dbo].[AeropuertosGrupo] ([Id], [Nombre], [Estado]) VALUES (2, N'GrupoB', 1)
INSERT [dbo].[AeropuertosGrupo] ([Id], [Nombre], [Estado]) VALUES (3, N'SNA', 1)
SET IDENTITY_INSERT [dbo].[AeropuertosGrupo] OFF
SET IDENTITY_INSERT [dbo].[Areas] ON 

INSERT [dbo].[Areas] ([Id], [Nombre], [Codigo], [Estado]) VALUES (18, N'12a', N'1234', 0)
INSERT [dbo].[Areas] ([Id], [Nombre], [Codigo], [Estado]) VALUES (19, N'123', N'123', 0)
INSERT [dbo].[Areas] ([Id], [Nombre], [Codigo], [Estado]) VALUES (20, N'ttters', N'4444', 0)
INSERT [dbo].[Areas] ([Id], [Nombre], [Codigo], [Estado]) VALUES (21, N'Contabilidad1001', N'1001', 1)
INSERT [dbo].[Areas] ([Id], [Nombre], [Codigo], [Estado]) VALUES (22, N'Contabilidad200', N'200', 1)
INSERT [dbo].[Areas] ([Id], [Nombre], [Codigo], [Estado]) VALUES (23, N'Contabilidad300', N'300', 1)
INSERT [dbo].[Areas] ([Id], [Nombre], [Codigo], [Estado]) VALUES (24, N'Contabilidad400', N'400', 1)
INSERT [dbo].[Areas] ([Id], [Nombre], [Codigo], [Estado]) VALUES (25, N'Contabilidad500', N'500', 1)
INSERT [dbo].[Areas] ([Id], [Nombre], [Codigo], [Estado]) VALUES (26, N'Contabilidad600', N'600', 1)
INSERT [dbo].[Areas] ([Id], [Nombre], [Codigo], [Estado]) VALUES (27, N'Contabilidad700', N'700', 1)
INSERT [dbo].[Areas] ([Id], [Nombre], [Codigo], [Estado]) VALUES (28, N'Contabilidad800', N'800', 0)
INSERT [dbo].[Areas] ([Id], [Nombre], [Codigo], [Estado]) VALUES (29, N'sdrfghj', N'3456789', 0)
INSERT [dbo].[Areas] ([Id], [Nombre], [Codigo], [Estado]) VALUES (30, N'sdfghj', N'2345678', 0)
INSERT [dbo].[Areas] ([Id], [Nombre], [Codigo], [Estado]) VALUES (31, N'Prueba Area', N'13', 0)
INSERT [dbo].[Areas] ([Id], [Nombre], [Codigo], [Estado]) VALUES (32, N'Nueva Area Modificada', N'16', 0)
INSERT [dbo].[Areas] ([Id], [Nombre], [Codigo], [Estado]) VALUES (33, N'Prueba  Mariela 2', N'20', 1)
INSERT [dbo].[Areas] ([Id], [Nombre], [Codigo], [Estado]) VALUES (34, N'Contabilidad 01', N'CONT01', 1)
SET IDENTITY_INSERT [dbo].[Areas] OFF
SET IDENTITY_INSERT [dbo].[BeneficiarioAdjuntos] ON 

INSERT [dbo].[BeneficiarioAdjuntos] ([Id], [IdBeneficiario], [IdAdjunto], [Estado]) VALUES (7, 8, 12, 1)
INSERT [dbo].[BeneficiarioAdjuntos] ([Id], [IdBeneficiario], [IdAdjunto], [Estado]) VALUES (8, 9, 13, 1)
INSERT [dbo].[BeneficiarioAdjuntos] ([Id], [IdBeneficiario], [IdAdjunto], [Estado]) VALUES (9, 8, 14, 1)
INSERT [dbo].[BeneficiarioAdjuntos] ([Id], [IdBeneficiario], [IdAdjunto], [Estado]) VALUES (10, 8, 15, 1)
INSERT [dbo].[BeneficiarioAdjuntos] ([Id], [IdBeneficiario], [IdAdjunto], [Estado]) VALUES (11, 8, 16, 1)
INSERT [dbo].[BeneficiarioAdjuntos] ([Id], [IdBeneficiario], [IdAdjunto], [Estado]) VALUES (12, 12, 17, 1)
INSERT [dbo].[BeneficiarioAdjuntos] ([Id], [IdBeneficiario], [IdAdjunto], [Estado]) VALUES (13, 18, 82, 1)
INSERT [dbo].[BeneficiarioAdjuntos] ([Id], [IdBeneficiario], [IdAdjunto], [Estado]) VALUES (14, 19, 83, 1)
INSERT [dbo].[BeneficiarioAdjuntos] ([Id], [IdBeneficiario], [IdAdjunto], [Estado]) VALUES (15, 20, 84, 1)
INSERT [dbo].[BeneficiarioAdjuntos] ([Id], [IdBeneficiario], [IdAdjunto], [Estado]) VALUES (16, 24, 102, 1)
SET IDENTITY_INSERT [dbo].[BeneficiarioAdjuntos] OFF
SET IDENTITY_INSERT [dbo].[BeneficiarioBancos] ON 

INSERT [dbo].[BeneficiarioBancos] ([Id], [IdBeneficiario], [TipoCuenta], [NroCuenta], [NombreBanco], [Sucursal], [CBU], [Titular], [FechaVigencia], [Estado], [Direccion], [BicSwift], [EsNacional]) VALUES (7, 8, N'Ahorro', N'nro cuenta', N'nombre', N'sucursal', N'cbu', N'titular', CAST(N'2018-10-02T00:00:00.000' AS DateTime), 1, NULL, NULL, NULL)
INSERT [dbo].[BeneficiarioBancos] ([Id], [IdBeneficiario], [TipoCuenta], [NroCuenta], [NombreBanco], [Sucursal], [CBU], [Titular], [FechaVigencia], [Estado], [Direccion], [BicSwift], [EsNacional]) VALUES (8, 8, N'Ahorro', N'nrocuenta1', N'banco', N'sucualr', N'cbu1', N'titular', CAST(N'2018-10-02T00:00:00.000' AS DateTime), 1, NULL, NULL, NULL)
INSERT [dbo].[BeneficiarioBancos] ([Id], [IdBeneficiario], [TipoCuenta], [NroCuenta], [NombreBanco], [Sucursal], [CBU], [Titular], [FechaVigencia], [Estado], [Direccion], [BicSwift], [EsNacional]) VALUES (9, 9, N'Ahorro', N'nro cuenta', N'nombre banco', N'sucursal', N'cbu123', N'titular', CAST(N'2018-10-09T00:00:00.000' AS DateTime), 1, NULL, NULL, NULL)
INSERT [dbo].[BeneficiarioBancos] ([Id], [IdBeneficiario], [TipoCuenta], [NroCuenta], [NombreBanco], [Sucursal], [CBU], [Titular], [FechaVigencia], [Estado], [Direccion], [BicSwift], [EsNacional]) VALUES (10, NULL, N'Corriente', N'123212', N'Sofitasa', N'San Cristobal', N'cbunueva12', N'2212', CAST(N'2018-11-06T00:00:00.000' AS DateTime), 1, NULL, NULL, NULL)
INSERT [dbo].[BeneficiarioBancos] ([Id], [IdBeneficiario], [TipoCuenta], [NroCuenta], [NombreBanco], [Sucursal], [CBU], [Titular], [FechaVigencia], [Estado], [Direccion], [BicSwift], [EsNacional]) VALUES (11, NULL, N'Ahorro', N'a sdas d asd', N'asdfdsafas das', N' asd asd as', N' asd asdas ', N' asd asd asd as', CAST(N'2018-11-06T00:00:00.000' AS DateTime), 1, NULL, NULL, NULL)
INSERT [dbo].[BeneficiarioBancos] ([Id], [IdBeneficiario], [TipoCuenta], [NroCuenta], [NombreBanco], [Sucursal], [CBU], [Titular], [FechaVigencia], [Estado], [Direccion], [BicSwift], [EsNacional]) VALUES (12, 9, N'Ahorro', N'asdas das', N'asdasdas', N' asda sd as', N'asd asd as as', N' asd asd as as', CAST(N'2018-11-06T00:00:00.000' AS DateTime), 1, NULL, NULL, NULL)
INSERT [dbo].[BeneficiarioBancos] ([Id], [IdBeneficiario], [TipoCuenta], [NroCuenta], [NombreBanco], [Sucursal], [CBU], [Titular], [FechaVigencia], [Estado], [Direccion], [BicSwift], [EsNacional]) VALUES (13, 9, N'Corriente', N'sdfgh', N'sdfds', N'sadfgh', N'sadfgh', N'dsfgh', CAST(N'2018-11-06T00:00:00.000' AS DateTime), 1, NULL, NULL, NULL)
INSERT [dbo].[BeneficiarioBancos] ([Id], [IdBeneficiario], [TipoCuenta], [NroCuenta], [NombreBanco], [Sucursal], [CBU], [Titular], [FechaVigencia], [Estado], [Direccion], [BicSwift], [EsNacional]) VALUES (14, 12, N'Corriente', N'12345', N'Santander', N'Palermo', N'1234', N'Gonzalo', CAST(N'2018-11-16T00:00:00.000' AS DateTime), 1, NULL, NULL, NULL)
INSERT [dbo].[BeneficiarioBancos] ([Id], [IdBeneficiario], [TipoCuenta], [NroCuenta], [NombreBanco], [Sucursal], [CBU], [Titular], [FechaVigencia], [Estado], [Direccion], [BicSwift], [EsNacional]) VALUES (15, 16, N'Ahorro', N'dfghj', N'dsfgh', N'sdfgh', N'dfghj', N'dfgh', CAST(N'2018-11-06T00:00:00.000' AS DateTime), 0, NULL, NULL, NULL)
INSERT [dbo].[BeneficiarioBancos] ([Id], [IdBeneficiario], [TipoCuenta], [NroCuenta], [NombreBanco], [Sucursal], [CBU], [Titular], [FechaVigencia], [Estado], [Direccion], [BicSwift], [EsNacional]) VALUES (16, 18, N'Corriente', N'123456', N'Galicia', N'CABA', N'23415145365', N'Aeropuerto', CAST(N'2018-11-23T00:00:00.000' AS DateTime), 1, NULL, NULL, NULL)
INSERT [dbo].[BeneficiarioBancos] ([Id], [IdBeneficiario], [TipoCuenta], [NroCuenta], [NombreBanco], [Sucursal], [CBU], [Titular], [FechaVigencia], [Estado], [Direccion], [BicSwift], [EsNacional]) VALUES (17, 19, N'Corriente', N'12123123123112314412', N'Nombre Banco', N'Nombre sucursal', N'27254401124', N'Mariela', CAST(N'2018-10-10T00:00:00.000' AS DateTime), 0, N'', N'', NULL)
INSERT [dbo].[BeneficiarioBancos] ([Id], [IdBeneficiario], [TipoCuenta], [NroCuenta], [NombreBanco], [Sucursal], [CBU], [Titular], [FechaVigencia], [Estado], [Direccion], [BicSwift], [EsNacional]) VALUES (18, 19, N'Corriente', N'12123123123112314412', N'Nombre Banco', N'Nombre sucursal', N'27254401124', N'Mariela', CAST(N'2018-10-10T00:00:00.000' AS DateTime), 0, N'', N'', NULL)
INSERT [dbo].[BeneficiarioBancos] ([Id], [IdBeneficiario], [TipoCuenta], [NroCuenta], [NombreBanco], [Sucursal], [CBU], [Titular], [FechaVigencia], [Estado], [Direccion], [BicSwift], [EsNacional]) VALUES (19, 19, N'Corriente', N'12123123123112314412', N'Nombre Banco', N'Nombre sucursal', N'27254401124', N'Mariela', CAST(N'2018-10-10T00:00:00.000' AS DateTime), 1, N'', N'', NULL)
INSERT [dbo].[BeneficiarioBancos] ([Id], [IdBeneficiario], [TipoCuenta], [NroCuenta], [NombreBanco], [Sucursal], [CBU], [Titular], [FechaVigencia], [Estado], [Direccion], [BicSwift], [EsNacional]) VALUES (20, 20, N'Corriente', N'12123123123112314412', N'Prueba Nombre Banco', N'Prueba Sucursal', N'12', N'prueba Titular', CAST(N'2018-10-10T00:00:00.000' AS DateTime), 0, N'', N'', NULL)
INSERT [dbo].[BeneficiarioBancos] ([Id], [IdBeneficiario], [TipoCuenta], [NroCuenta], [NombreBanco], [Sucursal], [CBU], [Titular], [FechaVigencia], [Estado], [Direccion], [BicSwift], [EsNacional]) VALUES (21, 20, N'Corriente', N'12123123123112314412', N'Prueba Nombre Banco', N'Prueba Sucursal', N'11', N'prueba Titular', CAST(N'2018-10-10T00:00:00.000' AS DateTime), 0, N'', N'', NULL)
INSERT [dbo].[BeneficiarioBancos] ([Id], [IdBeneficiario], [TipoCuenta], [NroCuenta], [NombreBanco], [Sucursal], [CBU], [Titular], [FechaVigencia], [Estado], [Direccion], [BicSwift], [EsNacional]) VALUES (22, 20, N'Corriente', N'12123123123112314412', N'Prueba Nombre Banco', N'Prueba Sucursal', N'11', N'prueba Titular', CAST(N'2018-10-10T00:00:00.000' AS DateTime), 0, N'', N'', NULL)
INSERT [dbo].[BeneficiarioBancos] ([Id], [IdBeneficiario], [TipoCuenta], [NroCuenta], [NombreBanco], [Sucursal], [CBU], [Titular], [FechaVigencia], [Estado], [Direccion], [BicSwift], [EsNacional]) VALUES (23, 20, N'Corriente', N'12123123123112314412', N'Prueba Nombre Banco', N'Prueba Sucursal', N'11', N'prueba Titular', CAST(N'2018-10-10T00:00:00.000' AS DateTime), 0, N'', N'', NULL)
INSERT [dbo].[BeneficiarioBancos] ([Id], [IdBeneficiario], [TipoCuenta], [NroCuenta], [NombreBanco], [Sucursal], [CBU], [Titular], [FechaVigencia], [Estado], [Direccion], [BicSwift], [EsNacional]) VALUES (24, 20, N'Corriente', N'12123123123112314412', N'Prueba Nombre Banco', N'Prueba Sucursal', N'12', N'prueba Titular', CAST(N'2018-10-10T00:00:00.000' AS DateTime), 1, N'', N'', NULL)
INSERT [dbo].[BeneficiarioBancos] ([Id], [IdBeneficiario], [TipoCuenta], [NroCuenta], [NombreBanco], [Sucursal], [CBU], [Titular], [FechaVigencia], [Estado], [Direccion], [BicSwift], [EsNacional]) VALUES (25, 20, N'Corriente', N'12123123123112314412', N'Prueba Nombre Banco', N'Prueba Sucursal', N'11', N'prueba Titular', CAST(N'2018-10-10T00:00:00.000' AS DateTime), 1, N'', N'', NULL)
INSERT [dbo].[BeneficiarioBancos] ([Id], [IdBeneficiario], [TipoCuenta], [NroCuenta], [NombreBanco], [Sucursal], [CBU], [Titular], [FechaVigencia], [Estado], [Direccion], [BicSwift], [EsNacional]) VALUES (26, 20, N'Corriente', N'12123123123112314412', N'Prueba Nombre Banco', N'Prueba Sucursal', N'11', N'prueba Titular', CAST(N'2018-10-10T00:00:00.000' AS DateTime), 1, N'', N'', NULL)
INSERT [dbo].[BeneficiarioBancos] ([Id], [IdBeneficiario], [TipoCuenta], [NroCuenta], [NombreBanco], [Sucursal], [CBU], [Titular], [FechaVigencia], [Estado], [Direccion], [BicSwift], [EsNacional]) VALUES (27, 20, N'Corriente', N'12123123123112314412', N'Prueba Nombre Banco', N'Prueba Sucursal', N'11', N'prueba Titular', CAST(N'2018-10-10T00:00:00.000' AS DateTime), 1, N'', N'', NULL)
INSERT [dbo].[BeneficiarioBancos] ([Id], [IdBeneficiario], [TipoCuenta], [NroCuenta], [NombreBanco], [Sucursal], [CBU], [Titular], [FechaVigencia], [Estado], [Direccion], [BicSwift], [EsNacional]) VALUES (28, 21, N'Corriente', N'12345678987678987654', N'Prueba nombre banco', N'Prueba Sucursal', N'1209897656783948736545', N'prueba Titular', CAST(N'2018-10-10T00:00:00.000' AS DateTime), 1, N'', N'', NULL)
INSERT [dbo].[BeneficiarioBancos] ([Id], [IdBeneficiario], [TipoCuenta], [NroCuenta], [NombreBanco], [Sucursal], [CBU], [Titular], [FechaVigencia], [Estado], [Direccion], [BicSwift], [EsNacional]) VALUES (29, 22, N'', N'12123212321232322323', N'nombre banco', N'', N'', N'', CAST(N'2018-11-16T00:00:00.000' AS DateTime), 1, N'direccion', N'1232', NULL)
INSERT [dbo].[BeneficiarioBancos] ([Id], [IdBeneficiario], [TipoCuenta], [NroCuenta], [NombreBanco], [Sucursal], [CBU], [Titular], [FechaVigencia], [Estado], [Direccion], [BicSwift], [EsNacional]) VALUES (30, 22, N'', N'12343546454789876567', N'nombre', N'', N'', N'', CAST(N'2018-11-09T00:00:00.000' AS DateTime), 1, N'direccion', N'12321', NULL)
INSERT [dbo].[BeneficiarioBancos] ([Id], [IdBeneficiario], [TipoCuenta], [NroCuenta], [NombreBanco], [Sucursal], [CBU], [Titular], [FechaVigencia], [Estado], [Direccion], [BicSwift], [EsNacional]) VALUES (31, 23, N'Ahorro', N'12123212321232322323', N'nombre banco', N'', N'0070999030004105263288', N'', CAST(N'2018-11-09T00:00:00.000' AS DateTime), 1, N'', N'', NULL)
INSERT [dbo].[BeneficiarioBancos] ([Id], [IdBeneficiario], [TipoCuenta], [NroCuenta], [NombreBanco], [Sucursal], [CBU], [Titular], [FechaVigencia], [Estado], [Direccion], [BicSwift], [EsNacional]) VALUES (32, 24, N'Ahorro', N'23432343234334343234', N'nombre banco', N'', N'2343232432432343234323', N'', CAST(N'2018-11-15T00:00:00.000' AS DateTime), 1, N'', N'', NULL)
INSERT [dbo].[BeneficiarioBancos] ([Id], [IdBeneficiario], [TipoCuenta], [NroCuenta], [NombreBanco], [Sucursal], [CBU], [Titular], [FechaVigencia], [Estado], [Direccion], [BicSwift], [EsNacional]) VALUES (33, 25, N'Corriente', N'12123123123112314412', N'Prueba Nombre Bando', N'Prueba Sucursal', N'1989876789999999999999', N'prueba titular', CAST(N'2018-10-10T00:00:00.000' AS DateTime), 1, N'', N'', NULL)
INSERT [dbo].[BeneficiarioBancos] ([Id], [IdBeneficiario], [TipoCuenta], [NroCuenta], [NombreBanco], [Sucursal], [CBU], [Titular], [FechaVigencia], [Estado], [Direccion], [BicSwift], [EsNacional]) VALUES (34, 26, N'', N'11111111111111111111', N'Banco extranjero', N'', N'', N'', CAST(N'2018-10-10T00:00:00.000' AS DateTime), 0, N'prueba dirección', N'asdf', NULL)
INSERT [dbo].[BeneficiarioBancos] ([Id], [IdBeneficiario], [TipoCuenta], [NroCuenta], [NombreBanco], [Sucursal], [CBU], [Titular], [FechaVigencia], [Estado], [Direccion], [BicSwift], [EsNacional]) VALUES (35, 26, N'', N'28376548398763849574', N'Otro  banco extranjero', N'', N'', N'', CAST(N'2018-10-15T00:00:00.000' AS DateTime), 0, N'prueba direccion', N'123', NULL)
INSERT [dbo].[BeneficiarioBancos] ([Id], [IdBeneficiario], [TipoCuenta], [NroCuenta], [NombreBanco], [Sucursal], [CBU], [Titular], [FechaVigencia], [Estado], [Direccion], [BicSwift], [EsNacional]) VALUES (36, 26, N'', N'28376548398763849574', N'Otro  banco extranjero', N'', N'', N'', CAST(N'2018-10-15T00:00:00.000' AS DateTime), 1, N'prueba direccion', N'123', NULL)
SET IDENTITY_INSERT [dbo].[BeneficiarioBancos] OFF
SET IDENTITY_INSERT [dbo].[Beneficiarios] ON 

INSERT [dbo].[Beneficiarios] ([Id], [RazonSocial], [Descripcion], [Cuit], [FechaAlta], [NacionalExtranjero], [Email], [Estado], [Telefono]) VALUES (8, N'razon social', N'descripcion', N'cuit', CAST(N'2018-10-02T00:00:00.000' AS DateTime), N'Extranjero', N'mail', 0, NULL)
INSERT [dbo].[Beneficiarios] ([Id], [RazonSocial], [Descripcion], [Cuit], [FechaAlta], [NacionalExtranjero], [Email], [Estado], [Telefono]) VALUES (9, N'tes', N'test', N'123123123', CAST(N'2018-10-29T00:00:00.000' AS DateTime), N'Nacional  ', N'test@test.com', 0, NULL)
INSERT [dbo].[Beneficiarios] ([Id], [RazonSocial], [Descripcion], [Cuit], [FechaAlta], [NacionalExtranjero], [Email], [Estado], [Telefono]) VALUES (10, N'Razon Test', N'Descripcion test', N'20123123', CAST(N'2018-10-30T00:00:00.000' AS DateTime), N'Nacional  ', N'test@test.com', 0, NULL)
INSERT [dbo].[Beneficiarios] ([Id], [RazonSocial], [Descripcion], [Cuit], [FechaAlta], [NacionalExtranjero], [Email], [Estado], [Telefono]) VALUES (11, N'Prueba Marney', N'Prueba Marney', N'Prueba Marney', CAST(N'2018-10-03T00:00:00.000' AS DateTime), N'Nacional  ', N'Prueba Marney', 0, NULL)
INSERT [dbo].[Beneficiarios] ([Id], [RazonSocial], [Descripcion], [Cuit], [FechaAlta], [NacionalExtranjero], [Email], [Estado], [Telefono]) VALUES (12, N'1', N'1', N'1', CAST(N'2018-11-01T00:00:00.000' AS DateTime), N'Nacional  ', N'a@a.com', 0, NULL)
INSERT [dbo].[Beneficiarios] ([Id], [RazonSocial], [Descripcion], [Cuit], [FechaAlta], [NacionalExtranjero], [Email], [Estado], [Telefono]) VALUES (13, N'test', N'test', N'1313131313', CAST(N'2018-11-14T00:00:00.000' AS DateTime), N'Nacional  ', N'test@test.com', 1, NULL)
INSERT [dbo].[Beneficiarios] ([Id], [RazonSocial], [Descripcion], [Cuit], [FechaAlta], [NacionalExtranjero], [Email], [Estado], [Telefono]) VALUES (14, N'Benefic 01', N'Benefic 01', N'123456799', CAST(N'2018-11-14T00:00:00.000' AS DateTime), N'Extranjero', N'glago@w3itsolutions.net', 1, NULL)
INSERT [dbo].[Beneficiarios] ([Id], [RazonSocial], [Descripcion], [Cuit], [FechaAlta], [NacionalExtranjero], [Email], [Estado], [Telefono]) VALUES (15, N'El Aeroport', N'El Aeroport', N'123456789', CAST(N'2018-11-14T00:00:00.000' AS DateTime), N'Nacional  ', N'aeroport@gmail.com', 0, NULL)
INSERT [dbo].[Beneficiarios] ([Id], [RazonSocial], [Descripcion], [Cuit], [FechaAlta], [NacionalExtranjero], [Email], [Estado], [Telefono]) VALUES (16, N'asd', N'asd', N'123123', CAST(N'2018-11-16T00:00:00.000' AS DateTime), N'Nacional  ', N'test@test.com', 0, NULL)
INSERT [dbo].[Beneficiarios] ([Id], [RazonSocial], [Descripcion], [Cuit], [FechaAlta], [NacionalExtranjero], [Email], [Estado], [Telefono]) VALUES (17, N'2345', N'ghsdf', N'sdfgh', CAST(N'2018-11-06T00:00:00.000' AS DateTime), N'Extranjero', N'3456', 0, NULL)
INSERT [dbo].[Beneficiarios] ([Id], [RazonSocial], [Descripcion], [Cuit], [FechaAlta], [NacionalExtranjero], [Email], [Estado], [Telefono]) VALUES (18, N'Extranjero 1', N'Extranjero 1', N'1234567', CAST(N'2018-11-20T00:00:00.000' AS DateTime), N'Extranjero', N'extranjero@gmail.com', 0, NULL)
INSERT [dbo].[Beneficiarios] ([Id], [RazonSocial], [Descripcion], [Cuit], [FechaAlta], [NacionalExtranjero], [Email], [Estado], [Telefono]) VALUES (19, N'Mariela Pagano', N'Prueba Beneficiario', N'2725440112jdjd', CAST(N'2018-10-10T00:00:00.000' AS DateTime), N'false     ', N'asdñlkfjañsldkfjasd', 0, N'')
INSERT [dbo].[Beneficiarios] ([Id], [RazonSocial], [Descripcion], [Cuit], [FechaAlta], [NacionalExtranjero], [Email], [Estado], [Telefono]) VALUES (20, N'Prueba Razón Social', N'prueba descripción', N'adsf', CAST(N'2018-10-10T00:00:00.000' AS DateTime), N'false     ', N'asdfasdfasdf', 0, N'')
INSERT [dbo].[Beneficiarios] ([Id], [RazonSocial], [Descripcion], [Cuit], [FechaAlta], [NacionalExtranjero], [Email], [Estado], [Telefono]) VALUES (21, N'Otra Prueba Razón Social', N'Prueba  descripción', N'25-09875565-3', CAST(N'2018-10-10T00:00:00.000' AS DateTime), N'true      ', N'asdfasdfasdf', 0, N'prueba telefono')
INSERT [dbo].[Beneficiarios] ([Id], [RazonSocial], [Descripcion], [Cuit], [FechaAlta], [NacionalExtranjero], [Email], [Estado], [Telefono]) VALUES (22, N'Razon Social', N'descripcion', N'12-12345678-8', CAST(N'2018-11-16T00:00:00.000' AS DateTime), N'false     ', N'mail', 1, N'telefono')
INSERT [dbo].[Beneficiarios] ([Id], [RazonSocial], [Descripcion], [Cuit], [FechaAlta], [NacionalExtranjero], [Email], [Estado], [Telefono]) VALUES (23, N'razon social', N'', N'12-45678987-7', CAST(N'2018-11-25T00:00:00.000' AS DateTime), N'true      ', N'', 1, N'')
INSERT [dbo].[Beneficiarios] ([Id], [RazonSocial], [Descripcion], [Cuit], [FechaAlta], [NacionalExtranjero], [Email], [Estado], [Telefono]) VALUES (24, N'razon social', N'', N'12-12345678-7', CAST(N'2018-11-25T00:00:00.000' AS DateTime), N'true      ', N'', 1, N'')
INSERT [dbo].[Beneficiarios] ([Id], [RazonSocial], [Descripcion], [Cuit], [FechaAlta], [NacionalExtranjero], [Email], [Estado], [Telefono]) VALUES (25, N'Prueba Razón Social', N'prueba descripción', N'27-25746332-1', CAST(N'2018-10-10T00:00:00.000' AS DateTime), N'true      ', N'prueba@gmail.com', 1, N'234234')
INSERT [dbo].[Beneficiarios] ([Id], [RazonSocial], [Descripcion], [Cuit], [FechaAlta], [NacionalExtranjero], [Email], [Estado], [Telefono]) VALUES (26, N'Otra Prueba Razón  Social', N'prueba descripción', N'27-28993771-2', CAST(N'2018-10-10T00:00:00.000' AS DateTime), N'false     ', N'prueba@gmail.com', 1, N'23432')
SET IDENTITY_INSERT [dbo].[Beneficiarios] OFF
SET IDENTITY_INSERT [dbo].[Cuentas] ON 

INSERT [dbo].[Cuentas] ([Id], [NroCuenta], [Nombre], [Descripcion], [FechaVigencia], [FechaCreacion], [IdLibranzaTipo], [IdAeropuertosGrupo], [Estado]) VALUES (23, N'12123123', N'test', N'test', CAST(N'2018-10-29T00:00:00.000' AS DateTime), CAST(N'2018-10-29T18:35:36.967' AS DateTime), 1, 1, 1)
INSERT [dbo].[Cuentas] ([Id], [NroCuenta], [Nombre], [Descripcion], [FechaVigencia], [FechaCreacion], [IdLibranzaTipo], [IdAeropuertosGrupo], [Estado]) VALUES (24, N'1', N'1', N'1', CAST(N'2018-10-31T00:00:00.000' AS DateTime), CAST(N'2018-10-30T15:22:48.577' AS DateTime), 1, 1, 1)
INSERT [dbo].[Cuentas] ([Id], [NroCuenta], [Nombre], [Descripcion], [FechaVigencia], [FechaCreacion], [IdLibranzaTipo], [IdAeropuertosGrupo], [Estado]) VALUES (25, N'1234', N'name', N'name2', CAST(N'2018-11-06T00:00:00.000' AS DateTime), CAST(N'2018-11-05T17:29:58.437' AS DateTime), 1, 1, 1)
INSERT [dbo].[Cuentas] ([Id], [NroCuenta], [Nombre], [Descripcion], [FechaVigencia], [FechaCreacion], [IdLibranzaTipo], [IdAeropuertosGrupo], [Estado]) VALUES (26, N'nnkjnkjn', N'klkklklkkkkl', N'kij', CAST(N'2018-11-07T00:00:00.000' AS DateTime), CAST(N'2018-11-13T15:57:42.743' AS DateTime), 1, 1, 0)
INSERT [dbo].[Cuentas] ([Id], [NroCuenta], [Nombre], [Descripcion], [FechaVigencia], [FechaCreacion], [IdLibranzaTipo], [IdAeropuertosGrupo], [Estado]) VALUES (27, N'99999999999999999999999999999999999999999999999999999999999999', N'cuenta nueve', N'nueve', CAST(N'2018-11-13T00:00:00.000' AS DateTime), CAST(N'2018-11-13T16:02:23.220' AS DateTime), 1, 1, 0)
INSERT [dbo].[Cuentas] ([Id], [NroCuenta], [Nombre], [Descripcion], [FechaVigencia], [FechaCreacion], [IdLibranzaTipo], [IdAeropuertosGrupo], [Estado]) VALUES (28, N'2134567890-', N'sdfghjk', N'sdfghjk', CAST(N'2018-11-08T00:00:00.000' AS DateTime), CAST(N'2018-11-16T21:07:56.197' AS DateTime), 1, 1, 0)
INSERT [dbo].[Cuentas] ([Id], [NroCuenta], [Nombre], [Descripcion], [FechaVigencia], [FechaCreacion], [IdLibranzaTipo], [IdAeropuertosGrupo], [Estado]) VALUES (29, N'Cuenta01', N'Cuenta01', N'CTA01', CAST(N'2018-11-30T00:00:00.000' AS DateTime), CAST(N'2018-11-20T09:54:00.297' AS DateTime), 1, 1, 1)
INSERT [dbo].[Cuentas] ([Id], [NroCuenta], [Nombre], [Descripcion], [FechaVigencia], [FechaCreacion], [IdLibranzaTipo], [IdAeropuertosGrupo], [Estado]) VALUES (30, N'32453456734567435674', N'Leonar Herrera', N'descripcion', CAST(N'2018-11-02T00:00:00.000' AS DateTime), CAST(N'2018-11-20T20:08:45.083' AS DateTime), 1, 1, 1)
INSERT [dbo].[Cuentas] ([Id], [NroCuenta], [Nombre], [Descripcion], [FechaVigencia], [FechaCreacion], [IdLibranzaTipo], [IdAeropuertosGrupo], [Estado]) VALUES (31, N'23456734567895467890', N'hjtgertgdfgfd gdfg df', N' dfg fdg f', CAST(N'2018-11-15T00:00:00.000' AS DateTime), CAST(N'2018-11-21T11:29:49.707' AS DateTime), 1, 1, 0)
INSERT [dbo].[Cuentas] ([Id], [NroCuenta], [Nombre], [Descripcion], [FechaVigencia], [FechaCreacion], [IdLibranzaTipo], [IdAeropuertosGrupo], [Estado]) VALUES (32, N'12123123123112312312', N'test1', N'test', CAST(N'2018-11-21T00:00:00.000' AS DateTime), CAST(N'2018-11-21T11:30:17.977' AS DateTime), 1, 1, 1)
INSERT [dbo].[Cuentas] ([Id], [NroCuenta], [Nombre], [Descripcion], [FechaVigencia], [FechaCreacion], [IdLibranzaTipo], [IdAeropuertosGrupo], [Estado]) VALUES (33, N'32413241132413241234', N'12', N'prueba  mariela', CAST(N'2018-10-10T00:00:00.000' AS DateTime), CAST(N'2018-11-22T10:54:54.673' AS DateTime), 1, 1, 1)
INSERT [dbo].[Cuentas] ([Id], [NroCuenta], [Nombre], [Descripcion], [FechaVigencia], [FechaCreacion], [IdLibranzaTipo], [IdAeropuertosGrupo], [Estado]) VALUES (34, N'12123123123112312316', N'Cuenta Mariela', N'prueba mariela Val.', CAST(N'2018-10-10T00:00:00.000' AS DateTime), CAST(N'2018-11-22T11:03:11.327' AS DateTime), 1, 1, 1)
INSERT [dbo].[Cuentas] ([Id], [NroCuenta], [Nombre], [Descripcion], [FechaVigencia], [FechaCreacion], [IdLibranzaTipo], [IdAeropuertosGrupo], [Estado]) VALUES (35, N'12394738694758648371', N'Nueva  Cuenta', N'NCM', CAST(N'2018-10-10T00:00:00.000' AS DateTime), CAST(N'2018-11-23T09:54:39.767' AS DateTime), 2, 1, 1)
INSERT [dbo].[Cuentas] ([Id], [NroCuenta], [Nombre], [Descripcion], [FechaVigencia], [FechaCreacion], [IdLibranzaTipo], [IdAeropuertosGrupo], [Estado]) VALUES (36, N'12123198123112312312', N'Prueba Nueva  Cuenta', N'PNC', CAST(N'2018-10-10T00:00:00.000' AS DateTime), CAST(N'2018-11-23T09:55:58.430' AS DateTime), 2, 2, 1)
INSERT [dbo].[Cuentas] ([Id], [NroCuenta], [Nombre], [Descripcion], [FechaVigencia], [FechaCreacion], [IdLibranzaTipo], [IdAeropuertosGrupo], [Estado]) VALUES (37, N'12123123123112314412', N'Prueba Cuenta Fiduci', N'PCF2', CAST(N'2018-10-10T00:00:00.000' AS DateTime), CAST(N'2018-11-23T10:03:49.023' AS DateTime), 2, 2, 0)
SET IDENTITY_INSERT [dbo].[Cuentas] OFF
SET IDENTITY_INSERT [dbo].[LibranzaTipo] ON 

INSERT [dbo].[LibranzaTipo] ([Id], [Nombre], [Estado]) VALUES (1, N'Tipo A', 1)
INSERT [dbo].[LibranzaTipo] ([Id], [Nombre], [Estado]) VALUES (2, N'Tipo B', 1)
SET IDENTITY_INSERT [dbo].[LibranzaTipo] OFF
SET IDENTITY_INSERT [dbo].[Log] ON 

INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (1, N'BLProveedor', N'Object reference not set to an instance of an object.', N'', CAST(N'2018-10-12T09:46:33.310' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (2, N'BLProveedor', N'Object reference not set to an instance of an object.', N'', CAST(N'2018-10-12T09:49:51.083' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (3, N'BLProveedor', N'Invalid object name ''Proveedores''.', N'', CAST(N'2018-10-26T16:01:17.967' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (4, N'BLProveedor', N'Invalid object name ''Proveedores''.', N'', CAST(N'2018-10-26T16:01:17.967' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (5, N'BLProveedor', N'Value cannot be null.
Parameter name: source', N'', CAST(N'2018-10-26T16:01:18.273' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (6, N'BLProveedor', N'Invalid object name ''Proveedores''.', N'', CAST(N'2018-10-26T17:14:08.910' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (7, N'BLProveedor', N'Invalid object name ''Proveedores''.', N'', CAST(N'2018-10-26T17:14:08.910' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (8, N'BLProveedor', N'Value cannot be null.
Parameter name: source', N'', CAST(N'2018-10-26T17:14:09.433' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (9, N'BLProveedor', N'Invalid object name ''Proveedores''.', N'', CAST(N'2018-10-26T17:15:08.617' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (10, N'BLProveedor', N'Invalid object name ''Proveedores''.', N'', CAST(N'2018-10-26T17:15:08.620' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (11, N'BLProveedor', N'Value cannot be null.
Parameter name: source', N'', CAST(N'2018-10-26T17:15:08.627' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (12, N'BLProveedor', N'Invalid object name ''Proveedores''.', N'', CAST(N'2018-10-26T17:15:24.863' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (13, N'BLProveedor', N'Invalid object name ''Proveedores''.', N'', CAST(N'2018-10-26T17:24:27.703' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (14, N'BLProveedor', N'Invalid object name ''Proveedores''.', N'', CAST(N'2018-10-26T17:24:27.737' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (15, N'BLProveedor', N'Value cannot be null.
Parameter name: source', N'', CAST(N'2018-10-26T17:24:27.953' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (16, N'BLProveedor', N'Invalid object name ''Proveedores''.', N'', CAST(N'2018-10-26T17:24:43.450' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (17, N'BLProveedor', N'Invalid object name ''Proveedores''.', N'', CAST(N'2018-10-26T17:24:43.450' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (18, N'BLProveedor', N'Value cannot be null.
Parameter name: source', N'', CAST(N'2018-10-26T17:24:43.453' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (19, N'BLProveedor', N'Invalid object name ''Proveedores''.', N'', CAST(N'2018-10-29T11:14:36.733' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (20, N'BLProveedor', N'Invalid object name ''Proveedores''.', N'', CAST(N'2018-10-29T11:14:36.740' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (21, N'BLProveedor', N'Value cannot be null.
Parameter name: source', N'', CAST(N'2018-10-29T11:14:37.013' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (22, N'BLProveedor', N'Invalid object name ''Proveedores''.', N'', CAST(N'2018-10-29T11:14:43.610' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (23, N'BLProveedor', N'Invalid object name ''Proveedores''.', N'', CAST(N'2018-10-29T11:14:43.610' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (24, N'BLProveedor', N'Value cannot be null.
Parameter name: source', N'', CAST(N'2018-10-29T11:14:43.613' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (25, N'BLProveedor', N'Invalid object name ''Proveedores''.', N'', CAST(N'2018-10-29T13:38:33.777' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (26, N'BLProveedor', N'Invalid object name ''Proveedores''.', N'', CAST(N'2018-10-29T13:38:33.760' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (27, N'BLProveedor', N'Value cannot be null.
Parameter name: source', N'', CAST(N'2018-10-29T13:38:34.003' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (28, N'BLProveedor', N'Invalid object name ''Proveedores''.', N'', CAST(N'2018-10-29T13:38:43.087' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (29, N'BLProveedor', N'Invalid object name ''Proveedores''.', N'', CAST(N'2018-10-29T13:38:43.097' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (30, N'BLProveedor', N'Value cannot be null.
Parameter name: source', N'', CAST(N'2018-10-29T13:38:43.103' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (31, N'BLProveedor', N'Invalid object name ''Proveedores''.', N'', CAST(N'2018-10-29T15:59:40.667' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (32, N'BLProveedor', N'Invalid object name ''Proveedores''.', N'', CAST(N'2018-10-29T15:59:40.840' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (33, N'BLProveedor', N'Value cannot be null.
Parameter name: source', N'', CAST(N'2018-10-29T15:59:40.953' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (34, N'BLProveedor', N'Invalid object name ''Proveedores''.', N'', CAST(N'2018-10-29T16:03:09.527' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (35, N'BLProveedor', N'Invalid object name ''Proveedores''.', N'', CAST(N'2018-10-29T16:03:09.527' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (36, N'BLProveedor', N'Value cannot be null.
Parameter name: source', N'', CAST(N'2018-10-29T16:03:09.757' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (37, N'BLProveedor', N'Invalid object name ''Proveedores''.', N'', CAST(N'2018-10-29T16:05:53.130' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (38, N'BLProveedor', N'Invalid object name ''Proveedores''.', N'', CAST(N'2018-10-29T16:05:53.133' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (39, N'BLProveedor', N'Value cannot be null.
Parameter name: source', N'', CAST(N'2018-10-29T16:05:53.133' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (40, N'BLProveedor', N'Invalid object name ''Proveedores''.', N'', CAST(N'2018-10-29T16:06:14.657' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (41, N'BLProveedor', N'Invalid object name ''Proveedores''.', N'', CAST(N'2018-10-29T16:06:14.657' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (42, N'BLProveedor', N'Value cannot be null.
Parameter name: source', N'', CAST(N'2018-10-29T16:06:14.660' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (43, N'BLProveedor', N'Invalid object name ''Proveedores''.', N'', CAST(N'2018-10-29T16:06:15.170' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (44, N'BLProveedor', N'Invalid object name ''Proveedores''.', N'', CAST(N'2018-10-29T16:06:15.170' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (45, N'BLProveedor', N'Value cannot be null.
Parameter name: source', N'', CAST(N'2018-10-29T16:06:15.173' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (46, N'BLProveedor', N'Invalid object name ''Proveedores''.', N'', CAST(N'2018-10-29T16:06:24.257' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (47, N'BLProveedor', N'Invalid object name ''Proveedores''.', N'', CAST(N'2018-10-29T16:06:24.257' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (48, N'BLProveedor', N'Value cannot be null.
Parameter name: source', N'', CAST(N'2018-10-29T16:06:24.260' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (49, N'BLProveedor', N'Invalid object name ''Proveedores''.', N'', CAST(N'2018-10-29T16:06:24.640' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (50, N'BLProveedor', N'Invalid object name ''Proveedores''.', N'', CAST(N'2018-10-29T16:06:24.640' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (51, N'BLProveedor', N'Value cannot be null.
Parameter name: source', N'', CAST(N'2018-10-29T16:06:24.647' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (52, N'BLProveedor', N'Invalid object name ''Proveedores''.', N'', CAST(N'2018-10-29T16:06:29.147' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (53, N'BLProveedor', N'Invalid object name ''Proveedores''.', N'', CAST(N'2018-10-29T16:06:29.147' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (54, N'BLProveedor', N'Value cannot be null.
Parameter name: source', N'', CAST(N'2018-10-29T16:06:29.150' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (55, N'BLProveedor', N'Invalid object name ''Proveedores''.', N'', CAST(N'2018-10-29T16:06:35.843' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (56, N'BLProveedor', N'Invalid object name ''Proveedores''.', N'', CAST(N'2018-10-29T16:06:57.193' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (57, N'BLProveedor', N'Invalid object name ''Proveedores''.', N'', CAST(N'2018-10-29T16:06:57.193' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (58, N'BLProveedor', N'Value cannot be null.
Parameter name: source', N'', CAST(N'2018-10-29T16:06:57.197' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (59, N'BLProveedor', N'Invalid object name ''Proveedores''.', N'', CAST(N'2018-10-29T16:07:09.800' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (60, N'BLProveedor', N'Invalid object name ''Proveedores''.', N'', CAST(N'2018-10-29T16:07:09.800' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (61, N'BLProveedor', N'Value cannot be null.
Parameter name: source', N'', CAST(N'2018-10-29T16:07:09.803' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (62, N'BLProveedor', N'Invalid object name ''Proveedores''.', N'', CAST(N'2018-10-29T16:07:19.763' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (63, N'BLProveedor', N'Invalid object name ''Proveedores''.', N'', CAST(N'2018-10-29T16:07:19.763' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (64, N'BLProveedor', N'Value cannot be null.
Parameter name: source', N'', CAST(N'2018-10-29T16:07:19.767' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (65, N'BLProveedor', N'Invalid object name ''Proveedores''.', N'', CAST(N'2018-10-29T16:07:28.877' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (66, N'BLProveedor', N'Invalid object name ''Proveedores''.', N'', CAST(N'2018-10-29T16:07:28.880' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (67, N'BLProveedor', N'Value cannot be null.
Parameter name: source', N'', CAST(N'2018-10-29T16:07:28.883' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (68, N'BLProveedor', N'Invalid object name ''Proveedores''.', N'', CAST(N'2018-10-29T16:08:31.847' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (69, N'BLProveedor', N'Value cannot be null.
Parameter name: source', N'', CAST(N'2018-10-29T16:08:31.850' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (70, N'BLProveedor', N'Invalid object name ''Proveedores''.', N'', CAST(N'2018-10-29T16:08:31.857' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (71, N'BLProveedor', N'Invalid object name ''Proveedores''.', N'', CAST(N'2018-10-29T16:09:02.217' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (72, N'BLProveedor', N'Value cannot be null.
Parameter name: source', N'', CAST(N'2018-10-29T16:09:02.220' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (73, N'BLProveedor', N'Invalid object name ''Proveedores''.', N'', CAST(N'2018-10-29T16:09:02.223' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (74, N'BLProveedor', N'Invalid object name ''Proveedores''.', N'', CAST(N'2018-10-29T16:09:15.783' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (75, N'BLProveedor', N'Invalid object name ''Proveedores''.', N'', CAST(N'2018-10-29T16:09:15.783' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (76, N'BLProveedor', N'Value cannot be null.
Parameter name: source', N'', CAST(N'2018-10-29T16:09:15.787' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (77, N'BLProveedor', N'Invalid object name ''Proveedores''.', N'', CAST(N'2018-10-29T16:09:19.227' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (78, N'BLProveedor', N'Invalid object name ''Proveedores''.', N'', CAST(N'2018-10-29T16:09:19.230' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (79, N'BLProveedor', N'Value cannot be null.
Parameter name: source', N'', CAST(N'2018-10-29T16:09:19.233' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (80, N'BLProveedor', N'Invalid object name ''Proveedores''.', N'', CAST(N'2018-10-29T16:09:19.673' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (81, N'BLProveedor', N'Invalid object name ''Proveedores''.', N'', CAST(N'2018-10-29T16:09:19.673' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (82, N'BLProveedor', N'Value cannot be null.
Parameter name: source', N'', CAST(N'2018-10-29T16:09:19.677' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (83, N'BLProveedor', N'Invalid object name ''Proveedores''.', N'', CAST(N'2018-10-29T16:09:24.740' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (84, N'BLProveedor', N'Invalid object name ''Proveedores''.', N'', CAST(N'2018-10-29T16:09:24.740' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (85, N'BLProveedor', N'Value cannot be null.
Parameter name: source', N'', CAST(N'2018-10-29T16:09:24.743' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (86, N'BLProveedor', N'Invalid object name ''Proveedores''.', N'', CAST(N'2018-10-29T16:09:52.497' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (87, N'BLProveedor', N'Invalid object name ''Proveedores''.', N'', CAST(N'2018-10-29T16:09:52.493' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (88, N'BLProveedor', N'Value cannot be null.
Parameter name: source', N'', CAST(N'2018-10-29T16:09:52.533' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (89, N'BLProveedor', N'Invalid object name ''Proveedores''.', N'', CAST(N'2018-10-29T16:10:08.127' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (90, N'BLProyecto', N'Invalid column name ''IdProveedor''.', N'', CAST(N'2018-10-31T16:38:34.990' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (91, N'BLProyecto', N'Invalid column name ''IdProveedor''.', N'', CAST(N'2018-10-31T16:40:24.823' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (92, N'BLProyecto', N'Invalid column name ''IdProveedor''.', N'', CAST(N'2018-10-31T16:45:42.880' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (93, N'BLProyecto', N'Invalid column name ''IdProveedor''.', N'', CAST(N'2018-10-31T16:47:29.487' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (94, N'BLProyecto', N'Invalid column name ''IdProveedor''.', N'', CAST(N'2018-10-31T16:48:26.290' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (95, N'BLProyecto', N'Invalid column name ''IdProveedor''.', N'', CAST(N'2018-10-31T16:48:59.637' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (96, N'BLProyecto', N'Invalid column name ''IdProveedor''.', N'', CAST(N'2018-10-31T16:49:49.283' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (97, N'BlProyecto', N'The property ''IdProyecto'' on entity type ''Proyectos'' could not be found. Ensure that the property exists and has been included in the model.', N'', CAST(N'2018-10-31T17:01:41.553' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (98, N'BlProyecto', N'Value cannot be null.
Parameter name: source', N'', CAST(N'2018-10-31T17:01:41.737' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (99, N'BlProyecto', N'The property ''IdProyecto'' on entity type ''Proyectos'' could not be found. Ensure that the property exists and has been included in the model.', N'', CAST(N'2018-10-31T17:02:26.647' AS DateTime))
GO
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (100, N'BlProyecto', N'Value cannot be null.
Parameter name: source', N'', CAST(N'2018-10-31T17:02:27.183' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (101, N'BlProyecto', N'The property ''IdProyecto'' on entity type ''Proyectos'' could not be found. Ensure that the property exists and has been included in the model.', N'', CAST(N'2018-10-31T17:08:59.593' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (102, N'BlProyecto', N'Value cannot be null.
Parameter name: source', N'', CAST(N'2018-10-31T17:09:19.030' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (103, N'BLProveedor', N'Invalid column name ''IdProveedor''.
Invalid column name ''IdProveedor''.', N'', CAST(N'2018-11-06T10:56:31.273' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (104, N'BLProveedor', N'Invalid column name ''IdProveedor''.
Invalid column name ''IdProveedor''.', N'', CAST(N'2018-11-06T11:11:27.563' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (105, N'BLBeneficiarioAdjuntos', N'An error occurred while updating the entries. See the inner exception for details.', N'The INSERT statement conflicted with the FOREIGN KEY constraint "FK_BeneficiarioAdjuntos_Adjuntos". The conflict occurred in database "OrsnaDatabase", table "dbo.Adjuntos", column ''Id''.
The statement has been terminated.', CAST(N'2018-11-06T11:11:32.493' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (106, N'BLProveedor', N'Invalid column name ''IdProveedor''.
Invalid column name ''IdProveedor''.', N'', CAST(N'2018-11-06T11:12:40.547' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (107, N'BLProveedor', N'Invalid column name ''IdProveedor''.
Invalid column name ''IdProveedor''.', N'', CAST(N'2018-11-06T11:12:58.230' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (108, N'BLProveedor', N'Invalid column name ''IdProveedor''.
Invalid column name ''IdProveedor''.', N'', CAST(N'2018-11-06T11:13:16.623' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (109, N'BLProveedor', N'Invalid column name ''IdProveedor''.
Invalid column name ''IdProveedor''.', N'', CAST(N'2018-11-06T11:18:28.960' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (110, N'BLBeneficiarioAdjuntos', N'An error occurred while updating the entries. See the inner exception for details.', N'The INSERT statement conflicted with the FOREIGN KEY constraint "FK_BeneficiarioAdjuntos_Adjuntos". The conflict occurred in database "OrsnaDatabase", table "dbo.Adjuntos", column ''Id''.
The statement has been terminated.', CAST(N'2018-11-06T11:37:33.343' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (111, N'BLBeneficiarioAdjuntos', N'An error occurred while updating the entries. See the inner exception for details.', N'The INSERT statement conflicted with the FOREIGN KEY constraint "FK_BeneficiarioAdjuntos_Adjuntos". The conflict occurred in database "OrsnaDatabase", table "dbo.Adjuntos", column ''Id''.
The statement has been terminated.', CAST(N'2018-11-06T11:58:28.193' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (112, N'BLBeneficiarioAdjuntos', N'An error occurred while updating the entries. See the inner exception for details.', N'The INSERT statement conflicted with the FOREIGN KEY constraint "FK_BeneficiarioAdjuntos_Adjuntos". The conflict occurred in database "OrsnaDatabase", table "dbo.Adjuntos", column ''Id''.
The statement has been terminated.', CAST(N'2018-11-06T12:01:05.703' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (113, N'BLBeneficiarioAdjuntos', N'The provider for the source IQueryable doesn''t implement IDbAsyncQueryProvider. Only providers that implement IDbAsyncQueryProvider can be used for Entity Framework asynchronous operations. For more details see http://go.microsoft.com/fwlink/?LinkId=287068.', N'', CAST(N'2018-11-06T12:05:37.953' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (114, N'BLBeneficiarioAdjuntos', N'The provider for the source IQueryable doesn''t implement IDbAsyncQueryProvider. Only providers that implement IDbAsyncQueryProvider can be used for Entity Framework asynchronous operations. For more details see http://go.microsoft.com/fwlink/?LinkId=287068.', N'', CAST(N'2018-11-06T12:05:42.583' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (115, N'BLBeneficiarioAdjuntos', N'The provider for the source IQueryable doesn''t implement IDbAsyncQueryProvider. Only providers that implement IDbAsyncQueryProvider can be used for Entity Framework asynchronous operations. For more details see http://go.microsoft.com/fwlink/?LinkId=287068.', N'', CAST(N'2018-11-06T12:05:51.853' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (116, N'BLFile', N'The key value at position 0 of the call to ''DbSet<Adjuntos>.Find'' was of type ''string'', which does not match the property type of ''int''.', N'', CAST(N'2018-11-06T17:38:16.043' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (117, N'BLFile', N'The key value at position 0 of the call to ''DbSet<Adjuntos>.Find'' was of type ''string'', which does not match the property type of ''int''.', N'', CAST(N'2018-11-06T17:38:25.420' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (118, N'BLFile', N'The key value at position 0 of the call to ''DbSet<Adjuntos>.Find'' was of type ''string'', which does not match the property type of ''int''.', N'', CAST(N'2018-11-06T17:38:31.347' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (119, N'BLFile', N'The key value at position 0 of the call to ''DbSet<Adjuntos>.Find'' was of type ''string'', which does not match the property type of ''int''.', N'', CAST(N'2018-11-06T17:39:13.973' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (120, N'BLBeneficiarioAdjuntos', N'The provider for the source IQueryable doesn''t implement IDbAsyncQueryProvider. Only providers that implement IDbAsyncQueryProvider can be used for Entity Framework asynchronous operations. For more details see http://go.microsoft.com/fwlink/?LinkId=287068.', N'', CAST(N'2018-11-07T12:48:18.630' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (121, N'BLBeneficiarioAdjuntos', N'The provider for the source IQueryable doesn''t implement IDbAsyncQueryProvider. Only providers that implement IDbAsyncQueryProvider can be used for Entity Framework asynchronous operations. For more details see http://go.microsoft.com/fwlink/?LinkId=287068.', N'', CAST(N'2018-11-07T12:48:21.113' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (122, N'BLBeneficiarioAdjuntos', N'The provider for the source IQueryable doesn''t implement IDbAsyncQueryProvider. Only providers that implement IDbAsyncQueryProvider can be used for Entity Framework asynchronous operations. For more details see http://go.microsoft.com/fwlink/?LinkId=287068.', N'', CAST(N'2018-11-07T12:48:29.133' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (123, N'BLProyecto', N'Object reference not set to an instance of an object.', N'', CAST(N'2018-11-12T11:44:35.253' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (124, N'BLProyecto', N'Object reference not set to an instance of an object.', N'', CAST(N'2018-11-12T11:44:35.250' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (125, N'BLProyecto', N'Object reference not set to an instance of an object.', N'', CAST(N'2018-11-12T11:45:24.020' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (126, N'BLProyecto', N'Object reference not set to an instance of an object.', N'', CAST(N'2018-11-12T11:45:24.887' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (127, N'BLProyecto', N'Object reference not set to an instance of an object.', N'', CAST(N'2018-11-12T11:45:26.987' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (128, N'BLProyecto', N'Object reference not set to an instance of an object.', N'', CAST(N'2018-11-12T11:47:29.570' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (129, N'BLProyecto', N'Object reference not set to an instance of an object.', N'', CAST(N'2018-11-12T11:50:10.807' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (130, N'BLProyecto', N'Object reference not set to an instance of an object.', N'', CAST(N'2018-11-12T12:11:07.700' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (131, N'BLProyecto', N'Object reference not set to an instance of an object.', N'', CAST(N'2018-11-12T12:11:07.830' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (132, N'BLProyecto', N'Object reference not set to an instance of an object.', N'', CAST(N'2018-11-12T12:11:30.433' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (133, N'BLProyecto', N'Object reference not set to an instance of an object.', N'', CAST(N'2018-11-12T12:11:30.657' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (134, N'BLProyecto', N'Object reference not set to an instance of an object.', N'', CAST(N'2018-11-12T12:14:33.143' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (135, N'BLProyecto', N'Object reference not set to an instance of an object.', N'', CAST(N'2018-11-12T12:14:33.093' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (136, N'BLProyecto', N'Object reference not set to an instance of an object.', N'', CAST(N'2018-11-12T12:15:27.197' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (137, N'BLProyecto', N'Object reference not set to an instance of an object.', N'', CAST(N'2018-11-12T12:15:27.440' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (138, N'BLProyecto', N'Object reference not set to an instance of an object.', N'', CAST(N'2018-11-12T12:16:43.797' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (139, N'BLProyecto', N'Object reference not set to an instance of an object.', N'', CAST(N'2018-11-12T12:18:38.137' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (140, N'BLProyecto', N'Object reference not set to an instance of an object.', N'', CAST(N'2018-11-12T12:18:56.963' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (141, N'BLProyecto', N'Object reference not set to an instance of an object.', N'', CAST(N'2018-11-12T12:49:00.667' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (142, N'BLProyecto', N'Object reference not set to an instance of an object.', N'', CAST(N'2018-11-12T12:49:00.723' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (143, N'BLProyecto', N'Object reference not set to an instance of an object.', N'', CAST(N'2018-11-12T12:53:45.360' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (144, N'BLProyecto', N'Object reference not set to an instance of an object.', N'', CAST(N'2018-11-12T13:53:51.963' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (145, N'BLProyecto', N'Object reference not set to an instance of an object.', N'', CAST(N'2018-11-12T13:53:51.957' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (146, N'BLProyecto', N'Object reference not set to an instance of an object.', N'', CAST(N'2018-11-12T13:53:58.827' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (147, N'BLProyecto', N'Object reference not set to an instance of an object.', N'', CAST(N'2018-11-12T13:53:58.833' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (148, N'BLProyecto', N'Object reference not set to an instance of an object.', N'', CAST(N'2018-11-12T13:54:00.003' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (149, N'BLProyecto', N'Object reference not set to an instance of an object.', N'', CAST(N'2018-11-12T13:54:00.010' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (150, N'BLProyecto', N'Input string was not in a correct format.', N'', CAST(N'2018-11-12T15:11:03.517' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (151, N'BLProyecto', N'Input string was not in a correct format.', N'', CAST(N'2018-11-12T15:27:22.860' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (152, N'BLProyecto', N'Input string was not in a correct format.', N'', CAST(N'2018-11-12T15:28:17.597' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (153, N'BLProyecto', N'Input string was not in a correct format.', N'', CAST(N'2018-11-12T15:28:55.403' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (154, N'BLProyecto', N'Input string was not in a correct format.', N'', CAST(N'2018-11-12T15:28:55.437' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (155, N'BLProyecto', N'Value cannot be null.
Parameter name: source', N'', CAST(N'2018-11-12T15:28:55.513' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (156, N'BLProyecto', N'Input string was not in a correct format.', N'', CAST(N'2018-11-12T15:29:15.697' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (157, N'BLProyecto', N'Input string was not in a correct format.', N'', CAST(N'2018-11-12T15:29:15.700' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (158, N'BLProyecto', N'Value cannot be null.
Parameter name: source', N'', CAST(N'2018-11-12T15:29:15.877' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (159, N'BLProyecto', N'Input string was not in a correct format.', N'', CAST(N'2018-11-12T15:29:24.297' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (160, N'BLProyecto', N'Input string was not in a correct format.', N'', CAST(N'2018-11-12T15:29:24.297' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (161, N'BLProyecto', N'Value cannot be null.
Parameter name: source', N'', CAST(N'2018-11-12T15:29:24.540' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (162, N'BLProyecto', N'Object reference not set to an instance of an object.', N'', CAST(N'2018-11-12T15:38:57.287' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (163, N'BLProyecto', N'Object reference not set to an instance of an object.', N'', CAST(N'2018-11-12T15:39:38.623' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (164, N'BLProyecto', N'Object reference not set to an instance of an object.', N'', CAST(N'2018-11-12T15:39:47.503' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (165, N'BLProyecto', N'Object reference not set to an instance of an object.', N'', CAST(N'2018-11-12T15:45:36.690' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (166, N'BLProyecto', N'Object reference not set to an instance of an object.', N'', CAST(N'2018-11-12T15:47:29.760' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (167, N'BLProyecto', N'Value cannot be null.
Parameter name: querySource', N'', CAST(N'2018-11-12T16:41:50.483' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (168, N'BLProyecto', N'Value cannot be null.
Parameter name: querySource', N'', CAST(N'2018-11-12T16:42:06.157' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (169, N'BLProyecto', N'Value cannot be null.
Parameter name: querySource', N'', CAST(N'2018-11-12T16:43:18.167' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (170, N'BLProyecto', N'Value cannot be null.
Parameter name: querySource', N'', CAST(N'2018-11-12T16:44:55.197' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (171, N'BLProyecto', N'Value cannot be null.
Parameter name: querySource', N'', CAST(N'2018-11-12T16:45:21.757' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (172, N'BLProyecto', N'Value cannot be null.
Parameter name: querySource', N'', CAST(N'2018-11-12T17:48:17.587' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (173, N'BLProyecto', N'Value cannot be null.
Parameter name: querySource', N'', CAST(N'2018-11-12T17:48:16.907' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (174, N'BLProyecto', N'Value cannot be null.
Parameter name: querySource', N'', CAST(N'2018-11-12T17:48:24.647' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (175, N'BLProyecto', N'Value cannot be null.
Parameter name: querySource', N'', CAST(N'2018-11-12T17:59:49.597' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (176, N'BLProyecto', N'Value cannot be null.
Parameter name: querySource', N'', CAST(N'2018-11-12T18:01:20.873' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (177, N'BLProyecto', N'Object reference not set to an instance of an object.', N'', CAST(N'2018-11-12T18:29:53.130' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (178, N'BLProyecto', N'Object reference not set to an instance of an object.', N'', CAST(N'2018-11-12T18:29:53.130' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (179, N'BLProyecto', N'Value cannot be null.
Parameter name: source', N'', CAST(N'2018-11-12T18:29:53.430' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (180, N'BLProyecto', N'Failed to compare two elements in the array.', N'At least one object must implement IComparable.', CAST(N'2018-11-12T18:30:27.167' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (181, N'BLProyecto', N'Failed to compare two elements in the array.', N'At least one object must implement IComparable.', CAST(N'2018-11-12T18:30:27.167' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (182, N'BLProyecto', N'Value cannot be null.
Parameter name: source', N'', CAST(N'2018-11-12T18:30:27.170' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (183, N'BLProyecto', N'Failed to compare two elements in the array.', N'At least one object must implement IComparable.', CAST(N'2018-11-12T18:30:27.887' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (184, N'BLProyecto', N'Failed to compare two elements in the array.', N'At least one object must implement IComparable.', CAST(N'2018-11-12T18:30:27.887' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (185, N'BLProyecto', N'Value cannot be null.
Parameter name: source', N'', CAST(N'2018-11-12T18:30:27.920' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (186, N'BLProyecto', N'Failed to compare two elements in the array.', N'At least one object must implement IComparable.', CAST(N'2018-11-12T18:30:28.507' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (187, N'BLProyecto', N'Failed to compare two elements in the array.', N'At least one object must implement IComparable.', CAST(N'2018-11-12T18:30:28.507' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (188, N'BLProyecto', N'Value cannot be null.
Parameter name: source', N'', CAST(N'2018-11-12T18:30:28.510' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (189, N'BLProyecto', N'Failed to compare two elements in the array.', N'At least one object must implement IComparable.', CAST(N'2018-11-12T18:30:28.987' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (190, N'BLProyecto', N'Failed to compare two elements in the array.', N'At least one object must implement IComparable.', CAST(N'2018-11-12T18:30:28.983' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (191, N'BLProyecto', N'Value cannot be null.
Parameter name: source', N'', CAST(N'2018-11-12T18:30:28.990' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (192, N'BLProyecto', N'An error occurred while updating the entries. See the inner exception for details.', N'SqlDateTime overflow. Must be between 1/1/1753 12:00:00 AM and 12/31/9999 11:59:59 PM.', CAST(N'2018-11-13T14:14:00.073' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (193, N'BLProyecto', N'An error occurred while updating the entries. See the inner exception for details.', N'SqlDateTime overflow. Must be between 1/1/1753 12:00:00 AM and 12/31/9999 11:59:59 PM.', CAST(N'2018-11-13T14:30:18.523' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (194, N'BLProyecto', N'An error occurred while updating the entries. See the inner exception for details.', N'The INSERT statement conflicted with the FOREIGN KEY constraint "FK_ProyectoProvincias_Proyectos". The conflict occurred in database "OrsnaDatabase", table "dbo.Proyectos", column ''Id''.
The statement has been terminated.', CAST(N'2018-11-13T14:34:02.473' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (195, N'BLProyecto', N'Value cannot be null.
Parameter name: source', N'', CAST(N'2018-11-13T14:48:17.560' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (196, N'BLProyecto', N'Failed to compare two elements in the array.', N'At least one object must implement IComparable.', CAST(N'2018-11-13T17:49:44.513' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (197, N'BLProyecto', N'Failed to compare two elements in the array.', N'At least one object must implement IComparable.', CAST(N'2018-11-13T17:49:44.513' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (198, N'BLProyecto', N'Value cannot be null.
Parameter name: source', N'', CAST(N'2018-11-13T17:49:44.563' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (199, N'BLProyecto', N'Failed to compare two elements in the array.', N'At least one object must implement IComparable.', CAST(N'2018-11-13T17:49:56.633' AS DateTime))
GO
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (200, N'BLProyecto', N'Failed to compare two elements in the array.', N'At least one object must implement IComparable.', CAST(N'2018-11-13T17:49:56.633' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (201, N'BLProyecto', N'Value cannot be null.
Parameter name: source', N'', CAST(N'2018-11-13T17:49:56.637' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (202, N'BLProyecto', N'Failed to compare two elements in the array.', N'At least one object must implement IComparable.', CAST(N'2018-11-14T10:00:23.587' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (203, N'BLProyecto', N'Failed to compare two elements in the array.', N'At least one object must implement IComparable.', CAST(N'2018-11-14T10:00:23.587' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (204, N'BLProyecto', N'Value cannot be null.
Parameter name: source', N'', CAST(N'2018-11-14T10:00:23.857' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (205, N'BLProyecto', N'Failed to compare two elements in the array.', N'At least one object must implement IComparable.', CAST(N'2018-11-14T10:00:25.023' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (206, N'BLProyecto', N'Failed to compare two elements in the array.', N'At least one object must implement IComparable.', CAST(N'2018-11-14T10:00:25.023' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (207, N'BLProyecto', N'Value cannot be null.
Parameter name: source', N'', CAST(N'2018-11-14T10:00:25.027' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (208, N'BLProyecto', N'Failed to compare two elements in the array.', N'At least one object must implement IComparable.', CAST(N'2018-11-14T10:00:25.927' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (209, N'BLProyecto', N'Failed to compare two elements in the array.', N'At least one object must implement IComparable.', CAST(N'2018-11-14T10:00:25.927' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (210, N'BLProyecto', N'Value cannot be null.
Parameter name: source', N'', CAST(N'2018-11-14T10:00:25.933' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (211, N'BLProyecto', N'Failed to compare two elements in the array.', N'At least one object must implement IComparable.', CAST(N'2018-11-14T10:00:26.623' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (212, N'BLProyecto', N'Failed to compare two elements in the array.', N'At least one object must implement IComparable.', CAST(N'2018-11-14T10:00:26.623' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (213, N'BLProyecto', N'Value cannot be null.
Parameter name: source', N'', CAST(N'2018-11-14T10:00:26.627' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (214, N'BLProyecto', N'Object reference not set to an instance of an object.', N'', CAST(N'2018-11-14T10:10:14.987' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (215, N'BLProyecto', N'Object reference not set to an instance of an object.', N'', CAST(N'2018-11-14T10:10:14.987' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (216, N'BLProyecto', N'Value cannot be null.
Parameter name: source', N'', CAST(N'2018-11-14T10:10:14.990' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (217, N'BLProyecto', N'Object reference not set to an instance of an object.', N'', CAST(N'2018-11-14T10:10:24.863' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (218, N'BLProyecto', N'Object reference not set to an instance of an object.', N'', CAST(N'2018-11-14T10:10:24.863' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (219, N'BLProyecto', N'Value cannot be null.
Parameter name: source', N'', CAST(N'2018-11-14T10:10:24.867' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (220, N'BLProyecto', N'Failed to compare two elements in the array.', N'At least one object must implement IComparable.', CAST(N'2018-11-15T09:40:03.457' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (221, N'BLProyecto', N'Failed to compare two elements in the array.', N'At least one object must implement IComparable.', CAST(N'2018-11-15T09:40:03.457' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (222, N'BLProyecto', N'Value cannot be null.
Parameter name: source', N'', CAST(N'2018-11-15T09:40:03.733' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (223, N'BLProyecto', N'Failed to compare two elements in the array.', N'At least one object must implement IComparable.', CAST(N'2018-11-15T09:40:04.680' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (224, N'BLProyecto', N'Failed to compare two elements in the array.', N'At least one object must implement IComparable.', CAST(N'2018-11-15T09:40:04.680' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (225, N'BLProyecto', N'Value cannot be null.
Parameter name: source', N'', CAST(N'2018-11-15T09:40:04.683' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (226, N'BLProyectoAdjuntos', N'An error occurred while updating the entries. See the inner exception for details.', N'The INSERT statement conflicted with the FOREIGN KEY constraint "FK_ProyectoAdjuntos_Proyectos". The conflict occurred in database "OrsnaDatabase", table "dbo.Proyectos", column ''Id''.
The statement has been terminated.', CAST(N'2018-11-15T12:48:58.563' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (227, N'BLProyectos', N'Object reference not set to an instance of an object.', N'', CAST(N'2018-11-15T14:11:10.073' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (228, N'BLProyectos', N'Object reference not set to an instance of an object.', N'', CAST(N'2018-11-15T14:11:13.400' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (229, N'BLProyecto', N'Invalid column name ''EstadoProyecto''.', N'', CAST(N'2018-11-16T16:31:13.870' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (230, N'BLProyecto', N'Invalid column name ''EstadoProyecto''.', N'', CAST(N'2018-11-16T16:31:13.870' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (231, N'BLProyecto', N'Value cannot be null.
Parameter name: source', N'', CAST(N'2018-11-16T16:31:19.930' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (232, N'BLProyecto', N'Invalid column name ''EstadoProyecto''.', N'', CAST(N'2018-11-16T16:31:28.850' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (233, N'BLProyecto', N'Invalid column name ''EstadoProyecto''.', N'', CAST(N'2018-11-16T16:31:36.307' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (234, N'BLProyecto', N'Value cannot be null.
Parameter name: source', N'', CAST(N'2018-11-16T16:31:36.340' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (235, N'BLProyecto', N'Invalid column name ''EstadoProyecto''.', N'', CAST(N'2018-11-16T16:34:13.817' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (236, N'BLProyecto', N'Invalid column name ''EstadoProyecto''.', N'', CAST(N'2018-11-16T16:34:13.857' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (237, N'BLProyecto', N'Value cannot be null.
Parameter name: source', N'', CAST(N'2018-11-16T16:34:16.467' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (238, N'BLProyecto', N'Invalid column name ''EstadoProyecto''.', N'', CAST(N'2018-11-16T16:41:39.283' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (239, N'BLProyecto', N'Invalid column name ''EstadoProyecto''.', N'', CAST(N'2018-11-16T16:41:39.283' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (240, N'BLProyecto', N'Value cannot be null.
Parameter name: source', N'', CAST(N'2018-11-16T16:41:39.507' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (241, N'BLProyecto', N'Invalid column name ''EstadoProyecto''.', N'', CAST(N'2018-11-16T16:41:49.550' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (242, N'BLProyecto', N'Invalid column name ''EstadoProyecto''.', N'', CAST(N'2018-11-16T16:41:49.593' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (243, N'BLProyecto', N'Value cannot be null.
Parameter name: source', N'', CAST(N'2018-11-16T16:41:52.490' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (244, N'BLProyecto', N'Invalid column name ''EstadoProyecto''.', N'', CAST(N'2018-11-16T16:42:06.797' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (245, N'BLProyecto', N'Invalid column name ''EstadoProyecto''.', N'', CAST(N'2018-11-16T16:42:06.797' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (246, N'BLProyecto', N'Value cannot be null.
Parameter name: source', N'', CAST(N'2018-11-16T16:42:11.777' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (247, N'BLProyecto', N'Object reference not set to an instance of an object.', N'', CAST(N'2018-11-16T17:42:33.307' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (248, N'BLProyecto', N'Object reference not set to an instance of an object.', N'', CAST(N'2018-11-16T17:46:34.053' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (249, N'BLProyecto', N'Object reference not set to an instance of an object.', N'', CAST(N'2018-11-16T17:49:37.667' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (250, N'BLProyecto', N'Object reference not set to an instance of an object.', N'', CAST(N'2018-11-16T17:50:19.153' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (251, N'BLProyecto', N'Object reference not set to an instance of an object.', N'', CAST(N'2018-11-16T17:51:04.567' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (252, N'BLProyecto', N'Object reference not set to an instance of an object.', N'', CAST(N'2018-11-16T17:51:14.753' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (253, N'BLProyecto', N'Object reference not set to an instance of an object.', N'', CAST(N'2018-11-16T17:52:06.560' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (254, N'BLProyecto', N'Object reference not set to an instance of an object.', N'', CAST(N'2018-11-16T17:52:51.277' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (255, N'BLProyecto', N'Object reference not set to an instance of an object.', N'', CAST(N'2018-11-16T18:16:18.073' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (256, N'BLProyecto', N'Object reference not set to an instance of an object.', N'', CAST(N'2018-11-16T18:17:36.077' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (257, N'BLProyecto', N'Object reference not set to an instance of an object.', N'', CAST(N'2018-11-16T18:18:46.027' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (258, N'BLProyecto', N'Object reference not set to an instance of an object.', N'', CAST(N'2018-11-16T18:22:57.950' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (259, N'BLProyecto', N'Object reference not set to an instance of an object.', N'', CAST(N'2018-11-16T18:29:00.870' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (260, N'BLProyecto', N'Object reference not set to an instance of an object.', N'', CAST(N'2018-11-16T18:29:11.687' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (261, N'BLProyecto', N'Object reference not set to an instance of an object.', N'', CAST(N'2018-11-16T18:38:21.977' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (262, N'BLProyecto', N'Object reference not set to an instance of an object.', N'', CAST(N'2018-11-16T18:41:22.147' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (263, N'BLProyecto', N'Object reference not set to an instance of an object.', N'', CAST(N'2018-11-16T18:44:47.260' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (264, N'BLProyecto', N'Object reference not set to an instance of an object.', N'', CAST(N'2018-11-16T18:48:59.817' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (265, N'BLProyecto', N'Object reference not set to an instance of an object.', N'', CAST(N'2018-11-16T18:50:05.900' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (266, N'BLProyectoAdjuntos', N'An error occurred while updating the entries. See the inner exception for details.', N'The INSERT statement conflicted with the FOREIGN KEY constraint "FK_ProyectoAdjuntos_Proyectos". The conflict occurred in database "OrsnaDatabase", table "dbo.Proyectos", column ''Id''.
The statement has been terminated.', CAST(N'2018-11-16T20:49:32.913' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (267, N'BLProyectoAdjuntos', N'An error occurred while updating the entries. See the inner exception for details.', N'The INSERT statement conflicted with the FOREIGN KEY constraint "FK_ProyectoAdjuntos_Proyectos". The conflict occurred in database "OrsnaDatabase", table "dbo.Proyectos", column ''Id''.
The statement has been terminated.', CAST(N'2018-11-16T20:53:17.550' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (268, N'BLProyecto', N'Invalid column name ''EstadoProyecto''.', N'', CAST(N'2018-11-17T07:35:18.200' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (269, N'BLProyecto', N'Invalid column name ''EstadoProyecto''.', N'', CAST(N'2018-11-17T07:35:18.233' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (270, N'BLProyecto', N'Value cannot be null.
Parameter name: source', N'', CAST(N'2018-11-17T07:35:18.500' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (271, N'BLProyecto', N'Invalid column name ''EstadoProyecto''.', N'', CAST(N'2018-11-17T07:35:18.693' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (272, N'BLProyectoAdjuntos', N'The provider for the source IQueryable doesn''t implement IDbAsyncQueryProvider. Only providers that implement IDbAsyncQueryProvider can be used for Entity Framework asynchronous operations. For more details see http://go.microsoft.com/fwlink/?LinkId=287068.', N'', CAST(N'2018-11-20T17:35:24.987' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (273, N'BLProyectoAdjuntos', N'The provider for the source IQueryable doesn''t implement IDbAsyncQueryProvider. Only providers that implement IDbAsyncQueryProvider can be used for Entity Framework asynchronous operations. For more details see http://go.microsoft.com/fwlink/?LinkId=287068.', N'', CAST(N'2018-11-20T17:53:07.603' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (274, N'BLProyecto', N'Invalid column name ''TipoCuenta''.', N'', CAST(N'2018-11-21T11:02:00.400' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (275, N'BLCuenta', N'Invalid column name ''TipoCuenta''.', N'', CAST(N'2018-11-21T11:02:06.077' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (276, N'BLProyecto', N'Invalid column name ''TipoCuenta''.', N'', CAST(N'2018-11-21T11:02:28.080' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (277, N'BLProyecto', N'Invalid column name ''TipoCuenta''.', N'', CAST(N'2018-11-21T11:08:35.893' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (278, N'BLProyecto', N'Invalid column name ''TipoCuenta''.', N'', CAST(N'2018-11-21T11:08:36.057' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (279, N'BLProyecto', N'Invalid column name ''TipoCuenta''.', N'', CAST(N'2018-11-21T11:08:42.427' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (280, N'BLCuenta', N'Invalid column name ''TipoCuenta''.', N'', CAST(N'2018-11-21T11:08:42.467' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (281, N'BLCuenta', N'Invalid column name ''TipoCuenta''.', N'', CAST(N'2018-11-21T11:08:44.603' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (282, N'BLProyecto', N'Invalid column name ''TipoCuenta''.', N'', CAST(N'2018-11-21T11:08:49.713' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (283, N'BLProyecto', N'Invalid column name ''TipoCuenta''.', N'', CAST(N'2018-11-21T11:08:52.023' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (284, N'BLProyecto', N'Invalid column name ''TipoCuenta''.', N'', CAST(N'2018-11-21T11:08:53.157' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (285, N'BLCuentas', N'Invalid column name ''TipoCuenta''.', N'', CAST(N'2018-11-21T11:08:59.423' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (286, N'BLCuentas', N'Invalid column name ''TipoCuenta''.', N'', CAST(N'2018-11-21T11:08:59.423' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (287, N'BLCuentas', N'Value cannot be null.
Parameter name: source', N'', CAST(N'2018-11-21T11:08:59.430' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (288, N'BLCuenta', N'Invalid column name ''TipoCuenta''.', N'', CAST(N'2018-11-21T11:08:59.863' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (289, N'BLCuentas', N'Invalid column name ''TipoCuenta''.', N'', CAST(N'2018-11-21T11:09:04.563' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (290, N'BLCuentas', N'Invalid column name ''TipoCuenta''.', N'', CAST(N'2018-11-21T11:09:04.563' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (291, N'BLCuentas', N'Value cannot be null.
Parameter name: source', N'', CAST(N'2018-11-21T11:09:04.567' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (292, N'BLCuentas', N'An error occurred while updating the entries. See the inner exception for details.', N'Invalid column name ''TipoCuenta''.', CAST(N'2018-11-21T11:09:16.247' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (293, N'BLCuentas', N'An error occurred while updating the entries. See the inner exception for details.', N'Invalid column name ''TipoCuenta''.', CAST(N'2018-11-21T11:09:36.277' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (294, N'BLCuentas', N'Invalid column name ''TipoCuenta''.', N'', CAST(N'2018-11-21T11:10:04.183' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (295, N'BLCuentas', N'Invalid column name ''TipoCuenta''.', N'', CAST(N'2018-11-21T11:10:04.183' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (296, N'BLCuentas', N'Value cannot be null.
Parameter name: source', N'', CAST(N'2018-11-21T11:10:04.190' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (297, N'BLBeneficiarioAdjuntos', N'The provider for the source IQueryable doesn''t implement IDbAsyncQueryProvider. Only providers that implement IDbAsyncQueryProvider can be used for Entity Framework asynchronous operations. For more details see http://go.microsoft.com/fwlink/?LinkId=287068.', N'', CAST(N'2018-11-23T11:51:36.823' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (298, N'BLBeneficiarioAdjuntos', N'The provider for the source IQueryable doesn''t implement IDbAsyncQueryProvider. Only providers that implement IDbAsyncQueryProvider can be used for Entity Framework asynchronous operations. For more details see http://go.microsoft.com/fwlink/?LinkId=287068.', N'', CAST(N'2018-11-23T11:51:38.790' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (299, N'BLBeneficiarioAdjuntos', N'The provider for the source IQueryable doesn''t implement IDbAsyncQueryProvider. Only providers that implement IDbAsyncQueryProvider can be used for Entity Framework asynchronous operations. For more details see http://go.microsoft.com/fwlink/?LinkId=287068.', N'', CAST(N'2018-11-23T11:51:39.627' AS DateTime))
GO
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (300, N'BLBeneficiarioAdjuntos', N'The provider for the source IQueryable doesn''t implement IDbAsyncQueryProvider. Only providers that implement IDbAsyncQueryProvider can be used for Entity Framework asynchronous operations. For more details see http://go.microsoft.com/fwlink/?LinkId=287068.', N'', CAST(N'2018-11-23T11:51:39.833' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (301, N'BLBeneficiarioAdjuntos', N'The provider for the source IQueryable doesn''t implement IDbAsyncQueryProvider. Only providers that implement IDbAsyncQueryProvider can be used for Entity Framework asynchronous operations. For more details see http://go.microsoft.com/fwlink/?LinkId=287068.', N'', CAST(N'2018-11-23T11:51:45.910' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (302, N'BLBeneficiarioAdjuntos', N'The provider for the source IQueryable doesn''t implement IDbAsyncQueryProvider. Only providers that implement IDbAsyncQueryProvider can be used for Entity Framework asynchronous operations. For more details see http://go.microsoft.com/fwlink/?LinkId=287068.', N'', CAST(N'2018-11-23T11:51:46.343' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (303, N'BLBeneficiarioAdjuntos', N'The provider for the source IQueryable doesn''t implement IDbAsyncQueryProvider. Only providers that implement IDbAsyncQueryProvider can be used for Entity Framework asynchronous operations. For more details see http://go.microsoft.com/fwlink/?LinkId=287068.', N'', CAST(N'2018-11-23T11:51:46.547' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (304, N'BLBeneficiarioAdjuntos', N'The provider for the source IQueryable doesn''t implement IDbAsyncQueryProvider. Only providers that implement IDbAsyncQueryProvider can be used for Entity Framework asynchronous operations. For more details see http://go.microsoft.com/fwlink/?LinkId=287068.', N'', CAST(N'2018-11-23T12:10:58.233' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (305, N'BLBeneficiarioAdjuntos', N'The provider for the source IQueryable doesn''t implement IDbAsyncQueryProvider. Only providers that implement IDbAsyncQueryProvider can be used for Entity Framework asynchronous operations. For more details see http://go.microsoft.com/fwlink/?LinkId=287068.', N'', CAST(N'2018-11-23T12:11:02.797' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (306, N'BLBeneficiarioAdjuntos', N'The provider for the source IQueryable doesn''t implement IDbAsyncQueryProvider. Only providers that implement IDbAsyncQueryProvider can be used for Entity Framework asynchronous operations. For more details see http://go.microsoft.com/fwlink/?LinkId=287068.', N'', CAST(N'2018-11-23T12:11:03.640' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (307, N'BLBeneficiarioAdjuntos', N'The provider for the source IQueryable doesn''t implement IDbAsyncQueryProvider. Only providers that implement IDbAsyncQueryProvider can be used for Entity Framework asynchronous operations. For more details see http://go.microsoft.com/fwlink/?LinkId=287068.', N'', CAST(N'2018-11-23T12:11:04.337' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (308, N'BLBeneficiarioAdjuntos', N'The provider for the source IQueryable doesn''t implement IDbAsyncQueryProvider. Only providers that implement IDbAsyncQueryProvider can be used for Entity Framework asynchronous operations. For more details see http://go.microsoft.com/fwlink/?LinkId=287068.', N'', CAST(N'2018-11-23T12:11:04.547' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (309, N'BLBeneficiarioAdjuntos', N'The provider for the source IQueryable doesn''t implement IDbAsyncQueryProvider. Only providers that implement IDbAsyncQueryProvider can be used for Entity Framework asynchronous operations. For more details see http://go.microsoft.com/fwlink/?LinkId=287068.', N'', CAST(N'2018-11-23T12:11:04.737' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (310, N'BLBeneficiarioAdjuntos', N'The provider for the source IQueryable doesn''t implement IDbAsyncQueryProvider. Only providers that implement IDbAsyncQueryProvider can be used for Entity Framework asynchronous operations. For more details see http://go.microsoft.com/fwlink/?LinkId=287068.', N'', CAST(N'2018-11-23T12:11:04.913' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (311, N'BLBeneficiarioAdjuntos', N'The provider for the source IQueryable doesn''t implement IDbAsyncQueryProvider. Only providers that implement IDbAsyncQueryProvider can be used for Entity Framework asynchronous operations. For more details see http://go.microsoft.com/fwlink/?LinkId=287068.', N'', CAST(N'2018-11-23T12:11:05.110' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (312, N'BLBeneficiarioAdjuntos', N'The provider for the source IQueryable doesn''t implement IDbAsyncQueryProvider. Only providers that implement IDbAsyncQueryProvider can be used for Entity Framework asynchronous operations. For more details see http://go.microsoft.com/fwlink/?LinkId=287068.', N'', CAST(N'2018-11-23T12:11:05.763' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (313, N'BLBeneficiarioAdjuntos', N'The provider for the source IQueryable doesn''t implement IDbAsyncQueryProvider. Only providers that implement IDbAsyncQueryProvider can be used for Entity Framework asynchronous operations. For more details see http://go.microsoft.com/fwlink/?LinkId=287068.', N'', CAST(N'2018-11-23T12:11:23.947' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (314, N'BLFile', N'The input is not a valid Base-64 string as it contains a non-base 64 character, more than two padding characters, or an illegal character among the padding characters.', N'', CAST(N'2018-11-25T09:25:57.083' AS DateTime))
INSERT [dbo].[Log] ([Id], [Ubicacion], [Mensaje], [Detalle], [Fecha]) VALUES (315, N'BLFile', N'The input is not a valid Base-64 string as it contains a non-base 64 character, more than two padding characters, or an illegal character among the padding characters.', N'', CAST(N'2018-11-25T09:26:03.023' AS DateTime))
SET IDENTITY_INSERT [dbo].[Log] OFF
SET IDENTITY_INSERT [dbo].[Provincias] ON 

INSERT [dbo].[Provincias] ([Id], [Nombre], [Estado]) VALUES (1, N'Buenos Aires', 1)
SET IDENTITY_INSERT [dbo].[Provincias] OFF
SET IDENTITY_INSERT [dbo].[ProyectoAdjuntos] ON 

INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (23, 56, 39, 1)
INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (24, 57, 40, 1)
INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (25, 56, 41, 1)
INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (28, 56, 44, 1)
INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (29, 56, 45, 1)
INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (30, 58, 46, 1)
INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (31, 58, 47, 1)
INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (32, 58, 48, 0)
INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (33, 58, 49, 0)
INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (34, 57, 50, 1)
INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (35, 57, 51, 1)
INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (36, 57, 52, 0)
INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (37, 59, 53, 1)
INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (38, 60, 54, 1)
INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (39, 61, 55, 1)
INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (40, 62, 56, 1)
INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (41, 60, 57, 0)
INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (42, 60, 58, 1)
INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (43, 60, 59, 0)
INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (44, 60, 60, 0)
INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (45, 60, 61, 0)
INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (46, 60, 62, 0)
INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (47, 64, 63, 1)
INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (48, 64, 64, 0)
INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (49, 60, 65, 0)
INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (50, 60, 66, 0)
INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (51, 62, 67, 0)
INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (52, 57, 68, 0)
INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (53, 61, 69, 0)
INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (54, 59, 70, 0)
INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (55, 61, 71, 0)
INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (56, 60, 72, 0)
INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (57, 58, 73, 1)
INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (58, 63, 74, 1)
INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (59, 60, 75, 0)
INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (60, 65, 76, 1)
INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (61, 65, 77, 0)
INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (62, 65, 78, 1)
INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (63, 61, 79, 0)
INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (64, 61, 80, 1)
INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (65, 64, 81, 1)
INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (66, 66, 85, 1)
INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (67, 67, 86, 1)
INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (68, 67, 87, 1)
INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (69, 68, 88, 1)
INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (70, 71, 89, 1)
INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (71, 72, 90, 1)
INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (72, 72, 91, 1)
INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (73, 72, 92, 1)
INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (74, 73, 93, 1)
INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (75, 73, 94, 1)
INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (76, 73, 95, 1)
INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (77, 73, 96, 0)
INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (78, 73, 97, 0)
INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (79, 73, 98, 0)
INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (80, 73, 99, 0)
INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (81, 73, 100, 1)
INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (82, 73, 101, 1)
SET IDENTITY_INSERT [dbo].[ProyectoAdjuntos] OFF
SET IDENTITY_INSERT [dbo].[ProyectoAeropuertos] ON 

INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (51, 57, 1, 0)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (52, 57, 2, 0)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (53, 56, 1, 0)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (54, 56, 1, 1)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (55, 58, 2, 0)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (56, 58, 3, 0)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (57, 58, 2, 0)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (58, 58, 3, 0)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (59, 58, 2, 0)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (60, 58, 3, 0)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (61, 58, 2, 1)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (62, 58, 3, 1)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (63, 57, 1, 0)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (64, 57, 2, 0)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (65, 57, 1, 1)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (66, 57, 2, 1)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (67, 59, 2, 1)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (68, 59, 3, 1)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (69, 60, 3, 0)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (70, 60, 3, 0)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (71, 60, 3, 0)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (72, 60, 2, 0)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (73, 61, 1, 1)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (74, 62, 1, 1)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (75, 60, 2, 0)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (76, 60, 3, 0)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (77, 60, 2, 0)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (78, 60, 3, 0)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (79, 60, 2, 0)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (80, 60, 2, 0)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (81, 60, 1, 0)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (82, 60, 3, 0)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (83, 63, 1, 1)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (84, 64, 3, 0)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (85, 64, 3, 0)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (86, 64, 3, 1)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (87, 64, 1, 1)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (88, 65, 1, 1)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (89, 60, 1, 1)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (90, 60, 2, 1)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (91, 60, 3, 1)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (92, 66, 1, 1)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (93, 67, 2, 0)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (94, 67, 2, 0)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (95, 67, 2, 0)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (96, 67, 3, 0)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (97, 67, 2, 1)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (98, 67, 3, 1)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (99, 68, 1, 1)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (100, 69, 1, 1)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (101, 69, 3, 1)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (102, 70, 1, 1)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (103, 71, 1, 1)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (104, 72, 1, 0)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (105, 72, 2, 0)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (106, 72, 1, 0)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (107, 72, 2, 0)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (108, 72, 1, 0)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (109, 72, 2, 0)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (110, 72, 1, 1)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (111, 72, 2, 1)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (112, 73, 1, 0)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (113, 73, 2, 0)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (114, 73, 1, 0)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (115, 73, 2, 0)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (116, 73, 1, 0)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (117, 73, 2, 0)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (118, 73, 1, 0)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (119, 73, 1, 0)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (120, 73, 1, 0)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (121, 73, 1, 0)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (122, 73, 1, 0)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (123, 73, 1, 0)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (124, 73, 1, 0)
INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (125, 73, 1, 1)
SET IDENTITY_INSERT [dbo].[ProyectoAeropuertos] OFF
SET IDENTITY_INSERT [dbo].[Proyectos] ON 

INSERT [dbo].[Proyectos] ([Id], [Nombre], [Descripcion], [IdCuenta], [IdProyecto], [IdArea], [NroContratacion], [NroObra], [CodObra], [NroPago], [NormaLegal], [Objeto], [MontoTotal], [FechaCreacion], [TipoEstado], [Estado], [IdProyectosEstado]) VALUES (56, N'sdfgh', N'gdsfghj', 25, N'123456', 21, N'23456', N'sadfgh', N'sdfgh', N'dsfghj', N'sdfgh', N'dsfghsadfghjkhgfdsadfghjk', CAST(0.00 AS Decimal(18, 2)), CAST(N'2018-11-16T00:00:00.000' AS DateTime), NULL, 0, 3)
INSERT [dbo].[Proyectos] ([Id], [Nombre], [Descripcion], [IdCuenta], [IdProyecto], [IdArea], [NroContratacion], [NroObra], [CodObra], [NroPago], [NormaLegal], [Objeto], [MontoTotal], [FechaCreacion], [TipoEstado], [Estado], [IdProyectosEstado]) VALUES (57, N'sadfgh', N'dsfgh', 25, N'3456', 21, N'34567', N'sdfgh', N'dsfgh', N'dsfghj', N'dsfgh', N'sdfg', CAST(0.00 AS Decimal(18, 2)), CAST(N'2018-11-16T00:00:00.000' AS DateTime), NULL, 1, 1)
INSERT [dbo].[Proyectos] ([Id], [Nombre], [Descripcion], [IdCuenta], [IdProyecto], [IdArea], [NroContratacion], [NroObra], [CodObra], [NroPago], [NormaLegal], [Objeto], [MontoTotal], [FechaCreacion], [TipoEstado], [Estado], [IdProyectosEstado]) VALUES (58, N'dsfghj', N'dsfghj', 25, N'456789', 22, N'4567', N'34567', N'sdfghj', N'dfghb', N'xcvb', N'objeto', CAST(0.00 AS Decimal(18, 2)), CAST(N'2018-11-16T00:00:00.000' AS DateTime), NULL, 1, 3)
INSERT [dbo].[Proyectos] ([Id], [Nombre], [Descripcion], [IdCuenta], [IdProyecto], [IdArea], [NroContratacion], [NroObra], [CodObra], [NroPago], [NormaLegal], [Objeto], [MontoTotal], [FechaCreacion], [TipoEstado], [Estado], [IdProyectosEstado]) VALUES (59, N'sdfgh', N'fgh', 24, N'34567', 22, N'34567', N'dsfgh', N'dfgh', N'dsfgh', N'dfghj', N'dfghj', CAST(0.00 AS Decimal(18, 2)), CAST(N'2018-11-18T00:00:00.000' AS DateTime), NULL, 1, 2)
INSERT [dbo].[Proyectos] ([Id], [Nombre], [Descripcion], [IdCuenta], [IdProyecto], [IdArea], [NroContratacion], [NroObra], [CodObra], [NroPago], [NormaLegal], [Objeto], [MontoTotal], [FechaCreacion], [TipoEstado], [Estado], [IdProyectosEstado]) VALUES (60, N'Pista Aterrizaje', N'Pista CABA', 23, N'001', 21, N'300', N'OBRA0102', N'CABA', N'PAGO01', N'NORMA01', N'Pista CABA', CAST(0.00 AS Decimal(18, 2)), CAST(N'2018-11-20T00:00:00.000' AS DateTime), NULL, 1, 1)
INSERT [dbo].[Proyectos] ([Id], [Nombre], [Descripcion], [IdCuenta], [IdProyecto], [IdArea], [NroContratacion], [NroObra], [CodObra], [NroPago], [NormaLegal], [Objeto], [MontoTotal], [FechaCreacion], [TipoEstado], [Estado], [IdProyectosEstado]) VALUES (61, N'Pista Ezeiza', N'1111', 23, N'002', 21, N'1212', N'1212', N'CABA Ezeiza', N'1212', N'1212', N'1212', CAST(0.00 AS Decimal(18, 2)), CAST(N'2018-11-20T00:00:00.000' AS DateTime), NULL, 1, 1)
INSERT [dbo].[Proyectos] ([Id], [Nombre], [Descripcion], [IdCuenta], [IdProyecto], [IdArea], [NroContratacion], [NroObra], [CodObra], [NroPago], [NormaLegal], [Objeto], [MontoTotal], [FechaCreacion], [TipoEstado], [Estado], [IdProyectosEstado]) VALUES (62, N'Ezeiza', N'Ezeiza', 29, N'200', 27, N'0101', N'0101', N'Ezeiza', N'0101', N'0101', N'0101', CAST(0.00 AS Decimal(18, 2)), CAST(N'2018-11-20T00:00:00.000' AS DateTime), NULL, 1, 1)
INSERT [dbo].[Proyectos] ([Id], [Nombre], [Descripcion], [IdCuenta], [IdProyecto], [IdArea], [NroContratacion], [NroObra], [CodObra], [NroPago], [NormaLegal], [Objeto], [MontoTotal], [FechaCreacion], [TipoEstado], [Estado], [IdProyectosEstado]) VALUES (63, N'Ampliacion de pistas', N'pista A', 25, N'353433', 23, N'4545', N'7654', N'34543', N'3456', N'345', N'345', CAST(0.00 AS Decimal(18, 2)), CAST(N'2018-11-20T00:00:00.000' AS DateTime), NULL, 1, 1)
INSERT [dbo].[Proyectos] ([Id], [Nombre], [Descripcion], [IdCuenta], [IdProyecto], [IdArea], [NroContratacion], [NroObra], [CodObra], [NroPago], [NormaLegal], [Objeto], [MontoTotal], [FechaCreacion], [TipoEstado], [Estado], [IdProyectosEstado]) VALUES (64, N'Ampliacion de embarques', N'salas de embarque', 25, N'33232', 23, N'65345', N'45678', N'6454345', N'34567', N'3456', N'mejoras del aeropuerto', CAST(0.00 AS Decimal(18, 2)), CAST(N'2018-11-20T00:00:00.000' AS DateTime), NULL, 1, 2)
INSERT [dbo].[Proyectos] ([Id], [Nombre], [Descripcion], [IdCuenta], [IdProyecto], [IdArea], [NroContratacion], [NroObra], [CodObra], [NroPago], [NormaLegal], [Objeto], [MontoTotal], [FechaCreacion], [TipoEstado], [Estado], [IdProyectosEstado]) VALUES (65, N'sdfg', N'asdfgh', 26, N'345', 23, N'', N'', N'', N'', N'', N'', CAST(0.00 AS Decimal(18, 2)), CAST(N'2018-11-20T00:00:00.000' AS DateTime), NULL, 1, 2)
INSERT [dbo].[Proyectos] ([Id], [Nombre], [Descripcion], [IdCuenta], [IdProyecto], [IdArea], [NroContratacion], [NroObra], [CodObra], [NroPago], [NormaLegal], [Objeto], [MontoTotal], [FechaCreacion], [TipoEstado], [Estado], [IdProyectosEstado]) VALUES (66, N'fasdfasd', N'asdfasdf', 23, N'123', 21, N'', N'', N'', N'', N'', N'', CAST(0.00 AS Decimal(18, 2)), CAST(N'2018-11-23T00:00:00.000' AS DateTime), NULL, 1, 1)
INSERT [dbo].[Proyectos] ([Id], [Nombre], [Descripcion], [IdCuenta], [IdProyecto], [IdArea], [NroContratacion], [NroObra], [CodObra], [NroPago], [NormaLegal], [Objeto], [MontoTotal], [FechaCreacion], [TipoEstado], [Estado], [IdProyectosEstado]) VALUES (67, N'Prueba Nuevo Proyecto', N'Prueba de carga  nuevo proyecto', 32, N'555', 22, N'12', N'14', N'ZN', N'45', N'123', N'Prueba Objeto', CAST(454.78 AS Decimal(18, 2)), CAST(N'2018-11-23T00:00:00.000' AS DateTime), NULL, 1, 2)
INSERT [dbo].[Proyectos] ([Id], [Nombre], [Descripcion], [IdCuenta], [IdProyecto], [IdArea], [NroContratacion], [NroObra], [CodObra], [NroPago], [NormaLegal], [Objeto], [MontoTotal], [FechaCreacion], [TipoEstado], [Estado], [IdProyectosEstado]) VALUES (68, N'Prueba Nuevo Proyecto 777', N'Prueba de carga  nuevo proyecto ', 29, N'777', 34, N'1', N'2', N'ZN', N'3', N'4', N'', CAST(10.00 AS Decimal(18, 2)), CAST(N'2018-11-25T00:00:00.000' AS DateTime), NULL, 1, 1)
INSERT [dbo].[Proyectos] ([Id], [Nombre], [Descripcion], [IdCuenta], [IdProyecto], [IdArea], [NroContratacion], [NroObra], [CodObra], [NroPago], [NormaLegal], [Objeto], [MontoTotal], [FechaCreacion], [TipoEstado], [Estado], [IdProyectosEstado]) VALUES (69, N'Prueba Nuevo Proyecto 888', N'Prueba de carga  nuevo proyecto ', 35, N'888', 34, N'asdf', N'sdfasdfa', N'ZN', N'fasdfds', N'fasdf', N'Prueba Objeto', CAST(25000.00 AS Decimal(18, 2)), CAST(N'2018-11-25T00:00:00.000' AS DateTime), NULL, 0, 1)
INSERT [dbo].[Proyectos] ([Id], [Nombre], [Descripcion], [IdCuenta], [IdProyecto], [IdArea], [NroContratacion], [NroObra], [CodObra], [NroPago], [NormaLegal], [Objeto], [MontoTotal], [FechaCreacion], [TipoEstado], [Estado], [IdProyectosEstado]) VALUES (70, N'Prueba Nuevo Proyecto 12345', N'Prueba de carga  nuevo proyecto ', 35, N'12345', 34, N'123', N'345', N'ZN', N'567', N'Norma Legal', N'Prueba  Objeto', CAST(25000.00 AS Decimal(18, 2)), CAST(N'2018-11-25T00:00:00.000' AS DateTime), NULL, 1, 1)
INSERT [dbo].[Proyectos] ([Id], [Nombre], [Descripcion], [IdCuenta], [IdProyecto], [IdArea], [NroContratacion], [NroObra], [CodObra], [NroPago], [NormaLegal], [Objeto], [MontoTotal], [FechaCreacion], [TipoEstado], [Estado], [IdProyectosEstado]) VALUES (71, N'Prueba Nuevo Proyecto 567', N'Prueba de carga  nuevo proyecto ', 35, N'567', 34, N'1', N'2', N'CABA', N'3', N'4', N'asdfasdf', CAST(12000.00 AS Decimal(18, 2)), CAST(N'2018-11-25T00:00:00.000' AS DateTime), NULL, 1, 1)
INSERT [dbo].[Proyectos] ([Id], [Nombre], [Descripcion], [IdCuenta], [IdProyecto], [IdArea], [NroContratacion], [NroObra], [CodObra], [NroPago], [NormaLegal], [Objeto], [MontoTotal], [FechaCreacion], [TipoEstado], [Estado], [IdProyectosEstado]) VALUES (72, N'Prueba Nuevo Proyecto 789', N'Prueba de carga  nuevo proyecto  ', 35, N'789', 34, N'12', N'13', N'CABA', N'14', N'Norma Legal 4', N'Prueba Objeto', CAST(15000.00 AS Decimal(18, 2)), CAST(N'2018-11-25T00:00:00.000' AS DateTime), NULL, 0, 1)
INSERT [dbo].[Proyectos] ([Id], [Nombre], [Descripcion], [IdCuenta], [IdProyecto], [IdArea], [NroContratacion], [NroObra], [CodObra], [NroPago], [NormaLegal], [Objeto], [MontoTotal], [FechaCreacion], [TipoEstado], [Estado], [IdProyectosEstado]) VALUES (73, N'Prueba Nuevo Proyecto 6789 ', N'Prueba de carga  nuevo proyecto  ', 36, N'6789', 34, N'12', N'13', N'CABA', N'14', N'Norma Legal 4', N'Otra Prueba Objeto', CAST(17000.00 AS Decimal(18, 2)), CAST(N'2018-11-25T00:00:00.000' AS DateTime), NULL, 1, 2)
SET IDENTITY_INSERT [dbo].[Proyectos] OFF
SET IDENTITY_INSERT [dbo].[ProyectosEstado] ON 

INSERT [dbo].[ProyectosEstado] ([Id], [Nombre], [Estado]) VALUES (1, N'EN EJECUCIÓN', 1)
INSERT [dbo].[ProyectosEstado] ([Id], [Nombre], [Estado]) VALUES (2, N'EJECUTADO 100%', 1)
INSERT [dbo].[ProyectosEstado] ([Id], [Nombre], [Estado]) VALUES (3, N'FINALIZADO', 1)
SET IDENTITY_INSERT [dbo].[ProyectosEstado] OFF
SET IDENTITY_INSERT [dbo].[Usuarios] ON 

INSERT [dbo].[Usuarios] ([Id], [email], [password], [Estado]) VALUES (1, N'root@root.com', N'123', 1)
SET IDENTITY_INSERT [dbo].[Usuarios] OFF
ALTER TABLE [dbo].[Adjuntos] ADD  CONSTRAINT [DF_Adjuntos_Estado]  DEFAULT ((1)) FOR [Estado]
GO
ALTER TABLE [dbo].[AeropuertosGrupo] ADD  CONSTRAINT [DF_AeropuertosGrupo_Estado]  DEFAULT ((1)) FOR [Estado]
GO
ALTER TABLE [dbo].[Areas] ADD  CONSTRAINT [DF_Areas_Estado]  DEFAULT ((1)) FOR [Estado]
GO
ALTER TABLE [dbo].[AreasModulos] ADD  CONSTRAINT [DF_AreasModulos_Estado]  DEFAULT ((1)) FOR [Estado]
GO
ALTER TABLE [dbo].[AreasModulosPermisos] ADD  CONSTRAINT [DF_AreasModulosPermisos_Estado]  DEFAULT ((1)) FOR [Estado]
GO
ALTER TABLE [dbo].[BeneficiarioAdjuntos] ADD  CONSTRAINT [DF_ProveedorAdjuntos_Baja]  DEFAULT ((1)) FOR [Estado]
GO
ALTER TABLE [dbo].[BeneficiarioBancos] ADD  CONSTRAINT [DF_BancoProveedores_Baja]  DEFAULT ((1)) FOR [Estado]
GO
ALTER TABLE [dbo].[Beneficiarios] ADD  CONSTRAINT [DF_Proveedores_Baja]  DEFAULT ((1)) FOR [Estado]
GO
ALTER TABLE [dbo].[Cuentas] ADD  CONSTRAINT [DF_Cuentas_Estado]  DEFAULT ((1)) FOR [Estado]
GO
ALTER TABLE [dbo].[LibranzaAeropuertos] ADD  CONSTRAINT [DF_LibranzaAeropuertos_Estado]  DEFAULT ((1)) FOR [Estado]
GO
ALTER TABLE [dbo].[LibranzaBeneficiarios] ADD  CONSTRAINT [DF_LibranzaBeneficiarios_Estado]  DEFAULT ((1)) FOR [Estado]
GO
ALTER TABLE [dbo].[LibranzaCesiones] ADD  CONSTRAINT [DF_LibranzaCesiones_Baja]  DEFAULT ((1)) FOR [Estado]
GO
ALTER TABLE [dbo].[LibranzaEmbargos] ADD  CONSTRAINT [DF_LibranzaEmbargos_Baja]  DEFAULT ((1)) FOR [Estado]
GO
ALTER TABLE [dbo].[LibranzaFacturas] ADD  CONSTRAINT [DF_LibranzaFacturas_Baja]  DEFAULT ((1)) FOR [Estado]
GO
ALTER TABLE [dbo].[Libranzas] ADD  CONSTRAINT [DF_Libranzas_Baja]  DEFAULT ((1)) FOR [Estado]
GO
ALTER TABLE [dbo].[LibranzasEstado] ADD  CONSTRAINT [DF_EstadoLibranzas_Estado]  DEFAULT ((1)) FOR [Estado]
GO
ALTER TABLE [dbo].[LibranzaTipo] ADD  CONSTRAINT [DF_LibranzaTipo_Estado]  DEFAULT ((1)) FOR [Estado]
GO
ALTER TABLE [dbo].[Modulos] ADD  CONSTRAINT [DF_Modulos_Estado]  DEFAULT ((1)) FOR [Estado]
GO
ALTER TABLE [dbo].[Permisos] ADD  CONSTRAINT [DF_Permisos_Estado]  DEFAULT ((1)) FOR [Estado]
GO
ALTER TABLE [dbo].[Provincias] ADD  CONSTRAINT [DF_Provincias_Baja]  DEFAULT ((1)) FOR [Estado]
GO
ALTER TABLE [dbo].[ProyectoAdjuntos] ADD  CONSTRAINT [DF_ProyectoAdjuntos_Baja]  DEFAULT ((1)) FOR [Estado]
GO
ALTER TABLE [dbo].[ProyectoAeropuertos] ADD  CONSTRAINT [DF_ProyectoAeropuertos_Baja]  DEFAULT ((1)) FOR [Estado]
GO
ALTER TABLE [dbo].[ProyectoBeneficiarios] ADD  CONSTRAINT [DF_ProyectoProveedores_Baja]  DEFAULT ((1)) FOR [Estado]
GO
ALTER TABLE [dbo].[ProyectoProvincias] ADD  CONSTRAINT [DF_ProyectoProvincias_Baja]  DEFAULT ((1)) FOR [Estado]
GO
ALTER TABLE [dbo].[Proyectos] ADD  CONSTRAINT [DF_Proyectos_Baja]  DEFAULT ((1)) FOR [Estado]
GO
ALTER TABLE [dbo].[ProyectosEstado] ADD  CONSTRAINT [DF_ProyectosGrupo_Estado]  DEFAULT ((1)) FOR [Estado]
GO
ALTER TABLE [dbo].[Usuarios] ADD  CONSTRAINT [DF_Usuarios_Estado]  DEFAULT ((1)) FOR [Estado]
GO
ALTER TABLE [dbo].[UsuariosAreas] ADD  CONSTRAINT [DF_UsuariosAreas_Estado]  DEFAULT ((1)) FOR [Estado]
GO
ALTER TABLE [dbo].[Aeropuertos]  WITH CHECK ADD  CONSTRAINT [FK_Aeropuertos_AeropuertosGrupo] FOREIGN KEY([IdAeropuertosGrupo])
REFERENCES [dbo].[AeropuertosGrupo] ([Id])
GO
ALTER TABLE [dbo].[Aeropuertos] CHECK CONSTRAINT [FK_Aeropuertos_AeropuertosGrupo]
GO
ALTER TABLE [dbo].[Aeropuertos]  WITH CHECK ADD  CONSTRAINT [FK_Aeropuertos_Provincias] FOREIGN KEY([IdProvincia])
REFERENCES [dbo].[Provincias] ([Id])
GO
ALTER TABLE [dbo].[Aeropuertos] CHECK CONSTRAINT [FK_Aeropuertos_Provincias]
GO
ALTER TABLE [dbo].[AreasModulos]  WITH CHECK ADD  CONSTRAINT [FK_AreasModulos_Areas] FOREIGN KEY([IdArea])
REFERENCES [dbo].[Areas] ([Id])
GO
ALTER TABLE [dbo].[AreasModulos] CHECK CONSTRAINT [FK_AreasModulos_Areas]
GO
ALTER TABLE [dbo].[AreasModulos]  WITH CHECK ADD  CONSTRAINT [FK_AreasModulos_Modulos] FOREIGN KEY([IdModulo])
REFERENCES [dbo].[Modulos] ([Id])
GO
ALTER TABLE [dbo].[AreasModulos] CHECK CONSTRAINT [FK_AreasModulos_Modulos]
GO
ALTER TABLE [dbo].[AreasModulosPermisos]  WITH CHECK ADD  CONSTRAINT [FK_AreasModulosPermisos_AreasModulos] FOREIGN KEY([IdAreaModulo])
REFERENCES [dbo].[AreasModulos] ([Id])
GO
ALTER TABLE [dbo].[AreasModulosPermisos] CHECK CONSTRAINT [FK_AreasModulosPermisos_AreasModulos]
GO
ALTER TABLE [dbo].[AreasModulosPermisos]  WITH CHECK ADD  CONSTRAINT [FK_AreasModulosPermisos_Permisos] FOREIGN KEY([IdPermiso])
REFERENCES [dbo].[Permisos] ([Id])
GO
ALTER TABLE [dbo].[AreasModulosPermisos] CHECK CONSTRAINT [FK_AreasModulosPermisos_Permisos]
GO
ALTER TABLE [dbo].[BeneficiarioAdjuntos]  WITH CHECK ADD  CONSTRAINT [FK_BeneficiarioAdjuntos_Adjuntos] FOREIGN KEY([IdAdjunto])
REFERENCES [dbo].[Adjuntos] ([Id])
GO
ALTER TABLE [dbo].[BeneficiarioAdjuntos] CHECK CONSTRAINT [FK_BeneficiarioAdjuntos_Adjuntos]
GO
ALTER TABLE [dbo].[BeneficiarioAdjuntos]  WITH CHECK ADD  CONSTRAINT [FK_BeneficiarioAdjuntos_Beneficiarios] FOREIGN KEY([IdBeneficiario])
REFERENCES [dbo].[Beneficiarios] ([Id])
GO
ALTER TABLE [dbo].[BeneficiarioAdjuntos] CHECK CONSTRAINT [FK_BeneficiarioAdjuntos_Beneficiarios]
GO
ALTER TABLE [dbo].[BeneficiarioBancos]  WITH CHECK ADD  CONSTRAINT [FK_BancoProveedores_Proveedores] FOREIGN KEY([IdBeneficiario])
REFERENCES [dbo].[Beneficiarios] ([Id])
GO
ALTER TABLE [dbo].[BeneficiarioBancos] CHECK CONSTRAINT [FK_BancoProveedores_Proveedores]
GO
ALTER TABLE [dbo].[Cuentas]  WITH CHECK ADD  CONSTRAINT [FK_Cuentas_AeropuertosGrupo] FOREIGN KEY([IdAeropuertosGrupo])
REFERENCES [dbo].[AeropuertosGrupo] ([Id])
GO
ALTER TABLE [dbo].[Cuentas] CHECK CONSTRAINT [FK_Cuentas_AeropuertosGrupo]
GO
ALTER TABLE [dbo].[Cuentas]  WITH CHECK ADD  CONSTRAINT [FK_Cuentas_LibranzaTipo] FOREIGN KEY([IdLibranzaTipo])
REFERENCES [dbo].[LibranzaTipo] ([Id])
GO
ALTER TABLE [dbo].[Cuentas] CHECK CONSTRAINT [FK_Cuentas_LibranzaTipo]
GO
ALTER TABLE [dbo].[LibranzaAeropuertos]  WITH CHECK ADD  CONSTRAINT [FK_LibranzaAeropuertos_Aeropuertos] FOREIGN KEY([IdAeropuerto])
REFERENCES [dbo].[Aeropuertos] ([Id])
GO
ALTER TABLE [dbo].[LibranzaAeropuertos] CHECK CONSTRAINT [FK_LibranzaAeropuertos_Aeropuertos]
GO
ALTER TABLE [dbo].[LibranzaAeropuertos]  WITH CHECK ADD  CONSTRAINT [FK_LibranzaAeropuertos_Libranzas] FOREIGN KEY([IdLibranza])
REFERENCES [dbo].[Libranzas] ([Id])
GO
ALTER TABLE [dbo].[LibranzaAeropuertos] CHECK CONSTRAINT [FK_LibranzaAeropuertos_Libranzas]
GO
ALTER TABLE [dbo].[LibranzaBeneficiarios]  WITH CHECK ADD  CONSTRAINT [FK_LibranzaBeneficiarios_BeneficiarioBancos] FOREIGN KEY([IdBeneficiarioBancos])
REFERENCES [dbo].[BeneficiarioBancos] ([Id])
GO
ALTER TABLE [dbo].[LibranzaBeneficiarios] CHECK CONSTRAINT [FK_LibranzaBeneficiarios_BeneficiarioBancos]
GO
ALTER TABLE [dbo].[LibranzaBeneficiarios]  WITH CHECK ADD  CONSTRAINT [FK_LibranzaBeneficiarios_Beneficiarios] FOREIGN KEY([IdBeneficiario])
REFERENCES [dbo].[Beneficiarios] ([Id])
GO
ALTER TABLE [dbo].[LibranzaBeneficiarios] CHECK CONSTRAINT [FK_LibranzaBeneficiarios_Beneficiarios]
GO
ALTER TABLE [dbo].[LibranzaBeneficiarios]  WITH CHECK ADD  CONSTRAINT [FK_LibranzaBeneficiarios_Libranzas] FOREIGN KEY([IdLibranzas])
REFERENCES [dbo].[Libranzas] ([Id])
GO
ALTER TABLE [dbo].[LibranzaBeneficiarios] CHECK CONSTRAINT [FK_LibranzaBeneficiarios_Libranzas]
GO
ALTER TABLE [dbo].[LibranzaCesiones]  WITH CHECK ADD  CONSTRAINT [FK_LibranzaCesiones_Libranzas] FOREIGN KEY([IdLibranza])
REFERENCES [dbo].[Libranzas] ([Id])
GO
ALTER TABLE [dbo].[LibranzaCesiones] CHECK CONSTRAINT [FK_LibranzaCesiones_Libranzas]
GO
ALTER TABLE [dbo].[LibranzaEmbargos]  WITH CHECK ADD  CONSTRAINT [FK_LibranzaEmbargos_Libranzas] FOREIGN KEY([IdLibranza])
REFERENCES [dbo].[Libranzas] ([Id])
GO
ALTER TABLE [dbo].[LibranzaEmbargos] CHECK CONSTRAINT [FK_LibranzaEmbargos_Libranzas]
GO
ALTER TABLE [dbo].[LibranzaFacturas]  WITH CHECK ADD  CONSTRAINT [FK_LibranzaFacturas_Libranzas] FOREIGN KEY([IdLibranza])
REFERENCES [dbo].[Libranzas] ([Id])
GO
ALTER TABLE [dbo].[LibranzaFacturas] CHECK CONSTRAINT [FK_LibranzaFacturas_Libranzas]
GO
ALTER TABLE [dbo].[Libranzas]  WITH CHECK ADD  CONSTRAINT [FK_Libranzas_EstadoLibranzas] FOREIGN KEY([IdLibranzaEstados])
REFERENCES [dbo].[LibranzasEstado] ([Id])
GO
ALTER TABLE [dbo].[Libranzas] CHECK CONSTRAINT [FK_Libranzas_EstadoLibranzas]
GO
ALTER TABLE [dbo].[Libranzas]  WITH CHECK ADD  CONSTRAINT [FK_Libranzas_Proyectos] FOREIGN KEY([IdProyecto])
REFERENCES [dbo].[Proyectos] ([Id])
GO
ALTER TABLE [dbo].[Libranzas] CHECK CONSTRAINT [FK_Libranzas_Proyectos]
GO
ALTER TABLE [dbo].[ProyectoAdjuntos]  WITH CHECK ADD  CONSTRAINT [FK_ProyectoAdjuntos_Adjuntos] FOREIGN KEY([IdAdjunto])
REFERENCES [dbo].[Adjuntos] ([Id])
GO
ALTER TABLE [dbo].[ProyectoAdjuntos] CHECK CONSTRAINT [FK_ProyectoAdjuntos_Adjuntos]
GO
ALTER TABLE [dbo].[ProyectoAdjuntos]  WITH CHECK ADD  CONSTRAINT [FK_ProyectoAdjuntos_Proyectos] FOREIGN KEY([IdProyecto])
REFERENCES [dbo].[Proyectos] ([Id])
GO
ALTER TABLE [dbo].[ProyectoAdjuntos] CHECK CONSTRAINT [FK_ProyectoAdjuntos_Proyectos]
GO
ALTER TABLE [dbo].[ProyectoAeropuertos]  WITH CHECK ADD  CONSTRAINT [FK_ProyectoAeropuertos_Proyectos] FOREIGN KEY([IdProyecto])
REFERENCES [dbo].[Proyectos] ([Id])
GO
ALTER TABLE [dbo].[ProyectoAeropuertos] CHECK CONSTRAINT [FK_ProyectoAeropuertos_Proyectos]
GO
ALTER TABLE [dbo].[ProyectoBeneficiarios]  WITH CHECK ADD  CONSTRAINT [FK_ProyectoProveedores_Proveedores] FOREIGN KEY([IdBeneficiario])
REFERENCES [dbo].[Beneficiarios] ([Id])
GO
ALTER TABLE [dbo].[ProyectoBeneficiarios] CHECK CONSTRAINT [FK_ProyectoProveedores_Proveedores]
GO
ALTER TABLE [dbo].[ProyectoBeneficiarios]  WITH CHECK ADD  CONSTRAINT [FK_ProyectoProveedores_Proyectos] FOREIGN KEY([IdProyecto])
REFERENCES [dbo].[Proyectos] ([Id])
GO
ALTER TABLE [dbo].[ProyectoBeneficiarios] CHECK CONSTRAINT [FK_ProyectoProveedores_Proyectos]
GO
ALTER TABLE [dbo].[ProyectoProvincias]  WITH CHECK ADD  CONSTRAINT [FK_ProyectoProvincias_Provincias] FOREIGN KEY([IdProvincia])
REFERENCES [dbo].[Provincias] ([Id])
GO
ALTER TABLE [dbo].[ProyectoProvincias] CHECK CONSTRAINT [FK_ProyectoProvincias_Provincias]
GO
ALTER TABLE [dbo].[ProyectoProvincias]  WITH CHECK ADD  CONSTRAINT [FK_ProyectoProvincias_Proyectos] FOREIGN KEY([IdProyecto])
REFERENCES [dbo].[Proyectos] ([Id])
GO
ALTER TABLE [dbo].[ProyectoProvincias] CHECK CONSTRAINT [FK_ProyectoProvincias_Proyectos]
GO
ALTER TABLE [dbo].[Proyectos]  WITH CHECK ADD  CONSTRAINT [FK_Proyectos_Areas] FOREIGN KEY([IdArea])
REFERENCES [dbo].[Areas] ([Id])
GO
ALTER TABLE [dbo].[Proyectos] CHECK CONSTRAINT [FK_Proyectos_Areas]
GO
ALTER TABLE [dbo].[Proyectos]  WITH CHECK ADD  CONSTRAINT [FK_Proyectos_Cuentas] FOREIGN KEY([IdCuenta])
REFERENCES [dbo].[Cuentas] ([Id])
GO
ALTER TABLE [dbo].[Proyectos] CHECK CONSTRAINT [FK_Proyectos_Cuentas]
GO
ALTER TABLE [dbo].[Proyectos]  WITH CHECK ADD  CONSTRAINT [FK_Proyectos_ProyectosEstado] FOREIGN KEY([IdProyectosEstado])
REFERENCES [dbo].[ProyectosEstado] ([Id])
GO
ALTER TABLE [dbo].[Proyectos] CHECK CONSTRAINT [FK_Proyectos_ProyectosEstado]
GO
ALTER TABLE [dbo].[UsuariosAreas]  WITH CHECK ADD  CONSTRAINT [FK_UsuariosAreas_Areas] FOREIGN KEY([IdArea])
REFERENCES [dbo].[Areas] ([Id])
GO
ALTER TABLE [dbo].[UsuariosAreas] CHECK CONSTRAINT [FK_UsuariosAreas_Areas]
GO
ALTER TABLE [dbo].[UsuariosAreas]  WITH CHECK ADD  CONSTRAINT [FK_UsuariosAreas_Usuarios] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuarios] ([Id])
GO
ALTER TABLE [dbo].[UsuariosAreas] CHECK CONSTRAINT [FK_UsuariosAreas_Usuarios]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'consultar sobre este campo, porque puede ser reemplazado por la tabla AdjuntoProyectos' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Proyectos', @level2type=N'COLUMN',@level2name=N'Objeto'
GO
USE [master]
GO
ALTER DATABASE [OrsnaDatabase] SET  READ_WRITE 
GO
