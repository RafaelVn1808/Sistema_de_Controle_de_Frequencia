using Microsoft.AspNetCore.Mvc;
using Sistema_de_Controle_de_Frequência.Models;
using SistemaDeControleDeFrequencia.Services;

namespace SistemaDeControleDeFrequencia.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class SetorController : ControllerBase
    {
        private readonly SetorService _service;

        public SetorController(SetorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var setores = await _service.GetAllAsync();
            return Ok(setores);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var setor = await _service.GetByIdAsync(id);
            if (setor == null)
                return NotFound();

            return Ok(setor);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Setor setor)
        {
            await _service.AddAsync(setor);
            return CreatedAtAction(nameof(Get), new { id = setor.Id }, setor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Setor setor)
        {
            if (id != setor.Id)
                return BadRequest();

            await _service.UpdateAsync(setor);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
