﻿using Microsoft.AspNetCore.Mvc;
using RobotsWantedLeague.Models;
using RobotsWantedLeague.Services;
using System.ComponentModel.DataAnnotations;

using System.Text.Json;

namespace RobotsWantedLeague.Controllers;

public class RobotRequest
{
    public string Country { get; set; }
    public string Continent { get; set; }
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


public class RobotsController : Controller
{
    private readonly ILogger<RobotsController> _logger;
    private readonly IRobotsService robotsService;

    public RobotsController(ILogger<RobotsController> logger, IRobotsService robotsService)
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
        if (string.IsNullOrWhiteSpace(robot.Country))
        {
            ViewBag.ErrorMessage = "Veuillez inscrire un pays.";
            return View("_RobotErrorMessages");
        }
        if (!ModelState.IsValid)
        {
            return View(robot);
        }
        // AUCUN CHANGEMENT SANS CETTE LIGNE : _ = new ChangeRobotCountryViewModel { NewCountry = robot.Country };
        bool IsCountryValid = robotsService.IsCountryValid(robot.Country);
        if (!IsCountryValid)
        {
            ViewBag.ErrorMessage = "Le pays n'est pas valide.";
            return View("_RobotErrorMessages");
        }
        Robot r = robotsService.CreateRobot(
            robot.Name,
            robot.Weight,
            robot.Height,
            robot.Country,
            robot.Continent
        );
        string htmxRedirectHeaderName = "HX-Redirect";
        string redirectURL = "/robots/robot?id=" + r.Id;
        Response.Headers.Add(htmxRedirectHeaderName, redirectURL);
        return Ok();
    }


    [HttpPost]
    public IActionResult ChangeRobotCountry(int robotId, string newCountry)
    {
        if (string.IsNullOrWhiteSpace(newCountry))
        {
            ViewBag.ErrorMessage = "Veuillez inscrire un pays.";
            return View("Robot", robotsService.GetRobotById(robotId));
        }

        var viewModel = new ChangeRobotCountryViewModel { NewCountry = newCountry };

        bool IsCountryValid = robotsService.IsCountryValid(newCountry);
        if (!IsCountryValid)
        {
            ViewBag.ErrorMessage = "Le pays n'est pas valide.";
            return View("Robot", robotsService.GetRobotById(robotId));
        }
        newCountry = char.ToUpper(newCountry[0]) + newCountry.Substring(1);
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

    [HttpPost]
    public IActionResult ChangeRobotContinent(int robotId, string newContinent)
    {
        robotsService.ChangeRobotContinent(robotId, newContinent);

        return RedirectToAction("Robot", new { id = robotId });
    }

}
