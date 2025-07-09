using API.Middleware;
using Application.Common.Interfaces;
using Application.Features.Auth.Commands;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Infrastructure.Caching;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Swagger configuration with JWT
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
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

// Database
builder.Services.AddDbContext<FloraDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Cache Configuration
builder.Services.Configure<CacheConfiguration>(builder.Configuration.GetSection(CacheConfiguration.SectionName));

var cacheConfig = builder.Configuration.GetSection(CacheConfiguration.SectionName).Get<CacheConfiguration>() ?? new CacheConfiguration();

if (cacheConfig.EnableCaching)
{
    if (cacheConfig.Type == CacheType.Redis && !string.IsNullOrEmpty(cacheConfig.RedisConnectionString))
    {
        // Redis Cache
        builder.Services.AddSingleton<IConnectionMultiplexer>(provider =>
        {
            return ConnectionMultiplexer.Connect(cacheConfig.RedisConnectionString);
        });
        builder.Services.AddScoped<ICacheService, RedisCacheService>();
    }
    else
    {
        // In-Memory Cache
        builder.Services.AddMemoryCache();
        builder.Services.AddScoped<ICacheService, InMemoryCacheService>();
    }
}
else
{
    // No-op cache service for when caching is disabled
    builder.Services.AddScoped<ICacheService, NoCacheService>();
}

// MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(LoginCommand).Assembly));

// Repositories and Services
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<ITokenService, JwtTokenService>();
builder.Services.AddScoped<IEmailService, EmailService>();

// JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
