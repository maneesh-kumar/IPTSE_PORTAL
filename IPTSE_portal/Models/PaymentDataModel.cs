namespace IPTSE_portal.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class PaymentDataModel : DbContext
    {
        public PaymentDataModel()
            : base("name=PaymentDataModel")
        {
        }

        public virtual DbSet<payment_details> payment_details { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<payment_details>()
                .Property(e => e.Id)
                .HasPrecision(18, 0);
        }
    }
}
