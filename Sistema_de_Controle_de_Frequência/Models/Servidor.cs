namespace Sistema_de_Controle_de_Frequência.Models
{
    public class Servidor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Matricula { get; set; }
        public int SetorId { get; set; }
        public Setor Setor { get; set; }
        public ICollection<FrequenciaServidor> FrequenciasServidores { get; set; }

    }
}
