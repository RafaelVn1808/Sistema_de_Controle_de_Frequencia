using Microsoft.AspNetCore.Mvc;
using SistemaDeControleDeFrequencia.DTOs.Setor;
using SistemaDeControleDeFrequencia.Services;
using System.Threading.Tasks;

namespace SistemaDeControleDeFrequencia.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class SetorController : ControllerBase {
        private readonly SetorService _service;

        public SetorController(SetorService service) {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get() {
            var setores = await _service.GetAllAsync();
            return Ok(setores);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) {
            var setor = await _service.GetByIdAsync(id);
            if (setor == null)
                return NotFound();

            return Ok(setor);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SetorCreateDTO dto) {
            try {
                await _service.AddAsync(dto);
                
                return CreatedAtAction(nameof(Get), new { id = dto.Nome }, dto);
                
            }
            catch (System.Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] SetorUpdateDTO dto) {
            if (id != dto.Id)
                return BadRequest("Id na URL e no corpo não coincidem.");

            try {
                await _service.UpdateAsync(dto);
                return NoContent();
            }
            catch (System.Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            var setor = await _service.GetByIdAsync(id);
            if (setor == null)
                return NotFound();

            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
