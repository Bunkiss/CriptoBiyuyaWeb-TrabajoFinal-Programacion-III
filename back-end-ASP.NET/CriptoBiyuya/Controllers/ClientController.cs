using CriptoBiyuya.DTOs;
using CriptoBiyuya.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;

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

        /* -------- GET -------- */

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
                    datetime = t.datetime,
                }).ToList()
            }).ToList();

            return Ok(clientsDTO);
        }

        /* -------- POST -------- */

        [HttpPost]
        public async Task<ActionResult<Client>> Post(ClientDTO clientDTO)
        {
            var client = new Client
            {
                name = clientDTO.name,
                email = clientDTO.email
            };

            _context.Clients.Add(client);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = client.id }, new ClientDTO
            {
                id = client.id,
                name = client.name,
                email = client.email
            }); 
        }

        /* -------- GET ById -------- */

        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> Get(int id)
        {
            var client = await _context.Clients.Include(x => x.transactions).FirstOrDefaultAsync(x => x.id == id);

            if (client == null)
            {
                return NotFound();
            }

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
                }).ToList()
            };

            return Ok(clientDTO);
        }

        /* -------- PUT -------- */
        /*
        [HttpPut("{id}")]
        public async Task<ActionResult<Client>> Put(int id, ClientDTO clientDTO)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(n => n.id == id);

            if (client == null)
            {
                return NotFound();
            }

            client.name = clientDTO.name;
            client.email = clientDTO.email;

            await _context.SaveChangesAsync();

            return NoContent();
        } */

        /* -------- DELETE -------- */

        [HttpDelete("{id}")]
        public async Task<ActionResult<Client>> Delete(int id)
        {
            var client = await _context.Clients.FindAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
