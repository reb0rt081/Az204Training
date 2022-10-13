

using System.Globalization;

using Azure;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;

namespace Az204.Queue.Receiver
{
    public class QueueReceiverApplication
    {

        public async Task RunQueue(string queueName)
        {
            string connectionString =
                "DefaultEndpointsProtocol=https;AccountName=2robertofunctionstorage;AccountKey=wONqNTBqT2vT6ZDOWHv6PWX2f3yiviy8q8ooLmfgCQsb4vX0ISKvGggjg28ddvrhoNYEK0dDufuZ+AStoBxQtA==;EndpointSuffix=core.windows.net";

            QueueClient queueClient = new QueueClient(connectionString, queueName);

            await queueClient.CreateIfNotExistsAsync();

            while (true)
            {
                Response<QueueMessage>? response = null;

                if (queueClient.Exists())
                {
                    //  Los 60 segundos son el tiempo que tenemos para procesar y borrar el mensaje
                    response = await queueClient.ReceiveMessageAsync(TimeSpan.FromSeconds(60));
                }

                if (response == null)
                {
                    Console.WriteLine("No more messages in queue ");
                }
                else
                {
                    Console.WriteLine("Received - " + response.Value?.MessageText);
                }
                

                Thread.Sleep(TimeSpan.FromSeconds(30));

                if (response != null)
                {
                    //  Aquí borramos el mensaje de la cola
                    await queueClient.DeleteMessageAsync(response.Value.MessageId, response.Value.PopReceipt);
                }

            }
        }
    }
}
