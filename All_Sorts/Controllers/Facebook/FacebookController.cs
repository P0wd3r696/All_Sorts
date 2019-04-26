using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace All_Sorts.Controllers.Facebook
{
    public class FacebookController : Controller
    {
        readonly string _accessToken;
        readonly string _pageID;
        readonly string _facebookAPI = "https://graph.facebook.com/";
        readonly string _pageEdgeFeed = "feed";
        readonly string _pageEdgePhotos = "photos";
        readonly string _postToPageURL;
        readonly string _postToPagePhotosURL;

        public FacebookController(string accessToken, string pageID)
        {
            _accessToken = accessToken;
            _pageID = pageID;
            _postToPageURL = $"{_facebookAPI}{pageID}/{_pageEdgeFeed}";
            _postToPagePhotosURL = $"{_facebookAPI}{pageID}/{_pageEdgePhotos}";
        }
        /// Publish a simple text post
        /// </summary>
        /// <returns>StatusCode and JSON response</returns>
        /// <param name="postText">Text for posting</param>
        public async Task<Tuple<int,string>> PublishSimplePost(string postText)
        {
            using (var http = new HttpClient())
            {
                var postData = new Dictionary<string, string>
                {
                    {"access_token", _accessToken },
                    {"message", postText }
                };
                var httpResponse = await http.PostAsync(
                    _postToPageURL,
                    new FormUrlEncodedContent(postData));
                var httpContent = await httpResponse.Content.ReadAsStringAsync();

                return new Tuple<int, string>(
                        (int)httpResponse.StatusCode,
                        httpContent
                    );
            }
        }
        /// Publish a post to Facebook page
        /// <returns>Result</returns>
        /// <param name="postText">Post to publish</param>
        /// <param name="pictureURL">Post to publish</param>
        public string PublishToFacebook(string postText, string pictureURL)
        {
            try
            {
                //upload picture first, then after we upload the rest
                var rezImage = Task.Run(async () =>
                    {
                        using (var http = new HttpClient())
                        {
                            return await UploadPhoto(pictureURL);
                        }
                    }
                );
                var rezImageJson = JObject.Parse(rezImage.Result.Item2);

                if (rezImage.Result.Item1 != 200)
                {
                    try //return error
                    {
                        return $"Error uploading photo to Facebook. {rezImageJson["error"]["message"].Value<string>()}";

                    }
                    catch (Exception ex)// return unknown error
                    {
                        return $"Unknown error uploading photo to Facebook. {ex.Message}";
                    }
                }
                //get post ID from the response
                string postID = rezImageJson["post_id"].Value<string>();

                //now we can update the photo with some text
                var rezText = Task.Run(async () =>
                {
                    using (var http = new HttpClient())
                    {
                        return await UpdatePhotoWithPost(postID, postText);
                    }
                });
                var rezTextJson = JObject.Parse(rezText.Result.Item2);

                if (rezText.Result.Item1 != 200)
                {
                    try
                    {
                        return $"Error posting to Facebook. {rezTextJson["error"]["message"].Value<string>()}";
                    }
                    catch (Exception ex)
                    {
                        return $"Unknown error posting to Facebook. {ex.Message}";
                    }
                }
                return "Ok";
            }
            catch (Exception ex)
            {
                return $"Unknown error publishing post to Facebook. {ex.Message}";
            }
        }
        /// Upload a picture (photo)        
        /// <returns>StatusCode and JSON response</returns>
        /// <param name="photoURL">URL of the picture to upload</param>
        public async Task<Tuple<int, string>> UploadPhoto(string photoURL)
        {
            using (var http = new HttpClient())
            {
                var postData = new Dictionary<string, string>{
                    { "access_token", _accessToken},
                    { "url", photoURL}
                };
                var httpResponse = await http.PostAsync(
                    _postToPagePhotosURL,
                    new FormUrlEncodedContent(postData)
                    );
                var httpContent = await httpResponse.Content.ReadAsStringAsync();

                return new Tuple<int, string>(
                    (int)httpResponse.StatusCode,
                    httpContent
                    );
            }
        }
        /// Update the uploaded picture (photo) with the given text
        /// <returns>StatusCode and JSON response</returns>
        /// <param name="postID">Post ID</param>
        /// <param name="postText">Text to add tp the post</param>
        public async Task<Tuple<int, string>> UpdatePhotoWithPost(string postID, string postText)
        {
            using (var http = new HttpClient())
            {
                var postData = new Dictionary<string, string>
                {
                    {"access_token",_accessToken },
                    {"message", postText}
                };
                var httpResponse = await http.PostAsync(
                    $"{_facebookAPI}{postID}",
                    new FormUrlEncodedContent(postData)
                    );
                var httpContent = await httpResponse.Content.ReadAsStringAsync();

                return new Tuple<int, string>(
                    (int)httpResponse.StatusCode,
                    httpContent
                    );
            }
        }
    }
}