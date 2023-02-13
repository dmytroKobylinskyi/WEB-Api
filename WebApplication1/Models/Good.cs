namespace Lab4.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Good
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? idGoods { get; set; }

        public int idGroup { get; set; }

        [StringLength(50)]
        public string name_goods { get; set; }

        [Column(TypeName = "numeric")]
        public decimal price { get; set; }
    }
}
