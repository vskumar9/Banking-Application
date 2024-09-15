using Microsoft.EntityFrameworkCore;

namespace BankApplicationAPI.Models;

public partial class SunBankContext : DbContext
{
    public SunBankContext()
    {
    }

    public SunBankContext(DbContextOptions<SunBankContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<AccountStatusType> AccountStatusTypes { get; set; }

    public virtual DbSet<AuditLog> AuditLogs { get; set; }

    public virtual DbSet<Complaint> Complaints { get; set; }

    public virtual DbSet<ComplaintFeedback> ComplaintFeedbacks { get; set; }

    public virtual DbSet<ComplaintResolution> ComplaintResolutions { get; set; }

    public virtual DbSet<ComplaintStatusHistory> ComplaintStatusHistories { get; set; }

    public virtual DbSet<ComplaintType> ComplaintTypes { get; set; }

    public virtual DbSet<Configuration> Configurations { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<LoanApplication> LoanApplications { get; set; }

    public virtual DbSet<LoanPaymentSchedule> LoanPaymentSchedules { get; set; }

    public virtual DbSet<LoanRepaymentLog> LoanRepaymentLogs { get; set; }

    public virtual DbSet<LoanType> LoanTypes { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RolePermission> RolePermissions { get; set; }

    public virtual DbSet<SavingsInterestRate> SavingsInterestRates { get; set; }

    public virtual DbSet<TransactionLog> TransactionLogs { get; set; }

    public virtual DbSet<TransactionType> TransactionTypes { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId);

            entity.ToTable("Account");

            entity.Property(e => e.AccountId).ValueGeneratedNever().HasColumnName("AccountID");
            entity.Property(e => e.AccountStatusTypeId).HasColumnName("AccountStatusTypeID");
            entity.Property(e => e.CurrentBalance).HasColumnType("money");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.InterestSavingsRateId).HasColumnName("InterestSavingsRateID");

            entity.HasOne(d => d.AccountStatusType).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.AccountStatusTypeId).HasConstraintName("FK__Account__Account__5165187F");

            entity.HasOne(d => d.Customer).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.CustomerId).HasConstraintName("FK__Account__Custome__4F7CD00D");

            entity.HasOne(d => d.InterestSavingsRate).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.InterestSavingsRateId).HasConstraintName("FK__Account__Interes__5070F446");
        });

        modelBuilder.Entity<AccountStatusType>(entity =>
        {
            entity.HasKey(e => e.AccountStatusTypeId);

            entity.ToTable("AccountStatusType");

            entity.Property(e => e.AccountStatusTypeId).HasColumnName("AccountStatusTypeID");
            entity.Property(e => e.AccountStatusDescription).HasMaxLength(30).IsUnicode(false);
        });

        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.HasKey(e => e.AuditLogId);
            entity.Property(e => e.Action).HasMaxLength(255);
            entity.Property(e => e.ActionDate).HasDefaultValueSql("(getdate())").HasColumnType("datetime");
            entity.Property(e => e.Details).HasMaxLength(1000);
            entity.Property(e => e.EmployeeId).HasMaxLength(255).IsUnicode(false).HasColumnName("EmployeeID");
            entity.Property(e => e.IpAddress).HasMaxLength(50);
            entity.HasOne(d => d.Employee).WithMany(p => p.AuditLogs).HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__AuditLogs__Emplo__6D0D32F4");
        });

        modelBuilder.Entity<Complaint>(entity =>
        {
            entity.HasKey(e => e.ComplaintId);

            entity.ToTable("Complaint");

            entity.Property(e => e.ComplaintId).HasColumnName("ComplaintID");
            entity.Property(e => e.ComplaintDate).HasColumnType("datetime");
            entity.Property(e => e.ComplaintDescription).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.Files).HasColumnName("File");
            entity.Property(e => e.ComplaintStatus).HasMaxLength(20).IsUnicode(false);
            entity.Property(e => e.ComplaintTypeId).HasColumnName("ComplaintTypeID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.ResolutionComments).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.ResolutionDate).HasColumnType("datetime");

            entity.HasOne(d => d.ComplaintType).WithMany(p => p.Complaints)
                .HasForeignKey(d => d.ComplaintTypeId).HasConstraintName("FK__Complaint__Compl__787EE5A0");

            entity.HasOne(d => d.Customer).WithMany(p => p.Complaints)
                .HasForeignKey(d => d.CustomerId).HasConstraintName("FK__Complaint__Custo__778AC167");

            entity.HasOne(d => d.Employee).WithMany(p => p.Complaints)
                .HasForeignKey(d => d.EmployeeId).HasConstraintName("FK__Complaint__Emplo__797309D9");
        });

        modelBuilder.Entity<ComplaintFeedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId);

            entity.ToTable("ComplaintFeedback");

            entity.Property(e => e.FeedbackId).HasColumnName("FeedbackID");
            entity.Property(e => e.ComplaintId).HasColumnName("ComplaintID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.FeedbackComments).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.FeedbackDate).HasColumnType("datetime");

            entity.HasOne(d => d.Complaint).WithMany(p => p.ComplaintFeedbacks)
                .HasForeignKey(d => d.ComplaintId).HasConstraintName("FK__Complaint__Compl__02FC7413");

            entity.HasOne(d => d.Customer).WithMany(p => p.ComplaintFeedbacks)
                .HasForeignKey(d => d.CustomerId).HasConstraintName("FK__Complaint__Custo__03F0984C");
        });

        modelBuilder.Entity<ComplaintResolution>(entity =>
        {
            entity.HasKey(e => e.ResolutionId);

            entity.ToTable("ComplaintResolution");

            entity.Property(e => e.ResolutionId).HasColumnName("ResolutionID");
            entity.Property(e => e.ComplaintId).HasColumnName("ComplaintID");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.ResolutionDate).HasColumnType("datetime");
            entity.Property(e => e.ResolutionDescription).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.ResolutionMethod).HasMaxLength(50).IsUnicode(false);

            entity.HasOne(d => d.Complaint).WithMany(p => p.ComplaintResolutions)
                .HasForeignKey(d => d.ComplaintId).HasConstraintName("FK__Complaint__Compl__7F2BE32F");

            entity.HasOne(d => d.Employee).WithMany(p => p.ComplaintResolutions)
                .HasForeignKey(d => d.EmployeeId).HasConstraintName("FK__Complaint__Emplo__00200768");
        });

        modelBuilder.Entity<ComplaintStatusHistory>(entity =>
        {
            entity.HasKey(e => e.StatusHistoryId);

            entity.ToTable("ComplaintStatusHistory");

            entity.Property(e => e.StatusHistoryId).HasColumnName("StatusHistoryID");
            entity.Property(e => e.ComplaintId).HasColumnName("ComplaintID");
            entity.Property(e => e.ComplaintStatus).HasMaxLength(20).IsUnicode(false);
            entity.Property(e => e.StatusComments).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.StatusDate).HasColumnType("datetime");

            entity.HasOne(d => d.Complaint).WithMany(p => p.ComplaintStatusHistories)
                .HasForeignKey(d => d.ComplaintId).HasConstraintName("FK__Complaint__Compl__7C4F7684");
        });

        modelBuilder.Entity<ComplaintType>(entity =>
        {
            entity.HasKey(e => e.ComplaintTypeId);

            entity.ToTable("ComplaintType");

            entity.Property(e => e.ComplaintTypeId).HasColumnName("ComplaintTypeID");
            entity.Property(e => e.ComplaintTypeDescription).HasMaxLength(100).IsUnicode(false);
            entity.Property(e => e.ComplaintTypeName).HasMaxLength(50).IsUnicode(false);
        });

        modelBuilder.Entity<Configuration>(entity =>
        {
            entity.HasKey(e => e.ConfigurationId);

            entity.Property(e => e.ConfigKey).HasMaxLength(100);
            entity.Property(e => e.ConfigValue).HasMaxLength(500);
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.LastUpdated).HasDefaultValueSql("(getdate())").HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(255).IsUnicode(false);

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.Configurations)
                .HasForeignKey(d => d.UpdatedBy).OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Configura__Updat__70DDC3D8");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId);

            entity.ToTable("Customer");

            entity.Property(e => e.CustomerId).ValueGeneratedNever().HasColumnName("CustomerID");
            entity.Property(e => e.CellPhone).HasMaxLength(10).IsUnicode(false).IsFixedLength();
            entity.Property(e => e.City).HasMaxLength(30).IsUnicode(false);
            entity.Property(e => e.CustomerAddress1).HasMaxLength(30).IsUnicode(false);
            entity.Property(e => e.CustomerAddress2).HasMaxLength(30).IsUnicode(false);
            entity.Property(e => e.CustomerFirstName).HasMaxLength(30).IsUnicode(false);
            entity.Property(e => e.CustomerLastName).HasMaxLength(30).IsUnicode(false);
            entity.Property(e => e.EmailAddress).HasMaxLength(30).IsUnicode(false);
            entity.Property(e => e.HomePhone).HasMaxLength(10).IsUnicode(false).IsFixedLength();
            entity.Property(e => e.State).HasMaxLength(20).IsUnicode(false).IsFixedLength();
            entity.Property(e => e.WorkPhone).HasMaxLength(10).IsUnicode(false).IsFixedLength();
            entity.Property(e => e.ZipCode).HasMaxLength(10).IsUnicode(false).IsFixedLength();
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId);

            entity.Property(e => e.EmployeeId).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.EmailAddress).HasMaxLength(255);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.EmployeeFirstName).HasMaxLength(255);
            entity.Property(e => e.EmployeeLastName).HasMaxLength(255);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime").HasDefaultValueSql("(getdate())");
            entity.Property(e => e.LastLoginDate).HasColumnType("datetime");
            entity.HasMany(e => e.AuditLogs).WithOne(a => a.Employee).HasForeignKey(a => a.EmployeeId);
            entity.HasMany(e => e.ComplaintResolutions).WithOne(cr => cr.Employee).HasForeignKey(cr => cr.EmployeeId);
            entity.HasMany(e => e.Complaints).WithOne(c => c.Employee).HasForeignKey(c => c.EmployeeId);
            entity.HasMany(e => e.Configurations).WithOne(c => c.UpdatedByNavigation).HasForeignKey(c => c.UpdatedBy);
            entity.HasMany(e => e.LoanApplications).WithOne(l => l.Employee).HasForeignKey(l => l.EmployeeId);
            entity.HasMany(e => e.LoanRepaymentLogs).WithOne(l => l.Employee).HasForeignKey(l => l.EmployeeId);
            entity.HasMany(e => e.TransactionLogs).WithOne(t => t.Employee).HasForeignKey(t => t.EmployeeId);
            entity.HasMany(e => e.UserRoles).WithOne(ur => ur.Employee).HasForeignKey(ur => ur.EmployeeId);
        });

        modelBuilder.Entity<LoanApplication>(entity =>
        {
            entity.HasKey(e => e.LoanId);

            entity.ToTable("LoanApplication");

            entity.Property(e => e.LoanId).HasColumnName("LoanID");
            entity.Property(e => e.Files).HasColumnName("Files");
            entity.Property(e => e.ApplicationDate).HasColumnType("datetime");
            entity.Property(e => e.ApprovalDate).HasColumnType("datetime");
            entity.Property(e => e.Comments).HasMaxLength(255).IsUnicode(false);
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.LoanAmount).HasColumnType("money");
            entity.Property(e => e.LoanStatus).HasMaxLength(20).IsUnicode(false);
            entity.Property(e => e.LoanTypeId).HasColumnName("LoanTypeID");

            entity.HasOne(d => d.Customer).WithMany(p => p.LoanApplications)
                .HasForeignKey(d => d.CustomerId).HasConstraintName("FK__LoanAppli__Custo__5FB337D6");

            entity.HasOne(d => d.Employee).WithMany(p => p.LoanApplications)
                .HasForeignKey(d => d.EmployeeId).HasConstraintName("FK__LoanAppli__Emplo__619B8048");

            entity.HasOne(d => d.LoanType).WithMany(p => p.LoanApplications)
                .HasForeignKey(d => d.LoanTypeId).HasConstraintName("FK__LoanAppli__LoanT__60A75C0F");
        });

        modelBuilder.Entity<LoanPaymentSchedule>(entity =>
        {
            entity.HasKey(e => e.PaymentId);

            entity.ToTable("LoanPaymentSchedule");

            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.BalanceAfterPayment).HasColumnType("money");
            entity.Property(e => e.LoanId).HasColumnName("LoanID");
            entity.Property(e => e.PaymentAmount).HasColumnType("money");
            entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentStatus).HasMaxLength(20).IsUnicode(false);

            entity.HasOne(d => d.Loan).WithMany(p => p.LoanPaymentSchedules)
                .HasForeignKey(d => d.LoanId).HasConstraintName("FK__LoanPayme__LoanI__6477ECF3");
        });

        modelBuilder.Entity<LoanRepaymentLog>(entity =>
        {
            entity.HasKey(e => e.RepaymentId);

            entity.ToTable("LoanRepaymentLog");

            entity.Property(e => e.RepaymentId).HasColumnName("RepaymentID");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.LoanId).HasColumnName("LoanID");
            entity.Property(e => e.RepaymentAmount).HasColumnType("money");
            entity.Property(e => e.RepaymentDate).HasColumnType("datetime");
            entity.Property(e => e.RepaymentMethod).HasMaxLength(50).IsUnicode(false);

            entity.HasOne(d => d.Employee).WithMany(p => p.LoanRepaymentLogs)
                .HasForeignKey(d => d.EmployeeId).HasConstraintName("FK__LoanRepay__Emplo__68487DD7");

            entity.HasOne(d => d.Loan).WithMany(p => p.LoanRepaymentLogs)
                .HasForeignKey(d => d.LoanId).HasConstraintName("FK__LoanRepay__LoanI__6754599E");
        });

        modelBuilder.Entity<LoanType>(entity =>
        {
            entity.HasKey(e => e.LoanTypeId);

            entity.ToTable("LoanType");

            entity.Property(e => e.LoanTypeId).HasColumnName("LoanTypeID");
            entity.Property(e => e.Description).HasMaxLength(100).IsUnicode(false);
            entity.Property(e => e.InterestRate).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.LoanTypeName).HasMaxLength(50).IsUnicode(false);
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.PermissionId);

            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.PermissionName).HasMaxLength(100);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId);

            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.RoleName).HasMaxLength(100);
        });

        modelBuilder.Entity<RolePermission>(entity =>
        {
            entity.HasKey(e => e.RolePermissionId);

            entity.HasOne(d => d.Permission).WithMany(p => p.RolePermissions)
                .HasForeignKey(d => d.PermissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RolePermi__Permi__693CA210");

            entity.HasOne(d => d.Role).WithMany(p => p.RolePermissions)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RolePermi__RoleI__68487DD7");
        });

        modelBuilder.Entity<SavingsInterestRate>(entity =>
        {
            entity.HasKey(e => e.InterestSavingsRateId);

            entity.Property(e => e.InterestSavingsRateId).HasColumnName("InterestSavingsRateID");
            entity.Property(e => e.InterestRateDescription).HasMaxLength(50).IsUnicode(false);
            entity.Property(e => e.InterestRateValue).HasColumnType("numeric(9, 9)");
        });

        modelBuilder.Entity<TransactionLog>(entity =>
        {
            entity.HasKey(e => e.TransactionId);

            entity.ToTable("TransactionLog");

            entity.Property(e => e.TransactionId)
                .ValueGeneratedNever()
                .HasColumnName("TransactionID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.NewBalance).HasColumnType("money");
            entity.Property(e => e.TransactionAmount).HasColumnType("money");
            entity.Property(e => e.TransactionDate).HasColumnType("datetime");
            entity.Property(e => e.TransactionTypeId).HasColumnName("TransactionTypeID");

            entity.HasOne(d => d.Account).WithMany(p => p.TransactionLogs)
                .HasForeignKey(d => d.AccountId).HasConstraintName("FK__Transacti__Accou__59063A47");

            entity.HasOne(d => d.Customer).WithMany(p => p.TransactionLogs)
                .HasForeignKey(d => d.CustomerId).HasConstraintName("FK__Transacti__Custo__5AEE82B9");

            entity.HasOne(d => d.Employee).WithMany(p => p.TransactionLogs)
                .HasForeignKey(d => d.EmployeeId).HasConstraintName("FK__Transacti__Emplo__59FA5E80");

            entity.HasOne(d => d.TransactionType).WithMany(p => p.TransactionLogs)
                .HasForeignKey(d => d.TransactionTypeId).HasConstraintName("FK__Transacti__Trans__5812160E");
        });

        modelBuilder.Entity<TransactionType>(entity =>
        {
            entity.HasKey(e => e.TransactionTypeId);

            entity.ToTable("TransactionType");

            entity.Property(e => e.TransactionTypeId).HasColumnName("TransactionTypeID");
            entity.Property(e => e.TransactionFeeAmount).HasColumnType("money");
            entity.Property(e => e.TransactionTypeDescription).HasMaxLength(50).IsUnicode(false);
            entity.Property(e => e.TransactionTypeName).HasMaxLength(10).IsUnicode(false).IsFixedLength();
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.UserRoleId);

            entity.Property(e => e.EmployeeId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("EmployeeID");

            entity.HasOne(d => d.Employee).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserRoles__Emplo__628FA481");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserRoles__RoleI__6383C8BA");
        });

        modelBuilder.Entity<Customer>().HasData(
            new Customer { CustomerId = "C111111", PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123"), CustomerFirstName = "sanjeev", CustomerLastName = "kumar", EmailAddress = "sanjeev@example.com", CellPhone = "9876543210", City = "Mumbai", State = "Maharashtra", ZipCode = "10001" },
            new Customer { CustomerId = "C111112", PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123"), CustomerFirstName = "sanjay", CustomerLastName = "ray", EmailAddress = "sanjay@example.com", CellPhone = "9876543212", City = "Kolkata", State = "west bengal", ZipCode = "90001" },
            new Customer { CustomerId = "C111113", PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123"), CustomerFirstName = "kumar", CustomerLastName = "reddy", EmailAddress = "kumar@example.com", CellPhone = "9876543213", City = "Chennai", State = "Tamilnadu", ZipCode = "60601" },
            new Customer { CustomerId = "C111114", PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123"), CustomerFirstName = "jay", CustomerLastName = "kumar", EmailAddress = "jay@example.com", CellPhone = "9876543214", City = "Kadapa", State = "Andhra pradesh", ZipCode = "77001" },
            new Customer { CustomerId = "C111115", PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123"), CustomerFirstName = "pavan", CustomerLastName = "kumar", EmailAddress = "pavan@example.com", CellPhone = "9876543215", City = "Amaravathi", State = "Andhra pradesh", ZipCode = "85001" }
        );

        modelBuilder.Entity<Employee>().HasData(
            new Employee { EmployeeId = "E111111", EmailAddress = "Amit@example.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123"), EmployeeFirstName = "Amit", EmployeeLastName = "Sharma"},
            new Employee { EmployeeId = "E111112", EmailAddress = "Neha@example.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123"), EmployeeFirstName = "Neha", EmployeeLastName = "Patel"},
            new Employee { EmployeeId = "E111113", EmailAddress = "Raj@example.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123"), EmployeeFirstName = "Raj", EmployeeLastName = "Kumar" },
            new Employee { EmployeeId = "E111114", EmailAddress = "Priya@example.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123"), EmployeeFirstName = "Priya", EmployeeLastName = "Desai"}
        );

        modelBuilder.Entity<Account>().HasData(
            new Account { AccountId = 1, AccountStatusTypeId = 1, CurrentBalance = 5000, CustomerId = "C111111", InterestSavingsRateId = 1 },
            new Account { AccountId = 2, AccountStatusTypeId = 2, CurrentBalance = 10000, CustomerId = "C111112", InterestSavingsRateId = 1 },
            new Account { AccountId = 3, AccountStatusTypeId = 3, CurrentBalance = 7500, CustomerId = "C111113", InterestSavingsRateId = 2 },
            new Account { AccountId = 4, AccountStatusTypeId = 1, CurrentBalance = 15000, CustomerId = "C111114", InterestSavingsRateId = 3 },
            new Account { AccountId = 5, AccountStatusTypeId = 2, CurrentBalance = 2000, CustomerId = "C111115", InterestSavingsRateId = 2 }
        );

        modelBuilder.Entity<AccountStatusType>().HasData(
            new AccountStatusType { AccountStatusTypeId = 1, AccountStatusDescription = "Active" },
            new AccountStatusType { AccountStatusTypeId = 2, AccountStatusDescription = "Closed" },
            new AccountStatusType { AccountStatusTypeId = 3, AccountStatusDescription = "Suspended" },
            new AccountStatusType { AccountStatusTypeId = 4, AccountStatusDescription = "Pending" },
            new AccountStatusType { AccountStatusTypeId = 5, AccountStatusDescription = "Frozen" }
        );

        modelBuilder.Entity<Complaint>().HasData(
            new Complaint { ComplaintId = 1, ComplaintDate = DateTime.Now, ComplaintDescription = "Issue with transaction", ComplaintStatus = "Open", ComplaintTypeId = 1, CustomerId = "C111111", EmployeeId = "E111111", ResolutionComments = "Pending", ResolutionDate = null },
            new Complaint { ComplaintId = 2, ComplaintDate = DateTime.Now, ComplaintDescription = "Incorrect charge", ComplaintStatus = "Resolved", ComplaintTypeId = 2, CustomerId = "C111112", EmployeeId = "E111112", ResolutionComments = "Refunded", ResolutionDate = DateTime.Now.AddDays(-1) },
            new Complaint { ComplaintId = 3, ComplaintDate = DateTime.Now, ComplaintDescription = "Account hacked", ComplaintStatus = "In Progress", ComplaintTypeId = 3, CustomerId = "C111113", EmployeeId = "E111113", ResolutionComments = "Investigating", ResolutionDate = null },
            new Complaint { ComplaintId = 4, ComplaintDate = DateTime.Now, ComplaintDescription = "Card not working", ComplaintStatus = "Resolved", ComplaintTypeId = 1, CustomerId = "C111114", EmployeeId = "E111114", ResolutionComments = "Card replaced", ResolutionDate = DateTime.Now.AddDays(-2) },
            new Complaint { ComplaintId = 5, ComplaintDate = DateTime.Now, ComplaintDescription = "Unauthorized transaction", ComplaintStatus = "Open", ComplaintTypeId = 2, CustomerId = "C111115", EmployeeId = "E111111", ResolutionComments = "Pending investigation", ResolutionDate = null }
        );

        modelBuilder.Entity<ComplaintFeedback>().HasData(
            new ComplaintFeedback { FeedbackId = 1, ComplaintId = 1, CustomerId = "C111111", FeedbackComments = "Waiting for response", FeedbackDate = DateTime.Now },
            new ComplaintFeedback { FeedbackId = 2, ComplaintId = 2, CustomerId = "C111112", FeedbackComments = "Good service", FeedbackDate = DateTime.Now.AddDays(-1) },
            new ComplaintFeedback { FeedbackId = 3, ComplaintId = 3, CustomerId = "C111113", FeedbackComments = "Please expedite", FeedbackDate = DateTime.Now },
            new ComplaintFeedback { FeedbackId = 4, ComplaintId = 4, CustomerId = "C111114", FeedbackComments = "Resolved quickly", FeedbackDate = DateTime.Now.AddDays(-2) },
            new ComplaintFeedback { FeedbackId = 5, ComplaintId = 5, CustomerId = "C111115", FeedbackComments = "Still no response", FeedbackDate = DateTime.Now }
        );

        modelBuilder.Entity<ComplaintResolution>().HasData(
            new ComplaintResolution { ResolutionId = 1, ComplaintId = 1, EmployeeId = "E111111", ResolutionDate = DateTime.Now.AddDays(-2), ResolutionDescription = "Investigated", ResolutionMethod = "Manual" },
            new ComplaintResolution { ResolutionId = 2, ComplaintId = 2, EmployeeId = "E111112", ResolutionDate = DateTime.Now.AddDays(-1), ResolutionDescription = "Refund processed", ResolutionMethod = "Automatic" },
            new ComplaintResolution { ResolutionId = 3, ComplaintId = 3, EmployeeId = "E111113", ResolutionDate = DateTime.Now, ResolutionDescription = "Ongoing investigation", ResolutionMethod = "Manual" },
            new ComplaintResolution { ResolutionId = 4, ComplaintId = 4, EmployeeId = "E111114", ResolutionDate = DateTime.Now.AddDays(-5), ResolutionDescription = "Card replaced", ResolutionMethod = "Automatic" },
            new ComplaintResolution { ResolutionId = 5, ComplaintId = 5, EmployeeId = "E111111", ResolutionDate = DateTime.Now.AddDays(-1), ResolutionDescription = "Waiting for confirmation", ResolutionMethod = "Manual" }
        );

        modelBuilder.Entity<ComplaintStatusHistory>().HasData(
            new ComplaintStatusHistory { StatusHistoryId = 1, ComplaintId = 1, ComplaintStatus = "Open", StatusComments = "Under review", StatusDate = DateTime.Now },
            new ComplaintStatusHistory { StatusHistoryId = 2, ComplaintId = 2, ComplaintStatus = "Resolved", StatusComments = "Refunded", StatusDate = DateTime.Now.AddDays(-1) },
            new ComplaintStatusHistory { StatusHistoryId = 3, ComplaintId = 3, ComplaintStatus = "In Progress", StatusComments = "Investigation ongoing", StatusDate = DateTime.Now },
            new ComplaintStatusHistory { StatusHistoryId = 4, ComplaintId = 4, ComplaintStatus = "Resolved", StatusComments = "Card replaced", StatusDate = DateTime.Now.AddDays(-2) },
            new ComplaintStatusHistory { StatusHistoryId = 5, ComplaintId = 5, ComplaintStatus = "Open", StatusComments = "Pending", StatusDate = DateTime.Now }
        );

        modelBuilder.Entity<ComplaintType>().HasData(
            new ComplaintType { ComplaintTypeId = 1, ComplaintTypeDescription = "Transaction Issues", ComplaintTypeName = "Transaction" },
            new ComplaintType { ComplaintTypeId = 2, ComplaintTypeDescription = "Account Issues", ComplaintTypeName = "Account" },
            new ComplaintType { ComplaintTypeId = 3, ComplaintTypeDescription = "Security Breach", ComplaintTypeName = "Security" },
            new ComplaintType { ComplaintTypeId = 4, ComplaintTypeDescription = "Card Issues", ComplaintTypeName = "Card" },
            new ComplaintType { ComplaintTypeId = 5, ComplaintTypeDescription = "General Complaint", ComplaintTypeName = "General" }
        );

        modelBuilder.Entity<LoanApplication>().HasData(
            new LoanApplication { LoanId = 1, ApplicationDate = DateTime.Now.AddDays(-10), ApprovalDate = DateTime.Now.AddDays(-5), Comments = "Approved for personal loan", CustomerId = "C111111", EmployeeId = "E111111", LoanAmount = 10000m, LoanStatus = "Approved", LoanTypeId = 1 },
            new LoanApplication { LoanId = 2, ApplicationDate = DateTime.Now.AddDays(-8), ApprovalDate = DateTime.Now.AddDays(-4), Comments = "Approved for home loan", CustomerId = "C111112", EmployeeId = "E111112", LoanAmount = 250000m, LoanStatus = "Approved", LoanTypeId = 2 },
            new LoanApplication { LoanId = 3, ApplicationDate = DateTime.Now.AddDays(-6), ApprovalDate = DateTime.Now.AddDays(-3), Comments = "Rejected due to insufficient credit score", CustomerId = "C111113", EmployeeId = "E111113", LoanAmount = 5000m, LoanStatus = "Rejected", LoanTypeId = 1 },
            new LoanApplication { LoanId = 4, ApplicationDate = DateTime.Now.AddDays(-4), ApprovalDate = DateTime.Now.AddDays(-2), Comments = "Approved for car loan", CustomerId = "C111114", EmployeeId = "E111113", LoanAmount = 20000m, LoanStatus = "Approved", LoanTypeId = 3 },
            new LoanApplication { LoanId = 5, ApplicationDate = DateTime.Now.AddDays(-2), ApprovalDate = DateTime.Now.AddDays(-1), Comments = "Pending approval", CustomerId = "C111115", EmployeeId = "E111111", LoanAmount = 15000m, LoanStatus = "Pending", LoanTypeId = 2 }
        );

        modelBuilder.Entity<LoanPaymentSchedule>().HasData(
            new LoanPaymentSchedule { PaymentId = 1, BalanceAfterPayment = 9000m, LoanId = 1, PaymentAmount = 1000m, PaymentDate = DateTime.Now.AddDays(-1), PaymentStatus = "Completed" },
            new LoanPaymentSchedule { PaymentId = 2, BalanceAfterPayment = 240000m, LoanId = 2, PaymentAmount = 10000m, PaymentDate = DateTime.Now.AddDays(-5), PaymentStatus = "Completed" },
            new LoanPaymentSchedule { PaymentId = 3, BalanceAfterPayment = 4000m, LoanId = 4, PaymentAmount = 1000m, PaymentDate = DateTime.Now.AddDays(-1), PaymentStatus = "Completed" },
            new LoanPaymentSchedule { PaymentId = 4, BalanceAfterPayment = 15000m, LoanId = 5, PaymentAmount = 5000m, PaymentDate = DateTime.Now, PaymentStatus = "Pending" },
            new LoanPaymentSchedule { PaymentId = 5, BalanceAfterPayment = 2000m, LoanId = 3, PaymentAmount = 3000m, PaymentDate = DateTime.Now.AddDays(-3), PaymentStatus = "Completed" }
        );

        modelBuilder.Entity<LoanRepaymentLog>().HasData(
            new LoanRepaymentLog { RepaymentId = 1, EmployeeId = "E111111", LoanId = 1, RepaymentAmount = 1000m, RepaymentDate = DateTime.Now.AddDays(-1), RepaymentMethod = "Bank Transfer" },
            new LoanRepaymentLog { RepaymentId = 2, EmployeeId = "E111112", LoanId = 2, RepaymentAmount = 10000m, RepaymentDate = DateTime.Now.AddDays(-5), RepaymentMethod = "Cheque" },
            new LoanRepaymentLog { RepaymentId = 3, EmployeeId = "E111113", LoanId = 4, RepaymentAmount = 1000m, RepaymentDate = DateTime.Now.AddDays(-1), RepaymentMethod = "Direct Debit" },
            new LoanRepaymentLog { RepaymentId = 4, EmployeeId = "E111114", LoanId = 5, RepaymentAmount = 5000m, RepaymentDate = DateTime.Now, RepaymentMethod = "Bank Transfer" },
            new LoanRepaymentLog { RepaymentId = 5, EmployeeId = "E111111", LoanId = 3, RepaymentAmount = 3000m, RepaymentDate = DateTime.Now.AddDays(-3), RepaymentMethod = "Cheque" }
        );

        modelBuilder.Entity<LoanType>().HasData(
            new LoanType { LoanTypeId = 1, Description = "Personal Loan", InterestRate = 5.00m, LoanTypeName = "Personal" },
            new LoanType { LoanTypeId = 2, Description = "Home Loan", InterestRate = 3.50m, LoanTypeName = "Home" },
            new LoanType { LoanTypeId = 3, Description = "Car Loan", InterestRate = 4.00m, LoanTypeName = "Car" },
            new LoanType { LoanTypeId = 4, Description = "Education Loan", InterestRate = 6.00m, LoanTypeName = "Education" },
            new LoanType { LoanTypeId = 5, Description = "Business Loan", InterestRate = 7.00m, LoanTypeName = "Business" }
        );

        modelBuilder.Entity<SavingsInterestRate>().HasData(
            new SavingsInterestRate { InterestSavingsRateId = 1, InterestRateDescription = "Basic Savings", InterestRateValue = 0.01m },
            new SavingsInterestRate { InterestSavingsRateId = 2, InterestRateDescription = "High Yield", InterestRateValue = 0.02m },
            new SavingsInterestRate { InterestSavingsRateId = 3, InterestRateDescription = "Premium", InterestRateValue = 0.03m },
            new SavingsInterestRate { InterestSavingsRateId = 4, InterestRateDescription = "Gold", InterestRateValue = 0.04m },
            new SavingsInterestRate { InterestSavingsRateId = 5, InterestRateDescription = "Platinum", InterestRateValue = 0.05m }
        );

        modelBuilder.Entity<TransactionLog>().HasData(
            new TransactionLog { TransactionId = 1, AccountId = 1, CustomerId = "C111111", EmployeeId = "E111111", NewBalance = 6000m, TransactionAmount = 1000m, TransactionDate = DateTime.Now.AddDays(-1), TransactionTypeId = 1 },
            new TransactionLog { TransactionId = 2, AccountId = 2, CustomerId = "C111112", EmployeeId = "E111112", NewBalance = 14000m, TransactionAmount = 1000m, TransactionDate = DateTime.Now.AddDays(-2), TransactionTypeId = 2 },
            new TransactionLog { TransactionId = 3, AccountId = 3, CustomerId = "C111113", EmployeeId = "E111113", NewBalance = 24000m, TransactionAmount = 1000m, TransactionDate = DateTime.Now.AddDays(-3), TransactionTypeId = 3 },
            new TransactionLog { TransactionId = 4, AccountId = 4, CustomerId = "C111114", EmployeeId = "E111114", NewBalance = 34000m, TransactionAmount = 1000m, TransactionDate = DateTime.Now.AddDays(-4), TransactionTypeId = 4 },
            new TransactionLog { TransactionId = 5, AccountId = 5, CustomerId = "C111115", EmployeeId = "E111111", NewBalance = 45000m, TransactionAmount = 5000m, TransactionDate = DateTime.Now.AddDays(-5), TransactionTypeId = 5 }
        );

        modelBuilder.Entity<TransactionType>().HasData(
            new TransactionType { TransactionTypeId = 1, TransactionFeeAmount = 2.00m, TransactionTypeDescription = "Deposit", TransactionTypeName = "Deposit" },
            new TransactionType { TransactionTypeId = 2, TransactionFeeAmount = 2.50m, TransactionTypeDescription = "Withdrawal", TransactionTypeName = "Withdrawal" },
            new TransactionType { TransactionTypeId = 3, TransactionFeeAmount = 3.00m, TransactionTypeDescription = "Transfer", TransactionTypeName = "Transfer" },
            new TransactionType { TransactionTypeId = 4, TransactionFeeAmount = 1.00m, TransactionTypeDescription = "Balance Inquiry", TransactionTypeName = "Inquiry" },
            new TransactionType { TransactionTypeId = 5, TransactionFeeAmount = 4.00m, TransactionTypeDescription = "Fee", TransactionTypeName = "Fee" }
        );

        modelBuilder.Entity<AuditLog>().HasData(
            new AuditLog { AuditLogId = 1, Action = "User Login", ActionDate = DateTime.Now, Details = "User logged in successfully", EmployeeId = "E111111", IpAddress = "192.168.1.1" },
            new AuditLog { AuditLogId = 2, Action = "Password Reset", ActionDate = DateTime.Now.AddMinutes(-10), Details = "Password reset for user John", EmployeeId = "E111111", IpAddress = "192.168.1.2" },
            new AuditLog { AuditLogId = 3, Action = "Created New User", ActionDate = DateTime.Now.AddHours(-1), Details = "Admin created a new user", EmployeeId = "E111111", IpAddress = "192.168.1.3" },
            new AuditLog { AuditLogId = 4, Action = "Failed Login Attempt", ActionDate = DateTime.Now.AddDays(-1), Details = "User failed login attempt", EmployeeId = "E111111", IpAddress = "192.168.1.4" },
            new AuditLog { AuditLogId = 5, Action = "Deleted Account", ActionDate = DateTime.Now.AddDays(-2), Details = "Admin deleted user account", EmployeeId = "E111112", IpAddress = "192.168.1.5" }
            );

        modelBuilder.Entity<Configuration>().HasData(
            new Configuration { ConfigurationId = 1, ConfigKey = "MaxLoginAttempts", ConfigValue = "5", Description = "Maximum number of login attempts before account lockout", LastUpdated = DateTime.Now, UpdatedBy = "E111111" },
            new Configuration { ConfigurationId = 2, ConfigKey = "SessionTimeout", ConfigValue = "30", Description = "Session timeout duration in minutes", LastUpdated = DateTime.Now.AddDays(-1), UpdatedBy = "E111111" },
            new Configuration { ConfigurationId = 3, ConfigKey = "PasswordExpirationDays", ConfigValue = "90", Description = "Number of days before a password expires", LastUpdated = DateTime.Now.AddDays(-7), UpdatedBy = "E111111" },
            new Configuration { ConfigurationId = 4, ConfigKey = "MinPasswordLength", ConfigValue = "8", Description = "Minimum password length for user accounts", LastUpdated = DateTime.Now.AddHours(-3), UpdatedBy = "E111111" },
            new Configuration { ConfigurationId = 5, ConfigKey = "EnableTwoFactorAuth", ConfigValue = "true", Description = "Enables or disables two-factor authentication", LastUpdated = DateTime.Now.AddMonths(-1), UpdatedBy = "E111111" }
            );

        modelBuilder.Entity<Permission>().HasData(
            new Permission { PermissionId = 1, PermissionName = "ViewAccount", Description = "View account details" },
            new Permission { PermissionId = 2, PermissionName = "TransferFunds", Description = "Transfer funds between accounts" },
            new Permission { PermissionId = 3, PermissionName = "ManageAccounts", Description = "Create or manage accounts" },
            new Permission { PermissionId = 4, PermissionName = "ViewTransactionHistory", Description = "View transaction history" }
        );

        modelBuilder.Entity<Role>().HasData(
            new Role { RoleId = 1, RoleName = "support", Description = "Regular customer role" },
            new Role { RoleId = 2, RoleName = "staff", Description = "Bank employee role" },
            new Role { RoleId = 3, RoleName = "cashier", Description = "Administrative role with full access" },
            new Role { RoleId = 4, RoleName = "admin", Description = "Administrative role with full access" }
        );

        modelBuilder.Entity<RolePermission>().HasData(
            // Customer permissions
            new RolePermission { RolePermissionId = 1, RoleId = 1, PermissionId = 1 }, 
            new RolePermission { RolePermissionId = 2, RoleId = 1, PermissionId = 4 }, 
            new RolePermission { RolePermissionId = 3, RoleId = 1, PermissionId = 2 }, 

            // Bank Employee permissions
            new RolePermission { RolePermissionId = 4, RoleId = 2, PermissionId = 1 }, 
            new RolePermission { RolePermissionId = 5, RoleId = 2, PermissionId = 4 }, 
            new RolePermission { RolePermissionId = 6, RoleId = 2, PermissionId = 3 }, 

            // Admin permissions
            new RolePermission { RolePermissionId = 7, RoleId = 4, PermissionId = 1 }, 
            new RolePermission { RolePermissionId = 8, RoleId = 4, PermissionId = 2 }, 
            new RolePermission { RolePermissionId = 9, RoleId = 4, PermissionId = 3 }, 
            new RolePermission { RolePermissionId = 10, RoleId = 4, PermissionId = 4 } 
        );

        modelBuilder.Entity<UserRole>().HasData(
            new UserRole { UserRoleId = 1, EmployeeId = "E111111", RoleId = 1 }, 
            new UserRole { UserRoleId = 2, EmployeeId = "E111112", RoleId = 2 }, 
            new UserRole { UserRoleId = 3, EmployeeId = "E111113", RoleId = 3 }, 
            new UserRole { UserRoleId = 4, EmployeeId = "E111114", RoleId = 4 }  
        );

    }


}
