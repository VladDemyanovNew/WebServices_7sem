using System;
using System.ServiceModel;

using Lab7.SyndicationServiceLibrary;

namespace Lab7.SyndicationHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var host = new ServiceHost(typeof(WsDvrFeed));
                host.Open();
                Console.WriteLine("Host has been opened successfully!");
                Console.ReadKey();
            }
            catch(Exception error)
            {
                Console.WriteLine($"Error when opening host: {error}");
            }
        }
    }
}
