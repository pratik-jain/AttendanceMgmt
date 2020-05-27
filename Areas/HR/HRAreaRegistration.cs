using System.Web.Mvc;

namespace AttendanceMgmt.Areas.HR
{
    public class HRAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "HR";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "HR_default",
                "HR/{controller}/{action}/{id}",
                new { action = "Home", id = UrlParameter.Optional },
                new { controller = "Employee|Home" },
                new[] { "AttendanceMgmt.Areas.HR.Controllers" }
            );
        }
    }
}