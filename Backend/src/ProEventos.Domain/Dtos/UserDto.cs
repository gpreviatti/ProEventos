using ProEventos.Domain.Enum;

namespace ProEventos.Domain.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public Titulo Title { get; set; }
        public string Description { get; set; }
        public Funcao Function { get; set; }
        public string ImageUrl { get; set; }
    }
}
