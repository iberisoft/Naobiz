using Microsoft.AspNetCore.WebUtilities;
using Naobiz.Data;

namespace Naobiz.Models.Emails
{
    public class ActivateModel : BaseModel
    {
        public string ActivationUrl
        {
            get
            {
                var url = SiteUrl + "activate";
                url = QueryHelpers.AddQueryString(url, "email", User.Email);
                url = QueryHelpers.AddQueryString(url, "code", User.ActivationCode);
                return url;
            }
        }
    }
}
