namespace AttendanceMgmt.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Holiday
    {
        public int HolidayId { get; set; }

        [Required]
        [StringLength(50)]
        public string HolidayName { get; set; }

        [Column(TypeName = "date")]
        public DateTime? date { get; set; }

        public string Description { get; set; }
    }
}
