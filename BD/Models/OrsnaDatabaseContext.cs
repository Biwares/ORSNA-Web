using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BD.Models
{
    public partial class OrsnaDatabaseContext : DbContext
    {
        string stringConnection;
        public OrsnaDatabaseContext(string con)
        {
            stringConnection = con;
        }


        public OrsnaDatabaseContext(DbContextOptions<OrsnaDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Adjuntos> Adjuntos { get; set; }
        public virtual DbSet<Aeropuertos> Aeropuertos { get; set; }
        public virtual DbSet<AeropuertosGrupo> AeropuertosGrupo { get; set; }
        public virtual DbSet<Areas> Areas { get; set; }
        public virtual DbSet<AreasModulos> AreasModulos { get; set; }
        public virtual DbSet<AreasModulosPermisos> AreasModulosPermisos { get; set; }
        public virtual DbSet<BeneficiarioAdjuntos> BeneficiarioAdjuntos { get; set; }
        public virtual DbSet<BeneficiarioBancos> BeneficiarioBancos { get; set; }

        public virtual DbSet<Beneficiarios> Beneficiarios { get; set; }
        public virtual DbSet<Beneficiarios> BeneficiariosCesion { get; set; }
        public virtual DbSet<Cuentas> Cuentas { get; set; }
        public virtual DbSet<LibranzaAeropuertos> LibranzaAeropuertos { get; set; }
        public virtual DbSet<LibranzaBeneficiarios> LibranzaBeneficiarios { get; set; }
        public virtual DbSet<LibranzaBeneficiariosCesiones> LibranzaBeneficiariosCesiones { get; set; }
        //public virtual DbSet<LibranzaCesiones> LibranzaCesiones { get; set; }
        public virtual DbSet<LibranzaDetalleWorkflow> LibranzaDetalleWorkflow { get; set; }
        public virtual DbSet<LibranzaEmbargos> LibranzaEmbargos { get; set; }
        public virtual DbSet<LibranzaFacturas> LibranzaFacturas { get; set; }
        public virtual DbSet<Libranzas> Libranzas { get; set; }
        public virtual DbSet<LibranzasEstado> LibranzasEstado { get; set; }
        public virtual DbSet<LibranzaTipo> LibranzaTipo { get; set; }
        public virtual DbSet<LibranzaAdjuntos> LibranzaAdjuntos { get; set; }
        public virtual DbSet<Log> Log { get; set; }
        public virtual DbSet<Modulos> Modulos { get; set; }
        public virtual DbSet<Permisos> Permisos { get; set; }
        public virtual DbSet<Provincias> Provincias { get; set; }
        public virtual DbSet<ProyectoAdjuntos> ProyectoAdjuntos { get; set; }
        public virtual DbSet<ProyectoAeropuertos> ProyectoAeropuertos { get; set; }
        public virtual DbSet<ProyectoBeneficiarios> ProyectoBeneficiarios { get; set; }
        public virtual DbSet<ProyectoProvincias> ProyectoProvincias { get; set; }
        public virtual DbSet<Proyectos> Proyectos { get; set; }
        public virtual DbSet<ProyectosEstado> ProyectosEstado { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
        public virtual DbSet<UsuariosAreas> UsuariosAreas { get; set; }
        public virtual DbSet<UsuarioRol> UsuarioRol { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<RolModulo> RolModulo { get; set; }
        public virtual DbSet<Destinos> Destinos { get; set; }
        public virtual DbSet<Moneda> Moneda { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(stringConnection);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Adjuntos>(entity =>
            {
                entity.Property(e => e.Estado).HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaAlta).HasColumnType("datetime");

                entity.Property(e => e.Modulo).HasMaxLength(50);

                entity.Property(e => e.NombreArchivo).HasMaxLength(500);

                entity.Property(e => e.TipoAnexo).HasMaxLength(50);
            });

            modelBuilder.Entity<Aeropuertos>(entity =>
            {
                entity.Property(e => e.CodIata).HasMaxLength(500);

                entity.Property(e => e.FechaInicio).HasColumnType("datetime");

                entity.Property(e => e.Fechafin).HasColumnType("datetime");

                entity.Property(e => e.Nombre).HasMaxLength(200);

                entity.Property(e => e.NombreCorto).HasMaxLength(500);

                entity.HasOne(d => d.IdAeropuertosGrupoNavigation)
                    .WithMany(p => p.Aeropuertos)
                    .HasForeignKey(d => d.IdAeropuertosGrupo)
                    .HasConstraintName("FK_Aeropuertos_AeropuertosGrupo");

                entity.HasOne(d => d.IdProvinciaNavigation)
                    .WithMany(p => p.Aeropuertos)
                    .HasForeignKey(d => d.IdProvincia)
                    .HasConstraintName("FK_Aeropuertos_Provincias");
            });

            modelBuilder.Entity<AeropuertosGrupo>(entity =>
            {
                entity.Property(e => e.Estado).HasDefaultValueSql("((1))");

                entity.Property(e => e.Nombre).HasMaxLength(100);
            });

            modelBuilder.Entity<Areas>(entity =>
            {
                entity.Property(e => e.Codigo).HasMaxLength(500);

                entity.Property(e => e.Estado).HasDefaultValueSql("((1))");

                entity.Property(e => e.Nombre).HasMaxLength(500);
            });

            modelBuilder.Entity<AreasModulos>(entity =>
            {
                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdAreaNavigation)
                    .WithMany(p => p.AreasModulos)
                    .HasForeignKey(d => d.IdArea)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AreasModulos_Areas");

                entity.HasOne(d => d.IdModuloNavigation)
                    .WithMany(p => p.AreasModulos)
                    .HasForeignKey(d => d.IdModulo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AreasModulos_Modulos");
            });

            modelBuilder.Entity<AreasModulosPermisos>(entity =>
            {
                entity.Property(e => e.Estado).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdAreaModuloNavigation)
                    .WithMany(p => p.AreasModulosPermisos)
                    .HasForeignKey(d => d.IdAreaModulo)
                    .HasConstraintName("FK_AreasModulosPermisos_AreasModulos");

                entity.HasOne(d => d.IdPermisoNavigation)
                    .WithMany(p => p.AreasModulosPermisos)
                    .HasForeignKey(d => d.IdPermiso)
                    .HasConstraintName("FK_AreasModulosPermisos_Permisos");
            });

            modelBuilder.Entity<BeneficiarioAdjuntos>(entity =>
            {
                entity.Property(e => e.Estado).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdAdjuntoNavigation)
                    .WithMany(p => p.BeneficiarioAdjuntos)
                    .HasForeignKey(d => d.IdAdjunto)
                    .HasConstraintName("FK_BeneficiarioAdjuntos_Adjuntos");

                entity.HasOne(d => d.IdBeneficiarioNavigation)
                    .WithMany(p => p.BeneficiarioAdjuntos)
                    .HasForeignKey(d => d.IdBeneficiario)
                    .HasConstraintName("FK_BeneficiarioAdjuntos_Beneficiarios");
            });

            modelBuilder.Entity<BeneficiarioBancos>(entity =>
            {
                entity.Property(e => e.BicSwift).HasMaxLength(100);

                entity.Property(e => e.Cbu)
                    .HasColumnName("CBU")
                    .HasMaxLength(100);

                entity.Property(e => e.Cuit).HasMaxLength(50);

                entity.Property(e => e.Direccion).HasMaxLength(500);

                entity.Property(e => e.Estado).HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaVigencia).HasColumnType("datetime");

                entity.Property(e => e.NombreBanco).HasMaxLength(500);

                entity.Property(e => e.NroCuenta).HasMaxLength(500);

                entity.Property(e => e.Sucursal).HasMaxLength(500);

                entity.Property(e => e.TipoCuenta)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.IdBeneficiarioNavigation)
                    .WithMany(p => p.BeneficiarioBancos)
                    .HasForeignKey(d => d.IdBeneficiario)
                    .HasConstraintName("FK_BancoProveedores_Proveedores");



            });

            modelBuilder.Entity<Beneficiarios>(entity =>
            {
                entity.Property(e => e.Cuit).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.Estado).HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaAlta).HasColumnType("datetime");

                entity.Property(e => e.NacionalExtranjero)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono).HasMaxLength(100);
            });

            modelBuilder.Entity<Cuentas>(entity =>
            {
                entity.Property(e => e.Estado).HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.FechaVigencia).HasColumnType("datetime");

                entity.Property(e => e.Nombre).HasMaxLength(500);

                entity.Property(e => e.NroCuenta).HasMaxLength(100);

                entity.HasOne(d => d.IdAeropuertosGrupoNavigation)
                    .WithMany(p => p.Cuentas)
                    .HasForeignKey(d => d.IdAeropuertosGrupo)
                    .HasConstraintName("FK_Cuentas_AeropuertosGrupo");

                entity.HasOne(d => d.IdLibranzaTipoNavigation)
                    .WithMany(p => p.Cuentas)
                    .HasForeignKey(d => d.IdLibranzaTipo)
                    .HasConstraintName("FK_Cuentas_LibranzaTipo");
            });

            modelBuilder.Entity<LibranzaAeropuertos>(entity =>
            {
                entity.Property(e => e.Estado).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdAeropuertoNavigation)
                    .WithMany(p => p.LibranzaAeropuertos)
                    .HasForeignKey(d => d.IdAeropuerto)
                    .HasConstraintName("FK_LibranzaAeropuertos_Aeropuertos");

                entity.HasOne(d => d.IdLibranzaNavigation)
                    .WithMany(p => p.LibranzaAeropuertos)
                    .HasForeignKey(d => d.IdLibranza)
                    .HasConstraintName("FK_LibranzaAeropuertos_Libranzas");
            });

            modelBuilder.Entity<LibranzaBeneficiarios>(entity =>
            {
                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdBeneficiarioNavigation)
                    .WithMany(p => p.LibranzaBeneficiarios)
                    .HasForeignKey(d => d.IdBeneficiario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LibranzaBeneficiarios_Beneficiarios");

                entity.HasOne(d => d.IdBeneficiarioBancosNavigation)
                    .WithMany(p => p.LibranzaBeneficiarios)
                    .HasForeignKey(d => d.IdBeneficiarioBancos)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LibranzaBeneficiarios_BeneficiarioBancos");


                entity.HasOne(d => d.IdLibranzasNavigation)
                    .WithMany(p => p.LibranzaBeneficiarios)
                    .HasForeignKey(d => d.IdLibranzas)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LibranzaBeneficiarios_Libranzas");
            });

        

            modelBuilder.Entity<LibranzaCesiones>(entity =>
            {
                entity.Property(e => e.Beneficiario).HasMaxLength(100);

                entity.Property(e => e.Cuit).HasMaxLength(50);

                entity.Property(e => e.Estado).HasDefaultValueSql("((1))");

                entity.Property(e => e.NroEscritura).HasMaxLength(50);

                entity.Property(e => e.RegistraCesion).HasMaxLength(50);

                entity.HasOne(d => d.IdLibranzaNavigation)
                    .WithMany(p => p.LibranzaCesiones)
                    .HasForeignKey(d => d.IdLibranza)
                    .HasConstraintName("FK_LibranzaCesiones_Libranzas");
            });

            modelBuilder.Entity<LibranzaDetalleWorkflow>(entity =>
            {
                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.Observaciones).IsRequired();

                entity.HasOne(d => d.IdEstadoAnteriorNavigation)
                    .WithMany(p => p.LibranzaDetalleWorkflowIdEstadoAnteriorNavigation)
                    .HasForeignKey(d => d.IdEstadoAnterior)
                    .HasConstraintName("FK_LibranzaDetalleWorkflow_LibranzasEstado");

                entity.HasOne(d => d.IdLibranzaNavigation)
                    .WithMany(p => p.LibranzaDetalleWorkflow)
                    .HasForeignKey(d => d.IdLibranza)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LibranzaDetalleWorkflow_Libranzas");

                entity.HasOne(d => d.IdNuevoEstadoNavigation)
                    .WithMany(p => p.LibranzaDetalleWorkflowIdNuevoEstadoNavigation)
                    .HasForeignKey(d => d.IdNuevoEstado)
                    .HasConstraintName("FK_LibranzaDetalleWorkflow_LibranzasEstado1");
            });

            modelBuilder.Entity<LibranzaEmbargos>(entity =>
            {
                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.NroEmbargo).HasMaxLength(50);

                entity.Property(e => e.Responsable).HasMaxLength(100);

                entity.HasOne(d => d.IdLibranzaNavigation)
                    .WithMany(p => p.LibranzaEmbargos)
                    .HasForeignKey(d => d.IdLibranza)
                    .HasConstraintName("FK_LibranzaEmbargos_Libranzas");
            });

            modelBuilder.Entity<LibranzaFacturas>(entity =>
            {
                entity.Property(e => e.Estado).HasDefaultValueSql("((1))");

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.Nro).HasMaxLength(50);

                entity.Property(e => e.Tipo).HasMaxLength(50);

                entity.HasOne(d => d.IdLibranzaNavigation)
                    .WithMany(p => p.LibranzaFacturas)
                    .HasForeignKey(d => d.IdLibranza)
                    .HasConstraintName("FK_LibranzaFacturas_Libranzas");
            });

            modelBuilder.Entity<Libranzas>(entity =>
            {
                entity.Property(e => e.Beneficiario)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(e => e.Moneda).WithMany().HasForeignKey(e => e.MonedaId).IsRequired();

                entity.Property(e => e.Estado).HasDefaultValueSql("((1))");

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.IdentificacionTributaria)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.NroEmbargo).HasMaxLength(200);

                entity.Property(e => e.NroEscritura)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.NroLibranza).HasMaxLength(100);

                entity.Property(e => e.ResponsableEmbargo).HasMaxLength(200);

                entity.Property(e => e.TasaDeCambio).HasColumnType("decimal(18, 10)");

                entity.HasOne(d => d.IdLibranzaEstadosNavigation)
                    .WithMany(p => p.Libranzas)
                    .HasForeignKey(d => d.IdLibranzaEstados)
                    .HasConstraintName("FK_Libranzas_EstadoLibranzas");

                entity.HasOne(d => d.IdProyectoNavigation)
                    .WithMany(p => p.Libranzas)
                    .HasForeignKey(d => d.IdProyecto)
                    .HasConstraintName("FK_Libranzas_Proyectos");
            });

            modelBuilder.Entity<LibranzasEstado>(entity =>
            {
                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<LibranzaTipo>(entity =>
            {
                entity.Property(e => e.Estado).HasDefaultValueSql("((1))");

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<LibranzaAdjuntos>(entity =>
            {
                entity.Property(e => e.Estado).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdAdjuntoNavigation)
                    .WithMany(l => l.LibranzaAdjuntos)
                    .HasForeignKey(d => d.IdAdjunto)
                    .HasConstraintName("FK_LibranzaAdjuntos_Adjuntos");

                entity.HasOne(d => d.IdLibranzaNavigation)
                    .WithMany(l => l.LibranzaAdjuntos)
                    .HasForeignKey(d => d.IdLibranza)
                    .HasConstraintName("FK_LibranzaAdjuntos_Libranzas");
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.Mensaje)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Ubicacion)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Modulos>(entity =>
            {
                entity.Property(e => e.Estado).HasDefaultValueSql("((1))");

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<Permisos>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Estado).HasDefaultValueSql("((1))");

                entity.Property(e => e.Key).HasMaxLength(200);

                entity.Property(e => e.Nombre).HasMaxLength(200);
            });

            modelBuilder.Entity<Provincias>(entity =>
            {
                entity.Property(e => e.Estado).HasDefaultValueSql("((1))");

                entity.Property(e => e.Nombre).HasMaxLength(500);
            });

            modelBuilder.Entity<ProyectoAdjuntos>(entity =>
            {
                entity.Property(e => e.Estado).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdAdjuntoNavigation)
                    .WithMany(p => p.ProyectoAdjuntos)
                    .HasForeignKey(d => d.IdAdjunto)
                    .HasConstraintName("FK_ProyectoAdjuntos_Adjuntos");

                entity.HasOne(d => d.IdProyectoNavigation)
                    .WithMany(p => p.ProyectoAdjuntos)
                    .HasForeignKey(d => d.IdProyecto)
                    .HasConstraintName("FK_ProyectoAdjuntos_Proyectos");
            });


            modelBuilder.Entity<ProyectoAeropuertos>(entity =>
            {
                entity.Property(e => e.Estado).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdProyectoNavigation)
                    .WithMany(p => p.ProyectoAeropuertos)
                    .HasForeignKey(d => d.IdProyecto)
                    .HasConstraintName("FK_ProyectoAeropuertos_Proyectos");
            });

            modelBuilder.Entity<ProyectoBeneficiarios>(entity =>
            {
                entity.Property(e => e.Estado).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdBeneficiarioNavigation)
                    .WithMany(p => p.ProyectoBeneficiarios)
                    .HasForeignKey(d => d.IdBeneficiario)
                    .HasConstraintName("FK_ProyectoProveedores_Proveedores");

                entity.HasOne(d => d.IdProyectoNavigation)
                    .WithMany(p => p.ProyectoBeneficiarios)
                    .HasForeignKey(d => d.IdProyecto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProyectoProveedores_Proyectos");
            });

            modelBuilder.Entity<ProyectoProvincias>(entity =>
            {
                entity.Property(e => e.Estado).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdProvinciaNavigation)
                    .WithMany(p => p.ProyectoProvincias)
                    .HasForeignKey(d => d.IdProvincia)
                    .HasConstraintName("FK_ProyectoProvincias_Provincias");

                entity.HasOne(d => d.IdProyectoNavigation)
                    .WithMany(p => p.ProyectoProvincias)
                    .HasForeignKey(d => d.IdProyecto)
                    .HasConstraintName("FK_ProyectoProvincias_Proyectos");
            });

            modelBuilder.Entity<Proyectos>(entity =>
            {
                entity.Property(e => e.CodObra).HasMaxLength(500);

                entity.Property(e => e.Estado).HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.IdProyecto).HasMaxLength(500);

                entity.Property(e => e.Nombre).HasMaxLength(500);

                entity.Property(e => e.NroContratacion).HasMaxLength(50);

                entity.Property(e => e.NroObra).HasMaxLength(50);

                entity.Property(e => e.NroPago).HasMaxLength(50);

                entity.Property(e => e.TipoEstado).HasMaxLength(100);

                entity.HasOne(d => d.IdAreaNavigation)
                    .WithMany(p => p.Proyectos)
                    .HasForeignKey(d => d.IdArea)
                    .HasConstraintName("FK_Proyectos_Areas");

                entity.HasOne(d => d.IdCuentaNavigation)
                    .WithMany(p => p.Proyectos)
                    .HasForeignKey(d => d.IdCuenta)
                    .HasConstraintName("FK_Proyectos_Cuentas");

                entity.HasOne(d => d.IdProyectosEstadoNavigation)
                    .WithMany(p => p.Proyectos)
                    .HasForeignKey(d => d.IdProyectosEstado)
                    .HasConstraintName("FK_Proyectos_ProyectosEstado");
            });

            modelBuilder.Entity<ProyectosEstado>(entity =>
            {
                entity.Property(e => e.Estado).HasDefaultValueSql("((1))");

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<Usuarios>(entity =>
            {
                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(200);

                entity.Property(e => e.Estado).HasDefaultValueSql("((1))");

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<UsuariosAreas>(entity =>
            {
                entity.Property(e => e.Estado).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdAreaNavigation)
                    .WithMany(p => p.UsuariosAreas)
                    .HasForeignKey(d => d.IdArea)
                    .HasConstraintName("FK_UsuariosAreas_Areas");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.UsuariosAreas)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK_UsuariosAreas_Usuarios");
            });
        }
    }
}
