using FluentValidation;
using LibraryManagementAPI.Domain.DTOs.Requests;

namespace LibraryManagementAPI.Business.Validators;

public class RegisterUserValidator:AbstractValidator<RegisterDto>
{
    public RegisterUserValidator()
    {
        RuleFor(x => x.FirstName).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .NotNull()
            .WithMessage("FirstName is required");
        RuleFor(x => x.LastName)
            .Cascade(CascadeMode.Stop).NotNull().NotEmpty()
            .WithMessage("Last is required");
        RuleFor(x => x.Email)
            .NotEmpty().EmailAddress()
            .WithMessage("Email Address is required");
        RuleFor(x => x.Password).NotEmpty()
            .WithMessage("PublishedDate is required")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches(@"[0-9]").WithMessage("Password must contain at least one digit.")
            .Matches(@"[!@#$%^&*(),.?""':{}|<>]").WithMessage("Password must contain at least one special character.");;

    }
}