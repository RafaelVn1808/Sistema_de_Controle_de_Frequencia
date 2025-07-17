namespace SistemaDeControleDeFrequencia.DTOs.Frequencia {
    public class FrequenciaCreateDTO {

        public string MesReferencia { get; set; }
        public DateTime DataEnvio { get; set; }
        public int StatusFrequenciaId { get; set; }
        public int SetorId { get; set; }

    }
}
