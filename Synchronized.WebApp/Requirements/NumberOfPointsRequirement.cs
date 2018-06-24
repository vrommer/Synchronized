using Microsoft.AspNetCore.Authorization;

namespace Synchronized.WebApp.Requirements

{    
    public class NumberOfPointsRequirement : IAuthorizationRequirement
    {
        public int MinimumPoints { get; private set; }

        public NumberOfPointsRequirement(int minimumPoints)
        {
            MinimumPoints = minimumPoints;
        }
    }


}
