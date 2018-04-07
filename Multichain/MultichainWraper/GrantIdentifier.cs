namespace Stoneycreek.libraries.MultichainWrapper
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
}
