// See https://aka.ms/new-console-template for more information

using Az204.Queue.Sender;

Console.WriteLine("Runing application senderd!");

QueueSenderApplication senderApplication = new QueueSenderApplication();

await senderApplication.RunQueue("testqueue");


