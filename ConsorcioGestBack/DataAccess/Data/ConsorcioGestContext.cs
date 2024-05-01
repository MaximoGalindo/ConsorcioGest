using System;
using System.Collections.Generic;
using DataAccess.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Data;

public partial class ConsorcioGestContext : DbContext
{
    public ConsorcioGestContext()
    {
    }

    public ConsorcioGestContext(DbContextOptions<ConsorcioGestContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CausaProblema> CausaProblemas { get; set; }

    public virtual DbSet<Condominio> Condominios { get; set; }

    public virtual DbSet<Consorcio> Consorcios { get; set; }

    public virtual DbSet<ConsorcioUsuario> ConsorcioUsuarios { get; set; }

    public virtual DbSet<ConsortiumConfiguration> ConsortiumConfigurations { get; set; }

    public virtual DbSet<Contacto> Contactos { get; set; }

    public virtual DbSet<Encuesta> Encuestas { get; set; }

    public virtual DbSet<EncuestasDetalle> EncuestasDetalles { get; set; }

    public virtual DbSet<EspacioAfectado> EspacioAfectados { get; set; }

    public virtual DbSet<EspacioComun> EspacioComuns { get; set; }

    public virtual DbSet<EstadoEncuestum> EstadoEncuesta { get; set; }

    public virtual DbSet<EstadoReclamo> EstadoReclamos { get; set; }

    public virtual DbSet<EstadoReserva> EstadoReservas { get; set; }

    public virtual DbSet<EstadoUsuario> EstadoUsuarios { get; set; }

    public virtual DbSet<Opcion> Opcions { get; set; }

    public virtual DbSet<Perfil> Perfils { get; set; }

    public virtual DbSet<Preguntum> Pregunta { get; set; }

    public virtual DbSet<Reclamo> Reclamos { get; set; }

    public virtual DbSet<ReclamoDetalle> ReclamoDetalles { get; set; }

    public virtual DbSet<Reserva> Reservas { get; set; }

    public virtual DbSet<TipoDocumento> TipoDocumentos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=ConsorcioGest;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CausaProblema>(entity =>
        {
            entity.ToTable("CAUSA_PROBLEMA");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");
            entity.Property(e => e.Observacion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("OBSERVACION");
        });

        modelBuilder.Entity<Condominio>(entity =>
        {
            entity.ToTable("CONDOMINIO");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IdConsorcio).HasColumnName("ID_CONSORCIO");
            entity.Property(e => e.NumeroDepartamento)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NUMERO_DEPARTAMENTO");
            entity.Property(e => e.Torre)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("TORRE");

            entity.HasOne(d => d.IdConsorcioNavigation).WithMany(p => p.Condominios)
                .HasForeignKey(d => d.IdConsorcio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CONDOMINIO_CONSORCIO");
        });

        modelBuilder.Entity<Consorcio>(entity =>
        {
            entity.ToTable("CONSORCIO");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Cuit)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CUIT");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");
            entity.Property(e => e.Ubicacion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UBICACION");
        });

        modelBuilder.Entity<ConsorcioUsuario>(entity =>
        {
            entity.ToTable("CONSORCIO_USUARIOS");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IdConsorcio).HasColumnName("ID_CONSORCIO");
            entity.Property(e => e.IdUsuario).HasColumnName("ID_USUARIO");

            entity.HasOne(d => d.IdConsorcioNavigation).WithMany(p => p.ConsorcioUsuarios)
                .HasForeignKey(d => d.IdConsorcio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CONSORCIOUSUARIO_CONSORCIO");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.ConsorcioUsuarios)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CONSORCIOUSUARIO_USUARIO");
        });

        modelBuilder.Entity<ConsortiumConfiguration>(entity =>
        {
            entity.ToTable("ConsortiumConfiguration");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CountDepartmentsByFloor)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.DeparmentConfiguration)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IdConsortium).HasColumnName("ID_CONSORTIUM");
            entity.Property(e => e.TowerName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdConsortiumNavigation).WithMany(p => p.ConsortiumConfigurations)
                .HasForeignKey(d => d.IdConsortium)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ConsortiumConfiguration_Consortium");
        });

        modelBuilder.Entity<Contacto>(entity =>
        {
            entity.ToTable("CONTACTO");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.IdConsorcio).HasColumnName("ID_CONSORCIO");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");
            entity.Property(e => e.Telefono)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TELEFONO");

            entity.HasOne(d => d.IdConsorcioNavigation).WithMany(p => p.Contactos)
                .HasForeignKey(d => d.IdConsorcio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CONTACTO_CONSORCIO");
        });

        modelBuilder.Entity<Encuesta>(entity =>
        {
            entity.ToTable("ENCUESTAS");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IdEstadoEncuesta).HasColumnName("ID_ESTADO_ENCUESTA");
            entity.Property(e => e.IdReclamo).HasColumnName("ID_RECLAMO");

            entity.HasOne(d => d.IdEstadoEncuestaNavigation).WithMany(p => p.Encuesta)
                .HasForeignKey(d => d.IdEstadoEncuesta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ENCUESTAS_ESTADOENCUESTAS");

            entity.HasOne(d => d.IdReclamoNavigation).WithMany(p => p.Encuesta)
                .HasForeignKey(d => d.IdReclamo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ENCUESTAS_RECLAMOS");
        });

        modelBuilder.Entity<EncuestasDetalle>(entity =>
        {
            entity.ToTable("ENCUESTAS_DETALLE");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Comentario)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("COMENTARIO");
            entity.Property(e => e.IdEncuesta).HasColumnName("ID_ENCUESTA");
            entity.Property(e => e.IdOpcion).HasColumnName("ID_OPCION");
            entity.Property(e => e.IdPregunta).HasColumnName("ID_PREGUNTA");

            entity.HasOne(d => d.IdEncuestaNavigation).WithMany(p => p.EncuestasDetalles)
                .HasForeignKey(d => d.IdEncuesta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ENCUESTASDETALLE_ENCUESTAS");

            entity.HasOne(d => d.IdOpcionNavigation).WithMany(p => p.EncuestasDetalles)
                .HasForeignKey(d => d.IdOpcion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ENCUESTASDETALLE_OPCION");

            entity.HasOne(d => d.IdPreguntaNavigation).WithMany(p => p.EncuestasDetalles)
                .HasForeignKey(d => d.IdPregunta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ENCUESTASDETALLE_PREGUNTA");
        });

        modelBuilder.Entity<EspacioAfectado>(entity =>
        {
            entity.ToTable("ESPACIO_AFECTADO");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");
            entity.Property(e => e.Observacion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("OBSERVACION");
        });

        modelBuilder.Entity<EspacioComun>(entity =>
        {
            entity.ToTable("ESPACIO_COMUN");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.HoraDesde)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("HORA_DESDE");
            entity.Property(e => e.HoraHasta)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("HORA_HASTA");
            entity.Property(e => e.IdConsorcio).HasColumnName("ID_CONSORCIO");
            entity.Property(e => e.Nombre)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");

            entity.HasOne(d => d.IdConsorcioNavigation).WithMany(p => p.EspacioComuns)
                .HasForeignKey(d => d.IdConsorcio)
                .HasConstraintName("FK_ESPACIOCOMUN_CONSORCIO");
        });

        modelBuilder.Entity<EstadoEncuestum>(entity =>
        {
            entity.ToTable("ESTADO_ENCUESTA");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");
            entity.Property(e => e.Observacion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("OBSERVACION");
        });

        modelBuilder.Entity<EstadoReclamo>(entity =>
        {
            entity.ToTable("ESTADO_RECLAMO");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");
            entity.Property(e => e.Observacion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("OBSERVACION");
        });

        modelBuilder.Entity<EstadoReserva>(entity =>
        {
            entity.ToTable("ESTADO_RESERVA");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");
            entity.Property(e => e.Observacion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("OBSERVACION");
        });

        modelBuilder.Entity<EstadoUsuario>(entity =>
        {
            entity.ToTable("ESTADO_USUARIO");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");
            entity.Property(e => e.Observacion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("OBSERVACION");
        });

        modelBuilder.Entity<Opcion>(entity =>
        {
            entity.ToTable("OPCION");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Opcion1)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("OPCION");
            entity.Property(e => e.ValorNumerico).HasColumnName("VALOR_NUMERICO");
        });

        modelBuilder.Entity<Perfil>(entity =>
        {
            entity.ToTable("PERFIL");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");
            entity.Property(e => e.Observacion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("OBSERVACION");
        });

        modelBuilder.Entity<Preguntum>(entity =>
        {
            entity.ToTable("PREGUNTA");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Pregunta)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("PREGUNTA");
        });

        modelBuilder.Entity<Reclamo>(entity =>
        {
            entity.ToTable("RECLAMOS");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Comentario)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("COMENTARIO");
            entity.Property(e => e.Fecha)
                .HasColumnType("date")
                .HasColumnName("FECHA");
            entity.Property(e => e.IdCausaProblema).HasColumnName("ID_CAUSA_PROBLEMA");
            entity.Property(e => e.IdEspacioAfectado).HasColumnName("ID_ESPACIO_AFECTADO");
            entity.Property(e => e.IdEstadoReclamo).HasColumnName("ID_ESTADO_RECLAMO");
            entity.Property(e => e.IdUsuario).HasColumnName("ID_USUARIO");
            entity.Property(e => e.NroReclamo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("NRO_RECLAMO");

            entity.HasOne(d => d.IdCausaProblemaNavigation).WithMany(p => p.Reclamos)
                .HasForeignKey(d => d.IdCausaProblema)
                .HasConstraintName("FK_RECLAMOS_CAUSAPROBLEMA");

            entity.HasOne(d => d.IdEspacioAfectadoNavigation).WithMany(p => p.Reclamos)
                .HasForeignKey(d => d.IdEspacioAfectado)
                .HasConstraintName("FK_RECLAMOS_ESPACIOAFECTADO");

            entity.HasOne(d => d.IdEstadoReclamoNavigation).WithMany(p => p.Reclamos)
                .HasForeignKey(d => d.IdEstadoReclamo)
                .HasConstraintName("FK_RECLAMOS_ESTADORECLAMO");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Reclamos)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK_RECLAMOS_USUARIOS");
        });

        modelBuilder.Entity<ReclamoDetalle>(entity =>
        {
            entity.ToTable("RECLAMO_DETALLE");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ESTADO");
            entity.Property(e => e.Fecha)
                .HasColumnType("date")
                .HasColumnName("FECHA");
            entity.Property(e => e.IdReclamo).HasColumnName("ID_RECLAMO");
            entity.Property(e => e.Observacion)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("OBSERVACION");

            entity.HasOne(d => d.IdReclamoNavigation).WithMany(p => p.ReclamoDetalles)
                .HasForeignKey(d => d.IdReclamo)
                .HasConstraintName("FK_RECLAMODETALLE_RECLAMOS");
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.ToTable("RESERVAS");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Fecha)
                .HasColumnType("date")
                .HasColumnName("FECHA");
            entity.Property(e => e.HoraDesde)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("HORA_DESDE");
            entity.Property(e => e.HoraHasta)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("HORA_HASTA");
            entity.Property(e => e.IdConsorcio).HasColumnName("ID_CONSORCIO");
            entity.Property(e => e.IdEspacioComun).HasColumnName("ID_ESPACIO_COMUN");
            entity.Property(e => e.IdEstadoReserva).HasColumnName("ID_ESTADO_RESERVA");
            entity.Property(e => e.IdUsuario).HasColumnName("ID_USUARIO");

            entity.HasOne(d => d.IdConsorcioNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdConsorcio)
                .HasConstraintName("FK_RESERVAS_CONSORCIO");

            entity.HasOne(d => d.IdEspacioComunNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdEspacioComun)
                .HasConstraintName("FK_RESERVAS_ESPACIOCOMUN");

            entity.HasOne(d => d.IdEstadoReservaNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdEstadoReserva)
                .HasConstraintName("FK_RESERVAS_ESTADORESERVA");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK_RESERVAS_USUARIOS");
        });

        modelBuilder.Entity<TipoDocumento>(entity =>
        {
            entity.ToTable("TIPO_DOCUMENTO");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");
            entity.Property(e => e.Observacion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("OBSERVACION");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.ToTable("USUARIOS");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Apellido)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("APELLIDO");
            entity.Property(e => e.Contrasenia)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CONTRASENIA");
            entity.Property(e => e.Documento).HasColumnName("DOCUMENTO");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Esinquilino)
                .HasDefaultValueSql("((0))")
                .HasColumnName("ESINQUILINO");
            entity.Property(e => e.Espropietario)
                .HasDefaultValueSql("((0))")
                .HasColumnName("ESPROPIETARIO");
            entity.Property(e => e.IdCondominio).HasColumnName("ID_CONDOMINIO");
            entity.Property(e => e.IdEstadoUsuario).HasColumnName("ID_ESTADO_USUARIO");
            entity.Property(e => e.IdPerfil).HasColumnName("ID_PERFIL");
            entity.Property(e => e.IdTipoDocumento).HasColumnName("ID_TIPO_DOCUMENTO");
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");
            entity.Property(e => e.Telefono)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TELEFONO");

            entity.HasOne(d => d.IdCondominioNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdCondominio)
                .HasConstraintName("FK_USUARIO_CONODMINIO");

            entity.HasOne(d => d.IdEstadoUsuarioNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdEstadoUsuario)
                .HasConstraintName("FK_USUARIO_ESTADO_USUARIO");

            entity.HasOne(d => d.IdPerfilNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdPerfil)
                .HasConstraintName("FK_USUARIO_PERFIL");

            entity.HasOne(d => d.IdTipoDocumentoNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdTipoDocumento)
                .HasConstraintName("FK_USUARIO_TIPO_DOCUMENTO");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
