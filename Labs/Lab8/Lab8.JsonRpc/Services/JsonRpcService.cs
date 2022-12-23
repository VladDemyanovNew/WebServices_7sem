using System.Web;

using Lab8.JsonRpc.Constants;
using Lab8.JsonRpc.Exceptions;
using Lab8.JsonRpc.Models;

namespace Lab8.JsonRpc.Services
{
    public class JsonRpcService
    {
        public int SetM(JsonRpcRequest jsonRpcRequest)
        {
            ValidateParamsCount(2, jsonRpcRequest);
            ValidateNumber(jsonRpcRequest, out int x);

            string k = jsonRpcRequest.Params[0].ToString();
            HttpContext.Current.Session[k] = x;

            return x;
        }

        public int GetM(JsonRpcRequest jsonRpcRequest)
        {
            ValidateParamsCount(1, jsonRpcRequest);
            ValidateKey(jsonRpcRequest, out int x);

            return x;
        }
            
        public int AddM(JsonRpcRequest jsonRpcRequest)
        {
            ValidateParamsCount(2, jsonRpcRequest);
            ValidateKey(jsonRpcRequest, out int currentX);
            ValidateNumber(jsonRpcRequest, out int x);

            int newX = currentX + x;
            string k = jsonRpcRequest.Params[0].ToString();
            HttpContext.Current.Session[k] = newX;

            return newX;
        }

        public int SubM(JsonRpcRequest jsonRpcRequest)
        {
            ValidateParamsCount(2, jsonRpcRequest);
            ValidateKey(jsonRpcRequest, out int currentX);
            ValidateNumber(jsonRpcRequest, out int x);

            int newX = currentX - x;
            string k = jsonRpcRequest.Params[0].ToString();
            HttpContext.Current.Session[k] = newX;

            return newX;
        }

        public int MulM(JsonRpcRequest jsonRpcRequest)
        {
            ValidateParamsCount(2, jsonRpcRequest);
            ValidateKey(jsonRpcRequest, out int currentX);
            ValidateNumber(jsonRpcRequest, out int x);

            int newX = currentX * x;
            string k = jsonRpcRequest.Params[0].ToString();
            HttpContext.Current.Session[k] = newX;

            return newX;
        }

        public int DivM(JsonRpcRequest jsonRpcRequest)
        {
            ValidateParamsCount(2, jsonRpcRequest);
            ValidateKey(jsonRpcRequest, out int currentX);
            ValidateNumber(jsonRpcRequest, out int x);

            if (x == 0)
            {
                throw new JsonRpcException(
                    jsonRpcRequest.Id,
                    jsonRpcRequest.Method, 
                    "Divide by zero exception.",
                    JsonRpcErrorCodes.InvalidParams);
            }

            int newX = currentX / x;
            string k = jsonRpcRequest.Params[0].ToString();
            HttpContext.Current.Session[k] = newX;

            return newX;
        }

        public void ErrorExit()
        {
            HttpContext.Current.Session[JsonRpcSessionKeys.IgnoreCallsAfterError] = true;
        }

        private static void ValidateParamsCount(int validCount, JsonRpcRequest jsonRpcRequest)
        {
            if (jsonRpcRequest.Params.Length < validCount)
            {
                throw new JsonRpcException(
                    jsonRpcRequest.Id,
                    jsonRpcRequest.Method, 
                    "Invalid count of params.",
                    JsonRpcErrorCodes.InvalidParams);
            }
        }

        private static void ValidateNumber(JsonRpcRequest jsonRpcRequest, out int x)
        {
            bool isInteger = int.TryParse(jsonRpcRequest.Params[1].ToString(), out x);
            if (!isInteger)
            {
                throw new JsonRpcException(
                    jsonRpcRequest.Id,
                    jsonRpcRequest.Method, 
                    "The second parameter must be a number.",
                    JsonRpcErrorCodes.InvalidParams);
            }
        }

        private static void ValidateKey(JsonRpcRequest jsonRpcRequest, out int currentX)
        {
            string k = jsonRpcRequest.Params[0].ToString();
            int? value = HttpContext.Current.Session[k] as int?;
            if (value == null)
            {
                throw new JsonRpcException(
                    jsonRpcRequest.Id,
                    jsonRpcRequest.Method, 
                    $"The value with key {k} isn't found.",
                    JsonRpcErrorCodes.InvalidParams);
            }

            currentX = value.Value;
        }
    }
}