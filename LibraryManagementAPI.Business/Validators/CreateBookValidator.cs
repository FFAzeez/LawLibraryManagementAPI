using FluentValidation;
using LibraryManagementAPI.Domain.DTOs.Requests;

namespace LibraryManagementAPI.Business.Validators;

public class CreateBookValidator:AbstractValidator<BookDTO>
{
    public CreateBookValidator()
    {
        RuleFor(x => x.Title).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .NotNull()
            .WithMessage("Title is required");
        RuleFor(x => x.Author).NotEmpty()
            .WithMessage("Author is required");
        RuleFor(x => x.ISBN).NotEmpty()
            .WithMessage("ISBN is required");
        RuleFor(x => x.PublishedDate).NotEmpty()
            .WithMessage("PublishedDate is required");
    }
}