﻿using Naobiz.Data;
using System.Collections.Generic;
using System.Linq;

namespace Naobiz
{
    class Settings
    {
        public string DbConnection { get; set; }

        public string SupportUrl { get; set; }

        public string SiteUrl { get; set; }

        public string SiteEmail { get; set; }

        public SmtpSettings Smtp { get; set; }

        public DashboardItemSettings[] Dashboard { get; set; }

        public IEnumerable<DashboardItemSettings> GetDashboard(User user) => user != null ?
            user.Paid || user.Admin ? Dashboard : Dashboard.Where(item => item.PaidOnly != true) :
            Dashboard.Where(item => item.PaidOnly == null);
    }

    class SmtpSettings
    {
        public string Host { get; set; }

        public int Port { get; set; }

        public string User { get; set; }

        public string Password { get; set; }
    }

    class DashboardItemSettings
    {
        public string Name { get; set; }

        public string LinkUrl { get; set; }

        public string ImageSource { get; set; }

        public bool? PaidOnly { get; set; }
    }
}
