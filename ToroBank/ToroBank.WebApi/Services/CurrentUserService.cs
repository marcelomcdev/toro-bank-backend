using ToroBank.Application.Common.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace ToroBank.WebApi.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == "sub_id")?.Value
                     ?? "anonymous";
        }

        public string UserId { get; }
    }
}
