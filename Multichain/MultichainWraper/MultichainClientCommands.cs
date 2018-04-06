using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stoneycreek.libraries.MultichainWrapper
{
    public static class MultichainClientCommands
    {
        public struct GrantIdentifier
        {
            public string AssetIdentifierIssue;

            public string StreamIdentifierWrite;

            public GrantIdentifier(string id) : this()
            {
                // nothing with id!
                this.AssetIdentifierIssue = "asset-identifier.issue";
                this.StreamIdentifierWrite = "stream-identifier.write";
            }
        }

        public enum GrantPermissions
        {
            connect,
            send,
            receive,
            issue,
            mine,
            admin,
            activate,
            create
        }

        // General utilities
        public const string GetBlockChainParams = "getblockchainparams";
        public const string Getruntimeparams = "getruntimeparams";
        public const string Setruntimeparam = "setruntimeparam";
        public const string Getinfo = "getinfo";
        public const string Stop = "stop";

        // Managing wallet addresses
        public const string Addmultisigaddress = "addmultisigaddress {0}";
        public const string Getaddresses = "getaddresses verbose={0}";
        public const string Getnewaddress = "getnewaddress";
        public const string Importaddress = "importaddress {0} rescan={1}";

        /// <summary>
        /// (addresses=*) (verbose=false) (count=MAX) (start=-count)
        /// </summary>
        public const string Listaddresses = "listaddresses addresses={0} verbose={1} count={2} start={3}"; // addresses=* verbose=false;

        // Working with non-wallet addresses
        public const string Createkeypairs = "createkeypairs";
        public const string Createmultisig = "createmultisig";
        public const string Validateaddress = "validateaddress";

        /// <summary>
        /// Adres (GetAddress needs to be called to get address);
        /// Permissions need to be comma delimeted -> example: connect,send,receive
        /// </summary>
        public const string Grant = "grant {0} {1} {2} {3} {4} {5} {6}";

        public const string Grantfrom = "grantfrom";
        public const string Grantwithdata = "grantwithdata";
        public const string Grantwithmetadata = "grantwithmetadata";
        public const string Grantwithdatafrom = "grantwithdatafrom";
        public const string Grantwithmetadatafrom = "grantwithmetadatafrom";
        public const string Listpermissions = "listpermissions";
        public const string Revoke = "revoke {0} {2}"; // address / rights
        public const string Revokefrom = "revokefrom";

        // Asset management
        public const string Issue = "issue";
        public const string Issuefrom = "issuefrom";
        public const string Issuemore = "issuemore";
        public const string Issuemorefrom = "issuemorefrom";
        public const string Iistassets = "listassets";

        // Querying wallet balances and transactions
        public const string Getaddressbalances = "getaddressbalances";
        public const string Getaddresstransaction = "getaddresstransaction";
        public const string Getmultibalances = "getmultibalances";
        public const string Gettotalbalances = "gettotalbalances";
        public const string Getwallettransaction = "getwallettransaction";
        public const string Listaddresstransactions = "listaddresstransactions";
        public const string Listwallettransactions = "listwallettransactions";

        // Sending one-way payments
        public const string Send = "send";
        public const string Sendasset = "sendasset";
        public const string Sendassetfrom = "sendassetfrom";
        public const string Sendfrom = "sendfrom";
        public const string Sendwithdata = "sendwithdata";
        public const string Sendwithdatafrom = "sendwithdatafrom";

        // Atomic exchange transactions
        public const string Appendrawexchange = "appendrawexchange";
        public const string Completerawexchange = "completerawexchange";
        public const string Createrawexchange = "createrawexchange";
        public const string Decoderawexchange = "decoderawexchange";
        public const string Disablerawtransaction = "disablerawtransaction";
        public const string Preparelockunspent = "preparelockunspent";
        public const string Preparelockunspentfrom = "preparelockunspentfrom";

        // Stream management
        public const string CreateStream = "create {0} {1} {2}";
        public const string CreateStreamfrom = "createfrom";
        public const string Liststreams = "liststreams";

        // Publishing stream items
        public const string Publish = "publish {0} {1} {2}";
        public const string Publishfrom = "publishfrom {0} {1} {2} {3}";

        // Managing stream and asset subscriptions
        public const string Subscribe = "subscribe {0} {1}";
        public const string Unsubscribe = "unsubscribe  {0}";

        // Querying subscribed assets
        public const string Getassettransaction = "getassettransaction";
        public const string Listassettransactions = "listassettransactions";

        // Querying subscribed streams
        public const string Getstreamitem = "getstreamitem {0} {1}";
        public const string Gettxoutdata = "gettxoutdata";
        public const string Liststreamblockitems = "liststreamblockitems";
        public const string Liststreamkeyitems = "liststreamkeyitems {0} {1}";
        public const string Liststreamkeys = "liststreamkeys {0}";
        public const string Liststreamitems = "liststreamitems";
        public const string Liststreampublisheritems = "liststreampublisheritems";
        public const string Liststreampublishers = "liststreampublishers";

        // Managing wallet unspent outputs
        public const string Combineunspent = "combineunspent";
        public const string Listlockunspent = "listlockunspent";
        public const string Listunspent = "listunspent";
        public const string Lockunspent = "lockunspent";

        // Working with raw transactions
        public const string Appendrawchange = "appendrawchange";
        public const string Appendrawdata = "appendrawdata";
        public const string Appendrawtransaction = "appendrawtransaction";
        public const string Createrawtransaction = "createrawtransaction";
        public const string Createrawsendfrom = "createrawsendfrom";
        public const string Decoderawtransaction = "decoderawtransaction";
        public const string Sendrawtransaction = "sendrawtransaction";
        public const string Signrawtransaction = "signrawtransaction";

        // Peer-to-peer connections
        public const string Addnode = "addnode";
        public const string Getaddednodeinfo = "getaddednodeinfo";
        public const string Getnetworkinfo = "getnetworkinfo";
        public const string Getpeerinfo = "getpeerinfo";
        public const string Ping = "ping";

        // Messaging signing and verification
        public const string Signmessage = "signmessage {0} {1}";
        public const string Verifymessage = "verifymessage {0} {1} {2}";

        // Querying the blockchain
        public const string Getblock = "getblock";
        public const string Getblockchaininfo = "getblockchaininfo";
        public const string Getblockhash = "getblockhash";
        public const string Getmempoolinfo = "getmempoolinfo";
        public const string Getrawmempool = "getrawmempool";
        public const string Getrawtransaction = "getrawtransaction";
        public const string Gettxout = "gettxout";
        public const string Listblocks = "listblocks";

        // Advanced wallet control
        public const string Backupwallet = "backupwallet";
        public const string Dumpprivkey = "dumpprivkey";
        public const string Dumpwallet = "dumpwallet";
        public const string Encryptwallet = "encryptwallet";
        public const string Getwalletinfo = "getwalletinfo";
        public const string Importprivkey = "importprivkey {0}";
        public const string Walletlock = "walletlock";
        public const string Walletpassphrase = "walletpassphrase";
        public const string Walletpassphrasechange = "walletpassphrasechange";

        // Blockchain upgrading
        public const string Approvefrom = "approvefrom";
        public const string Create = "create";
        public const string Createfrom = "createfrom";
        public const string Listupgrades = "listupgrades";

        // Advanced node control
        public const string Clearmempool = "clearmempool";
        public const string Pause = "pause";
        public const string Resume = "resume";
        public const string Setlastblock = "setlastblock";
    }
}
