namespace RobotsWantedLeague.Test.Services.Agents;

using RobotsWantedLeague.Models;
using RobotsWantedLeague.Services;

[TestClass]
public class AgentsServiceTest
{
    [TestMethod]
    public void TestCreateAgent()
    {
        AgentsService service = new AgentsService();
        Agent agent = service.CreateAgent("paul", "Europe");
        
        Assert.AreEqual(agent.Name, "paul");
        Assert.AreEqual(agent.Continent, "Europe");
    }

    [TestMethod]
    public void TestGetAgentById()
    {
        AgentsService service = new AgentsService();

        Assert.IsNull(service.GetAgentById(98));
        Agent agent = service.CreateAgent("paul", "Nord America");
        Agent? ret = service.GetAgentById(agent.Id);
        
        Assert.IsNotNull(ret);
        Assert.AreEqual(agent.Name, ret.Name);
    }

    [TestMethod]
    public void TestGetRobotById_MultipleRobots()
    {
        AgentsService service = new AgentsService();

        Agent agent = service.CreateAgent("paul", "Nord America");
        Agent agent2 = service.CreateAgent("max", "Europe");
        Agent? ret = service.GetAgentById(agent2.Id);

        Assert.IsNotNull(ret);
        Assert.AreEqual(agent2.Name, ret.Name);
    }

    [TestMethod]
    public void TestListRobots()
    {
        AgentsService service = new AgentsService();

        Agent agent = service.CreateAgent("paul", "Nord America");
        Agent agent2 = service.CreateAgent("max", "Europe");
        Assert.AreEqual(2, service.Agents.Count);
    }

    [TestMethod]
    public void TestDeleteRobotById()
    {
        AgentsService service = new AgentsService();
        Agent agent = service.CreateAgent("paul", "Nord America");
        Agent agent2 = service.CreateAgent("max", "Europe");

        Assert.IsTrue(service.DeleteAgentById(agent.Id));
        Assert.AreEqual(1, service.Agents.Count);
        Assert.IsNull(service.GetAgentById(agent.Id));
    }
}
