using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EFarmingProject.Models
{
    public class ViewBlogComment
    {
        public Tbl_Blog Blog { get; set; }

        public List<Tbl_Comment> Comment { get; set; }
    }
}