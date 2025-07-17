using Microsoft.AspNetCore.Mvc;
using Sistema_de_Controle_de_Frequência.Models;
using SistemaDeControleDeFrequencia.DTOs.Servidor;
using SistemaDeControleDeFrequencia.Services;

namespace SistemaDeControleDeFrequencia.Controllers {
    
    [ApiController]
    [Route("api/[controller]")]
    public class ServidorController : ControllerBase {

        private readonly ServidorService _service;

        public ServidorController(ServidorService service) {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get() {
            var servidores = await _service.GetAllAsync();
            return Ok(servidores);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) {
            var servidor = await _service.GetByIdAsync(id);
            return servidor == null ? NotFound() : Ok(servidor);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ServidorDTO dto) {

            Servidor servidor = new Servidor {
                Id = dto.Id,
                Nome = dto.Nome,
                Matricula = dto.Matricula,
                id_setor = dto.id_setor,
                Setor = dto.Setor,
            };

            if (servidor == null) return BadRequest("Servidor cannot be null.");
            await _service.AddAsync(servidor);
            return CreatedAtAction(nameof(Get), new { id = servidor.Id }, servidor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ServidorDTO dto) {

            Servidor servidor = new Servidor
            {
                Id = dto.Id,
                Nome = dto.Nome,
                Matricula = dto.Matricula,
                id_setor = dto.id_setor,
                Setor = dto.Setor,
            };

            if (id != servidor.Id) return BadRequest("ID mismatch.");
            if (servidor == null) return BadRequest("Servidor cannot be null.");
            await _service.UpdateAsync(servidor);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            var servidor = await _service.GetByIdAsync(id);
            if (servidor == null) return NotFound();
            await _service.DeleteAsync(id);
            return NoContent();
        }

    }
}
