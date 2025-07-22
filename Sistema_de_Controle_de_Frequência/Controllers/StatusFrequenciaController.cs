using Microsoft.AspNetCore.Mvc;
using SistemaDeControleDeFrequencia.DTOs.StatusFrequencia;
using SistemaDeControleDeFrequencia.Services;
using System.Threading.Tasks;

namespace SistemaDeControleDeFrequencia.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatusFrequenciaController : ControllerBase
    {
        private readonly StatusFrequenciaService _service;

        public StatusFrequenciaController(StatusFrequenciaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var status = await _service.GetAllAsync();
            return Ok(status);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var status = await _service.GetByIdAsync(id);
            if (status == null)
                return NotFound();

            return Ok(status);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] StatusFrequenciaCreateDTO dto)
        {
            await _service.AddAsync(dto);
            return Ok("Status criado com sucesso.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] StatusFrequenciaUpdateDTO dto)
        {
            if (id != dto.Id)
                return BadRequest("Id informado na URL não confere com o do corpo da requisição.");

            await _service.UpdateAsync(dto);
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
