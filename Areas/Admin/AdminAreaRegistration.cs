using System.Web.Mvc;

namespace AttendanceMgmt.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new {action = "Home", id = UrlParameter.Optional },
                new { controller = "AddUser|Home|UserAttendance|Holiday" },
                new[] { "AttendanceMgmt.Areas.Admin.Controllers" }
            );           
           
        }
    }
}