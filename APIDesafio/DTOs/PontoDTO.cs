using APIDesafio.DTOs;
using System.ComponentModel.DataAnnotations;

namespace ApiTeste.DTOs
{
    public class PontoDTO
    {
        [Required]
        public DataCompletaDTO InicioJornada { get; set; }
        [Required]
        public DataCompletaDTO FimJornada { get; set; }
        [Required]
        public string Id_Desenvolvedor { get; set; }
        [Required]
        public int Id_Projeto { get; set; }

    }
}
