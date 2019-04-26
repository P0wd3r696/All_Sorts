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
        private const string _accessToken = "AQUZubp9vSB5Ekf7wNOAwtXL1tqar9STKYTC7nLXDPLij4qFW3q5JU429AW7wvfO4tXXLA_Mthr6_" +
            "-lEKjTWUB4W5yW3ie05q_WAmbpED4Jnuujk67Lz4tHSD_UpOukTG6lYZE9Px-QgdLEQ-Cnj8ENRXiDd_n4JBxPlcs1IxEkg-aEtjpOz1BKVcLFEHKos" +
            "MUkXULZVmINA7q8RHPvYuCnnRTHHoxWv-rwtIWmM38XB290Wc4vkr_yOjTmJNIGu05bzbhaI88mt4_sdVI2kzsuZCg4wYhA49Sa1krlk0TWmK5TXlcjfs" +
            "HKpvS3mR0RmUv9QeLFE8CTPRqYhM8D6FDZ3p4wEXQ";

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