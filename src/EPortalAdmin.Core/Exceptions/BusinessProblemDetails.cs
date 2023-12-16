using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EPortalAdmin.Core.Exceptions
{
    public class BusinessProblemDetails : ProblemDetails
    {
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}

