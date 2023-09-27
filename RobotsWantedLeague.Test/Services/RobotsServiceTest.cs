namespace RobotsWantedLeague.Test.Services;
using RobotsWantedLeague.Models;
using RobotsWantedLeague.Services;

[TestClass]
public class RobotsServiceTest
{

    [TestMethod]
    public void TestCreateRobot()
    {
        RobotsService service = new RobotsService();
        Robot robot = service.CreateRobot("paul", 2, 3, "canada", "Nord America");
        Assert.AreEqual(robot.Name, "paul");
        Assert.AreEqual(robot.Weight, 2);
    }


    [TestMethod]
    public void TestGetRobotById()
    {
        RobotsService service = new RobotsService();
        Assert.IsNull(service.GetRobotById(98));
        Robot robot = service.CreateRobot("paul", 2, 3, "canada", "Nord America");
        Robot? ret = service.GetRobotById(robot.Id);
        Assert.IsNotNull(ret);
        Assert.AreEqual(robot.Name, ret.Name);
    }

    [TestMethod]
    public void TestGetRobotById_MultipleRobots()
    {
        RobotsService service = new RobotsService();
        Robot robot1 = service.CreateRobot("paul", 2, 3, "canada", "Nord America");
        Robot robot2 = service.CreateRobot("emanuel", 20, 30, "france", "Europe");
        Robot? ret = service.GetRobotById(robot2.Id);
        Assert.IsNotNull(ret);
        Assert.AreEqual(robot2.Name, ret.Name);
    }

    [TestMethod]
    public void TestListRobots()
    {
        RobotsService service = new RobotsService();
        Robot robot1 = service.CreateRobot("paul", 2, 3, "canada", "Nord America");
        Robot robot2 = service.CreateRobot("emanuel", 20, 30, "france", "Europe");
        Assert.AreEqual(2, service.Robots.Count);
    }

    [TestMethod]
    public void TestDeleteRobotById()
    {
        RobotsService service = new RobotsService();
        Robot robot1 = service.CreateRobot("paul", 2, 3, "canada", "Nord America");
        Robot robot2 = service.CreateRobot("emanuel", 20, 30, "france", "Europe");
        Robot robot3 = service.CreateRobot("xu", 1, 9, "chine", "Asia");

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
        Robot robot1 = service.CreateRobot("paul", 2, 3, "canada", "Nord America");
        Robot robot2 = service.CreateRobot("emanuel", 20, 30, "france", "Europe");
        Robot robot3 = service.CreateRobot("xu", 1, 9, "chine", "asia");
        Robot robot4 = service.CreateRobot("test", 1, 9, "chine", "asia");

        Assert.AreEqual(1, service.FilterRobots("canada").Count);
        Assert.AreEqual(2, service.FilterRobots("chine").Count);
        Assert.AreEqual(1, service.FilterRobots("Europe").Count);
        Assert.AreEqual(0, service.FilterRobots("jhgsg").Count);
        Assert.AreEqual(0, service.FilterRobots("").Count);
    }

}