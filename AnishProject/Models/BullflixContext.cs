using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AnishProject.Models;

public partial class BullflixContext : DbContext
{
    public BullflixContext()
    {
    }

    public BullflixContext(DbContextOptions<BullflixContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Documentary> Documentaries { get; set; }

    public virtual DbSet<DocumentaryReview> DocumentaryReviews { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<MovieReview> MovieReviews { get; set; }

    public virtual DbSet<TvShow> TvShows { get; set; }

    public virtual DbSet<TvShowReview> TvShowReviews { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=USBLRSNAGASOU12\\SQLEXPRESS;database=BULLFLIX;Trusted_Connection=True;TrustServerCertificate=True;Integrated Security=True;MultipleActiveResultSets=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Documentary>(entity =>
        {
            entity.HasKey(e => e.DocumentaryId).HasName("PK__Document__7A880405BD728532");

            entity.Property(e => e.DocumentaryId).HasColumnName("Documentary_ID");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.GenreId).HasColumnName("Genre_ID");
            entity.Property(e => e.Rating)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Genre).WithMany(p => p.Documentaries)
                .HasForeignKey(d => d.GenreId)
                .HasConstraintName("FKGenre_ID");
        });

        modelBuilder.Entity<DocumentaryReview>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Document__F85DA7EB1719D7F9");

            entity.ToTable("Documentary_Reviews");

            entity.Property(e => e.ReviewId).HasColumnName("Review_ID");
            entity.Property(e => e.Comment)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.DocumentaryId).HasColumnName("Documentary_ID");
            entity.Property(e => e.UserName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("User_Name");

            entity.HasOne(d => d.Documentary).WithMany(p => p.DocumentaryReviews)
                .HasForeignKey(d => d.DocumentaryId)
                .HasConstraintName("FK_Documentary_ID");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.GenreId).HasName("PK__Genre__964A200604D0C2E7");

            entity.ToTable("Genre");

            entity.Property(e => e.GenreId).HasColumnName("Genre_ID");
            entity.Property(e => e.GenreName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Genre_Name");
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.MovieId).HasName("PK__Movies__7A8804055C2595DA");

            entity.Property(e => e.MovieId).HasColumnName("Movie_ID");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.GenreId).HasColumnName("Genre_ID");
            entity.Property(e => e.Rating)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Genre).WithMany(p => p.Movies)
                .HasForeignKey(d => d.GenreId)
                .HasConstraintName("FK_Genre_ID");
        });

        modelBuilder.Entity<MovieReview>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Movie_Re__F85DA7EB444A65C9");

            entity.ToTable("Movie_Reviews");

            entity.Property(e => e.ReviewId).HasColumnName("Review_ID");
            entity.Property(e => e.Comment)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.MovieId).HasColumnName("Movie_ID");
            entity.Property(e => e.UserName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("User_Name");

            entity.HasOne(d => d.Movie).WithMany(p => p.MovieReviews)
                .HasForeignKey(d => d.MovieId)
                .HasConstraintName("FK_Movie_ID");
        });

        modelBuilder.Entity<TvShow>(entity =>
        {
            entity.HasKey(e => e.TvShowId).HasName("PK__TV_Shows__7A880405E2F0E75C");

            entity.ToTable("TV_Shows");

            entity.Property(e => e.TvShowId).HasColumnName("TV_Show_ID");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.GenreId).HasColumnName("Genre_ID");
            entity.Property(e => e.Rating)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Genre).WithMany(p => p.TvShows)
                .HasForeignKey(d => d.GenreId)
                .HasConstraintName("Genre_ID");
        });

        modelBuilder.Entity<TvShowReview>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__TV_Show___F85DA7EBA3BD5B24");

            entity.ToTable("TV_Show_Reviews");

            entity.Property(e => e.ReviewId).HasColumnName("Review_ID");
            entity.Property(e => e.Comment)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.TvShowId).HasColumnName("TV_Show_ID");
            entity.Property(e => e.UserName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("User_Name");

            entity.HasOne(d => d.TvShow).WithMany(p => p.TvShowReviews)
                .HasForeignKey(d => d.TvShowId)
                .HasConstraintName("FK_TV_Show_ID");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC070BE4AD30");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(20)
                .IsUnicode(false);
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
