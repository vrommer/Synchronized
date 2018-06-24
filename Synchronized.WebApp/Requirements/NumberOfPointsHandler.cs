using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Synchronized.Domain;
using Synchronized.WebApp.Requirements;
using System.Threading.Tasks;

public class NumberOfPointsHandler : AuthorizationHandler<NumberOfPointsRequirement>
{
    private UserManager<ApplicationUser> _userManager;
    private ILogger<NumberOfPointsHandler> _logger;

    public NumberOfPointsHandler(UserManager<ApplicationUser> userManager, ILogger<NumberOfPointsHandler> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                   NumberOfPointsRequirement requirement)
    {
        _logger.LogInformation("Entering NumberOfPointsHandler");
        var user = _userManager.GetUserAsync(context.User).Result;
        _logger.LogDebug($"Number of points: {user.Points}");
        if (user.Points >= requirement.MinimumPoints)
        {
            context.Succeed(requirement);
        }
        _logger.LogInformation("Leaving NumberOfPointsHandler");
        return Task.CompletedTask;
    }
}