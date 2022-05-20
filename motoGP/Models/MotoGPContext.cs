using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace motoGP.Models
{
    public partial class MotoGPContext : DbContext
    {
        public MotoGPContext()
        {
        }

        public MotoGPContext(DbContextOptions<MotoGPContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Championship> Championships { get; set; } = null!;
        public virtual DbSet<Pilot> Pilots { get; set; } = null!;
        public virtual DbSet<PilotRaceRe> PilotRaceRes { get; set; } = null!;
        public virtual DbSet<PilotYearInfo> PilotYearInfos { get; set; } = null!;
        public virtual DbSet<Race> Races { get; set; } = null!;
        public virtual DbSet<Team> Teams { get; set; } = null!;
        public virtual DbSet<TeamYearInfo> TeamYearInfos { get; set; } = null!;
        public virtual DbSet<TeamYearInfoPilot> TeamYearInfoPilots { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlite("Data Source=D:\\lab\\visualProgramming\\MotoGp1\\motoGP\\Assets\\MotoGP.sqlite3");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Championship>(entity =>
            {
                entity.HasKey(e => e.Year);

                entity.ToTable("Championship");

                entity.Property(e => e.Year).ValueGeneratedNever();
            });

            modelBuilder.Entity<Pilot>(entity =>
            {
                entity.ToTable("Pilot");

                entity.Property(e => e.PilotId)
                    .ValueGeneratedNever()
                    .HasColumnName("Pilot_id");

                entity.Property(e => e.BirthYear).HasColumnName("Birth year");
            });

            modelBuilder.Entity<PilotRaceRe>(entity =>
            {
                entity.HasKey(e => e.PilotRaceResId);

                entity.Property(e => e.PilotRaceResId)
                    .ValueGeneratedNever()
                    .HasColumnName("PilotRaceRes_id");

                entity.Property(e => e.PilotId).HasColumnName("Pilot_id");

                entity.Property(e => e.RaceId).HasColumnName("Race_id");

                entity.HasOne(d => d.Pilot)
                    .WithMany(p => p.PilotRaceRes)
                    .HasForeignKey(d => d.PilotId);

                entity.HasOne(d => d.Race)
                    .WithMany(p => p.PilotRaceRes)
                    .HasForeignKey(d => d.RaceId);
            });

            modelBuilder.Entity<PilotYearInfo>(entity =>
            {
                entity.HasKey(e => e.ResId);

                entity.ToTable("PilotYearInfo");

                entity.Property(e => e.ResId)
                    .ValueGeneratedNever()
                    .HasColumnName("Res_id");

                entity.Property(e => e.PilotId).HasColumnName("Pilot_id");

                entity.HasOne(d => d.Pilot)
                    .WithMany(p => p.PilotYearInfos)
                    .HasForeignKey(d => d.PilotId);
            });

            modelBuilder.Entity<Race>(entity =>
            {
                entity.ToTable("Race");

                entity.Property(e => e.RaceId)
                    .ValueGeneratedNever()
                    .HasColumnName("Race_id");

                entity.HasOne(d => d.YearNavigation)
                    .WithMany(p => p.Races)
                    .HasForeignKey(d => d.Year);
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.ToTable("Team");

                entity.Property(e => e.TeamId)
                    .ValueGeneratedNever()
                    .HasColumnName("Team_id");
            });

            modelBuilder.Entity<TeamYearInfo>(entity =>
            {
                entity.ToTable("TeamYearInfo");

                entity.Property(e => e.TeamYearInfoId)
                    .ValueGeneratedNever()
                    .HasColumnName("TeamYearInfo_id");

                entity.Property(e => e.TeamId).HasColumnName("Team_id");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.TeamYearInfos)
                    .HasForeignKey(d => d.TeamId);
            });

            modelBuilder.Entity<TeamYearInfoPilot>(entity =>
            {
                entity.HasKey(e => new { e.PilotId, e.TeamYearInfoId });

                entity.ToTable("TeamYearInfo_Pilot");

                entity.Property(e => e.PilotId).HasColumnName("Pilot_id");

                entity.Property(e => e.TeamYearInfoId).HasColumnName("TeamYearInfo_id");

                entity.HasOne(d => d.Pilot)
                    .WithMany(p => p.TeamYearInfoPilots)
                    .HasForeignKey(d => d.PilotId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
