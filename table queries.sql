create database SunBank;

use SunBank;
--use master;
--drop database SunBank;

CREATE TABLE Customer (
    CustomerID varchar PRIMARY KEY,
	PasswordHash nvarchar,
	CustomerFirstName VARCHAR(30),
    CustomerLastName VARCHAR(30),
    CustomerAddress1 VARCHAR(30),
    CustomerAddress2 VARCHAR(30),
    City VARCHAR(30),
    State CHAR(2),
    ZipCode CHAR(10),
    EmailAddress VARCHAR(30),
    HomePhone CHAR(10),
    CellPhone CHAR(10),
    WorkPhone CHAR(10),
	IsActive BIT NOT NULL DEFAULT 0,
	LastLoginDate datetime2
);

CREATE TABLE AccountStatusType (
    AccountStatusTypeID TINYINT PRIMARY KEY, -- Savings Account, Fixed Deposit Account, Business Account, Loan Account, 
    AccountStatusDescription VARCHAR(30)
);

CREATE TABLE SavingsInterestRates (
    InterestSavingsRateID TINYINT PRIMARY KEY,
    InterestRateValue NUMERIC(9, 9),
    InterestRateDescription VARCHAR(20)
);

CREATE TABLE Account (
    AccountID INT PRIMARY KEY,
    CurrentBalance MONEY,
    InterestSavingsRateID TINYINT,
    AccountStatusTypeID TINYINT,
	CustomerID varchar,
	FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID),
    FOREIGN KEY (InterestSavingsRateID) REFERENCES SavingsInterestRates(InterestSavingsRateID),
    FOREIGN KEY (AccountStatusTypeID) REFERENCES AccountStatusType(AccountStatusTypeID)
);

CREATE TABLE TransactionType (
    TransactionTypeID TINYINT PRIMARY KEY,
    TransactionTypeName CHAR(10),
    TransactionTypeDescription VARCHAR(50),
    TransactionFeeAmount MONEY
);

CREATE TABLE Employee (
    EmployeeID varchar PRIMARY KEY,
	EmailAddress VARCHAR(30),
	PasswordHash nvarchar,
    EmployeeFirstName VARCHAR(25),
    EmployeeLastName VARCHAR(25),
	EmployeeRole Varchar(10), -- Customer Support/Compliance Officer, cashier, staff, Admin
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
	IsActive BIT NOT NULL DEFAULT 0,
	LastLoginDate datetime2
);

CREATE TABLE Roles (
    RoleId INT PRIMARY KEY IDENTITY(1,1),
    RoleName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255) NULL
);

CREATE TABLE UserRoles (
    UserRoleId INT PRIMARY KEY IDENTITY(1,1),
    EmployeeID varchar NOT NULL,
    RoleId INT NOT NULL,
    FOREIGN KEY (EmployeeID) REFERENCES Employee(EmployeeID),
    FOREIGN KEY (RoleId) REFERENCES Roles(RoleId)
);

CREATE TABLE Permissions (
    PermissionId INT PRIMARY KEY IDENTITY(1,1),
    PermissionName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255) NULL
);

CREATE TABLE RolePermissions (
    RolePermissionId INT PRIMARY KEY IDENTITY(1,1),
    RoleId INT NOT NULL,
    PermissionId INT NOT NULL,
    FOREIGN KEY (RoleId) REFERENCES Roles(RoleId),
    FOREIGN KEY (PermissionId) REFERENCES Permissions(PermissionId)
);

CREATE TABLE AuditLogs (
    AuditLogId INT PRIMARY KEY IDENTITY(1,1),
    Action NVARCHAR(255) NOT NULL,
    EmployeeID varchar NOT NULL,
    ActionDate DATETIME NOT NULL DEFAULT GETDATE(),
    IpAddress NVARCHAR(50) NULL,
    Details NVARCHAR(1000) NULL,
    FOREIGN KEY (EmployeeID) REFERENCES Employee(EmployeeID)
);

CREATE TABLE Configurations (
    ConfigurationId INT PRIMARY KEY IDENTITY(1,1),
    ConfigKey NVARCHAR(100) NOT NULL,
    ConfigValue NVARCHAR(500) NOT NULL,
    Description NVARCHAR(255) NULL,
    LastUpdated DATETIME NOT NULL DEFAULT GETDATE(),
    UpdatedBy varchar NOT NULL,
    FOREIGN KEY (UpdatedBy) REFERENCES Employee(EmployeeID)
);

CREATE TABLE TransactionLog (
    TransactionID INT PRIMARY KEY,
    TransactionDate DATETIME,
    TransactionTypeID TINYINT,
    TransactionAmount MONEY,
    NewBalance MONEY,
    AccountID INT,
    EmployeeID varchar,
    CustomerID varchar,
    FOREIGN KEY (TransactionTypeID) REFERENCES TransactionType(TransactionTypeID),
    FOREIGN KEY (AccountID) REFERENCES Account(AccountID),
    FOREIGN KEY (EmployeeID) REFERENCES Employee(EmployeeID),
    FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID)
);

CREATE TABLE LoanType (
    LoanTypeID INT PRIMARY KEY IDENTITY(1,1),
    LoanTypeName VARCHAR(50), -- 'Personal Loan', 'Home Loan', 'Car Loan', etc.
    InterestRate DECIMAL(5,2), -- Interest rate for the loan type
    Description VARCHAR(100)
);

CREATE TABLE LoanApplication (
    LoanID INT PRIMARY KEY IDENTITY(1,1),
    CustomerID varchar FOREIGN KEY REFERENCES Customer(CustomerID),
    LoanTypeID INT FOREIGN KEY REFERENCES LoanType(LoanTypeID),
    LoanAmount MONEY,
    ApplicationDate DATETIME,
	Files varchar,
    LoanStatus VARCHAR(20),  -- 'Approved', 'Pending', 'Rejected', etc.
    EmployeeID varchar FOREIGN KEY REFERENCES Employee(EmployeeID), -- Loan officer or employee managing the application
    ApprovalDate DATETIME,
    Comments VARCHAR(255)
);

CREATE TABLE LoanPaymentSchedule (
    PaymentID INT PRIMARY KEY IDENTITY(1,1),
    LoanID INT FOREIGN KEY REFERENCES LoanApplication(LoanID),
    PaymentDate DATETIME,
    PaymentAmount MONEY,
    BalanceAfterPayment MONEY,
    PaymentStatus VARCHAR(20)  -- 'Paid', 'Missed', 'Pending', etc.
);

CREATE TABLE LoanRepaymentLog (
    RepaymentID INT PRIMARY KEY IDENTITY(1,1),
    LoanID INT FOREIGN KEY REFERENCES LoanApplication(LoanID),
    RepaymentDate DATETIME,
    RepaymentAmount MONEY,
    EmployeeID varchar FOREIGN KEY REFERENCES Employee(EmployeeID), -- Employee managing the repayment
    RepaymentMethod VARCHAR(50)  -- 'Online', 'Cash', 'Cheque', etc.
);

CREATE TABLE Complaint (
    ComplaintID INT PRIMARY KEY IDENTITY(1,1),
    CustomerID varchar FOREIGN KEY REFERENCES Customer(CustomerID),
    ComplaintTypeID INT FOREIGN KEY REFERENCES ComplaintType(ComplaintTypeID),
    ComplaintDate DATETIME,
    ComplaintDescription VARCHAR(255),
	Files varchar,
    ComplaintStatus VARCHAR(20),  -- 'Open', 'In Progress', 'Resolved', 'Closed'
    EmployeeID varchar FOREIGN KEY REFERENCES Employee(EmployeeID),  -- Employee handling the complaint
    ResolutionDate DATETIME,
    ResolutionComments VARCHAR(255)
);

CREATE TABLE ComplaintType (
    ComplaintTypeID INT PRIMARY KEY IDENTITY(1,1),
    ComplaintTypeName VARCHAR(50),  -- 'Service Issue', 'Transaction Issue', 'Account Problem', etc.
    ComplaintTypeDescription VARCHAR(100)
);

CREATE TABLE ComplaintStatusHistory (
    StatusHistoryID INT PRIMARY KEY IDENTITY(1,1),
    ComplaintID INT FOREIGN KEY REFERENCES Complaint(ComplaintID),
    StatusDate DATETIME,
    ComplaintStatus VARCHAR(20),  -- Status change like 'Open', 'In Progress', 'Resolved', 'Closed'
    StatusComments VARCHAR(255)
);

CREATE TABLE ComplaintResolution (
    ResolutionID INT PRIMARY KEY IDENTITY(1,1),
    ComplaintID INT FOREIGN KEY REFERENCES Complaint(ComplaintID),
    ResolutionDate DATETIME,
    ResolutionMethod VARCHAR(50),  -- 'Phone Call', 'Email', 'Branch Visit', etc.
    ResolutionDescription VARCHAR(255),  -- Details of how the complaint was resolved
    EmployeeID varchar FOREIGN KEY REFERENCES Employee(EmployeeID)
);

CREATE TABLE ComplaintFeedback (
    FeedbackID INT PRIMARY KEY IDENTITY(1,1),
    ComplaintID INT FOREIGN KEY REFERENCES Complaint(ComplaintID),
    CustomerID varchar FOREIGN KEY REFERENCES Customer(CustomerID),
    FeedbackDate DATETIME,
    FeedbackRating TINYINT CHECK (FeedbackRating BETWEEN 1 AND 5),  -- Rating from 1 to 5
    FeedbackComments VARCHAR(255)
);


