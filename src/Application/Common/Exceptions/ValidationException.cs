using FluentValidation.Results;

namespace AffiliateHub.Application.Common.Exceptions;

public class ValidationException : Exception
{
    public ValidationException()
        : base("One or more validation failures have occurred.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ValidationException(IEnumerable<ValidationFailure> failures)
        : this()
    {
        Errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => Char.ToLowerInvariant(failureGroup.Key[0]) + failureGroup.Key.Substring(1), failureGroup => failureGroup.ToArray());
    }

    public ValidationException(string propertyName, string errorMessage)
        : this()
    {
        var propertyNameKey = Char.ToLowerInvariant(propertyName[0]) + propertyName.Substring(1);
        Errors.Add(propertyNameKey, new[] { errorMessage });
    }

    public IDictionary<string, string[]> Errors { get; }
}
