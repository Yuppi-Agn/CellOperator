namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Administrator")]
    public partial class Administrator
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [Column("_Login")]
        [StringLength(50)]
        public string C_Login { get; set; }

        [Column("_Password")]
        [StringLength(50)]
        public string C_Password { get; set; }
    }
}
