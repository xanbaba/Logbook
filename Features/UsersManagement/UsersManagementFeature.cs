using FluentValidation;
using Logbook.Features.UsersManagement.Services;

namespace Logbook.Features.UsersManagement;

public abstract class UsersManagementFeature : IFeature
{
    public static void Build(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUsersContext, UsersContext>();

        builder.Services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<UsersMapperProfile>();
        });

        builder.Services.AddScoped<IValidator<UserDTO>, UserValidator>();
    }

    public static void Configure(WebApplication app)
    {
        app.MapGroup("/api").MapEndpoints<UsersManagementEndpointMapper>();
    }
}