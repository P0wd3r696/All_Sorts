using All_Sorts.Controllers.Facebook;
using All_Sorts.Controllers.Linkedin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Parameters;

namespace All_Sorts.Controllers.Strategy
{
    public interface ISimpleImageInterface
    {
        Task PostImageAsyncFB(string userPost, string userImage);
        Task PostImageAsyncTweet(string userPost, string userImage);
        Task PostImageAsyncIn(string userPost, string userImage);
    }
    
    public class SimpleImagePost : ISimpleImageInterface
    {
        public async Task PostImageAsyncFB(string userPost, string userImageUrl)
        {
            var facebook = new FacebookController(FacebookSettings.Access_Token,FacebookSettings.pageID);

            facebook.PublishToFacebook(userPost, userImageUrl);
        }
        public async Task PostImageAsyncIn(string userPost, string userImageUrl)
        {
            var linkedin = new InController();

            linkedin.PostToLinkedinAsync(userPost, LinkedinSettings.accessToken);
        }
        public async Task PostImageAsyncTweet(string userPost, string userImageUrl)
        {
            var image = System.IO.File.ReadAllBytes(userImageUrl);
            var media = Upload.UploadBinary(image);

            var tweet = Tweet.PublishTweet(userPost, new PublishTweetOptionalParameters
            {
                Medias = { media }
            });
        }
    }
}