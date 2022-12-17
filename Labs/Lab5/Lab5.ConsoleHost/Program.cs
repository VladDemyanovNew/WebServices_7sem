using System;
using System.ServiceModel;

namespace Lab5.ConsoleHost
{
    public static class Program
    {
        private static ServiceHost host = null;

        public static void Main(string[] args)
        {
            try
            {
                host = new ServiceHost(typeof(WebService.WcfSiplex));
                //var binding = new NetTcpBinding();
                var binding = new BasicHttpBinding(BasicHttpSecurityMode.None)
                {
                    AllowCookies = true,
                    SendTimeout = TimeSpan.FromMinutes(10),
                };

                // new Uri("net.tcp://localhost:5000/WcfSiplex"));
                _ = host.AddServiceEndpoint(
                    typeof(WebService.IWcfSiplex),
                    binding,
                    "http://localhost/WcfSiplex");

                while (true)
                {
                    Console.WriteLine("Actions:");
                    Console.WriteLine("1. Open");
                    Console.WriteLine("2. Close");

                    Console.Write("Action: ");
                    string action = Console.ReadLine();
                    switch (action)
                    {
                        case "1":
                            Open();
                            break;
                        case "2":
                            Close();
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
                Console.WriteLine(exception.ToString());
                Console.ForegroundColor = ConsoleColor.White;
            }
            finally
            {
                Close();
            }
        }

        private static void Open()
        {
            if (host.State == CommunicationState.Opened)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Can't open service, because it has been already started!");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            host.Open();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Service has been started successfully!");
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void Close()
        {
            host.Close();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Service has been stoped.");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
