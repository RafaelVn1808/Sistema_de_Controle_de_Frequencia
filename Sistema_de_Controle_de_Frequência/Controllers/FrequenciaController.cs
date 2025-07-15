using Microsoft.AspNetCore.Mvc;
using Sistema_de_Controle_de_Frequência.Models;
using Sistema_de_Controle_de_Frequência.Services;

namespace Sistema_de_Controle_de_Frequência.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FrequenciaController : ControllerBase
    {
        private readonly FrequenciaService _service;

        public FrequenciaController(FrequenciaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var frequencias = await _service.GetAllFrequenciasAsync();
            return Ok(frequencias);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var frequencia = await _service.GetFrequenciaByIdAsync(id);
            if (frequencia == null)
                return NotFound();

            return Ok(frequencia);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Frequencia frequencia)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _service.AddFrequenciaAsync(frequencia);
                return CreatedAtAction(nameof(Get), new { id = frequencia.Id }, frequencia);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Frequencia frequencia)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != frequencia.Id)
                return BadRequest();

            await _service.UpdateFrequenciaAsync(frequencia);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteFrequenciaAsync(id);
            return NoContent();
        }
    }
}
