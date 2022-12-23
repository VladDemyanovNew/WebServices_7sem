using System;

namespace Lab8.JsonRpc.Exceptions
{
    public class JsonRpcException : Exception
    {
        public JsonRpcException()
        {
        }

        public string RequestId { get; }

        public string JsonRpc { get; }

        public int Code { get; }

        public JsonRpcException(string requestId, string jsonRpc, string message, int code) : base(message)
        {
            this.RequestId = requestId;
            this.JsonRpc = jsonRpc;
            this.Code = code;
        }
    }
}