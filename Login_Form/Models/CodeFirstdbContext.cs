using Login_Form.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

public partial class CodeFirstdbContext : DbContext
{
    public CodeFirstdbContext(DbContextOptions<CodeFirstdbContext> options)
        : base(options)
    {
    }

    public DbSet<UserTbl> UserTbls { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
     


        modelBuilder.Entity<UserTbl>(entity =>
        {
            entity.ToTable("user_tbl");

            entity.HasKey(e => e.Id);


            entity.Property(e => e.Age).HasColumnName("age");

            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");

            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .HasColumnName("gender");

            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");

            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
