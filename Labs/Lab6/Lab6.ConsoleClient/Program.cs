using System;

using WsDvrModel;

namespace Lab6.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var uri = new Uri("http://localhost:53215/Services/WsDvr.svc");
            var wsDvr = new WsDvrEntities(uri);

            foreach (var student in wsDvr.Students)
            {
                Console.WriteLine(student.Id + " " + student.Name);
            }
        }
    }
}
