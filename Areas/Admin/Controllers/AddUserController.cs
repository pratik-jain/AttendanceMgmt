using AttendanceMgmt.Models;
using AttendanceMgmt.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace AttendanceMgmt.Areas.Admin.Controllers
{
    public class AddUserController : Controller
    {
        // GET: Admin/AddUse
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult AddEmployee()
        {
            vUser user = new vUser();
            return View(user);
        }

        [HttpPost]
        public ActionResult AddEmployee(vUser vUser, HttpPostedFileBase img1)
        {           
            if (ModelState.IsValid)
            {
                byte[] encPassword = EncPassword(vUser.Password);
                using (var context = new AttendanceManagment())
                {
                    var user = new User()
                    {
                        UserName = vUser.UserName,
                        Email = vUser.Email,
                        Password = encPassword,
                        UserTypeId = 3
                    };
                    context.Users.Add(user);
                    context.SaveChanges();

                    var userDetail = new UserDetail()
                    {
                        UserId = user.UserId,
                        Address = vUser.Address
                    };
                    userDetail.ProfilePhoto = new byte[img1.ContentLength];
                    img1.InputStream.Read(userDetail.ProfilePhoto, 0, img1.ContentLength);
                    context.UserDetails.Add(userDetail);
                    context.SaveChanges();
                }
            }
            return View();
        }

        [Authorize]
        public ActionResult AddHr()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddHr(vUser vUser,HttpPostedFileBase img1)
        {
            if (ModelState.IsValid)
            {
                byte[] encPassword = EncPassword(vUser.Password);
                using (var context = new AttendanceManagment())
                {
                    var user = new User()
                    {
                        UserName = vUser.UserName,
                        Email = vUser.Email,
                        Password = encPassword,
                        UserTypeId = 2
                    };
                    context.Users.Add(user);
                    context.SaveChanges();

                    var userDetail = new UserDetail()
                    {
                        UserId = user.UserId,
                        Address = vUser.Address
                    };
                    userDetail.ProfilePhoto = new byte[img1.ContentLength];
                    img1.InputStream.Read(userDetail.ProfilePhoto, 0, img1.ContentLength);
                    context.UserDetails.Add(userDetail);
                    context.SaveChanges();

                }
            }
            return View();
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