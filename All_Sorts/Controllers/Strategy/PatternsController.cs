using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using All_Sorts.Data;
using All_Sorts.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Tweetinvi;
using Tweetinvi.Parameters;

namespace All_Sorts.Controllers.Strategy
{
    public class PatternsController : Controller
    {
        public async void PostDecider(string userPost,string userImageUrl,string webRootPath, bool toFacebook, bool toLinkedin, bool toTwitter)
        {
            var simpleText = new SimpleTextPost();
            var simpleImage = new SimpleImagePost();
            
            if(userImageUrl == null)
            {
                //post the caption/status to sites
                if (toFacebook && toLinkedin && toTwitter)
                {
                    await simpleText.PostTextAsyncFB(userPost);
                    await simpleText.PostTextAsyncIn(userPost);
                    await simpleText.PostTextAsyncTweet(userPost);
                }
                else if (toFacebook && !toLinkedin && !toTwitter)
                {
                    await simpleText.PostTextAsyncFB(userPost);
                }
                else if (toFacebook && toLinkedin && !toTwitter)
                {
                    await simpleText.PostTextAsyncFB(userPost);
                    await simpleText.PostTextAsyncIn(userPost);
                }
                else if (!toFacebook && !toLinkedin && toTwitter)
                {
                    await simpleText.PostTextAsyncTweet(userPost);
                }
                else if (!toFacebook && toLinkedin && toTwitter)
                {
                    await simpleText.PostTextAsyncIn(userPost);
                    await simpleText.PostTextAsyncTweet(userPost);
                }
                else if (!toFacebook && toLinkedin && !toTwitter)
                {
                    await simpleText.PostTextAsyncIn(userPost);
                }
            }
            else
            {
                //upload image + caption to social media
                if (toFacebook && toLinkedin && toTwitter)
                {
                    await simpleImage.PostImageAsyncFB("1", "2");
                    await simpleImage.PostImageAsyncIn("1", "2");
                    await simpleImage.PostImageAsyncTweet("1", "2");
                }
                else if (toFacebook && !toLinkedin && !toTwitter)
                {
                    string httpPath;

                    var unknown = webRootPath + userImageUrl;

                    httpPath = webRootPath.Replace(webRootPath, @"https://localhost:44305/images/1050.png").Replace(@"\", "/");

                    await simpleImage.PostImageAsyncFB("", "");
                }
                else if (toFacebook && toLinkedin && !toTwitter)
                {
                    await simpleImage.PostImageAsyncIn("", "");

                    //string httpPath;
                    //var unknown = webRootPath + UserImage;
                    //httpPath = webRootPath.Replace(webRootPath, @"https://localhost:44305/images/1050.png").Replace(@"\", "/");

                    ///lets try it this way
                    ///inherit https://localhost:44305 + imagename (images/imageId)
                    ///

                    await simpleImage.PostImageAsyncFB("", "");
                }
                else if (!toFacebook && !toLinkedin && toTwitter)
                {
                    await simpleImage.PostImageAsyncTweet(userPost, webRootPath+userImageUrl);
                }
                else if (!toFacebook && toLinkedin && toTwitter)
                {
                    await simpleImage.PostImageAsyncIn(userPost, userImageUrl);

                    await simpleImage.PostImageAsyncTweet(userPost, userImageUrl);
                }
                else if (!toFacebook && toLinkedin && !toTwitter)
                {
                    await simpleImage.PostImageAsyncIn(userPost, userImageUrl);
                }
            }
        }
    }
}