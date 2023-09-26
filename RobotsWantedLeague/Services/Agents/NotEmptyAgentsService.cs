namespace RobotsWantedLeague.Services;

using RobotsWantedLeague.Models;

public class NotEmptyAgentsService : IAgentsService
{
    private readonly IAgentsService underlyingAgentsService;
    public List<Agent> Agents { get => underlyingAgentsService.Agents; }

    public NotEmptyAgentsService()
    {
        this.underlyingAgentsService = new AgentsService();
        this.underlyingAgentsService.CreateAgent("Mulder", "Amérique du Nord");
        this.underlyingAgentsService.CreateAgent("Scully", "Amérique du Sud");
        this.underlyingAgentsService.CreateAgent("Doggett", "Antarctique");
        this.underlyingAgentsService.CreateAgent("Reyes", "Europe");
        this.underlyingAgentsService.CreateAgent("Skinner", "Asie");
        this.underlyingAgentsService.CreateAgent("Spender", "Afrique");
        this.underlyingAgentsService.CreateAgent("Kersh", "Océanie");
    }

    public Agent CreateAgent(string name,
                          string continent)
    {
        return underlyingAgentsService.CreateAgent(name, continent);
    }

    public Agent? GetAgentById(int id)
    {
        return underlyingAgentsService.GetAgentById(id);
    }

    public bool DeleteAgentById(int id)
    {
        return underlyingAgentsService.DeleteAgentById(id);
    }

}