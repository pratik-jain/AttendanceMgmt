namespace AttendanceMgmt.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Attendance
    {
        public int AttendanceId { get; set; }

        public int UserId { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public TimeSpan FromTime { get; set; }

        public TimeSpan ToTime { get; set; }

        public bool? IsApproved { get; set; }

        public int? ApprovedById { get; set; }

        [StringLength(50)]
        public string ApprovedByName { get; set; }

        public virtual User User { get; set; }
    }
}
