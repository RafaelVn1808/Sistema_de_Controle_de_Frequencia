using System.ComponentModel.DataAnnotations;

namespace SistemaDeControleDeFrequencia.DTOs.Frequencia
{
    public class AlterarStatusFrequenciaDTO
    {
        [Required(ErrorMessage = "O Id da frequência é obrigatório.")]
        public int FrequenciaId { get; set; }

        [Required(ErrorMessage = "O novo status é obrigatório.")]
        public int NovoStatusId { get; set; }
    }
}
