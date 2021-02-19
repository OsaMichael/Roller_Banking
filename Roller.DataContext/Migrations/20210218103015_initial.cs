using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Roller.DataContext.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Admin_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Admin_Name = table.Column<string>(nullable: true),
                    Admin_address = table.Column<string>(nullable: true),
                    Admin_mobile = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Admin_Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    MiddleName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    Age = table.Column<int>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    Salary = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    LastPaymentDate = table.Column<string>(nullable: true),
                    TotalPayment = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    branch = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    ImageThumbnailUrl = table.Column<string>(nullable: true),
                    UploadDocument = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BranchManagers",
                columns: table => new
                {
                    Manager_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Manager_Name = table.Column<string>(nullable: true),
                    Manager_address = table.Column<string>(nullable: true),
                    Manager_mobile = table.Column<string>(nullable: true),
                    Manager_Salary = table.Column<double>(nullable: false),
                    Manager_Balance = table.Column<double>(nullable: false),
                    Manager_LastPaymentDate = table.Column<string>(nullable: true),
                    Manager_TotalPayment = table.Column<double>(nullable: false),
                    Manager_branch = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchManagers", x => x.Manager_Id);
                });

            migrationBuilder.CreateTable(
                name: "Branchs",
                columns: table => new
                {
                    Branch_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Branch_Location = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branchs", x => x.Branch_Id);
                });

            migrationBuilder.CreateTable(
                name: "Cashiers",
                columns: table => new
                {
                    Cashier_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Cashier_Name = table.Column<string>(nullable: true),
                    Cashier_address = table.Column<string>(nullable: true),
                    Cashier_mobile = table.Column<string>(nullable: true),
                    Cashier_Salary = table.Column<double>(nullable: false),
                    Cashier_LastPaymentDate = table.Column<string>(nullable: true),
                    Cashier_TotalPayment = table.Column<double>(nullable: false),
                    Cashier_Balance = table.Column<double>(nullable: false),
                    Cashier_branch = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cashiers", x => x.Cashier_Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    LastDateUpdated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    Givenname = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    OtherNames = table.Column<string>(nullable: true),
                    BVNNumber = table.Column<string>(nullable: true),
                    Streetaddress = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Zipcode = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    CountryCode = table.Column<string>(nullable: true),
                    Birthday = table.Column<DateTime>(nullable: true),
                    NationalId = table.Column<string>(nullable: true),
                    Telephonecountrycode = table.Column<string>(nullable: true),
                    Telephonenumber = table.Column<string>(nullable: true),
                    Emailaddress = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    CustAccNumber = table.Column<string>(nullable: true),
                    Cust_balance = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Cust_acc_type = table.Column<string>(nullable: true),
                    Deadline = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    ImageThumbnailUrl = table.Column<string>(nullable: true),
                    UploadDocument = table.Column<string>(nullable: true),
                    TransactionPin = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailSentLogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    LastDateUpdated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    RecipientEmail = table.Column<string>(nullable: true),
                    EmailContent = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Sender = table.Column<string>(nullable: true),
                    Reciever = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: true),
                    MessageBody = table.Column<string>(nullable: true),
                    SentDate = table.Column<string>(nullable: true),
                    DateToSend = table.Column<string>(nullable: true),
                    IsSent = table.Column<bool>(nullable: false),
                    Frequency = table.Column<string>(nullable: true),
                    CopyTo = table.Column<string>(nullable: true),
                    RecieverName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailSentLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Expense_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Expense_name = table.Column<string>(nullable: true),
                    Expense_date = table.Column<string>(nullable: true),
                    Expense_amount = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Expense_Id);
                });

            migrationBuilder.CreateTable(
                name: "HROfficers",
                columns: table => new
                {
                    HR_acc_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HR_name = table.Column<string>(nullable: true),
                    HR_address = table.Column<string>(nullable: true),
                    HR_mobile = table.Column<string>(nullable: true),
                    HR_Salary = table.Column<double>(nullable: false),
                    HR_LastPaymentDate = table.Column<string>(nullable: true),
                    HR_TotalPayment = table.Column<double>(nullable: false),
                    HR_Balance = table.Column<double>(nullable: false),
                    HR_branch = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HROfficers", x => x.HR_acc_Id);
                });

            migrationBuilder.CreateTable(
                name: "LoanOfficers",
                columns: table => new
                {
                    LOfficer_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LOfficer_name = table.Column<string>(nullable: true),
                    LOfficer_address = table.Column<string>(nullable: true),
                    LOfficer_mobile = table.Column<string>(nullable: true),
                    LOfficer_Salary = table.Column<double>(nullable: false),
                    LOfficer_LastPaymentDate = table.Column<string>(nullable: true),
                    LOfficer_TotalPayment = table.Column<double>(nullable: false),
                    LOfficer_Balance = table.Column<double>(nullable: false),
                    LOfficer_branch = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanOfficers", x => x.LOfficer_Id);
                });

            migrationBuilder.CreateTable(
                name: "LoCheckBooks",
                columns: table => new
                {
                    Check_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Check_User_name = table.Column<string>(nullable: true),
                    Check_apply_Date = table.Column<string>(nullable: true),
                    Check_status = table.Column<string>(nullable: true),
                    Check_fixed_Date = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoCheckBooks", x => x.Check_id);
                });

            migrationBuilder.CreateTable(
                name: "MDirectors",
                columns: table => new
                {
                    MD_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MD_name = table.Column<string>(nullable: true),
                    MD_address = table.Column<string>(nullable: true),
                    MD_Salary = table.Column<double>(nullable: false),
                    MD_Balance = table.Column<double>(nullable: false),
                    MD_mobile = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MDirectors", x => x.MD_Id);
                });

            migrationBuilder.CreateTable(
                name: "Officers",
                columns: table => new
                {
                    Officer_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Officer_Name = table.Column<string>(nullable: true),
                    Officer_address = table.Column<string>(nullable: true),
                    Officer_mobile = table.Column<string>(nullable: true),
                    Officer_Salary = table.Column<double>(nullable: false),
                    Officer_LastPaymentDate = table.Column<string>(nullable: true),
                    Officer_TotalPayment = table.Column<double>(nullable: false),
                    Officer_Balance = table.Column<double>(nullable: false),
                    Officer_branch = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Officers", x => x.Officer_Id);
                });

            migrationBuilder.CreateTable(
                name: "OTPs",
                columns: table => new
                {
                    OTPID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OTP = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OTPs", x => x.OTPID);
                });

            migrationBuilder.CreateTable(
                name: "TransferOfficers",
                columns: table => new
                {
                    TO_accId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TO_name = table.Column<string>(nullable: true),
                    TO_address = table.Column<string>(nullable: true),
                    TO_mobile = table.Column<string>(nullable: true),
                    TO_Salary = table.Column<string>(nullable: true),
                    TO_LastPaymentDate = table.Column<string>(nullable: true),
                    TO_TotalPayment = table.Column<double>(nullable: false),
                    TO_Balance = table.Column<string>(nullable: true),
                    TO_branch = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferOfficers", x => x.TO_accId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    RoleId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId1",
                        column: x => x.RoleId1,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    UserId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false),
                    RoleId1 = table.Column<int>(nullable: true),
                    UserId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId1",
                        column: x => x.RoleId1,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustId = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Frequency = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Balance = table.Column<decimal>(nullable: true),
                    AccountNumber = table.Column<string>(nullable: true),
                    customerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_Accounts_Customers_customerId",
                        column: x => x.customerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<int>(nullable: true),
                    Date = table.Column<DateTime>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Operation = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    Balance = table.Column<decimal>(nullable: false),
                    Symbol = table.Column<string>(nullable: true),
                    Bank = table.Column<string>(nullable: true),
                    Account = table.Column<string>(nullable: true),
                    AccountNumber = table.Column<string>(nullable: true),
                    Tr_Through = table.Column<string>(nullable: true),
                    Tr_EmpType = table.Column<string>(nullable: true),
                    Tr_AccName = table.Column<string>(nullable: true),
                    Tr_Date = table.Column<string>(nullable: true),
                    Tr_Branch = table.Column<string>(nullable: true),
                    ReferenceNo = table.Column<string>(nullable: true),
                    Depositor = table.Column<string>(nullable: true),
                    Attendant = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Transactions_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LoanInfo",
                columns: table => new
                {
                    Loan_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    User_acc_no = table.Column<int>(nullable: false),
                    LOfficer_Id = table.Column<int>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    Loan_Amount = table.Column<double>(nullable: false),
                    AmountTo_Pay = table.Column<double>(nullable: false),
                    Interest_Rate = table.Column<double>(nullable: false),
                    Loan_Amount_Paid = table.Column<double>(nullable: false),
                    Loan_Date = table.Column<string>(nullable: true),
                    Loan_Deadline = table.Column<string>(nullable: true),
                    Loan_Branch = table.Column<string>(nullable: true),
                    Manager_Approval = table.Column<string>(nullable: true),
                    MD_Approval = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    LoanCause = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanInfo", x => x.Loan_Id);
                    table.ForeignKey(
                        name: "FK_LoanInfo_LoanOfficers_LOfficer_Id",
                        column: x => x.LOfficer_Id,
                        principalTable: "LoanOfficers",
                        principalColumn: "LOfficer_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dispositions",
                columns: table => new
                {
                    DispositionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<int>(nullable: false),
                    AccountId = table.Column<int>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dispositions", x => x.DispositionId);
                    table.ForeignKey(
                        name: "FK_Dispositions_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dispositions_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Loans",
                columns: table => new
                {
                    LoanId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountId = table.Column<int>(nullable: false),
                    CustId = table.Column<int>(nullable: false),
                    LOfficer_Id = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    Duration = table.Column<int>(nullable: false),
                    Deadline = table.Column<string>(nullable: true),
                    Payments = table.Column<decimal>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    AccountNumber = table.Column<string>(nullable: true),
                    Loan_Amount = table.Column<decimal>(nullable: false),
                    AmountTo_Pay = table.Column<decimal>(nullable: false),
                    Interest_Rate = table.Column<decimal>(nullable: false),
                    Loan_Amount_Paid = table.Column<decimal>(nullable: false),
                    Loan_Date = table.Column<string>(nullable: true),
                    Manager_Approval = table.Column<string>(nullable: true),
                    LoanCause = table.Column<string>(nullable: true),
                    Frequency = table.Column<string>(nullable: true),
                    Total_Paid = table.Column<decimal>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loans", x => x.LoanId);
                    table.ForeignKey(
                        name: "FK_Loans_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PermenentOrder",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountId = table.Column<int>(nullable: false),
                    BankTo = table.Column<string>(nullable: true),
                    AccountTo = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(nullable: true),
                    Symbol = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermenentOrder", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_PermenentOrder_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    CardId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DispositionId = table.Column<int>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    Issued = table.Column<DateTime>(nullable: false),
                    Cctype = table.Column<string>(nullable: true),
                    Ccnumber = table.Column<string>(nullable: true),
                    Cvv2 = table.Column<string>(nullable: true),
                    ExpM = table.Column<int>(nullable: false),
                    ExpY = table.Column<int>(nullable: false),
                    CustomerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.CardId);
                    table.ForeignKey(
                        name: "FK_Cards_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cards_Dispositions_DispositionId",
                        column: x => x.DispositionId,
                        principalTable: "Dispositions",
                        principalColumn: "DispositionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupperAccounts",
                columns: table => new
                {
                    SupperId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LoanId = table.Column<int>(nullable: true),
                    SupperAcctNumber = table.Column<string>(nullable: true),
                    TransDate = table.Column<DateTime>(nullable: false),
                    TransactDate = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    CustAcctNumber = table.Column<string>(nullable: true),
                    SupperEmail = table.Column<string>(nullable: true),
                    SupperPhone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupperAccounts", x => x.SupperId);
                    table.ForeignKey(
                        name: "FK_SupperAccounts_Loans_LoanId",
                        column: x => x.LoanId,
                        principalTable: "Loans",
                        principalColumn: "LoanId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_customerId",
                table: "Accounts",
                column: "customerId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId1",
                table: "AspNetRoleClaims",
                column: "RoleId1");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId1",
                table: "AspNetUserClaims",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId1",
                table: "AspNetUserRoles",
                column: "RoleId1");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId1",
                table: "AspNetUserRoles",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_CustomerId",
                table: "Cards",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_DispositionId",
                table: "Cards",
                column: "DispositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Dispositions_AccountId",
                table: "Dispositions",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Dispositions_CustomerId",
                table: "Dispositions",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanInfo_LOfficer_Id",
                table: "LoanInfo",
                column: "LOfficer_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_AccountId",
                table: "Loans",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_PermenentOrder_AccountId",
                table: "PermenentOrder",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_SupperAccounts_LoanId",
                table: "SupperAccounts",
                column: "LoanId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CustomerId",
                table: "Transactions",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BranchManagers");

            migrationBuilder.DropTable(
                name: "Branchs");

            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "Cashiers");

            migrationBuilder.DropTable(
                name: "EmailSentLogs");

            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "HROfficers");

            migrationBuilder.DropTable(
                name: "LoanInfo");

            migrationBuilder.DropTable(
                name: "LoCheckBooks");

            migrationBuilder.DropTable(
                name: "MDirectors");

            migrationBuilder.DropTable(
                name: "Officers");

            migrationBuilder.DropTable(
                name: "OTPs");

            migrationBuilder.DropTable(
                name: "PermenentOrder");

            migrationBuilder.DropTable(
                name: "SupperAccounts");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "TransferOfficers");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Dispositions");

            migrationBuilder.DropTable(
                name: "LoanOfficers");

            migrationBuilder.DropTable(
                name: "Loans");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
