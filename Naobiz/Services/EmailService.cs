using FluentEmail.Core;
using Microsoft.AspNetCore.Hosting;
using Naobiz.Data;
using Naobiz.Models.Emails;
using Serilog;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Naobiz.Services
{
    class EmailService
    {
        readonly IFluentEmail m_Email;
        readonly IWebHostEnvironment m_Environment;
        readonly Settings m_Settings;

        public EmailService(IFluentEmail email, IWebHostEnvironment environment, Settings settings)
        {
            m_Email = email;
            m_Environment = environment;
            m_Settings = settings;
        }

        public async Task<bool> SendAsync<T>(User user, string subject, string fileName, T model)
            where T : BaseModel
        {
            model.User = user;
            model.SupportUrl = m_Settings.SupportUrl;
            model.SiteUrl = m_Settings.SiteUrl;
            model.SiteEmail = m_Settings.SiteEmail;
            try
            {
                var response = await m_Email.To(user.Email)
                    .Subject(subject)
                    .UsingTemplateFromFile(Path.Combine(m_Environment.WebRootPath, "emails", fileName), model)
                    .SendAsync();
                return response.Successful;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Exception occurred when sending email to {Email}", user.Email);
                return false;
            }
        }
    }
}
