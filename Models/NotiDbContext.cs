using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NotiApp.Models;

public partial class NotiDbContext : DbContext
{
    public NotiDbContext()
    {
    }

    public NotiDbContext(DbContextOptions<NotiDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Nota> Nota { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
/*#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-2UPJBBG; DataBase=NotiDB; Trusted_Connection=True; TrustServerCertificate=True;");*/

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Nota>(entity =>
        {
            entity.HasKey(e => e.IdNota).HasName("PK__Nota__4B2ACFF2CD84B9E2");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.TituloNota)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Nota)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("fk_Usuario");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF97B53849EF");

            entity.ToTable("Usuario");

            entity.Property(e => e.Clave)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
