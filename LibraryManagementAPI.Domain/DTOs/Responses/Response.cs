using System.Net;

namespace LibraryManagementAPI.Domain.DTOs.Responses
{
    public class Response<T>
    {
        public T Data { get; set; }
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
