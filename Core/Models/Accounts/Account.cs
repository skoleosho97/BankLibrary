using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models.Accounts
{
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string AccountNumber { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public Member PrimaryAccountHolder { get; set; } = new Member();

        public int Balance { get; set; }

        public List<Member> Members { get; set; } = [];

        //public List<Card> Cards { get; set; } = [];
    }
}
