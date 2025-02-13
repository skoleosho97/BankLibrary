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
        public Member PrimaryAccountHolder { get; set; } = null!;
        public int PrimaryAccountHolderId { get; set; }
        public int Balance { get; set; }
        public List<Member> Members { get; set; } = [];
        //public List<Card> Cards { get; set; } = [];

        private AccountType? GetAccountType()
        {
            var accountType = this.GetType().Name;

            if (Enum.TryParse<AccountType>(accountType, out AccountType enumType))
                return enumType;

            return null;
        }

        private static string GenerateRandomSequence(int length)
        {
            List<int> sequence = [];

            for (int i = 0; i < length; i++)
            {
                Random random = new();
                sequence.Add(random.Next(0, 9));
            }

            return string.Join("", sequence);
        }

        public string GenerateAccountNumber()
        {
            string accountTypeSegment = $"{(int)GetAccountType()! + 1}0{(int)GetAccountType()! + 1}";
            string randomNumberSegment = GenerateRandomSequence(4);

            return $"001{accountTypeSegment}{randomNumberSegment}";
        }
    }
}
