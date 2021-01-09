using System;

using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Net.Client;
using System.Runtime.InteropServices;

namespace GrpcAuthorClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serverAddress = "https://localhost:5001";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) {
                // The following statement allows you to call insecure services. To be used only in development environments.
                AppContext.SetSwitch(
                    "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
                serverAddress = "http://localhost:5000";
            }
            
            using var channel = GrpcChannel.ForAddress(serverAddress);
            var client =  new Greeter.GreeterClient(channel);
            var reply = await client.SayHelloAsync(
                              new HelloRequest { Name = "GreeterClient from Peter" });
            Console.WriteLine("Greeting: " + reply.Message);

            var client2 =  new Author.AuthorClient(channel);
            var reply2 = await client2.GetAuthorAsync(
                              new AuthorRequest { Name = "Antonio Gonzalez" });
            Console.WriteLine("Author: " + reply2.ToString());

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
