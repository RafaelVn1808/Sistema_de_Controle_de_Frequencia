namespace Sistema_de_Controle_de_Frequência.Model
{
    public class Setor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int NucleoId { get; set; }
        public Nucleo Nucleo { get; set; }

        public ICollection<Servidor> Servidores { get; set; }
        public ICollection<Frequencia> Frequencias { get; set; }
    }
}
