using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TerroscapeApp.Database;

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

    public virtual DbSet<Round> Rounds { get; set; }

    public virtual DbSet<Survivor> Survivors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=terroscape_stats;Username=postgres;Password=root");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum("game_name", new[] { "base", "feral_instincts", "amorphous_peril", "lethal_immortals", "putrefied_enmity" })
            .HasPostgresEnum("killer_win", new[] { "murder", "time", "other" })
            .HasPostgresEnum("survivor_state", new[] { "alive", "injured", "dead" })
            .HasPostgresEnum("survivor_win", new[] { "escape", "police", "plan", "other" });

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

        modelBuilder.Entity<Round>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("round_id_pk");

            entity.ToTable("round");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.DonePlan).HasColumnName("done_plan");
            entity.Property(e => e.DoneRadio)
                .HasDefaultValue(false)
                .HasColumnName("done_radio");
            entity.Property(e => e.FirstSurvivor).HasColumnName("first_survivor");
            entity.Property(e => e.GotKeys)
                .HasDefaultValue(false)
                .HasColumnName("got_keys");
            entity.Property(e => e.HasPlans)
                .HasDefaultValue(false)
                .HasColumnName("has_plans");
            entity.Property(e => e.KillerBoostNum)
                .HasDefaultValue(0)
                .HasColumnName("killer_boost_num");
            entity.Property(e => e.KillerId).HasColumnName("killer_id");
            entity.Property(e => e.KillerLevel)
                .HasDefaultValue(1)
                .HasColumnName("killer_level");
            entity.Property(e => e.KillerPlayerId).HasColumnName("killer_player_id");
            entity.Property(e => e.KillerWin).HasColumnName("killer_win");
            entity.Property(e => e.MapId).HasColumnName("map_id");
            entity.Property(e => e.SecondSurvivor).HasColumnName("second_survivor");
            entity.Property(e => e.SurvivorBoostNum)
                .HasDefaultValue(0)
                .HasColumnName("survivor_boost_num");
            entity.Property(e => e.ThirdSurvivor).HasColumnName("third_survivor");

            entity.HasOne(d => d.FirstSurvivorNavigation).WithMany(p => p.RoundFirstSurvivorNavigations)
                .HasForeignKey(d => d.FirstSurvivor)
                .HasConstraintName("round_first_survivor_fk");

            entity.HasOne(d => d.Killer).WithMany(p => p.Rounds)
                .HasForeignKey(d => d.KillerId)
                .HasConstraintName("round_killer_fk");

            entity.HasOne(d => d.KillerPlayer).WithMany(p => p.Rounds)
                .HasForeignKey(d => d.KillerPlayerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("round_killer_player_fk");

            entity.HasOne(d => d.Map).WithMany(p => p.Rounds)
                .HasForeignKey(d => d.MapId)
                .HasConstraintName("round_map_fk");

            entity.HasOne(d => d.SecondSurvivorNavigation).WithMany(p => p.RoundSecondSurvivorNavigations)
                .HasForeignKey(d => d.SecondSurvivor)
                .HasConstraintName("round_second_survivor_fk");

            entity.HasOne(d => d.ThirdSurvivorNavigation).WithMany(p => p.RoundThirdSurvivorNavigations)
                .HasForeignKey(d => d.ThirdSurvivor)
                .HasConstraintName("round_third_survivor_fk");
        });

        modelBuilder.Entity<Survivor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("survivor_id_pk");

            entity.ToTable("survivor");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.AvatarId).HasColumnName("avatar_id");
            entity.Property(e => e.PlayerId).HasColumnName("player_id");

            entity.HasOne(d => d.Avatar).WithMany(p => p.Survivors)
                .HasForeignKey(d => d.AvatarId)
                .HasConstraintName("survivor_avatar_fk");

            entity.HasOne(d => d.Player).WithMany(p => p.Survivors)
                .HasForeignKey(d => d.PlayerId)
                .HasConstraintName("survivor_player_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
