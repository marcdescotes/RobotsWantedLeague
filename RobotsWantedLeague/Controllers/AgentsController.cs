using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RobotsWantedLeague.Models;
using RobotsWantedLeague.Services;
using System.ComponentModel.DataAnnotations;

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

    public AgentsController(ILogger<AgentsController> logger,
                            IAgentsService agentsService)
    {
        _logger = logger;
        this.agentsService = agentsService;
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

    // [HttpGet]
    // public IActionResult CreateAgent()
    // {
    //     return View();
    // }

    // [HttpPost]
    // public IActionResult CreateAgent([FromBody] AgentRequest agent)
    // {

    //     Agent r = agentsService.CreateAgent(agent.Name,
    //                                         agent.Continent);
    //     string htmxRedirectHeaderName = "HX-Redirect";
    //     string redirectURL = "/agents/agent?id=" + r.Id;
    //     Response.Headers.Add(htmxRedirectHeaderName, redirectURL);
    //     return Ok();
    // }

}
