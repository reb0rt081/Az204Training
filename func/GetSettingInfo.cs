using System.IO;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

public static class GetSettingInfo
{
    [FunctionName("GetSettingInfo")]
    public static IActionResult Run(
        [HttpTrigger("GET")] HttpRequest request,
        [Blob("content/settings.json")] string json)
        => new OkObjectResult(json);

    //[FunctionName("GetSettingInfo")]
    //public static async Task<IActionResult> Run(
    //    [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
    //    ILogger log)
    //{
    //    log.LogInformation("C# HTTP trigger function processed a request.");
    //    string name = req.Query["name"];
    //    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
    //    dynamic data = JsonConvert.DeserializeObject(requestBody);
    //    name = name ?? data?.name;
    //    string responseMessage = string.IsNullOrEmpty(name)
    //        ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
    //        : $"Hello, {name}. This HTTP triggered function executed successfully.";
    //    return new OkObjectResult(responseMessage);
    //}
}