using System;
using System.Collections.Generic;
using BankAPI.Data.BankModels;
using Microsoft.EntityFrameworkCore;

namespace BankAPI.Data;

public partial class BancoContext : DbContext
{
    public BancoContext()
    {
    }

    public BancoContext(DbContextOptions<BancoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Administrador> Administradors { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Cuentum> Cuenta { get; set; }

    public virtual DbSet<TipoCuentum> TipoCuenta { get; set; }

    public virtual DbSet<TipoTransaccion> TipoTransaccions { get; set; }

    public virtual DbSet<Transaccion> Transaccions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Administrador>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Administ__3213E83F663D9053");

            entity.ToTable("Administrador");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.NumeroTelefono)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("numeroTelefono");
            entity.Property(e => e.Passwrd)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("passwrd");
            entity.Property(e => e.Tipo)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("tipo");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__cliente__3213E83FA1F2B20E");

            entity.ToTable("cliente", tb => tb.HasTrigger("afterClienteCreado"));

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.NumeroTelefono)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("numeroTelefono");
            entity.Property(e => e.Passwd)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasDefaultValue("12345")
                .HasColumnName("passwd");
        });

        modelBuilder.Entity<Cuentum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cuenta__3213E83F77F1E265");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdCliente).HasColumnName("idCliente");
            entity.Property(e => e.Saldo)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("saldo");
            entity.Property(e => e.TipoCuenta).HasColumnName("tipoCuenta");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Cuenta)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cuenta__idClient__5629CD9C");

            entity.HasOne(d => d.TipoCuentaNavigation).WithMany(p => p.Cuenta)
                .HasForeignKey(d => d.TipoCuenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cuenta__tipoCuen__5535A963");
        });

        modelBuilder.Entity<TipoCuentum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TipoCuen__3213E83FCB47AED8");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<TipoTransaccion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TipoTran__3213E83F10BE88C6");

            entity.ToTable("TipoTransaccion");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Transaccion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Transacc__3213E83F924AD147");

            entity.ToTable("Transaccion");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cantidad)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("cantidad");
            entity.Property(e => e.CuentaExterna).HasColumnName("cuentaExterna");
            entity.Property(e => e.CuentaId).HasColumnName("cuentaID");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.TipoTransaccion).HasColumnName("tipoTransaccion");

            entity.HasOne(d => d.Cuenta).WithMany(p => p.Transaccions)
                .HasForeignKey(d => d.CuentaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Transacci__cuent__59FA5E80");

            entity.HasOne(d => d.TipoTransaccionNavigation).WithMany(p => p.Transaccions)
                .HasForeignKey(d => d.TipoTransaccion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Transacci__tipoT__5AEE82B9");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
