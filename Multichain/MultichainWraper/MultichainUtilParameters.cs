using System;

namespace Stoneycreek.libraries.MultichainWrapper
{
    public class MultichainUtilParameters
    {        
        // Basic chain parameters
        public Protocol ChainProtocol { get; set; }                 // bitcoin or multichain
        public string ChainDescription {get; set; }                 // MultiChain blafiets2
        public RootStream RootStreamName { get; set; }              // RootStream or blank
        public bool RootStreamOpen { get; set; }                    // Allow anyone to publish in root stream
        public bool ChainIsTestnet { get; set; }                    // Content of the 'testnet' field of API responses, for compatibility.

        // Global permissions
        public bool AnyoneCanConnect { get; set; }                // Anyone can connect, i.e. a publicly readable blockchain.
        public bool AnyoneCanSend { get; set; }                   // Anyone can send, i.e. transaction signing not restricted by address.
        public bool AnyoneCanReceive { get; set; }                // Anyone can receive, i.e. transaction outputs not restricted by address.
        public bool AnyoneCanReceiveEmpty { get; set; }           // Anyone can receive empty output, i.e. without permission grants, asset transfers and zero native currency.
        public bool AnyoneCanCreate { get; set; }                 // Anyone can create new streams.
        public bool AnyoneCanIssue { get; set; }                  // Anyone can issue new native assets.
        public bool AnyoneCanMine { get; set; }                   // Anyone can mine blocks (confirm transactions).
        public bool AnyoneCanActivate { get; set; }               // Anyone can grant or revoke connect, send and receive permissions.
        public bool AnyoneCanAdmin { get; set; }                  // Anyone can grant or revoke all permissions.
        public bool SupportMinerPrecheck { get; set; }            // Require special metadata output with cached scriptPubKey for input, to support advanced miner checks.
        public bool AllowArbitraryOutputs { get; set; }           // Allow arbitrary (without clear destination) scripts.
        public bool AllowP2ShOutputs { get; set; }                // Allow pay_to_scripthash (P2SH) scripts, often used for multisig. Ignored if allow_arbitrary_outputs=true.

        // Consensus requirements
        public int SetupFirstBlocks = 60;                       // Length of initial setup phase in blocks, in which mining_diversity,
        
        // admin_consensus_* and mining_requires_peers are not applied. (1 _ 31536000)
        public double MiningDiversity = 0.3;                    // Miners must wait <mining_diversity>*<active miners> between blocks. (0 _ 1)
        public double AdminConsensusUpgrade = 0.5;              // <admin_consensus_upgrade>*<active admins> needed to upgrade the chain. (0 _ 1)
        public double AdminConsensusAdmin = 0.5;                // <admin_consensus_admin>*<active admins> needed to change admin perms. (0 _ 1)
        public double AdminConsensusActivate = 0.5;             // <admin_consensus_activate>*<active admins> to change activate perms. (0 _ 1)
        public double AdminConsensusMine = 0.5;                 // <admin_consensus_mine>*<active admins> to change mining permissions. (0 _ 1)
        public double AdminConsensusCreate = 0.0;               // <admin_consensus_issue>*<active admins> to change create permissions. (0 _ 1)
        public double AdminConsensusIssue = 0.0;                // <admin_consensus_issue>*<active admins> to change issue permissions. (0 _ 1)

        // Defaults for node runtime parameters
        public int LockAdminMineRounds = 10;                    // Ignore forks that reverse changes in admin or mine permissions after this many mining rounds have passed. Integer only. (0 _ 10000)
        public bool MiningRequiresPeers = true;                 // Nodes only mine blocks if connected to other nodes (ignored if only one permitted miner).
        public double MineEmptyRounds = 10;                     // Mine this many rounds of empty blocks before pausing to wait for new transactions. If negative, continue indefinitely (ignored if target_adjust_freq>0). Non_integer allowed. (_1 _ 1000)
        public double MiningTurnover = 0.5;                     // Prefer pure round robin between a subset of active miners to minimize forks (0.0) or random equal participation for all permitted miners (1.0). (0 _ 1)

        // Native blockchain currency (likely not required)
        public int InitialBlockReward = 0;                      // Initial block mining reward in raw native currency units. (0 _ _1486618624)
        public int FirstBlockReward = 1;                        // Different mining reward for first block only, ignored if negative. (_1 _ _1486618624)
        public Int64 RewardHalvingInterval = 52560000;          // Interval for halving of mining rewards, in blocks. (60 _ _1)
        public int RewardSpendableDelay = 1;                    // Delay before mining reward can be spent, in blocks. (1 _ 100000)
        public int MinimumPerOutput = 0;                        // Minimum native currency per output (anti_dust), in raw units.
                                                                // If set to _1, this is calculated from minimum_relay_fee. (_1 _ 1000000000)
        public Int64 MaximumPerOutput = 100000000000000;        // Maximum native currency per output, in raw units. (0 _ _1486618624)
        public int MinimumRelayFee = 0;                         // Minimum transaction fee, per 1000 bytes, in raw units of native currency. (0 _ 1000000000)
        public Int64 NativeCurrencyMultiple = 100000000;        // Number of raw units of native currency per display unit. (0 _ 1000000000)

        // Advanced mining parameters
        public bool SkipPowCheck = false;                       // Skip checking whether block hashes demonstrate proof of work.
        public int PowMinimumBits = 8;                          // Initial and minimum proof of work difficulty, in leading zero bits. (1 _ 32)
        public string TargetAdjustFreq = "_1";                  // Interval between proof of work difficulty adjustments, in seconds, if negative _ never adjusted. (_1 _ _1)
        public bool AllowMinDifficultyBlocks = false;           // Allow lower difficulty blocks if none after 2*<target_block_time>.

        // Standard transaction definitions
        public bool OnlyAcceptStdTxs = true;                    // Only accept and relay transactions which qualify as 'standard'.
        public Int64 MaxStdTxSize = 4194304;                    // Maximum size of standard transactions, in bytes. (1024 _ 100000000)
        public int MaxStdOpReturnsCount = 10;                   // Maximum number of OP_RETURN metadata outputs in standard transactions. (0 _ 1024)
        public Int64 MaxStdOpReturnSize = 2097152;              // Maximum size of OP_RETURN metadata in standard transactions, in bytes. (0 _ 67108864)
        public int MaxStdOpDropsCount = 5;                      // Maximum number of OP_DROPs per output in standard transactions. (0 _ 100)
        public int MaxStdElementSize = 8192;                    // Maximum size of data elements in standard transactions, in bytes. (128 _ 32768)

        // The following parameters were generated by multichain_util.
        // They SHOULD ONLY BE EDITED IF YOU KNOW WHAT YOU ARE DOING. 
        public int DefaultNetworkPort = 2691;                   // Default TCP/IP port for peer_to_peer connection with other nodes.
        public int DefaultRpcPort = 2690;                       // Default TCP/IP port for incoming JSON_RPC API requests.

        public string ChainName = "blafiets2";                  // Chain name, used as first argument for multichaind and multichain_cli.
        public int ProtocolVersion = 10009  ;                   // Protocol version at the moment of blockchain genesis.
        public string NetworkMessageStart = "f7feddf5";         // Magic value sent as the first 4 bytes of every peer_to_peer message.
        public string AddressPubkeyhashVersion = "003fe13c";    // Version bytes used for pay_to_pubkeyhash addresses.
        public string AddressScripthashVersion = "05a4cc64";    // Version bytes used for pay_to_scripthash addresses.
        public string PrivateKeyVersion = "8060b5a5";           // Version bytes used for exporting private keys.
        public string AddressChecksumValue = "3937d55b";        // Bytes used for XOR in address checksum calculation.

        // The following parameters were generated by multichaind.
        // They SHOULD NOT BE EDITED. 
        public string GenesisPubkey = null;                     // Genesis block coinbase output public key.
        public string GenesisVersion = null;                    // Genesis block version.
        public string GenesisTimestamp = null;                  // Genesis block timestamp.
        public string GenesisNbits = null;                      // Genesis block difficulty (nBits).
        public string GenesisNonce = null;                      // Genesis block nonce.
        public string GenesisPubkeyHash = null;                 // Genesis block coinbase output public key hash.
        public string GenesisHash = null;                       // Genesis block hash.
        public string ChainParamsHash = null;                   // Hash of blockchain parameters, to prevent accidental changes.
    }
}
