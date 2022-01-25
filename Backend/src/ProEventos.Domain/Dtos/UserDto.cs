using ProEventos.Domain.Enum;

namespace ProEventos.Domain.Dtos
{
    public class UserDto : EntityDto
    {   
        public string UserName { get; set; }
        public string Email { get; set; }

        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Titulo Title { get; set; }
        public string Description { get; set; }
        public Funcao Function { get; set; }
        public string ImageUrl { get; set; }

    }
}
