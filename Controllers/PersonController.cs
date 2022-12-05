using Microsoft.AspNetCore.Mvc;
using tp3_dotnet.Models;

namespace tp3_dotnet.Controllers;

public class PersonController : Controller
{
    private readonly IConfiguration _configuration;

    /* Getting configuration using dependency injection */
    public PersonController(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    [HttpGet]
    [Route("Person/all")]
    public IActionResult Index()
    {
        var allPersons = new Personal_info(_configuration).GetAllPerson();
        return View(allPersons);
    }
}