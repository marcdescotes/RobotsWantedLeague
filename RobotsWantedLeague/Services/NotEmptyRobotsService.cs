namespace RobotsWantedLeague.Services;

using RobotsWantedLeague.Models;

public class NotEmptyRobotsService: IRobotsService
{
    private readonly IRobotsService underlyingRobotsService;
    public List<Robot> Robots { get => underlyingRobotsService.Robots; }

    public NotEmptyRobotsService()
    {
        this.underlyingRobotsService = new RobotsService();
        this.underlyingRobotsService.CreateRobot("Alice", 1050, 2, "Bhutan");
        this.underlyingRobotsService.CreateRobot("Bob", 5001, 5, "Vanuatu");
        this.underlyingRobotsService.CreateRobot("Xu", 890, 1, "Taiwan");
    }

    public Robot CreateRobot(string name,
                          int weight,
                          int height,
                          string country)
    {
        return underlyingRobotsService.CreateRobot(name, weight, height, country);
    }


    public Robot? GetRobotById(int id){
        return underlyingRobotsService.GetRobotById(id);
    }

    public bool DeleteRobotById(int id){
        return underlyingRobotsService.DeleteRobotById(id);
    }

}