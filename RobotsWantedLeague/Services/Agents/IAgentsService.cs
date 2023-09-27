namespace RobotsWantedLeague.Services;
using RobotsWantedLeague.Models;

public interface IAgentsService
{
    public List<Agent> Agents { get; }

    public Agent CreateAgent(string name,
                          string continent);
    public Agent? GetAgentById(int id);
    public bool DeleteAgentById(int id);
}