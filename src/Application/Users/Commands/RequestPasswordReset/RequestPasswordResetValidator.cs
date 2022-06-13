using AffiliateHub.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AffiliateHub.Application.Users.Commands.RequestPasswordReset;

public class RequestPasswordResetCommandValidator : AbstractValidator<RequestPasswordResetCommand>
{
    private readonly IApplicationDbContext _context;

    public RequestPasswordResetCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.EmailAddress)
            .NotEmpty().WithMessage("Email address is required")
            .EmailAddress().WithMessage("Email Address is invalid");
    }
}
