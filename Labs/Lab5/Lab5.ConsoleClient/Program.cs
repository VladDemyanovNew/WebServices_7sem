using System;
using System.ServiceModel;

namespace Lab5.ConsoleClient
{
    public static class Program
    {
        private static WebService.IWcfSiplex idd = null;

        public static void Main(string[] args)
        {
            Console.WriteLine("Press any key to start client.");
            Console.ReadKey(true);

            try
            {
                // var binding = new NetTcpBinding();
                // var address = new EndpointAddress("net.tcp://localhost:5000/WcfSiplex");
                var binding = new BasicHttpBinding();
                var address = new EndpointAddress("http://localhost/WcfSiplex");
                var channel = new ChannelFactory<WebService.IWcfSiplex>(binding, address);

                idd = channel.CreateChannel();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Client has been started successfully!");
                Console.ForegroundColor = ConsoleColor.White;
                
                while (true)
                {
                    Console.WriteLine("Actions:");
                    Console.WriteLine("1. Add");
                    Console.WriteLine("2. Concat");
                    Console.WriteLine("3. Sum");
                    Console.WriteLine("4. Exit");

                    Console.Write("Action: ");
                    string action = Console.ReadLine();
                    switch (action)
                    {
                        case "1":
                            Add();
                            break;
                        case "2":
                            Concat();
                            break;
                        case "3":
                            break;
                        case "4":
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine();
                            continue;
                    }
                    Console.WriteLine();
                }
            }
            catch (Exception exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(exception.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        private static void Add()
        {
            Console.Write("x = ");
            string x = Console.ReadLine();

            Console.Write("y = ");
            string y = Console.ReadLine();

            int result = idd.Add(Convert.ToInt32(x), Convert.ToInt32(y));
            Console.WriteLine($"result = {result}");
        }

        private static void Concat()
        {
            Console.Write("s = ");
            string s = Console.ReadLine();

            Console.Write("d = ");
            string d = Console.ReadLine();

            string result = idd.Concat(s, Convert.ToDouble(d));
            Console.WriteLine($"result = {result}");
        }
    }
}
