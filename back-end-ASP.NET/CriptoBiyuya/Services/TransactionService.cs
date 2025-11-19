using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using CriptoBiyuya.Models;
using CriptoBiyuya.DTOs;


namespace CriptoBiyuya.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly AppDbContext _context;
        private readonly ICriptoYaService _cripto;
        private readonly string[] _allowedCryptos = new[] { "btc", "eth", "usdc" };

        public TransactionService(AppDbContext context, ICriptoYaService cripto)
        {
            _context = context;
            _cripto = cripto;
        }

        public async Task<Transaction> CreateAsync(TransactionDTO dto)
        {
            ValidateDto(dto);

            var client = await _context.Clients.FindAsync(dto.client_id);
            if (client == null) throw new InvalidOperationException("Cliente no encontrado");

            if (dto.action == "sale")
            {
                var balance = await GetBalanceAsync(dto.client_id, dto.crypto_code);
                if (dto.crypto_amount > balance)
                    throw new InvalidOperationException("Saldo insuficiente para la venta");
            }

            var price = await _cripto.GetPriceArs(dto.crypto_code, dto.action);
            var money = Math.Round(dto.crypto_amount * price, 2);

            var t = new Transaction
            {
                crypto_code = dto.crypto_code,
                action = dto.action,
                crypto_amount = dto.crypto_amount,
                datetime = dto.datetime,
                client_id = dto.client_id,
                money = money
            };

            _context.Transactions.Add(t);
            await _context.SaveChangesAsync();
            return t;
        }

        public async Task<List<Transaction>> GetAllAsync()
        {
            return await _context.Transactions
            .Include(t => t.client)
            .OrderByDescending(t => t.datetime)
            .ToListAsync();
        }

        public async Task<Transaction?> GetByIdAsync(int id)
        {
            return await _context.Transactions.Include(t => t.client).FirstOrDefaultAsync(t => t.id == id);
        }

        public async Task<List<Transaction>> GetByClientAsync(int clientId)
        {
            return await _context.Transactions
            .Where(t => t.client_id == clientId)
            .OrderByDescending(t => t.datetime)
            .ToListAsync();
        }

        public async Task UpdateAsync(int id, TransactionDTO dto)
        {
            ValidateDto(dto);

            var existing = await _context.Transactions.FindAsync(id);
            if (existing == null) throw new InvalidOperationException("Transacción no encontrada");

            var client = await _context.Clients.FindAsync(dto.client_id);
            if (client == null) throw new InvalidOperationException("Cliente no encontrado");

            existing.crypto_code = dto.crypto_code;
            existing.action = dto.action;
            existing.crypto_amount = dto.crypto_amount;
            existing.money = dto.money;
            existing.datetime = dto.datetime;
            existing.client_id = dto.client_id;

            await _context.SaveChangesAsync();
        }
        public async Task<Transaction?> PatchAsync(int id, TransPatchDTO dto)
        {
            var tx = await _context.Transactions.FindAsync(id);
            if (tx == null) return null;

            if (dto.Action != null)
                tx.action = dto.Action;

            if (dto.CryptoCode != null)
                tx.crypto_code = dto.CryptoCode;

            if (dto.Money.HasValue)
                tx.money = dto.Money.Value;

            if (dto.DateTime.HasValue)
                tx.datetime = dto.DateTime.Value;

            if (dto.ClientId.HasValue)
                tx.client_id = dto.ClientId.Value;

            await _context.SaveChangesAsync();
            return tx;
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _context.Transactions.FindAsync(id);
            if (existing == null) throw new InvalidOperationException("Transacción no encontrada");

            _context.Transactions.Remove(existing);
            await _context.SaveChangesAsync();
        }

        public async Task<decimal> GetBalanceAsync(int clientId, string cryptoCode)
        {
            var trans = await _context.Transactions
            .Where(t => t.client_id == clientId && t.crypto_code == cryptoCode)
            .ToListAsync();

            var compras = trans.Where(t => t.action == "purchase").Sum(t => t.crypto_amount);
            var ventas = trans.Where(t => t.action == "sale").Sum(t => t.crypto_amount);

            return compras - ventas;
        }

        private void ValidateDto(TransactionDTO dto)
        {
            if (dto.crypto_amount <= 0) throw new ArgumentException("crypto_amount debe ser mayor a 0");
            if (!_allowedCryptos.Contains(dto.crypto_code)) throw new ArgumentException("crypto_code inválido");
            if (dto.action != "purchase" && dto.action != "sale") throw new ArgumentException("action inválida");
        }
    }
}