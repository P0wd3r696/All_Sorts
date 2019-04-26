using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using All_Sorts.Data;
using All_Sorts.Models;
using All_Sorts.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Web;
using System.Collections.Specialized;
using System.Text;
using Newtonsoft.Json.Linq;
using static System.Net.WebRequestMethods;
using System.Net;
using All_Sorts.Controllers.Facebook;
using All_Sorts.Controllers.Linkedin;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using All_Sorts.Controllers.Twitter;
using Tweetinvi;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Tweetinvi.Parameters;
using Microsoft.AspNetCore.Server.Kestrel.Transport.Libuv.Internal.Networking;
using Microsoft.AspNetCore.Builder;
using All_Sorts.Controllers.Strategy;

namespace All_Sorts.Controllers
{
    public class PostsController : Controller
    {
        public static class LinkedinSettings
        {
            public static string url = "https://localhost:44347/Home";

            public static string accessToken = "";
        }
        public static class FacebookSettings
        {
            public static string pageID = "";

            public static string Access_Token = "";

            //public static string Access_Token = "EAAEAQCTS8w4BABnylvUl3T0kAHBJdZCgn8tFTZCquk9VwAFWz0c6uDT97Wt3ePzHdyC8wlcWiNoEs6bQZCLs6000GLPXMI11O1ZAs1TTnZAYEyyndfw82H1W5O311zoLCprlZBydilCZCWNPnAaiQUQCuK58xQQ6cUrALsh1jZCsJQZDZD";
        }
        public static class TwitterSettings
        {
            public static string ConsumerKey = "",
                                ConsumerKeySecret = "",
                                AccessToken = "",
                                AccessTokenSecret = "";
        }
        //private static readonly HttpClient client = new HttpClient();
        private readonly ApplicationDbContext _db;
        private readonly IHostingEnvironment _hostingEnvironment;

        public PostsController(ApplicationDbContext db, IHostingEnvironment hostingEnvironment)
        {
            _db = db;
            _hostingEnvironment = hostingEnvironment;
        }
        [Authorize]
        public async Task<IActionResult> Index(string userId = null)
        {
            if (userId == null)
            {
                //called when customer or guest logs in
                userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            var model = new PostAndCustomerViewModel
            {
                Posts = _db.Posts.Where(c => c.UserId == userId).OrderByDescending(s => s.DatePosted),
                UserObj = _db.Users.FirstOrDefault(u => u.Id == userId)
            };
            return View(model);
        }
        //get method for edit
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var editable = await _db.Posts.SingleOrDefaultAsync(c => c.Id == id);

            if(editable == null)
            {
                return NotFound();
            }

            return View(editable);
        }
        //post method for edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Post post)
        {
            if(id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                post.DatePosted = DateTime.Now;
                _db.Update(post);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }
        //get method for the delete method
        public async  Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var toBeDeleted = await _db.Posts.SingleOrDefaultAsync(m => m.Id == id);

            if(toBeDeleted == null)
            {
                return NotFound();
            }
            return View(toBeDeleted);
        }
        //post method for Delete
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            //var deletedPost = await _db.Posts.SingleOrDefaultAsync(m => m.Id == id);
            var deletedPost = await _db.Posts.FindAsync(id);

            if(deletedPost != null)
            {
                var uploads = Path.Combine(webRootPath, "images");
                var extension = deletedPost.UserImage.Substring(deletedPost.UserImage.LastIndexOf("."), deletedPost.UserImage.Length - deletedPost.UserImage.LastIndexOf("."));

                var imagePath = Path.Combine(uploads, deletedPost.Id + extension);

                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
                _db.Remove(deletedPost);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
        
        //create post get
        [HttpGet]
        public IActionResult Create(string userId)
        {
            Auth.SetUserCredentials(TwitterSettings.ConsumerKey, TwitterSettings.ConsumerKeySecret, TwitterSettings.AccessToken, TwitterSettings.AccessTokenSecret);
            var tweetuser = Tweetinvi.User.GetAuthenticatedUser();

            Post postObj = new Post
            {
                DatePosted = DateTime.Now,
                UserId = userId
            };
            return View(postObj);
        }
        //post create post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Post post, ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                post.DatePosted = DateTime.Now;
                _db.Add(post);
                await _db.SaveChangesAsync();
                
                //Image being updated Right? Also to be Posted
                string webRootPath = _hostingEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                var imagesItemFromDb = _db.Posts.Find(post.Id);
                
                var patterns = new PatternsController();
                
                if (files[0] != null && files[0].Length > 0)
                {
                    var uploads = Path.Combine(webRootPath, "images");
                    var extension = files[0].FileName.Substring(files[0].FileName.LastIndexOf("."), files[0].FileName.Length - files[0].FileName.LastIndexOf("."));

                    using (var filestream = new FileStream(Path.Combine(uploads, post.Id + extension), FileMode.Create))
                    {
                        files[0].CopyTo(filestream);
                    }
                    imagesItemFromDb.UserImage = @"\images\" + post.Id + extension;
                    await _db.SaveChangesAsync();

                    patterns.PostDecider(post.UserPost, post.UserImage, webRootPath, post.toFacebook, post.toLinkedin, post.toTwitter);
                }
                ///Text/Caption
                else
                {
                    patterns.PostDecider(post.UserPost, post.UserImage, webRootPath, post.toFacebook, post.toLinkedin, post.toTwitter);
                }
                return RedirectToAction(nameof(Index), new { userId = post.UserId });
            }
            return View(post);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
        }
    }
}