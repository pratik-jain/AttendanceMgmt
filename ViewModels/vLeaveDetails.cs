using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AttendanceMgmt.ViewModels
{
    public class vLeaveDetails
    {
        
        public int LeaveId { get; set; }

        public int UserId { get; set; }

       
        public string FromDate { get; set; }

      
        public string ToDate { get; set; }

      
        public bool? IsApproved { get; set; }

       
        public string ApprovedBy { get; set; }

     
        public string Reason { get; set; }

        public string UserName { get; set; }
    }
}