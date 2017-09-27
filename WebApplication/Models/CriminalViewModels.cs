using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using System.Web;

namespace WebApplication.Models
{
    public class CriminalViewModel
    {
        
        public CriminalViewModel()
        {
            Email = HttpContext.Current.User.Identity.Name;
        }

        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        public string Name { get; set; }
        
        [DataType(DataType.Text)]
        [Display(Name = "Nationality")]
        public string Nationality { get; set; }
        
        [DataType(DataType.Text)]
        [Display(Name = "Gender")]
        public char? Gender { get; set; }

        [Range(0, 150, ErrorMessage = "Age can only be between 0 and 150 years")]
        [DataType(DataType.Text)]
        [Display(Name = "Min Age")]
        public int? MinAge { get; set; }

        [Range(0, 150, ErrorMessage = "Age can only be between 0 and 150 years")]
        [DataType(DataType.Text)]
        [Display(Name = "Max Age")]
        public int? MaxAge { get; set; }

        [Range(0, 3.0, ErrorMessage = "Height can only be between 0.0 and 3.0 meters")]
        [DataType(DataType.Text)]
        [Display(Name = "Min Height")]
        public decimal? MinHeight { get; set; }
        
        [Range(0, 3.0, ErrorMessage = "Height can only be between 0.0 and 3.0 meters")]
        [DataType(DataType.Text)]
        [Display(Name = "Max Height")]
        public decimal? MaxHeight { get; set; }

        [Range(0, 300, ErrorMessage = "Weight can only be between 0.0 and 300.0 kg")]
        [DataType(DataType.Text)]
        [Display(Name = "Min Weight")]
        public decimal? MinWeight { get; set; }

        [Range(0, 300, ErrorMessage = "Weight can only be between 0.0 and 300.0 kg")]
        [DataType(DataType.Text)]
        [Display(Name = "Max Weight")]
        public decimal? MaxWeight { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Max Results")]
        public int? MaxResults { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Your Email")]
        public string Email { get; set; }
    }
}
