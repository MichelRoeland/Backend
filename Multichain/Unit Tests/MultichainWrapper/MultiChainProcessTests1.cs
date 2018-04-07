using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stoneycreek.libraries.MultichainWrapper;
using StoneyCreek.Services.Blockchain.DataContracts;

namespace Unit_Tests.MultichainWrapper
{
    [TestClass]
    public class MultiChainProcessTests1
    {
        [TestMethod]
        public void TestMultiChainCreate()
        {
            // Setup
            // Test
            var chain = new MultiChainProcess();

            // Check
            Assert.IsNotNull(chain);
        }

        [TestMethod]
        public void TestMultiChainStartServerProcess()
        {
            // Setup
            var processWrapper = new ProcessWrapper { Mock = true, ReplyMessage = "Blockchain test" };
            var chain = new MultiChainProcess(null, processWrapper);
            var ipAddress = IpAddress.GetLocalIPAddress();

            // Test
            var str = chain.StartServerProcess();

            // Check
            Assert.AreEqual("Blockchain test", str);
            Assert.AreEqual($"/c D:\\Development\\Eigenbouw\\bc\\MultiChain\\multichaind.exe testchain@{ipAddress}:1987 -daemon", processWrapper.ProcessInfo.Arguments);
        }

        [TestMethod]
        public void TestMultiChainCreateNewChain()
        {
            // Setup
            var processWrapper = new ProcessWrapper { Mock = true, ReplyMessage = "Blockchain test" };
            var chain = new MultiChainProcess(null, processWrapper);

            // Test
            var str = chain.CreateNewChain(new NewChainData { ChainName = "Test01", AdminConsensus = 1.0, CreateConsensus = 1.1, FirstBlocks = 1234 });

            // Check
            Assert.AreEqual("Blockchain test", str);
            Assert.AreEqual(@"/c D:\Development\Eigenbouw\bc\MultiChain\multichain-util.exe create Test01 -admin-consensus-admin=1 -admin-consensus-create=1.1 -setup-first-blocks=1234", processWrapper.ProcessInfo.Arguments);
        }

        [TestMethod]
        public void TestMultiChainCloneChain()
        {
            // Setup
            var processWrapper = new ProcessWrapper { Mock = true, ReplyMessage = "Blockchain test" };
            var chain = new MultiChainProcess(null, processWrapper);

            // Test
            var str = chain.CloneChain("Test01", "Test02");

            // Check
            Assert.AreEqual("Blockchain test", str);
            Assert.AreEqual(@"/c D:\Development\Eigenbouw\bc\MultiChain\multichain-util.exe clone Test01 Test02", processWrapper.ProcessInfo.Arguments);
        }

        [TestMethod]
        public void TestMultiChainStartClientProcess()
        {
            // Setup
            var processWrapper = new ProcessWrapper { Mock = true, ReplyMessage = "Blockchain test" };
            var chain = new MultiChainProcess(null, processWrapper);

            // Test
            var str = chain.StartClientProcess();

            // Check
            Assert.AreEqual("Blockchain test", str);
            Assert.AreEqual(@"/c D:\Development\Eigenbouw\bc\MultiChain\multichain-cli.exe testchain -deamon", processWrapper.ProcessInfo.Arguments);
        }
    }
}
