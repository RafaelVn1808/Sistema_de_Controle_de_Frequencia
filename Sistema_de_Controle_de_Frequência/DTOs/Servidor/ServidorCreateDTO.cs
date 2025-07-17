using Sistema_de_Controle_de_Frequência.Models;

namespace SistemaDeControleDeFrequencia.DTOs.Servidor
{
    public class ServidorCreateDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Matricula { get; set; }
        public int id_setor { get; set; }
        
        
    }
}
