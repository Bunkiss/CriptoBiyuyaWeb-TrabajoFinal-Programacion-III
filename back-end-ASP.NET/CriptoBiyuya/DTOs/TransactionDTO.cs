using System;
using System.ComponentModel.DataAnnotations;

namespace CriptoBiyuya.DTOs
{
    public class TransactionDTO
    {
        public int id { get; set; }
        [Required]
        public string crypto_code { get; set; }
        [Required]
        public string action { get; set; }
        [Required]
        [Range(0.00000001, double.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0")]
        public decimal crypto_amount { get; set; }
        public decimal money { get; set; }
        [Required]
        public DateTime datetime { get; set; }
        [Required]
        public int client_id { get; set; }
    }
}
