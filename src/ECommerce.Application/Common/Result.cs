namespace ECommerce.Application.Common
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;

        public T? Value { get; }
        public string? Error { get; }
        public ErrorType ErrorType { get; }

        private Result(bool isSuccess, T? value, string? error, ErrorType errorType)
        {
            IsSuccess = isSuccess;
            Value = value;
            Error = error;
            ErrorType = errorType;
        }

        public static Result<T> Success(T value)
        {
            return new Result<T>(true, value, null, ErrorType.None);
        }

        public static Result<T> Failure(string error)
        {
            return new Result<T>(false, default, error, ErrorType.Validation);
        }

        public static Result<T> NotFound(string error)
        {
            return new Result<T>(false, default, error, ErrorType.NotFound);
        }
    }
}