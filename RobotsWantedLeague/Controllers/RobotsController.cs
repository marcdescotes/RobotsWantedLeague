using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RobotsWantedLeague.Models;
using RobotsWantedLeague.Services;
using System.ComponentModel.DataAnnotations;

namespace RobotsWantedLeague.Controllers;

public class RobotRequest
{
    // [AllowedCountry(ErrorMessage = "Le pays n'est pas autorisé.")]
    // public string NewCountry { get; set; }
    public string Country { get; set; }
    public string Name { get; set; }
    public int Height { get; set; }
    public int Weight { get; set; }

//     public static readonly List<string> AllowedCountries = new List<string>
// {
//     "Canada",
//     "United States of America",
//     "Mexico",
    
// };

}

// public class AllowedCountryAttribute : ValidationAttribute
// {
//     protected override ValidationResult IsValid(object value, ValidationContext validationContext)
//     {
//         string country = value as string;
//         if (string.IsNullOrEmpty(country))
//         {
//             return ValidationResult.Success;
//         }

//         if (!CountryConstants.AllowedCountries.Contains(country, StringComparer.OrdinalIgnoreCase))
//         {
//             return new ValidationResult(ErrorMessage);
//         }

//         return ValidationResult.Success;
//     }
// }


public class RobotsController : Controller
{
    private readonly ILogger<RobotsController> _logger;
    private readonly IRobotsService robotsService;

    public RobotsController(ILogger<RobotsController> logger,
                            IRobotsService robotsService)
    {
        _logger = logger;
        this.robotsService = robotsService;
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
        robotsService.ChangeRobotCountry(robotId, newCountry);
        return RedirectToAction("Robot", new { id = robotId });
    }

}
