using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Naobiz.Data;
using System.Net;
using System.Threading.Tasks;

namespace Naobiz.Services
{
    class UserService
    {
        readonly AuthenticationStateProvider m_AuthenticationStateProvider;
        HttpContext m_HttpContext;

        public UserService(AuthenticationStateProvider authenticationStateProvider, IHttpContextAccessor httpContextAccessor)
        {
            m_AuthenticationStateProvider = authenticationStateProvider;
            m_HttpContext = httpContextAccessor.HttpContext;
        }

        public async Task<User> GetCurrentAsync(AppDbContext dbContext)
        {
            var state = await m_AuthenticationStateProvider.GetAuthenticationStateAsync();
            return state.User.Identity.IsAuthenticated ? await dbContext.Users.SingleOrDefaultAsync(user => user.Email == state.User.Identity.Name && user.ActivationCode == null) : null;
        }

        public IPAddress GetRemoteIpAddress()
        {
            if (m_HttpContext != null)
            {
                var ipAddress = m_HttpContext.Connection.RemoteIpAddress;
                ipAddress = ipAddress.MapToIPv4();
                m_HttpContext = null;
                return ipAddress;
            }
            return null;
        }
    }
}
