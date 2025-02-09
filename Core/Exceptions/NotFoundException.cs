namespace Core.Exceptions
{
    public class NotFoundException : BaseException
    {
        public override int StatusCode { get; } = 404;
        public NotFoundException(string message) : base(message) { }
    }
}
