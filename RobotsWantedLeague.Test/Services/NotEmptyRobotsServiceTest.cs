namespace RobotsWantedLeague.Test.Services;
using RobotsWantedLeague.Models;
using RobotsWantedLeague.Services;

[TestClass]
public class NotEmptyRobotsServiceTest
{

    [TestMethod]
    public void TestHappyPath()
    {
        IRobotsService service = new NotEmptyRobotsService();
        Assert.AreEqual(3, service.Robots.Count);
        Robot robot = service.CreateRobot("paul", 2, 3, "canada");
        Assert.AreEqual(robot.Name, "paul");
        Assert.AreEqual(robot.Weight, 2);

        Assert.AreEqual(4, service.Robots.Count);

        service.DeleteRobotById(robot.Id);
        Assert.AreEqual(3, service.Robots.Count);
    }


}