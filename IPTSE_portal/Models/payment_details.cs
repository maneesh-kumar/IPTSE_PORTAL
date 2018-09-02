namespace IPTSE_portal.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class payment_details
    {
        [Column(TypeName = "numeric")]
        [Required(ErrorMessage = "Login Id is Required!")]
        public decimal Id { get; set; }

        [Required(ErrorMessage = " Payment Id Required!")]
        [StringLength(50)]
        public string payment_id { get; set; }

        [Required(ErrorMessage = " Payment Date is Required!")]
        public DateTime payment_date { get; set; }

    }
}
