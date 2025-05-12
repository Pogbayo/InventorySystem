namespace InventorySystem.API.Common
{
    public class ApiResponse<T>
    {
        public bool Success { get; private set; }
        public string? Message { get; private set; }
        public T? Data { get; private set; }
        public string? Error { get; private set; }

        public static ApiResponse<T> SuccessResponse(T data, string? message = null)
            => new ApiResponse<T> { Success = true, Data = data, Message = message };

        public static ApiResponse<T> FailureResponse(string error, string? message = null)
        => new ApiResponse<T> { Success = false, Error = error, Message = message };
    }
}
