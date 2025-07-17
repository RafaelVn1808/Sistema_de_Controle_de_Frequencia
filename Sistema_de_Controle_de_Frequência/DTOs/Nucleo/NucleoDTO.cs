using SistemaDeControleDeFrequencia.DTOs.Setor;

namespace SistemaDeControleDeFrequencia.DTOs.Nucleo
{
    public class NucleoDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }    
        public string Cidade { get; set; }
        public ICollection<SetorDTO> Setores { get; set; }
    }
}
