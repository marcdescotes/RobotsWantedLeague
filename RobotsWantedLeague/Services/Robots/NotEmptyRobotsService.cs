namespace RobotsWantedLeague.Services;

using RobotsWantedLeague.Models;

public class NotEmptyRobotsService : IRobotsService
{
    private readonly IRobotsService underlyingRobotsService;
    public List<Robot> Robots
    {
        get => underlyingRobotsService.Robots;
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

        this.underlyingRobotsService.CreateRobot("Alice", 1050, 2, "Bhutan", agentMulder);
        this.underlyingRobotsService.CreateRobot("Bob", 5001, 5, "Vanuatu", agentScully);
        this.underlyingRobotsService.CreateRobot("Xu", 890, 1, "Taiwan", agentDoggett);
    }

    public Robot CreateRobot(string name, int weight, int height, string country, Agent agentAssigné)
    {
        return underlyingRobotsService.CreateRobot(name, weight, height, country, agentAssigné);
    }

    public Robot? GetRobotById(int id)
    {
        return underlyingRobotsService.GetRobotById(id);
    }

    public List<Robot> FilterRobots(string filter)
    {
        IEnumerable<Robot> q = from robot in Robots where robot.Country == filter select robot;
        return q.ToList();
    }

    public bool DeleteRobotById(int id)
    {
        return underlyingRobotsService.DeleteRobotById(id);
    }

    public void ChangeRobotCountry(int robotId, string newCountry)
    {
        underlyingRobotsService.ChangeRobotCountry(robotId, newCountry);
    }
}
