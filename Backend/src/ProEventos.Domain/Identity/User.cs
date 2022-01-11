using Microsoft.AspNetCore.Identity;
using ProEventos.Domain.Enum;
using System.Collections.Generic;

namespace ProEventos.Domain.Identity
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Titulo Title { get; set; }
        public string Description { get; set; }
        public Funcao Function { get; set; }
        public string ImageUrl { get; set; }
        public IEnumerable<UserRole> UserRoles { get; set; }
    }
}
