namespace ManageEventBackend.Applications.Responses
{
    public class Response
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public string? Error { get; set; }
        public object? Data { get; set; }
    }
}
