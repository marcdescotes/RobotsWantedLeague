namespace RobotsWantedLeague.Services;

using System.Text.Json;
using RobotsWantedLeague.Models;

public class NotEmptyRobotsService : IRobotsService
{
    private readonly List<string> _validCountries;
    private readonly IRobotsService underlyingRobotsService;
    public List<Robot> Robots
    {
        get => underlyingRobotsService.Robots;
    }

    public class GetValidCountries
    {
        public List<string> Countries { get; set; }
    }

    public bool IsCountryValid(string country)
    {
        return _validCountries.Contains(char.ToUpper(country[0]) + country.Substring(1));
    }

    public NotEmptyRobotsService()
    {
        this.underlyingRobotsService = new RobotsService();
        var agentsService = new AgentsService();

        Agent agentMulder = agentsService.Agents.FirstOrDefault(agent => agent.Name == "Mulder");
        Agent agentScully = agentsService.Agents.FirstOrDefault(agent => agent.Name == "Scully");
        Agent agentDoggett = agentsService.Agents.FirstOrDefault(agent => agent.Name == "Doggett");
        Agent agentReyes = agentsService.Agents.FirstOrDefault(agent => agent.Name == "Reyes");
        Agent agentSkinner = agentsService.Agents.FirstOrDefault(agent => agent.Name == "Skinner");
        Agent agentSpender = agentsService.Agents.FirstOrDefault(agent => agent.Name == "Spender");
        Agent agentKersh = agentsService.Agents.FirstOrDefault(agent => agent.Name == "Kersh");

        this.underlyingRobotsService.CreateRobot("Alice", 1050, 2, "Bhutan", "Asia", agentMulder);
        this.underlyingRobotsService.CreateRobot("Bob", 5001, 5, "Vanuatu", "Europe", agentScully);
        this.underlyingRobotsService.CreateRobot(
            "Xu",
            890,
            1,
            "Taiwan",
            "Antarctica",
            agentDoggett
        );

        var validCountriesJson = System.IO.File.ReadAllText("data/ValidCountries.json");
        var validCountries = JsonSerializer.Deserialize<GetValidCountries>(validCountriesJson);
        _validCountries = validCountries?.Countries ?? new List<string>();
    }

    public Robot CreateRobot(
        string name,
        int weight,
        int height,
        string country,
        string continent,
        Agent assignedAgent
    )
    {
        return underlyingRobotsService.CreateRobot(
            name,
            weight,
            height,
            country,
            continent,
            assignedAgent
        );
    }

    public Robot? GetRobotById(int id)
    {
        return underlyingRobotsService.GetRobotById(id);
    }

    public List<Robot> FilterRobots(string filter)
    {
        // IEnumerable<Robot> q = from robot in Robots where robot.Country == filter select robot;
        return underlyingRobotsService.FilterRobots(filter).ToList();
    }

    public bool DeleteRobotById(int id)
    {
        return underlyingRobotsService.DeleteRobotById(id);
    }

    public void ChangeRobotCountry(int robotId, string newCountry)
    {
        underlyingRobotsService.ChangeRobotCountry(robotId, newCountry);
    }

    public void ChangeRobotContinent(int robotId, string newContinent)
    {
        underlyingRobotsService.ChangeRobotContinent(robotId, newContinent);
    }

    public void AssignAgentToRobot(Robot robot, Agent newAgent)
    {
        underlyingRobotsService.AssignAgentToRobot(robot, newAgent);
    }
}
