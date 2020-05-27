namespace AttendanceMgmt.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class vUserLeaveDetail
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LeaveId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "date")]
        public DateTime FromDate { get; set; }

        [Key]
        [Column(Order = 3, TypeName = "date")]
        public DateTime ToDate { get; set; }

        [Key]
        [Column(Order = 4)]
        public bool IsApproved { get; set; }

        [StringLength(50)]
        public string ApprovedBy { get; set; }

        [Key]
        [Column(Order = 5)]
        public string Reason { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(50)]
        public string UserName { get; set; }
    }
}
