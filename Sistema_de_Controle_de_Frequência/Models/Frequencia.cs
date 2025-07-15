namespace Sistema_de_Controle_de_Frequência.Models
{
    public class Frequencia
    {
        public int Id { get; set; }
        public string MesReferencia { get; set; } // "07/2025"
        public DateTime DataEnvio { get; set; }
        public int StatusFrequenciaId { get; set; }
        public StatusFrequencia StatusFrequencia { get; set; }
        public int SetorId { get; set; }
        public Setor Setor { get; set; }
        public ICollection<FrequenciaServidor> FrequenciasServidores { get; set; }
    }
}
