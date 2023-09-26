namespace RobotsWantedLeague.Services;

using RobotsWantedLeague.Models;

public class AgentsService: IAgentsService
{
    private readonly List<Agent> agents;
    private int idGenerator = 0;
    public List<Agent> Agents { get => agents; }

    public AgentsService()
    {
        agents = new List<Agent>();
    }

    private int generateId(){
        idGenerator = idGenerator + 1;
        return idGenerator;
    }

    public Agent CreateAgent(string name,
                          string continent)
    {
        var agent = new Agent(generateId(), name, continent);
        agents.Add(agent);
        return agent;
    }

    private int getIndexOfAgentById(int id){
        int idx = 0;
        foreach (Agent agent in agents){
            if (agent.Id == id){
                return idx;
            }
            idx ++;
        }
        return -1;
    }

    public Agent? GetAgentById(int id){
        int indexToDelete = getIndexOfAgentById(id);
        if(indexToDelete == -1){
            return null;
        }
        return agents[indexToDelete];
    }

    public bool DeleteAgentById(int id){
        int indexToDelete = getIndexOfAgentById(id);
        if(indexToDelete == -1){
            return false;
        }
        agents.RemoveAt(indexToDelete);
        return true;
    }

}