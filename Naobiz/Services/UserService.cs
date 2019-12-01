using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Naobiz.Data;
using System.Threading.Tasks;

namespace Naobiz.Services
{
    class UserService
    {
        readonly AuthenticationStateProvider m_AuthenticationStateProvider;

        public UserService(AuthenticationStateProvider authenticationStateProvider)
        {
            m_AuthenticationStateProvider = authenticationStateProvider;
        }

        public async Task<User> GetCurrentAsync(AppDbContext dbContext)
        {
            var state = await m_AuthenticationStateProvider.GetAuthenticationStateAsync();
            return state.User.Identity.IsAuthenticated ? await dbContext.Users.SingleOrDefaultAsync(user => user.Email == state.User.Identity.Name && user.ActivationCode == null) : null;
        }
    }
}
