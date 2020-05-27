using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AttendanceMgmt.ViewModels
{
    public class vDateMonthAttendance
    {
        public string Date { get; set; }
        public bool IsApprroved { get; set; }
        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }

    }
}