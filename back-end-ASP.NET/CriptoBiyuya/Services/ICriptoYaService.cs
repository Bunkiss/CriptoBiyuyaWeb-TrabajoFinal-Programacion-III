using System.Threading.Tasks;

namespace CriptoBiyuya.Services
{
    public interface ICriptoYaService
    {
        Task<decimal> GetPriceArs(string cryptoCode, string action);
    }
}