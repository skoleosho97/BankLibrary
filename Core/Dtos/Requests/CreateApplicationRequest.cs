using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Dtos.Requests
{
    public class CreateApplicationRequest
    {
        // public ApplicationType ApplicationType { get; set; }
        [Required]
        public bool NoNewApplicants { get; set; }
        public List<int>? ApplicantList { get; set; }
        public List<CreateApplicantRequest>? Applicants { get; set; }
        
    }
}
