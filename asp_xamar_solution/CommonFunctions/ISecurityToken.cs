using Microsoft.Extensions.Configuration;

namespace asp_xamar_solution.CommonFunctions
{
    public interface ISecurityToken
    {
        string CreateToken(IConfiguration config);
    }
}
