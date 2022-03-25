using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BD.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adjuntos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Modulo = table.Column<string>(maxLength: 50, nullable: true),
                    FechaAlta = table.Column<DateTime>(type: "datetime", nullable: false),
                    NombreArchivo = table.Column<string>(maxLength: 500, nullable: true),
                    Estado = table.Column<bool>(nullable: true, defaultValueSql: "((1))"),
                    TipoAnexo = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adjuntos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AeropuertosGrupo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(maxLength: 100, nullable: true),
                    Estado = table.Column<bool>(nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AeropuertosGrupo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Areas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(maxLength: 500, nullable: true),
                    Codigo = table.Column<string>(maxLength: 500, nullable: true),
                    Estado = table.Column<bool>(nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Beneficiarios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RazonSocial = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    Cuit = table.Column<string>(maxLength: 50, nullable: true),
                    FechaAlta = table.Column<DateTime>(type: "datetime", nullable: false),
                    NacionalExtranjero = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    Email = table.Column<string>(maxLength: 100, nullable: true),
                    Estado = table.Column<bool>(nullable: true, defaultValueSql: "((1))"),
                    Telefono = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beneficiarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LibranzasEstado",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(maxLength: 50, nullable: true),
                    Estado = table.Column<bool>(nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibranzasEstado", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LibranzaTipo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(maxLength: 50, nullable: true),
                    Estado = table.Column<bool>(nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibranzaTipo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Log",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ubicacion = table.Column<string>(maxLength: 50, nullable: false),
                    Mensaje = table.Column<string>(maxLength: 500, nullable: false),
                    Detalle = table.Column<string>(nullable: true),
                    Fecha = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Modulos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(maxLength: 50, nullable: true),
                    Estado = table.Column<bool>(nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modulos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permisos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(maxLength: 200, nullable: true),
                    Key = table.Column<string>(maxLength: 200, nullable: true),
                    Estado = table.Column<bool>(nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permisos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Provincias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(maxLength: 500, nullable: true),
                    Estado = table.Column<bool>(nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provincias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProyectosEstado",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(maxLength: 50, nullable: true),
                    Estado = table.Column<bool>(nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProyectosEstado", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    email = table.Column<string>(maxLength: 200, nullable: true),
                    password = table.Column<string>(maxLength: 200, nullable: true),
                    Estado = table.Column<bool>(nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BeneficiarioAdjuntos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdBeneficiario = table.Column<int>(nullable: true),
                    IdAdjunto = table.Column<int>(nullable: true),
                    Estado = table.Column<bool>(nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeneficiarioAdjuntos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BeneficiarioAdjuntos_Adjuntos",
                        column: x => x.IdAdjunto,
                        principalTable: "Adjuntos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BeneficiarioAdjuntos_Beneficiarios",
                        column: x => x.IdBeneficiario,
                        principalTable: "Beneficiarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BeneficiarioBancos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdBeneficiario = table.Column<int>(nullable: true),
                    TipoCuenta = table.Column<string>(maxLength: 500, nullable: false),
                    NroCuenta = table.Column<string>(maxLength: 500, nullable: true),
                    NombreBanco = table.Column<string>(maxLength: 500, nullable: true),
                    Sucursal = table.Column<string>(maxLength: 500, nullable: true),
                    CBU = table.Column<string>(maxLength: 100, nullable: true),
                    Cuit = table.Column<string>(maxLength: 50, nullable: true),
                    Titular = table.Column<string>(nullable: true),
                    FechaVigencia = table.Column<DateTime>(type: "datetime", nullable: false),
                    Estado = table.Column<bool>(nullable: true, defaultValueSql: "((1))"),
                    Direccion = table.Column<string>(maxLength: 500, nullable: true),
                    BicSwift = table.Column<string>(maxLength: 100, nullable: true),
                    EsNacional = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeneficiarioBancos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BancoProveedores_Proveedores",
                        column: x => x.IdBeneficiario,
                        principalTable: "Beneficiarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cuentas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NroCuenta = table.Column<string>(maxLength: 100, nullable: true),
                    Nombre = table.Column<string>(maxLength: 500, nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    FechaVigencia = table.Column<DateTime>(type: "datetime", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    IdLibranzaTipo = table.Column<int>(nullable: true),
                    IdAeropuertosGrupo = table.Column<int>(nullable: true),
                    Estado = table.Column<bool>(nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuentas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cuentas_AeropuertosGrupo",
                        column: x => x.IdAeropuertosGrupo,
                        principalTable: "AeropuertosGrupo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cuentas_LibranzaTipo",
                        column: x => x.IdLibranzaTipo,
                        principalTable: "LibranzaTipo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AreasModulos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdArea = table.Column<int>(nullable: false),
                    IdModulo = table.Column<int>(nullable: false),
                    Estado = table.Column<bool>(nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AreasModulos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AreasModulos_Areas",
                        column: x => x.IdArea,
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AreasModulos_Modulos",
                        column: x => x.IdModulo,
                        principalTable: "Modulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Aeropuertos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(maxLength: 200, nullable: true),
                    IdProvincia = table.Column<int>(nullable: true),
                    FechaInicio = table.Column<DateTime>(type: "datetime", nullable: true),
                    Fechafin = table.Column<DateTime>(type: "datetime", nullable: true),
                    IdAeropuertosGrupo = table.Column<int>(nullable: true),
                    NombreCorto = table.Column<string>(maxLength: 500, nullable: true),
                    CodIata = table.Column<string>(maxLength: 500, nullable: true),
                    Estado = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aeropuertos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aeropuertos_AeropuertosGrupo",
                        column: x => x.IdAeropuertosGrupo,
                        principalTable: "AeropuertosGrupo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Aeropuertos_Provincias",
                        column: x => x.IdProvincia,
                        principalTable: "Provincias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsuariosAreas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdUsuario = table.Column<int>(nullable: true),
                    IdArea = table.Column<int>(nullable: true),
                    Estado = table.Column<bool>(nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosAreas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuariosAreas_Areas",
                        column: x => x.IdArea,
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsuariosAreas_Usuarios",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Proyectos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(maxLength: 500, nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    IdCuenta = table.Column<int>(nullable: true),
                    IdProyecto = table.Column<string>(maxLength: 500, nullable: true),
                    IdArea = table.Column<int>(nullable: true),
                    NroContratacion = table.Column<string>(maxLength: 50, nullable: true),
                    NroObra = table.Column<string>(maxLength: 50, nullable: true),
                    CodObra = table.Column<string>(maxLength: 500, nullable: true),
                    NroPago = table.Column<string>(maxLength: 50, nullable: true),
                    NormaLegal = table.Column<string>(maxLength: 500, nullable: true),
                    Objeto = table.Column<string>(nullable: true),
                    MontoTotal = table.Column<decimal>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    TipoEstado = table.Column<string>(maxLength: 100, nullable: true),
                    Estado = table.Column<bool>(nullable: true, defaultValueSql: "((1))"),
                    IdProyectosEstado = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proyectos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Proyectos_Areas",
                        column: x => x.IdArea,
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Proyectos_Cuentas",
                        column: x => x.IdCuenta,
                        principalTable: "Cuentas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Proyectos_ProyectosEstado",
                        column: x => x.IdProyectosEstado,
                        principalTable: "ProyectosEstado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AreasModulosPermisos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdAreaModulo = table.Column<int>(nullable: true),
                    IdPermiso = table.Column<int>(nullable: true),
                    Estado = table.Column<bool>(nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AreasModulosPermisos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AreasModulosPermisos_AreasModulos",
                        column: x => x.IdAreaModulo,
                        principalTable: "AreasModulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AreasModulosPermisos_Permisos",
                        column: x => x.IdPermiso,
                        principalTable: "Permisos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Libranzas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdLibranzaTipo = table.Column<int>(nullable: true),
                    NroLibranza = table.Column<string>(maxLength: 100, nullable: true),
                    IdProvincia = table.Column<int>(nullable: true),
                    IdProyecto = table.Column<int>(nullable: true),
                    IdLibranzaEstados = table.Column<int>(nullable: true),
                    MontoFondoReparo = table.Column<decimal>(nullable: true),
                    Multa = table.Column<decimal>(nullable: true),
                    Mora = table.Column<decimal>(nullable: true),
                    MontoRestante = table.Column<decimal>(nullable: true),
                    Fecha = table.Column<DateTime>(type: "datetime", nullable: false),
                    Estado = table.Column<bool>(nullable: true, defaultValueSql: "((1))"),
                    Sustituido = table.Column<bool>(nullable: true),
                    NroEmbargo = table.Column<string>(maxLength: 200, nullable: true),
                    ResponsableEmbargo = table.Column<string>(maxLength: 200, nullable: true),
                    MontoEmbargo = table.Column<decimal>(nullable: true),
                    SaldoEmbargo = table.Column<decimal>(nullable: true),
                    RegistraCesion = table.Column<bool>(nullable: true),
                    NroEscritura = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    Beneficiario = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    IdentificacionTributaria = table.Column<string>(unicode: false, maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Libranzas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Libranzas_EstadoLibranzas",
                        column: x => x.IdLibranzaEstados,
                        principalTable: "LibranzasEstado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Libranzas_Proyectos",
                        column: x => x.IdProyecto,
                        principalTable: "Proyectos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProyectoAdjuntos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdProyecto = table.Column<int>(nullable: true),
                    IdAdjunto = table.Column<int>(nullable: true),
                    Estado = table.Column<bool>(nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProyectoAdjuntos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProyectoAdjuntos_Adjuntos",
                        column: x => x.IdAdjunto,
                        principalTable: "Adjuntos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProyectoAdjuntos_Proyectos",
                        column: x => x.IdProyecto,
                        principalTable: "Proyectos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProyectoAeropuertos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdProyecto = table.Column<int>(nullable: true),
                    IdAeropuerto = table.Column<int>(nullable: true),
                    Estado = table.Column<bool>(nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProyectoAeropuertos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProyectoAeropuertos_Proyectos",
                        column: x => x.IdProyecto,
                        principalTable: "Proyectos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProyectoBeneficiarios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdProyecto = table.Column<int>(nullable: false),
                    IdBeneficiario = table.Column<int>(nullable: true),
                    Estado = table.Column<bool>(nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProyectoBeneficiarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProyectoProveedores_Proveedores",
                        column: x => x.IdBeneficiario,
                        principalTable: "Beneficiarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProyectoProveedores_Proyectos",
                        column: x => x.IdProyecto,
                        principalTable: "Proyectos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProyectoProvincias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdProyecto = table.Column<int>(nullable: true),
                    IdProvincia = table.Column<int>(nullable: true),
                    Estado = table.Column<bool>(nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProyectoProvincias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProyectoProvincias_Provincias",
                        column: x => x.IdProvincia,
                        principalTable: "Provincias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProyectoProvincias_Proyectos",
                        column: x => x.IdProyecto,
                        principalTable: "Proyectos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LibranzaAeropuertos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdLibranza = table.Column<int>(nullable: true),
                    IdAeropuerto = table.Column<int>(nullable: true),
                    Estado = table.Column<bool>(nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibranzaAeropuertos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LibranzaAeropuertos_Aeropuertos",
                        column: x => x.IdAeropuerto,
                        principalTable: "Aeropuertos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LibranzaAeropuertos_Libranzas",
                        column: x => x.IdLibranza,
                        principalTable: "Libranzas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LibranzaBeneficiarios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdLibranzas = table.Column<int>(nullable: false),
                    IdBeneficiario = table.Column<int>(nullable: false),
                    IdBeneficiarioBancos = table.Column<int>(nullable: false),
                    Estado = table.Column<bool>(nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibranzaBeneficiarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LibranzaBeneficiarios_Beneficiarios",
                        column: x => x.IdBeneficiario,
                        principalTable: "Beneficiarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LibranzaBeneficiarios_BeneficiarioBancos",
                        column: x => x.IdBeneficiarioBancos,
                        principalTable: "BeneficiarioBancos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LibranzaBeneficiarios_Libranzas",
                        column: x => x.IdLibranzas,
                        principalTable: "Libranzas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LibranzaCesiones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdLibranza = table.Column<int>(nullable: true),
                    Beneficiario = table.Column<string>(maxLength: 100, nullable: true),
                    Cuit = table.Column<string>(maxLength: 50, nullable: true),
                    RegistraCesion = table.Column<string>(maxLength: 50, nullable: true),
                    NroEscritura = table.Column<string>(maxLength: 50, nullable: true),
                    Estado = table.Column<bool>(nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibranzaCesiones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LibranzaCesiones_Libranzas",
                        column: x => x.IdLibranza,
                        principalTable: "Libranzas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LibranzaDetalleWorkflow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdLibranza = table.Column<int>(nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime", nullable: false),
                    IdUsuario = table.Column<int>(nullable: false),
                    Observaciones = table.Column<string>(nullable: false),
                    IdEstadoAnterior = table.Column<int>(nullable: true),
                    IdNuevoEstado = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibranzaDetalleWorkflow", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LibranzaDetalleWorkflow_LibranzasEstado",
                        column: x => x.IdEstadoAnterior,
                        principalTable: "LibranzasEstado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LibranzaDetalleWorkflow_Libranzas",
                        column: x => x.IdLibranza,
                        principalTable: "Libranzas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LibranzaDetalleWorkflow_LibranzasEstado1",
                        column: x => x.IdNuevoEstado,
                        principalTable: "LibranzasEstado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LibranzaEmbargos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdLibranza = table.Column<int>(nullable: true),
                    NroEmbargo = table.Column<string>(maxLength: 50, nullable: true),
                    Responsable = table.Column<string>(maxLength: 100, nullable: true),
                    Monto = table.Column<decimal>(nullable: true),
                    Saldo = table.Column<decimal>(nullable: true),
                    Estado = table.Column<bool>(nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibranzaEmbargos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LibranzaEmbargos_Libranzas",
                        column: x => x.IdLibranza,
                        principalTable: "Libranzas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LibranzaFacturas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdLibranza = table.Column<int>(nullable: true),
                    Tipo = table.Column<string>(maxLength: 50, nullable: true),
                    Nro = table.Column<string>(maxLength: 50, nullable: true),
                    Fecha = table.Column<DateTime>(type: "datetime", nullable: false),
                    Monto = table.Column<decimal>(nullable: true),
                    Iva = table.Column<decimal>(nullable: true),
                    Estado = table.Column<bool>(nullable: true, defaultValueSql: "((1))"),
                    Ibb = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibranzaFacturas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LibranzaFacturas_Libranzas",
                        column: x => x.IdLibranza,
                        principalTable: "Libranzas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aeropuertos_IdAeropuertosGrupo",
                table: "Aeropuertos",
                column: "IdAeropuertosGrupo");

            migrationBuilder.CreateIndex(
                name: "IX_Aeropuertos_IdProvincia",
                table: "Aeropuertos",
                column: "IdProvincia");

            migrationBuilder.CreateIndex(
                name: "IX_AreasModulos_IdArea",
                table: "AreasModulos",
                column: "IdArea");

            migrationBuilder.CreateIndex(
                name: "IX_AreasModulos_IdModulo",
                table: "AreasModulos",
                column: "IdModulo");

            migrationBuilder.CreateIndex(
                name: "IX_AreasModulosPermisos_IdAreaModulo",
                table: "AreasModulosPermisos",
                column: "IdAreaModulo");

            migrationBuilder.CreateIndex(
                name: "IX_AreasModulosPermisos_IdPermiso",
                table: "AreasModulosPermisos",
                column: "IdPermiso");

            migrationBuilder.CreateIndex(
                name: "IX_BeneficiarioAdjuntos_IdAdjunto",
                table: "BeneficiarioAdjuntos",
                column: "IdAdjunto");

            migrationBuilder.CreateIndex(
                name: "IX_BeneficiarioAdjuntos_IdBeneficiario",
                table: "BeneficiarioAdjuntos",
                column: "IdBeneficiario");

            migrationBuilder.CreateIndex(
                name: "IX_BeneficiarioBancos_IdBeneficiario",
                table: "BeneficiarioBancos",
                column: "IdBeneficiario");

            migrationBuilder.CreateIndex(
                name: "IX_Cuentas_IdAeropuertosGrupo",
                table: "Cuentas",
                column: "IdAeropuertosGrupo");

            migrationBuilder.CreateIndex(
                name: "IX_Cuentas_IdLibranzaTipo",
                table: "Cuentas",
                column: "IdLibranzaTipo");

            migrationBuilder.CreateIndex(
                name: "IX_LibranzaAeropuertos_IdAeropuerto",
                table: "LibranzaAeropuertos",
                column: "IdAeropuerto");

            migrationBuilder.CreateIndex(
                name: "IX_LibranzaAeropuertos_IdLibranza",
                table: "LibranzaAeropuertos",
                column: "IdLibranza");

            migrationBuilder.CreateIndex(
                name: "IX_LibranzaBeneficiarios_IdBeneficiario",
                table: "LibranzaBeneficiarios",
                column: "IdBeneficiario");

            migrationBuilder.CreateIndex(
                name: "IX_LibranzaBeneficiarios_IdBeneficiarioBancos",
                table: "LibranzaBeneficiarios",
                column: "IdBeneficiarioBancos");

            migrationBuilder.CreateIndex(
                name: "IX_LibranzaBeneficiarios_IdLibranzas",
                table: "LibranzaBeneficiarios",
                column: "IdLibranzas");

            migrationBuilder.CreateIndex(
                name: "IX_LibranzaCesiones_IdLibranza",
                table: "LibranzaCesiones",
                column: "IdLibranza");

            migrationBuilder.CreateIndex(
                name: "IX_LibranzaDetalleWorkflow_IdEstadoAnterior",
                table: "LibranzaDetalleWorkflow",
                column: "IdEstadoAnterior");

            migrationBuilder.CreateIndex(
                name: "IX_LibranzaDetalleWorkflow_IdLibranza",
                table: "LibranzaDetalleWorkflow",
                column: "IdLibranza");

            migrationBuilder.CreateIndex(
                name: "IX_LibranzaDetalleWorkflow_IdNuevoEstado",
                table: "LibranzaDetalleWorkflow",
                column: "IdNuevoEstado");

            migrationBuilder.CreateIndex(
                name: "IX_LibranzaEmbargos_IdLibranza",
                table: "LibranzaEmbargos",
                column: "IdLibranza");

            migrationBuilder.CreateIndex(
                name: "IX_LibranzaFacturas_IdLibranza",
                table: "LibranzaFacturas",
                column: "IdLibranza");

            migrationBuilder.CreateIndex(
                name: "IX_Libranzas_IdLibranzaEstados",
                table: "Libranzas",
                column: "IdLibranzaEstados");

            migrationBuilder.CreateIndex(
                name: "IX_Libranzas_IdProyecto",
                table: "Libranzas",
                column: "IdProyecto");

            migrationBuilder.CreateIndex(
                name: "IX_ProyectoAdjuntos_IdAdjunto",
                table: "ProyectoAdjuntos",
                column: "IdAdjunto");

            migrationBuilder.CreateIndex(
                name: "IX_ProyectoAdjuntos_IdProyecto",
                table: "ProyectoAdjuntos",
                column: "IdProyecto");

            migrationBuilder.CreateIndex(
                name: "IX_ProyectoAeropuertos_IdProyecto",
                table: "ProyectoAeropuertos",
                column: "IdProyecto");

            migrationBuilder.CreateIndex(
                name: "IX_ProyectoBeneficiarios_IdBeneficiario",
                table: "ProyectoBeneficiarios",
                column: "IdBeneficiario");

            migrationBuilder.CreateIndex(
                name: "IX_ProyectoBeneficiarios_IdProyecto",
                table: "ProyectoBeneficiarios",
                column: "IdProyecto");

            migrationBuilder.CreateIndex(
                name: "IX_ProyectoProvincias_IdProvincia",
                table: "ProyectoProvincias",
                column: "IdProvincia");

            migrationBuilder.CreateIndex(
                name: "IX_ProyectoProvincias_IdProyecto",
                table: "ProyectoProvincias",
                column: "IdProyecto");

            migrationBuilder.CreateIndex(
                name: "IX_Proyectos_IdArea",
                table: "Proyectos",
                column: "IdArea");

            migrationBuilder.CreateIndex(
                name: "IX_Proyectos_IdCuenta",
                table: "Proyectos",
                column: "IdCuenta");

            migrationBuilder.CreateIndex(
                name: "IX_Proyectos_IdProyectosEstado",
                table: "Proyectos",
                column: "IdProyectosEstado");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosAreas_IdArea",
                table: "UsuariosAreas",
                column: "IdArea");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosAreas_IdUsuario",
                table: "UsuariosAreas",
                column: "IdUsuario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AreasModulosPermisos");

            migrationBuilder.DropTable(
                name: "BeneficiarioAdjuntos");

            migrationBuilder.DropTable(
                name: "LibranzaAeropuertos");

            migrationBuilder.DropTable(
                name: "LibranzaBeneficiarios");

            migrationBuilder.DropTable(
                name: "LibranzaCesiones");

            migrationBuilder.DropTable(
                name: "LibranzaDetalleWorkflow");

            migrationBuilder.DropTable(
                name: "LibranzaEmbargos");

            migrationBuilder.DropTable(
                name: "LibranzaFacturas");

            migrationBuilder.DropTable(
                name: "Log");

            migrationBuilder.DropTable(
                name: "ProyectoAdjuntos");

            migrationBuilder.DropTable(
                name: "ProyectoAeropuertos");

            migrationBuilder.DropTable(
                name: "ProyectoBeneficiarios");

            migrationBuilder.DropTable(
                name: "ProyectoProvincias");

            migrationBuilder.DropTable(
                name: "UsuariosAreas");

            migrationBuilder.DropTable(
                name: "AreasModulos");

            migrationBuilder.DropTable(
                name: "Permisos");

            migrationBuilder.DropTable(
                name: "Aeropuertos");

            migrationBuilder.DropTable(
                name: "BeneficiarioBancos");

            migrationBuilder.DropTable(
                name: "Libranzas");

            migrationBuilder.DropTable(
                name: "Adjuntos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Modulos");

            migrationBuilder.DropTable(
                name: "Provincias");

            migrationBuilder.DropTable(
                name: "Beneficiarios");

            migrationBuilder.DropTable(
                name: "LibranzasEstado");

            migrationBuilder.DropTable(
                name: "Proyectos");

            migrationBuilder.DropTable(
                name: "Areas");

            migrationBuilder.DropTable(
                name: "Cuentas");

            migrationBuilder.DropTable(
                name: "ProyectosEstado");

            migrationBuilder.DropTable(
                name: "AeropuertosGrupo");

            migrationBuilder.DropTable(
                name: "LibranzaTipo");
        }
    }
}
