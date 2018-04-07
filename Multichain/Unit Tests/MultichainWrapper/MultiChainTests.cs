using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stoneycreek.libraries.MultichainWrapper;
using static Stoneycreek.libraries.MultichainWrapper.MultichainClientCommands;

namespace Unit_Tests.MultichainWrapper
{
    [TestClass]
    public class MultiChainTests
    {
        [TestMethod]
        public void TestMultiChainCreate()
        {
            // Setup
            // Test
            var chain = new MultiChain();

            // Check
            Assert.IsNotNull(chain);
        }

        [TestMethod]
        public void TestMultiChainHexStringToBytes()
        {
            // Setup
            var chain = new MultiChain();
            var verify = new byte[]
            {
                0x49, 0xd9, 0xc6, 0xac, 0x05, 0xfd, 0xec, 0x3e, 0xe6, 0x90, 0x99, 0xdf, 0x64, 0xa0, 0x1f, 0xd5,
                0xda, 0x2b, 0x4d, 0x93, 0x05, 0x0a, 0x13 ,0xdb, 0x3d, 0x5a, 0xca, 0x37, 0xb8, 0x83, 0x37, 0xac
            };

            // Test
            var bytes = chain.HexStringToBytes("49D9C6AC05FDEC3EE69099DF64A01FD5DA2B4D93050A13DB3D5ACA37B88337AC");

            // Check
            Assert.IsNotNull(bytes);
            for (int i = 0; i < verify.Length; i++)
            {
                Assert.AreEqual(verify[i], bytes[i]);
            }
        }

        [TestMethod]
        public void TestMultiChainStartServerProcess()
        {
            // Setup
            var processWrapper = new ProcessWrapper { Mock = true, ReplyMessage = "Blockchain test" };
            var chain = new MultiChain(null, processWrapper);
            var ipAddress = chain.GetLocalIPAddress();

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
            var chain = new MultiChain(null, processWrapper);

            // Test
            var str = chain.CreateNewChain("Test01", 1.0, 1.1, 1234);

            // Check
            Assert.AreEqual("Blockchain test", str);
            Assert.AreEqual(@"/c D:\Development\Eigenbouw\bc\MultiChain\multichain-util.exe create Test01 -admin-consensus-admin=1 -admin-consensus-create=1,1 -setup-first-blocks=1234", processWrapper.ProcessInfo.Arguments);
        }

        [TestMethod]
        public void TestMultiChainCloneChain()
        {
            // Setup
            var processWrapper = new ProcessWrapper { Mock = true, ReplyMessage = "Blockchain test" };
            var chain = new MultiChain(null, processWrapper);

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
            var chain = new MultiChain(null, processWrapper);

            // Test
            var str = chain.StartClientProcess();

            // Check
            Assert.AreEqual("Blockchain test", str);
            Assert.AreEqual(@"/c D:\Development\Eigenbouw\bc\MultiChain\multichain-cli.exe testchain -deamon", processWrapper.ProcessInfo.Arguments);
        }

        [TestMethod]
        public void TestMultiChainGrantPermisions()
        {
            // Setup
            var processWrapper = new ProcessWrapper { Mock = true, ReplyMessage = "Blockchain test" };
            var chain = new MultiChain(null, processWrapper);

            // Test
            var str = chain.GrantPermisions("Test01", new[] { GrantPermissions.activate }, "test02", "test03", "0.1");

            // Check
            Assert.AreEqual("Blockchain test", str);
            Assert.AreEqual("/c D:\\Development\\Eigenbouw\\bc\\MultiChain\\multichain-cli.exe testchain grant Test01 activate   0.1 \"test02\" \"test03\"", processWrapper.ProcessInfo.Arguments);
        }

        [TestMethod]
        public void TestMultiChainRevokePermisions()
        {
            // Setup
            var processWrapper = new ProcessWrapper { Mock = true, ReplyMessage = "Blockchain test" };
            var chain = new MultiChain(null, processWrapper);

            // Test
            var str = chain.RevokePermisions("Test01", new[] { GrantPermissions.activate }, "test02", "test03", "0.1");

            // Check
            Assert.AreEqual("Blockchain test", str);
            Assert.AreEqual("/c D:\\Development\\Eigenbouw\\bc\\MultiChain\\multichain-cli.exe testchain revoke Test01", processWrapper.ProcessInfo.Arguments);
        }

        [TestMethod]
        public void TestMultiChainListPermissions()
        {
            // Setup
            var processWrapper = new ProcessWrapper { Mock = true, ReplyMessage = "[{ \"address\": \"Test01\", \"startblock\": \"12345\", \"endblock\": \"23456\" }]" };
            var chain = new MultiChain(null, processWrapper);

            // Test
            var perms = chain.ListPermissions();

            // Check
            Assert.AreEqual(1, perms.permissions.Length);
            Assert.AreEqual("Test01", perms.permissions[0].address);
            Assert.AreEqual("12345", perms.permissions[0].startblock);
            Assert.AreEqual("23456", perms.permissions[0].endblock);
            Assert.AreEqual("/c D:\\Development\\Eigenbouw\\bc\\MultiChain\\multichain-cli.exe testchain listpermissions", processWrapper.ProcessInfo.Arguments);
        }

        [TestMethod]
        public void TestMultiChainListStreams()
        {
            // Setup
            var processWrapper = new ProcessWrapper { Mock = true, ReplyMessage = "[{ \"name\": \"Test01\", \"createtxid\": \"Test02\", \"open\": true, \"details\": \"abc\", \"subscribed\": true, \"synchronized\": true, \"items\": 1, \"confirmed\": 2, \"keys\": 3, \"publishers\": 4, \"streamref\": \"Test03\" }]" };
            var chain = new MultiChain(null, processWrapper);

            // Test
            var stream = chain.ListStreams();

            // Check
            Assert.AreEqual(1, stream.streams.Length);
            Assert.AreEqual("Test01", stream.streams[0].name);
            Assert.AreEqual("Test02", stream.streams[0].createtxid);
            Assert.IsTrue(stream.streams[0].open);
            Assert.AreEqual("abc", stream.streams[0].details);
            Assert.IsTrue(stream.streams[0].subscribed);
            Assert.IsTrue(stream.streams[0].synchronized);
            Assert.AreEqual(1, stream.streams[0].items);
            Assert.AreEqual(2, stream.streams[0].confirmed);
            Assert.AreEqual(3, stream.streams[0].keys);
            Assert.AreEqual(4, stream.streams[0].publishers);
            Assert.AreEqual("Test03", stream.streams[0].streamref);
            Assert.AreEqual("/c D:\\Development\\Eigenbouw\\bc\\MultiChain\\multichain-cli.exe testchain liststreams", processWrapper.ProcessInfo.Arguments);
        }

        [TestMethod]
        public void TestMultiChainCreateNewAddress()
        {
            // Setup
            var processWrapper = new ProcessWrapper { Mock = true, ReplyMessage = "Blockchain test" };
            var chain = new MultiChain(null, processWrapper);

            // Test
            var str = chain.CreateNewAddress();

            // Check
            Assert.AreEqual("Blockchain test", str);
            Assert.AreEqual("/c D:\\Development\\Eigenbouw\\bc\\MultiChain\\multichain-cli.exe testchain getnewaddress", processWrapper.ProcessInfo.Arguments);
        }

        [TestMethod]
        public void TestMultiChainCreateNewStream()
        {
            // Setup
            var processWrapper = new ProcessWrapper { Mock = true, ReplyMessage = "Blockchain test" };
            var chain = new MultiChain(null, processWrapper);

            // Test
            var str = chain.CreateNewStream(true, "Test01");

            // Check
            Assert.AreEqual("Blockchain test", str);
            Assert.AreEqual("/c D:\\Development\\Eigenbouw\\bc\\MultiChain\\multichain-cli.exe testchain create stream Test01 true", processWrapper.ProcessInfo.Arguments);
        }

        [TestMethod]
        public void TestMultiChainPublishMessage()
        {
            // Setup
            var processWrapper = new ProcessWrapper { Mock = true, ReplyMessage = "Blockchain test" };
            var chain = new MultiChain(null, processWrapper);

            // Test
            var str = chain.PublishMessage("KEY01", "A1B2C3E4F5", "Test01");

            // Check
            Assert.AreEqual("Blockchain test", str);
            Assert.AreEqual("/c D:\\Development\\Eigenbouw\\bc\\MultiChain\\multichain-cli.exe testchain publish Test01 \"KEY01\" A1B2C3E4F5", processWrapper.ProcessInfo.Arguments);
        }

        [TestMethod]
        public void TestMultiChainPublishMessageFrom()
        {
            // Setup
            var processWrapper = new ProcessWrapper { Mock = true, ReplyMessage = "Blockchain test" };
            var chain = new MultiChain(null, processWrapper);

            // Test
            var str = chain.PublishMessage("KEY01", "V3d45S", "A1B2C3E4F5", "Test01");

            // Check
            Assert.AreEqual("Blockchain test", str);
            Assert.AreEqual("/c D:\\Development\\Eigenbouw\\bc\\MultiChain\\multichain-cli.exe testchain publishfrom V3d45S Test01 \"KEY01\" A1B2C3E4F5", processWrapper.ProcessInfo.Arguments);
        }

        [TestMethod]
        public void TestMultiChainSubscribe()
        {
            // Setup
            var processWrapper = new ProcessWrapper { Mock = true, ReplyMessage = "Blockchain test" };
            var chain = new MultiChain(null, processWrapper);

            // Test
            var str = chain.Subscribe("Test01");

            // Check
            Assert.AreEqual("Blockchain test", str);
            Assert.AreEqual("/c D:\\Development\\Eigenbouw\\bc\\MultiChain\\multichain-cli.exe testchain subscribe Test01 true", processWrapper.ProcessInfo.Arguments);
        }

        [TestMethod]
        public void TestMultiChainUnSubscribe()
        {
            // Setup
            var processWrapper = new ProcessWrapper { Mock = true, ReplyMessage = "Blockchain test" };
            var chain = new MultiChain(null, processWrapper);

            // Test
            var str = chain.UnSubscribe("Test01");

            // Check
            Assert.AreEqual("Blockchain test", str);
            Assert.AreEqual("/c D:\\Development\\Eigenbouw\\bc\\MultiChain\\multichain-cli.exe testchain unsubscribe Test01", processWrapper.ProcessInfo.Arguments);
        }

        [TestMethod]
        public void TestMultiChainGetStreamKeys()
        {
            // Setup
            var processWrapper = new ProcessWrapper { Mock = true, ReplyMessage = "[{ \"key\": \"Test01\", \"items\": \"1\", \"confirmed\": \"2\" }]" };
            var chain = new MultiChain(null, processWrapper);

            // Test
            var keys = chain.GetStreamKeys("Test01");

            // Check
            Assert.AreEqual(1, keys.streamkeys.Length);
            Assert.AreEqual("Test01", keys.streamkeys[0].key);
            Assert.AreEqual(1, keys.streamkeys[0].items);
            Assert.AreEqual(2, keys.streamkeys[0].confirmed);
            Assert.AreEqual("/c D:\\Development\\Eigenbouw\\bc\\MultiChain\\multichain-cli.exe testchain liststreamkeys Test01", processWrapper.ProcessInfo.Arguments);
        }

        [TestMethod]
        public void TestMultiChainGetStreamItem()
        {
            // Setup
            var processWrapper = new ProcessWrapper { Mock = true, ReplyMessage = "{ \"publishers\": [\"Test01\"], \"key\": \"Test02\", \"data\": \"Test03\", \"confirmations\": 1, \"blocktime\": 2, \"txid\": \"Test04\" }" };
            var chain = new MultiChain(null, processWrapper);

            // Test
            var item = chain.GetStreamItem("Test01", "12345");

            // Check
            Assert.AreEqual(1, item.publishers.Length);
            Assert.AreEqual("Test01", item.publishers[0]);
            Assert.AreEqual("Test02", item.key);
            Assert.AreEqual("Test03", item.data);
            Assert.AreEqual(1, item.confirmations);
            Assert.AreEqual(2, item.blocktime);
            Assert.AreEqual("Test04", item.txid);
            Assert.AreEqual("/c D:\\Development\\Eigenbouw\\bc\\MultiChain\\multichain-cli.exe testchain getstreamitem Test01 12345", processWrapper.ProcessInfo.Arguments);
        }

        [TestMethod]
        public void TestMultiChainGetStreamItemByKey()
        {
            // Setup
            var processWrapper = new ProcessWrapper { Mock = true, ReplyMessage = "[{ \"publishers\": [\"Test01\"], \"key\": \"Test02\", \"data\": \"Test03\", \"confirmations\": 1, \"blocktime\": 2, \"txid\": \"Test04\" }]" };
            var chain = new MultiChain(null, processWrapper);

            // Test
            var item = chain.GetStreamItemByKey("Test01", "123");

            // Check
            Assert.AreEqual(1, item.streamitems.Length);
            Assert.AreEqual(1, item.streamitems[0].publishers.Length);
            Assert.AreEqual("Test01", item.streamitems[0].publishers[0]);
            Assert.AreEqual("Test02", item.streamitems[0].key);
            Assert.AreEqual("Test03", item.streamitems[0].data);
            Assert.AreEqual(1, item.streamitems[0].confirmations);
            Assert.AreEqual(2, item.streamitems[0].blocktime);
            Assert.AreEqual("Test04", item.streamitems[0].txid);
            Assert.AreEqual("/c D:\\Development\\Eigenbouw\\bc\\MultiChain\\multichain-cli.exe testchain liststreamkeyitems Test01 123", processWrapper.ProcessInfo.Arguments);
        }

        [TestMethod]
        public void TestMultiChainSignMessage()
        {
            // Setup
            var processWrapper = new ProcessWrapper { Mock = true, ReplyMessage = "Blockchain test" };
            var chain = new MultiChain(null, processWrapper);

            // Test
            var str = chain.SignMessage("A1B2C3E4F5", "Test01");

            // Check
            Assert.AreEqual("Blockchain test", str);
            Assert.AreEqual("/c D:\\Development\\Eigenbouw\\bc\\MultiChain\\multichain-cli.exe testchain signmessage A1B2C3E4F5 \"Test01\"", processWrapper.ProcessInfo.Arguments);
        }

        [TestMethod]
        public void TestMultiChainVerifyMessage()
        {
            // Setup
            var processWrapper = new ProcessWrapper { Mock = true, ReplyMessage = "Blockchain test" };
            var chain = new MultiChain(null, processWrapper);

            // Test
            var str = chain.VerifyMessage("1234", "A1B2C3E4F5", "Test01");

            // Check
            Assert.AreEqual("Blockchain test", str);
            Assert.AreEqual("/c D:\\Development\\Eigenbouw\\bc\\MultiChain\\multichain-cli.exe testchain verifymessage 1234 A1B2C3E4F5 \"Test01\"", processWrapper.ProcessInfo.Arguments);
        }

        [TestMethod]
        public void TestMultiChainImportPrivateKey()
        {
            // Setup
            var processWrapper = new ProcessWrapper { Mock = true, ReplyMessage = "Blockchain test" };
            var chain = new MultiChain(null, processWrapper);

            // Test
            var str = chain.ImportPrivateKey("A1B2C3E4F5");

            // Check
            Assert.AreEqual("Blockchain test", str);
            Assert.AreEqual("/c D:\\Development\\Eigenbouw\\bc\\MultiChain\\multichain-cli.exe testchain importprivkey A1B2C3E4F5", processWrapper.ProcessInfo.Arguments);
        }

        [TestMethod]
        public void TestMultiChainCreateKeys()
        {
            // Setup
            var processWrapper = new ProcessWrapper { Mock = true, ReplyMessage = "[{ \"address\": \"Test01\", \"pubkey\": \"Test02\", \"privkey\": \"Test03\" }]" };
            var chain = new MultiChain(null, processWrapper);

            // Test
            var keys = chain.CreateKeys();

            // Check
            Assert.AreEqual(1, keys.keypairs.Length);
            Assert.AreEqual("Test01", keys.keypairs[0].address);
            Assert.AreEqual("Test02", keys.keypairs[0].pubkey);
            Assert.AreEqual("Test03", keys.keypairs[0].privkey);
            Assert.AreEqual("/c D:\\Development\\Eigenbouw\\bc\\MultiChain\\multichain-cli.exe testchain createkeypairs", processWrapper.ProcessInfo.Arguments);
        }
    }
}
