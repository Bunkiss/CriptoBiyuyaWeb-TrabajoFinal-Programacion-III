using CriptoBiyuya.DTOs;
using CriptoBiyuya.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CriptoBiyuya.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly AppDbContext _context;
        public TransactionController(AppDbContext context)
        {
            _context = context;
        }

        /* -------- GET -------- */

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionDTO>>> Get()
        {
            var transactions = await _context.Transactions.ToListAsync();

            var transactionsDTO = transactions.Select(n => new TransactionDTO
            {
                id = n.id,
                crypto_code = n.crypto_code,
                action = n.action,
                crypto_amount = n.crypto_amount,
                money = n.money,
                datetime = n.datetime,
                client_id = n.client_id
            }).ToList();

            return Ok(transactionsDTO);
        }

        /* -------- POST -------- */

        [HttpPost]
        public async Task<ActionResult<Transaction>> Post(TransactionDTO transactionDTO)
        {
            var client = await _context.Clients.FindAsync(transactionDTO.client_id);
            if (client == null)
            {
                return NotFound("Cliente no encontrado");
            }

            var transaction = new Transaction
            {
                crypto_code = transactionDTO.crypto_code,
                action = transactionDTO.action,
                crypto_amount = transactionDTO.crypto_amount,
                money = transactionDTO.money,
                datetime = transactionDTO.datetime,
                client_id = transactionDTO.client_id,
                client = client
            };

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = transaction.id }, new TransactionDTO
            {
                id = transaction.id,
                crypto_code = transaction.crypto_code,
                action = transaction.action,
                crypto_amount = transaction.crypto_amount,
                money = transaction.money,
                datetime = transaction.datetime,
                client_id = transaction.client_id
            });
        }

        /* -------- GET ById -------- */

        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> Get(int id)
        {
            var transaction = await _context.Transactions.FirstOrDefaultAsync(x => x.id == id);

            if (transaction == null)
            {
                return NotFound();
            }

            var transactionDTO = new TransactionDTO
            {
                id = transaction.id,
                crypto_code = transaction.crypto_code,
                action = transaction.action,
                crypto_amount = transaction.crypto_amount,
                money = transaction.money,
                datetime = transaction.datetime,
                client_id = transaction.client_id
            };

            return Ok(transactionDTO);
        }

        /* -------- PUT -------- */

        [HttpPut("{id}")]
        public async Task<ActionResult<Transaction>> Put(int id, TransactionDTO transactionDTO)
        {
            var transaction = await _context.Transactions.FirstOrDefaultAsync(n => n.id == id);

            if (transaction == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.FindAsync(transactionDTO.client_id);
            if (client == null)
            {
                return BadRequest("El cliente especificado no existe.");
            }

            transaction.crypto_code = transactionDTO.crypto_code;
            transaction.action = transactionDTO.action;
            transaction.crypto_amount = transactionDTO.crypto_amount;
            transaction.money = transactionDTO.money;
            transaction.datetime = transactionDTO.datetime;
            transaction.client = client;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        /* -------- DELETE -------- */

        [HttpDelete("{id}")]
        public async Task<ActionResult<Transaction>> Delete(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);

            if (transaction == null)
            {
                return NotFound();
            }

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
