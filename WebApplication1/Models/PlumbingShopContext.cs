using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Lab4.Models
{
    public partial class PlumbingShopContext : DbContext
    {
        public PlumbingShopContext()
            : base("name=PlumbingShopContext")
        {
            
        }

        public virtual DbSet<Good> Goods { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
    }
}
