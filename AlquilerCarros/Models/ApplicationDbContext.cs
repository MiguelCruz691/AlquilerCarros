using System;
using System.Collections.Generic;
using AlquilerCarros.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace AlquilerCarros.Models;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Alquiler> Alquilers { get; set; }

    public DbSet<Carro> Carros { get; set; }

    public DbSet<Cliente> Clientes { get; set; }

    public DbSet<Pago> Pagos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Alquiler>(entity =>
        {
            entity.HasKey(e => e.IdAlquiler).HasName("PK__Alquiler__CB9A46B7570CD186");

            entity.ToTable("Alquiler");

            entity.Property(e => e.Saldo).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Tiempo).HasComputedColumnSql("(datediff(day,[FechaInicio],[FechaFin]))", false);

            entity.HasOne(d => d.IdCarroNavigation).WithMany(p => p.Alquilers)
                .HasForeignKey(d => d.IdCarro)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Alquiler__IdCarr__276EDEB3");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Alquilers)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Alquiler__IdClie__37A5467C");
        });

        modelBuilder.Entity<Carro>(entity =>
        {
            entity.HasKey(e => e.IdCarro).HasName("PK__Carro__6C9FE07BA28D1280");

            entity.ToTable("Carro");

            entity.HasIndex(e => e.Placa, "UQ__Carro__8310F99DAD8AC463").IsUnique();

            entity.Property(e => e.Marca).HasMaxLength(100);
            entity.Property(e => e.Modelo).HasMaxLength(75);
            entity.Property(e => e.Placa).HasMaxLength(6);
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__Cliente__D5946642F2E0E57A");

            entity.ToTable("Cliente");

            entity.Property(e => e.Cedula).HasMaxLength(15);
            entity.Property(e => e.Nombre).HasMaxLength(150);
            entity.Property(e => e.Telefono1).HasMaxLength(15);
            entity.Property(e => e.Telefono2).HasMaxLength(15);
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.IdPagos).HasName("PK__Pagos__04137C5BB493C4CE");

            entity.HasOne(d => d.IdAlquilerNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.IdAlquiler)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pagos__IdAlquile__34C8D9D1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    public async Task<List<ClienteCarroAlquilerDTO>> AlquilerCarro(DateTime fecha)
    {
        return await this.Set<ClienteCarroAlquilerDTO>()
            .FromSqlRaw("EXEC sp_BuscarAlquilerCarroCliente @Fecha",
                new SqlParameter("@Fecha", fecha)).ToListAsync();
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
