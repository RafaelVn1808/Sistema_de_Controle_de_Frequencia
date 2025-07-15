namespace Sistema_de_Controle_de_Frequência.Models
{
    public class StatusFrequencia
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public ICollection<Frequencia> Frequencias { get; set; }
    }
}
