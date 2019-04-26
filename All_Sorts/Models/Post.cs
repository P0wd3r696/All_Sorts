using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace All_Sorts.Models
{
    public class Post
    {
        [Required]
        public int Id { get; set; }
        
        [Display(Name = "Post")]
        public string UserPost { get; set; }

        //[Display(Name = "Caption")]
        //public string UserCaption { get; set; }
        
        public string UserImage { get; set; }

        [Display(Name = "Date Posted")]
        [DisplayFormat(DataFormatString = "{0:g}")]
        public DateTime DatePosted { get; set; }

        [Display(Name ="Post to Linkedin")]
        public bool toLinkedin { get; set; }

        [Display(Name = "Post to Facebook")]
        public bool toFacebook { get; set; }

        [Display(Name = "Post to Twitter")]
        public bool toTwitter { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
