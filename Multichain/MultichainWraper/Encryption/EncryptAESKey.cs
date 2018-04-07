using PemUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Stoneycreek.libraries.MultichainWrapper.Encryption
{
    internal class EncryptAESKey
    {
        public RSAParameters publicKey { get; private set; }
        public RSAParameters privateKey { get; private set; }

        private void GetP()
        {
            System.IO.Stream stream = null;
            using (var reader = new PemReader(stream))
            {
                var rsaParameters = reader.ReadRsaKey();
            }
        }

        public byte[] EncryptData(byte[] dataToEncrypt)
        {
            byte[] cipherbytes;

            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.ImportParameters(publicKey);

                cipherbytes = rsa.Encrypt(dataToEncrypt, true);
            }

            return cipherbytes;
        }

        public byte[] DecryptData(byte[] dataToEncrypt)
        {
            byte[] plain;

            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.ImportParameters(privateKey);

                plain = rsa.Decrypt(dataToEncrypt, true);
            }

            return plain;
        }
    }
}
