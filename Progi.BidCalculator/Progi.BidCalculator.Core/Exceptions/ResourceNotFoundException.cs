namespace Progi.BidCalculator.Core.Exceptions;

/// <summary>
/// Represents an exception that is thrown when a requested resource is not found.
/// </summary>
public sealed class ResourceNotFoundException : BusinessException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ResourceNotFoundException"/> class.
    /// </summary>
    public ResourceNotFoundException()
        : base()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ResourceNotFoundException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public ResourceNotFoundException(string message)
        : base(message)
    {
    }
}

