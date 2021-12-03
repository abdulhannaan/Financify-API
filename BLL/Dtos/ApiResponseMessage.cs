using System.Net;

namespace BLL.Dtos
{
    public class ApiResponseMessage
    {
        public ApiResponseMessage()
        {
            this.Message = "Request Failed.";
        }
        public HttpStatusCode Status { get; set; }
        public string Message { get; set; }
        public object Response { get; set; }
    }
}
