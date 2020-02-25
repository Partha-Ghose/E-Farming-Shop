using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EFarmingProject.Models
{
    [MetadataType(typeof(SellerProductMetaData))]
    public partial class Tbl_SellerProduct
    {
        public HttpPostedFileBase ImgFile { get; set; }
    }
    public class SellerProductMetaData
    {
        //[Display(Name = "Product Id")]
        //public int SProductId { get; set; }
        //[Display(Name = "Profile Id")]
        //public Nullable<int> BId { get; set; }
        [Display(Name = "Product Title :")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Product Title is required")]
        public string SProductTitle { get; set; }
        [Display(Name = "Description :")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Description is required")]
        public string SProductDescription { get; set; }
        [Display(Name = "Product Image :")]
        public string SProductImage { get; set; }
        [Display(Name = "Quantity :")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Quantity is required")]
        public Nullable<int> SQuantity { get; set; }
        [Display(Name = "Price :")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Price is required")]
        public Nullable<int> SPrice { get; set; }
        [Display(Name = "Category Type :")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Category is required")]
        public Nullable<int> CategoryId { get; set; }
        public Nullable<System.DateTime> SCreatedDate { get; set; }
        public Nullable<System.DateTime> SModifiedDate { get; set; }
        [Display(Name = "Active :")]
        public Nullable<bool> IsAdminApproved { get; set; }

        public HttpPostedFileBase ImgFile { get; set; }
    }
}