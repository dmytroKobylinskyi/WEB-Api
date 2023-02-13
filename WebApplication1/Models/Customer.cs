namespace Lab4.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Customer")]
    public partial class Customer
    {
        [Key]
        public long id_customer { get; set; }

        [StringLength(50)]
        public string customer_name { get; set; }

        [StringLength(50)]
        public string customer_surname { get; set; }

        [StringLength(50)]
        public string customer_city { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? customer_phone { get; set; }
    }
}
