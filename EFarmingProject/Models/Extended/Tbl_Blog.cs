using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EFarmingProject.Models
{
    [MetadataType(typeof(BlogMetaData))]
    public partial class Tbl_Blog
    {
        public HttpPostedFileBase BImageFill { get; set; }
    }
    public class BlogMetaData
    {
        [Display(Name = "Title :")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Title is required")]
        public string BlogTitle { get; set; }

        [Display(Name = "Image :")]
        public string BlogImage { get; set; }

        [Display(Name = "Short Description :")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Short Description is required")]
        public string BShortDescri { get; set; }

        [Display(Name = "Author Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Author Name is required")]
        public string AuthorName { get; set; }

        [Display(Name = "Long Description :")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Long Description is required")]
        public string LongDescription { get; set; }

        public Nullable<System.DateTime> BlCreatedDate { get; set; }

        public HttpPostedFileBase BImageFill { get; set; }
    }
}