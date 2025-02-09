using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Dtos.Requests
{
    public class CreateApplicationRequest
    {
        [Required]
        public string ApplicationType { get; set; } = string.Empty;
        [Required]
        public bool NoNewApplicants { get; set; }
        public List<int> ApplicantIds { get; set; } = [];
        public List<CreateApplicantRequest> Applicants { get; set; } = [];

        //public int ApplicationAmount { get; set; }
        //public int CardOfferId { get; set; }
        //public string DepositAccountNumber { get; set; } 
    }
}
