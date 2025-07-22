using System.ComponentModel.DataAnnotations;

namespace SistemaDeControleDeFrequencia.DTOs.Frequencia
{
    public class FrequenciaUpdateDTO
    {
        [Required(ErrorMessage = "O campo 'Id' é obrigatório.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo 'Mês de Referência' é obrigatório.")]
        [RegularExpression(@"^(0[1-9]|1[0-2])\/\d{4}$", ErrorMessage = "O campo 'Mês de Referência' deve estar no formato MM/YYYY.")]
        public string MesReferencia { get; set; }

        [Required(ErrorMessage = "O campo 'Data de Envio' é obrigatório.")]
        public DateTime DataEnvio { get; set; }

        [Required(ErrorMessage = "O campo 'Setor' é obrigatório.")]
        public int SetorId { get; set; }

        [Required(ErrorMessage = "O campo 'Status da Frequência' é obrigatório.")]
        public int StatusFrequenciaId { get; set; }
    }
}
