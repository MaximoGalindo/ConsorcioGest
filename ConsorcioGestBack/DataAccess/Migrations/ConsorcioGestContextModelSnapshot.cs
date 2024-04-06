﻿// <auto-generated />
using System;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccess.Migrations
{
    [DbContext(typeof(ConsorcioGestContext))]
    partial class ConsorcioGestContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DataAccess.Data.Models.CausaProblema", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("NOMBRE");

                    b.Property<string>("Observacion")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("OBSERVACION");

                    b.HasKey("Id");

                    b.ToTable("CAUSA_PROBLEMA", (string)null);
                });

            modelBuilder.Entity("DataAccess.Data.Models.Condominio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Departamento")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("DEPARTAMENTO");

                    b.Property<int>("IdConsorcio")
                        .HasColumnType("int")
                        .HasColumnName("ID_CONSORCIO");

                    b.Property<string>("Piso")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("PISO");

                    b.Property<string>("Torre")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("TORRE");

                    b.HasKey("Id");

                    b.HasIndex("IdConsorcio");

                    b.ToTable("CONDOMINIO", (string)null);
                });

            modelBuilder.Entity("DataAccess.Data.Models.Consorcio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Cuit")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("CUIT");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("NOMBRE");

                    b.Property<string>("Ubicacion")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("UBICACION");

                    b.HasKey("Id");

                    b.ToTable("CONSORCIO", (string)null);
                });

            modelBuilder.Entity("DataAccess.Data.Models.ConsorcioUsuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("IdConsorcio")
                        .HasColumnType("int")
                        .HasColumnName("ID_CONSORCIO");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int")
                        .HasColumnName("ID_USUARIO");

                    b.HasKey("Id");

                    b.HasIndex("IdConsorcio");

                    b.HasIndex("IdUsuario");

                    b.ToTable("CONSORCIO_USUARIOS", (string)null);
                });

            modelBuilder.Entity("DataAccess.Data.Models.Contacto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("EMAIL");

                    b.Property<int>("IdConsorcio")
                        .HasColumnType("int")
                        .HasColumnName("ID_CONSORCIO");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("NOMBRE");

                    b.Property<string>("Telefono")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("TELEFONO");

                    b.HasKey("Id");

                    b.HasIndex("IdConsorcio");

                    b.ToTable("CONTACTO", (string)null);
                });

            modelBuilder.Entity("DataAccess.Data.Models.Encuesta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("IdEstadoEncuesta")
                        .HasColumnType("int")
                        .HasColumnName("ID_ESTADO_ENCUESTA");

                    b.Property<int>("IdReclamo")
                        .HasColumnType("int")
                        .HasColumnName("ID_RECLAMO");

                    b.HasKey("Id");

                    b.HasIndex("IdEstadoEncuesta");

                    b.HasIndex("IdReclamo");

                    b.ToTable("ENCUESTAS", (string)null);
                });

            modelBuilder.Entity("DataAccess.Data.Models.EncuestasDetalle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Comentario")
                        .HasMaxLength(500)
                        .IsUnicode(false)
                        .HasColumnType("varchar(500)")
                        .HasColumnName("COMENTARIO");

                    b.Property<int>("IdEncuesta")
                        .HasColumnType("int")
                        .HasColumnName("ID_ENCUESTA");

                    b.Property<int>("IdOpcion")
                        .HasColumnType("int")
                        .HasColumnName("ID_OPCION");

                    b.Property<int>("IdPregunta")
                        .HasColumnType("int")
                        .HasColumnName("ID_PREGUNTA");

                    b.HasKey("Id");

                    b.HasIndex("IdEncuesta");

                    b.HasIndex("IdOpcion");

                    b.HasIndex("IdPregunta");

                    b.ToTable("ENCUESTAS_DETALLE", (string)null);
                });

            modelBuilder.Entity("DataAccess.Data.Models.EspacioAfectado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("NOMBRE");

                    b.Property<string>("Observacion")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("OBSERVACION");

                    b.HasKey("Id");

                    b.ToTable("ESPACIO_AFECTADO", (string)null);
                });

            modelBuilder.Entity("DataAccess.Data.Models.EspacioComun", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("HoraDesde")
                        .HasMaxLength(5)
                        .IsUnicode(false)
                        .HasColumnType("varchar(5)")
                        .HasColumnName("HORA_DESDE");

                    b.Property<string>("HoraHasta")
                        .HasMaxLength(5)
                        .IsUnicode(false)
                        .HasColumnType("varchar(5)")
                        .HasColumnName("HORA_HASTA");

                    b.Property<int?>("IdConsorcio")
                        .HasColumnType("int")
                        .HasColumnName("ID_CONSORCIO");

                    b.Property<string>("Nombre")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("NOMBRE");

                    b.HasKey("Id");

                    b.HasIndex("IdConsorcio");

                    b.ToTable("ESPACIO_COMUN", (string)null);
                });

            modelBuilder.Entity("DataAccess.Data.Models.EstadoEncuestum", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("NOMBRE");

                    b.Property<string>("Observacion")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("OBSERVACION");

                    b.HasKey("Id");

                    b.ToTable("ESTADO_ENCUESTA", (string)null);
                });

            modelBuilder.Entity("DataAccess.Data.Models.EstadoReclamo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("NOMBRE");

                    b.Property<string>("Observacion")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("OBSERVACION");

                    b.HasKey("Id");

                    b.ToTable("ESTADO_RECLAMO", (string)null);
                });

            modelBuilder.Entity("DataAccess.Data.Models.EstadoReserva", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("NOMBRE");

                    b.Property<string>("Observacion")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("OBSERVACION");

                    b.HasKey("Id");

                    b.ToTable("ESTADO_RESERVA", (string)null);
                });

            modelBuilder.Entity("DataAccess.Data.Models.EstadoUsuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("NOMBRE");

                    b.Property<string>("Observacion")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("OBSERVACION");

                    b.HasKey("Id");

                    b.ToTable("ESTADO_USUARIO", (string)null);
                });

            modelBuilder.Entity("DataAccess.Data.Models.Opcion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Opcion1")
                        .IsRequired()
                        .HasMaxLength(500)
                        .IsUnicode(false)
                        .HasColumnType("varchar(500)")
                        .HasColumnName("OPCION");

                    b.Property<int?>("ValorNumerico")
                        .HasColumnType("int")
                        .HasColumnName("VALOR_NUMERICO");

                    b.HasKey("Id");

                    b.ToTable("OPCION", (string)null);
                });

            modelBuilder.Entity("DataAccess.Data.Models.Perfil", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("NOMBRE");

                    b.Property<string>("Observacion")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("OBSERVACION");

                    b.HasKey("Id");

                    b.ToTable("PERFIL", (string)null);
                });

            modelBuilder.Entity("DataAccess.Data.Models.Preguntum", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Pregunta")
                        .IsRequired()
                        .HasMaxLength(500)
                        .IsUnicode(false)
                        .HasColumnType("varchar(500)")
                        .HasColumnName("PREGUNTA");

                    b.HasKey("Id");

                    b.ToTable("PREGUNTA", (string)null);
                });

            modelBuilder.Entity("DataAccess.Data.Models.Reclamo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Comentario")
                        .HasMaxLength(2000)
                        .IsUnicode(false)
                        .HasColumnType("varchar(2000)")
                        .HasColumnName("COMENTARIO");

                    b.Property<DateTime?>("Fecha")
                        .HasColumnType("date")
                        .HasColumnName("FECHA");

                    b.Property<int?>("IdCausaProblema")
                        .HasColumnType("int")
                        .HasColumnName("ID_CAUSA_PROBLEMA");

                    b.Property<int?>("IdEspacioAfectado")
                        .HasColumnType("int")
                        .HasColumnName("ID_ESPACIO_AFECTADO");

                    b.Property<int?>("IdEstadoReclamo")
                        .HasColumnType("int")
                        .HasColumnName("ID_ESTADO_RECLAMO");

                    b.Property<int?>("IdUsuario")
                        .HasColumnType("int")
                        .HasColumnName("ID_USUARIO");

                    b.Property<string>("NroReclamo")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("NRO_RECLAMO");

                    b.HasKey("Id");

                    b.HasIndex("IdCausaProblema");

                    b.HasIndex("IdEspacioAfectado");

                    b.HasIndex("IdEstadoReclamo");

                    b.HasIndex("IdUsuario");

                    b.ToTable("RECLAMOS", (string)null);
                });

            modelBuilder.Entity("DataAccess.Data.Models.ReclamoDetalle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Estado")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("ESTADO");

                    b.Property<DateTime?>("Fecha")
                        .HasColumnType("date")
                        .HasColumnName("FECHA");

                    b.Property<int?>("IdReclamo")
                        .HasColumnType("int")
                        .HasColumnName("ID_RECLAMO");

                    b.Property<string>("Observacion")
                        .HasMaxLength(500)
                        .IsUnicode(false)
                        .HasColumnType("varchar(500)")
                        .HasColumnName("OBSERVACION");

                    b.HasKey("Id");

                    b.HasIndex("IdReclamo");

                    b.ToTable("RECLAMO_DETALLE", (string)null);
                });

            modelBuilder.Entity("DataAccess.Data.Models.Reserva", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("Fecha")
                        .HasColumnType("date")
                        .HasColumnName("FECHA");

                    b.Property<string>("HoraDesde")
                        .HasMaxLength(5)
                        .IsUnicode(false)
                        .HasColumnType("varchar(5)")
                        .HasColumnName("HORA_DESDE");

                    b.Property<string>("HoraHasta")
                        .HasMaxLength(5)
                        .IsUnicode(false)
                        .HasColumnType("varchar(5)")
                        .HasColumnName("HORA_HASTA");

                    b.Property<int?>("IdConsorcio")
                        .HasColumnType("int")
                        .HasColumnName("ID_CONSORCIO");

                    b.Property<int?>("IdEspacioComun")
                        .HasColumnType("int")
                        .HasColumnName("ID_ESPACIO_COMUN");

                    b.Property<int?>("IdEstadoReserva")
                        .HasColumnType("int")
                        .HasColumnName("ID_ESTADO_RESERVA");

                    b.Property<int?>("IdUsuario")
                        .HasColumnType("int")
                        .HasColumnName("ID_USUARIO");

                    b.HasKey("Id");

                    b.HasIndex("IdConsorcio");

                    b.HasIndex("IdEspacioComun");

                    b.HasIndex("IdEstadoReserva");

                    b.HasIndex("IdUsuario");

                    b.ToTable("RESERVAS", (string)null);
                });

            modelBuilder.Entity("DataAccess.Data.Models.TipoDocumento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("NOMBRE");

                    b.Property<string>("Observacion")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("OBSERVACION");

                    b.HasKey("Id");

                    b.ToTable("TIPO_DOCUMENTO", (string)null);
                });

            modelBuilder.Entity("DataAccess.Data.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Apellido")
                        .HasMaxLength(150)
                        .IsUnicode(false)
                        .HasColumnType("varchar(150)")
                        .HasColumnName("APELLIDO");

                    b.Property<string>("Contrasenia")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("CONTRASENIA");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("EMAIL");

                    b.Property<bool>("Esinquilino")
                        .HasColumnType("bit")
                        .HasColumnName("ESINQUILINO");

                    b.Property<bool>("Espropietario")
                        .HasColumnType("bit")
                        .HasColumnName("ESPROPIETARIO");

                    b.Property<int>("IdCondominio")
                        .HasColumnType("int")
                        .HasColumnName("ID_CONDOMINIO");

                    b.Property<int>("IdEstadoUsuario")
                        .HasColumnType("int")
                        .HasColumnName("ID_ESTADO_USUARIO");

                    b.Property<int>("IdPerfil")
                        .HasColumnType("int")
                        .HasColumnName("ID_PERFIL");

                    b.Property<int>("IdTipoDocumento")
                        .HasColumnType("int")
                        .HasColumnName("ID_TIPO_DOCUMENTO");

                    b.Property<string>("Nombre")
                        .HasMaxLength(150)
                        .IsUnicode(false)
                        .HasColumnType("varchar(150)")
                        .HasColumnName("NOMBRE");

                    b.Property<string>("Telefono")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("TELEFONO");

                    b.HasKey("Id");

                    b.HasIndex("IdCondominio");

                    b.HasIndex("IdEstadoUsuario");

                    b.HasIndex("IdPerfil");

                    b.HasIndex("IdTipoDocumento");

                    b.ToTable("USUARIOS", (string)null);
                });

            modelBuilder.Entity("DataAccess.Data.Models.Condominio", b =>
                {
                    b.HasOne("DataAccess.Data.Models.Consorcio", "IdConsorcioNavigation")
                        .WithMany("Condominios")
                        .HasForeignKey("IdConsorcio")
                        .IsRequired()
                        .HasConstraintName("FK_CONDOMINIO_CONSORCIO");

                    b.Navigation("IdConsorcioNavigation");
                });

            modelBuilder.Entity("DataAccess.Data.Models.ConsorcioUsuario", b =>
                {
                    b.HasOne("DataAccess.Data.Models.Consorcio", "IdConsorcioNavigation")
                        .WithMany("ConsorcioUsuarios")
                        .HasForeignKey("IdConsorcio")
                        .IsRequired()
                        .HasConstraintName("FK_CONSORCIOUSUARIO_CONSORCIO");

                    b.HasOne("DataAccess.Data.Models.Usuario", "IdUsuarioNavigation")
                        .WithMany("ConsorcioUsuarios")
                        .HasForeignKey("IdUsuario")
                        .IsRequired()
                        .HasConstraintName("FK_CONSORCIOUSUARIO_USUARIO");

                    b.Navigation("IdConsorcioNavigation");

                    b.Navigation("IdUsuarioNavigation");
                });

            modelBuilder.Entity("DataAccess.Data.Models.Contacto", b =>
                {
                    b.HasOne("DataAccess.Data.Models.Consorcio", "IdConsorcioNavigation")
                        .WithMany("Contactos")
                        .HasForeignKey("IdConsorcio")
                        .IsRequired()
                        .HasConstraintName("FK_CONTACTO_CONSORCIO");

                    b.Navigation("IdConsorcioNavigation");
                });

            modelBuilder.Entity("DataAccess.Data.Models.Encuesta", b =>
                {
                    b.HasOne("DataAccess.Data.Models.EstadoEncuestum", "IdEstadoEncuestaNavigation")
                        .WithMany("Encuesta")
                        .HasForeignKey("IdEstadoEncuesta")
                        .IsRequired()
                        .HasConstraintName("FK_ENCUESTAS_ESTADOENCUESTAS");

                    b.HasOne("DataAccess.Data.Models.Reclamo", "IdReclamoNavigation")
                        .WithMany("Encuesta")
                        .HasForeignKey("IdReclamo")
                        .IsRequired()
                        .HasConstraintName("FK_ENCUESTAS_RECLAMOS");

                    b.Navigation("IdEstadoEncuestaNavigation");

                    b.Navigation("IdReclamoNavigation");
                });

            modelBuilder.Entity("DataAccess.Data.Models.EncuestasDetalle", b =>
                {
                    b.HasOne("DataAccess.Data.Models.Encuesta", "IdEncuestaNavigation")
                        .WithMany("EncuestasDetalles")
                        .HasForeignKey("IdEncuesta")
                        .IsRequired()
                        .HasConstraintName("FK_ENCUESTASDETALLE_ENCUESTAS");

                    b.HasOne("DataAccess.Data.Models.Opcion", "IdOpcionNavigation")
                        .WithMany("EncuestasDetalles")
                        .HasForeignKey("IdOpcion")
                        .IsRequired()
                        .HasConstraintName("FK_ENCUESTASDETALLE_OPCION");

                    b.HasOne("DataAccess.Data.Models.Preguntum", "IdPreguntaNavigation")
                        .WithMany("EncuestasDetalles")
                        .HasForeignKey("IdPregunta")
                        .IsRequired()
                        .HasConstraintName("FK_ENCUESTASDETALLE_PREGUNTA");

                    b.Navigation("IdEncuestaNavigation");

                    b.Navigation("IdOpcionNavigation");

                    b.Navigation("IdPreguntaNavigation");
                });

            modelBuilder.Entity("DataAccess.Data.Models.EspacioComun", b =>
                {
                    b.HasOne("DataAccess.Data.Models.Consorcio", "IdConsorcioNavigation")
                        .WithMany("EspacioComuns")
                        .HasForeignKey("IdConsorcio")
                        .HasConstraintName("FK_ESPACIOCOMUN_CONSORCIO");

                    b.Navigation("IdConsorcioNavigation");
                });

            modelBuilder.Entity("DataAccess.Data.Models.Reclamo", b =>
                {
                    b.HasOne("DataAccess.Data.Models.CausaProblema", "IdCausaProblemaNavigation")
                        .WithMany("Reclamos")
                        .HasForeignKey("IdCausaProblema")
                        .HasConstraintName("FK_RECLAMOS_CAUSAPROBLEMA");

                    b.HasOne("DataAccess.Data.Models.EspacioAfectado", "IdEspacioAfectadoNavigation")
                        .WithMany("Reclamos")
                        .HasForeignKey("IdEspacioAfectado")
                        .HasConstraintName("FK_RECLAMOS_ESPACIOAFECTADO");

                    b.HasOne("DataAccess.Data.Models.EstadoReclamo", "IdEstadoReclamoNavigation")
                        .WithMany("Reclamos")
                        .HasForeignKey("IdEstadoReclamo")
                        .HasConstraintName("FK_RECLAMOS_ESTADORECLAMO");

                    b.HasOne("DataAccess.Data.Models.Usuario", "IdUsuarioNavigation")
                        .WithMany("Reclamos")
                        .HasForeignKey("IdUsuario")
                        .HasConstraintName("FK_RECLAMOS_USUARIOS");

                    b.Navigation("IdCausaProblemaNavigation");

                    b.Navigation("IdEspacioAfectadoNavigation");

                    b.Navigation("IdEstadoReclamoNavigation");

                    b.Navigation("IdUsuarioNavigation");
                });

            modelBuilder.Entity("DataAccess.Data.Models.ReclamoDetalle", b =>
                {
                    b.HasOne("DataAccess.Data.Models.Reclamo", "IdReclamoNavigation")
                        .WithMany("ReclamoDetalles")
                        .HasForeignKey("IdReclamo")
                        .HasConstraintName("FK_RECLAMODETALLE_RECLAMOS");

                    b.Navigation("IdReclamoNavigation");
                });

            modelBuilder.Entity("DataAccess.Data.Models.Reserva", b =>
                {
                    b.HasOne("DataAccess.Data.Models.Consorcio", "IdConsorcioNavigation")
                        .WithMany("Reservas")
                        .HasForeignKey("IdConsorcio")
                        .HasConstraintName("FK_RESERVAS_CONSORCIO");

                    b.HasOne("DataAccess.Data.Models.EspacioComun", "IdEspacioComunNavigation")
                        .WithMany("Reservas")
                        .HasForeignKey("IdEspacioComun")
                        .HasConstraintName("FK_RESERVAS_ESPACIOCOMUN");

                    b.HasOne("DataAccess.Data.Models.EstadoReserva", "IdEstadoReservaNavigation")
                        .WithMany("Reservas")
                        .HasForeignKey("IdEstadoReserva")
                        .HasConstraintName("FK_RESERVAS_ESTADORESERVA");

                    b.HasOne("DataAccess.Data.Models.Usuario", "IdUsuarioNavigation")
                        .WithMany("Reservas")
                        .HasForeignKey("IdUsuario")
                        .HasConstraintName("FK_RESERVAS_USUARIOS");

                    b.Navigation("IdConsorcioNavigation");

                    b.Navigation("IdEspacioComunNavigation");

                    b.Navigation("IdEstadoReservaNavigation");

                    b.Navigation("IdUsuarioNavigation");
                });

            modelBuilder.Entity("DataAccess.Data.Models.Usuario", b =>
                {
                    b.HasOne("DataAccess.Data.Models.Condominio", "IdCondominioNavigation")
                        .WithMany("Usuarios")
                        .HasForeignKey("IdCondominio")
                        .IsRequired()
                        .HasConstraintName("FK_USUARIO_CONODMINIO");

                    b.HasOne("DataAccess.Data.Models.EstadoUsuario", "IdEstadoUsuarioNavigation")
                        .WithMany("Usuarios")
                        .HasForeignKey("IdEstadoUsuario")
                        .IsRequired()
                        .HasConstraintName("FK_USUARIO_ESTADO_USUARIO");

                    b.HasOne("DataAccess.Data.Models.Perfil", "IdPerfilNavigation")
                        .WithMany("Usuarios")
                        .HasForeignKey("IdPerfil")
                        .IsRequired()
                        .HasConstraintName("FK_USUARIO_PERFIL");

                    b.HasOne("DataAccess.Data.Models.TipoDocumento", "IdTipoDocumentoNavigation")
                        .WithMany("Usuarios")
                        .HasForeignKey("IdTipoDocumento")
                        .IsRequired()
                        .HasConstraintName("FK_USUARIO_TIPO_DOCUMENTO");

                    b.Navigation("IdCondominioNavigation");

                    b.Navigation("IdEstadoUsuarioNavigation");

                    b.Navigation("IdPerfilNavigation");

                    b.Navigation("IdTipoDocumentoNavigation");
                });

            modelBuilder.Entity("DataAccess.Data.Models.CausaProblema", b =>
                {
                    b.Navigation("Reclamos");
                });

            modelBuilder.Entity("DataAccess.Data.Models.Condominio", b =>
                {
                    b.Navigation("Usuarios");
                });

            modelBuilder.Entity("DataAccess.Data.Models.Consorcio", b =>
                {
                    b.Navigation("Condominios");

                    b.Navigation("ConsorcioUsuarios");

                    b.Navigation("Contactos");

                    b.Navigation("EspacioComuns");

                    b.Navigation("Reservas");
                });

            modelBuilder.Entity("DataAccess.Data.Models.Encuesta", b =>
                {
                    b.Navigation("EncuestasDetalles");
                });

            modelBuilder.Entity("DataAccess.Data.Models.EspacioAfectado", b =>
                {
                    b.Navigation("Reclamos");
                });

            modelBuilder.Entity("DataAccess.Data.Models.EspacioComun", b =>
                {
                    b.Navigation("Reservas");
                });

            modelBuilder.Entity("DataAccess.Data.Models.EstadoEncuestum", b =>
                {
                    b.Navigation("Encuesta");
                });

            modelBuilder.Entity("DataAccess.Data.Models.EstadoReclamo", b =>
                {
                    b.Navigation("Reclamos");
                });

            modelBuilder.Entity("DataAccess.Data.Models.EstadoReserva", b =>
                {
                    b.Navigation("Reservas");
                });

            modelBuilder.Entity("DataAccess.Data.Models.EstadoUsuario", b =>
                {
                    b.Navigation("Usuarios");
                });

            modelBuilder.Entity("DataAccess.Data.Models.Opcion", b =>
                {
                    b.Navigation("EncuestasDetalles");
                });

            modelBuilder.Entity("DataAccess.Data.Models.Perfil", b =>
                {
                    b.Navigation("Usuarios");
                });

            modelBuilder.Entity("DataAccess.Data.Models.Preguntum", b =>
                {
                    b.Navigation("EncuestasDetalles");
                });

            modelBuilder.Entity("DataAccess.Data.Models.Reclamo", b =>
                {
                    b.Navigation("Encuesta");

                    b.Navigation("ReclamoDetalles");
                });

            modelBuilder.Entity("DataAccess.Data.Models.TipoDocumento", b =>
                {
                    b.Navigation("Usuarios");
                });

            modelBuilder.Entity("DataAccess.Data.Models.Usuario", b =>
                {
                    b.Navigation("ConsorcioUsuarios");

                    b.Navigation("Reclamos");

                    b.Navigation("Reservas");
                });
#pragma warning restore 612, 618
        }
    }
}
