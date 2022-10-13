// See https://aka.ms/new-console-template for more information

using Az204.Queue.Receiver;

Console.WriteLine("Runing application Receiver!");

QueueReceiverApplication senderApplication = new QueueReceiverApplication();

await senderApplication.RunQueue("testqueue");


