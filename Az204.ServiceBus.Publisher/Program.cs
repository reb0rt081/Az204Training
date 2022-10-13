// See https://aka.ms/new-console-template for more information

using Az204.ServiceBus.Publisher;

Console.WriteLine("Service Bus Publisher!");

ServiceBusPublisherApplication application = new ServiceBusPublisherApplication();

await application.RunQueue("messagequeue", 3);
