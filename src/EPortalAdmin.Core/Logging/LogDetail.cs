namespace EPortalAdmin.Core.Logging
{
    public class LogDetail
    {
        public int? UserId { get; set; }
        public Guid CorrelationId { get; set; }
        public int EndpointId { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string QueryString { get; set; }
        public string HttpMethod { get; set; }
        public long ResponseTimeInMilliseconds { get; set; }
        public string IpAddress { get; set; }
        public string BrowserName { get; set; }
        public int ResponseHttpStatusCode { get; set; }
        public string RequestBody { get; set; }
        public string ResponseBody { get; set; }
        public string HttpHeaders { get; set; }
        public string RouteValuesJson { get; set; }
    }
}
