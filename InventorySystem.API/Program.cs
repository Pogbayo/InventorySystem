using InventorySystem.Application.Interfaces.IRepositories;
using InventorySystem.Application.Interfaces.IServices;
using InventorySystem.Application.Services;
using InventorySystem.Domain.Entities;
using InventorySystem.Infrastructure.Jwt_generator;
using InventorySystem.Infrastructure.Persistence;
using InventorySystem.Infrastructure.Respositories;
using InventorySystem.Infrastructure.SeedData;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using InventorySystem.Application.Mapper;
using AutoMapper;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using Microsoft.Extensions.Options;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] { }
        }
    });
});


builder.Services.AddControllers();
//builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

//DbContext
builder.Services.AddDbContext<InventorySystemDb>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHttpContextAccessor();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();


builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddHttpContextAccessor();

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//   .AddJwtBearer(options =>
//   {
//       options.Events = new JwtBearerEvents
//       {
//           OnTokenValidated = context =>
//           {
//               Console.WriteLine("Token validated successfully");
//               foreach (var claim in context.Principal!.Claims)
//               {
//                   Console.WriteLine($"Claim: {claim.Type} - {claim.Value}");
//               }
//               return Task.CompletedTask;
//           },
//           OnAuthenticationFailed = context =>
//           {
//               Console.WriteLine("Token validation failed");
//               Console.WriteLine(context.Exception.Message);
//               return Task.CompletedTask;
//           },
//           OnForbidden = context =>
//           {
//               Console.WriteLine("Forbidden: Access denied due to insufficient permissions");
//               return Task.CompletedTask;
//           }
//       };
//       options.TokenValidationParameters = new TokenValidationParameters
//       {
//           ValidateIssuerSigningKey = true,
//           // Automatically grab the JwtSettings and apply it to the validation parameters
//           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]!)),
//           ValidateIssuer = true,
//           ValidateAudience = true,
//           RequireExpirationTime = true,
//           ValidateLifetime = true,
//           RoleClaimType = ClaimTypes.Role,
//           NameClaimType = ClaimTypes.NameIdentifier,
//           ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
//           ValidAudience = builder.Configuration["JwtSettings:Audience"],
//           ClockSkew = TimeSpan.Zero,
//       };
//       options.Events = new JwtBearerEvents
//       {
//           OnTokenValidated = context =>
//           {
//               Console.WriteLine("Token validated successfully");
//               foreach (var claim in context.Principal!.Claims)
//               {
//                   Console.WriteLine($"Claim: {claim.Type} - {claim.Value}");
//               }
//               return Task.CompletedTask;
//           },
//           OnAuthenticationFailed = context =>
//           {
//               Console.WriteLine("Token validation failed");
//               Console.WriteLine(context.Exception.Message);
//               return Task.CompletedTask;
//           },
//           OnForbidden = context =>
//           {
//               Console.WriteLine("Forbidden: Access denied due to insufficient permissions");
//               return Task.CompletedTask;
//           }
//       };
//   });

//AuthHelper
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<UserService>();

//AuditLogHelper
builder.Services.AddScoped<IAuditLogRepository, AuditLogRepository>();
builder.Services.AddScoped<IAuditLogService, AuditLogService>();

builder.Services.AddScoped<ITokenService, TokenService>();

//var configuration = new MapperConfiguration(cfg =>
//{
//    cfg.AddProfile<MappingProfile>(); 
//});

//var mapper = configuration.CreateMapper();

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);


builder.Services.AddTransient<CurrentUserService>();


builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
builder.Services.AddScoped<IInventoryService, InventoryService>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<ISupplierService, SupplierService>();

builder.Services.AddScoped<IWarehouseRepository, WarehouseRepository>();
builder.Services.AddScoped<IWarehouseService, WarehouseService>();

builder.Services.AddScoped<IProductWarehouseRepository, ProductWarehouseRepository>();
builder.Services.AddScoped<IProductWarehouseService, ProductWarehouseService>();

builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
    .AddRoles<IdentityRole<Guid>>()
    .AddEntityFrameworkStores<InventorySystemDb>();
//.AddDefaultTokenProviders();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]!)),
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"]
    };
});


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<InventorySystemDb>();

    try
    {
        dbContext.Database.Migrate(); 
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Migration failed: {ex.Message}");
    }

    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
    await SeedData.SeedRolesAsync(roleManager);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


void LogUserClaims(IApplicationBuilder app)
{
    app.Use(async (context, next) =>
    {
        Console.WriteLine("HttpContext.User:");
        foreach (var claim in context.User.Claims)
        {
            Console.WriteLine($"Claim: {claim.Type} - {claim.Value}");
        }
        await next();
    });
}

// In the main pipeline

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();
LogUserClaims(app);

app.MapControllers();

app.Run();
