using EPortalAdmin.Core.Domain.Enums;

namespace EPortalAdmin.Core.Exceptions
{
    public class BaseException: Exception
    {
        protected BaseException(string? message, ExceptionCode? code):base(message)
        {
            this.Code = code ?? ExceptionCode.Unknown;
        }
        public ExceptionCode Code { get; set; }
    }
}
