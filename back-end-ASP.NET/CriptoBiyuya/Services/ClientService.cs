using CriptoBiyuya.DTOs;
using CriptoBiyuya.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CriptoBiyuya.Services
{
    public class ClientService : IClientService
    {
        private readonly AppDbContext _context;

        public ClientService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Client> CreateAsync(ClientDTO dto)
        {
            var client = new Client
            {
                name = dto.name,
                email = dto.email
            };

            _context.Clients.Add(client);
            await _context.SaveChangesAsync();

            return client;
        }

        public async Task<List<Client>> GetAllAsync()
        {
            return await _context.Clients
                .Include(c => c.transactions)
                .ToListAsync();
        }

        public async Task<Client?> GetByIdAsync(int id)
        {
            return await _context.Clients
                .Include(c => c.transactions)
                .FirstOrDefaultAsync(c => c.id == id);
        }

        public async Task UpdateAsync(int id, ClientDTO dto)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null) return;

            client.name = dto.name;
            client.email = dto.email;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null) return;

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
        }
    }
}
