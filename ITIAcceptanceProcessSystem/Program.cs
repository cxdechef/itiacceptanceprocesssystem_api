using ITIAcceptanceProcessSystem.Data;
using ITIAcceptanceProcessSystem.Helpers;
using ITIAcceptanceProcessSystem.Models;
using ITIAcceptanceProcessSystem.Repositories;
using ITIAcceptanceProcessSystem.Services;
using ITIAcceptanceProcessSystem.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<JwtHelper>();

// Configure JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    // Accessing JWT settings directly from IConfiguration
    var jwtSettings = builder.Configuration.GetSection("Jwt");
    var key = jwtSettings["Key"];
    var issuer = jwtSettings["Issuer"];
    var audience = jwtSettings["Audience"];

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = issuer,
        ValidAudience = audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
    };
});

// Register Generic Repository (for all entities)
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Dependency Injection for Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<CandidateService>();
builder.Services.AddScoped<InstructorService>();
builder.Services.AddScoped<AdminService>();

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ITI Acceptance Process System", Version = "v1" });
});

var app = builder.Build();

// Seed data after the application is built
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        await SeedData.Initialize(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ITI Acceptance Process System v1"));
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
