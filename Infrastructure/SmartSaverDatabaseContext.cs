using Microsoft.EntityFrameworkCore;

namespace FinSteady_API.Infrastructure;

public partial class SmartSaverDatabaseContext : DbContext
{
    public SmartSaverDatabaseContext()
    {
    }

    public SmartSaverDatabaseContext(DbContextOptions<SmartSaverDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Card> Cards { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<SavingGoal> SavingGoals { get; set; }

    public virtual DbSet<SavingRule> SavingRules { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=AJ;Database=SmartSaverDatabase;TrustServerCertificate=True;Trusted_Connection=True;MultipleActiveResultSets=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Card>(entity =>
        {
            entity.HasKey(e => e.CardId).HasName("PK__Card__55FECDAE1FCE8F4F");

            entity.ToTable("Card");

            entity.Property(e => e.AvailableBalance).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.SavingBalance).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalBalance).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.Cards)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Card__UserId__52593CB8");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__19093A0B7045B95C");

            entity.ToTable("Category");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<SavingGoal>(entity =>
        {
            entity.HasKey(e => e.GoalId).HasName("PK__Saving_G__8A4FFFD11414036C");

            entity.ToTable("Saving_Goal");

            entity.HasIndex(e => e.TargetDate, "idx_Saving_Goal_TargetDate");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FundBalance).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.TargetAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Category).WithMany(p => p.SavingGoals)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Saving_Go__Categ__656C112C");

            entity.HasOne(d => d.SavingRule).WithMany(p => p.SavingGoals)
                .HasForeignKey(d => d.SavingRuleId)
                .HasConstraintName("FK__Saving_Go__Savin__66603565");

            entity.HasOne(d => d.User).WithMany(p => p.SavingGoals)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Saving_Go__UserI__6477ECF3");
        });

        modelBuilder.Entity<SavingRule>(entity =>
        {
            entity.HasKey(e => e.SavingRuleId).HasName("PK__Saving_R__4050F25DE7C1E185");

            entity.ToTable("Saving_Rule");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Rulename)
                .HasMaxLength(255)
                .HasColumnName("rulename");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__Transact__55433A6BD830D540");

            entity.ToTable("Transaction");

            entity.HasIndex(e => e.Date, "idx_Transaction_Date");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.TransactionType).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.FromUser).WithMany(p => p.TransactionFromUsers)
                .HasForeignKey(d => d.FromUserId)
                .HasConstraintName("FK__Transacti__FromU__571DF1D5");

            entity.HasOne(d => d.ToUser).WithMany(p => p.TransactionToUsers)
                .HasForeignKey(d => d.ToUserId)
                .HasConstraintName("FK__Transacti__ToUse__5812160E");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CC4CA1835B49");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "UQ__User__A9D105347001A560").IsUnique();

            entity.HasIndex(e => e.Email, "idx_User_Email");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.Kycdoc).HasColumnName("KYCDoc");
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Active");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
