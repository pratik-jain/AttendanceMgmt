using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AttendanceMgmt.ViewModels
{
    public class vAttendanceDetails
    {
        public int UserId { get; set; }

       
        public string Date { get; set; }

       
        public string FromTime { get; set; }

       
        public int AttendanceId { get; set; }

        public string ToTime { get; set; }

       
        public string UserName { get; set; }

        
       
        public string Email { get; set; }

       
        public int UserTypeId { get; set; }

        public string UserTypeName { get; set; }
        public bool? IsApproved { get; set; }
        public int ApprovedById { get; set; }
        public string ApprovedByName { get; set; }
    }
}