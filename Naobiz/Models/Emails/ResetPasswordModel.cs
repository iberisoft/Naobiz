using Microsoft.AspNetCore.WebUtilities;
using Naobiz.Data;

namespace Naobiz.Models.Emails
{
    public class ResetPasswordModel
    {
        public ResetPasswordModel(User user, string siteUrl)
        {
            UserName = user.Name;
            SiteUrl = siteUrl;
            ResetUrl = SiteUrl + "reset-password";
            ResetUrl = QueryHelpers.AddQueryString(ResetUrl, "email", user.Email);
            ResetUrl = QueryHelpers.AddQueryString(ResetUrl, "code", user.PasswordResetCode);
        }

        public string UserName { get; }

        public string SiteUrl { get; }

        public string ResetUrl { get; }
    }
}
