using Microsoft.AspNetCore.Mvc;
using Sistema_de_Controle_de_Frequência.Models;
using Sistema_de_Controle_de_Frequência.Services;
using SistemaDeControleDeFrequencia.DTOs.Frequencia;

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
        public async Task<ActionResult<List<FrequenciaResponseDTO>>> GetAllAsync()
        {
            var frequencias = await _service.GetAllAsync();
            return Ok(frequencias);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var frequencia = await _service.GetByIdAsync(id);
            if (frequencia == null)
                return NotFound();

            return Ok(frequencia);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FrequenciaCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var frequencia = new Frequencia {
                MesReferencia = dto.MesReferencia,
                SetorId = dto.SetorId,
                
            };
            
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
        public async Task<IActionResult> Put(int id, [FromBody] FrequenciaUpdateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != dto.Id)
                return BadRequest("O ID informado na URL não corresponde ao ID do corpo da requisição.");

            var frequencia = new Frequencia {
                Id = dto.Id,
                MesReferencia = dto.MesReferencia,
                SetorId = dto.SetorId,
                
            };         

            await _service.UpdateFrequenciaAsync(frequencia);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteFrequenciaAsync(id);
            return NoContent();
        }

        [HttpGet("pdf")]
        public async Task<IActionResult> GetRelatorioPdf()
        {
            var frequencias = await _service.GetAllFrequenciasAsync();
            var pdfBytes = _service.GerarRelatorioPdf(frequencias);
            return File(pdfBytes, "application/pdf", "RelatorioFrequencias.pdf");
        }

        [HttpGet("excel")]
        public async Task<IActionResult> GetRelatorioExcel()
        {
            var frequencias = await _service.GetAllFrequenciasAsync();
            var excelBytes = _service.GerarRelatorioExcel(frequencias);
            return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "RelatorioFrequencias.xlsx");
        }



    }
}
