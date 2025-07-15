namespace Sistema_de_Controle_de_Frequência.Model
{
    public class Nucleo
    {
        public int Id { get; set; }
        public string Nome { get; set; } 
        public string Cidade { get; set; }

        public ICollection<Setor> Setores { get; set; }

    }
}
