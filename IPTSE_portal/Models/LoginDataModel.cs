namespace IPTSE_portal.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class LoginDataModel : DbContext
    {
        public LoginDataModel()
            : base("name=LoginDataModel")
        {
        }

        public virtual DbSet<login_table> login_table { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
