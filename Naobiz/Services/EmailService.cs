using FluentEmail.Core;
using Microsoft.AspNetCore.Hosting;
using Naobiz.Data;
using System.IO;
using System.Threading.Tasks;

namespace Naobiz.Services
{
    class EmailService
    {
        readonly IFluentEmail m_Email;
        readonly IWebHostEnvironment m_Environment;

        public EmailService(IFluentEmail email, IWebHostEnvironment environment)
        {
            m_Email = email;
            m_Environment = environment;
        }

        public async Task<bool> SendAsync<T>(User user, string subject, string fileName, T model)
        {
            try
            {
                var response = await m_Email.To(user.Email)
                    .Subject(subject)
                    .UsingTemplateFromFile(Path.Combine(m_Environment.WebRootPath, "emails", fileName), model)
                    .SendAsync();
                return response.Successful;
            }
            catch
            {
                return false;
            }
        }
    }
}
