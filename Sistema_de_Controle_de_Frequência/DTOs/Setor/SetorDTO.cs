using Sistema_de_Controle_de_Frequência.Models;
using SistemaDeControleDeFrequencia.DTOs.Frequencia;
using SistemaDeControleDeFrequencia.DTOs.Servidor;

namespace SistemaDeControleDeFrequencia.DTOs.Setor
{
    public class SetorDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int NucleoId { get; set; }
        public Nucleo Nucleo { get; set; }

        public ICollection<ServidorCreateDTO> ServidoresDTO { get; set; }
        public ICollection<FrequenciaCreateDTO> FrequenciasDTO { get; set; }
    }
}
