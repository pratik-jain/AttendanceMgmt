namespace AttendanceMgmt.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class vUserDetail
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }

        [Key]
        [Column(Order = 1)]
        public string Address { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string UserName { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(30)]
        public string Email { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(10)]
        public string UserTypeName { get; set; }
    }
}
