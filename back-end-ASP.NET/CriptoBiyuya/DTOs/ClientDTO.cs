using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CriptoBiyuya.Models;

namespace CriptoBiyuya.DTOs
{
    public class ClientDTO
    {
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        [EmailAddress]
        public string email { get; set; }
        public List<TransactionDTO> transactions { get; set; } = new List<TransactionDTO>();
    }
}
