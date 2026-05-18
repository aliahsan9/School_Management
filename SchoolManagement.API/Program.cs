using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;

using Microsoft.EntityFrameworkCore;

using Microsoft.IdentityModel.Tokens;

using Microsoft.OpenApi.Models;

using SchoolManagement.Application.Interfaces;

using SchoolManagement.Infrastructure.Persistence;

using SchoolManagement.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// ======================================================
// CONTROLLERS
// ======================================================

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

// ======================================================
// CORS
// ======================================================

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy =>
        {
            policy
                .WithOrigins(

                    "http://localhost:4200",
                    "https://localhost:4200"

                )

                .AllowAnyHeader()

                .AllowAnyMethod()

                .AllowCredentials();
        });
});

// ======================================================
// SWAGGER + JWT AUTH
// ======================================================

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "School Management API",
        Version = "v1"
    });

    // JWT AUTH FOR SWAGGER

    c.AddSecurityDefinition("Bearer",
        new OpenApiSecurityScheme
        {
            Name = "Authorization",

            Type = SecuritySchemeType.Http,

            Scheme = "Bearer",

            BearerFormat = "JWT",

            In = ParameterLocation.Header,

            Description =
                "Enter JWT Token Like: Bearer {token}"
        });

    c.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference =
                        new OpenApiReference
                        {
                            Type =
                                ReferenceType.SecurityScheme,

                            Id = "Bearer"
                        }
                },

                Array.Empty<string>()
            }
        });
});

// ======================================================
// DATABASE
// ======================================================

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString(
            "DefaultConnection"));
});

// ======================================================
// HTTP CONTEXT
// ======================================================

builder.Services.AddHttpContextAccessor();

// ======================================================
// DEPENDENCY INJECTION
// ======================================================

// AUTH

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IJwtService, JwtService>();

builder.Services.AddScoped<ITenantProvider, TenantProvider>();

// MODULE SERVICES

builder.Services.AddScoped<IStudentService, StudentService>();

builder.Services.AddScoped<ITeacherService, TeacherService>();

builder.Services.AddScoped<IClassService, ClassService>();

builder.Services.AddScoped<ISubjectService, SubjectService>();

builder.Services.AddScoped<IAttendanceService, AttendanceService>();

builder.Services.AddScoped<IFeeService, FeeService>();

builder.Services.AddScoped<IPaymentService, PaymentService>();

// ======================================================
// JWT AUTHENTICATION
// ======================================================

builder.Services
    .AddAuthentication(
        JwtBearerDefaults.AuthenticationScheme)

    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = true,

                ValidateAudience = true,

                ValidateLifetime = true,

                ValidateIssuerSigningKey = true,

                ValidIssuer =
                    builder.Configuration["Jwt:Issuer"],

                ValidAudience =
                    builder.Configuration["Jwt:Audience"],

                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            builder.Configuration["Jwt:Key"]!))
            };
    });

// ======================================================
// AUTHORIZATION
// ======================================================

builder.Services.AddAuthorization();

// ======================================================
// BUILD APP
// ======================================================

var app = builder.Build();

// ======================================================
// MIDDLEWARE
// ======================================================

// SWAGGER

app.UseSwagger();

app.UseSwaggerUI();

// HTTPS

app.UseHttpsRedirection();

// CORS

app.UseCors("AllowAngularApp");

// AUTH

app.UseAuthentication();

app.UseAuthorization();

// CONTROLLERS

app.MapControllers();

// ======================================================
// RUN
// ======================================================

app.Run();