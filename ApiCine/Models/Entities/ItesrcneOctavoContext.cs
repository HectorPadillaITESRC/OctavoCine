using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace ApiCine.Models.Entities;

public partial class ItesrcneOctavoContext : DbContext
{
    public ItesrcneOctavoContext()
    {
    }

    public ItesrcneOctavoContext(DbContextOptions<ItesrcneOctavoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Asientos> Asientos { get; set; }

    public virtual DbSet<Clientes> Clientes { get; set; }

    public virtual DbSet<Funciones> Funciones { get; set; }

    public virtual DbSet<Noticias> Noticias { get; set; }

    public virtual DbSet<Tickets> Tickets { get; set; }

    public virtual DbSet<Usuarios> Usuarios { get; set; }

  
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8_general_ci")
            .HasCharSet("utf8");

        modelBuilder.Entity<Asientos>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("asientos");

            entity.HasIndex(e => e.IdTicket, "asientos_tickets_FK");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.IdTicket).HasColumnType("int(11)");
            entity.Property(e => e.NumAsiento).HasColumnType("int(11)");

            entity.HasOne(d => d.IdTicketNavigation).WithMany(p => p.Asientos)
                .HasForeignKey(d => d.IdTicket)
                .HasConstraintName("asientos_tickets_FK");
        });

        modelBuilder.Entity<Clientes>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("clientes");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Contraseña).HasMaxLength(100);
            entity.Property(e => e.Nombre).HasMaxLength(150);
            entity.Property(e => e.Usuario).HasMaxLength(100);
        });

        modelBuilder.Entity<Funciones>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("funciones");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Horario).HasMaxLength(100);
            entity.Property(e => e.NombrePelicula).HasMaxLength(200);
            entity.Property(e => e.NumSala).HasColumnType("int(11)");
        });

        modelBuilder.Entity<Noticias>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("noticias");

            entity.HasIndex(e => e.AutorId, "autor_id");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.AutorId)
                .HasColumnType("int(11)")
                .HasColumnName("autor_id");
            entity.Property(e => e.Contenido)
                .HasColumnType("text")
                .HasColumnName("contenido");
            entity.Property(e => e.FechaPublicacion).HasColumnName("fecha_publicacion");
            entity.Property(e => e.ImagenUrl)
                .HasMaxLength(255)
                .HasColumnName("imagen_url");
            entity.Property(e => e.Titulo)
                .HasMaxLength(255)
                .HasColumnName("titulo");

            entity.HasOne(d => d.Autor).WithMany(p => p.Noticias)
                .HasForeignKey(d => d.AutorId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("noticias_ibfk_1");
        });

        modelBuilder.Entity<Tickets>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tickets");

            entity.HasIndex(e => e.IdCliente, "tickets_clientes_FK");

            entity.HasIndex(e => e.IdFuncion, "tickets_funciones_FK");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.IdCliente).HasColumnType("int(11)");
            entity.Property(e => e.IdFuncion).HasColumnType("int(11)");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tickets_clientes_FK");

            entity.HasOne(d => d.IdFuncionNavigation).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.IdFuncion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tickets_funciones_FK");
        });

        modelBuilder.Entity<Usuarios>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("usuarios");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Contraseña)
                .HasMaxLength(100)
                .HasColumnName("contraseña");
            entity.Property(e => e.EsAdmin)
                .HasDefaultValueSql("'0'")
                .HasColumnName("es_admin");
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .HasColumnName("nombre");
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(100)
                .HasColumnName("nombre_usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
