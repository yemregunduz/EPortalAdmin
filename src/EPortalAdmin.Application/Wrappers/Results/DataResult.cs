using System.Text.Json.Serialization;

namespace EPortalAdmin.Application.Wrappers.Results
{
    public class DataResult<T> : Result
    {
        public T Data { get; }

        [JsonConstructor]
        public DataResult(T data, bool isSuccess, string message) : base(isSuccess, message)
        {
            Data = data;
        }
        public DataResult(T data, bool isSuccess) : base(isSuccess)
        {
            Data = data;
        }
    }
}
