using Microsoft.EntityFrameworkCore;
using TestForOski.Api.Entities;

namespace TestForOski.Api
{
    public partial class ApplicationContext : DbContext
    {
        public ApplicationContext()
        {
        }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<Result> Results { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("accounts");

                entity.HasIndex(e => e.Email)
                    .HasName("UQ_EmailAccounts")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb3")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8mb3")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("PasswordHash")
                    .HasColumnType("varchar(64)")
                    .HasComment("SHA265 output size is 256 bits or 64 characters")
                    .HasCharSet("utf8mb3")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Role)
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8mb3")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Salt)
                    .IsRequired()
                    .HasColumnType("varchar(32)")
                    .HasComment("Standard salt size is 128 bits or 32 characters")
                    .HasCharSet("utf8mb3")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<Answer>(entity =>
            {
                entity.ToTable("answers");

                entity.HasIndex(e => e.QuestionId)
                    .HasName("FK_QuestionAnswer");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IsCorrect)
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb3")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuestionAnswer");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.ToTable("questions");

                entity.HasIndex(e => e.TestId)
                    .HasName("FK_TestQuestions");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.TestId).HasColumnName("TestID");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.TestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TestQuestions");
            });

            modelBuilder.Entity<Test>(entity =>
            {
                entity.ToTable("tests");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb3")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb3")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(40)")
                    .HasCharSet("utf8mb3")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<Result>(entity =>
            {
                entity.ToTable("results");

                entity.Property(e => e.Id)
                    .HasColumnName("ID");

                entity.HasIndex(e => e.TestId)
                    .HasName("FK_TestResult");

                entity.Property(e => e.TestId)
                    .HasColumnName("TestID");

                entity.HasIndex(e => e.UserId)
                    .HasName("FK_UserResult");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID");

                entity.Property(e => e.CorrectAnswerAmount)
                    .HasColumnName("CorrectAnswerAmount");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
