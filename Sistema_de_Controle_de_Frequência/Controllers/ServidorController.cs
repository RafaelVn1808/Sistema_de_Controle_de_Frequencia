using Microsoft.AspNetCore.Mvc;
using SistemaDeControleDeFrequencia.DTOs.Servidor;
using SistemaDeControleDeFrequencia.Services;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Post([FromBody] ServidorCreateDTO dto) {
            await _service.AddAsync(dto);
            return Ok("Servidor criado com sucesso.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ServidorUpdateDTO dto) {
            if (id != dto.Id)
                return BadRequest("ID da URL difere do ID do corpo da requisição.");

            await _service.UpdateAsync(dto);
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