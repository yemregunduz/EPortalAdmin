using EPortalAdmin.Core.Domain.Constants;
using EPortalAdmin.Core.Domain.Enums;
using EPortalAdmin.Core.Utilities.Extensions;

namespace EPortalAdmin.Core.Exceptions
{
    public class AuthorizationException(string message, ExceptionCode code) : BaseException(message,code)
    {
        public AuthorizationException(string message) : this(message, ExceptionCode.Unknown)
        {
        }
        public AuthorizationException(ExceptionCode code) : this(code.GetDescription(), code)
        {
        }
        public AuthorizationException() : this(DefaultExceptionMessages.Authorization,ExceptionCode.Unknown)
        {
            
        }
    }
}

