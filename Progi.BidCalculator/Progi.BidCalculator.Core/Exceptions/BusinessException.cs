namespace Progi.BidCalculator.Core.Exceptions;

/// <summary>
/// Represents an exception that is thrown when a business rule or logic fails.
/// </summary>
public class BusinessException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BusinessException"/> class.
    /// </summary>
    public BusinessException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BusinessException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public BusinessException(string message)
        : base(message)
    {
    }
}