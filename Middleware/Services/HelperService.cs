using Core.Models;

namespace Middleware.Services
{
    public class HelperService
    {
        public static class DeniedReasons
        {
            public static string INSUFFICIENT_INCOME = "Income is insufficient.";
        }

        public static void ProcessApplication(Application application, Action<string, List<string>> response)
        {
            List<string> reasons = [];

            if (CheckIncome(application))
                reasons.Add(DeniedReasons.INSUFFICIENT_INCOME);

            // Other checks go here

            if (reasons.Count is 0)
                response("APPROVED", ["Your application has been approved."]);
            else
                response("DENIED", reasons);

        }

        private static bool CheckIncome(Application application)
        {
            Applicant primary = application.PrimaryApplicant ?? throw new Exception();

            return primary.Income < 15000;
        }
    }
}
