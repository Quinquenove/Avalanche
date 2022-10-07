using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Avalanche.Data
{
    public partial class snowboardingContext : DbContext
    {
        public snowboardingContext()
        {
        }

        public snowboardingContext(DbContextOptions<snowboardingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Berg> Bergs { get; set; } = null!;
        public virtual DbSet<Gebirge> Gebirges { get; set; } = null!;
        public virtual DbSet<Profi> Profis { get; set; } = null!;
        public virtual DbSet<Schwierigkeit> Schwierigkeits { get; set; } = null!;
        public virtual DbSet<Snowboarder> Snowboarders { get; set; } = null!;
        public virtual DbSet<Sponsor> Sponsors { get; set; } = null!;
        public virtual DbSet<Sponsoring> Sponsorings { get; set; } = null!;
        public virtual DbSet<Trick> Tricks { get; set; } = null!;
        public virtual DbSet<Vertragsart> Vertragsarts { get; set; } = null!;
        public virtual DbSet<Wettkampf> Wettkampfs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlite("DataSource=C:\\Users\\thorhauer\\Documents\\Schulprojekt\\Avalanche\\snowboarding.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Berg>(entity =>
            {
                entity.ToTable("Berg");

                entity.Property(e => e.Id)
                    .HasColumnType("INT")
                    .ValueGeneratedNever();

                entity.Property(e => e.GebirgeId)
                    .HasColumnType("INT")
                    .HasColumnName("Gebirge_Id");

                entity.Property(e => e.SchwierigkeitId)
                    .HasColumnType("INT")
                    .HasColumnName("Schwierigkeit_Id");

                entity.HasOne(d => d.Gebirge)
                    .WithMany(p => p.Bergs)
                    .HasForeignKey(d => d.GebirgeId);

                entity.HasOne(d => d.Schwierigkeit)
                    .WithMany(p => p.Bergs)
                    .HasForeignKey(d => d.SchwierigkeitId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Gebirge>(entity =>
            {
                entity.ToTable("Gebirge");

                entity.Property(e => e.Id)
                    .HasColumnType("INT")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name).HasColumnType("Text");
            });

            modelBuilder.Entity<Profi>(entity =>
            {
                entity.HasKey(e => e.Lizenznummer);

                entity.ToTable("Profi");

                entity.HasIndex(e => e.Mitgliedsnummer, "IX_Profi_Mitgliedsnummer")
                    .IsUnique();

                entity.Property(e => e.Lizenznummer).HasColumnType("Text");

                entity.Property(e => e.BestTrickId)
                    .HasColumnType("INT")
                    .HasColumnName("Best_Trick_Id");

                entity.Property(e => e.Mitgliedsnummer).HasColumnType("Text");

                entity.Property(e => e.Weltcuppunkte).HasColumnType("INT");

                entity.HasOne(d => d.BestTrick)
                    .WithMany(p => p.Profis)
                    .HasForeignKey(d => d.BestTrickId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(d => d.MitgliedsnummerNavigation)
                    .WithOne(p => p.Profi)
                    .HasForeignKey<Profi>(d => d.Mitgliedsnummer);
            });

            modelBuilder.Entity<Schwierigkeit>(entity =>
            {
                entity.ToTable("Schwierigkeit");

                entity.Property(e => e.Id)
                    .HasColumnType("INT")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name).HasColumnType("Text");
            });

            modelBuilder.Entity<Snowboarder>(entity =>
            {
                entity.HasKey(e => e.Mitgliedsnummer);

                entity.ToTable("Snowboarder");

                entity.Property(e => e.Geburtstag).HasColumnType("Date");

                entity.Property(e => e.HausBergId)
                    .HasColumnType("INT")
                    .HasColumnName("Haus_Berg_Id");

                entity.Property(e => e.Kuenstlername).HasColumnType("Text");

                entity.Property(e => e.Nachname).HasColumnType("Text");

                entity.Property(e => e.Vorname).HasColumnType("Text");

                entity.HasOne(d => d.HausBerg)
                    .WithMany(p => p.Snowboarders)
                    .HasForeignKey(d => d.HausBergId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasMany(d => d.Wettkampfs)
                    .WithMany(p => p.Snowboarders)
                    .UsingEntity<Dictionary<string, object>>(
                        "Wettkaempfer",
                        l => l.HasOne<Wettkampf>().WithMany().HasForeignKey("WettkampfId"),
                        r => r.HasOne<Snowboarder>().WithMany().HasForeignKey("Snowboarder"),
                        j =>
                        {
                            j.HasKey("Snowboarder", "WettkampfId");

                            j.ToTable("Wettkaempfer");

                            j.IndexerProperty<long>("WettkampfId").HasColumnType("INT").HasColumnName("Wettkampf_Id");
                        });
            });

            modelBuilder.Entity<Sponsor>(entity =>
            {
                entity.ToTable("Sponsor");

                entity.Property(e => e.Id)
                    .HasColumnType("INT")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name).HasColumnType("Text");
            });

            modelBuilder.Entity<Sponsoring>(entity =>
            {
                entity.HasKey(e => new { e.Snowboarder, e.Sponsor });

                entity.ToTable("Sponsoring");

                entity.Property(e => e.Snowboarder).HasColumnType("Text");

                entity.Property(e => e.Sponsor).HasColumnType("INT");

                entity.Property(e => e.Vertragsart).HasColumnType("INT");

                entity.HasOne(d => d.SnowboarderNavigation)
                    .WithMany(p => p.Sponsorings)
                    .HasForeignKey(d => d.Snowboarder);

                entity.HasOne(d => d.SponsorNavigation)
                    .WithMany(p => p.Sponsorings)
                    .HasForeignKey(d => d.Sponsor);

                entity.HasOne(d => d.VertragsartNavigation)
                    .WithMany(p => p.Sponsorings)
                    .HasForeignKey(d => d.Vertragsart)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Trick>(entity =>
            {
                entity.ToTable("Trick");

                entity.Property(e => e.Id)
                    .HasColumnType("INT")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name).HasColumnType("Text");
            });

            modelBuilder.Entity<Vertragsart>(entity =>
            {
                entity.ToTable("Vertragsart");

                entity.Property(e => e.Id)
                    .HasColumnType("INT")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name).HasColumnType("Text");
            });

            modelBuilder.Entity<Wettkampf>(entity =>
            {
                entity.ToTable("Wettkampf");

                entity.Property(e => e.Id)
                    .HasColumnType("INT")
                    .ValueGeneratedNever();

                entity.Property(e => e.BergId)
                    .HasColumnType("INT")
                    .HasColumnName("Berg_Id");

                entity.Property(e => e.Jahr).HasColumnType("INT");

                entity.Property(e => e.Name).HasColumnType("Text");

                entity.Property(e => e.SponsorId)
                    .HasColumnType("INT")
                    .HasColumnName("Sponsor_Id");

                entity.HasOne(d => d.Berg)
                    .WithMany(p => p.Wettkampfs)
                    .HasForeignKey(d => d.BergId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(d => d.Sponsor)
                    .WithMany(p => p.Wettkampfs)
                    .HasForeignKey(d => d.SponsorId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
