namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Internet")]
    public partial class Internet
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public int? ID_number { get; set; }

        [Column("_Date", TypeName = "date")]
        public DateTime? C_Date { get; set; }

        [Column("_Data")]
        public decimal? C_Data { get; set; }

        public virtual Number Number { get; set; }
    }
}
