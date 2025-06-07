using System.ComponentModel.DataAnnotations;

namespace CriptoBiyuya.Models
{
    public class Client
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string email { get; set; }
        public List<Transaction> transactions { get; set; } // Navigation property for related transactions

    }
}
