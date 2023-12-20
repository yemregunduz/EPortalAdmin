namespace EPortalAdmin.Core.Logging
{
    public class ExceptionLog
    {
        public Guid CorrelationId { get; set; }
        public int HttpStatusCode { get; set; }
        public string Title { get; set; }
        public string ExceptionMessage { get; set; }
        public string InnerExceptionMessage { get; set; }
        public string StackTrace { get; set; }
        public string ValidationErrors { get; set; }
        public string Instance { get; set; }
        public string Type { get; set; }

    }
}
