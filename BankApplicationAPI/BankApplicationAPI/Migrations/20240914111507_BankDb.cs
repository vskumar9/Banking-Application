using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BankApplicationAPI.Migrations
{
    /// <inheritdoc />
    public partial class BankDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountStatusType",
                columns: table => new
                {
                    AccountStatusTypeID = table.Column<byte>(type: "tinyint", nullable: false),
                    AccountStatusDescription = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountStatusType", x => x.AccountStatusTypeID);
                });

            migrationBuilder.CreateTable(
                name: "ComplaintType",
                columns: table => new
                {
                    ComplaintTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComplaintTypeName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    ComplaintTypeDescription = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplaintType", x => x.ComplaintTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerFirstName = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    CustomerLastName = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    CustomerAddress1 = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    CustomerAddress2 = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    City = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    State = table.Column<string>(type: "char(20)", unicode: false, fixedLength: true, maxLength: 20, nullable: true),
                    ZipCode = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: true),
                    EmailAddress = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    HomePhone = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: true),
                    CellPhone = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: true),
                    WorkPhone = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LastLoginDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    EmployeeFirstName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    EmployeeLastName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LastLoginDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "LoanType",
                columns: table => new
                {
                    LoanTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoanTypeName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    InterestRate = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    Description = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanType", x => x.LoanTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    PermissionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermissionName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.PermissionId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "SavingsInterestRates",
                columns: table => new
                {
                    InterestSavingsRateID = table.Column<byte>(type: "tinyint", nullable: false),
                    InterestRateValue = table.Column<decimal>(type: "numeric(9,9)", nullable: true),
                    InterestRateDescription = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavingsInterestRates", x => x.InterestSavingsRateID);
                });

            migrationBuilder.CreateTable(
                name: "TransactionType",
                columns: table => new
                {
                    TransactionTypeID = table.Column<byte>(type: "tinyint", nullable: false),
                    TransactionTypeName = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: true),
                    TransactionTypeDescription = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    TransactionFeeAmount = table.Column<decimal>(type: "money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionType", x => x.TransactionTypeID);
                });

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    AuditLogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Action = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    EmployeeID = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    ActionDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    IpAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.AuditLogId);
                    table.ForeignKey(
                        name: "FK__AuditLogs__Emplo__6D0D32F4",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId");
                });

            migrationBuilder.CreateTable(
                name: "Complaint",
                columns: table => new
                {
                    ComplaintID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ComplaintTypeID = table.Column<int>(type: "int", nullable: true),
                    ComplaintDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ComplaintDescription = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    File = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ComplaintStatus = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    EmployeeID = table.Column<string>(type: "varchar(255)", nullable: true),
                    ResolutionDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ResolutionComments = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Complaint", x => x.ComplaintID);
                    table.ForeignKey(
                        name: "FK__Complaint__Compl__787EE5A0",
                        column: x => x.ComplaintTypeID,
                        principalTable: "ComplaintType",
                        principalColumn: "ComplaintTypeID");
                    table.ForeignKey(
                        name: "FK__Complaint__Custo__778AC167",
                        column: x => x.CustomerID,
                        principalTable: "Customer",
                        principalColumn: "CustomerID");
                    table.ForeignKey(
                        name: "FK__Complaint__Emplo__797309D9",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId");
                });

            migrationBuilder.CreateTable(
                name: "Configurations",
                columns: table => new
                {
                    ConfigurationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConfigKey = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ConfigValue = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedBy = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configurations", x => x.ConfigurationId);
                    table.ForeignKey(
                        name: "FK__Configura__Updat__70DDC3D8",
                        column: x => x.UpdatedBy,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId");
                });

            migrationBuilder.CreateTable(
                name: "LoanApplication",
                columns: table => new
                {
                    LoanID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LoanTypeID = table.Column<int>(type: "int", nullable: true),
                    LoanAmount = table.Column<decimal>(type: "money", nullable: true),
                    ApplicationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Files = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoanStatus = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    EmployeeID = table.Column<string>(type: "varchar(255)", nullable: true),
                    ApprovalDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Comments = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanApplication", x => x.LoanID);
                    table.ForeignKey(
                        name: "FK__LoanAppli__Custo__5FB337D6",
                        column: x => x.CustomerID,
                        principalTable: "Customer",
                        principalColumn: "CustomerID");
                    table.ForeignKey(
                        name: "FK__LoanAppli__Emplo__619B8048",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId");
                    table.ForeignKey(
                        name: "FK__LoanAppli__LoanT__60A75C0F",
                        column: x => x.LoanTypeID,
                        principalTable: "LoanType",
                        principalColumn: "LoanTypeID");
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    RolePermissionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => x.RolePermissionId);
                    table.ForeignKey(
                        name: "FK__RolePermi__Permi__693CA210",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "PermissionId");
                    table.ForeignKey(
                        name: "FK__RolePermi__RoleI__68487DD7",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId");
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserRoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeID = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.UserRoleId);
                    table.ForeignKey(
                        name: "FK__UserRoles__Emplo__628FA481",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId");
                    table.ForeignKey(
                        name: "FK__UserRoles__RoleI__6383C8BA",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId");
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    AccountID = table.Column<int>(type: "int", nullable: false),
                    CurrentBalance = table.Column<decimal>(type: "money", nullable: true),
                    InterestSavingsRateID = table.Column<byte>(type: "tinyint", nullable: true),
                    AccountStatusTypeID = table.Column<byte>(type: "tinyint", nullable: true),
                    CustomerID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.AccountID);
                    table.ForeignKey(
                        name: "FK__Account__Account__5165187F",
                        column: x => x.AccountStatusTypeID,
                        principalTable: "AccountStatusType",
                        principalColumn: "AccountStatusTypeID");
                    table.ForeignKey(
                        name: "FK__Account__Custome__4F7CD00D",
                        column: x => x.CustomerID,
                        principalTable: "Customer",
                        principalColumn: "CustomerID");
                    table.ForeignKey(
                        name: "FK__Account__Interes__5070F446",
                        column: x => x.InterestSavingsRateID,
                        principalTable: "SavingsInterestRates",
                        principalColumn: "InterestSavingsRateID");
                });

            migrationBuilder.CreateTable(
                name: "ComplaintFeedback",
                columns: table => new
                {
                    FeedbackID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComplaintID = table.Column<int>(type: "int", nullable: true),
                    CustomerID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    FeedbackDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    FeedbackRating = table.Column<byte>(type: "tinyint", nullable: true),
                    FeedbackComments = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplaintFeedback", x => x.FeedbackID);
                    table.ForeignKey(
                        name: "FK__Complaint__Compl__02FC7413",
                        column: x => x.ComplaintID,
                        principalTable: "Complaint",
                        principalColumn: "ComplaintID");
                    table.ForeignKey(
                        name: "FK__Complaint__Custo__03F0984C",
                        column: x => x.CustomerID,
                        principalTable: "Customer",
                        principalColumn: "CustomerID");
                });

            migrationBuilder.CreateTable(
                name: "ComplaintResolution",
                columns: table => new
                {
                    ResolutionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComplaintID = table.Column<int>(type: "int", nullable: true),
                    ResolutionDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ResolutionMethod = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    ResolutionDescription = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    EmployeeID = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplaintResolution", x => x.ResolutionID);
                    table.ForeignKey(
                        name: "FK__Complaint__Compl__7F2BE32F",
                        column: x => x.ComplaintID,
                        principalTable: "Complaint",
                        principalColumn: "ComplaintID");
                    table.ForeignKey(
                        name: "FK__Complaint__Emplo__00200768",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId");
                });

            migrationBuilder.CreateTable(
                name: "ComplaintStatusHistory",
                columns: table => new
                {
                    StatusHistoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComplaintID = table.Column<int>(type: "int", nullable: true),
                    StatusDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ComplaintStatus = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    StatusComments = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplaintStatusHistory", x => x.StatusHistoryID);
                    table.ForeignKey(
                        name: "FK__Complaint__Compl__7C4F7684",
                        column: x => x.ComplaintID,
                        principalTable: "Complaint",
                        principalColumn: "ComplaintID");
                });

            migrationBuilder.CreateTable(
                name: "LoanPaymentSchedule",
                columns: table => new
                {
                    PaymentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoanID = table.Column<int>(type: "int", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    PaymentAmount = table.Column<decimal>(type: "money", nullable: true),
                    BalanceAfterPayment = table.Column<decimal>(type: "money", nullable: true),
                    PaymentStatus = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanPaymentSchedule", x => x.PaymentID);
                    table.ForeignKey(
                        name: "FK__LoanPayme__LoanI__6477ECF3",
                        column: x => x.LoanID,
                        principalTable: "LoanApplication",
                        principalColumn: "LoanID");
                });

            migrationBuilder.CreateTable(
                name: "LoanRepaymentLog",
                columns: table => new
                {
                    RepaymentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoanID = table.Column<int>(type: "int", nullable: true),
                    RepaymentDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    RepaymentAmount = table.Column<decimal>(type: "money", nullable: true),
                    EmployeeID = table.Column<string>(type: "varchar(255)", nullable: true),
                    RepaymentMethod = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanRepaymentLog", x => x.RepaymentID);
                    table.ForeignKey(
                        name: "FK__LoanRepay__Emplo__68487DD7",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId");
                    table.ForeignKey(
                        name: "FK__LoanRepay__LoanI__6754599E",
                        column: x => x.LoanID,
                        principalTable: "LoanApplication",
                        principalColumn: "LoanID");
                });

            migrationBuilder.CreateTable(
                name: "TransactionLog",
                columns: table => new
                {
                    TransactionID = table.Column<int>(type: "int", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    TransactionTypeID = table.Column<byte>(type: "tinyint", nullable: true),
                    TransactionAmount = table.Column<decimal>(type: "money", nullable: true),
                    NewBalance = table.Column<decimal>(type: "money", nullable: true),
                    AccountID = table.Column<int>(type: "int", nullable: true),
                    EmployeeID = table.Column<string>(type: "varchar(255)", nullable: true),
                    CustomerID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionLog", x => x.TransactionID);
                    table.ForeignKey(
                        name: "FK__Transacti__Accou__59063A47",
                        column: x => x.AccountID,
                        principalTable: "Account",
                        principalColumn: "AccountID");
                    table.ForeignKey(
                        name: "FK__Transacti__Custo__5AEE82B9",
                        column: x => x.CustomerID,
                        principalTable: "Customer",
                        principalColumn: "CustomerID");
                    table.ForeignKey(
                        name: "FK__Transacti__Emplo__59FA5E80",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId");
                    table.ForeignKey(
                        name: "FK__Transacti__Trans__5812160E",
                        column: x => x.TransactionTypeID,
                        principalTable: "TransactionType",
                        principalColumn: "TransactionTypeID");
                });

            migrationBuilder.InsertData(
                table: "AccountStatusType",
                columns: new[] { "AccountStatusTypeID", "AccountStatusDescription" },
                values: new object[,]
                {
                    { (byte)1, "Active" },
                    { (byte)2, "Closed" },
                    { (byte)3, "Suspended" },
                    { (byte)4, "Pending" },
                    { (byte)5, "Frozen" }
                });

            migrationBuilder.InsertData(
                table: "ComplaintType",
                columns: new[] { "ComplaintTypeID", "ComplaintTypeDescription", "ComplaintTypeName" },
                values: new object[,]
                {
                    { 1, "Transaction Issues", "Transaction" },
                    { 2, "Account Issues", "Account" },
                    { 3, "Security Breach", "Security" },
                    { 4, "Card Issues", "Card" },
                    { 5, "General Complaint", "General" }
                });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "CustomerID", "CellPhone", "City", "CustomerAddress1", "CustomerAddress2", "CustomerFirstName", "CustomerLastName", "EmailAddress", "HomePhone", "IsActive", "LastLoginDate", "PasswordHash", "State", "WorkPhone", "ZipCode" },
                values: new object[,]
                {
                    { "C111111", "9876543210", "Mumbai", null, null, "sanjeev", "kumar", "sanjeev@example.com", null, false, null, "$2a$11$VmjWKqiXTJDOLG6XbgmIeOISm4XrIFexPBZg7caauDr2O8PAFPI4O", "Maharashtra", null, "10001" },
                    { "C111112", "9876543212", "Kolkata", null, null, "sanjay", "ray", "sanjay@example.com", null, false, null, "$2a$11$BF7SaXSsi8v0eYBX7bAkmuaVg.V2YQP5rr9xbaez2BdIeqfWnJAia", "west bengal", null, "90001" },
                    { "C111113", "9876543213", "Chennai", null, null, "kumar", "reddy", "kumar@example.com", null, false, null, "$2a$11$p6eGgOvi3OIBjSLjqsOEgeLtNHL4spLHP9iM7dXYGPjYoX0g9A9ui", "Tamilnadu", null, "60601" },
                    { "C111114", "9876543214", "Kadapa", null, null, "jay", "kumar", "jay@example.com", null, false, null, "$2a$11$B7a0pDGlZ21prRznMwjwPONhMxJzLamz0sGwTXjUsc5jPftnW9Ipy", "Andhra pradesh", null, "77001" },
                    { "C111115", "9876543215", "Amaravathi", null, null, "pavan", "kumar", "pavan@example.com", null, false, null, "$2a$11$o14FJOv8ypGvvhEkXXobAeMn812JeWGD0842J0hUk9EXOd3A3yEdS", "Andhra pradesh", null, "85001" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "EmailAddress", "EmployeeFirstName", "EmployeeLastName", "IsActive", "LastLoginDate", "PasswordHash" },
                values: new object[,]
                {
                    { "E111111", "Amit@example.com", "Amit", "Sharma", false, null, "$2a$11$vS9qVDq5JQhlRFMX0VyAaOFIMlp/OBFc/R0VreR1Dhmlrk2U4YLPG" },
                    { "E111112", "Neha@example.com", "Neha", "Patel", false, null, "$2a$11$E52hro/2nV2m//2ouUt4HuHykGWwCVaCQJv3A2HNVxfOU.5BSJsiO" },
                    { "E111113", "Raj@example.com", "Raj", "Kumar", false, null, "$2a$11$eFQ2lIMVfCVaOx9HMobQSOna8n2nvSXkuYp0ZGYanu9Sof42Ll57C" },
                    { "E111114", "Priya@example.com", "Priya", "Desai", false, null, "$2a$11$yKFaYtRcsVRFP/bQ/4Ss2uotcBhOvAN7HRiESNhbhUQuXmK5OnKRS" }
                });

            migrationBuilder.InsertData(
                table: "LoanType",
                columns: new[] { "LoanTypeID", "Description", "InterestRate", "LoanTypeName" },
                values: new object[,]
                {
                    { 1, "Personal Loan", 5.00m, "Personal" },
                    { 2, "Home Loan", 3.50m, "Home" },
                    { 3, "Car Loan", 4.00m, "Car" },
                    { 4, "Education Loan", 6.00m, "Education" },
                    { 5, "Business Loan", 7.00m, "Business" }
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "PermissionId", "Description", "PermissionName" },
                values: new object[,]
                {
                    { 1, "View account details", "ViewAccount" },
                    { 2, "Transfer funds between accounts", "TransferFunds" },
                    { 3, "Create or manage accounts", "ManageAccounts" },
                    { 4, "View transaction history", "ViewTransactionHistory" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "Description", "RoleName" },
                values: new object[,]
                {
                    { 1, "Regular customer role", "support" },
                    { 2, "Bank employee role", "staff" },
                    { 3, "Administrative role with full access", "cashier" },
                    { 4, "Administrative role with full access", "admin" }
                });

            migrationBuilder.InsertData(
                table: "SavingsInterestRates",
                columns: new[] { "InterestSavingsRateID", "InterestRateDescription", "InterestRateValue" },
                values: new object[,]
                {
                    { (byte)1, "Basic Savings", 0.01m },
                    { (byte)2, "High Yield", 0.02m },
                    { (byte)3, "Premium", 0.03m },
                    { (byte)4, "Gold", 0.04m },
                    { (byte)5, "Platinum", 0.05m }
                });

            migrationBuilder.InsertData(
                table: "TransactionType",
                columns: new[] { "TransactionTypeID", "TransactionFeeAmount", "TransactionTypeDescription", "TransactionTypeName" },
                values: new object[,]
                {
                    { (byte)1, 2.00m, "Deposit", "Deposit" },
                    { (byte)2, 2.50m, "Withdrawal", "Withdrawal" },
                    { (byte)3, 3.00m, "Transfer", "Transfer" },
                    { (byte)4, 1.00m, "Balance Inquiry", "Inquiry" },
                    { (byte)5, 4.00m, "Fee", "Fee" }
                });

            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "AccountID", "AccountStatusTypeID", "CurrentBalance", "CustomerID", "InterestSavingsRateID" },
                values: new object[,]
                {
                    { 1, (byte)1, 5000m, "C111111", (byte)1 },
                    { 2, (byte)2, 10000m, "C111112", (byte)1 },
                    { 3, (byte)3, 7500m, "C111113", (byte)2 },
                    { 4, (byte)1, 15000m, "C111114", (byte)3 },
                    { 5, (byte)2, 2000m, "C111115", (byte)2 }
                });

            migrationBuilder.InsertData(
                table: "AuditLogs",
                columns: new[] { "AuditLogId", "Action", "ActionDate", "Details", "EmployeeID", "IpAddress" },
                values: new object[,]
                {
                    { 1, "User Login", new DateTime(2024, 9, 14, 16, 45, 6, 735, DateTimeKind.Local).AddTicks(7454), "User logged in successfully", "E111111", "192.168.1.1" },
                    { 2, "Password Reset", new DateTime(2024, 9, 14, 16, 35, 6, 735, DateTimeKind.Local).AddTicks(7460), "Password reset for user John", "E111111", "192.168.1.2" },
                    { 3, "Created New User", new DateTime(2024, 9, 14, 15, 45, 6, 735, DateTimeKind.Local).AddTicks(7465), "Admin created a new user", "E111111", "192.168.1.3" },
                    { 4, "Failed Login Attempt", new DateTime(2024, 9, 13, 16, 45, 6, 735, DateTimeKind.Local).AddTicks(7469), "User failed login attempt", "E111111", "192.168.1.4" },
                    { 5, "Deleted Account", new DateTime(2024, 9, 12, 16, 45, 6, 735, DateTimeKind.Local).AddTicks(7473), "Admin deleted user account", "E111112", "192.168.1.5" }
                });

            migrationBuilder.InsertData(
                table: "Complaint",
                columns: new[] { "ComplaintID", "ComplaintDate", "ComplaintDescription", "ComplaintStatus", "ComplaintTypeID", "CustomerID", "EmployeeID", "File", "ResolutionComments", "ResolutionDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 9, 14, 16, 45, 6, 735, DateTimeKind.Local).AddTicks(4998), "Issue with transaction", "Open", 1, "C111111", "E111111", null, "Pending", null },
                    { 2, new DateTime(2024, 9, 14, 16, 45, 6, 735, DateTimeKind.Local).AddTicks(5030), "Incorrect charge", "Resolved", 2, "C111112", "E111112", null, "Refunded", new DateTime(2024, 9, 13, 16, 45, 6, 735, DateTimeKind.Local).AddTicks(5034) },
                    { 3, new DateTime(2024, 9, 14, 16, 45, 6, 735, DateTimeKind.Local).AddTicks(5048), "Account hacked", "In Progress", 3, "C111113", "E111113", null, "Investigating", null },
                    { 4, new DateTime(2024, 9, 14, 16, 45, 6, 735, DateTimeKind.Local).AddTicks(5054), "Card not working", "Resolved", 1, "C111114", "E111114", null, "Card replaced", new DateTime(2024, 9, 12, 16, 45, 6, 735, DateTimeKind.Local).AddTicks(5058) },
                    { 5, new DateTime(2024, 9, 14, 16, 45, 6, 735, DateTimeKind.Local).AddTicks(5062), "Unauthorized transaction", "Open", 2, "C111115", "E111111", null, "Pending investigation", null }
                });

            migrationBuilder.InsertData(
                table: "Configurations",
                columns: new[] { "ConfigurationId", "ConfigKey", "ConfigValue", "Description", "LastUpdated", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, "MaxLoginAttempts", "5", "Maximum number of login attempts before account lockout", new DateTime(2024, 9, 14, 16, 45, 6, 735, DateTimeKind.Local).AddTicks(7526), "E111111" },
                    { 2, "SessionTimeout", "30", "Session timeout duration in minutes", new DateTime(2024, 9, 13, 16, 45, 6, 735, DateTimeKind.Local).AddTicks(7532), "E111111" },
                    { 3, "PasswordExpirationDays", "90", "Number of days before a password expires", new DateTime(2024, 9, 7, 16, 45, 6, 735, DateTimeKind.Local).AddTicks(7536), "E111111" },
                    { 4, "MinPasswordLength", "8", "Minimum password length for user accounts", new DateTime(2024, 9, 14, 13, 45, 6, 735, DateTimeKind.Local).AddTicks(7540), "E111111" },
                    { 5, "EnableTwoFactorAuth", "true", "Enables or disables two-factor authentication", new DateTime(2024, 8, 14, 16, 45, 6, 735, DateTimeKind.Local).AddTicks(7544), "E111111" }
                });

            migrationBuilder.InsertData(
                table: "LoanApplication",
                columns: new[] { "LoanID", "ApplicationDate", "ApprovalDate", "Comments", "CustomerID", "EmployeeID", "Files", "LoanAmount", "LoanStatus", "LoanTypeID" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 9, 4, 16, 45, 6, 735, DateTimeKind.Local).AddTicks(5538), new DateTime(2024, 9, 9, 16, 45, 6, 735, DateTimeKind.Local).AddTicks(5543), "Approved for personal loan", "C111111", "E111111", null, 10000m, "Approved", 1 },
                    { 2, new DateTime(2024, 9, 6, 16, 45, 6, 735, DateTimeKind.Local).AddTicks(5556), new DateTime(2024, 9, 10, 16, 45, 6, 735, DateTimeKind.Local).AddTicks(5558), "Approved for home loan", "C111112", "E111112", null, 250000m, "Approved", 2 },
                    { 3, new DateTime(2024, 9, 8, 16, 45, 6, 735, DateTimeKind.Local).AddTicks(5565), new DateTime(2024, 9, 11, 16, 45, 6, 735, DateTimeKind.Local).AddTicks(5568), "Rejected due to insufficient credit score", "C111113", "E111113", null, 5000m, "Rejected", 1 },
                    { 4, new DateTime(2024, 9, 10, 16, 45, 6, 735, DateTimeKind.Local).AddTicks(5573), new DateTime(2024, 9, 12, 16, 45, 6, 735, DateTimeKind.Local).AddTicks(5592), "Approved for car loan", "C111114", "E111113", null, 20000m, "Approved", 3 },
                    { 5, new DateTime(2024, 9, 12, 16, 45, 6, 735, DateTimeKind.Local).AddTicks(5616), new DateTime(2024, 9, 13, 16, 45, 6, 735, DateTimeKind.Local).AddTicks(5619), "Pending approval", "C111115", "E111111", null, 15000m, "Pending", 2 }
                });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "RolePermissionId", "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 4, 1 },
                    { 3, 2, 1 },
                    { 4, 1, 2 },
                    { 5, 4, 2 },
                    { 6, 3, 2 },
                    { 7, 1, 4 },
                    { 8, 2, 4 },
                    { 9, 3, 4 },
                    { 10, 4, 4 }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "UserRoleId", "EmployeeID", "RoleId" },
                values: new object[,]
                {
                    { 1, "E111111", 1 },
                    { 2, "E111112", 2 },
                    { 3, "E111113", 3 },
                    { 4, "E111114", 4 }
                });

            migrationBuilder.InsertData(
                table: "ComplaintFeedback",
                columns: new[] { "FeedbackID", "ComplaintID", "CustomerID", "FeedbackComments", "FeedbackDate", "FeedbackRating" },
                values: new object[,]
                {
                    { 1, 1, "C111111", "Waiting for response", new DateTime(2024, 9, 14, 16, 45, 6, 735, DateTimeKind.Local).AddTicks(5126), null },
                    { 2, 2, "C111112", "Good service", new DateTime(2024, 9, 13, 16, 45, 6, 735, DateTimeKind.Local).AddTicks(5132), null },
                    { 3, 3, "C111113", "Please expedite", new DateTime(2024, 9, 14, 16, 45, 6, 735, DateTimeKind.Local).AddTicks(5136), null },
                    { 4, 4, "C111114", "Resolved quickly", new DateTime(2024, 9, 12, 16, 45, 6, 735, DateTimeKind.Local).AddTicks(5140), null },
                    { 5, 5, "C111115", "Still no response", new DateTime(2024, 9, 14, 16, 45, 6, 735, DateTimeKind.Local).AddTicks(5144), null }
                });

            migrationBuilder.InsertData(
                table: "ComplaintResolution",
                columns: new[] { "ResolutionID", "ComplaintID", "EmployeeID", "ResolutionDate", "ResolutionDescription", "ResolutionMethod" },
                values: new object[,]
                {
                    { 1, 1, "E111111", new DateTime(2024, 9, 12, 16, 45, 6, 735, DateTimeKind.Local).AddTicks(5198), "Investigated", "Manual" },
                    { 2, 2, "E111112", new DateTime(2024, 9, 13, 16, 45, 6, 735, DateTimeKind.Local).AddTicks(5205), "Refund processed", "Automatic" },
                    { 3, 3, "E111113", new DateTime(2024, 9, 14, 16, 45, 6, 735, DateTimeKind.Local).AddTicks(5210), "Ongoing investigation", "Manual" },
                    { 4, 4, "E111114", new DateTime(2024, 9, 9, 16, 45, 6, 735, DateTimeKind.Local).AddTicks(5214), "Card replaced", "Automatic" },
                    { 5, 5, "E111111", new DateTime(2024, 9, 13, 16, 45, 6, 735, DateTimeKind.Local).AddTicks(5218), "Waiting for confirmation", "Manual" }
                });

            migrationBuilder.InsertData(
                table: "ComplaintStatusHistory",
                columns: new[] { "StatusHistoryID", "ComplaintID", "ComplaintStatus", "StatusComments", "StatusDate" },
                values: new object[,]
                {
                    { 1, 1, "Open", "Under review", new DateTime(2024, 9, 14, 16, 45, 6, 735, DateTimeKind.Local).AddTicks(5274) },
                    { 2, 2, "Resolved", "Refunded", new DateTime(2024, 9, 13, 16, 45, 6, 735, DateTimeKind.Local).AddTicks(5279) },
                    { 3, 3, "In Progress", "Investigation ongoing", new DateTime(2024, 9, 14, 16, 45, 6, 735, DateTimeKind.Local).AddTicks(5284) },
                    { 4, 4, "Resolved", "Card replaced", new DateTime(2024, 9, 12, 16, 45, 6, 735, DateTimeKind.Local).AddTicks(5288) },
                    { 5, 5, "Open", "Pending", new DateTime(2024, 9, 14, 16, 45, 6, 735, DateTimeKind.Local).AddTicks(5291) }
                });

            migrationBuilder.InsertData(
                table: "LoanPaymentSchedule",
                columns: new[] { "PaymentID", "BalanceAfterPayment", "LoanID", "PaymentAmount", "PaymentDate", "PaymentStatus" },
                values: new object[,]
                {
                    { 1, 9000m, 1, 1000m, new DateTime(2024, 9, 13, 16, 45, 6, 735, DateTimeKind.Local).AddTicks(5781), "Completed" },
                    { 2, 240000m, 2, 10000m, new DateTime(2024, 9, 9, 16, 45, 6, 735, DateTimeKind.Local).AddTicks(5789), "Completed" },
                    { 3, 4000m, 4, 1000m, new DateTime(2024, 9, 13, 16, 45, 6, 735, DateTimeKind.Local).AddTicks(5795), "Completed" },
                    { 4, 15000m, 5, 5000m, new DateTime(2024, 9, 14, 16, 45, 6, 735, DateTimeKind.Local).AddTicks(5801), "Pending" },
                    { 5, 2000m, 3, 3000m, new DateTime(2024, 9, 11, 16, 45, 6, 735, DateTimeKind.Local).AddTicks(5806), "Completed" }
                });

            migrationBuilder.InsertData(
                table: "LoanRepaymentLog",
                columns: new[] { "RepaymentID", "EmployeeID", "LoanID", "RepaymentAmount", "RepaymentDate", "RepaymentMethod" },
                values: new object[,]
                {
                    { 1, "E111111", 1, 1000m, new DateTime(2024, 9, 13, 16, 45, 6, 735, DateTimeKind.Local).AddTicks(5875), "Bank Transfer" },
                    { 2, "E111112", 2, 10000m, new DateTime(2024, 9, 9, 16, 45, 6, 735, DateTimeKind.Local).AddTicks(5882), "Cheque" },
                    { 3, "E111113", 4, 1000m, new DateTime(2024, 9, 13, 16, 45, 6, 735, DateTimeKind.Local).AddTicks(5887), "Direct Debit" },
                    { 4, "E111114", 5, 5000m, new DateTime(2024, 9, 14, 16, 45, 6, 735, DateTimeKind.Local).AddTicks(5892), "Bank Transfer" },
                    { 5, "E111111", 3, 3000m, new DateTime(2024, 9, 11, 16, 45, 6, 735, DateTimeKind.Local).AddTicks(5897), "Cheque" }
                });

            migrationBuilder.InsertData(
                table: "TransactionLog",
                columns: new[] { "TransactionID", "AccountID", "CustomerID", "EmployeeID", "NewBalance", "TransactionAmount", "TransactionDate", "TransactionTypeID" },
                values: new object[,]
                {
                    { 1, 1, "C111111", "E111111", 6000m, 1000m, new DateTime(2024, 9, 13, 16, 45, 6, 735, DateTimeKind.Local).AddTicks(7258), (byte)1 },
                    { 2, 2, "C111112", "E111112", 14000m, 1000m, new DateTime(2024, 9, 12, 16, 45, 6, 735, DateTimeKind.Local).AddTicks(7278), (byte)2 },
                    { 3, 3, "C111113", "E111113", 24000m, 1000m, new DateTime(2024, 9, 11, 16, 45, 6, 735, DateTimeKind.Local).AddTicks(7301), (byte)3 },
                    { 4, 4, "C111114", "E111114", 34000m, 1000m, new DateTime(2024, 9, 10, 16, 45, 6, 735, DateTimeKind.Local).AddTicks(7308), (byte)4 },
                    { 5, 5, "C111115", "E111111", 45000m, 5000m, new DateTime(2024, 9, 9, 16, 45, 6, 735, DateTimeKind.Local).AddTicks(7321), (byte)5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_AccountStatusTypeID",
                table: "Account",
                column: "AccountStatusTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Account_CustomerID",
                table: "Account",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Account_InterestSavingsRateID",
                table: "Account",
                column: "InterestSavingsRateID");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_EmployeeID",
                table: "AuditLogs",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Complaint_ComplaintTypeID",
                table: "Complaint",
                column: "ComplaintTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Complaint_CustomerID",
                table: "Complaint",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Complaint_EmployeeID",
                table: "Complaint",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintFeedback_ComplaintID",
                table: "ComplaintFeedback",
                column: "ComplaintID");

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintFeedback_CustomerID",
                table: "ComplaintFeedback",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintResolution_ComplaintID",
                table: "ComplaintResolution",
                column: "ComplaintID");

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintResolution_EmployeeID",
                table: "ComplaintResolution",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintStatusHistory_ComplaintID",
                table: "ComplaintStatusHistory",
                column: "ComplaintID");

            migrationBuilder.CreateIndex(
                name: "IX_Configurations_UpdatedBy",
                table: "Configurations",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplication_CustomerID",
                table: "LoanApplication",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplication_EmployeeID",
                table: "LoanApplication",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplication_LoanTypeID",
                table: "LoanApplication",
                column: "LoanTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_LoanPaymentSchedule_LoanID",
                table: "LoanPaymentSchedule",
                column: "LoanID");

            migrationBuilder.CreateIndex(
                name: "IX_LoanRepaymentLog_EmployeeID",
                table: "LoanRepaymentLog",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_LoanRepaymentLog_LoanID",
                table: "LoanRepaymentLog",
                column: "LoanID");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PermissionId",
                table: "RolePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_RoleId",
                table: "RolePermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionLog_AccountID",
                table: "TransactionLog",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionLog_CustomerID",
                table: "TransactionLog",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionLog_EmployeeID",
                table: "TransactionLog",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionLog_TransactionTypeID",
                table: "TransactionLog",
                column: "TransactionTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_EmployeeID",
                table: "UserRoles",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "ComplaintFeedback");

            migrationBuilder.DropTable(
                name: "ComplaintResolution");

            migrationBuilder.DropTable(
                name: "ComplaintStatusHistory");

            migrationBuilder.DropTable(
                name: "Configurations");

            migrationBuilder.DropTable(
                name: "LoanPaymentSchedule");

            migrationBuilder.DropTable(
                name: "LoanRepaymentLog");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "TransactionLog");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Complaint");

            migrationBuilder.DropTable(
                name: "LoanApplication");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "TransactionType");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "ComplaintType");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "LoanType");

            migrationBuilder.DropTable(
                name: "AccountStatusType");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "SavingsInterestRates");
        }
    }
}
