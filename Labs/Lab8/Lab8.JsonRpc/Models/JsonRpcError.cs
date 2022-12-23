namespace Lab8.JsonRpc.Models
{
    public class JsonRpcError
    {
        public string Id { get; set; }

        public string JsonRpc { get; set; }

        public object Error { get; set; }
    }
}