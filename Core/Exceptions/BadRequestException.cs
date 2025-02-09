namespace Core.Exceptions
{
    public class BadRequestException : BaseException
    {
        public override int StatusCode { get; } = 409;
        public BadRequestException(string message) : base(message) { }
    }
}
