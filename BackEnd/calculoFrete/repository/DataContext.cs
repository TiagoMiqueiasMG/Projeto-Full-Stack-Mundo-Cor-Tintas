using Microsoft.EntityFrameworkCore;
using model;

namespace repository;

public partial class DataContext : DbContext
{
    public virtual DbSet<Frete> Fretes { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=localhost,1433;Database=calculoFrete;User ID=sa;Password=tmiq!2021;Trusted_Connection=False; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Frete>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("Frete");

            entity.Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

            entity.Property(e => e.Cep)
                .HasMaxLength(8)
                .IsUnicode(false);

            entity.Property(e => e.DataEntrega)
            .HasColumnType("datetime");

            entity.Property(e => e.NomeCliente)
                .HasMaxLength(120)
                .IsUnicode(false);

            entity.Property(e => e.Rua)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.ValorCompra)
            .HasColumnType("decimal(18, 0)");

            entity.Property(e => e.Distancia)
            .HasColumnType("decimal(18, 0)");

            entity.Property(e => e.ValorFrete)
            .HasColumnType("decimal(18, 0)");

            entity.Property(e => e.ValorKm)
            .HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Fretes)
                .HasForeignKey(d => d.Usuarioid)
                .HasConstraintName("FK_Frete_Usuario");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("Usuario");

            entity.Property(e => e.Id);

            entity.Property(e => e.Nome)
                .HasMaxLength(120)
                .IsUnicode(false);

            entity.Property(e => e.Email)
                .HasMaxLength(120)
                .IsUnicode(false);

            entity.Property(e => e.UId)
                .HasMaxLength(120)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
