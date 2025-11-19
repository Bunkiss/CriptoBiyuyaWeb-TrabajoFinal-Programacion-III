using System.Collections.Generic;
using System.Threading.Tasks;
using CriptoBiyuya.DTOs;
using CriptoBiyuya.Models;

namespace CriptoBiyuya.Services
{
    public interface IClientService
    {
        Task<Client> CreateAsync(ClientDTO dto);
        Task<List<Client>> GetAllAsync();
        Task<Client?> GetByIdAsync(int id);
        Task UpdateAsync(int id, ClientDTO dto);
        Task DeleteAsync(int id);
    }
}
