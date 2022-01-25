using System.Collections.Generic;

namespace ProEventos.Domain.Dtos
{
    public class PalestranteDto : EntityDto
    {
        public string Nome { get; set; }
        public string MiniCurriculo { get; set; }
        public string ImagemURL { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public IEnumerable<RedeSocialDto> RedesSociais { get; set; }
    }
}
