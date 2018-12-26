namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Visiting")]
    public partial class Visiting
    {
        public int ID { get; set; }

        [Column(TypeName = "date")]
        public DateTime StartTime { get; set; }

        [Column(TypeName = "date")]
        public DateTime FinishTime { get; set; }

        public double Price { get; set; }

        public int ClientID { get; set; }

        public int? TrainingID { get; set; }

        public virtual Client Client { get; set; }

        public virtual Training Training { get; set; }
    }
}
