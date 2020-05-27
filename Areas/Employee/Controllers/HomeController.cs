using AttendanceMgmt.Models;
using AttendanceMgmt.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace AttendanceMgmt.Areas.Employee.Controllers
{
    public class HomeController : Controller
    {
        // GET: Employee/Home
        [Authorize]
        public ActionResult Home()
        {
            DateTime dateTime = DateTime.Now;
            List<vDateMonthAttendance> list = new List<vDateMonthAttendance>();
            AttendanceManagementEntities1 attendanceManagementEntities1 = new AttendanceManagementEntities1();
            System.Data.Entity.Core.Objects.ObjectParameter jsonOut = new System.Data.Entity.Core.Objects.ObjectParameter("jsonOut", typeof(string));
            attendanceManagementEntities1.spDateMonthAttendance((int)Session["UserId"], dateTime.Month, jsonOut);
            var json = JsonConvert.DeserializeObject<List<vDateMonthAttendance>>(jsonOut.Value.ToString());
            if (json != null)
            {
                list = json.ToList();
                return View(list);
            }
            else
            {
                list = null;
                return View(list);
            }
        }

        [Authorize]
        public ActionResult Attendance()
        {
            return View();
        }
        [Authorize]
        [HttpGet]
        public JsonResult GetUserAttendance(string month)
        {
            int UserId = (int)Session["UserId"];
            System.Data.Entity.Core.Objects.ObjectParameter jsonOut = new System.Data.Entity.Core.Objects.ObjectParameter("jsonOut", typeof(string));
            AttendanceManagementEntities1 attendanceManagementEntities1 = new AttendanceManagementEntities1();
            attendanceManagementEntities1.spGetMonthlyAttendance(month, UserId, jsonOut);
            var json = JsonConvert.DeserializeObject<List<ViewModels.vAttendanceDetails>>(jsonOut.Value.ToString());
            if (json != null)
            {
                return Json(new { data = json.ToList() }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { data = "" }, JsonRequestBehavior.AllowGet);
            }
        }
        [Authorize]
        public ActionResult AddTodayAttendance(string mDate,string mFromTime,string mToTime)
        {
            DateTime date = Convert.ToDateTime(mDate);

            TimeSpan fromTime = TimeSpan.Parse(mFromTime);
            TimeSpan toTime = TimeSpan.Parse(mToTime);
            
            using (var context = new AttendanceManagment())
            {
                var holidayAvail = context.vHolidayDetails.Where(t => t.date == date).FirstOrDefault<vHolidayDetail>();
                if (holidayAvail != null)
                {
                    return RedirectToAction("Home", "Home", new { Area = "Employee" });
                }
                else
                {
                    var attendance = new Attendance()
                    {
                        UserId = (int)Session["UserId"],
                        Date = date,
                        FromTime = fromTime,
                        ToTime = toTime                        
                    };
                    context.Attendances.Add(attendance);
                    context.SaveChanges();
                    return RedirectToAction("Home", "Home", new { Area = "Employee" });
                }
            }
        }
        [Authorize]
        public ActionResult Leave()
        {
            return View();
        }
        [HttpGet]
        [Authorize]
        public JsonResult GetUserLeave(string month)
        {
            int EmpId = (int)Session["UserId"];
            System.Data.Entity.Core.Objects.ObjectParameter jsonOut = new System.Data.Entity.Core.Objects.ObjectParameter("jsonOut", typeof(string));
            AttendanceManagementEntities1 attendanceManagementEntities1 = new AttendanceManagementEntities1();
            attendanceManagementEntities1.spGetMonthlyLeaves(month, EmpId, jsonOut);
            var json = JsonConvert.DeserializeObject<List<ViewModels.vLeaveDetails>>(jsonOut.Value.ToString());
            if (json != null)
            {
                return Json(new { data = json.ToList() }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { data = "" }, JsonRequestBehavior.AllowGet);
            }
        }
        [Authorize]
        public ActionResult AddLeave(string mReason, string mFromDate, string mToDate)
        {
            DateTime Fromdate = Convert.ToDateTime(mFromDate);
            DateTime Todate = Convert.ToDateTime(mToDate);
            int UserId = (int)Session["UserId"];
            using (var context = new AttendanceManagment())
            {
                var leave = new Leaf()
                {
                    UserId = UserId,
                    FromDate = Fromdate,
                    ToDate = Todate,
                    Reason = mReason                    
                };
                context.Leaves.Add(leave);
                context.SaveChanges();
            }
            return RedirectToAction("Home", "Home", new { Area = "Employee" });
        }

        [HttpGet]
        public JsonResult GetUserDetail()
        {
            int UserId = (int)Session["UserId"];
            AttendanceManagment attendanceManagment = new AttendanceManagment();
            var userDetail = attendanceManagment.vUserFullDetails.Where(t => t.UserId == UserId).FirstOrDefault<vUserFullDetail>();
            List<ViewModels.vUserFullDetails> list = new List<ViewModels.vUserFullDetails>();
            list.Add(new ViewModels.vUserFullDetails()
            {
                ProfilePhoto = Convert.ToBase64String(userDetail.ProfilePhoto),
                UserName = userDetail.UserName,
                Email = userDetail.Email,
                Address = userDetail.Address
            });
            var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = Int32.MaxValue;
            return jsonResult;
        }

        [HttpPost]
        public ActionResult UpdateProfile(string uAddress, string uPassword, string uEmail)
        {
            int UserId = (int)Session["UserId"];
            byte[] encPassword = EncPassword(uPassword);
            using (var context = new AttendanceManagment())
            {
                try
                {
                    var isUserAvail = context.Users.Where(t => t.UserId == UserId).FirstOrDefault<AttendanceMgmt.Models.User>();
                    if (isUserAvail != null)
                    {                        
                        isUserAvail.Email = uEmail;
                        isUserAvail.Password = encPassword;
                    }                    

                    var userDetail = context.UserDetails.Where(t => t.UserId == UserId).FirstOrDefault<UserDetail>();
                    userDetail.Address = uAddress;                    

                    context.SaveChanges();
                }
                catch (Exception e)
                {

                }
            }
            return RedirectToAction("Home", "Home", new { Area = "Employee" });
        }
        [NonAction]
        public byte[] EncPassword(string inPassword)
        {
            string key = "sblw-3hn8-sqoy19";
            byte[] EncStr = UTF8Encoding.UTF8.GetBytes(inPassword);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cryptoTransform = tripleDES.CreateEncryptor();
            byte[] result = cryptoTransform.TransformFinalBlock(EncStr, 0, EncStr.Length);
            return result;
        }
    }
}