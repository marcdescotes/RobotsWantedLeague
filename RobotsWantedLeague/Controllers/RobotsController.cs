namespace RobotsWantedLeague.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RobotsWantedLeague.Models;
    using RobotsWantedLeague.Services;
    using System;
    using System.Linq;

    public class RobotRequest
    {
        public string Country { get; set; }
        public string Continent { get; set; }
        public string Name { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public string AgentName { get; set; }
    }

    public class AssignAgentToRobotRequest
    {
        public string AgentName { get; set; }
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
        private readonly IAgentsService agentsService;

        public RobotsController(
            ILogger<RobotsController> logger,
            IRobotsService robotsService,
            IAgentsService agentsService
        )
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
            if (string.IsNullOrWhiteSpace(robot.Country))
            {
                ViewBag.ErrorMessage = "Veuillez inscrire un pays.";
                return View("_RobotErrorMessages");
            }
            if (!ModelState.IsValid)
            {
                return View(robot);
            }

            Agent assignedAgent = agentsService.Agents.FirstOrDefault(
                agent => agent.Name == robot.AgentName
            );

            if (assignedAgent != null)
            {
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
                    robot.Continent,
                    assignedAgent
                );

                // robotsService.AssignAgentToRobot(r, assignedAgent);

                return RedirectToAction("Robot", new { id = r.Id });
            }
            else
            {
                ViewBag.ErrorMessage = "Agent non trouvé.";
                return View("_RobotErrorMessages");
            }
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
                viewModel.ErrorMessage =
                    "Une erreur s'est produite lors de la modification du pays du robot.";
                return View("Robot", viewModel);
            }
        }

        [HttpGet]
        public IActionResult FilterRobots(string filter)
        {
            var filteredRobots = robotsService.FilterRobots(filter);
            return View("index", filteredRobots);
        }



        public void AssignRobotToAgent(Robot robot, Agent agent)
        {
            if (robot.AssignedAgent != null)
            {
                var formerAssignedAgent = robot.AssignedAgent;
                robot.FormerAssignedAgents.Add(formerAssignedAgent);
                robot.AssignedAgent = agent;
            }
            else
            {
                robot.AssignedAgent = agent;
            }
        }


        [HttpPost]
        public IActionResult DispatchAssignRobotToAgent(string robotId, string agentName)
        {
            Robot? robot = robotsService.Robots.FirstOrDefault(
                robot => robot.Id == Int32.Parse(robotId)
            );

            Agent? assignedAgent = agentsService.Agents.FirstOrDefault(
                agent => agent.Name == agentName
            );

            if (string.IsNullOrWhiteSpace(agentName))
            {
                ViewBag.ErrorMessage = "Veuillez entrer un agent";
                return View("Robot", robotsService.GetRobotById(Int32.Parse(robotId)));
            }

            if (assignedAgent == null)
            {
                ViewBag.ErrorMessage = "L'agent n'est pas valide";
                return View("Robot", robotsService.GetRobotById(Int32.Parse(robotId)));
            }

            if (robot != null && assignedAgent != null)
            {
                AssignRobotToAgent(robot, assignedAgent);

                return RedirectToAction("Robot", new { id = robotId });
            }
            else
            {
                ViewBag.ErrorMessage = "Veuillez inscrire un pays.";
                return View("Robot", new { id = robotId });
            }
        }

        [HttpPost]
        public IActionResult ChangeRobotContinent(int robotId, string newContinent)
        {
            robotsService.ChangeRobotContinent(robotId, newContinent);
            return RedirectToAction("Robot", new { id = robotId });
        }
    }
}
