using System.Globalization;
using Azure;
using Azure.Messaging.ServiceBus;

namespace Az204.ServiceBus.Publisher
{
    public class ServiceBusPublisherApplication
    {
        public async Task RunQueue(string queueName, int numberOfMessages)
        {
            string connectionString =
                "Endpoint=sb://sbnamespaceroberto.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=FNd+WzIf5gcw/bW1YcOay7pzzfzRu26gcZ7TRy6B97Q=";

            ServiceBusClient client = new ServiceBusClient(connectionString);
            ServiceBusSender? sender = client.CreateSender(queueName);

            using ServiceBusMessageBatch messageBatch = await sender.CreateMessageBatchAsync();
            {
                try
                {
                    while (true)
                    {
                        for (int i = 1; i <= numberOfMessages; i++)
                        {
                            if (!messageBatch.TryAddMessage(new ServiceBusMessage($"Message {i} at {DateTime.Now.ToString("yyyy-MM-dd-HH:mm:ss", CultureInfo.InvariantCulture)}")))
                            {
                                throw new Exception($"The message {i} is too large to fit in the batch.");
                            }
                        }


                        await sender.SendMessagesAsync(messageBatch);
                        Console.WriteLine($"A batch of {numberOfMessages} messages has been published to the queue.");



                        Console.WriteLine($"Send {numberOfMessages} messages at {DateTime.Now.ToString("yyyy-MM-dd-HH:mm:ss", CultureInfo.InvariantCulture)}");

                        Thread.Sleep(TimeSpan.FromSeconds(30));

                    }
                }
                finally
                {
                    await sender.DisposeAsync();
                    await client.DisposeAsync();
                }
            }
        }
    }
}
