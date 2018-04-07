using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stoneycreek.libraries.MultichainWrapper.Encryption;

namespace Unit_Tests.MultichainWrapper.Encryption
{
    [TestClass]
    public class EncryptStreamTests2
    {
        [TestMethod]
        public void TestEncryptStreamCreate()
        {
            // Setup
            // Test
            var enc = new EncryptStream();

            // Check
            Assert.IsNotNull(enc);
        }

        [TestMethod]
        public void TestEncryptStreamGenerateNewKey()
        {
            // Setup
            var enc = new EncryptStream();

            // Test
            enc.GenerateNewKey();

            // Check
            Assert.IsNotNull(enc.Key);
            Assert.AreEqual(32, enc.Key.Length);
        }

        [TestMethod]
        public void TestEncryptStreamEncrypt()
        {
            // Setup
            var enc = new EncryptStream();
            enc.GenerateNewKey();

            // Test
            var hexstring = enc.Encrypt("Test Message");

            // Check
            Assert.IsNotNull(hexstring);
            Assert.AreEqual(64, hexstring.Length);
        }

        [TestMethod]
        public void TestEncryptStreamDecrypt()
        {
            // Setup
            var enc = new EncryptStream();
            enc.Key = new byte[]
            {
                0xcd, 0x4f, 0x51, 0x98, 0xa3, 0xe8, 0x6c, 0xf4, 0x56, 0x18, 0x30, 0xf0, 0xdf, 0x18, 0x5e, 0x37,
                0x3c, 0x00, 0x3d, 0x12, 0x45, 0x74, 0xf2, 0xa6, 0x80, 0x0d, 0x5e, 0x84, 0xa8, 0x62, 0x03, 0x22
            };

            // Test
            var planestring = enc.Decrypt("49D9C6AC05FDEC3EE69099DF64A01FD5DA2B4D93050A13DB3D5ACA37B88337AC");

            // Check
            Assert.IsNotNull(planestring);
            Assert.AreEqual("Test Message", planestring);
        }
    }
}
