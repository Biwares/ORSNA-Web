﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace BD.Migrations
{
    public partial class InitialDataToTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql(@"
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
                    INSERT [dbo].[Areas] ([Id], [Nombre], [Codigo], [Estado]) VALUES (31, N'Administración', N'ADM1', 0)
                    SET IDENTITY_INSERT [dbo].[Areas] OFF

                    SET IDENTITY_INSERT [dbo].[AeropuertosGrupo] ON 

                    INSERT [dbo].[AeropuertosGrupo] ([Id], [Nombre], [Estado]) VALUES (1, N'GrupoA', 1)
                    INSERT [dbo].[AeropuertosGrupo] ([Id], [Nombre], [Estado]) VALUES (2, N'GrupoB', 1)
                    INSERT [dbo].[AeropuertosGrupo] ([Id], [Nombre], [Estado]) VALUES (3, N'SNA', 1)
                    SET IDENTITY_INSERT [dbo].[AeropuertosGrupo] OFF

                    SET IDENTITY_INSERT [dbo].[Beneficiarios] ON 

                    INSERT [dbo].[Beneficiarios] ([Id], [RazonSocial], [Descripcion], [Cuit], [FechaAlta], [NacionalExtranjero], [Email], [Estado], [Telefono]) VALUES (8, N'razon social', N'descripcion', N'cuit', CAST(N'2018-10-02T00:00:00.000' AS DateTime), N'Extranjero', N'mail', 1, NULL)
                    INSERT [dbo].[Beneficiarios] ([Id], [RazonSocial], [Descripcion], [Cuit], [FechaAlta], [NacionalExtranjero], [Email], [Estado], [Telefono]) VALUES (9, N'tes', N'test', N'123123123', CAST(N'2018-10-29T00:00:00.000' AS DateTime), N'Nacional  ', N'test@test.com', 1, NULL)
                    INSERT [dbo].[Beneficiarios] ([Id], [RazonSocial], [Descripcion], [Cuit], [FechaAlta], [NacionalExtranjero], [Email], [Estado], [Telefono]) VALUES (10, N'Razon Test', N'Descripcion test', N'20123123', CAST(N'2018-10-30T00:00:00.000' AS DateTime), N'Nacional  ', N'test@test.com', 1, NULL)
                    INSERT [dbo].[Beneficiarios] ([Id], [RazonSocial], [Descripcion], [Cuit], [FechaAlta], [NacionalExtranjero], [Email], [Estado], [Telefono]) VALUES (11, N'Prueba Marney', N'Prueba Marney', N'Prueba Marney', CAST(N'2018-10-03T00:00:00.000' AS DateTime), N'Nacional  ', N'Prueba Marney', 0, NULL)
                    INSERT [dbo].[Beneficiarios] ([Id], [RazonSocial], [Descripcion], [Cuit], [FechaAlta], [NacionalExtranjero], [Email], [Estado], [Telefono]) VALUES (12, N'1', N'1', N'1', CAST(N'2018-11-01T00:00:00.000' AS DateTime), N'Nacional  ', N'a@a.com', 1, NULL)
                    INSERT [dbo].[Beneficiarios] ([Id], [RazonSocial], [Descripcion], [Cuit], [FechaAlta], [NacionalExtranjero], [Email], [Estado], [Telefono]) VALUES (13, N'test', N'test', N'1313131313', CAST(N'2018-11-14T00:00:00.000' AS DateTime), N'Nacional  ', N'test@test.com', 1, NULL)
                    INSERT [dbo].[Beneficiarios] ([Id], [RazonSocial], [Descripcion], [Cuit], [FechaAlta], [NacionalExtranjero], [Email], [Estado], [Telefono]) VALUES (14, N'Benefic 01', N'Benefic 01', N'123456799', CAST(N'2018-11-14T00:00:00.000' AS DateTime), N'Extranjero', N'glago@w3itsolutions.net', 1, NULL)
                    INSERT [dbo].[Beneficiarios] ([Id], [RazonSocial], [Descripcion], [Cuit], [FechaAlta], [NacionalExtranjero], [Email], [Estado], [Telefono]) VALUES (15, N'El Aeroport', N'El Aeroport', N'123456789', CAST(N'2018-11-14T00:00:00.000' AS DateTime), N'Nacional  ', N'aeroport@gmail.com', 1, NULL)
                    INSERT [dbo].[Beneficiarios] ([Id], [RazonSocial], [Descripcion], [Cuit], [FechaAlta], [NacionalExtranjero], [Email], [Estado], [Telefono]) VALUES (16, N'asd', N'asd', N'123123', CAST(N'2018-11-16T00:00:00.000' AS DateTime), N'Nacional  ', N'test@test.com', 0, NULL)
                    INSERT [dbo].[Beneficiarios] ([Id], [RazonSocial], [Descripcion], [Cuit], [FechaAlta], [NacionalExtranjero], [Email], [Estado], [Telefono]) VALUES (17, N'2345', N'ghsdf', N'sdfgh', CAST(N'2018-11-06T00:00:00.000' AS DateTime), N'Extranjero', N'3456', 0, NULL)
                    INSERT [dbo].[Beneficiarios] ([Id], [RazonSocial], [Descripcion], [Cuit], [FechaAlta], [NacionalExtranjero], [Email], [Estado], [Telefono]) VALUES (18, N'Extranjero 1', N'Extranjero 1', N'1234567', CAST(N'2018-11-20T00:00:00.000' AS DateTime), N'Extranjero', N'extranjero@gmail.com', 0, NULL)
                    INSERT [dbo].[Beneficiarios] ([Id], [RazonSocial], [Descripcion], [Cuit], [FechaAlta], [NacionalExtranjero], [Email], [Estado], [Telefono]) VALUES (19, N'Repuestos Ezeiza', N'Repuestos Ezeiza', N'27-12345678-7', CAST(N'2018-11-29T00:00:00.000' AS DateTime), N'true      ', N'repuestos@gmail.com', 1, N'1137659876')
                    INSERT [dbo].[Beneficiarios] ([Id], [RazonSocial], [Descripcion], [Cuit], [FechaAlta], [NacionalExtranjero], [Email], [Estado], [Telefono]) VALUES (20, N'CONSTRUCCIONES ELECTROMECANICAS DEL OESTE S.A.', N'CONSTRUCCIONES ELECTROMECANICAS DEL OESTE S.A.', N'30-61199512-8', CAST(N'2018-12-03T00:00:00.000' AS DateTime), N'false     ', N'construcciones@gmail.com', 1, N'1137659876')
                    INSERT [dbo].[Beneficiarios] ([Id], [RazonSocial], [Descripcion], [Cuit], [FechaAlta], [NacionalExtranjero], [Email], [Estado], [Telefono]) VALUES (21, N'Repuestos Ezeiza', N'', N'27-12345678-9', CAST(N'2018-12-05T00:00:00.000' AS DateTime), N'false     ', N'', 0, N'')
                    INSERT [dbo].[Beneficiarios] ([Id], [RazonSocial], [Descripcion], [Cuit], [FechaAlta], [NacionalExtranjero], [Email], [Estado], [Telefono]) VALUES (22, N'Repuestos Ezeiza', N'CONSTRUCCIONES ELECTROMECANICAS DEL OESTE S.A.', N'30-61199512-9', CAST(N'2018-12-05T00:00:00.000' AS DateTime), N'true      ', N'', 0, N'')
                    INSERT [dbo].[Beneficiarios] ([Id], [RazonSocial], [Descripcion], [Cuit], [FechaAlta], [NacionalExtranjero], [Email], [Estado], [Telefono]) VALUES (23, N'Repuestos Ezeiza', N'Repuestos Ezeiza', N'30-61199512-0', CAST(N'2018-12-06T00:00:00.000' AS DateTime), N'false     ', N'construcciones@gmail.com', 1, N'1137659876')
                    INSERT [dbo].[Beneficiarios] ([Id], [RazonSocial], [Descripcion], [Cuit], [FechaAlta], [NacionalExtranjero], [Email], [Estado], [Telefono]) VALUES (24, N'Repuestos Ezeiza', N'CTAAero', N'30-61199512-1', CAST(N'2018-12-06T00:00:00.000' AS DateTime), N'false     ', N'construcciones@gmail.com', 1, N'1137659876')
                    INSERT [dbo].[Beneficiarios] ([Id], [RazonSocial], [Descripcion], [Cuit], [FechaAlta], [NacionalExtranjero], [Email], [Estado], [Telefono]) VALUES (25, N'Repuestos Ezeiza', N'111', N'27-12345678-4', CAST(N'2018-12-06T00:00:00.000' AS DateTime), N'false     ', N'111', 1, N'111')
                    INSERT [dbo].[Beneficiarios] ([Id], [RazonSocial], [Descripcion], [Cuit], [FechaAlta], [NacionalExtranjero], [Email], [Estado], [Telefono]) VALUES (26, N'Repuestos Ezeiza', N'CTAAero', N'igiu887867676789', CAST(N'2018-12-07T00:00:00.000' AS DateTime), N'false     ', N'construcciones@gmail.com', 1, N'1137659876')
                    SET IDENTITY_INSERT [dbo].[Beneficiarios] OFF

                    SET IDENTITY_INSERT [dbo].[LibranzasEstado] ON 

                    INSERT [dbo].[LibranzasEstado] ([Id], [Nombre], [Estado]) VALUES (1, N'En elaboración', 1)
                    INSERT [dbo].[LibranzasEstado] ([Id], [Nombre], [Estado]) VALUES (2, N'Emitida (GAP)', 1)
                    INSERT [dbo].[LibranzasEstado] ([Id], [Nombre], [Estado]) VALUES (3, N'Pendiente de firma USG / DIR', 1)
                    INSERT [dbo].[LibranzasEstado] ([Id], [Nombre], [Estado]) VALUES (4, N'En el Banco', 1)
                    INSERT [dbo].[LibranzasEstado] ([Id], [Nombre], [Estado]) VALUES (5, N'En el Ministerio', 1)
                    INSERT [dbo].[LibranzasEstado] ([Id], [Nombre], [Estado]) VALUES (6, N'Rechazada por el ministerio', 1)
                    INSERT [dbo].[LibranzasEstado] ([Id], [Nombre], [Estado]) VALUES (7, N'Pagada', 1)
                    INSERT [dbo].[LibranzasEstado] ([Id], [Nombre], [Estado]) VALUES (8, N'Anulada', 1)
                    SET IDENTITY_INSERT [dbo].[LibranzasEstado] OFF

                    SET IDENTITY_INSERT [dbo].[LibranzaTipo] ON 

                    INSERT [dbo].[LibranzaTipo] ([Id], [Nombre], [Estado]) VALUES (1, N'Tipo A', 1)
                    INSERT [dbo].[LibranzaTipo] ([Id], [Nombre], [Estado]) VALUES (2, N'Tipo B', 1)
                    SET IDENTITY_INSERT [dbo].[LibranzaTipo] OFF

                    SET IDENTITY_INSERT [dbo].[ProyectosEstado] ON 

                    INSERT [dbo].[ProyectosEstado] ([Id], [Nombre], [Estado]) VALUES (1, N'EN EJECUCIÓN', 1)
                    INSERT [dbo].[ProyectosEstado] ([Id], [Nombre], [Estado]) VALUES (2, N'EJECUTADO 100%', 1)
                    INSERT [dbo].[ProyectosEstado] ([Id], [Nombre], [Estado]) VALUES (3, N'FINALIZADO', 1)
                    SET IDENTITY_INSERT [dbo].[ProyectosEstado] OFF

                    SET IDENTITY_INSERT [dbo].[Provincias] ON 

                    INSERT [dbo].[Provincias] ([Id], [Nombre], [Estado]) VALUES (1, N'Buenos Aires', 1)
                    SET IDENTITY_INSERT [dbo].[Provincias] OFF

                    SET IDENTITY_INSERT [dbo].[Usuarios] ON 

                    INSERT [dbo].[Usuarios] ([Id], [email], [password], [Estado]) VALUES (1, N'root@root.com', N'123', 1)
                    SET IDENTITY_INSERT [dbo].[Usuarios] OFF


                    SET IDENTITY_INSERT [dbo].[Cuentas] ON 

                    INSERT [dbo].[Cuentas] ([Id], [NroCuenta], [Nombre], [Descripcion], [FechaVigencia], [FechaCreacion], [IdLibranzaTipo], [IdAeropuertosGrupo], [Estado]) VALUES (23, N'12123123123212321232', N'test', N'test', CAST(N'2018-10-29T00:00:00.000' AS DateTime), CAST(N'2018-10-29T18:35:36.967' AS DateTime), 2, 2, 0)
                    INSERT [dbo].[Cuentas] ([Id], [NroCuenta], [Nombre], [Descripcion], [FechaVigencia], [FechaCreacion], [IdLibranzaTipo], [IdAeropuertosGrupo], [Estado]) VALUES (24, N'1', N'1', N'1', CAST(N'2018-10-31T00:00:00.000' AS DateTime), CAST(N'2018-10-30T15:22:48.577' AS DateTime), NULL, NULL, 0)
                    INSERT [dbo].[Cuentas] ([Id], [NroCuenta], [Nombre], [Descripcion], [FechaVigencia], [FechaCreacion], [IdLibranzaTipo], [IdAeropuertosGrupo], [Estado]) VALUES (25, N'1234', N'name', N'name2', CAST(N'2018-11-06T00:00:00.000' AS DateTime), CAST(N'2018-11-05T17:29:58.437' AS DateTime), NULL, NULL, 0)
                    INSERT [dbo].[Cuentas] ([Id], [NroCuenta], [Nombre], [Descripcion], [FechaVigencia], [FechaCreacion], [IdLibranzaTipo], [IdAeropuertosGrupo], [Estado]) VALUES (26, N'nnkjnkjn', N'klkklklkkkkl', N'kij', CAST(N'2018-11-07T00:00:00.000' AS DateTime), CAST(N'2018-11-13T15:57:42.743' AS DateTime), NULL, NULL, 0)
                    INSERT [dbo].[Cuentas] ([Id], [NroCuenta], [Nombre], [Descripcion], [FechaVigencia], [FechaCreacion], [IdLibranzaTipo], [IdAeropuertosGrupo], [Estado]) VALUES (27, N'99999999999999999999999999999999999999999999999999999999999999', N'cuenta nueve', N'nueve', CAST(N'2018-11-13T00:00:00.000' AS DateTime), CAST(N'2018-11-13T16:02:23.220' AS DateTime), NULL, NULL, 0)
                    INSERT [dbo].[Cuentas] ([Id], [NroCuenta], [Nombre], [Descripcion], [FechaVigencia], [FechaCreacion], [IdLibranzaTipo], [IdAeropuertosGrupo], [Estado]) VALUES (28, N'2134567890-', N'sdfghjk', N'sdfghjk', CAST(N'2018-11-08T00:00:00.000' AS DateTime), CAST(N'2018-11-16T21:07:56.197' AS DateTime), NULL, NULL, 0)
                    INSERT [dbo].[Cuentas] ([Id], [NroCuenta], [Nombre], [Descripcion], [FechaVigencia], [FechaCreacion], [IdLibranzaTipo], [IdAeropuertosGrupo], [Estado]) VALUES (29, N'12323123212321232123', N'Cuenta01', N'CTA01', CAST(N'2018-11-30T00:00:00.000' AS DateTime), CAST(N'2018-11-20T09:54:00.297' AS DateTime), 2, 2, 0)
                    INSERT [dbo].[Cuentas] ([Id], [NroCuenta], [Nombre], [Descripcion], [FechaVigencia], [FechaCreacion], [IdLibranzaTipo], [IdAeropuertosGrupo], [Estado]) VALUES (30, N'32453456734567435674', N'nombre cuenta', N'descripcion', CAST(N'2018-11-02T00:00:00.000' AS DateTime), CAST(N'2018-11-20T20:08:45.083' AS DateTime), 1, 1, 0)
                    INSERT [dbo].[Cuentas] ([Id], [NroCuenta], [Nombre], [Descripcion], [FechaVigencia], [FechaCreacion], [IdLibranzaTipo], [IdAeropuertosGrupo], [Estado]) VALUES (31, N'41246490000000000000', N'PATRIMONIO DE AFECTACION PARA EL FINANCIAMIENTO DE OBRAS DEL SISTEMA NACIONAL DE AEROPUERTOS.', N'PAT SNAtt', CAST(N'2018-12-03T00:00:00.000' AS DateTime), CAST(N'2018-12-03T14:49:23.083' AS DateTime), 1, 1, 0)
                    INSERT [dbo].[Cuentas] ([Id], [NroCuenta], [Nombre], [Descripcion], [FechaVigencia], [FechaCreacion], [IdLibranzaTipo], [IdAeropuertosGrupo], [Estado]) VALUES (32, N'12345678901234567892', N'Cuenta Aeropuerto Ezeiza', N'CTAAero', CAST(N'2018-12-25T00:00:00.000' AS DateTime), CAST(N'2018-12-05T12:28:28.220' AS DateTime), 1, 1, 0)
                    INSERT [dbo].[Cuentas] ([Id], [NroCuenta], [Nombre], [Descripcion], [FechaVigencia], [FechaCreacion], [IdLibranzaTipo], [IdAeropuertosGrupo], [Estado]) VALUES (33, N'12345678901234567823', N'Administración', N'CTAAero', NULL, CAST(N'2018-12-06T10:36:37.010' AS DateTime), 1, 1, 1)
                    INSERT [dbo].[Cuentas] ([Id], [NroCuenta], [Nombre], [Descripcion], [FechaVigencia], [FechaCreacion], [IdLibranzaTipo], [IdAeropuertosGrupo], [Estado]) VALUES (34, N'44444', N'4444444', N'444444', CAST(N'2018-12-17T00:00:00.000' AS DateTime), CAST(N'2018-12-06T15:44:49.760' AS DateTime), 1, 2, 0)
                    INSERT [dbo].[Cuentas] ([Id], [NroCuenta], [Nombre], [Descripcion], [FechaVigencia], [FechaCreacion], [IdLibranzaTipo], [IdAeropuertosGrupo], [Estado]) VALUES (35, N'AFGRTU45/7', N'Cuenta B', N'CTAB', CAST(N'2018-12-24T00:00:00.000' AS DateTime), CAST(N'2018-12-10T09:57:39.930' AS DateTime), 2, 2, 1)
                    INSERT [dbo].[Cuentas] ([Id], [NroCuenta], [Nombre], [Descripcion], [FechaVigencia], [FechaCreacion], [IdLibranzaTipo], [IdAeropuertosGrupo], [Estado]) VALUES (36, N'Cuenta A', N'Cuenta A', N'CTAA', CAST(N'2018-12-25T00:00:00.000' AS DateTime), CAST(N'2018-12-10T15:56:18.707' AS DateTime), 1, 1, 1)
                    SET IDENTITY_INSERT [dbo].[Cuentas] OFF


                    SET IDENTITY_INSERT [dbo].[Proyectos] ON 

                    INSERT [dbo].[Proyectos] ([Id], [Nombre], [Descripcion], [IdCuenta], [IdProyecto], [IdArea], [NroContratacion], [NroObra], [CodObra], [NroPago], [NormaLegal], [Objeto], [MontoTotal], [FechaCreacion], [TipoEstado], [Estado], [IdProyectosEstado]) VALUES (56, N'sdfgh', N'gdsfghj', 25, N'123456', 21, N'23456', N'sadfgh', N'sdfgh', N'dsfghj', N'sdfgh', N'dsfghsadfghjkhgfdsadfghjk', CAST(0.00 AS Decimal(18, 2)), CAST(N'2018-11-16T00:00:00.000' AS DateTime), NULL, 0, 3)
                    INSERT [dbo].[Proyectos] ([Id], [Nombre], [Descripcion], [IdCuenta], [IdProyecto], [IdArea], [NroContratacion], [NroObra], [CodObra], [NroPago], [NormaLegal], [Objeto], [MontoTotal], [FechaCreacion], [TipoEstado], [Estado], [IdProyectosEstado]) VALUES (57, N'sadfgh', N'dsfgh', 25, N'3456', 21, N'34567', N'sdfgh', N'dsfgh', N'dsfghj', N'dsfgh', N'sdfg', CAST(0.00 AS Decimal(18, 2)), CAST(N'2018-11-16T00:00:00.000' AS DateTime), NULL, 0, 1)
                    INSERT [dbo].[Proyectos] ([Id], [Nombre], [Descripcion], [IdCuenta], [IdProyecto], [IdArea], [NroContratacion], [NroObra], [CodObra], [NroPago], [NormaLegal], [Objeto], [MontoTotal], [FechaCreacion], [TipoEstado], [Estado], [IdProyectosEstado]) VALUES (58, N'dsfghj', N'dsfghj', 25, N'456789', 22, N'4567', N'34567', N'sdfghj', N'dfghb', N'xcvb', N'objeto', CAST(0.00 AS Decimal(18, 2)), CAST(N'2018-11-16T00:00:00.000' AS DateTime), NULL, 1, 3)
                    INSERT [dbo].[Proyectos] ([Id], [Nombre], [Descripcion], [IdCuenta], [IdProyecto], [IdArea], [NroContratacion], [NroObra], [CodObra], [NroPago], [NormaLegal], [Objeto], [MontoTotal], [FechaCreacion], [TipoEstado], [Estado], [IdProyectosEstado]) VALUES (59, N'sdfgh', N'fgh', 24, N'34567', 22, N'34567', N'dsfgh', N'dfgh', N'dsfgh', N'dfghj', N'dfghj', CAST(0.00 AS Decimal(18, 2)), CAST(N'2018-11-18T00:00:00.000' AS DateTime), NULL, 1, 2)
                    INSERT [dbo].[Proyectos] ([Id], [Nombre], [Descripcion], [IdCuenta], [IdProyecto], [IdArea], [NroContratacion], [NroObra], [CodObra], [NroPago], [NormaLegal], [Objeto], [MontoTotal], [FechaCreacion], [TipoEstado], [Estado], [IdProyectosEstado]) VALUES (60, N'Pista Aterrizaje', N'Pista CABA', 23, N'001', 21, N'300', N'OBRA0102', N'CABA', N'PAGO01', N'NORMA01', N'Pista CABA', CAST(0.00 AS Decimal(18, 2)), CAST(N'2018-11-20T00:00:00.000' AS DateTime), NULL, 1, 1)
                    INSERT [dbo].[Proyectos] ([Id], [Nombre], [Descripcion], [IdCuenta], [IdProyecto], [IdArea], [NroContratacion], [NroObra], [CodObra], [NroPago], [NormaLegal], [Objeto], [MontoTotal], [FechaCreacion], [TipoEstado], [Estado], [IdProyectosEstado]) VALUES (61, N'Pista Ezeiza', N'1111', 23, N'002', 21, N'1212', N'1212', N'CABA Ezeiza', N'1212', N'1212', N'1212', CAST(0.00 AS Decimal(18, 2)), CAST(N'2018-11-20T00:00:00.000' AS DateTime), NULL, 1, 1)
                    INSERT [dbo].[Proyectos] ([Id], [Nombre], [Descripcion], [IdCuenta], [IdProyecto], [IdArea], [NroContratacion], [NroObra], [CodObra], [NroPago], [NormaLegal], [Objeto], [MontoTotal], [FechaCreacion], [TipoEstado], [Estado], [IdProyectosEstado]) VALUES (62, N'Ezeiza', N'Ezeiza', 29, N'200', 27, N'0101', N'0101', N'Ezeiza', N'0101', N'0101', N'0101', CAST(0.00 AS Decimal(18, 2)), CAST(N'2018-11-20T00:00:00.000' AS DateTime), NULL, 1, 1)
                    INSERT [dbo].[Proyectos] ([Id], [Nombre], [Descripcion], [IdCuenta], [IdProyecto], [IdArea], [NroContratacion], [NroObra], [CodObra], [NroPago], [NormaLegal], [Objeto], [MontoTotal], [FechaCreacion], [TipoEstado], [Estado], [IdProyectosEstado]) VALUES (63, N'Ampliacion de pistas', N'pista A', 25, N'353433', 23, N'4545', N'7654', N'34543', N'3456', N'345', N'345', CAST(0.00 AS Decimal(18, 2)), CAST(N'2018-11-20T00:00:00.000' AS DateTime), NULL, 1, 1)
                    INSERT [dbo].[Proyectos] ([Id], [Nombre], [Descripcion], [IdCuenta], [IdProyecto], [IdArea], [NroContratacion], [NroObra], [CodObra], [NroPago], [NormaLegal], [Objeto], [MontoTotal], [FechaCreacion], [TipoEstado], [Estado], [IdProyectosEstado]) VALUES (64, N'Ampliacion de embarques', N'salas de embarque', 25, N'33232', 23, N'65345', N'45678', N'6454345', N'34567', N'3456', N'mejoras del aeropuerto', CAST(0.00 AS Decimal(18, 2)), CAST(N'2018-11-20T00:00:00.000' AS DateTime), NULL, 0, 2)
                    INSERT [dbo].[Proyectos] ([Id], [Nombre], [Descripcion], [IdCuenta], [IdProyecto], [IdArea], [NroContratacion], [NroObra], [CodObra], [NroPago], [NormaLegal], [Objeto], [MontoTotal], [FechaCreacion], [TipoEstado], [Estado], [IdProyectosEstado]) VALUES (65, N'sdfg', N'asdfgh', 26, N'345', 23, N'', N'', N'', N'', N'', N'', CAST(0.00 AS Decimal(18, 2)), CAST(N'2018-11-20T00:00:00.000' AS DateTime), NULL, 1, 2)
                    INSERT [dbo].[Proyectos] ([Id], [Nombre], [Descripcion], [IdCuenta], [IdProyecto], [IdArea], [NroContratacion], [NroObra], [CodObra], [NroPago], [NormaLegal], [Objeto], [MontoTotal], [FechaCreacion], [TipoEstado], [Estado], [IdProyectosEstado]) VALUES (66, N'Proyecto Aeroparque', N'Aeroparque', 29, N'PRO01', 27, N'10000', N'10000', N'AEROPARQ', N'10000', N'10000', N'10000', CAST(100000.00 AS Decimal(18, 2)), CAST(N'2018-11-29T00:00:00.000' AS DateTime), NULL, 1, 1)
                    INSERT [dbo].[Proyectos] ([Id], [Nombre], [Descripcion], [IdCuenta], [IdProyecto], [IdArea], [NroContratacion], [NroObra], [CodObra], [NroPago], [NormaLegal], [Objeto], [MontoTotal], [FechaCreacion], [TipoEstado], [Estado], [IdProyectosEstado]) VALUES (67, N'Mejora Luces', N'Mejora Luces', 29, N'3344', 21, N'3344', N'3344', N'CABA', N'3344', N'3344', N'3344', CAST(3000000.00 AS Decimal(18, 2)), CAST(N'2018-12-03T00:00:00.000' AS DateTime), NULL, 0, 1)
                    INSERT [dbo].[Proyectos] ([Id], [Nombre], [Descripcion], [IdCuenta], [IdProyecto], [IdArea], [NroContratacion], [NroObra], [CodObra], [NroPago], [NormaLegal], [Objeto], [MontoTotal], [FechaCreacion], [TipoEstado], [Estado], [IdProyectosEstado]) VALUES (68, N'PROYECTO01', N'PROYECTO01', 31, N'PROYECTO01', 25, N'00597/2018 - (ORSNA)', N'SLA 3910', N'CABA', N'EX-2018-13179155-APN-USG#ORSNA', N'Orden de Compra Nº 4700349560 - (AA2000 S.A.)', N'Certificado de Obra Nº 2 - 22,15% de Avance de Obra. Porcentaje de obra ejecutado a la fecha del presente certificado: 26,18%. Orden de Compra Nº 4700349560 - (AA2000 S.A.)', CAST(60000000.00 AS Decimal(18, 2)), CAST(N'2018-12-03T00:00:00.000' AS DateTime), NULL, 1, 1)
                    INSERT [dbo].[Proyectos] ([Id], [Nombre], [Descripcion], [IdCuenta], [IdProyecto], [IdArea], [NroContratacion], [NroObra], [CodObra], [NroPago], [NormaLegal], [Objeto], [MontoTotal], [FechaCreacion], [TipoEstado], [Estado], [IdProyectosEstado]) VALUES (69, N'PROYECTO02', N'PROYECTO02', 29, N'PROYECTO02', 21, N'00597/2018 - (ORSNA)', N'SLA 3910', N'CABA', N'EX-2018-13179155-APN-USG#ORSNA', N'Orden de Compra Nº 4700349560 - (AA2000 S.A.)', N'', CAST(60000100.00 AS Decimal(18, 2)), CAST(N'2018-12-05T00:00:00.000' AS DateTime), NULL, 1, 1)
                    INSERT [dbo].[Proyectos] ([Id], [Nombre], [Descripcion], [IdCuenta], [IdProyecto], [IdArea], [NroContratacion], [NroObra], [CodObra], [NroPago], [NormaLegal], [Objeto], [MontoTotal], [FechaCreacion], [TipoEstado], [Estado], [IdProyectosEstado]) VALUES (70, N'PROYECTO02', N'PROYECTO02', 33, N'PROYECTO03', 22, N'00597/2018 - (ORSNA)', N'SLA 3910', N'SALTA', N'Nro expediente Pago', N'Norma Legal', N'', CAST(92.00 AS Decimal(18, 2)), CAST(N'2018-12-06T00:00:00.000' AS DateTime), NULL, 1, 3)
                    INSERT [dbo].[Proyectos] ([Id], [Nombre], [Descripcion], [IdCuenta], [IdProyecto], [IdArea], [NroContratacion], [NroObra], [CodObra], [NroPago], [NormaLegal], [Objeto], [MontoTotal], [FechaCreacion], [TipoEstado], [Estado], [IdProyectosEstado]) VALUES (71, N'5566', N'444', 33, N'2345', 24, N'1111', N'2222', N'4444', N'33333', N'33333', N'44444', CAST(300000000.00 AS Decimal(18, 2)), CAST(N'2018-12-06T00:00:00.000' AS DateTime), NULL, 1, 2)
                    INSERT [dbo].[Proyectos] ([Id], [Nombre], [Descripcion], [IdCuenta], [IdProyecto], [IdArea], [NroContratacion], [NroObra], [CodObra], [NroPago], [NormaLegal], [Objeto], [MontoTotal], [FechaCreacion], [TipoEstado], [Estado], [IdProyectosEstado]) VALUES (72, N'PROYECTO_CTA_B', N'PROYECTO_CTA_B', 35, N'PROYECTO_CTA_B', 21, N'00597/2018 - (ORSNA)', N'SLA 3910', N'CABA', N'EX-2018-13179155-APN-USG#ORSNA', N'Orden de Compra Nº 4700349560 - (AA2000 S.A.)', N'', CAST(1000000.00 AS Decimal(18, 2)), CAST(N'2018-12-10T00:00:00.000' AS DateTime), NULL, 1, 1)
                    INSERT [dbo].[Proyectos] ([Id], [Nombre], [Descripcion], [IdCuenta], [IdProyecto], [IdArea], [NroContratacion], [NroObra], [CodObra], [NroPago], [NormaLegal], [Objeto], [MontoTotal], [FechaCreacion], [TipoEstado], [Estado], [IdProyectosEstado]) VALUES (73, N'PROYECTO_CTA_A', N'PROYECTO_CTA_A', 36, N'PROYECTO_CTA_A', 22, N'00597/2018 - (ORSNA)', N'SLA 3910', N'CABA', N'EX-2018-13179155-APN-USG#ORSNA', N'Norma Legal', N'', CAST(2000000.00 AS Decimal(18, 2)), CAST(N'2018-12-10T00:00:00.000' AS DateTime), NULL, 1, 1)
                    SET IDENTITY_INSERT [dbo].[Proyectos] OFF




                    SET IDENTITY_INSERT [dbo].[Libranzas] ON 

                    INSERT [dbo].[Libranzas] ([Id], [IdLibranzaTipo], [NroLibranza], [IdProvincia], [IdProyecto], [IdLibranzaEstados], [MontoFondoReparo], [Multa], [Mora], [MontoRestante], [Fecha], [Estado], [Sustituido], [NroEmbargo], [ResponsableEmbargo], [MontoEmbargo], [SaldoEmbargo], [RegistraCesion], [NroEscritura], [Beneficiario], [IdentificacionTributaria]) VALUES (3, NULL, N'001/2018', 1, 66, 1, CAST(1000.00 AS Decimal(18, 2)), CAST(1000.00 AS Decimal(18, 2)), CAST(1000.00 AS Decimal(18, 2)), NULL, CAST(N'2018-11-30T00:00:00.000' AS DateTime), 1, 0, N'1000', N'1000', CAST(1000.00 AS Decimal(18, 2)), CAST(1000.00 AS Decimal(18, 2)), 0, N'100', N'10000', N'10000')
                    INSERT [dbo].[Libranzas] ([Id], [IdLibranzaTipo], [NroLibranza], [IdProvincia], [IdProyecto], [IdLibranzaEstados], [MontoFondoReparo], [Multa], [Mora], [MontoRestante], [Fecha], [Estado], [Sustituido], [NroEmbargo], [ResponsableEmbargo], [MontoEmbargo], [SaldoEmbargo], [RegistraCesion], [NroEscritura], [Beneficiario], [IdentificacionTributaria]) VALUES (4, NULL, N'002/2018', 1, 67, 1, CAST(2000.00 AS Decimal(18, 2)), CAST(2000.00 AS Decimal(18, 2)), CAST(2000.00 AS Decimal(18, 2)), NULL, CAST(N'2018-12-03T00:00:00.000' AS DateTime), 1, 0, N'2', N'Luis', CAST(2000.00 AS Decimal(18, 2)), CAST(2000.00 AS Decimal(18, 2)), 0, N'2000', N'Luis', N'Luis')
                    INSERT [dbo].[Libranzas] ([Id], [IdLibranzaTipo], [NroLibranza], [IdProvincia], [IdProyecto], [IdLibranzaEstados], [MontoFondoReparo], [Multa], [Mora], [MontoRestante], [Fecha], [Estado], [Sustituido], [NroEmbargo], [ResponsableEmbargo], [MontoEmbargo], [SaldoEmbargo], [RegistraCesion], [NroEscritura], [Beneficiario], [IdentificacionTributaria]) VALUES (5, NULL, N'003/2018', 1, 68, 1, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, CAST(N'2018-12-03T00:00:00.000' AS DateTime), 1, 0, N'', N'', NULL, NULL, NULL, N'', N'', N'')
                    INSERT [dbo].[Libranzas] ([Id], [IdLibranzaTipo], [NroLibranza], [IdProvincia], [IdProyecto], [IdLibranzaEstados], [MontoFondoReparo], [Multa], [Mora], [MontoRestante], [Fecha], [Estado], [Sustituido], [NroEmbargo], [ResponsableEmbargo], [MontoEmbargo], [SaldoEmbargo], [RegistraCesion], [NroEscritura], [Beneficiario], [IdentificacionTributaria]) VALUES (6, 2, N'004/2018', 1, 72, 1, CAST(100000.00 AS Decimal(18, 2)), CAST(100000.00 AS Decimal(18, 2)), CAST(100000.00 AS Decimal(18, 2)), NULL, CAST(N'2018-12-10T00:00:00.000' AS DateTime), 1, 0, N'123', N'123', CAST(100000.00 AS Decimal(18, 2)), CAST(500000.00 AS Decimal(18, 2)), 1, N'123', N'123', N'123')
                    INSERT [dbo].[Libranzas] ([Id], [IdLibranzaTipo], [NroLibranza], [IdProvincia], [IdProyecto], [IdLibranzaEstados], [MontoFondoReparo], [Multa], [Mora], [MontoRestante], [Fecha], [Estado], [Sustituido], [NroEmbargo], [ResponsableEmbargo], [MontoEmbargo], [SaldoEmbargo], [RegistraCesion], [NroEscritura], [Beneficiario], [IdentificacionTributaria]) VALUES (7, 2, N'005/2018', 1, 66, 1, NULL, NULL, NULL, NULL, CAST(N'2018-12-10T00:00:00.000' AS DateTime), 1, 0, N'', N'', NULL, NULL, NULL, N'', N'', N'')
                    INSERT [dbo].[Libranzas] ([Id], [IdLibranzaTipo], [NroLibranza], [IdProvincia], [IdProyecto], [IdLibranzaEstados], [MontoFondoReparo], [Multa], [Mora], [MontoRestante], [Fecha], [Estado], [Sustituido], [NroEmbargo], [ResponsableEmbargo], [MontoEmbargo], [SaldoEmbargo], [RegistraCesion], [NroEscritura], [Beneficiario], [IdentificacionTributaria]) VALUES (8, 1, N'006/2018', 1, 73, 1, NULL, CAST(350000.00 AS Decimal(18, 2)), CAST(100000.00 AS Decimal(18, 2)), NULL, CAST(N'2018-12-10T00:00:00.000' AS DateTime), 1, 1, N'3345621', N'ana', CAST(10000.00 AS Decimal(18, 2)), CAST(50000.00 AS Decimal(18, 2)), 0, N'1233', N'1233', N'1233')
                    INSERT [dbo].[Libranzas] ([Id], [IdLibranzaTipo], [NroLibranza], [IdProvincia], [IdProyecto], [IdLibranzaEstados], [MontoFondoReparo], [Multa], [Mora], [MontoRestante], [Fecha], [Estado], [Sustituido], [NroEmbargo], [ResponsableEmbargo], [MontoEmbargo], [SaldoEmbargo], [RegistraCesion], [NroEscritura], [Beneficiario], [IdentificacionTributaria]) VALUES (9, 2, N'007/2018', 1, 60, 1, CAST(300.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)), CAST(200.00 AS Decimal(18, 2)), NULL, CAST(N'2018-12-12T00:00:00.000' AS DateTime), 1, 0, N'', N'', NULL, NULL, NULL, N'', N'', N'')
                    SET IDENTITY_INSERT [dbo].[Libranzas] OFF

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
                    INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (79, N'Proyecto', CAST(N'2018-11-29T14:49:51.857' AS DateTime), N'aeroparquepista.jpg', 1, N'2')
                    INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (80, N'Proyecto', CAST(N'2018-12-03T11:15:54.540' AS DateTime), N'pista.jpg', 1, N'2')
                    INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (81, N'Proyecto', CAST(N'2018-12-03T11:16:34.520' AS DateTime), N'aeroparquepista.jpg', 1, N'1')
                    INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (82, N'Proyecto', CAST(N'2018-12-03T14:56:00.617' AS DateTime), N'pista.jpg', 1, N'2')
                    INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (83, N'Beneficiario', CAST(N'2018-12-05T12:23:02.350' AS DateTime), N'aeroparquepista.jpg', 0, NULL)
                    INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (84, N'Beneficiario', CAST(N'2018-12-05T12:24:17.497' AS DateTime), N'aeroparquepista.jpg', 0, NULL)
                    INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (85, N'Beneficiario', CAST(N'2018-12-05T12:24:23.747' AS DateTime), N'acces.txt', 0, NULL)
                    INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (86, N'Beneficiario', CAST(N'2018-12-05T12:24:28.490' AS DateTime), N'mi-factura.pdf', 0, NULL)
                    INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (87, N'Beneficiario', CAST(N'2018-12-05T12:24:57.907' AS DateTime), N'pista.jpg', 0, NULL)
                    INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (88, N'Beneficiario', CAST(N'2018-12-05T12:25:13.433' AS DateTime), N'mi-factura.pdf', 1, NULL)
                    INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (89, N'Proyecto', CAST(N'2018-12-05T13:55:58.450' AS DateTime), N'aeroparquepista.jpg', 1, N'2')
                    INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (90, N'Proyecto', CAST(N'2018-12-05T14:03:22.653' AS DateTime), N'mi-factura.pdf', 1, N'1')
                    INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (91, N'Proyecto', CAST(N'2018-12-05T14:03:42.127' AS DateTime), N'aeroparquepista.jpg', 1, N'1')
                    INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (92, N'Beneficiario', CAST(N'2018-12-06T09:40:38.607' AS DateTime), N'pista.jpg', 0, NULL)
                    INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (93, N'Beneficiario', CAST(N'2018-12-06T09:40:51.910' AS DateTime), N'pista.jpg', 0, NULL)
                    INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (94, N'Beneficiario', CAST(N'2018-12-06T09:41:08.657' AS DateTime), N'mi-factura.pdf', 0, NULL)
                    INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (95, N'Beneficiario', CAST(N'2018-12-06T09:41:15.007' AS DateTime), N'aeroparquepista.jpg', 0, NULL)
                    INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (96, N'Beneficiario', CAST(N'2018-12-06T11:23:40.970' AS DateTime), N'BenefAlta_ValidacionIdentifTribut_06-12-2018 11-17-50.png', 0, NULL)
                    INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (97, N'Beneficiario', CAST(N'2018-12-06T11:24:02.327' AS DateTime), N'BeneficCtaBancaria_06-12-2018 11-06-38.png', 0, NULL)
                    INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (98, N'Beneficiario', CAST(N'2018-12-06T15:35:19.700' AS DateTime), N'aeroparquepista.jpg', 0, NULL)
                    INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (99, N'Beneficiario', CAST(N'2018-12-06T15:36:33.197' AS DateTime), N'aeroparquepista.jpg', 0, NULL)
                    INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (100, N'Beneficiario', CAST(N'2018-12-06T15:42:58.137' AS DateTime), N'AltaEditbenef_AcentoGuardo06-12-2018 15-36-14.png', 1, NULL)
                    INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (101, N'Proyecto', CAST(N'2018-12-06T15:55:17.943' AS DateTime), N'BenefAlta_ValidacionIdentifTribut_06-12-2018 11-17-50.png', 1, N'2')
                    INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (102, N'Proyecto', CAST(N'2018-12-06T16:51:52.187' AS DateTime), N'aeroparquepista.jpg', 1, N'2')
                    INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (103, N'Beneficiario', CAST(N'2018-12-07T09:39:35.647' AS DateTime), N'AltaBenef_IdentifTribut_fixed_07-12-2018 9-36-56.png', 0, NULL)
                    INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (104, N'Beneficiario', CAST(N'2018-12-07T09:39:53.883' AS DateTime), N'BenefRaon_fixed_07-12-2018 9-21-42.png', 0, NULL)
                    INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (105, N'Proyecto', CAST(N'2018-12-10T14:39:04.643' AS DateTime), N'aeroparquepista.jpg', 1, N'2')
                    INSERT [dbo].[Adjuntos] ([Id], [Modulo], [FechaAlta], [NombreArchivo], [Estado], [TipoAnexo]) VALUES (106, N'Proyecto', CAST(N'2018-12-10T15:57:16.167' AS DateTime), N'aeroparquepista.jpg', 1, N'2')
                    SET IDENTITY_INSERT [dbo].[Adjuntos] OFF
                    SET IDENTITY_INSERT [dbo].[Aeropuertos] ON 

                    INSERT [dbo].[Aeropuertos] ([Id], [Nombre], [IdProvincia], [FechaInicio], [Fechafin], [IdAeropuertosGrupo], [NombreCorto], [CodIata], [Estado]) VALUES (1, N'Ezeiza', 1, CAST(N'2018-10-02T00:00:00.000' AS DateTime), CAST(N'2018-10-02T00:00:00.000' AS DateTime), 2, N'EZE', N'EZP123', 1)
                    INSERT [dbo].[Aeropuertos] ([Id], [Nombre], [IdProvincia], [FechaInicio], [Fechafin], [IdAeropuertosGrupo], [NombreCorto], [CodIata], [Estado]) VALUES (2, N'Ezpeleta', 1, CAST(N'2018-10-02T00:00:00.000' AS DateTime), CAST(N'2018-10-02T00:00:00.000' AS DateTime), 1, N'EZP', N'EZP123', 1)
                    INSERT [dbo].[Aeropuertos] ([Id], [Nombre], [IdProvincia], [FechaInicio], [Fechafin], [IdAeropuertosGrupo], [NombreCorto], [CodIata], [Estado]) VALUES (3, N'Iguazu', 1, CAST(N'2018-10-02T00:00:00.000' AS DateTime), CAST(N'2018-10-02T00:00:00.000' AS DateTime), 1, N'IGU', N'IGU123', 1)
                    SET IDENTITY_INSERT [dbo].[Aeropuertos] OFF


                    SET IDENTITY_INSERT [dbo].[BeneficiarioAdjuntos] ON 

                    INSERT [dbo].[BeneficiarioAdjuntos] ([Id], [IdBeneficiario], [IdAdjunto], [Estado]) VALUES (7, 8, 12, 1)
                    INSERT [dbo].[BeneficiarioAdjuntos] ([Id], [IdBeneficiario], [IdAdjunto], [Estado]) VALUES (8, 9, 13, 1)
                    INSERT [dbo].[BeneficiarioAdjuntos] ([Id], [IdBeneficiario], [IdAdjunto], [Estado]) VALUES (9, 8, 14, 1)
                    INSERT [dbo].[BeneficiarioAdjuntos] ([Id], [IdBeneficiario], [IdAdjunto], [Estado]) VALUES (10, 8, 15, 1)
                    INSERT [dbo].[BeneficiarioAdjuntos] ([Id], [IdBeneficiario], [IdAdjunto], [Estado]) VALUES (11, 8, 16, 1)
                    INSERT [dbo].[BeneficiarioAdjuntos] ([Id], [IdBeneficiario], [IdAdjunto], [Estado]) VALUES (12, 12, 17, 1)
                    INSERT [dbo].[BeneficiarioAdjuntos] ([Id], [IdBeneficiario], [IdAdjunto], [Estado]) VALUES (13, 20, 83, 0)
                    INSERT [dbo].[BeneficiarioAdjuntos] ([Id], [IdBeneficiario], [IdAdjunto], [Estado]) VALUES (14, 20, 84, 0)
                    INSERT [dbo].[BeneficiarioAdjuntos] ([Id], [IdBeneficiario], [IdAdjunto], [Estado]) VALUES (15, 20, 85, 0)
                    INSERT [dbo].[BeneficiarioAdjuntos] ([Id], [IdBeneficiario], [IdAdjunto], [Estado]) VALUES (16, 20, 86, 0)
                    INSERT [dbo].[BeneficiarioAdjuntos] ([Id], [IdBeneficiario], [IdAdjunto], [Estado]) VALUES (17, 20, 87, 0)
                    INSERT [dbo].[BeneficiarioAdjuntos] ([Id], [IdBeneficiario], [IdAdjunto], [Estado]) VALUES (18, 20, 88, 1)
                    INSERT [dbo].[BeneficiarioAdjuntos] ([Id], [IdBeneficiario], [IdAdjunto], [Estado]) VALUES (19, 23, 92, 0)
                    INSERT [dbo].[BeneficiarioAdjuntos] ([Id], [IdBeneficiario], [IdAdjunto], [Estado]) VALUES (20, 23, 93, 0)
                    INSERT [dbo].[BeneficiarioAdjuntos] ([Id], [IdBeneficiario], [IdAdjunto], [Estado]) VALUES (21, 23, 94, 0)
                    INSERT [dbo].[BeneficiarioAdjuntos] ([Id], [IdBeneficiario], [IdAdjunto], [Estado]) VALUES (22, 23, 95, 0)
                    INSERT [dbo].[BeneficiarioAdjuntos] ([Id], [IdBeneficiario], [IdAdjunto], [Estado]) VALUES (23, 24, 96, 0)
                    INSERT [dbo].[BeneficiarioAdjuntos] ([Id], [IdBeneficiario], [IdAdjunto], [Estado]) VALUES (24, 24, 97, 0)
                    INSERT [dbo].[BeneficiarioAdjuntos] ([Id], [IdBeneficiario], [IdAdjunto], [Estado]) VALUES (25, 25, 98, 0)
                    INSERT [dbo].[BeneficiarioAdjuntos] ([Id], [IdBeneficiario], [IdAdjunto], [Estado]) VALUES (26, 25, 99, 0)
                    INSERT [dbo].[BeneficiarioAdjuntos] ([Id], [IdBeneficiario], [IdAdjunto], [Estado]) VALUES (27, 25, 100, 1)
                    INSERT [dbo].[BeneficiarioAdjuntos] ([Id], [IdBeneficiario], [IdAdjunto], [Estado]) VALUES (28, 26, 103, 0)
                    INSERT [dbo].[BeneficiarioAdjuntos] ([Id], [IdBeneficiario], [IdAdjunto], [Estado]) VALUES (29, 26, 104, 0)
                    SET IDENTITY_INSERT [dbo].[BeneficiarioAdjuntos] OFF
                    SET IDENTITY_INSERT [dbo].[BeneficiarioBancos] ON 

                    INSERT [dbo].[BeneficiarioBancos] ([Id], [IdBeneficiario], [TipoCuenta], [NroCuenta], [NombreBanco], [Sucursal], [CBU], [Cuit], [Titular], [FechaVigencia], [Estado], [Direccion], [BicSwift], [EsNacional]) VALUES (7, 8, N'Ahorro', N'nro cuenta', N'nombre', N'sucursal', N'cbu', N'cuit', N'titular', CAST(N'2018-10-02T00:00:00.000' AS DateTime), 1, NULL, NULL, NULL)
                    INSERT [dbo].[BeneficiarioBancos] ([Id], [IdBeneficiario], [TipoCuenta], [NroCuenta], [NombreBanco], [Sucursal], [CBU], [Cuit], [Titular], [FechaVigencia], [Estado], [Direccion], [BicSwift], [EsNacional]) VALUES (8, 8, N'Ahorro', N'nrocuenta1', N'banco', N'sucualr', N'cbu1', N'cuit', N'titular', CAST(N'2018-10-02T00:00:00.000' AS DateTime), 1, NULL, NULL, NULL)
                    INSERT [dbo].[BeneficiarioBancos] ([Id], [IdBeneficiario], [TipoCuenta], [NroCuenta], [NombreBanco], [Sucursal], [CBU], [Cuit], [Titular], [FechaVigencia], [Estado], [Direccion], [BicSwift], [EsNacional]) VALUES (9, 9, N'Ahorro', N'nro cuenta', N'nombre banco', N'sucursal', N'cbu123', N'cuit', N'titular', CAST(N'2018-10-09T00:00:00.000' AS DateTime), 1, NULL, NULL, NULL)
                    INSERT [dbo].[BeneficiarioBancos] ([Id], [IdBeneficiario], [TipoCuenta], [NroCuenta], [NombreBanco], [Sucursal], [CBU], [Cuit], [Titular], [FechaVigencia], [Estado], [Direccion], [BicSwift], [EsNacional]) VALUES (10, NULL, N'Corriente', N'123212', N'Sofitasa', N'San Cristobal', N'cbunueva12', N'descripcion', N'2212', CAST(N'2018-11-06T00:00:00.000' AS DateTime), 1, NULL, NULL, NULL)
                    INSERT [dbo].[BeneficiarioBancos] ([Id], [IdBeneficiario], [TipoCuenta], [NroCuenta], [NombreBanco], [Sucursal], [CBU], [Cuit], [Titular], [FechaVigencia], [Estado], [Direccion], [BicSwift], [EsNacional]) VALUES (11, NULL, N'Ahorro', N'a sdas d asd', N'asdfdsafas das', N' asd asd as', N' asd asdas ', N'as das das', N' asd asd asd as', CAST(N'2018-11-06T00:00:00.000' AS DateTime), 1, NULL, NULL, NULL)
                    INSERT [dbo].[BeneficiarioBancos] ([Id], [IdBeneficiario], [TipoCuenta], [NroCuenta], [NombreBanco], [Sucursal], [CBU], [Cuit], [Titular], [FechaVigencia], [Estado], [Direccion], [BicSwift], [EsNacional]) VALUES (12, 9, N'Ahorro', N'asdas das', N'asdasdas', N' asda sd as', N'asd asd as as', N' asd asd as', N' asd asd as as', CAST(N'2018-11-06T00:00:00.000' AS DateTime), 1, NULL, NULL, NULL)
                    INSERT [dbo].[BeneficiarioBancos] ([Id], [IdBeneficiario], [TipoCuenta], [NroCuenta], [NombreBanco], [Sucursal], [CBU], [Cuit], [Titular], [FechaVigencia], [Estado], [Direccion], [BicSwift], [EsNacional]) VALUES (13, 9, N'Corriente', N'sdfgh', N'sdfds', N'sadfgh', N'sadfgh', N'asdfg', N'dsfgh', CAST(N'2018-11-06T00:00:00.000' AS DateTime), 1, NULL, NULL, NULL)
                    INSERT [dbo].[BeneficiarioBancos] ([Id], [IdBeneficiario], [TipoCuenta], [NroCuenta], [NombreBanco], [Sucursal], [CBU], [Cuit], [Titular], [FechaVigencia], [Estado], [Direccion], [BicSwift], [EsNacional]) VALUES (14, 12, N'Corriente', N'12345', N'Santander', N'Palermo', N'1234', N'1234', N'Gonzalo', CAST(N'2018-11-16T00:00:00.000' AS DateTime), 1, NULL, NULL, NULL)
                    INSERT [dbo].[BeneficiarioBancos] ([Id], [IdBeneficiario], [TipoCuenta], [NroCuenta], [NombreBanco], [Sucursal], [CBU], [Cuit], [Titular], [FechaVigencia], [Estado], [Direccion], [BicSwift], [EsNacional]) VALUES (15, 16, N'Ahorro', N'dfghj', N'dsfgh', N'sdfgh', N'dfghj', N'sdfgh', N'dfgh', CAST(N'2018-11-06T00:00:00.000' AS DateTime), 0, NULL, NULL, NULL)
                    INSERT [dbo].[BeneficiarioBancos] ([Id], [IdBeneficiario], [TipoCuenta], [NroCuenta], [NombreBanco], [Sucursal], [CBU], [Cuit], [Titular], [FechaVigencia], [Estado], [Direccion], [BicSwift], [EsNacional]) VALUES (16, 18, N'Corriente', N'123456', N'Galicia', N'CABA', N'23415145365', N'41343554', N'Aeropuerto', CAST(N'2018-11-23T00:00:00.000' AS DateTime), 1, NULL, NULL, NULL)
                    INSERT [dbo].[BeneficiarioBancos] ([Id], [IdBeneficiario], [TipoCuenta], [NroCuenta], [NombreBanco], [Sucursal], [CBU], [Cuit], [Titular], [FechaVigencia], [Estado], [Direccion], [BicSwift], [EsNacional]) VALUES (17, 19, N'Corriente', N'12345678901234567892', N'Galicia', N'centro', N'0070999030004105263288', NULL, N'Repuestos Ezeiza', CAST(N'2018-12-06T00:00:00.000' AS DateTime), 1, N'', N'', NULL)
                    INSERT [dbo].[BeneficiarioBancos] ([Id], [IdBeneficiario], [TipoCuenta], [NroCuenta], [NombreBanco], [Sucursal], [CBU], [Cuit], [Titular], [FechaVigencia], [Estado], [Direccion], [BicSwift], [EsNacional]) VALUES (18, 20, N'Corriente', N'00000000003470034566', N'Banco de la Nación Argentina', N'2327 - Maipú - Mendoza', N'0110347020034700345662', NULL, N'CONSTRUCCIONES ELECTROMECANICAS DEL OESTE S.A.', CAST(N'2018-12-31T00:00:00.000' AS DateTime), 0, N'', N'', NULL)
                    INSERT [dbo].[BeneficiarioBancos] ([Id], [IdBeneficiario], [TipoCuenta], [NroCuenta], [NombreBanco], [Sucursal], [CBU], [Cuit], [Titular], [FechaVigencia], [Estado], [Direccion], [BicSwift], [EsNacional]) VALUES (19, 21, N'Corriente', N'12345678901234567892', N'Galicia', N'centro', N'0070999030004105263288', NULL, N'Repuestos Ezeiza', CAST(N'2018-12-26T00:00:00.000' AS DateTime), 0, N'', N'', NULL)
                    INSERT [dbo].[BeneficiarioBancos] ([Id], [IdBeneficiario], [TipoCuenta], [NroCuenta], [NombreBanco], [Sucursal], [CBU], [Cuit], [Titular], [FechaVigencia], [Estado], [Direccion], [BicSwift], [EsNacional]) VALUES (20, 21, N'Corriente', N'12345678901234567892', N'Galicia', N'centro', N'0070999030004105263288', NULL, N'Repuestos Ezeiza', CAST(N'2018-12-26T00:00:00.000' AS DateTime), 1, N'', N'', NULL)
                    INSERT [dbo].[BeneficiarioBancos] ([Id], [IdBeneficiario], [TipoCuenta], [NroCuenta], [NombreBanco], [Sucursal], [CBU], [Cuit], [Titular], [FechaVigencia], [Estado], [Direccion], [BicSwift], [EsNacional]) VALUES (21, 20, N'Corriente', N'00000000003470034566', N'Banco de la Nación Argentina', N'2327 - Maipú - Mendoza', N'0110347020034700345662', NULL, N'CONSTRUCCIONES ELECTROMECANICAS DEL OESTE S.A.', CAST(N'2018-12-31T00:00:00.000' AS DateTime), 0, N'', N'', NULL)
                    INSERT [dbo].[BeneficiarioBancos] ([Id], [IdBeneficiario], [TipoCuenta], [NroCuenta], [NombreBanco], [Sucursal], [CBU], [Cuit], [Titular], [FechaVigencia], [Estado], [Direccion], [BicSwift], [EsNacional]) VALUES (22, 20, N'Corriente', N'00000000003470034566', N'Banco de la Nación Argentina', N'2327 - Maipú - Mendoza', N'0110347020034700345662', NULL, N'CONSTRUCCIONES ELECTROMECANICAS DEL OESTE S.A.', CAST(N'2018-12-31T00:00:00.000' AS DateTime), 0, N'', N'', NULL)
                    INSERT [dbo].[BeneficiarioBancos] ([Id], [IdBeneficiario], [TipoCuenta], [NroCuenta], [NombreBanco], [Sucursal], [CBU], [Cuit], [Titular], [FechaVigencia], [Estado], [Direccion], [BicSwift], [EsNacional]) VALUES (23, 20, N'Corriente', N'00000000003470034566', N'Banco de la Nación Argentina', N'2327 - Maipú - Mendoza', N'0110347020034700345662', NULL, N'CONSTRUCCIONES ELECTROMECANICAS DEL OESTE S.A.', CAST(N'2018-12-31T00:00:00.000' AS DateTime), 1, N'', N'', NULL)
                    INSERT [dbo].[BeneficiarioBancos] ([Id], [IdBeneficiario], [TipoCuenta], [NroCuenta], [NombreBanco], [Sucursal], [CBU], [Cuit], [Titular], [FechaVigencia], [Estado], [Direccion], [BicSwift], [EsNacional]) VALUES (24, 22, N'Corriente', N'12345678901234567892', N'Galicia', N'centro', N'0070999030004105263288', NULL, N'Repuestos Ezeiza', CAST(N'2018-12-19T00:00:00.000' AS DateTime), 1, N'', N'', NULL)
                    INSERT [dbo].[BeneficiarioBancos] ([Id], [IdBeneficiario], [TipoCuenta], [NroCuenta], [NombreBanco], [Sucursal], [CBU], [Cuit], [Titular], [FechaVigencia], [Estado], [Direccion], [BicSwift], [EsNacional]) VALUES (25, 23, N'Corriente', N'12345678901234567892', N'Santander', N'centro', N'0070999030004105263288', NULL, N'Repuestos Ezeiza', CAST(N'2018-12-27T00:00:00.000' AS DateTime), 0, N'', N'', NULL)
                    INSERT [dbo].[BeneficiarioBancos] ([Id], [IdBeneficiario], [TipoCuenta], [NroCuenta], [NombreBanco], [Sucursal], [CBU], [Cuit], [Titular], [FechaVigencia], [Estado], [Direccion], [BicSwift], [EsNacional]) VALUES (26, 23, N'Corriente', N'12345678901234567892', N'Santander', N'centro', N'0070999030004105263288', NULL, N'Repuestos Ezeiza', CAST(N'2018-12-27T00:00:00.000' AS DateTime), 1, N'', N'', NULL)
                    INSERT [dbo].[BeneficiarioBancos] ([Id], [IdBeneficiario], [TipoCuenta], [NroCuenta], [NombreBanco], [Sucursal], [CBU], [Cuit], [Titular], [FechaVigencia], [Estado], [Direccion], [BicSwift], [EsNacional]) VALUES (27, 24, N'', N'12345678901234567892', N'Santander', N'', N'', NULL, N'', CAST(N'2018-12-20T00:00:00.000' AS DateTime), 1, N'centro', N'3546456456456', NULL)
                    INSERT [dbo].[BeneficiarioBancos] ([Id], [IdBeneficiario], [TipoCuenta], [NroCuenta], [NombreBanco], [Sucursal], [CBU], [Cuit], [Titular], [FechaVigencia], [Estado], [Direccion], [BicSwift], [EsNacional]) VALUES (28, 25, N'Corriente', N'11111111111111111111', N'1111', N'111', N'1111111111111111111111', NULL, N'111111', CAST(N'2018-12-28T00:00:00.000' AS DateTime), 0, N'', N'', NULL)
                    INSERT [dbo].[BeneficiarioBancos] ([Id], [IdBeneficiario], [TipoCuenta], [NroCuenta], [NombreBanco], [Sucursal], [CBU], [Cuit], [Titular], [FechaVigencia], [Estado], [Direccion], [BicSwift], [EsNacional]) VALUES (29, 25, N'Corriente', N'11111111111111111111', N'1111', N'111', N'1111111111111111111111', NULL, N'111111', CAST(N'2018-12-28T00:00:00.000' AS DateTime), 0, N'', N'', NULL)
                    INSERT [dbo].[BeneficiarioBancos] ([Id], [IdBeneficiario], [TipoCuenta], [NroCuenta], [NombreBanco], [Sucursal], [CBU], [Cuit], [Titular], [FechaVigencia], [Estado], [Direccion], [BicSwift], [EsNacional]) VALUES (30, 25, N'Corriente', N'11111111111111111111', N'1111', N'111', N'1111111111111111111111', NULL, N'111111', CAST(N'2018-12-28T00:00:00.000' AS DateTime), 1, N'', N'', NULL)
                    INSERT [dbo].[BeneficiarioBancos] ([Id], [IdBeneficiario], [TipoCuenta], [NroCuenta], [NombreBanco], [Sucursal], [CBU], [Cuit], [Titular], [FechaVigencia], [Estado], [Direccion], [BicSwift], [EsNacional]) VALUES (31, 26, N'Ahorro', N'12345678901234567892', N'Santander', N'centro', N'0070999030004105263288', NULL, N'Repuestos Ezeiza', CAST(N'2018-12-07T00:00:00.000' AS DateTime), 0, N'', N'', NULL)
                    INSERT [dbo].[BeneficiarioBancos] ([Id], [IdBeneficiario], [TipoCuenta], [NroCuenta], [NombreBanco], [Sucursal], [CBU], [Cuit], [Titular], [FechaVigencia], [Estado], [Direccion], [BicSwift], [EsNacional]) VALUES (32, 26, N'Ahorro', N'12345678901234567892', N'Santander', N'centro', N'0070999030004105263288', NULL, N'Repuestos Ezeiza', CAST(N'2018-12-07T00:00:00.000' AS DateTime), 1, N'', N'', NULL)
                    SET IDENTITY_INSERT [dbo].[BeneficiarioBancos] OFF


                    SET IDENTITY_INSERT [dbo].[LibranzaBeneficiarios] ON 

                    INSERT [dbo].[LibranzaBeneficiarios] ([Id], [IdLibranzas], [IdBeneficiario], [IdBeneficiarioBancos], [Estado]) VALUES (2, 3, 19, 17, 0)
                    INSERT [dbo].[LibranzaBeneficiarios] ([Id], [IdLibranzas], [IdBeneficiario], [IdBeneficiarioBancos], [Estado]) VALUES (3, 3, 19, 17, 1)
                    INSERT [dbo].[LibranzaBeneficiarios] ([Id], [IdLibranzas], [IdBeneficiario], [IdBeneficiarioBancos], [Estado]) VALUES (4, 4, 18, 16, 0)
                    INSERT [dbo].[LibranzaBeneficiarios] ([Id], [IdLibranzas], [IdBeneficiario], [IdBeneficiarioBancos], [Estado]) VALUES (5, 4, 18, 16, 0)
                    INSERT [dbo].[LibranzaBeneficiarios] ([Id], [IdLibranzas], [IdBeneficiario], [IdBeneficiarioBancos], [Estado]) VALUES (6, 5, 20, 18, 1)
                    INSERT [dbo].[LibranzaBeneficiarios] ([Id], [IdLibranzas], [IdBeneficiario], [IdBeneficiarioBancos], [Estado]) VALUES (7, 4, 18, 16, 1)
                    INSERT [dbo].[LibranzaBeneficiarios] ([Id], [IdLibranzas], [IdBeneficiario], [IdBeneficiarioBancos], [Estado]) VALUES (8, 6, 20, 23, 0)
                    INSERT [dbo].[LibranzaBeneficiarios] ([Id], [IdLibranzas], [IdBeneficiario], [IdBeneficiarioBancos], [Estado]) VALUES (9, 6, 20, 23, 1)
                    INSERT [dbo].[LibranzaBeneficiarios] ([Id], [IdLibranzas], [IdBeneficiario], [IdBeneficiarioBancos], [Estado]) VALUES (10, 7, 19, 17, 1)
                    INSERT [dbo].[LibranzaBeneficiarios] ([Id], [IdLibranzas], [IdBeneficiario], [IdBeneficiarioBancos], [Estado]) VALUES (11, 8, 20, 23, 1)
                    INSERT [dbo].[LibranzaBeneficiarios] ([Id], [IdLibranzas], [IdBeneficiario], [IdBeneficiarioBancos], [Estado]) VALUES (12, 9, 20, 23, 0)
                    INSERT [dbo].[LibranzaBeneficiarios] ([Id], [IdLibranzas], [IdBeneficiario], [IdBeneficiarioBancos], [Estado]) VALUES (13, 9, 20, 23, 1)
                    SET IDENTITY_INSERT [dbo].[LibranzaBeneficiarios] OFF
                    SET IDENTITY_INSERT [dbo].[LibranzaFacturas] ON 

                    INSERT [dbo].[LibranzaFacturas] ([Id], [IdLibranza], [Tipo], [Nro], [Fecha], [Monto], [Iva], [Estado], [Ibb]) VALUES (2, 3, N'Debito', N'12234546546657', CAST(N'2018-11-30T00:00:00.000' AS DateTime), CAST(500000.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), 0, CAST(12.00 AS Decimal(18, 2)))
                    INSERT [dbo].[LibranzaFacturas] ([Id], [IdLibranza], [Tipo], [Nro], [Fecha], [Monto], [Iva], [Estado], [Ibb]) VALUES (3, 3, N'Factura', N'455666', CAST(N'2018-11-30T00:00:00.000' AS DateTime), CAST(300000.00 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), 0, CAST(10.00 AS Decimal(18, 2)))
                    INSERT [dbo].[LibranzaFacturas] ([Id], [IdLibranza], [Tipo], [Nro], [Fecha], [Monto], [Iva], [Estado], [Ibb]) VALUES (4, 3, N'Debito', N'12234546546657', CAST(N'2018-11-30T00:00:00.000' AS DateTime), CAST(500000.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), 1, CAST(12.00 AS Decimal(18, 2)))
                    INSERT [dbo].[LibranzaFacturas] ([Id], [IdLibranza], [Tipo], [Nro], [Fecha], [Monto], [Iva], [Estado], [Ibb]) VALUES (5, 4, N'Debito', N'1234567898', CAST(N'2018-12-03T00:00:00.000' AS DateTime), CAST(500000.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), 0, CAST(12.00 AS Decimal(18, 2)))
                    INSERT [dbo].[LibranzaFacturas] ([Id], [IdLibranza], [Tipo], [Nro], [Fecha], [Monto], [Iva], [Estado], [Ibb]) VALUES (6, 4, N'Debito', N'1234567898', CAST(N'2018-12-03T00:00:00.000' AS DateTime), CAST(500000.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), 0, CAST(12.00 AS Decimal(18, 2)))
                    INSERT [dbo].[LibranzaFacturas] ([Id], [IdLibranza], [Tipo], [Nro], [Fecha], [Monto], [Iva], [Estado], [Ibb]) VALUES (7, 5, N'Factura', N'0004-00003228', CAST(N'2018-06-13T00:00:00.000' AS DateTime), CAST(52000000.00 AS Decimal(18, 2)), NULL, 1, NULL)
                    INSERT [dbo].[LibranzaFacturas] ([Id], [IdLibranza], [Tipo], [Nro], [Fecha], [Monto], [Iva], [Estado], [Ibb]) VALUES (8, 4, N'Debito', N'1234567898', CAST(N'2018-12-03T00:00:00.000' AS DateTime), CAST(500000.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), 1, CAST(12.00 AS Decimal(18, 2)))
                    INSERT [dbo].[LibranzaFacturas] ([Id], [IdLibranza], [Tipo], [Nro], [Fecha], [Monto], [Iva], [Estado], [Ibb]) VALUES (9, 4, N'Factura', N'11111111', CAST(N'2018-12-03T00:00:00.000' AS DateTime), CAST(1000.00 AS Decimal(18, 2)), NULL, 1, NULL)
                    INSERT [dbo].[LibranzaFacturas] ([Id], [IdLibranza], [Tipo], [Nro], [Fecha], [Monto], [Iva], [Estado], [Ibb]) VALUES (10, 6, N'Factura', N'1234567898', CAST(N'2018-12-12T00:00:00.000' AS DateTime), CAST(200000.00 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), 0, CAST(10.00 AS Decimal(18, 2)))
                    INSERT [dbo].[LibranzaFacturas] ([Id], [IdLibranza], [Tipo], [Nro], [Fecha], [Monto], [Iva], [Estado], [Ibb]) VALUES (11, 6, N'Factura', N'1234567898', CAST(N'2018-12-10T00:00:00.000' AS DateTime), CAST(700000.00 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), 1, CAST(10.00 AS Decimal(18, 2)))
                    INSERT [dbo].[LibranzaFacturas] ([Id], [IdLibranza], [Tipo], [Nro], [Fecha], [Monto], [Iva], [Estado], [Ibb]) VALUES (12, 7, N'Factura', N'12', CAST(N'2018-12-10T00:00:00.000' AS DateTime), CAST(120000.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), 1, CAST(12.00 AS Decimal(18, 2)))
                    INSERT [dbo].[LibranzaFacturas] ([Id], [IdLibranza], [Tipo], [Nro], [Fecha], [Monto], [Iva], [Estado], [Ibb]) VALUES (13, 8, N'Factura', N'77788899999', CAST(N'2018-12-10T00:00:00.000' AS DateTime), CAST(1500000.00 AS Decimal(18, 2)), NULL, 1, NULL)
                    INSERT [dbo].[LibranzaFacturas] ([Id], [IdLibranza], [Tipo], [Nro], [Fecha], [Monto], [Iva], [Estado], [Ibb]) VALUES (14, 9, N'Factura', N'12', CAST(N'2018-10-12T00:00:00.000' AS DateTime), CAST(10000.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), 0, CAST(11.00 AS Decimal(18, 2)))
                    INSERT [dbo].[LibranzaFacturas] ([Id], [IdLibranza], [Tipo], [Nro], [Fecha], [Monto], [Iva], [Estado], [Ibb]) VALUES (15, 9, N'Factura', N'12', CAST(N'2018-10-12T00:00:00.000' AS DateTime), CAST(10000.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), 1, CAST(11.00 AS Decimal(18, 2)))
                    SET IDENTITY_INSERT [dbo].[LibranzaFacturas] OFF

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
                    INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (63, 66, 79, 1)
                    INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (64, 67, 80, 1)
                    INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (65, 67, 81, 1)
                    INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (66, 68, 82, 1)
                    INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (67, 69, 89, 1)
                    INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (68, 69, 90, 1)
                    INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (69, 69, 91, 1)
                    INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (70, 70, 101, 1)
                    INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (71, 71, 102, 1)
                    INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (72, 72, 105, 1)
                    INSERT [dbo].[ProyectoAdjuntos] ([Id], [IdProyecto], [IdAdjunto], [Estado]) VALUES (73, 73, 106, 1)
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
                    INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (80, 60, 2, 1)
                    INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (81, 60, 1, 1)
                    INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (82, 60, 3, 1)
                    INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (83, 63, 1, 1)
                    INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (84, 64, 3, 0)
                    INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (85, 64, 3, 0)
                    INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (86, 64, 3, 1)
                    INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (87, 64, 1, 1)
                    INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (88, 65, 1, 1)
                    INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (89, 66, 3, 1)
                    INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (90, 67, 1, 0)
                    INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (91, 67, 1, 0)
                    INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (92, 67, 1, 1)
                    INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (93, 68, 1, 1)
                    INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (94, 69, 1, 0)
                    INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (95, 69, 1, 0)
                    INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (96, 69, 1, 0)
                    INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (97, 69, 1, 1)
                    INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (98, 70, 1, 1)
                    INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (99, 70, 2, 1)
                    INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (100, 70, 3, 1)
                    INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (101, 71, 1, 1)
                    INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (102, 71, 2, 1)
                    INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (103, 72, 1, 0)
                    INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (104, 72, 1, 1)
                    INSERT [dbo].[ProyectoAeropuertos] ([Id], [IdProyecto], [IdAeropuerto], [Estado]) VALUES (105, 73, 2, 1)
                    SET IDENTITY_INSERT [dbo].[ProyectoAeropuertos] OFF
                ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                EXEC sp_MSForEachTable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL'
                EXEC sp_MSForEachTable 'DELETE FROM ?'
                EXEC sp_MSForEachTable 'ALTER TABLE ? CHECK CONSTRAINT ALL'
                ");
        }
    }
}
