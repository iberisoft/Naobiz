using Microsoft.AspNetCore.WebUtilities;
using Naobiz.Data;

namespace Naobiz.Models.Emails
{
    public class ActivateModel
    {
        public ActivateModel(User user, string siteUrl)
        {
            UserName = user.Name;
            SiteUrl = siteUrl;
            ActivationUrl = SiteUrl + "activate";
            ActivationUrl = QueryHelpers.AddQueryString(ActivationUrl, "email", user.Email);
            ActivationUrl = QueryHelpers.AddQueryString(ActivationUrl, "code", user.ActivationCode);
        }

        public string UserName { get; }

        public string SiteUrl { get; }

        public string ActivationUrl { get; }
    }
}
