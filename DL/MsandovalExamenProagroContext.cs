using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DL;

public partial class MsandovalExamenProagroContext : DbContext
{
    public MsandovalExamenProagroContext()
    {
    }

    public MsandovalExamenProagroContext(DbContextOptions<MsandovalExamenProagroContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Estado> Estados { get; set; }

    public virtual DbSet<GeoReferencia> GeoReferencias { get; set; }

    public virtual DbSet<Permiso> Permisos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-G7UVJH5; Database= MSandovalExamenProagro; TrustServerCertificate=True;Trusted_Connection=True; User ID=sa; Password=pass@word1;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Estado>(entity =>
        {
            entity.HasKey(e => e.IdEstado).HasName("PK__Estado__FBB0EDC1A8365C29");

            entity.ToTable("Estado");

            entity.Property(e => e.NombreEstado)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Siglas)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<GeoReferencia>(entity =>
        {
            entity.HasKey(e => e.IdGeorreferencia).HasName("PK__GeoRefer__A624DB4945579EEF");

            entity.Property(e => e.Latitud)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Longitud)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.GeoReferencia)
                .HasForeignKey(d => d.IdEstado)
                .HasConstraintName("FK__GeoRefere__IdEst__145C0A3F");
        });

        modelBuilder.Entity<Permiso>(entity =>
        {
            entity.HasNoKey();

            entity.HasOne(d => d.IdEstadoNavigation).WithMany()
                .HasForeignKey(d => d.IdEstado)
                .HasConstraintName("FK__Permisos__IdEsta__35BCFE0A");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany()
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Permisos__IdUsua__34C8D9D1");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF9782282B2F");

            entity.ToTable("Usuario");

            entity.Property(e => e.FechaNacimiento).HasColumnType("date");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Rfc)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("RFC");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
