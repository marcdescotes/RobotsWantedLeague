using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RobotsWantedLeague.Models;
using RobotsWantedLeague.Services;
using System.ComponentModel.DataAnnotations;

namespace RobotsWantedLeague.Controllers;


public class RobotRequest
{
    public string Country { get; set; }
    public string Name { get; set; }
    public int Height { get; set; }
    public int Weight { get; set; }

}

public class ChangeRobotCountryViewModel
{
    public string NewCountry { get; set; }
    public string ErrorMessage { get; set; }
}


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

    // [HttpPost]
    // public IActionResult ChangeRobotCountry(int robotId, string newCountry)
    // {
    //     robotsService.ChangeRobotCountry(robotId, newCountry);
    //     return RedirectToAction("Robot", new { id = robotId });
    // }



[HttpPost]
public IActionResult ChangeRobotCountry(int robotId, string newCountry)
{
    // Liste des pays valides
    string[] validCountries = { "Canada", "USA", "Mexico", "Brésil" };

    // Créez une instance de ChangeRobotCountryViewModel
    var viewModel = new ChangeRobotCountryViewModel { NewCountry = newCountry };

    // Vérifie si le pays soumis est valide
    if (!validCountries.Contains(newCountry))
    {
        // Si le pays n'est pas valide, ajoutez un message d'erreur
        viewModel.ErrorMessage = "Le pays n'est pas valide. Les pays valides sont : Canada, USA, Mexico, Brésil";
        // Retournez la vue avec le modèle
        return View("Robot", viewModel); // Remplacez "VotreVue" par le nom de votre vue
    }

    // Si le pays est valide, effectuez les actions nécessaires ici
    try
    {
        robotsService.ChangeRobotCountry(robotId, newCountry);
        return RedirectToAction("Robot", new { id = robotId });
    }
    catch (Exception ex)
    {
        // Gérez les erreurs éventuelles ici
        viewModel.ErrorMessage = "Une erreur s'est produite lors de la modification du pays du robot.";
        // Retournez la vue avec le modèle
        return View("Robot", viewModel); // Remplacez "VotreVue" par le nom de votre vue
    }
}







}
