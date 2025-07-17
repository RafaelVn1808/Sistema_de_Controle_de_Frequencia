using Sistema_de_Controle_de_Frequência.Models;

namespace SistemaDeControleDeFrequencia.DTOs.Frequencia
{
    public class FrequenciaResponseDTO
    {
        public int Id { get; set; }
        public string MesReferencia { get; set; }
        public DateTime DataEnvio { get; set; }
        public string StatusFrequenciaNome { get; set; }
        public string SetorNome { get; set; }

    }
}
