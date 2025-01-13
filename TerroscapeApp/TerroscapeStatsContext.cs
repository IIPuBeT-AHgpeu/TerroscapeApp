using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TerroscapeApp;

public partial class TerroscapeStatsContext : DbContext
{
    public TerroscapeStatsContext()
    {
    }

    public TerroscapeStatsContext(DbContextOptions<TerroscapeStatsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Avatar> Avatars { get; set; }

    public virtual DbSet<Killer> Killers { get; set; }

    public virtual DbSet<Map> Maps { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=terroscape_stats;Username=postgres;Password=root");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum("game_name", new[] { "base", "feral_instincts", "amorphous_peril", "lethal_immortals", "putrefied_enmity" });

        modelBuilder.Entity<Avatar>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("survivor_pkey");

            entity.ToTable("avatar");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.FirstSkill)
                .HasMaxLength(50)
                .HasColumnName("first_skill");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
            entity.Property(e => e.SecondSkill)
                .HasMaxLength(50)
                .HasColumnName("second_skill");
        });

        modelBuilder.Entity<Killer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("killer_id_pk");

            entity.ToTable("killer");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
            entity.Property(e => e.Strength).HasColumnName("strength");
        });

        modelBuilder.Entity<Map>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("map_id_pk");

            entity.ToTable("map");

            entity.HasIndex(e => e.Name, "map_name_uq").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.LocationsNum).HasColumnName("locations_num");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("player_id_pk");

            entity.ToTable("player");

            entity.HasIndex(e => e.Login, "player_login_uq").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Login)
                .HasMaxLength(20)
                .HasColumnName("login");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
