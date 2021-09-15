using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace SwiftHR.Models
{
    public partial class SHR_SHOBIGROUP_DBContext : DbContext
    {
        private const string ConnectionString = "Server=SHOBI-GROUP\\SHOBI;Database=SHR_SHOBIGROUP_DB;Trusted_Connection=True;";

        public SHR_SHOBIGROUP_DBContext()
        {
        }

        public SHR_SHOBIGROUP_DBContext(DbContextOptions<SHR_SHOBIGROUP_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AttandancePolicy> AttandancePolicies { get; set; }
        public virtual DbSet<AttandancePolicyRule> AttandancePolicyRules { get; set; }
        public virtual DbSet<AttandancePolicyRulesCategory> AttandancePolicyRulesCategories { get; set; }
        public virtual DbSet<AttandancePolicyRulesMapping> AttandancePolicyRulesMappings { get; set; }
        public virtual DbSet<AttandancePolicySetup> AttandancePolicySetups { get; set; }
        public virtual DbSet<AuthorizedSignatory> AuthorizedSignatories { get; set; }
        public virtual DbSet<BankMaster> BankMasters { get; set; }
        public virtual DbSet<EmpAddress> EmpAddress { get; set; }
        public virtual DbSet<CurrancyMaster> CurrancyMasters { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Designation> Designations { get; set; }
        public virtual DbSet<EmailTemplate> EmailTemplates { get; set; }
        public virtual DbSet<EmpBankDetail> EmpBankDetails { get; set; }
        public virtual DbSet<EmpDirectoryMapping> EmpDirectoryMappings { get; set; }
        public virtual DbSet<EmpDocument> EmpDocuments { get; set; }
        public virtual DbSet<EmpEducationDetail> EmpEducationDetails { get; set; }
        public virtual DbSet<EmpGeneralSetting> EmpGeneralSettings { get; set; }
        public virtual DbSet<EmpInfoConfiguration> EmpInfoConfigurations { get; set; }
        public virtual DbSet<EmpInfoSection> EmpInfoSections { get; set; }
        public virtual DbSet<EmpLeavingReason> EmpLeavingReasons { get; set; }
        public virtual DbSet<EmpMasterItem> EmpMasterItems { get; set; }
        public virtual DbSet<EmpMasterItemCategory> EmpMasterItemCategories { get; set; }
        public virtual DbSet<EmpNoSeriesFormatting> EmpNoSeriesFormattings { get; set; }
        public virtual DbSet<EmpOnboardingDetail> EmpOnboardingDetails { get; set; }
        public virtual DbSet<EmpSettingsCategory> EmpSettingsCategories { get; set; }
        public virtual DbSet<EmpSettingsCategoryValue> EmpSettingsCategoryValues { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeFeed> EmployeeFeeds { get; set; }
        public virtual DbSet<FeedsCommentsAndLike> FeedsCommentsAndLikes { get; set; }
        public virtual DbSet<FeedsGroup> FeedsGroups { get; set; }
        public virtual DbSet<LeaveApplyDetail> LeaveApplyDetails { get; set; }
        public virtual DbSet<LeavePolicySetup> LeavePolicySetups { get; set; }
        public virtual DbSet<LeaveType> LeaveTypes { get; set; }
        public virtual DbSet<LeaveTypeCategory> LeaveTypeCategories { get; set; }
        public virtual DbSet<LeaveTypeMapping> LeaveTypeMappings { get; set; }
        public virtual DbSet<LeaveTypeScheme> LeaveTypeSchemes { get; set; }
        public virtual DbSet<MasterDataItem> MasterDataItems { get; set; }
        public virtual DbSet<MasterDataItemType> MasterDataItemTypes { get; set; }
        public virtual DbSet<PageAccessSetup> PageAccessSetups { get; set; }
        public virtual DbSet<PageModule> PageModules { get; set; }
        public virtual DbSet<PasswordSetupSetting> PasswordSetupSettings { get; set; }
        public virtual DbSet<PrevEmploymentDetail> PrevEmploymentDetails { get; set; }
        public virtual DbSet<PreviousEmploymentDetail> PreviousEmploymentDetails { get; set; }
        public virtual DbSet<PurchaseModuleScheme> PurchaseModuleSchemes { get; set; }
        public virtual DbSet<RoleMaster> RoleMasters { get; set; }
        public virtual DbSet<SettingsDataItem> SettingsDataItems { get; set; }
        public virtual DbSet<SettingsDataItemType> SettingsDataItemTypes { get; set; }
        public virtual DbSet<Shift> Shifts { get; set; }
        public virtual DbSet<ShiftHoursCalculationScheme> ShiftHoursCalculationSchemes { get; set; }
        public virtual DbSet<ShiftSession> ShiftSessions { get; set; }
        public virtual DbSet<UserActionLog> UserActionLogs { get; set; }
        public virtual DbSet<UserDetail> UserDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                // optionsBuilder.UseSqlServer("Server=VIKI;Database=SHR_SHOBIGROUP_DB;UID=SHOBI_TECH;PWD=SHOBI_TECH;");
                DbContextOptionsBuilder dbContextOptionsBuilder = optionsBuilder.UseSqlServer(@ConnectionString);
            }
            }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AttandancePolicy>(entity =>
            {
                entity.ToTable("AttandancePolicy");

                entity.Property(e => e.AttandancePolicyName)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AttandancePolicyRule>(entity =>
            {
                entity.Property(e => e.AttandancePolicyRule1)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("AttandancePolicyRule");

                entity.Property(e => e.AttandancePolicyRuleName)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Example)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.MarkStatusFor)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AttandancePolicyRulesCategory>(entity =>
            {
                entity.ToTable("AttandancePolicyRulesCategory");

                entity.Property(e => e.AttandancePolicyRulesCategoryName)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AttandancePolicyRulesMapping>(entity =>
            {
                entity.ToTable("AttandancePolicyRulesMapping");

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AttandancePolicySetup>(entity =>
            {
                entity.ToTable("AttandancePolicySetup");

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SchemeName)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AuthorizedSignatory>(entity =>
            {
                entity.ToTable("AuthorizedSignatory");

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Designation)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.SectionName)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.SignatureImagePath)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<BankMaster>(entity =>
            {
                entity.HasKey(e => e.BankMasterDataId);

                entity.ToTable("BankMaster");

                entity.Property(e => e.BankMasterDataId).HasColumnName("bankMasterDataId");

                entity.Property(e => e.BankCode)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("bankCode");

                entity.Property(e => e.BankName)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("bankName");

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Ifsccode)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("IFSCCODE");
            });
            
            modelBuilder.Entity<EmpAddress>(entity =>
            {
                entity.HasKey(e => e.EmpAddressId);

                entity.ToTable("EmpAddress");

                entity.Property(e => e.EmpAddressId).HasColumnName("EmpAddressId");

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("EmployeeId");

                entity.Property(e => e.Address)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("Address");

                entity.Property(e => e.Country)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Country");

                entity.Property(e => e.State)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("State");

                entity.Property(e => e.City)
                   .HasMaxLength(50)
                   .IsUnicode(false)
                   .HasColumnName("City");

                entity.Property(e => e.Pin)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Pin");

                entity.Property(e => e.IsPermanentAddress)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("IsPermanentAddress");

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CreatedDate");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CreatedBy");
            });
            modelBuilder.Entity<CurrancyMaster>(entity =>
            {
                entity.HasKey(e => e.CurrencyId);

                entity.ToTable("CurrancyMaster");

                entity.Property(e => e.Code)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CurrencyName)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DepartmentCode)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DepartmentName)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Designation>(entity =>
            {
                entity.ToTable("Designation");

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DesignationCode)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DesignationName)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EmailTemplate>(entity =>
            {
                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.EmailTemplateHtml).IsUnicode(false);

                entity.Property(e => e.EmailTemplateTitle)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EmpBankDetail>(entity =>
            {
                entity.HasKey(e => e.EmpBankDetailsId);

                entity.Property(e => e.AccountType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BankAccountNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BankName)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.BranchName)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DdpayableAt)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("DDPayableAt");

                entity.Property(e => e.DocumentFileName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Ifsc)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("IFSC");

                entity.Property(e => e.NameAsPerBankRecords)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentType)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.VerificationComments)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.VerificationStatus)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.VerifiedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EmpDirectoryMapping>(entity =>
            {
                entity.HasKey(e => e.EmpDirectoryFieldMappingId);

                entity.Property(e => e.AnnualCtc).HasColumnName("AnnualCTC");
            });

            modelBuilder.Entity<EmpDocument>(entity =>
            {
                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DocumentCategory)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DocumentFilePath)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.EmpDoumentName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.VerificationComments)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.VerificationStatus)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.VerifiedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EmpEducationDetail>(entity =>
            {
                entity.HasKey(e => e.EmpEducationId);

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Degree)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DocumentFileName)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.NameOfInstitute)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.PassingYear)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Percentage)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Program)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.VerificationComments)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.VerificationStatus)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.VerifiedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EmpInfoConfiguration>(entity =>
            {
                entity.ToTable("EmpInfoConfiguration");

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.EmpInfoConfigItem)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EmpInfoSection>(entity =>
            {
                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.EmpInfoSectionName)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EmpLeavingReason>(entity =>
            {
                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.EmpLeavingReason1)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("EmpLeavingReason");

                entity.Property(e => e.Pfcode)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("PFCode");
            });

            modelBuilder.Entity<EmpMasterItem>(entity =>
            {
                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.EmpMasterItemDescription)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.EmpMasterItemName)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EmpMasterItemCategory>(entity =>
            {
                entity.ToTable("EmpMasterItemCategory");

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.EmpMasterItemCategoryName)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EmpNoSeriesFormatting>(entity =>
            {
                entity.HasKey(e => e.EmployeeNoSeriesId);

                entity.ToTable("EmpNoSeriesFormatting");

                entity.Property(e => e.Description)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.EmpSeriesName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Format)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.MappingWithEmployeeStatus)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.SerialNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EmpOnboardingDetail>(entity =>
            {
                entity.HasKey(e => e.EmpOnboardingDetailsId);

                entity.Property(e => e.AlternateContactName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.AlternateContactNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.BloodGroup)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DateOfBirth)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.MaritalStatus)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.MarriageDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SpouceName)
                   .HasMaxLength(150)
                   .IsUnicode(false);

                entity.Property(e => e.MothersName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.NomineeDob)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("NomineeDOB");

                entity.Property(e => e.NomineeName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.NomineeContactNumber)
                   .HasMaxLength(20)
                   .IsUnicode(false);

                entity.Property(e => e.OnbemployeeId).HasColumnName("ONBEmployeeId");

                entity.Property(e => e.PermanentAddress)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.PlaceOfBirth)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.PresentAddress)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.RelationWithNominee)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Religion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OnboardingStatus)
                   .HasColumnName("OnboardingStatus");

            });

            modelBuilder.Entity<EmpSettingsCategory>(entity =>
            {
                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.EmpSettingsCategoryDescription)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.EmpSettingsCategoryName)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EmpSettingsCategoryValue>(entity =>
            {
                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.EmpSettingsCategoryValue1)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("EmpSettingsCategoryValue");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

               
                entity.Property(e => e.AdharCardName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AdharCardNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AlternateNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ConfirmationDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ContactNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CostCenter)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DateOfBirth)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PlaceOfBirth)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DateOfJoining)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Department)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Designation)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.EmergencyContactName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EmergencyNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeProfilePhoto)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeStatus)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FathersName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FunctionalGrade)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.BloodGroup)
                   .HasMaxLength(10)
                   .IsUnicode(false);

                entity.Property(e => e.Religion)
                   .HasMaxLength(20)
                   .IsUnicode(false);

                entity.Property(e => e.Grade)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.IncludeEsi).HasColumnName("IncludeESI");

                entity.Property(e => e.IncludeLwf).HasColumnName("IncludeLWF");

                entity.Property(e => e.LastName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Level)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Location)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.MaritalStatus)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.MothersName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NomineeDob)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("NomineeDOB");

                entity.Property(e => e.NomineeName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NomineeContactNumber)
                   .HasMaxLength(20)
                   .IsUnicode(false);

                entity.Property(e => e.NomineeRelation)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Panname)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("PANName");

                entity.Property(e => e.Pannumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PANNumber");

                entity.Property(e => e.PassportExpiryDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PassportNumber)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentMethod)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PersonalEmail)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Pfnumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PFNumber");

                entity.Property(e => e.ProbationPeriod)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ReportingManager)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SpouseName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SubLevel)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Uannumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("UANNumber");
            });

            modelBuilder.Entity<EmployeeFeed>(entity =>
            {
                entity.HasKey(e => e.FeedsId);

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedTime)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FeedsDescription)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.FeedsFileName)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.VisibilityDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FeedsCommentsAndLike>(entity =>
            {
                entity.HasKey(e => e.FeedsClid);

                entity.ToTable("FeedsCommentsAndLike");

                entity.Property(e => e.FeedsClid).HasColumnName("FeedsCLId");

                entity.Property(e => e.Comments)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedTime)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FeedsGroup>(entity =>
            {
                entity.ToTable("FeedsGroup");

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FeedsGroupDescription)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.FeedsGroupName)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LeaveApplyDetail>(entity =>
            {
                entity.HasKey(e => e.EmpLeaveId);

                entity.Property(e => e.LeaveAppliedOn)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LeaveFromDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LeaveReason)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.LeaveRejectReason)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.LeaveStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LeaveStatusChangeDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LeaveToDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LeaveType)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.ReportingManagerName)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LeavePolicySetup>(entity =>
            {
                entity.ToTable("LeavePolicySetup");

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LeaveType>(entity =>
            {
                entity.Property(e => e.Code)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.LeaveTypeName)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.SortOrder)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LeaveTypeCategory>(entity =>
            {
                entity.ToTable("LeaveTypeCategory");

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LeaveTypeCategoryName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LeaveTypeCategoryDesc)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LeaveTypeMapping>(entity =>
            {
                entity.ToTable("LeaveTypeMapping");

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LeaveAllotNoOfDaysPerMonth)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.LeaveAllotTotalNoDaysInYear)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LeaveTypeScheme>(entity =>
            {
                entity.ToTable("LeaveTypeScheme");

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                 .HasMaxLength(50)
                 .IsUnicode(false);

                entity.Property(e => e.LeaveTypeSchemeName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.LeaveTypeSchemeDescription)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.SchemeAppliedFrom)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SchemeAppliedTill)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MasterDataItem>(entity =>
            {
                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ItemDescription)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.MasterDataItemValue)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MasterDataItemType>(entity =>
            {
                entity.HasKey(e => e.ItemTypeId);

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ItemTypeName)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PageAccessSetup>(entity =>
            {
                entity.HasKey(e => e.PageAccessId);

                entity.ToTable("PageAccessSetup");

                entity.Property(e => e.ModifiedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PageModule>(entity =>
            {
                entity.Property(e => e.PageModuleName)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PasswordSetupSetting>(entity =>
            {
                entity.HasKey(e => e.PasswordSettingId);
            });

            modelBuilder.Entity<PrevEmploymentDetail>(entity =>
            {
                entity.HasKey(e => e.PrevEmploymentDetailsId);


                entity.Property(e => e.ContactPerson1)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.PrevEmployeeId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PrevEmploymentOrder)
                   .HasMaxLength(50)
                   .IsUnicode(false);

                entity.Property(e => e.VerifiedBy)
                  .HasMaxLength(50)
                  .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                 .HasMaxLength(50)
                 .IsUnicode(false);

                entity.Property(e => e.ContactPerson1No)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ContactPerson2)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ContactPerson2No)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ContactPerson3)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ContactPerson3No)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Designation)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.JoinedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LeavingDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LeavingReason)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.PrevEmploymentName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.PrevCompanyAddress)
                   .HasMaxLength(500)
                   .IsUnicode(false);

                entity.Property(e => e.VerificationComments)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.VerificationStatus)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.VerifiedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PreviousEmploymentDetail>(entity =>
            {
                entity.HasKey(e => e.PrevEmploymentId);

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PrevCompanyAddress)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.PrevCompanyDesignation)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.PrevCompanyName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.PrevFromDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PrevToDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PurchaseModuleScheme>(entity =>
            {
                entity.ToTable("PurchaseModuleScheme");

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.PurchaseModuleCode)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.PurchaseModuleName)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RoleMaster>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.ToTable("RoleMaster");

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RoleDescription)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.RoleName)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SettingsDataItem>(entity =>
            {
                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SettingsDataItemName)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.SettingsDataItemValue)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SettingsDataItemType>(entity =>
            {
                entity.HasKey(e => e.SettingsItemTypeId);

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SettingsItemTypeName)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Shift>(entity =>
            {
                entity.ToTable("Shift");

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FullDayMinimumHours)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HalfDayMinimumHours)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ShiftCode)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.ShiftName)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ShiftHoursCalculationScheme>(entity =>
            {
                entity.ToTable("ShiftHoursCalculationScheme");

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ShiftHoursCalculationSchemeName)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ShiftSession>(entity =>
            {
                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.GraceInTime)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.GraceOutTime)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.InMarginTime)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.InTime)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.OutMarginTime)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.OutTime)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ShiftSessionName)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserActionLog>(entity =>
            {
                entity.HasKey(e => e.SalogId)
                    .HasName("PK_ActionLogDetails");

                entity.ToTable("UserActionLog");

                entity.Property(e => e.SalogId).HasColumnName("SALogId");

                entity.Property(e => e.Action)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserDetail>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.HasIndex(e => e.UserName, "IX_UserDetails")
                    .IsUnique();

                entity.Property(e => e.Contact)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.IsPwdChangeFt).HasColumnName("IsPwdChangeFT");

                entity.Property(e => e.LastName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ProfilePicturePath)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserPassword)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
