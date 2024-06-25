namespace Progi.BidCalculator.Core.Exceptions;

/// <summary>
/// Represents an exception that is thrown when validation fails.
/// </summary>
public sealed class ValidationException : BusinessException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationException"/> class.
    /// </summary>
    public ValidationException()
        : base()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public ValidationException(string message)
        : base(message)
    {
    }
}

