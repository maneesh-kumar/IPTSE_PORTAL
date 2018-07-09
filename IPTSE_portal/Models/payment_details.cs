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
        public decimal Id { get; set; }

        [Required]
        [StringLength(50)]
        public string payment_id { get; set; }

        public DateTime payment_date { get; set; }
    }
}
