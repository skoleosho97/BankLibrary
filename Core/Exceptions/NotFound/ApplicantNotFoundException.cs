namespace Core.Exceptions.NotFound
{
    public class ApplicantNotFoundException : Exception
    {
        public ApplicantNotFoundException() : base("Applicant could not be found.") {}
    }
}
