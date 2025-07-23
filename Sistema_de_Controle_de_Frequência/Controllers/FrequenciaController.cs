using Microsoft.AspNetCore.Mvc;
using Sistema_de_Controle_de_Frequência.Services;
using SistemaDeControleDeFrequencia.DTOs.Frequencia;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Create([FromBody] FrequenciaCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _service.AddAsync(dto);
                return Ok("Frequência criada com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] FrequenciaUpdateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != dto.Id)
                return BadRequest("O ID informado na URL não corresponde ao ID do corpo da requisição.");

            await _service.UpdateFrequenciaAsync(new Models.Frequencia
            {
                Id = dto.Id,
                MesReferencia = dto.MesReferencia,
                SetorId = dto.SetorId,
                StatusFrequenciaId = dto.StatusFrequenciaId,
                DataEnvio = dto.DataEnvio
            });

            return NoContent();
        }

        [HttpPut("status")]
        public async Task<IActionResult> UpdateStatus([FromBody] FrequenciaUpdateStatusDTO dto)
        {
            await _service.UpdateStatusAsync(dto);
            return Ok("Status da frequência atualizado com sucesso.");
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

        [HttpPut("alterar-status")]
        public async Task<IActionResult> AlterarStatus([FromBody] AlterarStatusFrequenciaDTO dto)
        {
            try
            {
                await _service.AlterarStatusAsync(dto.FrequenciaId, dto.NovoStatusId);
                return Ok("Status alterado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("relatorio-individual")]
        public async Task<IActionResult> GetRelatorioIndividual([FromBody] FrequenciaIndividualFiltroDTO dto)
        {
            var result = await _service.GetIndividualAsync(dto);
            return Ok(result);
        }

        [HttpGet("paginado")]
        public async Task<IActionResult> GetPaginado([FromQuery] FrequenciaFiltroPaginadoDTO filtro)
        {
            var resultado = await _service.GetPaginadoAsync(filtro);
            return Ok(resultado);
        }


    }
}
