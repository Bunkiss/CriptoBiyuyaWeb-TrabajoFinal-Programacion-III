namespace CriptoBiyuya.DTOs
{
    public class TransPatchDTO
    {
        public string? Action { get; set; }
        public string? CryptoCode { get; set; }
        public decimal? Money { get; set; }
        public DateTime? DateTime { get; set; }
        public int? ClientId { get; set; }
    }
}
