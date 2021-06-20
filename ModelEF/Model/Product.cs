namespace ModelEF.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        public int ProductID { get; set; }

        [Required]
        [StringLength(250)]
        public string ProductName { get; set; }
        [DisplayFormat(ApplyFormatInEditMode =true,DataFormatString = "{0:c}" )]
        public double? UnitCost { get; set; }

        public int Quantity { get; set; }

        [StringLength(50)]
        public string Image { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        public int? Status { get; set; }

        public int? CateID { get; set; }

        public virtual Category Category { get; set; }
    }
}
