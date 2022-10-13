// See https://aka.ms/new-console-template for more information

using Az204.ServiceBus.Reader;

Console.WriteLine("Service Bus Publisher!");

ServiceBusReaderApplication application = new ServiceBusReaderApplication();

await application.RunQueue("messagequeue", 1);
