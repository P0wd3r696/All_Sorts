using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using All_Sorts.Controllers.Strategy;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace All_Sorts.Controllers.Linkedin
{
    public class InController : Controller
    {
        private string linkedinSharesEndpoint = "https://api.linkedin.com/v1/companies/18788968/shares?oauth2_access_token={0}";
        private const string defaultUrl = "https://localhost:447347/home";
        private const string defaultImageUrl = "https://localhost:447347/tomyimages";
        private const string _accessToken = "";

        public bool PostToLinkedinAsync(string userText, string accessToken, string title = "Posted from All-Sortz App", string submittedUrl = defaultUrl, string submittedImageUrl= defaultImageUrl)
        {
            var requestUrl = String.Format(linkedinSharesEndpoint, accessToken);

            var message = new
            {
                comment = userText,
                content = new Dictionary<string, string>
                {
                    {"title", title },
                    {"submitted-url", submittedUrl },
                    {"submitted-image-url", submittedImageUrl }
                },
                visibility = new
                {
                    code = "anyone"
                }
            };
            var requestJson = JsonConvert.SerializeObject(message);

            var client = new WebClient();

            var requestHeaders = new NameValueCollection
            {
                {"Content-Type", "application/json" },
                {"x-li-format", "json" }
            };

            client.Headers.Add(requestHeaders);

            var responseJson = client.UploadString(requestUrl, "POST", requestJson);

            var response = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseJson);

            return response.ContainsValue("UpdateKey");
        }
    }
}