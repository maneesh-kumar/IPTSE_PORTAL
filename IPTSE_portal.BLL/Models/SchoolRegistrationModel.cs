using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPTSE_portal.BLL.Models
{
    [Serializable]
    public class SchoolRegistrationModel
    {
        [Key]
        [Required]
        public decimal Id { get; set; }

        [Display(Name = "Institution Name")]
        [Required(ErrorMessage = "Institution Name Required!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Must Beetween 3 to 50!")]
        [RegularExpression(@"([a-z]|[A-Z]|[ ])*", ErrorMessage = "Invalid Institution Name!")]
        public string institution_name { get; set; }

        [Display(Name = "Principal/Director Name")]
        [Required(ErrorMessage = "Principal/Director Name Required!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Must Beetween 3 to 50!")]
        [RegularExpression(@"([a-z]|[A-Z]|[ ])*", ErrorMessage = "Invalid Principal/Director Name!")]
        public string principal_director_name { get; set; }

        [Display(Name = "Branch Name")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Must Beetween 3 to 50!")]
        [RegularExpression(@"([a-z]|[A-Z]|[ ])*", ErrorMessage = "Invalid Branch Name!")]
        public string branch_name { get; set; }

        [Display(Name = "Institution Type")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Must Between 3 to 50!")]
        [Required(ErrorMessage = "Institution Type Required!")]
        public string institution_type { get; set; }

        [Display(Name = "Address1")]
        [Required(ErrorMessage = "Address Required!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Must Beetween 3 to 50!")]
        [RegularExpression(@"([a-z]|[A-Z]|[0-9]|[ ]|[-]|[_]|[/]|[:]|[,]|[(]|[)]|[.])*", ErrorMessage = "Invalid Characters!")]
        public string addr1 { get; set; }
        public string addr2 { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "City Required!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Must Beetween 3 to 50!")]
        [RegularExpression(@"([a-z]|[A-Z]|[ ])*", ErrorMessage = "Invalid City Name!")]
        public string city { get; set; }

        [Display(Name = "State")]
        [Required(ErrorMessage = "State Required!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Must Between 3 to 50!")]
        public string state { get; set; }

        [Display(Name = "ZipCode")]
        [Required(ErrorMessage = "PIN Code Required!")]
        [StringLength(10)]
        [RegularExpression(@"^(?!00000)[0-9]{6,6}$", ErrorMessage = "Pin code is invalid.")]
        public string zipcode { get; set; }

        [Display(Name = "Country")]
        [Required(ErrorMessage = "Country Required!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Must Beetween 3 to 50!")]
        [RegularExpression(@"([a-z]|[A-Z]|[ ])*", ErrorMessage = "Invalid Country!")]
        public string country { get; set; }

        [Display(Name = "Institution Contact Number")]
        [Required(ErrorMessage = "Institution Contact Number Required!")]
        [StringLength(11, MinimumLength = 8, ErrorMessage = "Must Beetween 8 to 11 digits!")]
        [RegularExpression(@"^(?!00000)[0-9]{11,11}$", ErrorMessage = "Invalid Institution Contact Number!")]
        public string institution_contact { get; set; }

        [Required(ErrorMessage = "Email-Id Required!")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email Format is wrong")]
        [StringLength(50)]
        [EmailAddress]
        public string institution_email { get; set; }

        [Display(Name = "Contact Person Name")]
        [Required(ErrorMessage = "Contact Person Name Required!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Must Beetween 3 to 50!")]
        [RegularExpression(@"([a-z]|[A-Z]|[ ])*", ErrorMessage = "Invalid Contact Person Name!")]
        public string contact_person_name { get; set; }

        [Display(Name = "Contact Number")]
        [Required(ErrorMessage = "Contact Number Required!")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Must Enter 10 digit mobile no.!")]
        [RegularExpression(@"^(?!00000)[0-9]{10,10}$", ErrorMessage = "Invalid Contact Number!")]
        public string contact_person_contact { get; set; }

        [Required(ErrorMessage = "Email-Id Required!")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email Format is wrong")]
        [StringLength(50)]
        [EmailAddress]
        public string contact_person_email { get; set; }

        [Display(Name = "Information Source")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Must Between 3 to 50!")]
        [Required(ErrorMessage = "Information Source Required!")]
        public string information_source { get; set; }

        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public Nullable<System.DateTime> Updated_Date { get; set; }

        [Required(ErrorMessage = "password Required!")]
        [StringLength(50)]
        [Display(Name = "password")]
        public string password { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Confirm Password required")]
        [CompareAttribute("password", ErrorMessage = "Confirm Password doesn't match.")]
        [StringLength(50)]
        [Display(Name = "confirmpassword")]
        public string confirmpassword { get; set; }
    }
}