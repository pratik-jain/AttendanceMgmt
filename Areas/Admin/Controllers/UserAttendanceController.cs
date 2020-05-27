using AttendanceMgmt.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace AttendanceMgmt.Areas.Admin.Controllers
{
    public class UserAttendanceController : Controller
    {
        // GET: Admin/UserAttendance
        [Authorize]
        public ActionResult UserAttendance(int UserId)
        {
               Session["UserIdForAttendance"] = UserId;
              return View();
        }

        [HttpGet]
        public JsonResult GetUserAttendance(string month)
        {
            int UserId = (int)Session["UserIdForAttendance"];           
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
                return Json(new { data = "" },JsonRequestBehavior.AllowGet);
            }
        }
        [Authorize]
        public ActionResult ApproveAttendance(int AttendanceId,int UserId)
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
                
                return RedirectToAction("UserAttendance", "UserAttendance", new { Area = "Admin" ,UserId=UserId});
            }
        }

        [Authorize]
        public ActionResult RejectAttendance(int AttendanceId, int UserId)
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

                return RedirectToAction("UserAttendance", "UserAttendance", new { Area = "Admin", UserId = UserId });
            }
        }
        public ActionResult Delete(int AttendanceId)
        {
            int UserId = (int)Session["UserIdForAttendance"];
            using (var context = new AttendanceManagment())
            {                
                context.Attendances.Remove(context.Attendances.Single(a => a.AttendanceId == AttendanceId));
                context.SaveChanges();
            }
            return RedirectToAction("UserAttendance", "UserAttendance", new { Area="Admin",UserId=UserId});
        }

        [Authorize]
        public ActionResult UserLeave(int UserId)
        {
            Session["UserIdForLeave"] = UserId;
            return View();
        }

        [HttpGet]
        public JsonResult GetUserLeave(string month)
        {
            int UserId = (int)Session["UserIdForLeave"];
            System.Data.Entity.Core.Objects.ObjectParameter jsonOut = new System.Data.Entity.Core.Objects.ObjectParameter("jsonOut", typeof(string));
            AttendanceManagementEntities1 attendanceManagementEntities1 = new AttendanceManagementEntities1();
            attendanceManagementEntities1.spGetMonthlyLeaves(month, UserId, jsonOut);
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
        public ActionResult ApproveLeave(int LeaveId, int UserId)
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

                return RedirectToAction("UserLeave", "UserAttendance", new { Area = "Admin", EmpId = UserId });
            }
        }
        public ActionResult RejectLeave(int LeaveId, int UserId)
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

                return RedirectToAction("UserLeave", "UserAttendance", new { Area = "Admin", EmpId = UserId });
            }
        }

        public ActionResult DeleteLeave(int LeaveId)
        {
            int UserId = (int)Session["UserIdForLeave"];
            using (var context = new AttendanceManagment())
            {
                context.Leaves.Remove(context.Leaves.Single(a => a.LeaveId == LeaveId));
                context.SaveChanges();
            }
            return RedirectToAction("UserLeave", "UserAttendance", new { Area = "Admin", EmpId = UserId });
        }
    }
}