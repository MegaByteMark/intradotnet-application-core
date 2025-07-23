using IntraDotNet.Application.Core.Interfaces;

namespace IntraDotNet.Application.Core.Results;

/// <summary>
/// Represents the result of an operation that can return a value.
/// </summary>
/// <typeparam name="T">The type of the value returned by the operation.</typeparam>
/// <remarks>
/// This class encapsulates the outcome of an operation, indicating whether it was successful or not.
/// It can also hold a value if the operation succeeded, or error messages if it failed.
/// </remarks>
/// <example>
/// <code>
/// var result = ValueResult<int>.Success(42);
/// if (result.IsSuccess)
/// {
///     // Operation succeeded, use result.Value
///     Console.WriteLine($"Value: {result.Value}");
/// }
/// else
/// {
///     // Handle failure
///     Console.WriteLine(result.AggregateErrors);
/// }
/// </code>
/// </example>
/// <seealso cref="Result"/>
/// <seealso cref="IResult"/>
public struct ValueResult<T> : IResult
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
    /// Gets the value returned by the operation if it was successful.
    /// </summary>
    /// <value>
    /// The value of type <typeparamref name="T"/> if the operation was successful; otherwise, <c>null</c>.
    /// </value>
    /// <remarks>
    /// This property holds the result of the operation if it succeeded. If the operation failed,
    /// this property will be <c>null</c>.
    /// </remarks>
    public T? Value { get; }

    /// <summary>
    /// Gets the collection of error messages associated with the operation.
    /// </summary>
    /// <value>
    /// An enumerable collection of error messages. If the operation was successful, this will be an empty collection.
    /// </value>
    /// <remarks>
    /// This property contains any error messages that were generated during the operation. If there are no errors, it will return an empty collection.
    /// </remarks>
    public IEnumerable<string> Errors { get; }

    private ValueResult(T? value)
    {
        IsSuccess = true;
        Value = value;
        Errors = [];
    }

    private ValueResult(IEnumerable<string> errors)
    {
        IsSuccess = false;
        Value = default;
        Errors = errors;
    }

    /// <summary>
    /// Gets a concatenated string of all error messages, separated by new lines.
    /// </summary>
    /// <value>
    /// A single string containing all error messages, or <c>null</c> if there are no errors.
    /// </value>
    /// <remarks>
    /// This property aggregates all error messages into a single string, making it easier to display or log errors.
    /// If there are no errors, this property will return <c>null</c>.
    /// </remarks>
    /// <returns>
    /// A string containing all error messages, or <c>null</c> if there are no errors.
    /// </returns>
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
    /// Creates a successful result with no value.
    /// </summary>
    /// <returns>A new instance of <see cref="ValueResult{T}"/> representing a successful operation with no value.</returns>
    /// <remarks>
    /// This method is useful when the operation is successful but does not return a specific value.
    /// </remarks>
    /// <returns>
    /// A new instance of <see cref="ValueResult{T}"/> representing a successful operation with no value.
    /// </returns>
    public static ValueResult<T> Success() => new(default(T));

    /// <summary>
    /// Creates a successful result with a specified value.
    /// </summary>
    /// <param name="value">The value to return if the operation was successful.</param>
    /// <returns>A new instance of <see cref="ValueResult{T}"/> representing a successful operation with the specified value.</returns>
    /// <remarks>
    /// This method is used to create a result that indicates the operation was successful and includes a value.
    /// </remarks>
    /// <returns>
    /// A new instance of <see cref="ValueResult{T}"/> representing a successful operation with the specified value.
    /// </returns>
    public static ValueResult<T> Success(T value) => new(value);

    /// <summary>
    /// Creates a failed result with a single error message.
    /// </summary>
    /// <param name="error">The error message.</param>
    /// <returns>A new instance of <see cref="ValueResult{T}"/> representing a failed operation with the specified error message.</returns>
    /// <remarks>
    /// This method is used to create a result that indicates the operation failed with a specific error message.
    /// </remarks>
    /// <returns>
    /// A new instance of <see cref="ValueResult{T}"/> representing a failed operation with the specified error message.
    /// </returns>
    public static ValueResult<T> Failure(string error) => new([error]);

    /// <summary>
    /// Creates a failed result with multiple error messages.
    /// </summary>
    /// <param name="errors">An enumerable collection of error messages.</param>    
    /// <returns>A new instance of <see cref="ValueResult{T}"/> representing a failed operation with the specified error messages.</returns>
    /// <remarks>
    /// This method is used to create a result that indicates the operation failed with multiple error messages.
    /// </remarks>
    /// <returns>
    /// A new instance of <see cref="ValueResult{T}"/> representing a failed operation with the specified error messages.
    /// </returns>
    public static ValueResult<T> Failure(IEnumerable<string> errors) => new(errors);

    /// <summary>
    /// Creates a failed result with a single exception.
    /// </summary>
    /// <param name="ex">The exception that caused the failure.</param>
    /// <returns>A new instance of <see cref="ValueResult{T}"/> representing a failed operation with the exception's message.</returns>
    /// <remarks>
    /// This method is used to create a result that indicates the operation failed due to an exception.
    /// If the exception's message is <c>null</c>, it will create a result with an empty collection of errors.
    /// </remarks>
    /// <returns>
    /// A new instance of <see cref="ValueResult{T}"/> representing a failed operation with the exception's message.
    /// </returns>
    public static ValueResult<T> Failure(Exception ex) => new(ex.Message is not null ? [ex.Message] : Array.Empty<string>());

    /// <summary>
    /// Creates a failed result from a collection of exceptions.
    /// </summary>
    /// <param name="exceptions">An enumerable collection of exceptions that caused the failure.</param>
    /// <returns>A new instance of <see cref="ValueResult{T}"/> representing a failed operation with the exception messages.</returns>
    /// <remarks>
    /// This method is used to create a result that indicates the operation failed due to multiple exceptions.
    /// It will extract the messages from the exceptions and return them as error messages.
    /// </remarks>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="exceptions"/> is <c>null</c>.</exception>
    /// <exception cref="ArgumentException">Thrown if <paramref name="exceptions"/> is empty.</exception>
    /// <returns>A new instance of <see cref="ValueResult{T}"/> representing a failed operation with the exception messages.</returns>
    public static ValueResult<T> Failure(IEnumerable<Exception> exceptions) => new(exceptions.Select(e => e.Message).Where(m => m is not null));


    public static implicit operator ValueResult<T>(T value) => Success(value);
    public static implicit operator ValueResult<T>(string error) => Failure(error);
}