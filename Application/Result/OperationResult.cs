namespace Application.Result;

public class OperationResult<T>
{
    public bool IsSuccess { get; init; }
    public string Message { get; init; } = string.Empty;
    public T? Data { get; init; }
}
