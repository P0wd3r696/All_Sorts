using All_Sorts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace All_Sorts.ViewModel
{
    public class PostAndCustomerViewModel
    {
        public ApplicationUser UserObj { get; set; }

        public IEnumerable<Post> Posts { get; set; }

        //public IEnumerable<UserPageId> UserPageIds { get; set; }
    }
}
