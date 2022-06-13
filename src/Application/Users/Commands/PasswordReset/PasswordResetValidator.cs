using AffiliateHub.Application.Common.Interfaces;
using FluentValidation;

namespace AffiliateHub.Application.Users.Commands.PasswordReset;

public class PasswordResetCommandValidator : AbstractValidator<PasswordResetCommand>
{
    private readonly IApplicationDbContext _context;

    public PasswordResetCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Token)
            .NotEmpty().WithMessage("Token is required");

        RuleFor(v => v.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters");
    }
}
