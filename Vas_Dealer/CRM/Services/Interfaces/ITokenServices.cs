using VAS.Dealer.Models.CRM;
using VAS.Dealer.Models.Entities;
using System.Security.Claims;

namespace VAS.Dealer.Services.Interfaces
{
    public interface ITokenServices
    {
        bool VerifyAccessToken(string token);
        UserTokenDTO GetUserToken(MP_Account user);
        UserTokenDTO RefreshUserToken(string oldToken);
        ClaimsPrincipal GetClaimsPrincipalByToken(string token);
        UserTokenDTO Logon(MP_Account user);
    }
}
