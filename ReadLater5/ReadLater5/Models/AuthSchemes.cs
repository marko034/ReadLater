using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ReadLater5.Models
{
    public class AuthSchemes
    {
        public const string AuthScheme = "Identity.Application" + ","
            + JwtBearerDefaults.AuthenticationScheme;
    }
}
