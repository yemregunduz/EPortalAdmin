using EPortalAdmin.Core.Domain.Constants;
using EPortalAdmin.Core.Domain.Enums;
using EPortalAdmin.Core.Utilities.Extensions;

namespace EPortalAdmin.Core.Exceptions
{
    public class NotFoundException(string message, ExceptionCode code) : BaseException(message, code)
    {
        public NotFoundException(string message) : this(message, ExceptionCode.Unknown)
        {
        }

        public NotFoundException(ExceptionCode code) : this(code.GetDescription(), code)
        {
        }

        public NotFoundException() : this(DefaultExceptionMessages.NotFound, ExceptionCode.Unknown)
        {
        }
    }
}
