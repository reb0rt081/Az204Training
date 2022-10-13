using System.Globalization;
using Azure;
using Azure.Messaging.ServiceBus;

namespace Az204.ServiceBus.Reader
{
    public class ServiceBusReaderApplication
    {
        public async Task RunQueue(string queueName, int runningTimeInMinutes)
        {
            string connectionString =
                "Endpoint=sb://sbnamespaceroberto.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=FNd+WzIf5gcw/bW1YcOay7pzzfzRu26gcZ7TRy6B97Q=";

            ServiceBusClient client = new ServiceBusClient(connectionString);
            ServiceBusProcessor? processor = client.CreateProcessor(queueName);

            processor.ProcessMessageAsync += ProcessorOnProcessMessageAsync;
            processor.ProcessErrorAsync += ProcessorOnProcessErrorAsync;

            await processor.StartProcessingAsync();

            Console.WriteLine($"Wait for {runningTimeInMinutes} minutes");

            int i = 0;
            while (i < runningTimeInMinutes)
            {
                await Task.Delay(TimeSpan.FromSeconds(30));
                i++;
            }
            
            await processor.StopProcessingAsync();
        }

        private Task ProcessorOnProcessErrorAsync(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }

        private async Task ProcessorOnProcessMessageAsync(ProcessMessageEventArgs args)
        {
            string body = args.Message.Body.ToString();
            Console.WriteLine($"Received: {body}");
            await args.CompleteMessageAsync(args.Message);
        }
    }
}
