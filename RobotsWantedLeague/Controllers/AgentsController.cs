using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RobotsWantedLeague.Models;
using RobotsWantedLeague.Services;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace RobotsWantedLeague.Controllers;

public class AgentRequest
{
    public string Name { get; set; }
    public string Continent { get; set; }
}

public class AgentsController : Controller
{
    private readonly ILogger<AgentsController> _logger;
    private readonly IAgentsService agentsService;
    private readonly IRobotsService robotsService;

    public AgentsController(ILogger<AgentsController> logger,
                            IAgentsService agentsService,
                            IRobotsService robotsService)
    {
        _logger = logger;
        this.agentsService = agentsService;
        this.robotsService = robotsService;
    }

    public IActionResult Index()
    {
        return View(agentsService.Agents);
    }

    public IActionResult Agent(int id)
    {
        Agent? agent = agentsService.GetAgentById(id);
        if (agent == null)
        {
            return NotFound();
        }
        return View(agent);
    }

    [HttpGet]
    public IActionResult CreateAgent()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CreateAgent([FromBody] AgentRequest agentRequest)
    {
        if (!ModelState.IsValid)
        {
            return View(agentRequest);
        }

        string robotName = HttpContext.Request.Form["RobotName"];

        Robot assignedRobot = robotsService.Robots.FirstOrDefault(robot => robot.Name == robotName);

        Agent agent = agentsService.CreateAgent(agentRequest.Name, agentRequest.Continent);

        if (assignedRobot != null)
        {
            assignedRobot.AssignedAgent = agent;
            agent.AssignedRobots.Add(assignedRobot);
        }

        return RedirectToAction("Agent", new { id = agent.Id });
    }
}
