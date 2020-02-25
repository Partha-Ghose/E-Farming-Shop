using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EFarmingProject.Models
{
    [MetadataType(typeof(ProductMetaData))]
    public partial class Tbl_Product
    {
        public HttpPostedFileBase ImageFil { get; set; }
    }
    public class ProductMetaData
    {
        [Display(Name = "Product ID :")]
        public int ProductId { get; set; }
        [Display(Name = "Product Title :")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Product title is required")]
        public string ProductTitle { get; set; }
        [Display(Name = "Description :")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Description is required")]
        public string ProductDescription { get; set; }
        [Display(Name = "Product Image :")]
        public string ProductImage { get; set; }
        [Display(Name = "Quantity :")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Quantity is required")]
        public Nullable<int> Quantity { get; set; }
        [Display(Name = "Price :")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Price is required")]
        public Nullable<int> Price { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Category Name is required")]
        public Nullable<int> CategoryId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Active is required")]
        public Nullable<bool> IsActive { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Featured is required")]
        public Nullable<bool> IsFeatured { get; set; }
        public HttpPostedFileBase ImageFil { get; set; }
    }
}