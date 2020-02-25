using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EFarmingProject.Models
{
    [MetadataType(typeof(SupportMetaData))]
    public partial class Tbl_Support
    {
        public HttpPostedFileBase ImageFils { get; set; }
    }
    public class SupportMetaData
    {
        [Display(Name = "Product Title :")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Product title is required")]
        public string ProductTitle { get; set; }
        [Display(Name = "Description :")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Description is required")]
        public string ProductDescription { get; set; }
        [Display(Name = "Product Image :")]
        public string ProductImage { get; set; }

        public HttpPostedFileBase ImageFils { get; set; }
    }
}