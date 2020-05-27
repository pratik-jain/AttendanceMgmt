namespace AttendanceMgmt.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class vHolidayDetail
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string HolidayName { get; set; }

        public string Description { get; set; }

        [Column(TypeName = "date")]
        public DateTime? date { get; set; }

        [Key]
        [Column(Order = 1)]
        public int HolidayId { get; set; }
    }
}
