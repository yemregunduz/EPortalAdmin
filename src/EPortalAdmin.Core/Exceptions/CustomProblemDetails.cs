using EPortalAdmin.Core.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EPortalAdmin.Core.Exceptions
{
    public class CustomProblemDetails : ProblemDetails
    {
        public ExceptionCode Code { get; set; }
        public string CodeDescription { get; set; }
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
