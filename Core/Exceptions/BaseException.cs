namespace Core.Exceptions
{
    public abstract class BaseException : Exception
    {
        public string Error { get; }
        public abstract int StatusCode { get; }

        public BaseException(string error)
        {
            Error = error;
        }
    }
}
