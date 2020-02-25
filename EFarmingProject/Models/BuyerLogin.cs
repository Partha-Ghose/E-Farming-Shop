using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EFarmingProject.Models
{
    public class BuyerLogin
    {
        [Display(Name = "Email ID")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email ID required")]
        [DataType(DataType.EmailAddress)]
        public string BEmailId { get; set; }

        [Display(Name = "Password")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Minimum 6 characters required")]
        public string BPassword { get; set; }
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }

    }
}