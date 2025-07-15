namespace Sistema_de_Controle_de_Frequência.Model
{
    public class StatusFrequencia
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public ICollection<Frequencia> Frequencias { get; set; }
    }
}
