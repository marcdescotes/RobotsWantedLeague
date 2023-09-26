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
        this.underlyingRobotsService.CreateRobot("Alice", 1050, 2, "Bhutan", "Asia");
        this.underlyingRobotsService.CreateRobot("Bob", 5001, 5, "Vanuatu", "Oceania");
        this.underlyingRobotsService.CreateRobot("Xu", 890, 1, "Taiwan", "Asia");
    }

    public Robot CreateRobot(string name, int weight, int height, string country, string continent)
    {
        return underlyingRobotsService.CreateRobot(name, weight, height, country, continent);
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
}
