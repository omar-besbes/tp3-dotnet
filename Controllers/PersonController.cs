using Microsoft.AspNetCore.Mvc;
using tp3_dotnet.Models;

namespace tp3_dotnet.Controllers;

[Route("Person")]
public class PersonController : Controller
{
    private readonly IConfiguration _configuration;

    /* Getting configuration using dependency injection */
    public PersonController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet]
    [Route("all")]
    public IActionResult Index()
    {
        var allPersons = new Personal_info(_configuration).GetAllPerson();
        return View(allPersons);
    }

    [HttpGet]
    [Route("{id:int}")]
    public IActionResult ById(int id)
    {
        var person = new Personal_info(_configuration).GetPerson(id);
        if (person == null)
            return NotFound(new NotFoundObjectResult("The person with given ID was not found."));
        return View(person);
    }

    [HttpGet]
    [Route("search")]
    public IActionResult Search()
    {
        return View();
    }

    [HttpPost]
    [Route("search")]
    public IActionResult Search(string firstName, string country)
    {
        if (ModelState.IsValid)
        {
            var result = new Personal_info(_configuration).GetPersons(firstName, null, null, country);
            if (result.Count == 0) return View(new Tuple<Person, bool, bool>(null, true, false));
            var id = result[0].Id;
            return RedirectToAction("ById", new { id = id });
        }

        return View(new Tuple<Person, bool, bool>(null, false, true));
    }
}