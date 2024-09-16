using BankApplicationAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using BankApplicationAPI.Models;
using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Repository;
using BankApplicationAPI.Exceptions;
using BankApplicationAPI.Helpers;
using BankApplicationAPI.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add Swagger/OpenAPI for API documentation.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Setup DB Context with SQL Server, and retry on failure logic.
builder.Services.AddDbContext<SunBankContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure())
);

// Prevent reference loop issues when serializing objects.
builder.Services.AddControllers().AddNewtonsoftJson(opt => {
    opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

// Add JWT Authentication scheme.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Key"]!)),
                       ValidateIssuer = false,  
                       ValidateAudience = false ,
                       ValidateLifetime = true,
                   };
               });

// Swagger for JWT authorization UI
builder.Services.AddSwaggerGen(c => {
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<ApplicationUtil>();
builder.Services.AddScoped<IdHelper>();
builder.Services.AddHostedService<TokenExpiryService>();



builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<IAccount, AccountRepository>();
builder.Services.AddScoped<IAccountStatusType, AccountStatusTypeRepository>();
builder.Services.AddScoped<IAuditLog, AuditLogRepository>();
builder.Services.AddScoped<IComplaint, ComplaintRepository>();
builder.Services.AddScoped<IComplaintFeedback, ComplaintFeedbackRepository>();
builder.Services.AddScoped<IComplaintResolution, ComplaintResolutionRepository>();
builder.Services.AddScoped<IComplaintStatusHistory, ComplaintStatusHistoryRepository>();
builder.Services.AddScoped<IComplaintType, ComplaintTypeRepository>();
builder.Services.AddScoped<BankApplicationAPI.Interfaces.IConfiguration, ConfigurationRepository>();
builder.Services.AddScoped<ICustomer, CustomerRepository>();
builder.Services.AddScoped<IEmployee, EmployeeRepository>();
builder.Services.AddScoped<ILoanApplication, LoanApplicationRepository>();
builder.Services.AddScoped<ILoanPaymentSchedule, LoanPaymentScheduleRepository>();
builder.Services.AddScoped<ILoanRepaymentLog, LoanRepaymentLogRepository>();
builder.Services.AddScoped<ILoanType, LoanTypeRepository>();
builder.Services.AddScoped<IPermission, PermissionRepository>();
builder.Services.AddScoped<IRole, RoleRepository>();
builder.Services.AddScoped<IRolePermission, RolePermissionRepository>();
builder.Services.AddScoped<ISavingsInterestRate, SavingsInterestRateRepository>();
builder.Services.AddScoped<ITransactionLog, TransactionLogRepository>();
builder.Services.AddScoped<ITransactionType, TransactionTypeRepository>();
builder.Services.AddScoped<IUserRole, UserRoleRepository>();

builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<AccountStatusTypeService>();
builder.Services.AddScoped<AuditLogService>();
builder.Services.AddScoped<ComplaintFeedbackService>();
builder.Services.AddScoped<ComplaintResolutionService>();
builder.Services.AddScoped<ComplaintService>();
builder.Services.AddScoped<ComplaintStatusHistoryService>();
builder.Services.AddScoped<ComplaintTypeService>();
builder.Services.AddScoped<ConfigurationService>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<LoanApplicationService>();
builder.Services.AddScoped<LoanPaymentScheduleService>();
builder.Services.AddScoped<LoanRepaymentLogService>();
builder.Services.AddScoped<LoanTypeService>();
builder.Services.AddScoped<PermissionService>();
builder.Services.AddScoped<RolePermissionService>();
builder.Services.AddScoped<RoleService>();
builder.Services.AddScoped<SavingsInterestRateService>();
builder.Services.AddScoped<TransactionLogService>();
builder.Services.AddScoped<TransactionTypeService>();
builder.Services.AddScoped<UserRoleService>();





builder.Services.AddLogging(configure => configure.AddConsole());

builder.Services.AddCors(options => {
    options.AddPolicy("AllowAngularClient", builder => builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});

var app = builder.Build();
app.UseCors("AllowAngularClient");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use HTTPS for security.
app.UseHttpsRedirection();

// Enable Authentication/Authorization.
app.UseAuthentication(); // Add Authentication middleware
app.UseAuthorization();

// Map Controllers to handle API requests.
app.MapControllers();

app.Run();
