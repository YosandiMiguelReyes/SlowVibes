namespace Application.Result;

public sealed class OperationResult<T>
{
    public bool IsSuccess { get;}
    public bool IsFailure => !IsSuccess;
    public string? Error { get;}
    public T? Value { get; init; }

    private OperationResult(bool isSuccess, string? error, T? value)
    {
        IsSuccess = isSuccess;
        Error = error;
        Value = value;
    }

    public static OperationResult<T> Success(T value)
    {
        ArgumentNullException.ThrowIfNull(value);

        return new OperationResult<T>(
            isSuccess: true,
            value: value,
            error: null);
    }

    public static OperationResult<T> Failure(string error)
    {
        if (string.IsNullOrWhiteSpace(error))
            throw new ArgumentException(
                "El mensaje de error es obligatorio.",
                nameof(error));

        return new OperationResult<T>(
            isSuccess: false,
            value: default,
            error: error);
    }
}

