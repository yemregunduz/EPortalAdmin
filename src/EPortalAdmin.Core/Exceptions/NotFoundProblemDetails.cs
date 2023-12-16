using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EPortalAdmin.Core.Exceptions
{
    public class NotFoundProblemDetails : ProblemDetails
    {
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
