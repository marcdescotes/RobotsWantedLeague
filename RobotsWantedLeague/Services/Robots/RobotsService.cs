namespace RobotsWantedLeague.Services;

using System.Text.Json;
using RobotsWantedLeague.Models;

public class RobotsService : IRobotsService
{
    private readonly List<string> _validCountries;
    private readonly List<Robot> robots;
    private int idGenerator = 0;
    public List<Robot> Robots
    {
        get => robots;
    }

    public RobotsService()
    {
        robots = new List<Robot>();

        var validCountriesJson = System.IO.File.ReadAllText("data/ValidCountries.json");
        var validCountries = JsonSerializer.Deserialize<GetValidCountries>(validCountriesJson);
        _validCountries = validCountries?.Countries ?? new List<string>();
    }

    private int generateId()
    {
        idGenerator = idGenerator + 1;
        return idGenerator;
    }

    public Robot CreateRobot(string name, int weight, int height, string country)
    {
        var robot = new Robot(generateId(), name, weight, height, country);
        robots.Add(robot);
        return robot;
    }

    public List<Robot> FilterRobots(string filter)
    {
        IEnumerable<Robot> q = from robot in Robots where robot.Country == filter select robot;
        return q.ToList();
    }

    private int getIndexOfRobotById(int id)
    {
        int idx = 0;
        foreach (Robot robot in robots)
        {
            if (robot.Id == id)
            {
                return idx;
            }
            idx++;
        }
        return -1;
    }

    public Robot? GetRobotById(int id)
    {
        int indexToDelete = getIndexOfRobotById(id);
        if (indexToDelete == -1)
        {
            return null;
        }
        return robots[indexToDelete];
    }

    public bool DeleteRobotById(int id)
    {
        int indexToDelete = getIndexOfRobotById(id);
        if (indexToDelete == -1)
        {
            return false;
        }
        robots.RemoveAt(indexToDelete);
        return true;
    }

    public void ChangeRobotCountry(int robotId, string newCountry)
    {
        Robot robot = GetRobotById(robotId);
        if (robot != null)
        {
            robot.VisitedCountries.Add(robot.Country);
            robot.Country = newCountry;
        }
    }

    public class GetValidCountries
    {
        public List<string> Countries { get; set; }
    }

    public bool IsCountryValid(string country)
    {
        return _validCountries.Contains(char.ToUpper(country[0]) + country.Substring(1));
    }

}
