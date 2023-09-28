namespace RobotsWantedLeague.Test.Services;

using RobotsWantedLeague.Models;
using RobotsWantedLeague.Services;

[TestClass]
public class RobotsControllerTest
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
}
