using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Stoneycreek.libraries.MultichainWrapper.Encryption
{
    public class EncryptStream
    {
        #region Properties

        public byte[] Key { get; set; }

        public byte[] Iv { get; private set; }

        #endregion Properties

        #region Public Methods

        public void GenerateNewKey()
        {
            Key = GenerateRandomNumber(32);
        }

        public string Encrypt(string message)
        {
            this.Iv = GenerateRandomNumber(16);

            var encrypted = Encrypt(Encoding.UTF8.GetBytes(message));

            var hexString = BitConverter.ToString(this.Iv).Replace("-", string.Empty).Replace(" ", string.Empty);
            hexString += BitConverter.ToString(encrypted).Replace("-", string.Empty).Replace(" ", string.Empty);

            return hexString;
        }

        public string Decrypt(string encrypted)
        {
            var size = (encrypted.Length - 32) / 2;

            this.Iv = new byte[16];
            byte[] encryptedBytes = new byte[size];

            for (int i = 0; i < 32; i += 2)
            {
                this.Iv[i / 2] = Convert.ToByte(encrypted.Substring(i, 2), 16);
            }

            for (int i = 32; i < encrypted.Length; i += 2)
            {
                encryptedBytes[(i - 32) / 2] = Convert.ToByte(encrypted.Substring(i, 2), 16);
            }

            var decrypted = Decrypt(encryptedBytes);

            return Encoding.UTF8.GetString(decrypted);
        }

        #endregion Public Methods

        #region Private Methods

        private byte[] GenerateRandomNumber(int length)
        {
            using (var randomNumberGenerator = new RNGCryptoServiceProvider())
            {
                var randomNumber = new byte[length];
                randomNumberGenerator.GetBytes(randomNumber);

                return randomNumber;
            }
        }

        private byte[] Encrypt(byte[] dataToEncrypt)
        {
            using (var aes = new AesCryptoServiceProvider())
            {
                using (var memoryStream = new MemoryStream())
                {
                    this.SetFields(aes);

                    var cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write);

                    cryptoStream.Write(dataToEncrypt, 0, dataToEncrypt.Length);
                    cryptoStream.FlushFinalBlock();

                    return memoryStream.ToArray();
                }
            }
        }

        private byte[] Decrypt(byte[] dataToDecrypt)
        {
            using (var aes = new AesCryptoServiceProvider())
            {
                using (var memoryStream = new MemoryStream())
                {
                    this.SetFields(aes);

                    var cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write);

                    cryptoStream.Write(dataToDecrypt, 0, dataToDecrypt.Length);
                    cryptoStream.FlushFinalBlock();

                    return memoryStream.ToArray();
                }
            }
        }

        private void SetFields(AesCryptoServiceProvider aes)
        {
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            aes.Key = this.Key;
            aes.IV = this.Iv;
        }

        #endregion Private Methods
    }
}
