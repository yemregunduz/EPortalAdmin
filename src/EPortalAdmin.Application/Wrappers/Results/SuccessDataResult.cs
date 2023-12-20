using System.Text.Json.Serialization;

namespace EPortalAdmin.Application.Wrappers.Results
{
    public class SuccessDataResult<T> : DataResult<T>
    {
        [JsonConstructor]
        public SuccessDataResult(T data, string message) : base(data, isSuccess: true, message)
        {

        }
        public SuccessDataResult(T data) : base(data, isSuccess: true)
        {

        }
    }
}
