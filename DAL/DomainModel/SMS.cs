namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SMS
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public int? ID_number_host { get; set; }

        [Required]
        [StringLength(20)]
        public string Number_slave { get; set; }

        public int? ID_Expenses { get; set; }

        [Column("_Date")]
        public DateTime? C_Date { get; set; }

        [Column("_Data")]
        [StringLength(255)]
        public string C_Data { get; set; }

        public byte? Connection_type { get; set; }

        public virtual Expenses Expenses { get; set; }

        public virtual Number Number { get; set; }
    }
}
