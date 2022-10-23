using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab2
{
    public static class GlobalVariables
    {
        public static int Result { get; set; }

        public static Stack<int> Stack { get; set; } = new Stack<int>();
    }
}