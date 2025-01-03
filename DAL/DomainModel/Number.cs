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
            C_Service_Connection_History = new HashSet<C_Service_Connection_History>();
            Calling = new HashSet<Calling>();
            Expenses = new HashSet<Expenses>();
            Internet = new HashSet<Internet>();
            SMS = new HashSet<SMS>();
            Tarif_History = new HashSet<Tarif_History>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public int ID_Tarif { get; set; }

        public int? ID_Client { get; set; }

        [Column("Number")]
        [Required]
        [StringLength(20)]
        public string Number1 { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal Bill { get; set; }

        public DateTime TarifDate { get; set; }

        [Column("_status")]
        public byte? C_status { get; set; }

        public int SMS_remains_amount { get; set; }

        public int MINUTES_remains_amount { get; set; }

        public int Internet_remains_amount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<C_Service_Connection> C_Service_Connection { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<C_Service_Connection_History> C_Service_Connection_History { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Calling> Calling { get; set; }

        public virtual Client Client { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Expenses> Expenses { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Internet> Internet { get; set; }

        public virtual Tarif Tarif { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SMS> SMS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tarif_History> Tarif_History { get; set; }
    }
}
