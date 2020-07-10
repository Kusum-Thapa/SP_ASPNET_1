using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SP_ASPNET_1.Models
{
    public class BlogComment
    {
        public int BlogCommentId { get; set; }
        public string Comment { get; set; }
        public int BlogPostId { get; set; }

    }
}