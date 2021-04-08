using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services;

namespace WebServices
{

    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.Web.Script.Services.ScriptService]

    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        public InformationModel SendInformationModelJSON(string json)
        {
            return JsonConvert.DeserializeObject<InformationModel>(json);
        }

        [WebMethod]
        public void SendOnlineStatus()
        {
            new HttpClient().GetAsync("http://localhost:7071/api/CheckOnlineFunction");
        }

        [WebMethod]
        public void SendRequestToDB(string json)
        {
            new HttpClient().GetAsync("http://localhost:7071/api/DataBaseFunction?json=" + json);
        }
    }

    public class InformationModel
    {
        public string ComputerName { get; set; }
        public string TimeZone { get; set; }
        public string OS_Name { get; set; }
        public string NetVersion { get; set; }
    }
}
