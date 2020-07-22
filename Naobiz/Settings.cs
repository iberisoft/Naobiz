using FluentEmail.Mailgun;
using Naobiz.Data;
using System.Collections.Generic;
using System.Linq;

namespace Naobiz
{
    public class Settings
    {
        public string DbConnection { get; set; }

        public string DependeeServiceName { get; set; }

        public string SupportUrl { get; set; }

        public string SiteUrl { get; set; }

        public string SiteEmail { get; set; }

        public decimal PremiumPrice { get; set; }

        public decimal TaxRate { get; set; }

        public SmtpSettings Smtp { get; set; }

        public MailgunSettings Mailgun { get; set; }

        public PaypalSettings Paypal { get; set; }

        public DashboardItemSettings[] Dashboard { get; set; }

        public IEnumerable<DashboardItemSettings> GetDashboard(User user) => user != null ?
            user.Paid || user.Admin ? Dashboard : Dashboard.Where(item => item.PaidOnly != true) :
            Dashboard.Where(item => item.PaidOnly == null);
    }

    public class SmtpSettings
    {
        public string Host { get; set; }

        public int Port { get; set; }

        public string User { get; set; }

        public string Password { get; set; }
    }

    public class MailgunSettings
    {
        public string DomainName { get; set; }

        public string ApiKey { get; set; }

        public MailGunRegion Region { get; set; }
    }

    public class PaypalSettings
    {
        public string ClientId { get; set; }

        public string ClientSecret { get; set; }
    }

    public class DashboardItemSettings
    {
        public string Name { get; set; }

        public string LinkUrl { get; set; }

        public string ImageSource { get; set; }

        public bool? PaidOnly { get; set; }
    }
}
