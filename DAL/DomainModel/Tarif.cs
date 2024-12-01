namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Tarif")]
    public partial class Tarif
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tarif()
        {
            Number = new HashSet<Number>();
            Tarif_History = new HashSet<Tarif_History>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal Price { get; set; }

        public int SMS_amount { get; set; }

        public int MINUTES_amount { get; set; }

        public int Internet_amount { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal call_price_inCity { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal call_price_outCity { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal call_price_outContry { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal sms_price { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal internet_price { get; set; }

        [StringLength(244)]
        public string name { get; set; }

        [Column("_type")]
        public byte? C_type { get; set; }

        [Column("_status")]
        public byte? C_status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Number> Number { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tarif_History> Tarif_History { get; set; }
    }
}
