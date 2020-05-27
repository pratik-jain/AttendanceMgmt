using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AttendanceMgmt.ViewModels
{
    public class vAttendance
    {
        public int AttendanceId { get; set; }

        public int UserId { get; set; }
        
        public string Date { get; set; }

        public string FromTime { get; set; }

        public string ToTime { get; set; }

        public bool IsApproved { get; set; }

        public int? ApprovedById { get; set; }
        
        public string ApprovedByName { get; set; }


    }
}