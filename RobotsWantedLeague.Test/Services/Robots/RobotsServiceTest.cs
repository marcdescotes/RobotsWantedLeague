namespace RobotsWantedLeague.Test.Services.Robots;

using RobotsWantedLeague.Models;
using RobotsWantedLeague.Services;

[TestClass]
public class RobotsServiceTest
{
    [TestMethod]
    public void TestCreateRobot()
    {
        RobotsService service = new RobotsService();
        IAgentsService serviceAgent = new NotEmptyAgentsService();

        Agent agent = serviceAgent.CreateAgent("paul", "Nord America");

        Robot robot = service.CreateRobot("paul", 2, 3, "canada", "Nord America", agent);
        Assert.AreEqual(robot.Name, "paul");
        Assert.AreEqual(robot.Weight, 2);
    }

    [TestMethod]
    public void TestGetRobotById()
    {
        RobotsService service = new RobotsService();
        IAgentsService serviceAgent = new NotEmptyAgentsService();

        Agent agent = serviceAgent.CreateAgent("paul", "Nord America");
        Assert.IsNull(service.GetRobotById(98));
        Robot robot = service.CreateRobot("paul", 2, 3, "canada", "Nord America", agent);
        Robot? ret = service.GetRobotById(robot.Id);
        Assert.IsNotNull(ret);
        Assert.AreEqual(robot.Name, ret.Name);
    }

    [TestMethod]
    public void TestGetRobotById_MultipleRobots()
    {
        RobotsService service = new RobotsService();
        IAgentsService serviceAgent = new NotEmptyAgentsService();

        Agent agent = serviceAgent.CreateAgent("paul", "Nord America");
        Robot robot1 = service.CreateRobot("paul", 2, 3, "canada", "Nord America", agent);
        Robot robot2 = service.CreateRobot("emanuel", 20, 30, "france", "Europe", agent);
        Robot? ret = service.GetRobotById(robot2.Id);
        Assert.IsNotNull(ret);
        Assert.AreEqual(robot2.Name, ret.Name);
    }

    [TestMethod]
    public void TestListRobots()
    {
        RobotsService service = new RobotsService();
        IAgentsService serviceAgent = new NotEmptyAgentsService();

        Agent agent = serviceAgent.CreateAgent("paul", "Nord America");
        Robot robot1 = service.CreateRobot("paul", 2, 3, "canada", "Nord America", agent);
        Robot robot2 = service.CreateRobot("emanuel", 20, 30, "france", "Europe", agent);
        Assert.AreEqual(2, service.Robots.Count);
    }

    [TestMethod]
    public void TestDeleteRobotById()
    {
        RobotsService service = new RobotsService();
        IAgentsService serviceAgent = new NotEmptyAgentsService();

        Agent agent = serviceAgent.CreateAgent("paul", "Nord America");
        Robot robot1 = service.CreateRobot("paul", 2, 3, "canada", "Nord America", agent);
        Robot robot2 = service.CreateRobot("emanuel", 20, 30, "france", "Europe", agent);
        Robot robot3 = service.CreateRobot("xu", 1, 9, "chine", "Asia", agent);

        Assert.IsTrue(service.DeleteRobotById(robot2.Id));

        Assert.AreEqual(2, service.Robots.Count);
        Assert.IsNull(service.GetRobotById(robot2.Id));
    }

    [TestMethod]
    public void Test_ValidCountry()
    {
        RobotsService service = new RobotsService();

        Assert.IsTrue(service.IsCountryValid("Canada"));
        Assert.IsFalse(service.IsCountryValid("awdaefwfd"));
        Assert.IsTrue(service.IsNullOrWhiteSpace(""));
    }

    [TestMethod]
    public void FilterRobots()
    {
        RobotsService service = new RobotsService();
        IAgentsService serviceAgent = new NotEmptyAgentsService();

        Agent agent = serviceAgent.CreateAgent("paul", "Nord America");
        Robot robot1 = service.CreateRobot("paul", 2, 3, "canada", "Nord America", agent);
        Robot robot2 = service.CreateRobot("emanuel", 20, 30, "france", "Europe", agent);
        Robot robot3 = service.CreateRobot("xu", 1, 9, "chine", "asia", agent);
        Robot robot4 = service.CreateRobot("test", 1, 9, "chine", "asia", agent);

        Assert.AreEqual(1, service.FilterRobots("canada").Count);
        Assert.AreEqual(2, service.FilterRobots("chine").Count);
        Assert.AreEqual(1, service.FilterRobots("Europe").Count);
        Assert.AreEqual(0, service.FilterRobots("jhgsg").Count);
        Assert.AreEqual(0, service.FilterRobots("").Count);
    }

    [TestMethod]
    public void TestAssignAgentToRobot()
    {
        RobotsService service = new RobotsService();
        IAgentsService serviceAgent = new NotEmptyAgentsService();

        Agent agent1 = serviceAgent.CreateAgent("paul", "Nord America");
        Robot robot1 = service.CreateRobot("paul", 2, 3, "canada", "Nord America", null);

        Assert.IsNull(robot1.AssignedAgent);

        service.AssignAgentToRobot(robot1, agent1);

        Assert.AreEqual(robot1.AssignedAgent, agent1);

        Assert.IsTrue(agent1.AssignedRobots.Contains(robot1));
    }
}
