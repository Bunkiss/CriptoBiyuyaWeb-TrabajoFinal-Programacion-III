using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CriptoBiyuya.DTOs;
using CriptoBiyuya.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CriptoBiyuya.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClientController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientDTO>>> Get()
        {
            var clients = await _context.Clients.Include(x => x.transactions).ToListAsync();
            var clientsDTO = clients.Select(n => new ClientDTO
            {
                id = n.id,
                email = n.email,
                name = n.name,
                transactions = n.transactions?.Select(t => new TransactionDTO
                {
                    id = t.id,
                    crypto_code = t.crypto_code,
                    action = t.action,
                    crypto_amount = t.crypto_amount,
                    money = t.money,
                    datetime = t.datetime
                }).ToList() ?? new List<TransactionDTO>()
            }).ToList();

            return Ok(clientsDTO);
        }

        [HttpPost]
        public async Task<ActionResult<ClientDTO>> Post([FromBody] ClientDTO clientDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var client = new Client
            {
                name = clientDTO.name,
                email = clientDTO.email
            };

            _context.Clients.Add(client);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = client.id }, new ClientDTO
            {
                id = client.id,
                name = client.name,
                email = client.email
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClientDTO>> GetById(int id)
        {
            var client = await _context.Clients.Include(x => x.transactions).FirstOrDefaultAsync(x => x.id == id);

            if (client == null) return NotFound();

            var clientDTO = new ClientDTO
            {
                id = client.id,
                name = client.name,
                email = client.email,
                transactions = client.transactions?.Select(t => new TransactionDTO
                {
                    id = t.id,
                    crypto_code = t.crypto_code,
                    action = t.action,
                    crypto_amount = t.crypto_amount,
                    money = t.money,
                    datetime = t.datetime
                }).ToList() ?? new List<TransactionDTO>()
            };

            return Ok(clientDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null) return NotFound();

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
