namespace StoneyCreek.Services.Blockchain.DataContracts
{
    public class GrantPermisionsData
    {
        public string Address { get; set; }

        public GrantPermissions[] Permissions { get; set; }

        public string Comment { get; set; }

        public string CommentTo { get; set; }

        public string NativeAmount { get; set; }
    }
}
