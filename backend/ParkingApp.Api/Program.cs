using Microsoft.EntityFrameworkCore;
using ParkingApp.Infrastructure.Persistence;
using ParkingApp.Application.Interfaces;
using ParkingApp.Infrastructure.Services;
using ParkingApp.Api.Middlewares;
using FluentValidation;
using FluentValidation.AspNetCore;
using ParkingApp.Application.Validators;
using ParkingApp.Infrastructure.Configurations;
using ParkingApp.Infrastructure.ExternalServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers();

builder.Services
    .AddFluentValidationAutoValidation();

builder.Services
    .AddValidatorsFromAssemblyContaining<CreateVehicleEntryRequestValidator>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString)
    );
});

builder.Services.AddScoped<IVehicleService, VehicleService>();

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

builder.Services.Configure<EmailApiSettings>(
    builder.Configuration.GetSection("EmailApi"));

builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection("EmailSettings"));

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.UseCors("AllowAngular");

//app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();