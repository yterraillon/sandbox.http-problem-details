using Microsoft.AspNetCore.Mvc;

namespace DemoApi.Controllers;

[ApiController]
[Route("[controller]")]
public class DemoController : ControllerBase
{
    private readonly ILogger<DemoController> _logger;

    public DemoController(ILogger<DemoController> logger) => _logger = logger;

    [HttpPost]
    public ActionResult Post()
    {
        var problemDetails = new ProblemDetails
        {
            Detail = "The request parameters failed to validate.",
            Instance = null,
            Status = 400,
            Title = "Validation Error",
            Type = "https://example.net/validation-error",
        };
        
        problemDetails.Extensions.Add("invalidParams", new List<ValidationProblemDetailsParam>()
        {
            new("name", "Cannot be blank."),
            new("age", "Must be great or equals to 18.")
        });
        
        return new ObjectResult(problemDetails)
        {
            StatusCode = 400
        };
    }

    private class ValidationProblemDetailsParam
    {
        public ValidationProblemDetailsParam(string name, string reason)
        {
            Name = name;
            Reason = reason;
        }

        public string Name { get; set; }
        public string Reason { get; set; }
    }
}