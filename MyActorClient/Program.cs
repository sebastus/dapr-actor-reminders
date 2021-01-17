using Dapr.Actors;
using Dapr.Actors.Client;
using MyActor.Interfaces;
using System;
using System.Threading.Tasks;

namespace MyActorClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            for (int i = 0; i < 10; i++)
            {
                await InvokeActorMethodWithRemotingAsync(i);
            }

            while (!Console.KeyAvailable)
            {
                System.Threading.Thread.Sleep(10);
            }
        }
        static async Task InvokeActorMethodWithRemotingAsync(int actorId)
        {
            var actorType = "MyActor";      // Registered Actor Type in Actor Service
            var actorID = new ActorId(actorId.ToString());

            // Create the local proxy by using the same interface that the service implements
            // By using this proxy, you can call strongly typed methods on the interface using Remoting.
            var proxy = ActorProxy.Create<IMyActor>(actorID, actorType);
            var response = await proxy.SetDataAsync(new MyData()
            {
                PropertyA = "ValueA",
                PropertyB = "ValueB",
            });
            Console.WriteLine(response);

            await proxy.RegisterReminder("MyFirstReminder");

            var savedData = await proxy.GetDataAsync();
            Console.WriteLine(savedData);
        }
    }
}
