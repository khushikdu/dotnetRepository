using System.Security.Claims;

namespace Assessment_1.Utils
{
    public class JwtUtils
    {
        public static string? GetEmailFromClaims(ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Email)?.Value;
        }

        public static string? GetRoleFromClaims(ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Role)?.Value;
        }
    }
}
