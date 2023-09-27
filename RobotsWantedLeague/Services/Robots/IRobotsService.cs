namespace RobotsWantedLeague.Services;
using RobotsWantedLeague.Models;

public interface IRobotsService
{
    public List<Robot> Robots { get; }

    public Robot CreateRobot(string name,
                          int weight,
                          int height,
                          string country,
                          string continent, Agent assignedAgent);
    public Robot? GetRobotById(int id);
    public bool DeleteRobotById(int id);
    public void ChangeRobotCountry(int robotId, string newCountry);
    public void ChangeRobotAgent(int robotId, Agent newAgent);
    public void ChangeRobotContinent(int robotId, string newContinent);
    public bool IsCountryValid(string country);
    public List<Robot> FilterRobots(string filter);
}