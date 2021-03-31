using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Epsic.Authx.Authorization
{
    public class HospitalEmployeeAuthorizationHandler : AuthorizationHandler<HospitalEmployeeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HospitalEmployeeRequirement requirement)
        {
            var userEmail = context.User.FindFirst(ClaimTypes.Email);

            if (userEmail.Value?.Contains(requirement.HospitalDomainName) == true)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }

    public class HospitalEmployeeRequirement : IAuthorizationRequirement 
    { 
        public string HospitalDomainName { get; set; }

        public HospitalEmployeeRequirement(string domain)
        {
            HospitalDomainName = domain;
        }
    }
}