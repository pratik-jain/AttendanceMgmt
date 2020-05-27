namespace AttendanceMgmt.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Leaf
    {
        [Key]
        public int LeaveId { get; set; }

        public int UserId { get; set; }

        [Column(TypeName = "date")]
        public DateTime FromDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime ToDate { get; set; }

        public bool? IsApproved { get; set; }

        [StringLength(50)]
        public string ApprovedBy { get; set; }

        [Required]
        public string Reason { get; set; }

        public virtual User User { get; set; }
    }
}
