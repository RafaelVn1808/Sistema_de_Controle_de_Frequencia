namespace Sistema_de_Controle_de_Frequência.Model
{
    public class FrequenciaServidor
    {
        public int Id { get; set; }
        public int FrequenciaId { get; set; }
        public Frequencia Frequencia { get; set; }
        public int ServidorId { get; set; }
        public Servidor Servidor { get; set; }
    }
}
