using System.Collections.Generic;
using System.Web;
using System.Web.Http;

using Lab8.JsonRpc.Constants;
using Lab8.JsonRpc.Exceptions;
using Lab8.JsonRpc.Models;
using Lab8.JsonRpc.Services;

namespace Lab8.JsonRpc.Controllers
{
    public class JsonRpcController : ApiController
    {
        private readonly JsonRpcService _jsonRpcService;

        public JsonRpcController()
        {
            this._jsonRpcService = new JsonRpcService();
        }

        [HttpPost]
        public object[] ProcessPackage([FromBody] JsonRpcRequest[] jsonRpcPackage)
        {
            var result = new List<object>();
            foreach (var jsonRpcRequest in jsonRpcPackage)
            {
                object jsonRpcResponse = ProcessRequest(jsonRpcRequest);

                bool? ignoreCallsAfterError = HttpContext.Current.Session[JsonRpcSessionKeys.IgnoreCallsAfterError] as bool?;
                if (ignoreCallsAfterError.HasValue && ignoreCallsAfterError.Value && jsonRpcResponse is JsonRpcError)
                {
                    result.Add(jsonRpcResponse);
                    HttpContext.Current.Session.Clear();
                    return result.ToArray();
                }

                result.Add(jsonRpcResponse);
            }

            return result.ToArray();
        }

        private object ProcessRequest(JsonRpcRequest jsonRpcRequest)
        {
            try
            {
                object result = 0;
                var method = jsonRpcRequest.Method.ToLower();
                switch (method)
                {
                    case "setm":
                        result = this._jsonRpcService.SetM(jsonRpcRequest);
                        break;
                    case "getm":
                        result = this._jsonRpcService.GetM(jsonRpcRequest);
                        break;
                    case "addm":
                        result = this._jsonRpcService.AddM(jsonRpcRequest);
                        break;
                    case "subm":
                        result = this._jsonRpcService.SubM(jsonRpcRequest);
                        break;
                    case "mulm":
                        result = this._jsonRpcService.MulM(jsonRpcRequest);
                        break;
                    case "divm":
                        result = this._jsonRpcService.DivM(jsonRpcRequest);
                        break;
                    case "errorexit":
                        this._jsonRpcService.ErrorExit();
                        result = "enabled";
                        break;
                    default:
                        throw new JsonRpcException(
                            jsonRpcRequest.Id,
                            jsonRpcRequest.JsonRpc,
                            "Invalid count of params.",
                            JsonRpcErrorCodes.MethodNotFound);
                }

                return new JsonRpcResponse
                {
                    Id = jsonRpcRequest.Id,
                    JsonRpc = jsonRpcRequest.JsonRpc,
                    Result = result,
                };
            }
            catch (JsonRpcException exception)
            {
                return new JsonRpcError
                {
                    Id = exception.RequestId,
                    JsonRpc = exception.JsonRpc,
                    Error = new
                    {
                        Message = exception.Message,
                        Code = exception.Code,
                    },
                };
            }
        }
    }
}
