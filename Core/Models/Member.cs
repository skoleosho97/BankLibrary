using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Models.Accounts;

namespace Core.Models
{
    public class Member
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public string MembershipId { get; set; } = string.Empty;
        public Applicant Applicant { get; set; } = new Applicant();
        public List<Account> Accounts { get; set; } = [];
        //public List<Card> Cards { get; set; } = [];
    }
}
