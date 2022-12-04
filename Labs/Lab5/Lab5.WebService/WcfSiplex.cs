using System;

namespace Lab5.WebService
{
    public class WcfSiplex : IWcfSiplex
    {
        public int Add(int x, int y) => x + y;

        public string Concat(string s, double d) => s + d;

        public A Sum(A a1, A a2) => new A
        {
            S = a1.S + a2.S,
            K = a1.K + a2.K,
            F = a1.F + a2.F,
        };
    }
}
