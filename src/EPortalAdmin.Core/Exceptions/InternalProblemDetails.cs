using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EPortalAdmin.Core.Exceptions
{
    public class InternalProblemDetails : ProblemDetails
    {
        public override string ToString() => JsonConvert.SerializeObject(this);
        public string InnerExceptionDetail { get; set; }

    }
}
