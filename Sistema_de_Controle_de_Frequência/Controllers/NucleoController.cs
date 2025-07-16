using Microsoft.AspNetCore.Mvc;
using Sistema_de_Controle_de_Frequência.Models;
using SistemaDeControleDeFrequencia.Services;

namespace SistemaDeControleDeFrequencia.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NucleoController : ControllerBase
    {
        private readonly NucleoService _service;

        public NucleoController(NucleoService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var nucleo = await _service.GetByIdAsync(id);
            return nucleo == null ? NotFound() : Ok(nucleo);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Nucleo nucleo)
        {
            await _service.AddAsync(nucleo);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Nucleo nucleo)
        {
            if (id != nucleo.Id) return BadRequest();
            await _service.UpdateAsync(nucleo);
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
