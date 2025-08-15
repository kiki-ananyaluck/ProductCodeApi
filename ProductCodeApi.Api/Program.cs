using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ProductCodeApi.Application.Interfaces;
using ProductCodeApi.Application.Services;
using ProductCodeApi.Application.Validators;
using ProductCodeApi.Infrastructure.Data;
using ProductCodeApi.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

// เพิ่ม CORS ให้ Angular
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") // Angular URL
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProductCodeService, ProductCodeService>();
builder.Services.AddScoped<IProductCodeRepository, ProductCodeRepository>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateCodeValidator>();

var app = builder.Build();

app.UseCors("AllowAngular");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
