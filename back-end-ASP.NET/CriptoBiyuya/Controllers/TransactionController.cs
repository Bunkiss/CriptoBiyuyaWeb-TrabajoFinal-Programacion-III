using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CriptoBiyuya.Services;
using CriptoBiyuya.DTOs;
using CriptoBiyuya.Models;
using System;
using System.Collections.Generic;


namespace CriptoBiyuya.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _service;

        public TransactionController(ITransactionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> Get()
        {
            var trans = await _service.GetAllAsync();
            return Ok(trans);
        }

        [HttpGet("by-client/{clientId}")]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetByClient(int clientId)
        {
            var trans = await _service.GetByClientAsync(clientId);
            return Ok(trans);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> Get(int id)
        {
            var trans = await _service.GetByIdAsync(id);
            if (trans == null) return NotFound();
            return Ok(trans);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TransactionDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);


            try
            {
                var created = await _service.CreateAsync(dto);
                var responseDto = new TransactionDTO
                {
                    id = created.id,
                    crypto_code = created.crypto_code,
                    action = created.action,
                    crypto_amount = created.crypto_amount,
                    money = created.money,
                    datetime = created.datetime,
                    client_id = created.client_id
                };


                return CreatedAtAction(nameof(Get), new { id = created.id }, responseDto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] TransactionDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                await _service.UpdateAsync(id, dto);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}