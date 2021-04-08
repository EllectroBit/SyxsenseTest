using System.IO;
using System.Threading.Tasks;
using DataModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using LinqToDB;
using System.Linq;

namespace AzureFunctions
{
    public static class DataBaseFunction
    {
        [FunctionName("DataBaseFunction")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            
            string json = req.Query["json"];
            User user = JsonConvert.DeserializeObject<User>(json);

            log.LogInformation("Hello, {0}!", user.Name);

            using (var db = new TestDB())
            {
                User res = db.Users.Where(u => u.Id == user.Id).FirstOrDefault();
                if(res != null && (res.Name != user.Name || res.Age != user.Age))
                {
                    db.Users.Where(u => u.Id == user.Id).Set(u => u.Name, user.Name).Set(u => u.Age, user.Age).Update();
                }
                else if(res == null)
                {
                    db.Users.Insert(() => new User() { Name = user.Name, Age = user.Age });
                }
            }

            return new OkResult();
        }
    }
}

