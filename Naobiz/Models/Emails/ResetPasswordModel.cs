using Microsoft.AspNetCore.WebUtilities;
using Naobiz.Data;

namespace Naobiz.Models.Emails
{
    public class ResetPasswordModel : BaseModel
    {
        public string ResetUrl
        {
            get
            {
                var url = SiteUrl + "reset-password";
                url = QueryHelpers.AddQueryString(url, "email", User.Email);
                url = QueryHelpers.AddQueryString(url, "code", User.PasswordResetCode);
                return url;
            }
        }
    }
}
