namespace IPTSE_portal.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class RegistrationDataModel : DbContext
    {
        public RegistrationDataModel()
            : base("name=RegistrationDataModel")
        {
        }

        public virtual DbSet<IPTSE_Reg_table> IPTSE_Reg_table { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IPTSE_Reg_table>()
                .Property(e => e.Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<IPTSE_Reg_table>()
                .Property(e => e.gender)
                .IsFixedLength();

            modelBuilder.Entity<IPTSE_Reg_table>()
                .Property(e => e.contact)
                .HasPrecision(18, 0);
        }
    }
}
