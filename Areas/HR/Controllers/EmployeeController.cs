using AttendanceMgmt.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace AttendanceMgmt.Areas.HR.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: HR/Employee
        [Authorize]
        public ActionResult Employee()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAllEmployee()
        {
            using (var context = new AttendanceManagment())
            {
                var emp = new { data = context.vUserDetails.Where(t => t.UserTypeName == "Employee").ToList<vUserDetail>() };
                return Json(emp, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult UpdateEmployee(int uUserId, string uUserName, string uEmail,string uPassword)
        {
            using (var context = new AttendanceManagment())
            {
                byte[] encPassword = EncPassword(uPassword);

                var isUserAvail = context.Users.Where(t => t.UserId == uUserId).FirstOrDefault<AttendanceMgmt.Models.User>();
                    if (isUserAvail != null)
                    {
                        isUserAvail.UserName = uUserName;
                        isUserAvail.Email = uEmail;
                        isUserAvail.Password = encPassword;
                    }
                    context.SaveChanges();               
            }
            return RedirectToAction("Employee", "Employee", new { Area = "HR" });
        }

        [HttpGet]
        public JsonResult GetUserDetail(int UserId)
        {
            AttendanceManagment attendanceManagment = new AttendanceManagment();
            var userDetail = attendanceManagment.vUserFullDetails.Where(t => t.UserId == UserId).ToList();
            var jsonResult = Json(userDetail, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = Int32.MaxValue;
            return jsonResult;
        }
        public ActionResult EmployeeAttendance(int EmpId)
        {
            Session["EmpIdForAttendance"] = EmpId;
            return View();
        }

        [HttpGet]
        public JsonResult GetEmployeeAttendance(string month)
        {
            int EmpId = (int)Session["EmpIdForAttendance"];
            System.Data.Entity.Core.Objects.ObjectParameter jsonOut = new System.Data.Entity.Core.Objects.ObjectParameter("jsonOut", typeof(string));
            AttendanceManagementEntities1 attendanceManagementEntities1 = new AttendanceManagementEntities1();
            attendanceManagementEntities1.spGetMonthlyAttendance(month, EmpId, jsonOut);
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

        public ActionResult ApproveAttendance(int AttendanceId, int EmpId)
        {
            using (var context = new AttendanceManagment())
            {

                var approve = context.Attendances.Where(t => t.AttendanceId == AttendanceId).FirstOrDefault<Attendance>();
                if (approve != null)
                {
                    approve.IsApproved = true;
                    approve.ApprovedById = (int)Session["UserId"];
                    approve.ApprovedByName = (string)Session["UserName"];
                }
                context.SaveChanges();

                return RedirectToAction("EmployeeAttendance", "Employee", new { Area = "HR", EmpId = EmpId });
            }
        }

        public ActionResult RejectAttendance(int AttendanceId, int EmpId)
        {
            using (var context = new AttendanceManagment())
            {

                var approve = context.Attendances.Where(t => t.AttendanceId == AttendanceId).FirstOrDefault<Attendance>();
                if (approve != null)
                {
                    approve.IsApproved = false;
                    approve.ApprovedById = (int)Session["UserId"];
                    approve.ApprovedByName = (string)Session["UserName"];
                }
                context.SaveChanges();

                return RedirectToAction("EmployeeAttendance", "Employee", new { Area = "HR", EmpId = EmpId });
            }
        }

        public ActionResult DeleteAttendance(int AttendanceId)
        {
            int UserId = (int)Session["EmpIdForAttendance"];
            using (var context = new AttendanceManagment())
            {
                context.Attendances.Remove(context.Attendances.Single(a => a.AttendanceId == AttendanceId));
                context.SaveChanges();
            }
            return RedirectToAction("EmployeeAttendance", "Employee", new { Area = "HR", EmpId = UserId });
        }

        public ActionResult EmployeeLeaves(int EmpId)
        {
            Session["EmpIdForLeave"] = EmpId;
            return View();
        }

        [HttpGet]
        public JsonResult GetEmployeeLeaves(string month)
        {
            int EmpId = (int)Session["EmpIdForLeave"];
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

        public ActionResult ApproveLeave(int LeaveId, int EmpId)
        {
            using (var context = new AttendanceManagment())
            {

                var approve = context.Leaves.Where(t => t.LeaveId == LeaveId).FirstOrDefault<Leaf>();
                if (approve != null)
                {
                    approve.IsApproved = true;                    
                    approve.ApprovedBy = (string)Session["UserName"];
                }
                context.SaveChanges();

                return RedirectToAction("EmployeeLeaves", "Employee", new { Area = "HR", EmpId = EmpId });
            }
        }

        public ActionResult RejectLeave(int LeaveId, int EmpId)
        {
            using (var context = new AttendanceManagment())
            {

                var approve = context.Leaves.Where(t => t.LeaveId == LeaveId).FirstOrDefault<Leaf>();
                if (approve != null)
                {
                    approve.IsApproved = false;
                    approve.ApprovedBy = (string)Session["UserName"];
                }
                context.SaveChanges();

                return RedirectToAction("EmployeeLeaves", "Employee", new { Area = "HR", EmpId = EmpId });
            }
        }

        public ActionResult DeleteLeave(int LeaveId)
        {
            int UserId = (int)Session["EmpIdForLeave"];
            using (var context = new AttendanceManagment())
            {
                context.Leaves.Remove(context.Leaves.Single(a => a.LeaveId == LeaveId));
                context.SaveChanges();
            }
            return RedirectToAction("EmployeeLeaves", "Employee", new { Area = "HR", EmpId = UserId });
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