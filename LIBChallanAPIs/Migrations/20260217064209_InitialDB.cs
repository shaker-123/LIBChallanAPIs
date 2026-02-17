using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LIBChallanAPIs.Migrations
{
    /// <inheritdoc />
    public partial class InitialDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActivityStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    StatusName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityStatus", x => x.Id);
                    table.UniqueConstraint("AK_ActivityStatus_StatusId", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "BatteryStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    StatusName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatteryStatuses", x => x.Id);
                    table.UniqueConstraint("AK_BatteryStatuses_StatusId", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "CorrectiveActions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActionId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorrectiveActions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DefectDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DefectTypeId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DefectName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefectDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EntityTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityTypeId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TypeCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityTypes", x => x.Id);
                    table.UniqueConstraint("AK_EntityTypes_EntityTypeId", x => x.EntityTypeId);
                });

            migrationBuilder.CreateTable(
                name: "FirmwareStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirmwareStatusId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    FirmwareStatusName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BatteryType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FirmwareStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GSTSlabMasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GSTSlabId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    SlabCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GSTSlabMasters", x => x.Id);
                    table.UniqueConstraint("AK_GSTSlabMasters_GSTSlabId", x => x.GSTSlabId);
                });

            migrationBuilder.CreateTable(
                name: "GSTTypeMasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GSTTypeId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    GSTTypeCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GSTTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GSTTypeMasters", x => x.Id);
                    table.UniqueConstraint("AK_GSTTypeMasters_GSTTypeId", x => x.GSTTypeId);
                });

            migrationBuilder.CreateTable(
                name: "MasterCountries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CountryName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Continent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrencyCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterCountries", x => x.Id);
                    table.UniqueConstraint("AK_MasterCountries_CountryId", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "MasterRoles",
                columns: table => new
                {
                    RoleId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterRoles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "PartChangeMasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    PartName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartChangeMasters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EntityMasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    EntityName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    EntityTypeId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ContactPerson = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityMasters", x => x.Id);
                    table.UniqueConstraint("AK_EntityMasters_EntityId", x => x.EntityId);
                    table.ForeignKey(
                        name: "FK_EntityMasters_EntityTypes_EntityTypeId",
                        column: x => x.EntityTypeId,
                        principalTable: "EntityTypes",
                        principalColumn: "EntityTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GSTMasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GSTId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    GSTSlabId = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    GSTTypeId = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    GSTPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EffectiveFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EffectiveTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GSTMasters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GSTMasters_GSTSlabMasters_GSTSlabId",
                        column: x => x.GSTSlabId,
                        principalTable: "GSTSlabMasters",
                        principalColumn: "GSTSlabId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GSTMasters_GSTTypeMasters_GSTTypeId",
                        column: x => x.GSTTypeId,
                        principalTable: "GSTTypeMasters",
                        principalColumn: "GSTTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StateMasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StateId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    StateName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CountryId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GstCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateMasters", x => x.Id);
                    table.UniqueConstraint("AK_StateMasters_StateId", x => x.StateId);
                    table.ForeignKey(
                        name: "FK_StateMasters_MasterCountries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "MasterCountries",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserTypeId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.UniqueConstraint("AK_Users_UserId", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_UserTypes_UserTypeId",
                        column: x => x.UserTypeId,
                        principalTable: "UserTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CityMasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CityName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StateId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AreaCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityMasters", x => x.Id);
                    table.UniqueConstraint("AK_CityMasters_CityId", x => x.CityId);
                    table.ForeignKey(
                        name: "FK_CityMasters_StateMasters_StateId",
                        column: x => x.StateId,
                        principalTable: "StateMasters",
                        principalColumn: "StateId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MasterUserRoles",
                columns: table => new
                {
                    UserRefId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterUserRoles", x => new { x.UserRefId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_MasterUserRoles_MasterRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "MasterRoles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MasterUserRoles_Users_UserRefId",
                        column: x => x.UserRefId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrgLegalDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LegalId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    EntityId = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    GSTINNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    CINNumber = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    PANNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CityId = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrgLegalDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrgLegalDetails_CityMasters_CityId",
                        column: x => x.CityId,
                        principalTable: "CityMasters",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrgLegalDetails_EntityMasters_EntityId",
                        column: x => x.EntityId,
                        principalTable: "EntityMasters",
                        principalColumn: "EntityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WarehouseId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    WarehouseCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    EntityId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.Id);
                    table.UniqueConstraint("AK_Warehouses_WarehouseId", x => x.WarehouseId);
                    table.ForeignKey(
                        name: "FK_Warehouses_CityMasters_CityId",
                        column: x => x.CityId,
                        principalTable: "CityMasters",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Warehouses_EntityMasters_EntityId",
                        column: x => x.EntityId,
                        principalTable: "EntityMasters",
                        principalColumn: "EntityId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AddressMasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    EntityId = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    WarehouseId = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    EntityTypeId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    AddressLine1 = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    AddressLine2 = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CityId = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressMasters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AddressMasters_CityMasters_CityId",
                        column: x => x.CityId,
                        principalTable: "CityMasters",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AddressMasters_EntityMasters_EntityId",
                        column: x => x.EntityId,
                        principalTable: "EntityMasters",
                        principalColumn: "EntityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AddressMasters_EntityTypes_EntityTypeId",
                        column: x => x.EntityTypeId,
                        principalTable: "EntityTypes",
                        principalColumn: "EntityTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AddressMasters_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "WarehouseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServiceActivities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ActivityEngineerId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    EntityId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    WarehouseId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    StatusId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ActivityDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumberOfBatteriesOnSite = table.Column<int>(type: "int", nullable: false),
                    NumberOfBatteriesOnSiteRTF = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceActivities", x => x.Id);
                    table.UniqueConstraint("AK_ServiceActivities_ActivityId", x => x.ActivityId);
                    table.ForeignKey(
                        name: "FK_ServiceActivities_ActivityStatus_StatusId",
                        column: x => x.StatusId,
                        principalTable: "ActivityStatus",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceActivities_EntityMasters_EntityId",
                        column: x => x.EntityId,
                        principalTable: "EntityMasters",
                        principalColumn: "EntityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceActivities_Users_ActivityEngineerId",
                        column: x => x.ActivityEngineerId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceActivities_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "WarehouseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BatteryTrans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BatteryTransId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ActivityId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CurrentStatusId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    WarehouseId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    BatterySerial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BatteryIdInLIBSystem = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Barcode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatteryTrans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BatteryTrans_BatteryStatuses_CurrentStatusId",
                        column: x => x.CurrentStatusId,
                        principalTable: "BatteryStatuses",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BatteryTrans_EntityMasters_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "EntityMasters",
                        principalColumn: "EntityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BatteryTrans_ServiceActivities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "ServiceActivities",
                        principalColumn: "ActivityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BatteryTrans_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "WarehouseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "ActivityStatus",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsActive", "StatusId", "StatusName", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(3312), "URR002", true, "ACT001", "Open", null, null },
                    { 2, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(3317), "URR002", true, "ACT002", "InProgress", null, null },
                    { 3, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(3320), "URR002", true, "ACT003", "Closed", null, null }
                });

            migrationBuilder.InsertData(
                table: "BatteryStatuses",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsActive", "StatusId", "StatusName", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(2267), "URR002", true, "BST001", "Repaired", null, null },
                    { 2, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(2271), "URR002", true, "BST002", "Return to Factory", null, null },
                    { 3, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(2274), "URR002", true, "BST003", "Hold", null, null },
                    { 4, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(2277), "URR002", true, "BST004", "Dead Battery", null, null },
                    { 5, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(2280), "URR002", true, "BST005", "Opened at Battery Smart WH", null, null }
                });

            migrationBuilder.InsertData(
                table: "CorrectiveActions",
                columns: new[] { "Id", "ActionId", "ActionName", "CreatedAt", "CreatedBy", "IsActive", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, "CRA001", "Anderson connector screw fix", new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(1630), "URR002", true, null, null },
                    { 2, "CRA002", "Battery Charged with wake up charger", new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(1635), "URR002", true, null, null },
                    { 3, "CRA003", "Battery wake up with the charger", new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(1638), "URR002", true, null, null },
                    { 4, "CRA004", "BMS reset & software update", new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(1641), "URR002", true, null, null },
                    { 5, "CRA005", "CAN pin fixed", new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(1644), "URR002", true, null, null },
                    { 6, "CRA006", "Charging & Discharging done", new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(1647), "URR002", true, null, null },
                    { 7, "CRA007", "Handle fixed", new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(1651), "URR002", true, null, null },
                    { 8, "CRA008", "Handle screw fixed", new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(1655), "URR002", true, null, null },
                    { 9, "CRA009", "IOT glass changed", new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(1659), "URR002", true, null, null },
                    { 10, "CRA010", "New connector clip fixed", new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(1663), "URR002", true, null, null },
                    { 11, "CRA011", "New Handle fixed", new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(1665), "URR002", true, null, null },
                    { 12, "CRA012", "RTF", new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(1668), "URR002", true, null, null },
                    { 13, "CRA013", "Dead Battery", new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(1672), "URR002", true, null, null },
                    { 14, "CRA014", "Top Screw fixed", new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(1675), "URR002", true, null, null },
                    { 15, "CRA015", "NTC fixed / Replaced", new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(1678), "URR002", true, null, null },
                    { 16, "CRA016", "CAN wire fixed", new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(1681), "URR002", true, null, null },
                    { 17, "CRA017", "Fuse changed", new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(1684), "URR002", true, null, null }
                });

            migrationBuilder.InsertData(
                table: "DefectDetails",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DefectName", "DefectTypeId", "IsActive", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(2061), "URR002", "No issues", "DDT001", true, null, null },
                    { 2, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(2065), "URR002", "CAN Pin Backout", "DDT002", true, null, null },
                    { 3, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(2068), "URR002", "NTC Issue", "DDT003", true, null, null },
                    { 4, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(2071), "URR002", "Cell Unbalance", "DDT004", true, null, null },
                    { 5, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(2074), "URR002", "Battery Deep Discharge", "DDT005", true, null, null },
                    { 6, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(2078), "URR002", "BMS Issue", "DDT006", true, null, null },
                    { 7, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(2081), "URR002", "String Issue", "DDT007", true, null, null },
                    { 8, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(2084), "URR002", "Handle Pin Missing", "DDT008", true, null, null },
                    { 9, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(2087), "URR002", "Already Marked as RTF", "DDT009", true, null, null },
                    { 10, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(2090), "URR002", "IOT Issue", "DDT010", true, null, null },
                    { 11, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(2094), "URR002", "IOT Glass Damage", "DDT011", true, null, null },
                    { 12, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(2096), "URR002", "CAN Communication Issue", "DDT012", true, null, null },
                    { 13, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(2099), "URR002", "Handle Missing", "DDT013", true, null, null },
                    { 14, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(2102), "URR002", "CAN Wire Damage", "DDT014", true, null, null },
                    { 15, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(2106), "URR002", "Water Ingress", "DDT015", true, null, null },
                    { 16, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(2109), "URR002", "Fuse Burn", "DDT016", true, null, null },
                    { 17, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(2112), "URR002", "Connector Clip Missing", "DDT017", true, null, null },
                    { 18, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(2115), "URR002", "Connector Damaged", "DDT018", true, null, null },
                    { 19, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(2118), "URR002", "CAN Wire Cut", "DDT019", true, null, null },
                    { 20, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(2120), "URR002", "Unbalance & IOT Glass Damage", "DDT020", true, null, null },
                    { 21, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(2123), "URR002", "Top Cover Screw Missing", "DDT021", true, null, null },
                    { 22, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(2126), "URR002", "BMS Sleep Mode", "DDT022", true, null, null },
                    { 23, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(2129), "URR002", "CAN Wire Broken", "DDT023", true, null, null },
                    { 24, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(2132), "URR002", "Anderson Connector Burn", "DDT024", true, null, null },
                    { 25, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(2134), "URR002", "Anderson Connector Screw Missing", "DDT025", true, null, null },
                    { 26, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(2137), "URR002", "Battery is Tempered", "DDT026", true, null, null },
                    { 27, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(2140), "URR002", "Already Repaired by IA", "DDT027", true, null, null }
                });

            migrationBuilder.InsertData(
                table: "EntityTypes",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "EntityTypeId", "IsActive", "TypeCode", "TypeName", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "URR002", "ETY001", true, "CUSTOMER_WH", "Customer Warehouse", null, null },
                    { 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "URR002", "ETY002", true, "DELHI_WH", "DELHI Warehouse", null, null },
                    { 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "URR002", "ETY003", true, "PLANT_WH", "Plant Warehouse", null, null },
                    { 4, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "URR002", "ETY004", true, "CUSTOMER_MN", "Customer Main", null, null }
                });

            migrationBuilder.InsertData(
                table: "FirmwareStatuses",
                columns: new[] { "Id", "BatteryType", "CreatedAt", "CreatedBy", "FirmwareStatusId", "FirmwareStatusName", "IsActive", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, "45Ah", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "FWS001", "Updated", true, null, null },
                    { 2, "45Ah", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "FWS002", "Not Updated", true, null, null },
                    { 3, "45Ah", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "FWS003", "Already Updated", true, null, null }
                });

            migrationBuilder.InsertData(
                table: "GSTSlabMasters",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "GSTSlabId", "IsActive", "SlabCode", "TotalPercentage", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(2460), "URR002", "TXS001", true, "GST_0", 0m, null, null },
                    { 2, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(2470), "URR002", "TXS002", true, "GST_5", 5m, null, null },
                    { 3, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(2474), "URR002", "TXS003", true, "GST_12", 12m, null, null },
                    { 4, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(2479), "URR002", "TXS004", true, "GST_18", 18m, null, null },
                    { 5, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(2483), "URR002", "TXS005", true, "GST_28", 28m, null, null }
                });

            migrationBuilder.InsertData(
                table: "GSTTypeMasters",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "GSTTypeCode", "GSTTypeId", "GSTTypeName", "IsActive", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "CGST", "TXT001", "Central Goods and Services Tax", true, null, null },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "SGST", "TXT002", "State Goods and Services Tax", true, null, null },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "IGST", "TXT003", "Integrated Goods and Services Tax", true, null, null },
                    { 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "UTGST", "TXT004", "Union Territory Goods and Services Tax", true, null, null },
                    { 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "CESS", "TXT005", "GST Compensation Cess", true, null, null },
                    { 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "RCM", "TXT006", "Reverse Charge Mechanism", true, null, null }
                });

            migrationBuilder.InsertData(
                table: "MasterCountries",
                columns: new[] { "Id", "Continent", "CountryId", "CountryName", "CreatedAt", "CreatedBy", "CurrencyCode", "IsActive", "PhoneCode", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, "Asia", "CNM001", "India", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "URR002", "INR", true, "+91", null, null },
                    { 2, "North America", "CNM002", "United States", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "URR002", "USD", false, "+1", null, null },
                    { 3, "Europe", "CNM003", "United Kingdom", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "URR002", "GBP", false, "+44", null, null },
                    { 4, "North America", "CNM004", "Canada", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "URR002", "CAD", false, "+1", null, null },
                    { 5, "Oceania", "CNM005", "Australia", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "URR002", "AUD", false, "+61", null, null },
                    { 6, "Europe", "CNM006", "Germany", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "URR002", "EUR", false, "+49", null, null },
                    { 7, "Europe", "CNM007", "France", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "URR002", "EUR", false, "+33", null, null },
                    { 8, "Asia", "CNM008", "Japan", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "URR002", "JPY", false, "+81", null, null },
                    { 9, "Asia", "CNM009", "China", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "URR002", "CNY", false, "+86", null, null },
                    { 10, "South America", "CNM010", "Brazil", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "URR002", "BRL", false, "+55", null, null },
                    { 11, "North America", "CNM011", "Mexico", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "URR002", "MXN", false, "+52", null, null },
                    { 12, "Europe/Asia", "CNM012", "Russia", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "URR002", "RUB", false, "+7", null, null },
                    { 13, "Africa", "CNM013", "South Africa", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "URR002", "ZAR", false, "+27", null, null },
                    { 14, "Asia", "CNM014", "United Arab Emirates", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "URR002", "AED", false, "+971", null, null },
                    { 15, "Asia", "CNM015", "Saudi Arabia", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "URR002", "SAR", false, "+966", null, null },
                    { 16, "Asia", "CNM016", "Singapore", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "URR002", "SGD", false, "+65", null, null },
                    { 17, "Asia", "CNM017", "South Korea", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "URR002", "KRW", false, "+82", null, null },
                    { 18, "Europe", "CNM018", "Italy", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "URR002", "EUR", false, "+39", null, null },
                    { 19, "Europe", "CNM019", "Spain", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "URR002", "EUR", false, "+34", null, null },
                    { 20, "Europe", "CNM020", "Netherlands", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "URR002", "EUR", false, "+31", null, null }
                });

            migrationBuilder.InsertData(
                table: "MasterRoles",
                columns: new[] { "RoleId", "CreatedAt", "CreatedBy", "Description", "IsActive", "RoleName", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { "RLM001", new DateTime(2026, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Full system access", true, "SUPER_ROLE", null, null },
                    { "RLM002", new DateTime(2026, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Administrative access", true, "ADMIN_ROLE", null, null },
                    { "RLM003", new DateTime(2026, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Field Service Engineer role", true, "FSE_ROLE", null, null },
                    { "RLM004", new DateTime(2026, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Logistics and warehouse role", true, "LOGISTIC_ROLE", null, null },
                    { "RLM005", new DateTime(2026, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "MIS and reporting user", true, "MIS_ROLE", null, null }
                });

            migrationBuilder.InsertData(
                table: "PartChangeMasters",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsActive", "PartId", "PartName", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(1935), "URR002", true, "PCM001", "Anderson Bracket", null, null },
                    { 2, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(1940), "URR002", true, "PCM002", "Anderson connector Screw", null, null },
                    { 3, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(1944), "URR002", true, "PCM003", "Anderson SB 75", null, null },
                    { 4, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(1947), "URR002", true, "PCM004", "Handle + Handle Screw", null, null },
                    { 5, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(1950), "URR002", true, "PCM005", "Fuse", null, null },
                    { 6, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(1953), "URR002", true, "PCM006", "Top Cover Screw - M3 x 16", null, null },
                    { 7, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(1956), "URR002", true, "PCM007", "IOT Glass-Black", null, null },
                    { 8, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(1959), "URR002", true, "PCM008", "IOT Glass-White", null, null },
                    { 9, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(1962), "URR002", true, "PCM009", "NTC", null, null },
                    { 10, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(1965), "URR002", true, "PCM010", "IOT Screw - M3 x 12", null, null },
                    { 11, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(1967), "URR002", true, "PCM011", "Handle Screw", null, null }
                });

            migrationBuilder.InsertData(
                table: "UserTypes",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Description", "IsActive", "TypeId", "TypeName", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "System administrator user", true, "UST001", "ADMIN_USER", null, null },
                    { 2, new DateTime(2026, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Service / FSE engineer user", true, "UST002", "SERVICE_USER", null, null },
                    { 3, new DateTime(2026, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Warehouse operation user", true, "UST003", "WAREHOUSE_USER", null, null },
                    { 4, new DateTime(2026, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Plant operation user", true, "UST004", "PLANT_USER", null, null },
                    { 5, new DateTime(2026, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "MIS and reporting user", true, "UST005", "MIS_USER", null, null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Email", "FullName", "IsActive", "PasswordHash", "Phone", "UpdatedAt", "UpdatedBy", "UserId", "UserName", "UserTypeId" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 2, 17, 6, 42, 6, 547, DateTimeKind.Utc).AddTicks(9557), "System", "superuser@system.com", "Ids User", true, new byte[] { 64, 134, 140, 119, 239, 133, 134, 172, 29, 254, 208, 51, 69, 93, 70, 240, 166, 5, 234, 189, 248, 101, 255, 181, 33, 60, 150, 251, 27, 102, 187, 135 }, "9999999999", null, null, "URR001", "IdsUser", null },
                    { 2, new DateTime(2026, 2, 17, 6, 42, 6, 547, DateTimeKind.Utc).AddTicks(9580), "System", "admin@Lib.com", "Lib Admin User", true, new byte[] { 165, 190, 135, 161, 62, 60, 231, 123, 67, 242, 42, 240, 30, 163, 16, 142, 138, 94, 110, 84, 99, 196, 240, 192, 23, 61, 88, 191, 16, 101, 27, 36 }, "8888888888", null, null, "URR002", "LibAdminUser", null },
                    { 3, new DateTime(2026, 2, 17, 6, 42, 6, 547, DateTimeKind.Utc).AddTicks(9596), "System", "akashprajapati@system.com", "Mr. Akash Prajapati", true, new byte[] { 165, 190, 135, 161, 62, 60, 231, 123, 67, 242, 42, 240, 30, 163, 16, 142, 138, 94, 110, 84, 99, 196, 240, 192, 23, 61, 88, 191, 16, 101, 27, 36 }, "9000000003", null, null, "URR003", "AkashPrajapati", null },
                    { 4, new DateTime(2026, 2, 17, 6, 42, 6, 547, DateTimeKind.Utc).AddTicks(9616), "System", "akashyadav@system.com", "Mr. Akash Yadav", true, new byte[] { 165, 190, 135, 161, 62, 60, 231, 123, 67, 242, 42, 240, 30, 163, 16, 142, 138, 94, 110, 84, 99, 196, 240, 192, 23, 61, 88, 191, 16, 101, 27, 36 }, "9000000004", null, null, "URR004", "AkashYadav", null },
                    { 5, new DateTime(2026, 2, 17, 6, 42, 6, 547, DateTimeKind.Utc).AddTicks(9625), "System", "akhilshukla@system.com", "Mr. Akhil Shukla", true, new byte[] { 165, 190, 135, 161, 62, 60, 231, 123, 67, 242, 42, 240, 30, 163, 16, 142, 138, 94, 110, 84, 99, 196, 240, 192, 23, 61, 88, 191, 16, 101, 27, 36 }, "9000000005", null, null, "URR005", "AkhilShukla", null },
                    { 6, new DateTime(2026, 2, 17, 6, 42, 6, 547, DateTimeKind.Utc).AddTicks(9640), "System", "arifiqwal@system.com", "Mr. Arif Iqwal", true, new byte[] { 165, 190, 135, 161, 62, 60, 231, 123, 67, 242, 42, 240, 30, 163, 16, 142, 138, 94, 110, 84, 99, 196, 240, 192, 23, 61, 88, 191, 16, 101, 27, 36 }, "9000000006", null, null, "URR006", "ArifIqwal", null },
                    { 7, new DateTime(2026, 2, 17, 6, 42, 6, 547, DateTimeKind.Utc).AddTicks(9656), "System", "babu@system.com", "Mr. Babu", true, new byte[] { 165, 190, 135, 161, 62, 60, 231, 123, 67, 242, 42, 240, 30, 163, 16, 142, 138, 94, 110, 84, 99, 196, 240, 192, 23, 61, 88, 191, 16, 101, 27, 36 }, "9000000007", null, null, "URR007", "Babu", null },
                    { 8, new DateTime(2026, 2, 17, 6, 42, 6, 547, DateTimeKind.Utc).AddTicks(9665), "System", "deepakrajput@system.com", "Mr. Deepak Rajput", true, new byte[] { 165, 190, 135, 161, 62, 60, 231, 123, 67, 242, 42, 240, 30, 163, 16, 142, 138, 94, 110, 84, 99, 196, 240, 192, 23, 61, 88, 191, 16, 101, 27, 36 }, "9000000008", null, null, "URR008", "DeepakRajput", null },
                    { 9, new DateTime(2026, 2, 17, 6, 42, 6, 547, DateTimeKind.Utc).AddTicks(9674), "System", "devendrakumar1@system.com", "Mr. Devendra Kumar", true, new byte[] { 165, 190, 135, 161, 62, 60, 231, 123, 67, 242, 42, 240, 30, 163, 16, 142, 138, 94, 110, 84, 99, 196, 240, 192, 23, 61, 88, 191, 16, 101, 27, 36 }, "9000000009", null, null, "URR009", "DevendraKumar1", null },
                    { 10, new DateTime(2026, 2, 17, 6, 42, 6, 547, DateTimeKind.Utc).AddTicks(9684), "System", "gauravkumar@system.com", "Mr. Gaurav Kumar", true, new byte[] { 165, 190, 135, 161, 62, 60, 231, 123, 67, 242, 42, 240, 30, 163, 16, 142, 138, 94, 110, 84, 99, 196, 240, 192, 23, 61, 88, 191, 16, 101, 27, 36 }, "9000000010", null, null, "URR010", "GauravKumar", null },
                    { 11, new DateTime(2026, 2, 17, 6, 42, 6, 547, DateTimeKind.Utc).AddTicks(9692), "System", "ketangupta@system.com", "Mr. Ketan Gupta", true, new byte[] { 165, 190, 135, 161, 62, 60, 231, 123, 67, 242, 42, 240, 30, 163, 16, 142, 138, 94, 110, 84, 99, 196, 240, 192, 23, 61, 88, 191, 16, 101, 27, 36 }, "9000000011", null, null, "URR011", "KetanGupta", null },
                    { 12, new DateTime(2026, 2, 17, 6, 42, 6, 547, DateTimeKind.Utc).AddTicks(9701), "System", "manishkumar@system.com", "Mr. Manish Kumar", true, new byte[] { 165, 190, 135, 161, 62, 60, 231, 123, 67, 242, 42, 240, 30, 163, 16, 142, 138, 94, 110, 84, 99, 196, 240, 192, 23, 61, 88, 191, 16, 101, 27, 36 }, "9000000012", null, null, "URR012", "ManishKumar", null },
                    { 13, new DateTime(2026, 2, 17, 6, 42, 6, 547, DateTimeKind.Utc).AddTicks(9710), "System", "pradeepkumar@system.com", "Mr. Pradeep Kumar", true, new byte[] { 165, 190, 135, 161, 62, 60, 231, 123, 67, 242, 42, 240, 30, 163, 16, 142, 138, 94, 110, 84, 99, 196, 240, 192, 23, 61, 88, 191, 16, 101, 27, 36 }, "9000000013", null, null, "URR013", "PradeepKumar", null },
                    { 14, new DateTime(2026, 2, 17, 6, 42, 6, 547, DateTimeKind.Utc).AddTicks(9719), "System", "rajnimore@system.com", "Mr. Raj Nimore", true, new byte[] { 165, 190, 135, 161, 62, 60, 231, 123, 67, 242, 42, 240, 30, 163, 16, 142, 138, 94, 110, 84, 99, 196, 240, 192, 23, 61, 88, 191, 16, 101, 27, 36 }, "9000000014", null, null, "URR014", "RajNimore", null },
                    { 15, new DateTime(2026, 2, 17, 6, 42, 6, 547, DateTimeKind.Utc).AddTicks(9729), "System", "rampandey@system.com", "Mr. Ram Pandey", true, new byte[] { 165, 190, 135, 161, 62, 60, 231, 123, 67, 242, 42, 240, 30, 163, 16, 142, 138, 94, 110, 84, 99, 196, 240, 192, 23, 61, 88, 191, 16, 101, 27, 36 }, "9000000015", null, null, "URR015", "RamPandey", null },
                    { 16, new DateTime(2026, 2, 17, 6, 42, 6, 547, DateTimeKind.Utc).AddTicks(9738), "System", "salaudinansari@system.com", "Mr. Salaudin Ansari", true, new byte[] { 165, 190, 135, 161, 62, 60, 231, 123, 67, 242, 42, 240, 30, 163, 16, 142, 138, 94, 110, 84, 99, 196, 240, 192, 23, 61, 88, 191, 16, 101, 27, 36 }, "9000000016", null, null, "URR016", "SalaudinAnsari", null },
                    { 17, new DateTime(2026, 2, 17, 6, 42, 6, 547, DateTimeKind.Utc).AddTicks(9878), "System", "sameersabane@system.com", "Mr. Sameer Sabane", true, new byte[] { 165, 190, 135, 161, 62, 60, 231, 123, 67, 242, 42, 240, 30, 163, 16, 142, 138, 94, 110, 84, 99, 196, 240, 192, 23, 61, 88, 191, 16, 101, 27, 36 }, "9000000017", null, null, "URR017", "SameerSabane", null },
                    { 18, new DateTime(2026, 2, 17, 6, 42, 6, 547, DateTimeKind.Utc).AddTicks(9887), "System", "shivendrasingh@system.com", "Mr. Shivendra Singh", true, new byte[] { 165, 190, 135, 161, 62, 60, 231, 123, 67, 242, 42, 240, 30, 163, 16, 142, 138, 94, 110, 84, 99, 196, 240, 192, 23, 61, 88, 191, 16, 101, 27, 36 }, "9000000018", null, null, "URR018", "ShivendraSingh", null },
                    { 19, new DateTime(2026, 2, 17, 6, 42, 6, 547, DateTimeKind.Utc).AddTicks(9896), "System", "vivekshukla@system.com", "Mr. Vivek Shukla", true, new byte[] { 165, 190, 135, 161, 62, 60, 231, 123, 67, 242, 42, 240, 30, 163, 16, 142, 138, 94, 110, 84, 99, 196, 240, 192, 23, 61, 88, 191, 16, 101, 27, 36 }, "9000000019", null, null, "URR019", "VivekShukla", null },
                    { 20, new DateTime(2026, 2, 17, 6, 42, 6, 547, DateTimeKind.Utc).AddTicks(9905), "System", "abhishekkumar@system.com", "Mr. Abhishek Kumar", true, new byte[] { 165, 190, 135, 161, 62, 60, 231, 123, 67, 242, 42, 240, 30, 163, 16, 142, 138, 94, 110, 84, 99, 196, 240, 192, 23, 61, 88, 191, 16, 101, 27, 36 }, "9000000020", null, null, "URR020", "AbhishekKumar", null },
                    { 21, new DateTime(2026, 2, 17, 6, 42, 6, 547, DateTimeKind.Utc).AddTicks(9915), "System", "devendrakumar2@system.com", "Mr. Devendra Kumar", true, new byte[] { 165, 190, 135, 161, 62, 60, 231, 123, 67, 242, 42, 240, 30, 163, 16, 142, 138, 94, 110, 84, 99, 196, 240, 192, 23, 61, 88, 191, 16, 101, 27, 36 }, "9000000021", null, null, "URR021", "DevendraKumar2", null },
                    { 22, new DateTime(2026, 2, 17, 6, 42, 6, 547, DateTimeKind.Utc).AddTicks(9925), "System", "nikhishukla@system.com", "Mr. Nikhil Shukla", true, new byte[] { 165, 190, 135, 161, 62, 60, 231, 123, 67, 242, 42, 240, 30, 163, 16, 142, 138, 94, 110, 84, 99, 196, 240, 192, 23, 61, 88, 191, 16, 101, 27, 36 }, "9000000022", null, null, "URR022", "NikhilShukla", null },
                    { 23, new DateTime(2026, 2, 17, 6, 42, 6, 547, DateTimeKind.Utc).AddTicks(9934), "System", "rajthakur@system.com", "Mr. Raj Thakur", true, new byte[] { 165, 190, 135, 161, 62, 60, 231, 123, 67, 242, 42, 240, 30, 163, 16, 142, 138, 94, 110, 84, 99, 196, 240, 192, 23, 61, 88, 191, 16, 101, 27, 36 }, "9000000023", null, null, "URR023", "RajThakur", null },
                    { 24, new DateTime(2026, 2, 17, 6, 42, 6, 547, DateTimeKind.Utc).AddTicks(9944), "System", "shivendrasingh2@system.com", "Mr. Shivendra Singh", true, new byte[] { 165, 190, 135, 161, 62, 60, 231, 123, 67, 242, 42, 240, 30, 163, 16, 142, 138, 94, 110, 84, 99, 196, 240, 192, 23, 61, 88, 191, 16, 101, 27, 36 }, "9000000024", null, null, "URR024", "ShivendraSingh2", null },
                    { 25, new DateTime(2026, 2, 17, 6, 42, 6, 547, DateTimeKind.Utc).AddTicks(9953), "System", "shobhitsingh@system.com", "Mr. Shobhit Singh", true, new byte[] { 165, 190, 135, 161, 62, 60, 231, 123, 67, 242, 42, 240, 30, 163, 16, 142, 138, 94, 110, 84, 99, 196, 240, 192, 23, 61, 88, 191, 16, 101, 27, 36 }, "9000000025", null, null, "URR025", "ShobhitSingh", null },
                    { 26, new DateTime(2026, 2, 17, 6, 42, 6, 547, DateTimeKind.Utc).AddTicks(9962), "System", "tausifansari@system.com", "Mr. Tausif Ansari", true, new byte[] { 165, 190, 135, 161, 62, 60, 231, 123, 67, 242, 42, 240, 30, 163, 16, 142, 138, 94, 110, 84, 99, 196, 240, 192, 23, 61, 88, 191, 16, 101, 27, 36 }, "9000000026", null, null, "URR026", "TausifAnsari", null },
                    { 27, new DateTime(2026, 2, 17, 6, 42, 6, 547, DateTimeKind.Utc).AddTicks(9971), "System", "mukulyadav@system.com", "Mr. Mukul Yadav", true, new byte[] { 165, 190, 135, 161, 62, 60, 231, 123, 67, 242, 42, 240, 30, 163, 16, 142, 138, 94, 110, 84, 99, 196, 240, 192, 23, 61, 88, 191, 16, 101, 27, 36 }, "9000000027", null, null, "URR027", "MukulYadav", null },
                    { 28, new DateTime(2026, 2, 17, 6, 42, 6, 547, DateTimeKind.Utc).AddTicks(9980), "System", "vicky@system.com", "Mr. Vicky", true, new byte[] { 165, 190, 135, 161, 62, 60, 231, 123, 67, 242, 42, 240, 30, 163, 16, 142, 138, 94, 110, 84, 99, 196, 240, 192, 23, 61, 88, 191, 16, 101, 27, 36 }, "9000000028", null, null, "URR028", "Vicky", null },
                    { 29, new DateTime(2026, 2, 17, 6, 42, 6, 547, DateTimeKind.Utc).AddTicks(9989), "System", "thakursahab@system.com", "Mr. Thakur Sahab", true, new byte[] { 165, 190, 135, 161, 62, 60, 231, 123, 67, 242, 42, 240, 30, 163, 16, 142, 138, 94, 110, 84, 99, 196, 240, 192, 23, 61, 88, 191, 16, 101, 27, 36 }, "9000000029", null, null, "URR029", "ThakurSahab", null },
                    { 30, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(15), "System", "yatishraj@system.com", "Mr. Yatish Raj", true, new byte[] { 165, 190, 135, 161, 62, 60, 231, 123, 67, 242, 42, 240, 30, 163, 16, 142, 138, 94, 110, 84, 99, 196, 240, 192, 23, 61, 88, 191, 16, 101, 27, 36 }, "9000000030", null, null, "URR030", "YatishRaj", null },
                    { 31, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(26), "System", "shravankumarprajapati@system.com", "Mr. Shravan Kumar Prajapati", true, new byte[] { 165, 190, 135, 161, 62, 60, 231, 123, 67, 242, 42, 240, 30, 163, 16, 142, 138, 94, 110, 84, 99, 196, 240, 192, 23, 61, 88, 191, 16, 101, 27, 36 }, "9000000031", null, null, "URR031", "ShravanKumarPrajapati", null },
                    { 32, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(36), "System", "amanjaiswal@system.com", "Mr. Aman Jaiswal", true, new byte[] { 165, 190, 135, 161, 62, 60, 231, 123, 67, 242, 42, 240, 30, 163, 16, 142, 138, 94, 110, 84, 99, 196, 240, 192, 23, 61, 88, 191, 16, 101, 27, 36 }, "9000000032", null, null, "URR032", "AmanJaiswal", null },
                    { 33, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(46), "System", "shailendra@system.com", "Mr. Shailendra", true, new byte[] { 165, 190, 135, 161, 62, 60, 231, 123, 67, 242, 42, 240, 30, 163, 16, 142, 138, 94, 110, 84, 99, 196, 240, 192, 23, 61, 88, 191, 16, 101, 27, 36 }, "9000000033", null, null, "URR033", "Shailendra", null }
                });

            migrationBuilder.InsertData(
                table: "EntityMasters",
                columns: new[] { "Id", "ContactPerson", "CreatedAt", "CreatedBy", "Email", "EntityId", "EntityName", "EntityTypeId", "IsActive", "Mobile", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "URR002", null, "ETM001", "Battery Smart Pvt Ltd", "ETY001", true, null, null, null },
                    { 2, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "URR002", null, "ETM002", "Upgrid Solutions Pvt Ltd", "ETY002", true, null, null, null },
                    { 3, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "URR002", null, "ETM003", "Mumbai Batteries Pvt Ltd", "ETY003", true, null, null, null },
                    { 4, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "URR002", null, "ETM004", "Global Eneergy Service Pvt Ltd", "ETY004", true, null, null, null },
                    { 5, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "URR002", null, "ETM005", "Bangalore Eneergy Solutions Pvt Ltd", "ETY003", true, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "GSTMasters",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "EffectiveFrom", "EffectiveTo", "GSTId", "GSTPercentage", "GSTSlabId", "GSTTypeId", "IsActive", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "TAX001", 0m, "TXS001", "TXT001", true, null, null },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "TAX002", 0m, "TXS001", "TXT002", true, null, null },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "TAX003", 0m, "TXS001", "TXT003", true, null, null },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "TAX004", 2.5m, "TXS002", "TXT001", true, null, null },
                    { 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "TAX005", 2.5m, "TXS002", "TXT002", true, null, null },
                    { 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "TAX006", 5m, "TXS002", "TXT003", true, null, null },
                    { 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "TAX007", 6m, "TXS003", "TXT001", true, null, null },
                    { 8, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "TAX008", 6m, "TXS003", "TXT002", true, null, null },
                    { 9, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "TAX009", 12m, "TXS003", "TXT003", true, null, null },
                    { 10, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "TAX010", 9m, "TXS004", "TXT001", true, null, null },
                    { 11, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "TAX011", 9m, "TXS004", "TXT002", true, null, null },
                    { 12, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "TAX012", 18m, "TXS004", "TXT003", true, null, null },
                    { 13, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "TAX013", 14m, "TXS005", "TXT001", true, null, null },
                    { 14, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "TAX014", 14m, "TXS005", "TXT002", true, null, null },
                    { 15, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "TAX015", 28m, "TXS005", "TXT003", true, null, null }
                });

            migrationBuilder.InsertData(
                table: "MasterUserRoles",
                columns: new[] { "RoleId", "UserRefId", "CreatedAt", "CreatedBy", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { "RLM001", 1, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(270), "URR002", null, null },
                    { "RLM002", 2, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(274), "URR002", null, null },
                    { "RLM003", 3, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(277), "URR002", null, null },
                    { "RLM003", 4, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(280), "URR002", null, null },
                    { "RLM003", 5, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(283), "URR002", null, null },
                    { "RLM003", 6, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(285), "URR002", null, null },
                    { "RLM003", 7, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(288), "URR002", null, null },
                    { "RLM003", 8, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(290), "URR002", null, null },
                    { "RLM003", 9, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(293), "URR002", null, null },
                    { "RLM003", 10, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(295), "URR002", null, null },
                    { "RLM003", 11, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(298), "URR002", null, null },
                    { "RLM003", 12, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(300), "URR002", null, null },
                    { "RLM003", 13, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(303), "URR002", null, null },
                    { "RLM003", 14, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(306), "URR002", null, null },
                    { "RLM003", 15, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(308), "URR002", null, null },
                    { "RLM003", 16, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(310), "URR002", null, null },
                    { "RLM003", 17, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(313), "URR002", null, null },
                    { "RLM003", 18, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(315), "URR002", null, null },
                    { "RLM003", 19, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(318), "URR002", null, null },
                    { "RLM003", 20, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(483), "URR002", null, null },
                    { "RLM003", 21, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(487), "URR002", null, null },
                    { "RLM003", 22, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(490), "URR002", null, null },
                    { "RLM003", 23, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(492), "URR002", null, null },
                    { "RLM003", 24, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(494), "URR002", null, null },
                    { "RLM003", 25, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(497), "URR002", null, null },
                    { "RLM003", 26, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(500), "URR002", null, null },
                    { "RLM003", 27, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(502), "URR002", null, null },
                    { "RLM003", 28, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(505), "URR002", null, null },
                    { "RLM003", 29, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(507), "URR002", null, null },
                    { "RLM003", 30, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(510), "URR002", null, null },
                    { "RLM003", 31, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(512), "URR002", null, null },
                    { "RLM003", 32, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(514), "URR002", null, null },
                    { "RLM003", 33, new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(516), "URR002", null, null }
                });

            migrationBuilder.InsertData(
                table: "StateMasters",
                columns: new[] { "Id", "CountryId", "CreatedAt", "CreatedBy", "GstCode", "IsActive", "Region", "StateId", "StateName", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, "CNM001", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "URR002", "37", true, "South India", "STM001", "Andhra Pradesh", null, null },
                    { 2, "CNM001", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "URR002", "12", true, "North East India", "STM002", "Arunachal Pradesh", null, null },
                    { 3, "CNM001", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "URR002", "18", true, "North East India", "STM003", "Assam", null, null },
                    { 4, "CNM001", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "URR002", "10", true, "East India", "STM004", "Bihar", null, null },
                    { 5, "CNM001", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "URR002", "22", true, "Central India", "STM005", "Chhattisgarh", null, null },
                    { 6, "CNM001", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "URR002", "30", true, "West India", "STM006", "Goa", null, null },
                    { 7, "CNM001", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "URR002", "24", true, "West India", "STM007", "Gujarat", null, null },
                    { 8, "CNM001", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "URR002", "06", true, "North India", "STM008", "Haryana", null, null },
                    { 9, "CNM001", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "URR002", "02", true, "North India", "STM009", "Himachal Pradesh", null, null },
                    { 10, "CNM001", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "URR002", "20", true, "East India", "STM010", "Jharkhand", null, null },
                    { 11, "CNM001", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "URR002", "29", true, "South India", "STM011", "Karnataka", null, null },
                    { 12, "CNM001", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "URR002", "32", true, "South India", "STM012", "Kerala", null, null },
                    { 13, "CNM001", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "URR002", "23", true, "Central India", "STM013", "Madhya Pradesh", null, null },
                    { 14, "CNM001", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "URR002", "27", true, "West India", "STM014", "Maharashtra", null, null },
                    { 15, "CNM001", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "URR002", "14", true, "North East India", "STM015", "Manipur", null, null },
                    { 16, "CNM001", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "URR002", "17", true, "North East India", "STM016", "Meghalaya", null, null },
                    { 17, "CNM001", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "URR002", "15", true, "North East India", "STM017", "Mizoram", null, null },
                    { 18, "CNM001", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "URR002", "13", true, "North East India", "STM018", "Nagaland", null, null },
                    { 19, "CNM001", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "URR002", "21", true, "East India", "STM019", "Odisha", null, null },
                    { 20, "CNM001", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "URR002", "03", true, "North India", "STM020", "Punjab", null, null },
                    { 21, "CNM001", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "URR002", "08", true, "North India", "STM021", "Rajasthan", null, null },
                    { 22, "CNM001", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "URR002", "11", true, "North East India", "STM022", "Sikkim", null, null },
                    { 23, "CNM001", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "URR002", "33", true, "South India", "STM023", "Tamil Nadu", null, null },
                    { 24, "CNM001", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "URR002", "36", true, "South India", "STM024", "Telangana", null, null },
                    { 25, "CNM001", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "URR002", "16", true, "North East India", "STM025", "Tripura", null, null },
                    { 26, "CNM001", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "URR002", "09", true, "North India", "STM026", "Uttar Pradesh", null, null },
                    { 27, "CNM001", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "URR002", "05", true, "North India", "STM027", "Uttarakhand", null, null },
                    { 28, "CNM001", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "URR002", "19", true, "East India", "STM028", "West Bengal", null, null },
                    { 29, "CNM001", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "URR002", "07", true, "North India", "STM029", "Delhi", null, null }
                });

            migrationBuilder.InsertData(
                table: "CityMasters",
                columns: new[] { "Id", "AreaCode", "CityId", "CityName", "CreatedAt", "CreatedBy", "IsActive", "PostalCode", "StateId", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, "011", "CTM001", "New Delhi", new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(1356), "URR002", true, "110001", "STM029", null, null },
                    { 2, "011", "CTM002", "North Delhi", new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(1367), "URR002", true, "110007", "STM029", null, null },
                    { 3, "011", "CTM003", "South Delhi", new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(1372), "URR002", true, "110016", "STM029", null, null },
                    { 4, "022", "CTM004", "Mumbai", new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(1376), "URR002", true, "400001", "STM014", null, null },
                    { 5, "020", "CTM005", "Pune", new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(1380), "URR002", true, "411001", "STM014", null, null },
                    { 6, "0712", "CTM006", "Nagpur", new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(1387), "URR002", true, "440001", "STM014", null, null },
                    { 7, "080", "CTM007", "Bengaluru", new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(1391), "URR002", true, "560001", "STM011", null, null },
                    { 8, "0821", "CTM008", "Mysuru", new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(1394), "URR002", true, "570001", "STM011", null, null },
                    { 9, "044", "CTM009", "Chennai", new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(1398), "URR002", true, "600001", "STM023", null, null },
                    { 10, "0422", "CTM010", "Coimbatore", new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(1406), "URR002", true, "641001", "STM023", null, null },
                    { 11, "079", "CTM011", "Ahmedabad", new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(1410), "URR002", true, "380001", "STM007", null, null },
                    { 12, "0261", "CTM012", "Surat", new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(1414), "URR002", true, "395003", "STM007", null, null },
                    { 13, "033", "CTM013", "Kolkata", new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(1418), "URR002", true, "700001", "STM028", null, null },
                    { 14, "0522", "CTM014", "Lucknow", new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(1422), "URR002", true, "226001", "STM026", null, null },
                    { 15, "0512", "CTM015", "Kanpur", new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(1426), "URR002", true, "208001", "STM026", null, null },
                    { 16, "040", "CTM016", "Hyderabad", new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(1430), "URR002", true, "500001", "STM024", null, null },
                    { 17, "0141", "CTM017", "Jaipur", new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(1434), "URR002", true, "302001", "STM021", null, null },
                    { 18, "0755", "CTM018", "Bhopal", new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(1441), "URR002", true, "462001", "STM013", null, null },
                    { 19, "0731", "CTM019", "Indore", new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(1445), "URR002", true, "452001", "STM013", null, null }
                });

            migrationBuilder.InsertData(
                table: "AddressMasters",
                columns: new[] { "Id", "AddressId", "AddressLine1", "AddressLine2", "CityId", "CreatedAt", "CreatedBy", "EntityId", "EntityTypeId", "IsActive", "PostalCode", "UpdatedAt", "UpdatedBy", "WarehouseId" },
                values: new object[] { 5, "ADR005", "22, MG Road", null, "CTM007", new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(3144), "URR002", "ETM005", "ETY001", true, "560001", null, null, null });

            migrationBuilder.InsertData(
                table: "OrgLegalDetails",
                columns: new[] { "Id", "CINNumber", "CityId", "CreatedAt", "CreatedBy", "EntityId", "GSTINNumber", "IsActive", "LegalId", "PANNumber", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, "L12345MH2020PTC123456", "CTM004", new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(3229), "URR002", "ETM001", "27ABCDE1234F1Z5", true, "OLD001", "ABCDE1234F", null, null },
                    { 2, "L23456DL2021PTC654321", "CTM001", new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(3236), "URR002", "ETM002", "29ABCDE5678G1Z6", true, "OLD002", "ABCDE5678G", null, null },
                    { 5, "L56789TS2024PTC555666", "CTM016", new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(3240), "URR002", "ETM002", "36ABCDE7777K1Z9", true, "OLD003", "ABCDE7777K", null, null }
                });

            migrationBuilder.InsertData(
                table: "Warehouses",
                columns: new[] { "Id", "CityId", "CreatedAt", "CreatedBy", "EntityId", "IsActive", "UpdatedAt", "UpdatedBy", "WarehouseCode", "WarehouseId" },
                values: new object[,]
                {
                    { 1, "CTM001", new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(3040), "URR002", "ETM001", true, null, null, "NewDelhi-WH001", "WHS001" },
                    { 2, "CTM002", new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(3045), "URR002", "ETM002", true, null, null, "NorthDelhi-WH002", "WHS002" },
                    { 3, "CTM003", new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(3049), "URR002", "ETM003", false, null, null, "Mumbai-WH003", "WHS003" }
                });

            migrationBuilder.InsertData(
                table: "AddressMasters",
                columns: new[] { "Id", "AddressId", "AddressLine1", "AddressLine2", "CityId", "CreatedAt", "CreatedBy", "EntityId", "EntityTypeId", "IsActive", "PostalCode", "UpdatedAt", "UpdatedBy", "WarehouseId" },
                values: new object[,]
                {
                    { 1, "ADR001", "123, Connaught Place", "Near India Gate", "CTM001", new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(3123), "URR002", "ETM001", "ETY002", true, "110001", null, null, "WHS001" },
                    { 2, "ADR002", "45, Rohini Sector 12", null, "CTM002", new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(3129), "URR002", "ETM002", "ETY002", true, "110007", null, null, "WHS002" },
                    { 3, "ADR003", "789, Andheri East", "Near Chhatrapati Complex", "CTM004", new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(3135), "URR002", "ETM003", "ETY003", true, "400001", null, null, "WHS003" },
                    { 4, "ADR004", "56, Laxmi Nagar", null, "CTM003", new DateTime(2026, 2, 17, 6, 42, 6, 548, DateTimeKind.Utc).AddTicks(3139), "URR002", "ETM004", "ETY002", true, "110016", null, null, "WHS001" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityStatus_StatusId",
                table: "ActivityStatus",
                column: "StatusId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AddressMasters_CityId",
                table: "AddressMasters",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_AddressMasters_EntityId",
                table: "AddressMasters",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_AddressMasters_EntityTypeId",
                table: "AddressMasters",
                column: "EntityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AddressMasters_WarehouseId",
                table: "AddressMasters",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_BatteryTrans_ActivityId",
                table: "BatteryTrans",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_BatteryTrans_CurrentStatusId",
                table: "BatteryTrans",
                column: "CurrentStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_BatteryTrans_CustomerId",
                table: "BatteryTrans",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_BatteryTrans_WarehouseId",
                table: "BatteryTrans",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_CityMasters_CityId",
                table: "CityMasters",
                column: "CityId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CityMasters_StateId",
                table: "CityMasters",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityMasters_EntityId",
                table: "EntityMasters",
                column: "EntityId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EntityMasters_EntityTypeId",
                table: "EntityMasters",
                column: "EntityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityTypes_EntityTypeId",
                table: "EntityTypes",
                column: "EntityTypeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GSTMasters_GSTSlabId",
                table: "GSTMasters",
                column: "GSTSlabId");

            migrationBuilder.CreateIndex(
                name: "IX_GSTMasters_GSTTypeId",
                table: "GSTMasters",
                column: "GSTTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_GSTTypeMasters_GSTTypeCode",
                table: "GSTTypeMasters",
                column: "GSTTypeCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MasterCountries_CountryName",
                table: "MasterCountries",
                column: "CountryName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MasterUserRoles_RoleId",
                table: "MasterUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_OrgLegalDetails_CityId",
                table: "OrgLegalDetails",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_OrgLegalDetails_EntityId",
                table: "OrgLegalDetails",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceActivities_ActivityEngineerId",
                table: "ServiceActivities",
                column: "ActivityEngineerId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceActivities_ActivityId",
                table: "ServiceActivities",
                column: "ActivityId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceActivities_EntityId",
                table: "ServiceActivities",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceActivities_StatusId",
                table: "ServiceActivities",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceActivities_WarehouseId",
                table: "ServiceActivities",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_StateMasters_CountryId",
                table: "StateMasters",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_StateMasters_StateName",
                table: "StateMasters",
                column: "StateName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserId",
                table: "Users",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserTypeId",
                table: "Users",
                column: "UserTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_CityId",
                table: "Warehouses",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_EntityId",
                table: "Warehouses",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_WarehouseId",
                table: "Warehouses",
                column: "WarehouseId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddressMasters");

            migrationBuilder.DropTable(
                name: "BatteryTrans");

            migrationBuilder.DropTable(
                name: "CorrectiveActions");

            migrationBuilder.DropTable(
                name: "DefectDetails");

            migrationBuilder.DropTable(
                name: "FirmwareStatuses");

            migrationBuilder.DropTable(
                name: "GSTMasters");

            migrationBuilder.DropTable(
                name: "MasterUserRoles");

            migrationBuilder.DropTable(
                name: "OrgLegalDetails");

            migrationBuilder.DropTable(
                name: "PartChangeMasters");

            migrationBuilder.DropTable(
                name: "BatteryStatuses");

            migrationBuilder.DropTable(
                name: "ServiceActivities");

            migrationBuilder.DropTable(
                name: "GSTSlabMasters");

            migrationBuilder.DropTable(
                name: "GSTTypeMasters");

            migrationBuilder.DropTable(
                name: "MasterRoles");

            migrationBuilder.DropTable(
                name: "ActivityStatus");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Warehouses");

            migrationBuilder.DropTable(
                name: "UserTypes");

            migrationBuilder.DropTable(
                name: "CityMasters");

            migrationBuilder.DropTable(
                name: "EntityMasters");

            migrationBuilder.DropTable(
                name: "StateMasters");

            migrationBuilder.DropTable(
                name: "EntityTypes");

            migrationBuilder.DropTable(
                name: "MasterCountries");
        }
    }
}
