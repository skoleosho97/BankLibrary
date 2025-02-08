using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Application
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //public ApplicationType ApplicationType { get; set; }

        //public ApplicationStatus ApplicationStatus { get; set; }
        
        //public Applicant PrimaryApplicant { get; set; }
        
        //public List<Applicant> Applicants { get; set; }

    }
}
