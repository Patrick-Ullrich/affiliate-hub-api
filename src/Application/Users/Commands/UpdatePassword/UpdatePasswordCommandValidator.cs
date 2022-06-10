using FluentValidation;

namespace AffiliateHub.Application.Users.Commands.UpdatePassword;

public class UpdatePasswordCommandValidator : AbstractValidator<UpdatePasswordCommand>
{

    public UpdatePasswordCommandValidator()
    {
        RuleFor(v => v.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters");

        RuleFor(v => v.CurrentPassword)
            .NotEmpty().WithMessage("Current Password Password is required");
    }
}
