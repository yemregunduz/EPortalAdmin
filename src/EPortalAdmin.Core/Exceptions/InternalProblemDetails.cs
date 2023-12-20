namespace EPortalAdmin.Core.Exceptions
{
    public class InternalProblemDetails : CustomProblemDetails
    {
        public string InnerExceptionDetail { get; set; }

    }
}
