using System.ComponentModel.DataAnnotations;

namespace Sistema_de_Controle_de_Frequência.Models
{
    public class Frequencia
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo 'Mês de Referência' é obrigatório.")]
        [RegularExpression(@"^(0[1-9]|1[0-2])\/\d{4}$", ErrorMessage = "O campo 'Mês de Referência' deve estar no formato MM/YYYY.")]
        public string MesReferencia { get; set; } // "07/2025"

        [Required(ErrorMessage = "O campo 'Data de Envio' é obrigatório.")]
        [DataType(DataType.Date)]
        public DateTime DataEnvio { get; set; }

        [Required(ErrorMessage = "O campo 'Status da Frequência' é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "Selecione um 'Status da Frequência' válido.")]
        public int StatusFrequenciaId { get; set; }
        public StatusFrequencia StatusFrequencia { get; set; }

        [Required(ErrorMessage = "O campo 'Setor' é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "Selecione um 'Setor' válido.")]
        public int SetorId { get; set; }


        public Setor Setor { get; set; }
        public ICollection<FrequenciaServidor> FrequenciasServidores { get; set; }
    }
}
