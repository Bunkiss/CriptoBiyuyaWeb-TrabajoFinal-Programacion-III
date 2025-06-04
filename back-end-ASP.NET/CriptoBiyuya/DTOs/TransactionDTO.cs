using CriptoBiyuya.Models;

namespace CriptoBiyuya.DTOs
{
    public class TransactionDTO
    {
        public int id { get; set; }
        public string crypto_code { get; set; }
        public string action { get; set; }
        public decimal crypto_amount { get; set; }
        public decimal money { get; set; }
        public DateTime datetime { get; set; }
        public int client_id { get; set; }
    }
}
