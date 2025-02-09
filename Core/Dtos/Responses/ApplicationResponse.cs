using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Dtos.Responses
{
    public class ApplicationResponse
    {
        public int Id { get; set; }
        public string ApplicationType { get; set; } = string.Empty;
        public string ApplicationStatus { get; set; } = string.Empty;
        public List<ApplicantResponse> Applicants { get; set; } = [];
        public Applicant PrimaryApplicant { get; set; } = new Applicant();
    }
}
