namespace AffiliateHub.Application.Common.Exceptions;

public class NotAuthorizedAccessException : Exception
{
    public NotAuthorizedAccessException() : base() { }

    public NotAuthorizedAccessException(string message)
        : base(message)
    {
    }
}
