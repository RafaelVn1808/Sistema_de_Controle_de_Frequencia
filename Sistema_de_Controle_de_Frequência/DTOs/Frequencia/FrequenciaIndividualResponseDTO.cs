namespace SistemaDeControleDeFrequencia.DTOs.Frequencia
{
    public class FrequenciaIndividualResponseDTO
    {
        public string ServidorNome { get; set; }
        public string SetorNome { get; set; }
        public string MesReferencia { get; set; }
        public string StatusNome { get; set; }
        public DateTime DataEnvio { get; set; }
    }
}
