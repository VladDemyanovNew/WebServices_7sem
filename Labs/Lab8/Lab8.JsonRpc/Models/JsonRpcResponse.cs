namespace Lab8.JsonRpc.Models
{
    public class JsonRpcResponse
    {
        public string Id { get; set; }

        public string JsonRpc { get; set; }

        public object Result { get; set; }
    }
}