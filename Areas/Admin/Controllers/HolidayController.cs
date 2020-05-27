using AttendanceMgmt.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AttendanceMgmt.Areas.Admin.Controllers
{
    public class HolidayController : Controller
    {
        // GET: Admin/Holiday
        [Authorize]
        public ActionResult Holiday()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetHoliday()
        {
            System.Data.Entity.Core.Objects.ObjectParameter jsonOut = new System.Data.Entity.Core.Objects.ObjectParameter("jsonOut", typeof(string));
            AttendanceManagementEntities1 attendanceManagementEntities1 = new AttendanceManagementEntities1();
            attendanceManagementEntities1.spHolidayDetails(jsonOut);
            var json = JsonConvert.DeserializeObject<List<ViewModels.vHolidayDetails>>(jsonOut.Value.ToString());
            if (json != null)
            {
                return Json(new { data = json.ToList() }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { data = "" }, JsonRequestBehavior.AllowGet);
            }


        }

        [HttpPost]
        public ActionResult AddHoliday(string uName,string uDate,string uDesc)
        {
            DateTime date = Convert.ToDateTime(uDate);
            using (var context = new AttendanceManagment())
            {
                var holiday = new Holiday()
                {
                    HolidayName = uName,
                    date = date,
                    Description = uDesc
                };
                context.Holidays.Add(holiday);                
                context.SaveChanges();
            }
            return RedirectToAction("Home","Home",new { Area = "Admin"});
        }
    }
}