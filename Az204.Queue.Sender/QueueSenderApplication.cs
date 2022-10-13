

using System.Globalization;

using Azure.Storage.Queues;

namespace Az204.Queue.Sender
{
    public class QueueSenderApplication
    {

        public async Task RunQueue(string queueName)
        {
            string connectionString =
                "DefaultEndpointsProtocol=https;AccountName=2robertofunctionstorage;AccountKey=wONqNTBqT2vT6ZDOWHv6PWX2f3yiviy8q8ooLmfgCQsb4vX0ISKvGggjg28ddvrhoNYEK0dDufuZ+AStoBxQtA==;EndpointSuffix=core.windows.net";

            QueueClient queueClient = new QueueClient(connectionString, queueName);
            
            await queueClient.CreateIfNotExistsAsync();

            while (true)
            {
                string message = $"Send message at {DateTime.Now.ToString("yyyy-MM-dd-HH:mm:ss", CultureInfo.InvariantCulture)}";
                
                if (queueClient.Exists())
                {

                    await queueClient.SendMessageAsync(Base64Encode(message));
                }

                Console.WriteLine(message);

                Thread.Sleep(TimeSpan.FromSeconds(30));
            }
        }

        private static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}
