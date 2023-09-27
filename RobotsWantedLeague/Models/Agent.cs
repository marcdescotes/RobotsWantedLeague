namespace RobotsWantedLeague.Models;

public class Agent
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Continent { get; set; }
    public List<Robot> AssignedRobots { get; set; } = new List<Robot>();
    public List<Robot> FormerAssignedRobots { get; set; } = new List<Robot>();

    public Agent(int Id, string Name, string Continent)
    {
        this.Id = Id;
        this.Name = Name;
        this.Continent = Continent;
    }
}