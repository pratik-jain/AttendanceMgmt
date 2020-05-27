using AttendanceMgmt.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AttendanceMgmt.Models;
using System.Security.Cryptography;
using System.Text;

namespace AttendanceMgmt.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Admin/Home
        [Authorize]
        public ActionResult Home()
        {           
            return View();
        }

        [HttpGet]
        public JsonResult GetAllUsers()
        {
            AttendanceManagment attendanceManagment = new AttendanceManagment();
            var userList = new { data = attendanceManagment.vUserDetails.ToList() };
            return Json(userList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int UserId)
        {
            using(var context = new AttendanceManagment())
            {
                context.UserDetails.Remove(context.UserDetails.Single(a => a.UserId == UserId));
                context.Users.Remove(context.Users.Single(a => a.UserId == UserId));
                context.SaveChanges();
            }

            return RedirectToAction("Home","Home",new { Area="Admin"});
        }
              
        [HttpPost]
        public ActionResult UpdateUser(int uUserId ,string uUserName,string uEmail,string uUserType)
        {
            int userTypeId;
            if(uUserType == "Admin")
            {
                userTypeId = 1;
            }
            else if (uUserType == "HR")
            {
                userTypeId = 2;
            }
            else
            {
                userTypeId = 3;
            }
            using (var context = new AttendanceManagment())
            {
                
                try
                {
                    var isUserAvail = context.Users.Where(t => t.UserId == uUserId).FirstOrDefault<AttendanceMgmt.Models.User>();
                    if( isUserAvail != null){
                        isUserAvail.UserName = uUserName;
                        isUserAvail.Email = uEmail;
                        isUserAvail.UserTypeId = userTypeId;
                    }
                    context.SaveChanges();
                }
                catch (Exception e)
                {

                }
            }
            return RedirectToAction("Home","Home",new { Area="Admin"});
        }

        [HttpGet]
        public JsonResult GetEmpDetail(int UserId)
        {
            AttendanceManagment attendanceManagment = new AttendanceManagment();
            var userDetail = attendanceManagment.vUserFullDetails.Where(t => t.UserId == UserId).ToList();
            var jsonResult = Json(userDetail, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = Int32.MaxValue;
            return jsonResult;
        }

        [HttpGet]
        public JsonResult GetUserDetail()
        {
            int UserId = (int)Session["UserId"];
            AttendanceManagment attendanceManagment = new AttendanceManagment();
            var userDetail = attendanceManagment.vUserFullDetails.Where(t => t.UserId == UserId).FirstOrDefault<vUserFullDetail>();
            List<ViewModels.vUserFullDetails> list = new List<ViewModels.vUserFullDetails>();
            list.Add(new ViewModels.vUserFullDetails() { 
                ProfilePhoto = Convert.ToBase64String(userDetail.ProfilePhoto),UserName = userDetail.UserName,
                Email = userDetail.Email,Address = userDetail.Address
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
            return RedirectToAction("Home", "Home", new { Area = "Admin" });
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




