using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace IPTSE_portal.BLL.Models
{
    [DataContract]
    public class SchoolRegistrationModel
    {
        [Key]
        [Required]
        [DataMember]
        public decimal Id { get; set; }

        [Display(Name = "Institution Name")]
        [Required(ErrorMessage = "Institution Name Required!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Must Beetween 3 to 50!")]
        [RegularExpression(@"([a-zA-Z .,\-])*", ErrorMessage = "Invalid Institution Name!")]
        [DataMember]
        public string institution_name { get; set; }

        [Display(Name = "Principal/Director Name")]
        [Required(ErrorMessage = "Principal/Director Name Required!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Must Beetween 3 to 50!")]
        [RegularExpression(@"([a-zA-Z .,\-])*", ErrorMessage = "Invalid Principal/Director Name!")]
        [DataMember]
        public string principal_director_name { get; set; }

        [Display(Name = "Branch Name")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Must Beetween 3 to 50!")]
        [RegularExpression(@"([a-zA-Z .,\-])*", ErrorMessage = "Invalid Branch Name!")]
        [DataMember]
        public string branch_name { get; set; }

        [Display(Name = "Institution Type")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Must Between 3 to 50!")]
        [Required(ErrorMessage = "Institution Type Required!")]
        [DataMember]
        public string institution_type { get; set; }

        [Display(Name = "Address1")]
        [Required(ErrorMessage = "Address Required!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Must Beetween 3 to 50!")]
        [RegularExpression(@"([a-z]|[A-Z]|[0-9]|[ ]|[-]|[_]|[/]|[:]|[,]|[(]|[)]|[.])*", ErrorMessage = "Invalid Characters!")]
        [DataMember]
        public string addr1 { get; set; }

        [DataMember]
        public string addr2 { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "City Required!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Must Beetween 3 to 50!")]
        [RegularExpression(@"([a-zA-Z .,\-])*", ErrorMessage = "Invalid City Name!")]
        [DataMember]
        public string city { get; set; }

        [Display(Name = "State")]
        [Required(ErrorMessage = "State Required!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Must Between 3 to 50!")]
        [DataMember]
        public string state { get; set; }

        [Display(Name = "ZipCode")]
        [Required(ErrorMessage = "PIN Code Required!")]
        [StringLength(10)]
        [RegularExpression(@"^(?!00000)[0-9]{6,6}$", ErrorMessage = "Pin code is invalid.")]
        [DataMember]
        public string zipcode { get; set; }

        [Display(Name = "Country")]
        [Required(ErrorMessage = "Country Required!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Must Beetween 3 to 50!")]
        [RegularExpression(@"([a-zA-Z .,\-])*", ErrorMessage = "Invalid Country!")]
        [DataMember]
        public string country { get; set; }

        [Display(Name = "Institution Contact Number")]
        [Required(ErrorMessage = "Institution Contact Number Required!")]
        [StringLength(10, MinimumLength = 8, ErrorMessage = "Must Beetween 8 to 10 digits!")]
        [RegularExpression(@"^(?!00000)[0-9]{10,10}$", ErrorMessage = "Invalid Institution Contact Number!")]
        [DataMember]
        public string institution_contact { get; set; }

        [Required(ErrorMessage = "Email-Id Required!")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email Format is wrong")]
        [StringLength(50)]
        [EmailAddress]
        [DataMember]
        public string institution_email { get; set; }

        [Display(Name = "Contact Person Name")]
        [Required(ErrorMessage = "Contact Person Name Required!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Must Beetween 3 to 50!")]
        [RegularExpression(@"([a-zA-Z .,\-])*", ErrorMessage = "Invalid Contact Person Name!")]
        [DataMember]
        public string contact_person_name { get; set; }

        [Display(Name = "Contact Number")]
        [Required(ErrorMessage = "Contact Number Required!")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Must Enter 10 digit mobile no.!")]
        [RegularExpression(@"^(?!00000)[0-9]{10,10}$", ErrorMessage = "Invalid Contact Number!")]
        [DataMember]
        public string contact_person_contact { get; set; }

        [Required(ErrorMessage = "Email-Id Required!")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email Format is wrong")]
        [StringLength(50)]
        [EmailAddress]
        [DataMember]
        public string contact_person_email { get; set; }

        [Display(Name = "Information Source")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Must Between 3 to 50!")]
        [DataMember]
        public string information_source { get; set; }

        [DataMember]
        public Nullable<bool> IsActive { get; set; }

        [DataMember]
        public Nullable<System.DateTime> Created_Date { get; set; }

        [DataMember]
        public Nullable<System.DateTime> Updated_Date { get; set; }

        [Required(ErrorMessage = "password Required!")]
        [StringLength(50)]
        [Display(Name = "password")]
        [DataMember]
        public string password { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Confirm Password required")]
        [CompareAttribute("password", ErrorMessage = "Confirm Password doesn't match.")]
        [StringLength(50)]
        [Display(Name = "confirmpassword")]
        [DataMember]
        public string confirmpassword { get; set; }
    }
}