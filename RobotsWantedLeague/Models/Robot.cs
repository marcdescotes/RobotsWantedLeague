namespace RobotsWantedLeague.Models;

public class Robot
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Weight { get; set; }
    public int Height { get; set; }
    public string Country { get; set; }
    public string Continent { get; set; }
    public List<string> VisitedCountries { get; set; } = new List<string>();
    public Agent AssignedAgent { get; set; }
    public List<Agent> FormerAssignedAgents { get; set; } = new List<Agent>();

    public Robot(
        int Id,
        string Name,
        int Weight,
        int Height,
        string Country,
        string Continent,
        Agent AssignedAgent
    )
    {
        this.Id = Id;
        this.Name = Name;
        this.Weight = Weight;
        this.Height = Height;
        this.Country = Country;
        this.Continent = Continent;
        this.AssignedAgent = AssignedAgent;
    }
}
