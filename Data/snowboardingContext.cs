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
                string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string aPath = System.IO.Path.Combine(sCurrentDirectory, "snowboarding.db");
                optionsBuilder.UseSqlite("DataSource="+ aPath);
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

                entity.HasOne(d => d.BestTrickNavigation)
                    .WithMany(p => p.Profis)
                    .HasForeignKey(d => d.BestTrick)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(d => d.MitgliedsnummerNavigation)
                    .WithMany(p => p.Profis)
                    .HasForeignKey(d => d.Mitgliedsnummer)
                    .OnDelete(DeleteBehavior.SetNull);
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

                entity.HasOne(d => d.VertragsartNavigation)
                    .WithMany(p => p.Sponsorings)
                    .HasForeignKey(d => d.Vertragsart)
                    .OnDelete(DeleteBehavior.SetNull);
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
                entity.HasKey(e => new { e.Name, e.Jahr });

                entity.ToTable("wettkampf");

                entity.Property(e => e.Name)
                    .HasColumnType("varchar")
                    .HasColumnName("name");

                entity.Property(e => e.Jahr)
                    .HasColumnType("year")
                    .HasColumnName("jahr");

                entity.Property(e => e.Berg)
                    .HasColumnType("varchar")
                    .HasColumnName("berg");

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
