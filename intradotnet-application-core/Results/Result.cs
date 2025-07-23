using IntraDotNet.Application.Core.Interfaces;

namespace IntraDotNet.Application.Core.Results;

/// <summary>
/// Represents the result of an operation.
/// </summary>
/// <remarks>
/// This class encapsulates the outcome of an operation, indicating whether it was successful or not.
/// It can also hold error messages if the operation failed.
/// </remarks>
/// <example>
/// <code>
/// var result = Result.Success();
/// if (result.IsSuccess)
/// {
///     // Operation succeeded
/// }
/// else
/// {
///     // Handle failure
///     Console.WriteLine(result.AggregateErrors);
/// }
/// </code>
/// </example>
/// <seealso cref="ValueResult{T}"/>
/// <seealso cref="IResult"/>
public struct Result : IResult
{
    /// <summary>
    /// Gets a value indicating whether the operation was successful.
    /// </summary>
    /// <value>
    /// <c>true</c> if the operation was successful; otherwise, <c>false</c>.
    /// </value>
    /// <remarks>
    /// This property indicates the success status of the operation.
    /// </remarks>
    public bool IsSuccess { get; }

    /// <summary>
    /// Gets a value indicating whether the operation failed.
    /// </summary>
    /// <value>
    /// <c>true</c> if the operation failed; otherwise, <c>false</c>.
    /// </value>
    /// <remarks>
    /// This property is the inverse of <see cref="IsSuccess"/> and indicates whether the operation was not successful.
    /// </remarks>
    public bool IsFailure => !IsSuccess;

    /// <summary>
    /// Gets the collection of error messages associated with the operation.
    /// </summary>
    /// <value>
    /// An enumerable collection of error messages. If the operation was successful, this will be an empty collection.
    /// </value>
    /// <remarks>
    /// This property contains any error messages that were generated during the operation. If there are no errors, it will return an empty collection.
    /// </remarks>
    public IEnumerable<string> Errors { get; } = [];

    private Result(bool isSuccess)
    {
        IsSuccess = isSuccess;
    }

    private Result(IEnumerable<string>? errors)
    {
        IsSuccess = false;
        Errors = errors ?? [];
    }

    /// <summary>
    /// Gets a string that aggregates all error messages.
    /// </summary>
    /// <value>
    /// A single string containing all error messages, separated by new lines. If there are no errors, this will be <c>null</c>.
    /// </value>
    /// <remarks>
    /// This property concatenates all error messages into a single string for easier display or logging.
    /// If there are no errors, it returns <c>null</c>.
    /// </remarks>
    public string? AggregateErrors
    {
        get
        {
            if (Errors is null || !Errors.Any())
            {
                return null;
            }

            return string.Join(Environment.NewLine, Errors);
        }
    }

    /// <summary>
    /// Creates a successful result.
    /// </summary>
    /// <returns>
    /// A <see cref="Result"/> instance representing a successful operation.
    /// </returns>
    /// <remarks>
    /// This method is used to create a result that indicates the operation was successful.
    /// </remarks>
    public static Result Success() => new(true);

    /// <summary>
    /// Creates a failed result with a single error message.
    /// </summary>
    /// <param name="error">The error message.</param>
    /// <returns>
    /// A <see cref="Result"/> instance representing a failed operation with the specified error message.
    /// </returns>
    /// <remarks>
    /// This method is used to create a result that indicates the operation failed with a specific error message.
    /// </remarks>
    public static Result Failure(string error) => new([error]);

    /// <summary>
    /// Creates a failed result with multiple error messages.
    /// </summary>
    /// <param name="errors">An enumerable collection of error messages.</param>
    /// <returns>
    /// A <see cref="Result"/> instance representing a failed operation with the specified error messages.
    /// </returns>
    /// <remarks>
    /// This method is used to create a result that indicates the operation failed with multiple error messages.
    /// </remarks>
    public static Result Failure(IEnumerable<string> errors) => new(errors);

    /// <summary>
    /// Creates a failed result from an exception.
    /// </summary>
    /// <param name="ex">The exception that caused the failure.</param>
    /// <returns>
    /// A <see cref="Result"/> instance representing a failed operation with the exception's message.
    /// </returns>
    /// <remarks>
    /// This method is used to create a result that indicates the operation failed due to an exception.
    /// If the exception's message is <c>null</c>, it will create a result with an empty collection of errors.
    /// </remarks>
    public static Result Failure(Exception ex) => new(ex.Message is not null ? [ex.Message] : Array.Empty<string>());

    /// <summary>
    /// Creates a failed result from multiple exceptions.
    /// </summary>
    /// <param name="exceptions">An enumerable collection of exceptions that caused the failure.</param>
    /// <returns>
    /// A <see cref="Result"/> instance representing a failed operation with the messages from the exceptions.
    /// </returns>
    /// <remarks>
    /// This method is used to create a result that indicates the operation failed due to multiple exceptions.
    /// It collects the messages from each exception and creates a result with those messages.
    /// </remarks>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="exceptions"/> is <c>null</c>.</exception>
    public static Result Failure(IEnumerable<Exception> exceptions) => new(exceptions.Select(e => e.Message).Where(m => m is not null));

    public static implicit operator Result(string error) => Failure(error);
}