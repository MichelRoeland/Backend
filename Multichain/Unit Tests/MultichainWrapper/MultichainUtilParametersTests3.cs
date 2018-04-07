using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stoneycreek.libraries.MultichainWrapper;

namespace Unit_Tests.MultichainWrapper
{
    [TestClass]
    public class MultichainUtilParametersTests4
    {
        [TestMethod]
        public void TestMultichainUtilParametersCreate()
        {
            // Setup
            // Test
            var param = new MultichainUtilParameters();

            // Check
            Assert.IsNotNull(param);
        }

        [TestMethod]
        public void TestMultichainUtilParametersReadWrite()
        {
            // Setup
            var param = new MultichainUtilParameters();

            // Test
            param.ChainProtocol = new Protocol();
            param.ChainDescription = "Test01";
            param.RootStreamName = new RootStream();
            param.RootStreamOpen = true;
            param.ChainIsTestnet = true;
            param.AnyoneCanConnect = true;
            param.AnyoneCanSend = true;
            param.AnyoneCanReceive = true;
            param.AnyoneCanReceiveEmpty = true;
            param.AnyoneCanCreate = true;
            param.AnyoneCanIssue = true;
            param.AnyoneCanMine = true;
            param.AnyoneCanActivate = true;
            param.AnyoneCanAdmin = true;
            param.SupportMinerPrecheck = true;
            param.AllowArbitraryOutputs = true;
            param.AllowP2ShOutputs = true;

            // Check
            Assert.IsNotNull(param);
            Assert.IsInstanceOfType(param.ChainProtocol, typeof(Protocol));
            Assert.AreEqual("Test01", param.ChainDescription);
            Assert.IsInstanceOfType(param.RootStreamName, typeof(RootStream));
            Assert.IsTrue(param.RootStreamOpen);
            Assert.IsTrue(param.ChainIsTestnet);
            Assert.IsTrue(param.AnyoneCanConnect);
            Assert.IsTrue(param.AnyoneCanSend);
            Assert.IsTrue(param.AnyoneCanReceive);
            Assert.IsTrue(param.AnyoneCanReceiveEmpty);
            Assert.IsTrue(param.AnyoneCanCreate);
            Assert.IsTrue(param.AnyoneCanIssue);
            Assert.IsTrue(param.AnyoneCanMine);
            Assert.IsTrue(param.AnyoneCanActivate);
            Assert.IsTrue(param.AnyoneCanAdmin);
            Assert.IsTrue(param.SupportMinerPrecheck);
            Assert.IsTrue(param.AllowArbitraryOutputs);
            Assert.IsTrue(param.AllowP2ShOutputs);
        }
    }
}
