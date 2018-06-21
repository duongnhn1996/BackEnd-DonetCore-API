using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using EmailWeb.Models;

namespace EmailWeb.Models
{
    public partial class webMailContext : DbContext
    {
        public webMailContext()
        {
        }

        public webMailContext(DbContextOptions<webMailContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Emails> Email { get; set; }
        public virtual DbSet<User> User { get; set; }
       

        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //            if (!optionsBuilder.IsConfigured)
        //            {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        //                optionsBuilder.UseSqlServer("Server=DESKTOP-GGLC8LP\\DUONGSQL;Database=webMail;Trusted_Connection=True;");
        //            }
        //        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Emails>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.MailTo)
                    .HasColumnName("mailto")
                    .HasMaxLength(100);

                entity.Property(e => e.Messages).HasColumnName("messages");

                entity.Property(e => e.Subject)
                    .HasColumnName("subject")
                    .HasMaxLength(200);

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.EmailNavigation)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Email_User");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fullname)
                    .HasColumnName("fullname")
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Role)
                    .HasColumnName("role")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });
        }
    }
}
