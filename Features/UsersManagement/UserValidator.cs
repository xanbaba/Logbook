using FluentValidation;
using Logbook.Entities;

namespace Logbook.Features.UsersManagement;

public class UserValidator : AbstractValidator<UserDTO>
{
    public UserValidator()
    {
        RuleFor(u => u.Email).EmailAddress().NotEmpty().MaximumLength(255)
            .When(u => u.Email is not null);
        RuleFor(u => u.FirstName).NotEmpty().MaximumLength(50);
        RuleFor(u => u.LastName).NotEmpty().MaximumLength(100);
        RuleFor(u => u.FatherName).NotEmpty().MaximumLength(50)
            .When(u => u.FatherName is not null);
        RuleFor(u => u.UtcBornAt)
            .Must(b => b.HasValue && b.Value < DateOnly.FromDateTime(DateTime.UtcNow))
            .WithMessage("You can't add not born people :)");
        RuleFor(u => u.Role).NotEmpty().IsEnumName(typeof(UserRole), false)
            .When(u => u.Role is not null)
            .WithMessage("Role must be either of these values: student, teacher, admin");
        RuleFor(u => u.Login).NotEmpty().Length(8, 50);
        RuleFor(u => u.Password).NotEmpty().Length(8, 255)
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$")
            .WithMessage(
                "Password must be at least 8 characters long, contain at least one uppercase and one lowercase letter, one number and one special character(@, $, !, %, *, ?, &).");
    }
}