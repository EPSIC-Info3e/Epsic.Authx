using System.Threading.Tasks;
using Epsic.Authx.Models;
using Microsoft.AspNetCore.Authorization;

namespace Epsic.Authx.Authorization
{
    public class SamePatientAuthorizationHandler : AuthorizationHandler<SamePatientRequirement, TestCovid>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            SamePatientRequirement requirement,
            TestCovid resource)
        {
            var userId = context.User.FindFirst("Id");
            if (userId.Value == resource.User?.Id)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }

    public class SamePatientRequirement : IAuthorizationRequirement { }
}