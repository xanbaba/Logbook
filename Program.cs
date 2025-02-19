using Logbook;
using Logbook.DataAccess;
using Logbook.Extensions;
using Logbook.Features.AuthFeature;
using Logbook.Features.UsersManagement;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(optionsBuilder =>
{
    optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


// Features
builder.AddFeature<UsersManagementFeature>();
builder.AddFeature<AuthFeature>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Middleware for handling either BadRequest or Internal errors
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Features
app.UseFeature<UsersManagementFeature>();
app.UseFeature<AuthFeature>();

app.Services.CreateScope().ServiceProvider.GetRequiredService<AppDbContext>().Database.EnsureDeleted();
app.Services.CreateScope().ServiceProvider.GetRequiredService<AppDbContext>().Database.EnsureCreated();
// app.Services.CreateScope().ServiceProvider.GetRequiredService<AppDbContext>().Database.Migrate();
app.Run();
