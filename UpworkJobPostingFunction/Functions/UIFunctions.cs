using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Jobson.Functions
{
    public static class UIFunctions
    {
        [FunctionName("UiFunctions")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "{controller}/{action}")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            var controller = new MyController();
            return controller.MyAction();
        }

        [FunctionName("UiFunctionsWithId")]
        public static IActionResult RunWithId(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "{controller}/{action}/{id?}")] HttpRequest req, string id,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request WITH id.");
            var controller = new MyController();
            if (Int32.TryParse(id, out int result))
            {
                return controller.MyActionWithId(result);
            }
            
            return controller.NotFound(id);
        }
    }
}

public class MyController : ControllerBase
{
    [HttpGet]
    [Route("my-action")]
    public IActionResult MyAction()
    {
        // Controller action logic
        return Ok("Hello from MyAction!");
    }

    [HttpGet]
    [Route("my-action/{id}")]
    public IActionResult MyActionWithId(int id)
    {
        // Controller action logic with id parameter
        return Ok($"Hello from MyActionWithId! ID: {id}");
    }

    [HttpGet]
    [Route("my-action/notfound")]
    public IActionResult MyActionWithIdNotFound(string id)
    {
        // Controller action logic with id parameter
        return NotFound($"Hello from MyActionWithId! ID: {id} is not found.");
    }
}