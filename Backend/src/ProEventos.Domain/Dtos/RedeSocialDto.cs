namespace ProEventos.Domain.Dtos
{
    public class RedeSocialDto : EntityDto
    {
        public string Nome { get; set; }
        public string URL { get; set; }
        public int? PalestranteId { get; set; }
    }
}
