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
                entity.HasKey(e => e.Name);

                entity.ToTable("berg");

                entity.Property(e => e.Name)
                    .HasColumnType("text")
                    .HasColumnName("name");

                entity.Property(e => e.Gebirge)
                    .HasColumnType("text")
                    .HasColumnName("gebirge");

                entity.Property(e => e.Schwierigkeit)
                    .HasColumnType("text")
                    .HasColumnName("schwierigkeit");

                entity.HasOne(d => d.GebirgeNavigation)
                    .WithMany(p => p.Bergs)
                    .HasForeignKey(d => d.Gebirge)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(d => d.SchwierigkeitNavigation)
                    .WithMany(p => p.Bergs)
                    .HasForeignKey(d => d.Schwierigkeit)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Gebirge>(entity =>
            {
                entity.HasKey(e => e.Name);

                entity.ToTable("gebirge");

                entity.Property(e => e.Name)
                    .HasColumnType("text")
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Profi>(entity =>
            {
                entity.HasKey(e => e.Lizenznummer);

                entity.ToTable("profi");

                entity.HasIndex(e => e.Mitgliedsnummer, "IX_profi_mitgliedsnummer")
                    .IsUnique();

                entity.Property(e => e.Lizenznummer)
                    .HasColumnType("text")
                    .HasColumnName("lizenznummer");

                entity.Property(e => e.BestTrick)
                    .HasColumnType("text")
                    .HasColumnName("best_trick");

                entity.Property(e => e.Mitgliedsnummer)
                    .HasColumnType("text")
                    .HasColumnName("mitgliedsnummer");

                entity.Property(e => e.Weltcuppunkte)
                    .HasColumnType("integer")
                    .HasColumnName("weltcuppunkte");

                entity.HasOne(d => d.MitgliedsnummerNavigation)
                    .WithOne(p => p.Profi)
                    .HasForeignKey<Profi>(d => d.Mitgliedsnummer)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Schwierigkeit>(entity =>
            {
                entity.HasKey(e => e.Name);

                entity.ToTable("schwierigkeit");

                entity.Property(e => e.Name)
                    .HasColumnType("text")
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Snowboarder>(entity =>
            {
                entity.HasKey(e => e.Mitgliedsnummer);

                entity.ToTable("snowboarder");

                entity.Property(e => e.Mitgliedsnummer)
                    .HasColumnType("text")
                    .HasColumnName("mitgliedsnummer");

                entity.Property(e => e.Geburtstag)
                    .HasColumnType("date")
                    .HasColumnName("geburtstag");

                entity.Property(e => e.HausBerg)
                    .HasColumnType("text")
                    .HasColumnName("haus_berg");

                entity.Property(e => e.Kuenstlername)
                    .HasColumnType("text")
                    .HasColumnName("kuenstlername");

                entity.Property(e => e.Nachname)
                    .HasColumnType("text")
                    .HasColumnName("nachname");

                entity.Property(e => e.Vorname)
                    .HasColumnType("text")
                    .HasColumnName("vorname");

                entity.HasOne(d => d.HausBergNavigation)
                    .WithMany(p => p.Snowboarders)
                    .HasForeignKey(d => d.HausBerg)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(d => d.MitgliedsnummerNavigation)
                    .WithOne(p => p.Snowboarder)
                    .HasPrincipalKey<Profi>(p => p.Mitgliedsnummer)
                    .HasForeignKey<Snowboarder>(d => d.Mitgliedsnummer)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasMany(d => d.Wettkampfs)
                    .WithMany(p => p.Snowboarders)
                    .UsingEntity<Dictionary<string, object>>(
                        "Wettkaempfer",
                        l => l.HasOne<Wettkampf>().WithMany().HasForeignKey("WettkampfId"),
                        r => r.HasOne<Snowboarder>().WithMany().HasForeignKey("Snowboarder"),
                        j =>
                        {
                            j.HasKey("Snowboarder", "WettkampfId");

                            j.ToTable("wettkaempfer");

                            j.IndexerProperty<string>("Snowboarder").HasColumnType("varchar").HasColumnName("snowboarder");

                            j.IndexerProperty<long>("WettkampfId").HasColumnType("integer").HasColumnName("wettkampf_id");
                        });
            });

            modelBuilder.Entity<Sponsor>(entity =>
            {
                entity.HasKey(e => e.Name);

                entity.ToTable("sponsor");

                entity.Property(e => e.Name)
                    .HasColumnType("text")
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Sponsoring>(entity =>
            {
                entity.HasKey(e => new { e.Snowboarder, e.Sponsor });

                entity.ToTable("sponsoring");

                entity.Property(e => e.Snowboarder)
                    .HasColumnType("text")
                    .HasColumnName("snowboarder");

                entity.Property(e => e.Sponsor)
                    .HasColumnType("text")
                    .HasColumnName("sponsor");

                entity.Property(e => e.Vertragsart)
                    .HasColumnType("text")
                    .HasColumnName("vertragsart");

                entity.HasOne(d => d.SnowboarderNavigation)
                    .WithMany(p => p.Sponsorings)
                    .HasForeignKey(d => d.Snowboarder);

                entity.HasOne(d => d.SponsorNavigation)
                    .WithMany(p => p.Sponsorings)
                    .HasForeignKey(d => d.Sponsor);

                entity.HasOne(d => d.VertragsartNavigation)
                    .WithMany(p => p.Sponsorings)
                    .HasForeignKey(d => d.Vertragsart)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Trick>(entity =>
            {
                entity.HasKey(e => e.Name);

                entity.ToTable("trick");

                entity.Property(e => e.Name)
                    .HasColumnType("text")
                    .HasColumnName("name");

                entity.Property(e => e.Beschreibung)
                    .HasColumnType("text")
                    .HasColumnName("beschreibung");
            });

            modelBuilder.Entity<Vertragsart>(entity =>
            {
                entity.HasKey(e => e.Name);

                entity.ToTable("vertragsart");

                entity.Property(e => e.Name)
                    .HasColumnType("text")
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Wettkampf>(entity =>
            {
                entity.HasKey(e => e.Rowid);

                entity.ToTable("wettkampf");

                entity.Property(e => e.Rowid)
                    .ValueGeneratedNever()
                    .HasColumnName("rowid");

                entity.Property(e => e.Berg)
                    .HasColumnType("varchar")
                    .HasColumnName("berg");

                entity.Property(e => e.Jahr)
                    .HasColumnType("year")
                    .HasColumnName("jahr");

                entity.Property(e => e.Name)
                    .HasColumnType("varchar")
                    .HasColumnName("name");

                entity.Property(e => e.Preisgeld)
                    .HasColumnType("double")
                    .HasColumnName("preisgeld");

                entity.Property(e => e.Sponsor)
                    .HasColumnType("varchar")
                    .HasColumnName("sponsor");

                entity.HasOne(d => d.BergNavigation)
                    .WithMany(p => p.Wettkampfs)
                    .HasForeignKey(d => d.Berg)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(d => d.SponsorNavigation)
                    .WithMany(p => p.Wettkampfs)
                    .HasForeignKey(d => d.Sponsor)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
