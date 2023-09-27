﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RobotsWantedLeague.Models;
using RobotsWantedLeague.Services;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using System.Text.Json;

namespace RobotsWantedLeague.Controllers;

public class RobotRequest
{
    public string Country { get; set; }
    public string Name { get; set; }
    public int Height { get; set; }
    public int Weight { get; set; }
    public string AgentName { get; set; }
}

public class SearchRobotRequest
{
    public string Filter { get; set; }
}

public class RobotsController : Controller
{
    private readonly ILogger<RobotsController> _logger;
    private readonly IRobotsService robotsService;
    private readonly IAgentsService agentsService;

    public RobotsController(ILogger<RobotsController> logger, IRobotsService robotsService, IAgentsService agentsService)
    {
        _logger = logger;
        this.robotsService = robotsService;
        this.agentsService = agentsService;
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
        if (!ModelState.IsValid)
        {
            return View(robot);
        }

        Agent assignedAgent = agentsService.Agents.FirstOrDefault(agent => agent.Name == robot.AgentName);

        if (assignedAgent != null)
        {
            Robot r = robotsService.CreateRobot(robot.Name, robot.Weight, robot.Height, robot.Country, assignedAgent);

            if (r.AssignedAgent != null)
            {
                r.AssignedAgent.FormerAssignedRobots.Add(r);
            }

            assignedAgent.AssignedRobots.Add(r);

            string htmxRedirectHeaderName = "HX-Redirect";
            string redirectURL = "/robots/robot?id=" + r.Id;
            Response.Headers.Add(htmxRedirectHeaderName, redirectURL);

            return Ok();
        }
        else
        {
            return BadRequest("Agent non trouvé.");
        }
    }


    [HttpPost]
    public IActionResult ChangeRobotCountry(int robotId, string newCountry)
    {
        robotsService.ChangeRobotCountry(robotId, newCountry);
        return RedirectToAction("Robot", new { id = robotId });
    }

    [HttpGet]
    public IActionResult FilterRobots(string filter)
    {
        var filteredRobots = robotsService.FilterRobots(filter);
        return View("index", filteredRobots);
    }
    [HttpGet]
    public IActionResult ChangeRobotAgent(int robotId)
    {
        Robot robot = robotsService.GetRobotById(robotId);
        if (robot == null)
        {
            return NotFound();
        }

        List<Agent> agents = agentsService.Agents.ToList();

        ViewData["Agents"] = agents;
        ViewData["Robot"] = robot;

        return View();
    }


    [HttpPost]
    public IActionResult UpdateRobotAgent(int robotId, int agentId)
    {
        Robot robot = robotsService.GetRobotById(robotId);
        if (robot == null)
        {
            return NotFound();
        }

        Agent newAgent = agentsService.Agents.FirstOrDefault(agent => agent.Id == agentId);
        if (newAgent != null)
        {
            robotsService.ChangeRobotAgent(robotId, newAgent);

            return RedirectToAction("Robot", new { id = robotId });
        }
        else
        {
            return BadRequest("Agent non trouvé.");
        }
    }
}