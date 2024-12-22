namespace CF_Sample_Api.Contracts
{
    public class ApiResponse<T>
    {
        public T? Data { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public IList<string>? ErrorMsgs { get; set; }
        public bool IsSuccess { get; set; }
        public ApiResponse() => ErrorMsgs = [];
    }
}
