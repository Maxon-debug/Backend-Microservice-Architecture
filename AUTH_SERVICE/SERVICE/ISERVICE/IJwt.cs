using AUTH_SERVICE.Models;

namespace AUTH_SERVICE.SERVICE.ISERVICE
{
    public interface IJwt
    {
        string GenerateToken(SystemUser user, IEnumerable<string> Roles);
    }
}
