namespace RobotsWantedLeague.Services;

using RobotsWantedLeague.Models;

public class RobotsService: IRobotsService
{
    private readonly List<Robot> robots;
    private int idGenerator = 0;
    public List<Robot> Robots { get => robots; }

    public RobotsService()
    {
        robots = new List<Robot>();
    }

    private int generateId(){
        idGenerator = idGenerator + 1;
        return idGenerator;
    }

    public Robot CreateRobot(string name,
                          int weight,
                          int height,
                          string country)
    {
        var robot = new Robot(generateId(), name, weight, height, country);
        robots.Add(robot);
        return robot;
    }

    private int getIndexOfRobotById(int id){
        int idx = 0;
        foreach (Robot robot in robots){
            if (robot.Id == id){
                return idx;
            }
            idx ++;
        }
        return -1;
    }

    public Robot? GetRobotById(int id){
        int indexToDelete = getIndexOfRobotById(id);
        if(indexToDelete == -1){
            return null;
        }
        return robots[indexToDelete];
    }

    public bool DeleteRobotById(int id){
        int indexToDelete = getIndexOfRobotById(id);
        if(indexToDelete == -1){
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

}