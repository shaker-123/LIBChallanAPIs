using LIBChallanAPIs.Models;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    private readonly IHttpContextAccessor _httpContext;

    public AppDbContext(
        DbContextOptions<AppDbContext> options,
        IHttpContextAccessor httpContext)
        : base(options)
    {
        _httpContext = httpContext;
    }

    public DbSet<AppUser> MasterUsers { get; set; }
    public DbSet<Role> MasterRoles { get; set; }
    public DbSet<UserRole> MasterUserRoles { get; set; }
    public DbSet<CountryMaster> MasterCountries { get; set; }

    public DbSet<StateMaster> StateMasters { get; set; }
    public DbSet<GSTMaster> GSTMasters { get; set; }
    public DbSet<CityMaster> CityMasters { get; set; }
    public DbSet<GSTTypeMaster> GSTTypeMasters { get; set; }

    public DbSet<GSTSlabMaster> GSTSlabMasters { get; set; }

    public DbSet<DefectDetail> DefectDetails { get; set; }
    public DbSet<UserType> UserTypes { get; set; }
    public DbSet<CorrectiveAction> CorrectiveActions { get; set; }
    public DbSet<PartChangeMaster> PartChangeMasters { get; set; }
    public DbSet<BatteryStatus> BatteryStatuses { get; set; }
    public DbSet<EntityType> EntityTypes { get; set; }

    public DbSet<EntityMaster> EntityMasters { get; set; }
    public DbSet<Warehouse> Warehouses { get; set; }
    public DbSet<AddressMaster> AddressMasters { get; set; }

    public DbSet<OrgLegalDetail> OrgLegalDetails { get; set; }

    //public DbSet<ActivityStatus> ActivityStatus { get; set; }
    //public DbSet<BatteryTran> BatteryTrans { get; set;  }

    //public DbSet<ServiceActivity> ServiceActivities { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppUser>()
            .HasIndex(x => x.UserName)
            .IsUnique();

        modelBuilder.Entity<CountryMaster>()
        .HasIndex(x => x.CountryName)
        .IsUnique();

        modelBuilder.Entity<StateMaster>()
            .HasIndex(x => x.StateName)
            .IsUnique();

        modelBuilder.Entity<GSTTypeMaster>()
            .HasIndex(x => x.GSTTypeCode)
            .IsUnique();

        modelBuilder.Entity<AddressMaster>()
       .HasOne(a => a.AddressType)
       .WithMany()
       .HasForeignKey(a => a.AddressTypeId)
       .HasPrincipalKey(t => t.AddressTypeId);

        modelBuilder.Entity<StateMaster>()
       .HasOne(s => s.Country)
       .WithMany(c => c.States)
       .HasPrincipalKey(c => c.CountryId)
       .HasForeignKey(s => s.CountryId)
       .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<GSTMaster>()
       .HasOne(g => g.GSTSlab)
       .WithMany(s => s.GSTDetails)
       .HasPrincipalKey(s => s.GSTSlabId)
       .HasForeignKey(g => g.GSTSlabId)
       .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<GSTMaster>()
            .HasOne(g => g.GSTType)
            .WithMany(t => t.GSTMasters)
            .HasPrincipalKey(t => t.GSTTypeId)
            .HasForeignKey(g => g.GSTTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<CityMaster>()
        .HasOne(c => c.State)
        .WithMany(s => s.Cities)
        .HasPrincipalKey(s => s.StateId)
        .HasForeignKey(c => c.StateId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<OrgLegalDetail>()
        .HasOne(o => o.CityMaster)
        .WithMany(c => c.OrgLegalDetails)
        .HasPrincipalKey(c => c.CityId)
        .HasForeignKey(o => o.CityId);

        modelBuilder.Entity<OrgLegalDetail>()
        .HasOne(o => o.Customer)
        .WithMany()
        .HasPrincipalKey(e => e.EntityId)
        .HasForeignKey(o => o.EntityId);

        modelBuilder.Entity<UserRole>()
            .HasKey(ur => new { ur.UserRefId, ur.RoleId });

        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.UserRefId);

        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(ur => ur.RoleId);


        // ============================
        // UNIQUE BUSINESS KEYS
        // ============================

        modelBuilder.Entity<EntityMaster>()
            .HasIndex(e => e.EntityId)
            .IsUnique();

        modelBuilder.Entity<CityMaster>()
            .HasIndex(c => c.CityId)
            .IsUnique();

        modelBuilder.Entity<Warehouse>()
            .HasIndex(w => w.WarehouseId)
            .IsUnique();

        modelBuilder.Entity<EntityType>()
            .HasIndex(a => a.AddressTypeId)
            .IsUnique();

        // ============================
        // ADDRESSMASTER RELATIONSHIPS
        // ============================

        modelBuilder.Entity<AddressMaster>()
            .HasOne(a => a.Entity)
            .WithMany(e => e.Addresses)
            .HasForeignKey(a => a.EntityId)
            .HasPrincipalKey(e => e.EntityId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<AddressMaster>()
            .HasOne(a => a.City)
            .WithMany(c => c.Addresses)
            .HasForeignKey(a => a.CityId)
            .HasPrincipalKey(c => c.CityId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<AddressMaster>()
            .HasOne(a => a.Warehouse)
            .WithMany()
            .HasForeignKey(a => a.WarehouseId)
            .HasPrincipalKey(w => w.WarehouseId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<AddressMaster>()
            .HasOne(a => a.AddressType)
            .WithMany()
            .HasForeignKey(a => a.AddressTypeId)
            .HasPrincipalKey(t => t.AddressTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        // ============================
        // WAREHOUSE RELATIONSHIPS
        // ============================

        modelBuilder.Entity<Warehouse>()
            .HasOne(w => w.EntityM)
            .WithMany(e => e.Warehouses)
            .HasForeignKey(w => w.EntityId)
            .HasPrincipalKey(e => e.EntityId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Warehouse>()
            .HasOne(w => w.CityM)
            .WithMany(c => c.Warehouses)
            .HasForeignKey(w => w.CityId)
            .HasPrincipalKey(c => c.CityId)
            .OnDelete(DeleteBehavior.Restrict);


        // Seed User Types
        modelBuilder.Entity<UserType>().HasData(
        new UserType { Id = 1, TypeId = "UST001", TypeName = "ADMIN_USER", Description = "System administrator user", IsActive = true, CreatedAt = new DateTime(2026, 02, 11) },
        new UserType { Id = 2, TypeId = "UST002", TypeName = "SERVICE_USER", Description = "Service / FSE engineer user", IsActive = true, CreatedAt = new DateTime(2026, 02, 11) },
        new UserType { Id = 3, TypeId = "UST003", TypeName = "WAREHOUSE_USER", Description = "Warehouse operation user", IsActive = true, CreatedAt = new DateTime(2026, 02, 11) },
        new UserType { Id = 4, TypeId = "UST004", TypeName = "PLANT_USER", Description = "Plant operation user", IsActive = true, CreatedAt = new DateTime(2026, 02, 11) },
        new UserType { Id = 5, TypeId = "UST005", TypeName = "MIS_USER", Description = "MIS and reporting user", IsActive = true, CreatedAt = new DateTime(2026, 02, 11) }
        );

        // Seed Roles
        modelBuilder.Entity<Role>().HasData(
        new Role { RoleId = "RLM001", RoleName = "SUPER_ROLE", Description = "Full system access", IsActive = true, CreatedAt = new DateTime(2026, 02, 11) },
        new Role { RoleId = "RLM002", RoleName = "ADMIN_ROLE", Description = "Administrative access", IsActive = true, CreatedAt = new DateTime(2026, 02, 11) },
        new Role { RoleId = "RLM003", RoleName = "FSE_ROLE", Description = "Field Service Engineer role", IsActive = true, CreatedAt = new DateTime(2026, 02, 11) },
        new Role { RoleId = "RLM004", RoleName = "LOGISTIC_ROLE", Description = "Logistics and warehouse role", IsActive = true, CreatedAt = new DateTime(2026, 02, 11) },
        new Role { RoleId = "RLM005", RoleName = "MIS_ROLE", Description = "MIS and reporting user", IsActive = true, CreatedAt = new DateTime(2026, 02, 11) }
        );

        // Seed Users
        modelBuilder.Entity<AppUser>().HasData(
                new AppUser  /* Password Is For IdsUser = Ids@123 */
                {
                    Id = 1,
                    UserId = "URR001",
                    UserName = "IdsUser",
                    FullName = "Ids User",
                    Phone = "9999999999",
                    Email = "superuser@system.com",
                    /* Password Is For IdsUser = Ids@123 */
                    PasswordHash = new byte[] { 64, 134, 140, 119, 239, 133, 134, 172, 29, 254, 208, 51, 69, 93, 70, 240, 166, 5, 234, 189, 248, 101, 255, 181, 33, 60, 150, 251, 27, 102, 187, 135 },
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "System"
                },
                new AppUser  /* Password Is For LibAdminUser = LibAdmin@123 */
                {
                    Id = 2,
                    UserId = "URR002",
                    UserName = "LibAdminUser",
                    FullName = "Lib Admin User",
                    Phone = "8888888888",
                    Email = "admin@Lib.com",
                    /* Password Is For IdsUser = LibAdmin@123 */
                    PasswordHash = new byte[] { 165, 190, 135, 161, 62, 60, 231, 123, 67, 242, 42, 240, 30, 163, 16, 142, 138, 94, 110, 84, 99, 196, 240, 192, 23, 61, 88, 191, 16, 101, 27, 36 },
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "System"
                }
          );


        modelBuilder.Entity<UserRole>().HasData(
                  new UserRole { UserRefId = 1, RoleId = "RLM001", CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
                  new UserRole { UserRefId = 2, RoleId = "RLM002", CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" });


        modelBuilder.Entity<CountryMaster>().HasData(
     new CountryMaster { Id = 1, CountryId = "CNM001", CountryName = "India", Continent = "Asia", PhoneCode = "+91", CurrencyCode = "INR", IsActive = true, CreatedAt = new DateTime(2026, 1, 1), CreatedBy = "URR002" },
     new CountryMaster { Id = 2, CountryId = "CNM002", CountryName = "United States", Continent = "North America", PhoneCode = "+1", CurrencyCode = "USD", IsActive = false, CreatedAt = new DateTime(2026, 1, 1), CreatedBy = "URR002" },
     new CountryMaster { Id = 3, CountryId = "CNM003", CountryName = "United Kingdom", Continent = "Europe", PhoneCode = "+44", CurrencyCode = "GBP", IsActive = false, CreatedAt = new DateTime(2026, 1, 1), CreatedBy = "URR002" },
     new CountryMaster { Id = 4, CountryId = "CNM004", CountryName = "Canada", Continent = "North America", PhoneCode = "+1", CurrencyCode = "CAD", IsActive = false, CreatedAt = new DateTime(2026, 1, 1), CreatedBy = "URR002" },
     new CountryMaster { Id = 5, CountryId = "CNM005", CountryName = "Australia", Continent = "Oceania", PhoneCode = "+61", CurrencyCode = "AUD", IsActive = false, CreatedAt = new DateTime(2026, 1, 1), CreatedBy = "URR002" },
     new CountryMaster { Id = 6, CountryId = "CNM006", CountryName = "Germany", Continent = "Europe", PhoneCode = "+49", CurrencyCode = "EUR", IsActive = false, CreatedAt = new DateTime(2026, 1, 1), CreatedBy = "URR002" },
     new CountryMaster { Id = 7, CountryId = "CNM007", CountryName = "France", Continent = "Europe", PhoneCode = "+33", CurrencyCode = "EUR", IsActive = false, CreatedAt = new DateTime(2026, 1, 1), CreatedBy = "URR002" },
     new CountryMaster { Id = 8, CountryId = "CNM008", CountryName = "Japan", Continent = "Asia", PhoneCode = "+81", CurrencyCode = "JPY", IsActive = false, CreatedAt = new DateTime(2026, 1, 1), CreatedBy = "URR002" },
     new CountryMaster { Id = 9, CountryId = "CNM009", CountryName = "China", Continent = "Asia", PhoneCode = "+86", CurrencyCode = "CNY", IsActive = false, CreatedAt = new DateTime(2026, 1, 1), CreatedBy = "URR002" },
     new CountryMaster { Id = 10, CountryId = "CNM010", CountryName = "Brazil", Continent = "South America", PhoneCode = "+55", CurrencyCode = "BRL", IsActive = false, CreatedAt = new DateTime(2026, 1, 1), CreatedBy = "URR002" },
     new CountryMaster { Id = 11, CountryId = "CNM011", CountryName = "Mexico", Continent = "North America", PhoneCode = "+52", CurrencyCode = "MXN", IsActive = false, CreatedAt = new DateTime(2026, 1, 1), CreatedBy = "URR002" },
     new CountryMaster { Id = 12, CountryId = "CNM012", CountryName = "Russia", Continent = "Europe/Asia", PhoneCode = "+7", CurrencyCode = "RUB", IsActive = false, CreatedAt = new DateTime(2026, 1, 1), CreatedBy = "URR002" },
     new CountryMaster { Id = 13, CountryId = "CNM013", CountryName = "South Africa", Continent = "Africa", PhoneCode = "+27", CurrencyCode = "ZAR", IsActive = false, CreatedAt = new DateTime(2026, 1, 1), CreatedBy = "URR002" },
     new CountryMaster { Id = 14, CountryId = "CNM014", CountryName = "United Arab Emirates", Continent = "Asia", PhoneCode = "+971", CurrencyCode = "AED", IsActive = false, CreatedAt = new DateTime(2026, 1, 1), CreatedBy = "URR002" },
     new CountryMaster { Id = 15, CountryId = "CNM015", CountryName = "Saudi Arabia", Continent = "Asia", PhoneCode = "+966", CurrencyCode = "SAR", IsActive = false, CreatedAt = new DateTime(2026, 1, 1), CreatedBy = "URR002" },
     new CountryMaster { Id = 16, CountryId = "CNM016", CountryName = "Singapore", Continent = "Asia", PhoneCode = "+65", CurrencyCode = "SGD", IsActive = false, CreatedAt = new DateTime(2026, 1, 1), CreatedBy = "URR002" },
     new CountryMaster { Id = 17, CountryId = "CNM017", CountryName = "South Korea", Continent = "Asia", PhoneCode = "+82", CurrencyCode = "KRW", IsActive = false, CreatedAt = new DateTime(2026, 1, 1), CreatedBy = "URR002" },
     new CountryMaster { Id = 18, CountryId = "CNM018", CountryName = "Italy", Continent = "Europe", PhoneCode = "+39", CurrencyCode = "EUR", IsActive = false, CreatedAt = new DateTime(2026, 1, 1), CreatedBy = "URR002" },
     new CountryMaster { Id = 19, CountryId = "CNM019", CountryName = "Spain", Continent = "Europe", PhoneCode = "+34", CurrencyCode = "EUR", IsActive = false, CreatedAt = new DateTime(2026, 1, 1), CreatedBy = "URR002" },
     new CountryMaster { Id = 20, CountryId = "CNM020", CountryName = "Netherlands", Continent = "Europe", PhoneCode = "+31", CurrencyCode = "EUR", IsActive = false, CreatedAt = new DateTime(2026, 1, 1), CreatedBy = "URR002" }
 );



        // Seed Indian States (CountryId = 1 → India)
        var states = new List<StateMaster>
{
    new StateMaster { Id = 1,  StateId = "STM001", StateName = "Andhra Pradesh", CountryId = "CNM001", Region = "South India", GstCode = "37", IsActive = true, CreatedAt = new DateTime(2026,1,1), CreatedBy = "URR002" },
    new StateMaster { Id = 2,  StateId = "STM002", StateName = "Arunachal Pradesh", CountryId = "CNM001", Region = "North East India", GstCode = "12", IsActive = true, CreatedAt = new DateTime(2026,1,1), CreatedBy = "URR002" },
    new StateMaster { Id = 3,  StateId = "STM003", StateName = "Assam", CountryId = "CNM001", Region = "North East India", GstCode = "18", IsActive = true, CreatedAt = new DateTime(2026,1,1), CreatedBy = "URR002" },
    new StateMaster { Id = 4,  StateId = "STM004", StateName = "Bihar", CountryId = "CNM001", Region = "East India", GstCode = "10", IsActive = true, CreatedAt = new DateTime(2026,1,1), CreatedBy = "URR002" },
    new StateMaster { Id = 5,  StateId = "STM005", StateName = "Chhattisgarh", CountryId = "CNM001", Region = "Central India", GstCode = "22", IsActive = true, CreatedAt = new DateTime(2026,1,1), CreatedBy = "URR002" },
    new StateMaster { Id = 6,  StateId = "STM006", StateName = "Goa", CountryId = "CNM001", Region = "West India", GstCode = "30", IsActive = true, CreatedAt = new DateTime(2026,1,1), CreatedBy = "URR002" },
    new StateMaster { Id = 7,  StateId = "STM007", StateName = "Gujarat", CountryId = "CNM001", Region = "West India", GstCode = "24", IsActive = true, CreatedAt = new DateTime(2026,1,1), CreatedBy = "URR002" },
    new StateMaster { Id = 8,  StateId = "STM008", StateName = "Haryana", CountryId = "CNM001", Region = "North India", GstCode = "06", IsActive = true, CreatedAt = new DateTime(2026,1,1), CreatedBy = "URR002" },
    new StateMaster { Id = 9,  StateId = "STM009", StateName = "Himachal Pradesh", CountryId = "CNM001", Region = "North India", GstCode = "02", IsActive = true, CreatedAt = new DateTime(2026,1,1), CreatedBy = "URR002" },
    new StateMaster { Id = 10, StateId = "STM010", StateName = "Jharkhand", CountryId = "CNM001", Region = "East India", GstCode = "20", IsActive = true, CreatedAt = new DateTime(2026,1,1), CreatedBy = "URR002" },
    new StateMaster { Id = 11, StateId = "STM011", StateName = "Karnataka", CountryId = "CNM001", Region = "South India", GstCode = "29", IsActive = true, CreatedAt = new DateTime(2026,1,1), CreatedBy = "URR002" },
    new StateMaster { Id = 12, StateId = "STM012", StateName = "Kerala", CountryId = "CNM001", Region = "South India", GstCode = "32", IsActive = true, CreatedAt = new DateTime(2026,1,1), CreatedBy = "URR002" },
    new StateMaster { Id = 13, StateId = "STM013", StateName = "Madhya Pradesh", CountryId = "CNM001", Region = "Central India", GstCode = "23", IsActive = true, CreatedAt = new DateTime(2026,1,1), CreatedBy = "URR002" },
    new StateMaster { Id = 14, StateId = "STM014", StateName = "Maharashtra", CountryId = "CNM001", Region = "West India", GstCode = "27", IsActive = true, CreatedAt = new DateTime(2026,1,1), CreatedBy = "URR002" },
    new StateMaster { Id = 15, StateId = "STM015", StateName = "Manipur", CountryId = "CNM001", Region = "North East India", GstCode = "14", IsActive = true, CreatedAt = new DateTime(2026,1,1), CreatedBy = "URR002" },
    new StateMaster { Id = 16, StateId = "STM016", StateName = "Meghalaya", CountryId = "CNM001", Region = "North East India", GstCode = "17", IsActive = true, CreatedAt = new DateTime(2026,1,1), CreatedBy = "URR002" },
    new StateMaster { Id = 17, StateId = "STM017", StateName = "Mizoram", CountryId = "CNM001", Region = "North East India", GstCode = "15", IsActive = true, CreatedAt = new DateTime(2026,1,1), CreatedBy = "URR002" },
    new StateMaster { Id = 18, StateId = "STM018", StateName = "Nagaland", CountryId = "CNM001", Region = "North East India", GstCode = "13", IsActive = true, CreatedAt = new DateTime(2026,1,1), CreatedBy = "URR002" },
    new StateMaster { Id = 19, StateId = "STM019", StateName = "Odisha", CountryId = "CNM001", Region = "East India", GstCode = "21", IsActive = true, CreatedAt = new DateTime(2026,1,1), CreatedBy = "URR002" },
    new StateMaster { Id = 20, StateId = "STM020", StateName = "Punjab", CountryId = "CNM001", Region = "North India", GstCode = "03", IsActive = true, CreatedAt = new DateTime(2026,1,1), CreatedBy = "URR002" },
    new StateMaster { Id = 21, StateId = "STM021", StateName = "Rajasthan", CountryId = "CNM001", Region = "North India", GstCode = "08", IsActive = true, CreatedAt = new DateTime(2026,1,1), CreatedBy = "URR002" },
    new StateMaster { Id = 22, StateId = "STM022", StateName = "Sikkim", CountryId = "CNM001", Region = "North East India", GstCode = "11", IsActive = true, CreatedAt = new DateTime(2026,1,1), CreatedBy = "URR002" },
    new StateMaster { Id = 23, StateId = "STM023", StateName = "Tamil Nadu", CountryId = "CNM001", Region = "South India", GstCode = "33", IsActive = true, CreatedAt = new DateTime(2026,1,1), CreatedBy = "URR002" },
    new StateMaster { Id = 24, StateId = "STM024", StateName = "Telangana", CountryId = "CNM001", Region = "South India", GstCode = "36", IsActive = true, CreatedAt = new DateTime(2026,1,1), CreatedBy = "URR002" },
    new StateMaster { Id = 25, StateId = "STM025", StateName = "Tripura", CountryId = "CNM001", Region = "North East India", GstCode = "16", IsActive = true, CreatedAt = new DateTime(2026,1,1), CreatedBy = "URR002" },
    new StateMaster { Id = 26, StateId = "STM026", StateName = "Uttar Pradesh", CountryId = "CNM001", Region = "North India", GstCode = "09", IsActive = true, CreatedAt = new DateTime(2026,1,1), CreatedBy = "URR002" },
    new StateMaster { Id = 27, StateId = "STM027", StateName = "Uttarakhand", CountryId = "CNM001", Region = "North India", GstCode = "05", IsActive = true, CreatedAt = new DateTime(2026,1,1), CreatedBy = "URR002" },
    new StateMaster { Id = 28, StateId = "STM028", StateName = "West Bengal", CountryId = "CNM001", Region = "East India", GstCode = "19", IsActive = true, CreatedAt = new DateTime(2026,1,1), CreatedBy = "URR002" },
    new StateMaster { Id = 29, StateId = "STM029", StateName = "Delhi", CountryId = "CNM001", Region = "North India", GstCode = "07", IsActive = true, CreatedAt = new DateTime(2026,1,1), CreatedBy = "URR002" }
};

        modelBuilder.Entity<StateMaster>().HasData(states);

        var cities = new List<CityMaster>
{
    new CityMaster { Id = 1,  CityId="CTM001", CityName="New Delhi", StateId="STM029", PostalCode="110001", AreaCode="011", IsActive=true, CreatedAt=DateTime.UtcNow, CreatedBy = "URR002" },
    new CityMaster { Id = 2,  CityId="CTM002", CityName="North Delhi", StateId="STM029", PostalCode="110007", AreaCode="011", IsActive=true, CreatedAt=DateTime.UtcNow, CreatedBy = "URR002" },
    new CityMaster { Id = 3,  CityId="CTM003", CityName="South Delhi", StateId="STM029", PostalCode="110016", AreaCode="011", IsActive=true, CreatedAt=DateTime.UtcNow, CreatedBy = "URR002" },
    new CityMaster { Id = 4,  CityId="CTM004", CityName="Mumbai", StateId="STM014", PostalCode="400001", AreaCode="022", IsActive=true, CreatedAt=DateTime.UtcNow, CreatedBy = "URR002" },
    new CityMaster { Id = 5,  CityId="CTM005", CityName="Pune", StateId="STM014", PostalCode="411001", AreaCode="020", IsActive=true, CreatedAt=DateTime.UtcNow, CreatedBy = "URR002" },
    new CityMaster { Id = 6,  CityId="CTM006", CityName="Nagpur", StateId="STM014", PostalCode="440001", AreaCode="0712", IsActive=true, CreatedAt=DateTime.UtcNow, CreatedBy = "URR002" },
    new CityMaster { Id = 7,  CityId="CTM007", CityName="Bengaluru", StateId="STM011", PostalCode="560001", AreaCode="080", IsActive=true, CreatedAt=DateTime.UtcNow, CreatedBy = "URR002" },
    new CityMaster { Id = 8,  CityId="CTM008", CityName="Mysuru", StateId="STM011", PostalCode="570001", AreaCode="0821", IsActive=true, CreatedAt=DateTime.UtcNow, CreatedBy = "URR002" },
    new CityMaster { Id = 9,  CityId="CTM009", CityName="Chennai", StateId="STM023", PostalCode="600001", AreaCode="044", IsActive=true, CreatedAt=DateTime.UtcNow, CreatedBy = "URR002" },
    new CityMaster { Id = 10, CityId="CTM010", CityName="Coimbatore", StateId="STM023", PostalCode="641001", AreaCode="0422", IsActive=true, CreatedAt=DateTime.UtcNow, CreatedBy = "URR002" },
    new CityMaster { Id = 11, CityId="CTM011", CityName="Ahmedabad", StateId="STM007", PostalCode="380001", AreaCode="079", IsActive=true, CreatedAt=DateTime.UtcNow, CreatedBy = "URR002" },
    new CityMaster { Id = 12, CityId="CTM012", CityName="Surat", StateId="STM007", PostalCode="395003", AreaCode="0261", IsActive=true, CreatedAt=DateTime.UtcNow, CreatedBy = "URR002" },
    new CityMaster { Id = 13, CityId="CTM013", CityName="Kolkata", StateId="STM028", PostalCode="700001", AreaCode="033", IsActive=true, CreatedAt=DateTime.UtcNow, CreatedBy = "URR002" },
    new CityMaster { Id = 14, CityId="CTM014", CityName="Lucknow", StateId="STM026", PostalCode="226001", AreaCode="0522", IsActive=true, CreatedAt=DateTime.UtcNow, CreatedBy = "URR002" },
    new CityMaster { Id = 15, CityId="CTM015", CityName="Kanpur", StateId="STM026", PostalCode="208001", AreaCode="0512", IsActive=true, CreatedAt=DateTime.UtcNow, CreatedBy = "URR002" },
    new CityMaster { Id = 16, CityId="CTM016", CityName="Hyderabad", StateId="STM024", PostalCode="500001", AreaCode="040", IsActive=true, CreatedAt=DateTime.UtcNow, CreatedBy = "URR002" },
    new CityMaster { Id = 17, CityId="CTM017", CityName="Jaipur", StateId="STM021", PostalCode="302001", AreaCode="0141", IsActive=true, CreatedAt=DateTime.UtcNow, CreatedBy = "URR002" },
    new CityMaster { Id = 18, CityId="CTM018", CityName="Bhopal", StateId="STM013", PostalCode="462001", AreaCode="0755", IsActive=true, CreatedAt=DateTime.UtcNow, CreatedBy = "URR002" },
    new CityMaster { Id = 19, CityId="CTM019", CityName="Indore", StateId="STM013", PostalCode="452001", AreaCode="0731", IsActive=true, CreatedAt=DateTime.UtcNow, CreatedBy = "URR002" }};
        modelBuilder.Entity<CityMaster>().HasData(cities);


        modelBuilder.Entity<CorrectiveAction>().HasData(
            new CorrectiveAction { Id = 1, ActionId = "CRA001", ActionName = "Anderson connector screw fix", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
            new CorrectiveAction { Id = 2, ActionId = "CRA002", ActionName = "Battery Charged with wake up charger", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
            new CorrectiveAction { Id = 3, ActionId = "CRA003", ActionName = "Battery wake up with the charger", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
            new CorrectiveAction { Id = 4, ActionId = "CRA004", ActionName = "BMS reset & software update", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
            new CorrectiveAction { Id = 5, ActionId = "CRA005", ActionName = "CAN pin fixed", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
            new CorrectiveAction { Id = 6, ActionId = "CRA006", ActionName = "Charging & Discharging done", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
            new CorrectiveAction { Id = 7, ActionId = "CRA007", ActionName = "Handle fixed", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
            new CorrectiveAction { Id = 8, ActionId = "CRA008", ActionName = "Handle screw fixed", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
            new CorrectiveAction { Id = 9, ActionId = "CRA009", ActionName = "IOT glass changed", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
            new CorrectiveAction { Id = 10, ActionId = "CRA010", ActionName = "New connector clip fixed", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
            new CorrectiveAction { Id = 11, ActionId = "CRA011", ActionName = "New Handle fixed", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
            new CorrectiveAction { Id = 12, ActionId = "CRA012", ActionName = "RTF", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
            new CorrectiveAction { Id = 13, ActionId = "CRA013", ActionName = "Dead Battery", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
            new CorrectiveAction { Id = 14, ActionId = "CRA014", ActionName = "Top Screw fixed", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
            new CorrectiveAction { Id = 15, ActionId = "CRA015", ActionName = "NTC fixed / Replaced", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
            new CorrectiveAction { Id = 16, ActionId = "CRA016", ActionName = "CAN wire fixed", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
            new CorrectiveAction { Id = 17, ActionId = "CRA017", ActionName = "Fuse changed", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" });

        // Seed Part Change Master
        modelBuilder.Entity<PartChangeMaster>().HasData(
        new PartChangeMaster { Id = 1, PartId = "PCM001", PartName = "Anderson Bracket", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
        new PartChangeMaster { Id = 2, PartId = "PCM002", PartName = "Anderson connector Screw", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
        new PartChangeMaster { Id = 3, PartId = "PCM003", PartName = "Anderson SB 75", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
        new PartChangeMaster { Id = 4, PartId = "PCM004", PartName = "Handle + Handle Screw", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
        new PartChangeMaster { Id = 5, PartId = "PCM005", PartName = "Fuse", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
        new PartChangeMaster { Id = 6, PartId = "PCM006", PartName = "Top Cover Screw - M3 x 16", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
        new PartChangeMaster { Id = 7, PartId = "PCM007", PartName = "IOT Glass-Black", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
        new PartChangeMaster { Id = 8, PartId = "PCM008", PartName = "IOT Glass-White", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
        new PartChangeMaster { Id = 9, PartId = "PCM009", PartName = "NTC", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
        new PartChangeMaster { Id = 10, PartId = "PCM010", PartName = "IOT Screw - M3 x 12", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
        new PartChangeMaster { Id = 11, PartId = "PCM011", PartName = "Handle Screw", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" });


        modelBuilder.Entity<DefectDetail>().HasData(
            new DefectDetail { Id = 1, DefectTypeId = "DDT001", DefectName = "No issues", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
            new DefectDetail { Id = 2, DefectTypeId = "DDT002", DefectName = "CAN Pin Backout", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
            new DefectDetail { Id = 3, DefectTypeId = "DDT003", DefectName = "NTC Issue", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
            new DefectDetail { Id = 4, DefectTypeId = "DDT004", DefectName = "Cell Unbalance", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
            new DefectDetail { Id = 5, DefectTypeId = "DDT005", DefectName = "Battery Deep Discharge", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
            new DefectDetail { Id = 6, DefectTypeId = "DDT006", DefectName = "BMS Issue", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
            new DefectDetail { Id = 7, DefectTypeId = "DDT007", DefectName = "String Issue", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
            new DefectDetail { Id = 8, DefectTypeId = "DDT008", DefectName = "Handle Pin Missing", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
            new DefectDetail { Id = 9, DefectTypeId = "DDT009", DefectName = "Already Marked as RTF", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
            new DefectDetail { Id = 10, DefectTypeId = "DDT010", DefectName = "IOT Issue", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
            new DefectDetail { Id = 11, DefectTypeId = "DDT011", DefectName = "IOT Glass Damage", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
            new DefectDetail { Id = 12, DefectTypeId = "DDT012", DefectName = "CAN Communication Issue", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
            new DefectDetail { Id = 13, DefectTypeId = "DDT013", DefectName = "Handle Missing", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
            new DefectDetail { Id = 14, DefectTypeId = "DDT014", DefectName = "CAN Wire Damage", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
            new DefectDetail { Id = 15, DefectTypeId = "DDT015", DefectName = "Water Ingress", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
            new DefectDetail { Id = 16, DefectTypeId = "DDT016", DefectName = "Fuse Burn", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
            new DefectDetail { Id = 17, DefectTypeId = "DDT017", DefectName = "Connector Clip Missing", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
            new DefectDetail { Id = 18, DefectTypeId = "DDT018", DefectName = "Connector Damaged", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
            new DefectDetail { Id = 19, DefectTypeId = "DDT019", DefectName = "CAN Wire Cut", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
            new DefectDetail { Id = 20, DefectTypeId = "DDT020", DefectName = "Unbalance & IOT Glass Damage", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
            new DefectDetail { Id = 21, DefectTypeId = "DDT021", DefectName = "Top Cover Screw Missing", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
            new DefectDetail { Id = 22, DefectTypeId = "DDT022", DefectName = "BMS Sleep Mode", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
            new DefectDetail { Id = 23, DefectTypeId = "DDT023", DefectName = "CAN Wire Broken", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
            new DefectDetail { Id = 24, DefectTypeId = "DDT024", DefectName = "Anderson Connector Burn", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
            new DefectDetail { Id = 25, DefectTypeId = "DDT025", DefectName = "Anderson Connector Screw Missing", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
            new DefectDetail { Id = 26, DefectTypeId = "DDT026", DefectName = "Battery is Tempered", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
            new DefectDetail { Id = 27, DefectTypeId = "DDT027", DefectName = "Already Repaired by IA", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" });


        modelBuilder.Entity<BatteryStatus>().HasData(
            new BatteryStatus { Id = 1, StatusId = "BST001", StatusName = "Repaired", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
            new BatteryStatus { Id = 2, StatusId = "BST002", StatusName = "Return to Factory", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
            new BatteryStatus { Id = 3, StatusId = "BST003", StatusName = "Hold", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
            new BatteryStatus { Id = 4, StatusId = "BST004", StatusName = "Dead Battery", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
            new BatteryStatus { Id = 5, StatusId = "BST005", StatusName = "Opened at Battery Smart WH", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" }
        );

        // Seed GST Types
        modelBuilder.Entity<GSTTypeMaster>().HasData(
            new GSTTypeMaster { Id = 2, GSTTypeId = "TXT001", GSTTypeCode = "CGST", GSTTypeName = "Central Goods and Services Tax", IsActive = true },
            new GSTTypeMaster { Id = 3, GSTTypeId = "TXT002", GSTTypeCode = "SGST", GSTTypeName = "State Goods and Services Tax", IsActive = true },
            new GSTTypeMaster { Id = 4, GSTTypeId = "TXT003", GSTTypeCode = "IGST", GSTTypeName = "Integrated Goods and Services Tax", IsActive = true },
            new GSTTypeMaster { Id = 5, GSTTypeId = "TXT004", GSTTypeCode = "UTGST", GSTTypeName = "Union Territory Goods and Services Tax", IsActive = true },
            new GSTTypeMaster { Id = 6, GSTTypeId = "TXT005", GSTTypeCode = "CESS", GSTTypeName = "GST Compensation Cess", IsActive = true },
            new GSTTypeMaster { Id = 7, GSTTypeId = "TXT006", GSTTypeCode = "RCM", GSTTypeName = "Reverse Charge Mechanism", IsActive = true });

        modelBuilder.Entity<GSTSlabMaster>().HasData(
            new GSTSlabMaster { Id = 1, GSTSlabId = "TXS001", SlabCode = "GST_0", TotalPercentage = 0, IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
            new GSTSlabMaster { Id = 2, GSTSlabId = "TXS002", SlabCode = "GST_5", TotalPercentage = 5, IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
            new GSTSlabMaster { Id = 3, GSTSlabId = "TXS003", SlabCode = "GST_12", TotalPercentage = 12, IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
            new GSTSlabMaster { Id = 4, GSTSlabId = "TXS004", SlabCode = "GST_18", TotalPercentage = 18, IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
            new GSTSlabMaster { Id = 5, GSTSlabId = "TXS005", SlabCode = "GST_28", TotalPercentage = 28, IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" }
        );

        modelBuilder.Entity<GSTMaster>().HasData(
            // ================= 0% =================
            new GSTMaster { Id = 1, GSTId = "TAX001", GSTSlabId = "TXS001", GSTTypeId = "TXT001", GSTPercentage = 0, EffectiveFrom = new DateTime(2020, 1, 1), IsActive = true },
            new GSTMaster { Id = 2, GSTId = "TAX002", GSTSlabId = "TXS001", GSTTypeId = "TXT002", GSTPercentage = 0, EffectiveFrom = new DateTime(2020, 1, 1), IsActive = true },
            new GSTMaster { Id = 3, GSTId = "TAX003", GSTSlabId = "TXS001", GSTTypeId = "TXT003", GSTPercentage = 0, EffectiveFrom = new DateTime(2020, 1, 1), IsActive = true },
            // ================= 5% =================
            new GSTMaster { Id = 4, GSTId = "TAX004", GSTSlabId = "TXS002", GSTTypeId = "TXT001", GSTPercentage = 2.5m, EffectiveFrom = new DateTime(2020, 1, 1), IsActive = true },
            new GSTMaster { Id = 5, GSTId = "TAX005", GSTSlabId = "TXS002", GSTTypeId = "TXT002", GSTPercentage = 2.5m, EffectiveFrom = new DateTime(2020, 1, 1), IsActive = true },
            new GSTMaster { Id = 6, GSTId = "TAX006", GSTSlabId = "TXS002", GSTTypeId = "TXT003", GSTPercentage = 5m, EffectiveFrom = new DateTime(2020, 1, 1), IsActive = true },
            // ================= 12% =================
            new GSTMaster { Id = 7, GSTId = "TAX007", GSTSlabId = "TXS003", GSTTypeId = "TXT001", GSTPercentage = 6m, EffectiveFrom = new DateTime(2020, 1, 1), IsActive = true },
            new GSTMaster { Id = 8, GSTId = "TAX008", GSTSlabId = "TXS003", GSTTypeId = "TXT002", GSTPercentage = 6m, EffectiveFrom = new DateTime(2020, 1, 1), IsActive = true },
            new GSTMaster { Id = 9, GSTId = "TAX009", GSTSlabId = "TXS003", GSTTypeId = "TXT003", GSTPercentage = 12m, EffectiveFrom = new DateTime(2020, 1, 1), IsActive = true },
            // ================= 18% =================
            new GSTMaster { Id = 10, GSTId = "TAX010", GSTSlabId = "TXS004", GSTTypeId = "TXT001", GSTPercentage = 9m, EffectiveFrom = new DateTime(2020, 1, 1), IsActive = true },
            new GSTMaster { Id = 11, GSTId = "TAX011", GSTSlabId = "TXS004", GSTTypeId = "TXT002", GSTPercentage = 9m, EffectiveFrom = new DateTime(2020, 1, 1), IsActive = true },
            new GSTMaster { Id = 12, GSTId = "TAX012", GSTSlabId = "TXS004", GSTTypeId = "TXT003", GSTPercentage = 18m, EffectiveFrom = new DateTime(2020, 1, 1), IsActive = true },
            // ================= 28% =================
            new GSTMaster { Id = 13, GSTId = "TAX013", GSTSlabId = "TXS005", GSTTypeId = "TXT001", GSTPercentage = 14m, EffectiveFrom = new DateTime(2020, 1, 1), IsActive = true },
            new GSTMaster { Id = 14, GSTId = "TAX014", GSTSlabId = "TXS005", GSTTypeId = "TXT002", GSTPercentage = 14m, EffectiveFrom = new DateTime(2020, 1, 1), IsActive = true },
            new GSTMaster { Id = 15, GSTId = "TAX015", GSTSlabId = "TXS005", GSTTypeId = "TXT003", GSTPercentage = 28m, EffectiveFrom = new DateTime(2020, 1, 1), IsActive = true }
        );


        modelBuilder.Entity<EntityType>().HasData(
            new EntityType { Id = 1, AddressTypeId = "ADRT01", TypeCode = "CUSTOMER_WH", TypeName = "Customer Warehouse", IsActive = true, CreatedAt = new DateTime(2024, 1, 1), CreatedBy = "URR002" },
            new EntityType { Id = 2, AddressTypeId = "ADRT02", TypeCode = "DELHI_WH", TypeName = "DELHI Warehouse", IsActive = true, CreatedAt = new DateTime(2024, 1, 1), CreatedBy = "URR002" },
            new EntityType { Id = 3, AddressTypeId = "ADRT03", TypeCode = "PLANT_WH", TypeName = "Plant Warehouse", IsActive = true, CreatedAt = new DateTime(2024, 1, 1), CreatedBy = "URR002" },
            new EntityType { Id = 4, AddressTypeId = "ADRT04", TypeCode = "CUSTOMER_MN", TypeName = "Customer Main", IsActive = true, CreatedAt = new DateTime(2024, 1, 1), CreatedBy = "URR002" });


        modelBuilder.Entity<EntityMaster>().HasData(
            new EntityMaster { Id = 1, EntityId = "ETM001", EntityName = "Battery Smart Pvt Ltd", IsActive = true, CreatedAt = new DateTime(2026, 1, 1), CreatedBy = "URR002" },
            new EntityMaster { Id = 2, EntityId = "ETM002", EntityName = "Upgrid Solutions Pvt Ltd", IsActive = true, CreatedAt = new DateTime(2026, 1, 1), CreatedBy = "URR002" },
            new EntityMaster { Id = 3, EntityId = "ETM003", EntityName = "Mumbai Batteries Pvt Ltd", IsActive = true, CreatedAt = new DateTime(2026, 1, 1), CreatedBy = "URR002" }, 
            new EntityMaster { Id = 4, EntityId = "ETM004", EntityName = "Global Eneergy Service Pvt Ltd", IsActive = true, CreatedAt = new DateTime(2026, 1, 1), CreatedBy = "URR002" },
            new EntityMaster { Id = 5, EntityId = "ETM005", EntityName = "Bangalore Eneergy Solutions Pvt Ltd", IsActive = true, CreatedAt = new DateTime(2026, 1, 1), CreatedBy = "URR002" });

        modelBuilder.Entity<Warehouse>().HasData(
    new Warehouse { Id = 1, WarehouseId = "WHS001", WarehouseCode = "NewDelhi-WH001", CityId = "CTM001", EntityId = "ETM001", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
    new Warehouse { Id = 2, WarehouseId = "WHS002", WarehouseCode = "NorthDelhi-WH002", CityId = "CTM002", EntityId = "ETM002", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
    new Warehouse { Id = 3, WarehouseId = "WHS003", WarehouseCode = "Mumbai-WH003", CityId = "CTM003", EntityId = "ETM003", IsActive = false, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" });

        modelBuilder.Entity<AddressMaster>().HasData(
      new AddressMaster { Id = 1, AddressId = "ADR001", EntityId = "ETM001", WarehouseId = "WHS001", AddressTypeId = "ADRT02", AddressLine1 = "123, Connaught Place", AddressLine2 = "Near India Gate", CityId = "CTM001", PostalCode = "110001", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
      new AddressMaster { Id = 2, AddressId = "ADR002", EntityId = "ETM002", WarehouseId = "WHS002", AddressTypeId = "ADRT02", AddressLine1 = "45, Rohini Sector 12", AddressLine2 = null, CityId = "CTM002", PostalCode = "110007", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
      new AddressMaster { Id = 3, AddressId = "ADR003", EntityId = "ETM003", WarehouseId = "WHS003", AddressTypeId = "ADRT03", AddressLine1 = "789, Andheri East", AddressLine2 = "Near Chhatrapati Complex", CityId = "CTM004", PostalCode = "400001", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
      new AddressMaster { Id = 4, AddressId = "ADR004", EntityId = "ETM004", WarehouseId = "WHS001", AddressTypeId = "ADRT02", AddressLine1 = "56, Laxmi Nagar", AddressLine2 = null, CityId = "CTM003", PostalCode = "110016", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
      new AddressMaster { Id = 5, AddressId = "ADR005", EntityId = "ETM005", WarehouseId = null, AddressTypeId = "ADRT01", AddressLine1 = "22, MG Road", AddressLine2 = null, CityId = "CTM007", PostalCode = "560001", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" });

        modelBuilder.Entity<OrgLegalDetail>().HasData(
            new OrgLegalDetail { Id = 1, LegalId = "OLD001", EntityId = "ETM001", GSTINNumber = "27ABCDE1234F1Z5", CINNumber = "L12345MH2020PTC123456", PANNumber = "ABCDE1234F", CityId = "CTM004", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
            new OrgLegalDetail { Id = 2, LegalId = "OLD002", EntityId = "ETM002", GSTINNumber = "29ABCDE5678G1Z6", CINNumber = "L23456DL2021PTC654321", PANNumber = "ABCDE5678G", CityId = "CTM001", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
            new OrgLegalDetail { Id = 5, LegalId = "OLD003", EntityId = "ETM002", GSTINNumber = "36ABCDE7777K1Z9", CINNumber = "L56789TS2024PTC555666", PANNumber = "ABCDE7777K", CityId = "CTM016", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" });


        modelBuilder.Entity<ActivityStatus>().HasData(
                new ActivityStatus { Id = 1, StatusId = "ACT001", StatusName = "Open", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
                new ActivityStatus { Id = 2, StatusId = "ACT002", StatusName = "InProgress", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" },
                new ActivityStatus { Id = 3, StatusId = "ACT003", StatusName = "Closed", IsActive = true, CreatedAt = DateTime.UtcNow, CreatedBy = "URR002" });


        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(
     CancellationToken cancellationToken = default)
    {
        var userId = GetCurrentUserId();

        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = DateTime.UtcNow;
                entry.Entity.CreatedBy = userId;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = DateTime.UtcNow;
                entry.Entity.UpdatedBy = userId;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }


    private string? GetCurrentUserId()
    {
        return _httpContext.HttpContext?
            .User?
            .FindFirst("UserId")?
            .Value;
    }


}
