using System.Threading.Tasks;
using CriptoBiyuya.Models;
using CriptoBiyuya.DTOs;
using System.Collections.Generic;

namespace CriptoBiyuya.Services
{
    public interface ITransactionService
    {
        Task<Transaction> CreateAsync(TransactionDTO dto);
        Task<List<Transaction>> GetAllAsync();
        Task<Transaction?> GetByIdAsync(int id);
        Task<List<Transaction>> GetByClientAsync(int clientId);
        Task UpdateAsync(int id, TransactionDTO dto);
        Task DeleteAsync(int id);
        Task<decimal> GetBalanceAsync(int clientId, string cryptoCode);
    }
}