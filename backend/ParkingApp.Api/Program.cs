using FluentValidation;
using FluentValidation.AspNetCore;

using Microsoft.EntityFrameworkCore;

using ParkingApp.Api.Middlewares;

using ParkingApp.Application.Interfaces;
using ParkingApp.Application.Validators;

using ParkingApp.Infrastructure.Configurations;
using ParkingApp.Infrastructure.Persistence;
using ParkingApp.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);


// ==============================
// Controllers & Validation
// ==============================

builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddValidatorsFromAssemblyContaining<
    CreateVehicleEntryRequestValidator>();


// ==============================
// Swagger
// ==============================

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();


// ==============================
// Database
// ==============================

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString =
        builder.Configuration.GetConnectionString(
            "DefaultConnection"
        );

    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString)
    );
});


// ==============================
// Dependency Injection
// ==============================

builder.Services.AddScoped<
    IVehicleService,
    VehicleService>();

builder.Services.AddHttpClient<
    IEmailService,
    EmailService>();


// ==============================
// Configuration Bindings
// ==============================

builder.Services.Configure<EmailApiSettings>(
    builder.Configuration.GetSection("EmailApi"));


// ==============================
// CORS
// ==============================

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin();
    });
});


// ==============================
// Build App
// ==============================

var app = builder.Build();


// ==============================
// Middleware Pipeline
// ==============================

app.UseSwagger();

app.UseSwaggerUI();

app.UseCors("AllowAngular");

// app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();