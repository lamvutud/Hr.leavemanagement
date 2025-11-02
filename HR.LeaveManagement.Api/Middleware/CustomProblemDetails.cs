using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.Api.Middleware
{
    public class CustomProblemDetails: ProblemDetails
    {
        public List<string> Errors { get; set; }
    }
}
