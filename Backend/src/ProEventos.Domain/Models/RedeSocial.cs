namespace ProEventos.Domain
{
    public class RedeSocial : Entity
    {
        public string Nome { get; set; }
        public string URL { get; set; }
        public int PalestranteId { get; set; }
        public Palestrante Palestrante { get; set; }
    }
}
