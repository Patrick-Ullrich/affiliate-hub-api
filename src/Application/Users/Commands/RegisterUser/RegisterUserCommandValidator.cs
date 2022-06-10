using AffiliateHub.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AffiliateHub.Application.Users.Commands.RegisterUser;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    private readonly IApplicationDbContext _context;

    public RegisterUserCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.EmailAddress)
            .NotEmpty().WithMessage("Email address is required")
            .MustAsync(BeUniqueEmailAddress).WithMessage("Email Address is already in use")
            .EmailAddress().WithMessage("Email Address is invalid");

        RuleFor(v => v.PhoneNumber)
            .MustAsync(BeUniquePhoneNumber).WithMessage("Phone number is already in use");

        RuleFor(v => v.FirstName)
            .NotEmpty().WithMessage("First name is required")
            .MaximumLength(100).WithMessage("First name must not exceed 100 characters");

        RuleFor(v => v.LastName)
            .NotEmpty().WithMessage("Last name is required")
            .MaximumLength(100).WithMessage("Last name must not exceed 100 characters");

        RuleFor(v => v.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters");
    }

    public async Task<bool> BeUniqueEmailAddress(string emailAddress, CancellationToken cancellationToken)
    {
        return await _context.Users
            .AllAsync(l => l.EmailAddress != emailAddress, cancellationToken);
    }

    public async Task<bool> BeUniquePhoneNumber(string phoneNumber, CancellationToken cancellationToken)
    {
        return await _context.Users
            .AllAsync(l => l.PhoneNumber != phoneNumber, cancellationToken);
    }
}
