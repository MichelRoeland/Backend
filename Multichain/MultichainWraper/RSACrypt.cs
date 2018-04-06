using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Stoneycreek.libraries.MultichainWrapper
{
    public class RSACrypt
    {
        RSA rsa = new RSACryptoServiceProvider(2048); // Generate a new 2048 bit RSA key
        
        public string AssignNewKey()
        {
            const int PROVIDER_RSA_FULL = 1;
            const string CONTAINER_NAME = "KeyContainer";
            CspParameters cspParams;
            cspParams = new CspParameters(PROVIDER_RSA_FULL);
            cspParams.KeyContainerName = CONTAINER_NAME;
            cspParams.Flags = CspProviderFlags.UseMachineKeyStore;
            cspParams.ProviderName = "Microsoft Strong Cryptographic Provider";


            rsa = new RSACryptoServiceProvider(cspParams);
            // rsa.PersistKeyInCsp = false;

            string publicPrivateKeyXML = rsa.ToXmlString(true);
            string publicOnlyKeyXML = rsa.ToXmlString(false);
            // do stuff with keys...

            return publicPrivateKeyXML;
        }

    }
}
