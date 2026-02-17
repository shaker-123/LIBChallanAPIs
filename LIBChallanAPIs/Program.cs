using LIBChallanAPIs.IRepositories;
using LIBChallanAPIs.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<ICountryMasterRepository, CountryMasterRepository>();
builder.Services.AddScoped<IStateMasterRepository, StateMasterRepository>();
builder.Services.AddScoped<ICityMasterRepository, CityMasterRepository>();
builder.Services.AddScoped<IGSTMasterRepository, GSTMasterRepository>();
builder.Services.AddScoped<IGSTTypeMasterRepository, GSTTypeMasterRepository>();
builder.Services.AddScoped<IDefectDetailsRepository, DefectDetailsRepository>();
builder.Services.AddScoped<IUserTypeRepository, UserTypeRepository>();
builder.Services.AddScoped<ICorrectiveActionRepository, CorrectiveActionRepository>();
builder.Services.AddScoped<IPartChangeMasterRepository, PartChangeMasterRepository>();
builder.Services.AddScoped<IBatteryStatusRepository, BatteryStatusRepository>();
builder.Services.AddScoped<IAddressTypeRepository, AddressTypeRepository>();
builder.Services.AddScoped<IEntityMasterRepository, EntityMasterRepository>();
builder.Services.AddScoped<IWarehouseRepository, WarehouseRepository>();
builder.Services.AddScoped<IAddressMasterRepository, AddressMasterRepository>();
builder.Services.AddScoped<IOrgLegalDetailsRepository, OrgLegalDetailsRepository>();
builder.Services.AddScoped<IServiceActivityRepository, ServiceActivityRepository>();
builder.Services.AddScoped<IFirmwareStatusRepository, FirmwareStatusRepository>();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],

        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddAuthorization();


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "LIBChallanAPIs",
        Version = "v1"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter JWT token like: Bearer {your token}"
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
            Array.Empty<string>()
        }
    });
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact", policy =>
    {
        policy
            .AllowAnyOrigin()    
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithExposedHeaders(
                "X-Total-Count",
                "X-File-Name"
            );
    });
});


var app = builder.Build();



app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "LIBChallanAPIs v1");
    c.RoutePrefix = "swagger";
});

app.UseHttpsRedirection();

app.UseCors("AllowReact"); 

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
