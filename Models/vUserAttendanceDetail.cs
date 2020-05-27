namespace AttendanceMgmt.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class vUserAttendanceDetail
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "date")]
        public DateTime Date { get; set; }

        [Key]
        [Column(Order = 2)]
        public TimeSpan FromTime { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AttendanceId { get; set; }

        [Key]
        [Column(Order = 4)]
        public TimeSpan ToTime { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(50)]
        public string UserName { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(30)]
        public string Email { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserTypeId { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(10)]
        public string UserTypeName { get; set; }

        [Key]
        [Column(Order = 9)]
        public bool IsApproved { get; set; }

        public int? ApprovedById { get; set; }

        [StringLength(50)]
        public string ApprovedByName { get; set; }
    }
}
