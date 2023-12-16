namespace EPortalAdmin.Core.Logging
{
    public class LogDetailWithException : LogDetail
    {
        public string ExceptionMessage { get; set; }
        public string InnerExceptionMessage { get; set; }
        public string StackTrace { get; set; }
    }
}
