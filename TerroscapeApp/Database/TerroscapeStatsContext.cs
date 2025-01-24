using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TerroscapeApp.Models;

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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum("game_name_enum", ["base", "feral_instincts", "amorphous_peril", "lethal_immortals", "putrefied_enmity"])
            .HasPostgresEnum("win_enum", ["murder", "time", "escape", "police", "plan", "other"])
            .HasPostgresEnum("survivor_state_enum", ["alive", "injured", "dead"]);

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

            entity.Property(e => e.GameName)
                .HasColumnName("game_name")
                .HasColumnType("game_name_enum")
                .HasDefaultValue(DBEnums.GameNameEnum.Base);
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

            entity.Property(e => e.GameName)
                .HasColumnName("game_name")
                .HasColumnType("game_name_enum")
                .HasDefaultValue(DBEnums.GameNameEnum.Base);
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
            entity.Property(e => e.SurvivorBoostNum)
                .HasDefaultValue(0)
                .HasColumnName("survivor_boost_num");           



            entity.Property(e => e.FirstPlayerId).HasColumnName("first_survivor_player").IsRequired(true);
            entity.HasOne(d => d.FirstPlayer).WithMany(p => p.FirstSurvivorRounds)
                .HasForeignKey(d => d.FirstPlayerId)
                .HasConstraintName("round_first_player_fk")
                .OnDelete(DeleteBehavior.Cascade);
            entity.Property(a => a.FirstAvatarId).HasColumnName("first_survivor_avatar").IsRequired(true);
            entity.HasOne(d => d.FirstAvatar).WithMany(p => p.FirstSurvivorRounds)
                .HasForeignKey(d => d.FirstAvatarId)
                .HasConstraintName("round_first_avatar_fk")
                .OnDelete(DeleteBehavior.Cascade);
            entity.Property(s => s.FirstState)
                .HasColumnName("first_survivor_state")
                .HasColumnType("survivor_state_enum")
                .HasDefaultValue(DBEnums.SurvivorStateEnum.Alive);

            entity.Property(e => e.SecondPlayerId).HasColumnName("second_survivor_player").IsRequired(false);
            entity.HasOne(d => d.SecondPlayer).WithMany(p => p.SecondSurvivorRounds)
                .HasForeignKey(d => d.SecondPlayerId)
                .HasConstraintName("round_second_player_fk")
                .OnDelete(DeleteBehavior.Cascade);
            entity.Property(a => a.SecondAvatarId).HasColumnName("second_survivor_avatar").IsRequired(true);
            entity.HasOne(d => d.SecondAvatar).WithMany(p => p.SecondSurvivorRounds)
                .HasForeignKey(d => d.SecondAvatarId)
                .HasConstraintName("round_second_avatar_fk")
                .OnDelete(DeleteBehavior.Cascade);
            entity.Property(s => s.SecondState)
                .HasColumnName("second_survivor_state")
                .HasColumnType("survivor_state_enum")
                .HasDefaultValue(DBEnums.SurvivorStateEnum.Alive);

            entity.Property(e => e.ThirdPlayerId).HasColumnName("third_survivor_player").IsRequired(false);
            entity.HasOne(d => d.ThirdPlayer).WithMany(p => p.ThirdSurvivorRounds)
                .HasForeignKey(d => d.ThirdPlayerId)
                .HasConstraintName("round_third_player_fk")
                .OnDelete(DeleteBehavior.Cascade);
            entity.Property(a => a.ThirdAvatarId).HasColumnName("third_survivor_avatar").IsRequired(true);
            entity.HasOne(d => d.ThirdAvatar).WithMany(p => p.ThirdSurvivorRounds)
                .HasForeignKey(d => d.ThirdAvatarId)
                .HasConstraintName("round_third_avatar_fk")
                .OnDelete(DeleteBehavior.Cascade);
            entity.Property(s => s.ThirdState)
                .HasColumnName("third_survivor_state")
                .HasColumnType("survivor_state_enum")
                .HasDefaultValue(DBEnums.SurvivorStateEnum.Alive);

            entity.Property(d => d.Date).HasColumnName("date").IsRequired(false);

            entity.HasOne(d => d.Killer).WithMany(p => p.Rounds)
                .HasForeignKey(d => d.KillerId)
                .HasConstraintName("round_killer_fk");

            entity.HasOne(d => d.KillerPlayer).WithMany(p => p.KillerRounds)
                .HasForeignKey(d => d.KillerPlayerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("round_killer_player_fk");

            entity.HasOne(d => d.Map).WithMany(p => p.Rounds)
                .HasForeignKey(d => d.MapId)
                .HasConstraintName("round_map_fk");            

            entity.Property(e => e.WinWay)
                .HasColumnName("win_way")
                .HasColumnType("win_enum")
                .HasDefaultValue(DBEnums.WinEnum.Murder);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
