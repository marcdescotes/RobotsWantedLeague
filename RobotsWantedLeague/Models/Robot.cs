namespace RobotsWantedLeague.Models;

public class Robot
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Weight { get; set; }
    public int Height { get; set; }
    public string Country { get; set; }
    public string NewCountry { get; set; }
    public List<string> VisitedCountries { get; set; } = new List<string>();
    public Agent AgentAssigné { get; set; }
    public List<Agent> AnciensAgentsAssignés { get; set; } = new List<Agent>();


    public Robot(int Id, string Name, int Weight, int Height, string Country, Agent AgentAssigné)
    {
        this.Id = Id;
        this.Name = Name;
        this.Weight = Weight;
        this.Height = Height;
        this.Country = Country;
        this.AgentAssigné = AgentAssigné;

        if (AgentAssigné != null)
        {
            AnciensAgentsAssignés.Add(AgentAssigné);
        }
    }
}