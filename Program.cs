using Logbook;
using Logbook.Features.UsersManagement;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapEndpoints<UsersManagementEndpointMapper>();
app.MapEndpoints<StudentsManagementEndpointMapper>();
app.MapEndpoints<TeachersManagementEndpointMapper>();
app.MapEndpoints<AdminsManagementEndpointMapper>();

app.Run();
