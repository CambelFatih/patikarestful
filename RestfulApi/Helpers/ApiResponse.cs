//Helpers/ApiResponse.cs
namespace MyWebApi.Helpers
{
    public class ApiResponse<T>
    {
        public T? Data { get; set; }
        public string? Error { get; set; }
        public int StatusCode { get; set; }
    }
}
