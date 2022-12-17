using System.Web.Script.Services;
using System.Xml.Serialization;

using Newtonsoft.Json;

using Lab4.WebClient.ServerClass;

namespace Lab4.WebClient
{
    [ScriptService]
    public class SimplexService : Simplex
    {
        [return: XmlElement("Simplex.AddResult")]
        public override int Add(int x, int y) => x + y;

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [return: XmlElement("Simplex.AddsResult")]
        public override string Adds(int x, int y) => JsonConvert.SerializeObject(x + y);

        [return: XmlElement("Simplex.ConcatResult")]
        public override string Concat(string s, double d) => s + d;

        [return: XmlElement("Simplex.SumResult")]
        public override A Sum(A a1, A a2) => new A
        {
            S = a1.S + a2.S,
            K = a1.K + a2.K,
            F = a1.F + a2.F,
        };
    }
}