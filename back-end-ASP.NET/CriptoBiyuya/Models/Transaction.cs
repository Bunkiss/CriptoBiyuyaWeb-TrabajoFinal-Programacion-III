using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CriptoBiyuya.Models
{
    public class Transaction
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string crypto_code { get; set; }
        [Required]
        public string action { get; set; } // "purchase" or "sale"
        [Required]
        public decimal crypto_amount { get; set; }
        [Required]
        public decimal money { get; set; }
        [Required]
        public DateTime datetime { get; set; } //"yyyy-MM-dd HH:mm"
        [ForeignKey("Client")]
        public int client_id { get; set; }
        public Client? client { get; set; }

    }
}
/*
 "crypto_code": "usdc",
   "action": "purchase",
   "client_id": 2,
   "crypto_amount": "1.01",
   "datetime": "2021-07-11 17:50"

 */