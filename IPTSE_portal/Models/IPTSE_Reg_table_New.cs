namespace IPTSE_portal.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class IPTSE_Reg_table_New
    {
        [Column(TypeName = "numeric")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public decimal Id { get; set; }

        [Display(Name = "First Name")]
        [Required( ErrorMessage = "First Name Required!")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Must Between 3 to 50!")]
        [RegularExpression(@"([a-zA-Z .,\-])*", ErrorMessage = "Invalid First Name!")]
        public string first_name { get; set; }

        [RegularExpression(@"([a-zA-Z .,\-])*", ErrorMessage = "Invalid Middle Name!")]
        [Display(Name = "Middle Name")]
        public string mid_name { get; set; }

        [RegularExpression(@"([a-zA-Z .,\-])*", ErrorMessage = "Invalid Last Name!")]
        [Display(Name = "Last Name")]
        public string last_name { get; set; }

        [Required(ErrorMessage = "Date Of Birth Required!")]
        [Column(TypeName = "date")]
        [Display(Name = "DOB")]
        public DateTime dob { get; set; }

        [Required(ErrorMessage = "Gender is Required!")]
        [StringLength(10, ErrorMessage = "Gender is Required!")]
        [Display(Name = "Sex")]
        public string gender { get; set; }

        [Required(ErrorMessage = "Email-Id Required!")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email Format is wrong")]
        [StringLength(50)]
        [EmailAddress]
        public string email { get; set; }

        [Required(ErrorMessage = "Father Name Required!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Must Between 3 to 50!")]
        [RegularExpression(@"([a-zA-Z .,\-])*", ErrorMessage = "Invalid Father's Name!")]
        [Display(Name = "Father's Name")]
        public string fathername { get; set; }

        [Required(ErrorMessage = "Mother Name Required!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Must Between 3 to 50!")]
        [RegularExpression(@"([a-zA-Z .,\-])*", ErrorMessage = "Invalid Mother's Name!")]
        [Display(Name = "Mother's Name")]
        public string mothername { get; set; }

        [Required(ErrorMessage = "Country Name Required!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Must Between 3 to 50!")]
        [RegularExpression(@"([a-zA-Z .,\-])*", ErrorMessage = "Invalid Country Name!")]
        [Display(Name = "Country")]
        public string country { get; set; }

        [Required(ErrorMessage = "Address Required!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Must Between 3 to 50!")]
        [RegularExpression(@"([a-z]|[A-Z]|[0-9]|[ ]|[-]|[_]|[/]|[:]|[,]|[(]|[)]|[.])*", ErrorMessage = "Invalid Characters!")]
        [Display(Name = "Address")]
        public string addr1 { get; set; }

        [StringLength(50)]
        [RegularExpression(@"([a-z]|[A-Z]|[0-9]|[ ]|[-]|[_]|[/]|[:]|[,]|[(]|[)]|[.])*", ErrorMessage = "Invalid Characters!")]
        [Display(Name = "Address")]
        public string addr2 { get; set; }

        [Required(ErrorMessage = "City Name Required!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Must Between 3 to 50!")]
        [RegularExpression(@"([a-zA-Z .])*", ErrorMessage = "Invalid City Name!")]
        [Display(Name = "City")]
        public string city { get; set; }

        [Required(ErrorMessage = "State Name Required!")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Must Between 2 to 50!")]
        [RegularExpression(@"([a-zA-Z .])*", ErrorMessage = "Invalid State Name!")]
        [Display(Name = "State")]
        public string state { get; set; }

        [Required(ErrorMessage = "Pin Code Required!")]
        [RegularExpression(@"^(?!00000)[0-9]{6,6}$", ErrorMessage = "Pin code is invalid.")] // US or Canada
        [StringLength(10)]
        public string zipcode { get; set; }

        [Required(ErrorMessage = "Contact Required!")]
        [RegularExpression(@"^(?!00000)[0-9]{10,10}$", ErrorMessage = "Invalid Contact!")]
        [Column(TypeName = "numeric")]
        [Display(Name = "Mobile")]
        public string contact { get; set; }

        [Required(ErrorMessage = "Password Required!")]
        [StringLength(50)]
        [Display(Name = "Password")]
        public string password { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Confirm Password required")]
        [CompareAttribute("password", ErrorMessage = "Confirm Password doesn't match.")]
        [StringLength(50)]
        [Display(Name = "confirmPassword")]
        public string confirmpassword { get; set; }

        [Required(ErrorMessage = "School Name Required!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Must Between 3 to 50!")]
        [RegularExpression(@"([a-zA-Z .,\-])*", ErrorMessage = "Invalid School Name!")]
        [Display(Name = "School Name")]
        public string schoolname { get; set; }

        [Required(ErrorMessage = "InstitutionType is Required!")]
        [StringLength(10, ErrorMessage = "InstitutionType is Required!")]
        [Display(Name = "InstitutionType")]
        public string InstitutionType { get; set; }

        [Required(ErrorMessage = "Standard/Course Required!")]
        [StringLength(50, MinimumLength = 1)]
        [Display(Name = "Standard")]
        public string standard { get; set; }
        
        [Required(ErrorMessage = "Your Answer Required!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Must Between 3 to 50!")]
        //[RegularExpression(@"([a-z]|[A-Z]|[ ]|[/])*", ErrorMessage = "Invalid Name!")]
        [Display(Name = "Volunteer Name")]
        public string volunteername { get; set; }
    }
}
