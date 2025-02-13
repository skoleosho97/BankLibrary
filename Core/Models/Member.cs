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
        public string MembershipId { get; set; } = GenerateMembershipId();
        public Applicant Applicant { get; set; } = null!;
        public List<Account> Accounts { get; set; } = [];
        //public List<Card> Cards { get; set; } = [];

        private static string GenerateMembershipId()
        {
            List<int> sequence = [];

            for (int i = 0; i < 8; i++)
            {
                Random random = new();
                sequence.Add(random.Next(0, 9));
            }

            return string.Join("", sequence);
        }
    }
}
