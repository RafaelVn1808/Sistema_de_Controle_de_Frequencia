namespace Sistema_de_Controle_de_Frequência.Model
{
    public class Servidor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Matricula { get; set; }
        public int id_setor { get; set; }
        public Setor Setor { get; set; }
        public ICollection<FrequenciaServidor> FrequenciasServidores { get; set; }

    }
}
