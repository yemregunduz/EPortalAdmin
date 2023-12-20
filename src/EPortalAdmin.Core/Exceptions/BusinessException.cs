using EPortalAdmin.Core.Domain.Constants;
using EPortalAdmin.Core.Domain.Enums;
using EPortalAdmin.Core.Utilities.Extensions;

namespace EPortalAdmin.Core.Exceptions
{
    public class BusinessException(string message, ExceptionCode code) : BaseException(message,code)
    {
        public BusinessException(string message) : this(message, ExceptionCode.Unknown)
        {
        }

        public BusinessException(ExceptionCode code) : this(code.GetDescription(), code)
        {
        }

        public BusinessException() : this(DefaultExceptionMessages.Business, ExceptionCode.Unknown)
        {
        }
    }
}