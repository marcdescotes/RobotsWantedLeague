namespace RobotsWantedLeague.Test.Services.Robots;

using RobotsWantedLeague.Models;
using RobotsWantedLeague.Services;

[TestClass]
public class NotEmptyRobotsServiceTest
{
    [TestMethod]
    public void TestHappyPath()
    {
        IRobotsService service = new NotEmptyRobotsService();
        IAgentsService serviceAgent = new NotEmptyAgentsService();

        Assert.AreEqual(3, service.Robots.Count);
        Agent agent = serviceAgent.CreateAgent("paul", "Europe");
        Robot robot = service.CreateRobot("paul", 2, 3, "canada", "Nord America", agent);
        Assert.AreEqual(robot.Name, "paul");
        Assert.AreEqual(robot.Weight, 2);

        Assert.AreEqual(4, service.Robots.Count);

        service.DeleteRobotById(robot.Id);
        Assert.AreEqual(3, service.Robots.Count);
    }

    [TestMethod]
    public void TestAssignAgentToRobot()
    {
        IRobotsService service = new NotEmptyRobotsService();
        IAgentsService serviceAgent = new NotEmptyAgentsService();

        Agent agent1 = serviceAgent.CreateAgent("paul", "Europe");
        Agent agent2 = serviceAgent.CreateAgent("john", "Asia");

        Robot robot1 = service.CreateRobot("robot1", 2, 3, "canada", "Nord America", agent1);

        service.AssignAgentToRobot(robot1, agent2);
        Assert.AreEqual(robot1.AssignedAgent, agent2);
        Assert.IsTrue(agent2.AssignedRobots.Contains(robot1));
        Assert.IsFalse(agent1.AssignedRobots.Contains(robot1));

        Robot robot2 = service.CreateRobot("robot2", 4, 5, "france", "Europe", null);
        service.AssignAgentToRobot(robot2, agent2);
        Assert.AreEqual(robot2.AssignedAgent, agent2);
        Assert.IsTrue(agent2.AssignedRobots.Contains(robot2));

        service.AssignAgentToRobot(robot2, null);
        Assert.IsNull(robot2.AssignedAgent);
        Assert.IsFalse(agent2.AssignedRobots.Contains(robot2));

        service.AssignAgentToRobot(null, agent1);

        Assert.IsNull(agent1.AssignedRobots.FirstOrDefault());
    }
}
