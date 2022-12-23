namespace Lab8.JsonRpc.Models
{
    public class JsonRpcRequest
    {
        public string Id { get; set; }

        public string JsonRpc { get; set; }

        public string Method { get; set; }

        public object[] Params { get; set; }
    }
}