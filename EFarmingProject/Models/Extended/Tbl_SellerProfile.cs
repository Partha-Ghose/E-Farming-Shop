using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EFarmingProject.Models
{
    [MetadataType(typeof(SellerMetaData))]
    public partial class Tbl_SellerProfile
    {
        public string SConfirmPassword { get; set; }
        public HttpPostedFileBase ImageFilee { get; set; }
    }
    public class SellerMetaData
    {
        [Display(Name = "First Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "First name required")]
        public string SFirstName { get; set; }
        [Display(Name = "Last Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last name required")]
        public string SLastName { get; set; }
        [Display(Name = "Email ID")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email ID required")]
        [DataType(DataType.EmailAddress)]
        public string SEmailId { get; set; }
        [Display(Name = "Date of birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<System.DateTime> SDateOfBirth { get; set; }
        [Display(Name = "Password")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Minimum 6 characters required")]
        public string SPassword { get; set; }
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("SPassword", ErrorMessage = "Confirm password and password do not match")]
        public string SConfirmPassword { get; set; }
        [Display(Name = "Mobile No")]
        [MaxLength(11)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Mobile No is required")]
        public int SMobileNo { get; set; }
        [Display(Name = "Another Mobile No")]
        [MaxLength(11)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Mobile No is required")]
        public int SAnotherMobileNo { get; set; }
        [Display(Name = "Village")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Village is required")]
        public string Svillage { get; set; }
        [Display(Name = "Thana")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Thana is required")]
        public string SThana { get; set; }
        [Display(Name = "Post-code")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Post-code is required")]
        public Nullable<int> Spostcode { get; set; }
        [Display(Name = "Nid No")]
        [MaxLength(13)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Nid is required")]
        public Nullable<int> SNidno { get; set; }
        [Display(Name = "District")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "District is required")]
        public string SDistrict { get; set; }
        [Display(Name = "Division")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Division is required")]
        public string SDivision { get; set; }
        [Display(Name = "Description")]
        public string SDescription { get; set; }
        [Display(Name = "Seller Image")]
        public string SImage { get; set; }
        public HttpPostedFileBase ImageFilee { get; set; }
    }
}