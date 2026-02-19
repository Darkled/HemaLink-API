namespace Application.Models
{
    public class ResponseDto<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public IEnumerable<string?>? Errors { get; set; }

        public static ResponseDto<T> Ok(T? data, string? message = null)
        {
            return new ResponseDto<T>
            {
                Success = true,
                Data = data,
                Message = message
            };
        }

        public static ResponseDto<T> Fail(string? error, string? message = null)
        {
            return new ResponseDto<T>
            {
                Success = false,
                Errors = new[] { error },
                Message = message
            };
        }

        public static ResponseDto<T> Fail(IEnumerable<string> errors, string? message = null)
        {
            return new ResponseDto<T>
            {
                Success = false,
                Errors = errors,
                Message = message
            };
        }
    }
}
