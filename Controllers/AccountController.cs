using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AttendanceMgmt.Models;
using AttendanceMgmt.ViewModels;

namespace AttendanceMgmt.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        [AllowAnonymous]
        public ActionResult SignIn()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(vUser user)
        {

            if (user.UserName != null && user.Password != null)
            {
                
                string input = user.Password;
                string key = "sblw-3hn8-sqoy19";
                byte[] EncStr = UTF8Encoding.UTF8.GetBytes(input);
                TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
                tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
                tripleDES.Mode = CipherMode.ECB;
                tripleDES.Padding = PaddingMode.PKCS7;
                ICryptoTransform cryptoTransform = tripleDES.CreateEncryptor();
                byte[] result = cryptoTransform.TransformFinalBlock(EncStr, 0, EncStr.Length);

                using (var context = new AttendanceManagment())
                {
                    var isUserValid = context.Users.Where(t => t.UserName == user.UserName && t.Password == result).FirstOrDefault<User>();
                    if (isUserValid != null)
                    {
                        var userDetial = context.UserDetails.Where(t => t.UserId == isUserValid.UserId).FirstOrDefault<UserDetail>();
                        Session["UserId"] = isUserValid.UserId;
                        //Session["UserName"] = isUserValid.UserName;
                        //Session["ProfilePhoto"] = userDetial.ProfilePhoto;
                        if (isUserValid.UserTypeId == 1)
                        {                            
                            FormsAuthentication.SetAuthCookie(isUserValid.UserName,false);
                            return RedirectToAction("Home","Home",new { area = "Admin"});
                        }
                        else if (isUserValid.UserTypeId == 2)
                        {
                            FormsAuthentication.SetAuthCookie(isUserValid.UserName, false);
                            return RedirectToAction("Home", "Home", new { area = "HR" });
                        }
                        else if(isUserValid.UserTypeId == 3)
                        {
                            FormsAuthentication.SetAuthCookie(isUserValid.UserName, false);
                            return RedirectToAction("Home", "Home", new { area = "Employee" });
                        }
                        else
                        {
                            return View();
                        }
                    }
                    else
                    {
                        return View();
                    }
                   // return Content("<b>" + isUserValid.UserName);
                }
            }
            else
            {
                return View();
            }            
        }        
       
        public ActionResult LogOut()
        {
            Session.Clear();
            string[] myCookies = Request.Cookies.AllKeys;
            foreach (string cookie in myCookies)
            {
                Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
            }
            return RedirectToAction("SignIn","Account");
        }
    }
}