using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

[ApiController]
public class HomeController : ControllerBase
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet("/")]
    public IActionResult Index()
    {
        _logger.LogInformation("Accediendo al método Index");
        return Ok("Home page");
    }

    [HttpGet("get")]
    public IEnumerable<string> Get()
    {
        _logger.LogInformation("Accediendo al método Get");
        return new string[] { "value1", "value2" };
    }
}