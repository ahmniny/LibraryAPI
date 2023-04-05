using FluentValidation;
using LibraryAPI.dto;
using LibraryAPI.Models;

namespace LibraryAPI.validators
{
    public class BookValidator:AbstractValidator<BookDTO>
    {
        public BookValidator()
        {
            RuleFor(b => b.Title).NotEmpty();
            RuleFor(b => b.Year).NotEmpty();
            RuleFor(b => b.AuthorId).NotEmpty();
        }
    }
}
