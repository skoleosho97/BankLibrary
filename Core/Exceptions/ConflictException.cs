namespace Core.Exceptions
{
    public class ConflictException : BaseException
    {
        public override int StatusCode { get; } = 409;
        public ConflictException(string message) : base(message) { }
    }
}
