namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("_Service_Connection")]
    public partial class C_Service_Connection
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public int? ID_number { get; set; }

        public int? ID_Service { get; set; }

        [Column("_Date")]
        public DateTime? C_Date { get; set; }

        [Column("_status")]
        public byte? C_status { get; set; }

        public virtual C_Service C_Service { get; set; }

        public virtual Number Number { get; set; }
    }
}
