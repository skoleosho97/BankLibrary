using Core.Models;

namespace Middleware.Services
{
    public class HelperService
    {
        public static class DeniedReasons
        {
            public static string INSUFFICIENT_INCOME = "Income is insufficient.";
        }

        public static async Task ProcessApplication(Application application, Func<string, List<string>, Task> response)
        {
            List<string> reasons = [];

            CheckIncome(application, reasons);

            // Other checks go here

            if (reasons.Count is 0)
                await response("APPROVED", ["Your application has been approved."]);
            else
                await response("DENIED", reasons);
        }

        private static void CheckIncome(Application application, List<string> reasons)
        {
            Applicant primary = application.PrimaryApplicant ?? throw new Exception();

            if (primary.Income < 15000)
                reasons.Add(DeniedReasons.INSUFFICIENT_INCOME);
        }
    }
}
