using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobBoard.Web.Infrastructure.Constants
{
    public static class Web
    {
        public const string CandidateRole = "CANDIDATE";
        public const string EmployerRole = "EMPLOYER";
        public const string AdministratorRole = "Administrator";

        public const string CandidatesArea = "Candidate";
        public const string EmployersArea = "Employer";
        public const string HomeArea = "Home";

        public const string TempDataSuccessMessageKey = "SuccessMessage";
        public const string TempDataErrorMessageKey = "ErrorMessage";
    }
}
