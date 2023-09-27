namespace RobotsWantedLeague.Test.Services.Agents;
using RobotsWantedLeague.Models;
using RobotsWantedLeague.Services;

[TestClass]
public class NotEmptyAgentsServiceTest
{

    [TestMethod]
    public void TestHappyPath()
    {
        IAgentsService service = new NotEmptyAgentsService();
        Assert.AreEqual(7, service.Agents.Count);
        Agent agent = service.CreateAgent("paul", "Nord America");
        Assert.AreEqual(agent.Name, "paul");

        Assert.AreEqual(8, service.Agents.Count);

        service.DeleteAgentById(agent.Id);
        Assert.AreEqual(7, service.Agents.Count);
    }


}