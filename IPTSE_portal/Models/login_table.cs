namespace IPTSE_portal.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class login_table
    {
        [Column(TypeName = "numeric")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public decimal Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Password should be minimum 5 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}
