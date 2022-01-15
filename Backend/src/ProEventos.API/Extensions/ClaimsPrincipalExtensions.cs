namespace System.Security.Claims
{
    public static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// Retorna o nome do usuário autenticado
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static string GetUserName(this ClaimsPrincipal user) => user.FindFirst(ClaimTypes.Name)?.Value;

        /// <summary>
        /// Retorna o nome do usuário autenticado
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static int GetUserId(this ClaimsPrincipal user) 
        {
            var id = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.Parse(id);
        }
    }
}
