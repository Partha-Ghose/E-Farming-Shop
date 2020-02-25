using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EFarmingProject.Models
{
    [MetadataType(typeof(BuyerMetaData))]
    public partial class Tbl_BuyerProfile
    {
        public string BConfirmPassword { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
    }
    public class BuyerMetaData
    {
        [Display(Name = "First Name")]
        [DataType(DataType.Text)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "First name required")]
        public string BFirstName { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last name required")]
        public string BLastName { get; set; }

        [Display(Name = "Email ID")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email ID required")]
        [DataType(DataType.EmailAddress)]
        public string BEmailId { get; set; }

        [Display(Name = "Date of birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<System.DateTime> BDateOfBirth { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [StringLength(132, MinimumLength = 6)]
        [Required(ErrorMessage = "Password is required.")]
        public string BPassword { get; set; }
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [StringLength(132, MinimumLength = 6)]
        [Required(ErrorMessage = "Confirmation Password is required.")]
        [Compare("BPassword", ErrorMessage = "Password and Confirmation Password must match.")]
        public string BConfirmPassword { get; set; }

        //[Display(Name = "Password")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        //[DataType(DataType.Password)]
        //[MinLength(6, ErrorMessage = "Minimum 6 characters required")]
        //public string BPassword { get; set; }

        //[Display(Name = "Confirm Password")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Conform-Password is required")]
        //[DataType(DataType.Password)]
        //[Compare("BPassword", ErrorMessage = "Confirm password and password do not match")]
        //public string BConfirmPassword { get; set; }

        [Display(Name = "Nid No :")]
        [MaxLength(13)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Nid is required")]
        public string BNidNo { get; set; }
        [Display(Name = "Description :")]
        public string BDescription { get; set; }
        //[Display(Name = "Nid Type")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Nid Type is required")]
        //public Nullable<int> NidTypeId { get; set; }
        [Display(Name = "User Image :")]
        public string BImage { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
        [Display(Name = "Do you want to Buyer ? :")]
        public Nullable<bool> IsBuyer { get; set; }
        [Display(Name = "Do you want to Seller ? :")]
        public Nullable<bool> IsSeller { get; set; }
        public Nullable<int> DistrictId { get; set; }
        [Display(Name = "Mobile No :")]
        [DataType(DataType.PhoneNumber)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Mobile No is required")]
        public Nullable<int> MobileNo { get; set; }
        [Display(Name = "Another Mobile No")]
        [DataType(DataType.PhoneNumber)]
        public Nullable<int> AnotherMobileNo { get; set; }
        [Display(Name = "Post-Code :")]
        [DataType(DataType.PostalCode)]
        [Required(ErrorMessage = "Post-Code is required.")]
        public Nullable<int> Postcode { get; set; }
        [Display(Name = "Thana :")]
        [Required(ErrorMessage = "Thana is required.")]
        public string Thana { get; set; }
        [Display(Name = "Division :")]
        [Required(ErrorMessage = "Division is required.")]
        public string Division { get; set; }
        [Display(Name = "Village :")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Village is required")]
        public string Village { get; set; }
        [Display(Name = "NID Type :")]
        public Nullable<int> NidTypeId { get; set; }
        public Nullable<bool> IsBuyerActivated { get; set; }
        public Nullable<bool> IsSellerActivated { get; set; }
    }
}