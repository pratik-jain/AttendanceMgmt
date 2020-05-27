namespace AttendanceMgmt.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserDetail
    {
        public int UserDetailId { get; set; }

        public int UserId { get; set; }

        [Required]
        public string Address { get; set; }

        public byte[] ProfilePhoto { get; set; }

        public virtual User User { get; set; }
    }
}
