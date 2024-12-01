namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Number")]
    public partial class Number
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Number()
        {
            C_Service_Connection = new HashSet<C_Service_Connection>();
            Calling = new HashSet<Calling>();
            Calling1 = new HashSet<Calling>();
            Expenses = new HashSet<Expenses>();
            Internet = new HashSet<Internet>();
            SMS = new HashSet<SMS>();
            SMS1 = new HashSet<SMS>();
            Tarif_History = new HashSet<Tarif_History>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public int ID_Tarif { get; set; }

        public int? ID_Client { get; set; }

        public int? ID_Monthly_remains_tarif { get; set; }

        [Column("Number")]
        [Required]
        [StringLength(20)]
        public string Number1 { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal Bill { get; set; }

        public DateTime TarifDate { get; set; }

        [Column("_status")]
        public byte? C_status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<C_Service_Connection> C_Service_Connection { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Calling> Calling { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Calling> Calling1 { get; set; }

        public virtual Client Client { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Expenses> Expenses { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Internet> Internet { get; set; }

        public virtual Monthly_remains_tarif Monthly_remains_tarif { get; set; }

        public virtual Tarif Tarif { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SMS> SMS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SMS> SMS1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tarif_History> Tarif_History { get; set; }
    }
}
