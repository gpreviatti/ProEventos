using ProEventos.Domain.Enum;

namespace ProEventos.Domain.Dtos
{
    public class UserLoginDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
