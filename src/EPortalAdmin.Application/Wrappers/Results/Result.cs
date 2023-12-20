using System.Text.Json.Serialization;

namespace EPortalAdmin.Application.Wrappers.Results
{
    public class Result
    {
        [JsonConstructor]
        public Result(bool isSuccess, string message) : this(isSuccess)
        {
            Message = message;
        }

        public Result(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
        public bool IsSuccess { get; }

        public string Message { get; }
    }
}
