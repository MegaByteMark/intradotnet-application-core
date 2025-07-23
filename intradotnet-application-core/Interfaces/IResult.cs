namespace IntraDotNet.Application.Core.Interfaces;

/// <summary>
/// Interface representing a result of an operation.
/// </summary>
public interface IResult
{
    bool IsSuccess { get; }
    bool IsFailure { get; }
    public IEnumerable<string> Errors { get; }
    public string? AggregateErrors { get; }
}
