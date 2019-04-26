using All_Sorts.Controllers.Facebook;
using All_Sorts.Controllers.Linkedin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tweetinvi;

namespace All_Sorts.Controllers.Strategy
{
    public interface ISimpleTextInteface
    {
        Task PostTextAsyncFB(string userPost);
        Task PostTextAsyncTweet(string userPost);
        Task PostTextAsyncIn(string userPost);
    }

    public static class LinkedinSettings
    {
        public static string url = "https://localhost:44347/Home";

        public static string accessToken = "AQUZubp9vSB5Ekf7wNOAwtXL1tqar9STKYTC7nLXDPLij4qFW3q5JU429AW7wvfO4tXXLA_Mthr6_-lEKjTWUB4W5yW3ie05q" +
            "_WAmbpED4Jnuujk67Lz4tHSD_UpOukTG6lYZE9Px-QgdLEQ-Cnj8ENRXiDd_n4JBxPlcs1IxEkg-aEtjpOz1BKVcLFEHKosMUkXULZVmINA7q8RHPvYuCnnRTHHoxWv" +
            "-rwtIWmM38XB290Wc4vkr_yOjTmJNIGu05bzbhaI88mt4_sdVI2kzsuZCg4wYhA49Sa1krlk0TWmK5TXlcjfsHKpvS3mR0RmUv9QeLFE8CTPRqYhM8D6FDZ3p4wEXQ";
    }
    public static class FacebookSettings
    {
        public static string pageID = "177502029051938";

        public static string Access_Token = "EAAEAQCTS8w4BAERTIcBmpYyMRPQNnLCybBcoR5vlFhfVmIiIQD70tjNCGUqC0KW1ghgZBZBI3H5ufHXZBwZBsb6AKEeSTeaZAk5cZBkg5PJ" +
                                            "ZCoZAn6AwJWxfLuIio31Ylsn4tynhoyKnMdVvZB3UvEWmjZCiYu7feGU32QrfQtQXTqCQZDZD";

        //public static string Access_Token = "EAAEAQCTS8w4BABnylvUl3T0kAHBJdZCgn8tFTZCquk9VwAFWz0c6uDT97Wt3ePzHdyC8wlcWiNoEs6bQZCLs6000GLPXMI11O1ZAs1TTnZAYEyyndfw82H1W5O311zoLCprlZBydilCZCWNPnAaiQUQCuK58xQQ6cUrALsh1jZCsJQZDZD";
    }
    public static class TwitterSettings
    {
        public static string ConsumerKey = "qllUThirtgYjSoCKdvArwwKKR",
                            ConsumerKeySecret = "GPiVBJ3NQ2o3yTgAKmllgfZVIcCqcNCje8HLZNflBP3MSnFU1h",
                            AccessToken = "534605601-4rK4YOQxBJXCz8viVAliAQQ5c8pGLWkKEz7WkZ9Y",
                            AccessTokenSecret = "H44h2jNrtEi6IvIeYkZdBmNWIuoyZrKMQG9Q9lSKHPQpn";
    }

    public class SimpleTextPost : ISimpleTextInteface
    {
        public async Task PostTextAsyncFB(string userPost)
        {
            var facebook = new FacebookController(FacebookSettings.Access_Token,FacebookSettings.pageID);
            await facebook.PublishSimplePost(userPost);
        }

        public async Task PostTextAsyncIn(string userPost)
        {
            var linkedin = new InController();
            linkedin.PostToLinkedinAsync(userPost, LinkedinSettings.accessToken);
        }

        public async Task PostTextAsyncTweet(string userPost)
        {
            var tweet = Tweet.PublishTweet(userPost);
        }
    }
}
