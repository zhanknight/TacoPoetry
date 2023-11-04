using Microsoft.EntityFrameworkCore;
using TacoPoetry.API.Models;

namespace TacoPoetry.API.Contexts;

public partial class TacoPoetryContext : DbContext
{

    public TacoPoetryContext()
    {
    }

    public TacoPoetryContext(DbContextOptions<TacoPoetryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Author { get; set; }

    public virtual DbSet<Content> Content { get; set; }

    public virtual DbSet<ContentType> ContentType { get; set; }

    public virtual DbSet<Tag> Tag { get; set; }

    public virtual DbSet<TagMap> TagMap { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.AuthorId).HasName("PK__Author__70DAFC14057FE522");
        });

        modelBuilder.Entity<Content>(entity =>
        {
            entity.HasKey(e => e.ContentId).HasName("PK__Content__2907A87E46F881F2");

            entity.HasOne(d => d.ContentAuthorNavigation).WithMany(p => p.Content)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Content__Content__656C112C");

            entity.HasOne(d => d.ContentTypeNavigation).WithMany(p => p.Content)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Content__Content__6477ECF3");

            // entity.Navigation(e => e.TagMap).AutoInclude();

        });

        modelBuilder.Entity<ContentType>(entity =>
        {
            entity.HasKey(e => e.ContentTypeId).HasName("PK__ContentT__2026066A66746F83");

            entity.Property(e => e.ContentTypeId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.TagId).HasName("PK__Tag__657CFA4CC674D76C");
        });

        modelBuilder.Entity<TagMap>(entity =>
        {
            entity.HasKey(e => e.TagMapId).HasName("PK__TagMap__4883479041E5A40A");

            entity.HasOne(d => d.MappedContent).WithMany(p => p.TagMap)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TagMap__MappedCo__04E4BC85");

            entity.HasOne(d => d.MappedTag).WithMany(p => p.TagMap)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TagMap__MappedTa__05D8E0BE");

            entity.Navigation(e => e.MappedTag).AutoInclude();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}