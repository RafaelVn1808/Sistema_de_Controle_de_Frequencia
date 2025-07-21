using Microsoft.AspNetCore.Mvc;
using SistemaDeControleDeFrequencia.DTOs.Nucleo;
using SistemaDeControleDeFrequencia.Services;
using System.Threading.Tasks;

namespace SistemaDeControleDeFrequencia.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class NucleoController : ControllerBase {
        private readonly NucleoService _service;

        public NucleoController(NucleoService service) {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get() {
            var nucleos = await _service.GetAllAsync();
            return Ok(nucleos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) {
            var nucleo = await _service.GetByIdAsync(id);
            return nucleo == null ? NotFound() : Ok(nucleo);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NucleoCreateDTO dto) {
            await _service.AddAsync(dto);
            return Ok("Núcleo criado com sucesso.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] NucleoUpdateDTO dto) {
            if (id != dto.Id)
                return BadRequest("ID da URL difere do ID do corpo da requisição.");

            await _service.UpdateAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}