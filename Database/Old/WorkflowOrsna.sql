GO
USE [WorkflowOrsna]
GO
/****** Object:  Table [dbo].[estado_dependencia]    Script Date: 9/19/2018 10:59:29 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[estado_dependencia](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_workflow] [int] NULL,
	[id_estado_desde] [int] NULL,
	[id_estado_hasta] [int] NULL,
	[parametrizable] [bit] NULL,
 CONSTRAINT [PK_estado_dependencia] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[estados]    Script Date: 9/19/2018 10:59:29 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[estados](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_workflow] [int] NULL,
	[nombre] [nvarchar](500) NULL,
	[estado_inicial] [bit] NULL,
	[estado_final] [bit] NULL,
 CONSTRAINT [PK_estados] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[parametro_secuencia_estado]    Script Date: 9/19/2018 10:59:29 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[parametro_secuencia_estado](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_parametro] [int] NULL,
	[id_estado_siguiente] [int] NOT NULL,
	[valor_parametro] [nvarchar](50) NULL,
 CONSTRAINT [PK_parametro_secuencia_estado] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[parametros]    Script Date: 9/19/2018 10:59:29 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[parametros](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [nvarchar](500) NULL,
 CONSTRAINT [PK_parametros] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[parametros_estado_dependencia]    Script Date: 9/19/2018 10:59:29 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[parametros_estado_dependencia](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_parametro] [int] NULL,
	[id_estado_dependencia] [int] NULL,
 CONSTRAINT [PK_parametro_estado_dependencia] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[proceso_workflow]    Script Date: 9/19/2018 10:59:29 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[proceso_workflow](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_estado_actual] [int] NULL,
	[id_workflow] [int] NULL,
 CONSTRAINT [PK_proceso_workflow] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tareas_estado]    Script Date: 9/19/2018 10:59:29 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tareas_estado](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[metodo] [nvarchar](500) NULL,
	[id_estado] [int] NULL,
	[parametros] [nvarchar](500) NULL,
	[ruta_dll] [nvarchar](500) NULL,
	[nombre_libreria] [nvarchar](500) NULL,
	[nombre_clase] [nvarchar](500) NULL,
	[es_asincrona] [bit] NULL,
 CONSTRAINT [PK_tareas_estados] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tracks]    Script Date: 9/19/2018 10:59:29 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tracks](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[metodo] [nvarchar](500) NULL,
	[tipo_respuesta] [nvarchar](100) NULL,
	[respuesta] [nvarchar](max) NULL,
	[descripcion] [nvarchar](max) NULL,
	[fecha] [datetime] NULL,
 CONSTRAINT [PK_trackeo] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[workflow]    Script Date: 9/19/2018 10:59:29 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[workflow](
	[id] [int] NOT NULL,
	[nombre] [nvarchar](500) NULL,
	[estado] [bit] NULL,
 CONSTRAINT [PK_workflow] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[tareas_estado] ADD  CONSTRAINT [DF_tareas_estado_es_asincrona]  DEFAULT ((0)) FOR [es_asincrona]
GO
ALTER TABLE [dbo].[estado_dependencia]  WITH CHECK ADD  CONSTRAINT [FK_estado_dependencia_estados] FOREIGN KEY([id_estado_desde])
REFERENCES [dbo].[estados] ([id])
GO
ALTER TABLE [dbo].[estado_dependencia] CHECK CONSTRAINT [FK_estado_dependencia_estados]
GO
ALTER TABLE [dbo].[estado_dependencia]  WITH CHECK ADD  CONSTRAINT [FK_estado_dependencia_estados1] FOREIGN KEY([id_estado_hasta])
REFERENCES [dbo].[estados] ([id])
GO
ALTER TABLE [dbo].[estado_dependencia] CHECK CONSTRAINT [FK_estado_dependencia_estados1]
GO
ALTER TABLE [dbo].[estado_dependencia]  WITH CHECK ADD  CONSTRAINT [FK_estado_dependencia_workflow] FOREIGN KEY([id_workflow])
REFERENCES [dbo].[workflow] ([id])
GO
ALTER TABLE [dbo].[estado_dependencia] CHECK CONSTRAINT [FK_estado_dependencia_workflow]
GO
ALTER TABLE [dbo].[estados]  WITH CHECK ADD  CONSTRAINT [FK_estados_workflow] FOREIGN KEY([id_workflow])
REFERENCES [dbo].[workflow] ([id])
GO
ALTER TABLE [dbo].[estados] CHECK CONSTRAINT [FK_estados_workflow]
GO
ALTER TABLE [dbo].[parametro_secuencia_estado]  WITH CHECK ADD  CONSTRAINT [FK_parametro_secuencia_estado_estados] FOREIGN KEY([id_estado_siguiente])
REFERENCES [dbo].[estados] ([id])
GO
ALTER TABLE [dbo].[parametro_secuencia_estado] CHECK CONSTRAINT [FK_parametro_secuencia_estado_estados]
GO
ALTER TABLE [dbo].[parametro_secuencia_estado]  WITH CHECK ADD  CONSTRAINT [FK_parametro_secuencia_estado_parametros] FOREIGN KEY([id_parametro])
REFERENCES [dbo].[parametros] ([id])
GO
ALTER TABLE [dbo].[parametro_secuencia_estado] CHECK CONSTRAINT [FK_parametro_secuencia_estado_parametros]
GO
ALTER TABLE [dbo].[parametros_estado_dependencia]  WITH CHECK ADD  CONSTRAINT [FK_parametro_estado_dependencia_estado_dependencia] FOREIGN KEY([id_estado_dependencia])
REFERENCES [dbo].[estado_dependencia] ([id])
GO
ALTER TABLE [dbo].[parametros_estado_dependencia] CHECK CONSTRAINT [FK_parametro_estado_dependencia_estado_dependencia]
GO
ALTER TABLE [dbo].[parametros_estado_dependencia]  WITH CHECK ADD  CONSTRAINT [FK_parametro_estado_dependencia_parametros] FOREIGN KEY([id_parametro])
REFERENCES [dbo].[parametros] ([id])
GO
ALTER TABLE [dbo].[parametros_estado_dependencia] CHECK CONSTRAINT [FK_parametro_estado_dependencia_parametros]
GO
ALTER TABLE [dbo].[proceso_workflow]  WITH CHECK ADD  CONSTRAINT [FK_proceso_workflow_estados] FOREIGN KEY([id_estado_actual])
REFERENCES [dbo].[estados] ([id])
GO
ALTER TABLE [dbo].[proceso_workflow] CHECK CONSTRAINT [FK_proceso_workflow_estados]
GO
ALTER TABLE [dbo].[proceso_workflow]  WITH CHECK ADD  CONSTRAINT [FK_proceso_workflow_workflow] FOREIGN KEY([id_workflow])
REFERENCES [dbo].[workflow] ([id])
GO
ALTER TABLE [dbo].[proceso_workflow] CHECK CONSTRAINT [FK_proceso_workflow_workflow]
GO
ALTER TABLE [dbo].[tareas_estado]  WITH CHECK ADD  CONSTRAINT [FK_tareas_estados_estados] FOREIGN KEY([id_estado])
REFERENCES [dbo].[estados] ([id])
GO
ALTER TABLE [dbo].[tareas_estado] CHECK CONSTRAINT [FK_tareas_estados_estados]
GO
USE [master]
GO
ALTER DATABASE [workflowNet] SET  READ_WRITE 
GO
