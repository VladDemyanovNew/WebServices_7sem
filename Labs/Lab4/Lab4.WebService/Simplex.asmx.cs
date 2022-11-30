using System.Web.Script.Services;
using System.Web.Services;
using System.IO;

using Newtonsoft.Json;

using Lab4.WebService.Models;

namespace Lab4.WebService
{
    /// <summary>
    /// Performs simple operations.
    /// </summary>
    [ScriptService]
    [WebService(Namespace = "http://dvr/", Description = "Performs simple operations.")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class Simplex : System.Web.Services.WebService
    {

        [WebMethod(Description = "Returns sum.", MessageName = "Simplex.Add")]
        public int Add(int x, int y) => x + y;

        [ScriptMethod(ResponseFormat=ResponseFormat.Json)]
        [WebMethod(Description = "Returns sum.", MessageName = "Simplex.Adds")]
        public string Adds(int x, int y) => JsonConvert.SerializeObject(x + y);

        [WebMethod(Description = "Returns concatenation.", MessageName = "Simplex.Concat")]
        public string Concat(string s, double d) => s + d;

        [WebMethod(Description = "Returns instance of class A.", MessageName = "Simplex.Sum")]
        public A Sum(A a1, A a2)
        {
            var str = this.Context.Request.InputStream;
            str.Position = 0;

            var streamReader = new StreamReader(str);
            var body = streamReader.ReadToEnd();
            streamReader.Close();

            return new A
            {
                S = a1.S + a2.S,
                K = a1.K + a2.K,
                F = a1.F + a2.F,
            };
        }
            
    }
}
