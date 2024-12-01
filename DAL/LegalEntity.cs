namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LegalEntity")]
    public partial class LegalEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [StringLength(255)]
        public string OrganizationName { get; set; }

        [StringLength(255)]
        public string MSRN { get; set; }

        [StringLength(255)]
        public string IdividualTaxpayerNumber { get; set; }

        [StringLength(250)]
        public string Adress { get; set; }

        public int? ClientID { get; set; }

        public virtual Client Client { get; set; }
    }
}
