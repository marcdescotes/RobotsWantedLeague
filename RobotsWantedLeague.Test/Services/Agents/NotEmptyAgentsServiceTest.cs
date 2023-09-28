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

    [TestMethod]
    public void TestAssignRobotToAgent()
    {
        AgentsService service = new AgentsService();
        IRobotsService serviceRobot = new NotEmptyRobotsService();

        Agent agent = service.CreateAgent("Agent1", "Europe");
        Robot robot = serviceRobot.CreateRobot("Robot1", 100, 200, "France", "Europe", null);

        Assert.AreEqual(agent.AssignedRobots.Count, 0);

        service.AssignRobotToAgent(robot, agent);

        Assert.AreEqual(agent.AssignedRobots.Count, 1);
        Assert.IsTrue(agent.AssignedRobots.Contains(robot));

        Assert.AreEqual(robot.AssignedAgent, agent);
    }
}
