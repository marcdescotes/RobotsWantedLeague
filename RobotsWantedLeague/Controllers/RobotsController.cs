using Microsoft.AspNetCore.Mvc;
using RobotsWantedLeague.Models;
using RobotsWantedLeague.Services;
using System.Text.Json;

namespace RobotsWantedLeague.Controllers;

public class RobotRequest
{
    public string Country { get; set; }
    public string Name { get; set; }
    public int Height { get; set; }
    public int Weight { get; set; }
}

public class SearchRobotRequest
{
    public string Filter { get; set; }
}

public class ChangeRobotCountryViewModel
{
    public string NewCountry { get; set; }
    public string ErrorMessage { get; set; }
}

public class GetValidCountries
{
    public List<string> Countries { get; set; }
}

public class RobotsController : Controller
{
    private readonly ILogger<RobotsController> _logger;
    private readonly IRobotsService robotsService;
    private readonly List<string> _validCountries;

    public RobotsController(ILogger<RobotsController> logger, IRobotsService robotsService)
    {
        _logger = logger;
        this.robotsService = robotsService;

        var validCountriesJson = System.IO.File.ReadAllText("data/ValidCountries.json");

        var validCountries = JsonSerializer.Deserialize<GetValidCountries>(validCountriesJson);

        _validCountries = validCountries?.Countries ?? new List<string>();
    }

    private bool IsCountryValid(string country)
    {
        return _validCountries.Contains(char.ToUpper(country[0]) + country.Substring(1));
    }

    public IActionResult Index()
    {
        return View(robotsService.Robots);

    }
    public IActionResult Robot(int id)
    {
        Robot? robot = robotsService.GetRobotById(id);
        if (robot == null)
        {
            return NotFound();
        }
        return View(robot);
    }


    [HttpGet]
    public IActionResult CreateRobot()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CreateRobot([FromBody] RobotRequest robot)
    {
        var viewModel = new ChangeRobotCountryViewModel { NewCountry = robot.Country };

        if (!IsCountryValid(robot.Country))
        {
            ViewBag.ErrorMessage = "Le pays n'est pas valide.";
            return View("_RobotErrorMessages");
        }
        Robot r = robotsService.CreateRobot(robot.Name,
                                            robot.Weight,
                                            robot.Height,
                                            robot.Country);
        string htmxRedirectHeaderName = "HX-Redirect";
        string redirectURL = "/robots/robot?id=" + r.Id;
        Response.Headers.Add(htmxRedirectHeaderName, redirectURL);
        return Ok();
    }


    [HttpPost]
    public IActionResult ChangeRobotCountry(int robotId, string newCountry)
    {

        var viewModel = new ChangeRobotCountryViewModel { NewCountry = newCountry };

        // Vérifie si le pays est valide
        if (!IsCountryValid(newCountry))
        {
            // Lancer une erreur si le pays n'est pas valide
            ViewBag.ErrorMessage = "Le pays n'est pas valide.";
            return View("Robot", robotsService.GetRobotById(robotId)); // nom de la vue
        }

        newCountry = char.ToUpper(newCountry[0]) + newCountry.Substring(1);

        // Si le pays est valide, rouler les fonctions
        try
        {
            robotsService.ChangeRobotCountry(robotId, newCountry);
            return RedirectToAction("Robot", new { id = robotId });
        }
        catch (Exception ex)
        {
            viewModel.ErrorMessage = "Une erreur s'est produite lors de la modification du pays du robot.";

            return View("Robot", viewModel);
        }
    }

    [HttpGet]
    public IActionResult FilterRobots(string filter)
    {
        var filteredRobots = robotsService.FilterRobots(filter);
        return View("index", filteredRobots);
    }
}
