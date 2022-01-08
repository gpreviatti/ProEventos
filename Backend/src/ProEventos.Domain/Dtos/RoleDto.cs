using System.Collections.Generic;

namespace ProEventos.Domain.Dtos
{
    public class RoleDto
    {
        public IEnumerable<UserRoleDto> UserRoles { get; set; }
    }
}
