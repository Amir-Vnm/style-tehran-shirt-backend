using System.Net;

namespace Shop.Common.Dto
{
    public class ApiResult
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
    public class ApiResult<T>
    {
        public bool IsSuccess { get; set; }
        public T Data { get; set; }
        public string? Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
