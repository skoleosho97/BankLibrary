using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Application
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string ApplicationType { get; set; } = string.Empty;

        [Required]
        public string ApplicationStatus { get; set; } = string.Empty;
        
        [Required]
        public Applicant PrimaryApplicant { get; set; } = null!;

        public int PrimaryApplicantId { get; set; }
        
        public List<Applicant> Applicants { get; set; } = null!;

        //public int ApplicationAmount { get; set; }
        //public string LoanType { get; set; }
        //public CreditCardOffer CardOffer { get; set; }
        //public Account DepositAccount { get; set; } 

    }
}
