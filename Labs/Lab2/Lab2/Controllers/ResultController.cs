using Lab2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Lab2.Controllers
{
    public class ResultController : ApiController
    {
        public Result Get()
        {
            var peek = GlobalVariables.Stack.Count > 0 ? GlobalVariables.Stack.Peek() : 0;

            return new Result
            {
                Value = GlobalVariables.Result + peek,
            };
        }

        public Result Post(int value)
        {
            GlobalVariables.Result = value;
            var peek = GlobalVariables.Stack.Count > 0 ? GlobalVariables.Stack.Peek() : 0;

            return new Result {
                Value = GlobalVariables.Result + peek,
            };
        }

        public void Put(int add)
        {
            GlobalVariables.Stack.Push(add);
        }

        public void Delete()
        {
            if (GlobalVariables.Stack.Count == 0)
            {
                throw new InvalidOperationException("Can't perform stack pop, because it is empty");
            }

            GlobalVariables.Stack.Pop();
        }
    }
}
