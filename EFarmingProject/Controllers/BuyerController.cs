using EFarmingProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EFarmingProject.Controllers
{
    public class BuyerController : Controller
    {
        //Registration Action
        [HttpGet]
        public ActionResult BuyerRegister()
        {
            using (dbMyEFarmingProjectEntities db = new dbMyEFarmingProjectEntities())
            {
                List<Tbl_District> list = db.Tbl_District.ToList();
                ViewBag.Dislist = new SelectList(list, "DistrictId", "DistrictType");

                List<Tbl_NidType> listt = db.Tbl_NidType.ToList();
                ViewBag.Nidlist = new SelectList(listt, "NidTypeId", "NidType");
            }
               
            //dbMyEFarmingProjectEntities db = new dbMyEFarmingProjectEntities();
            //List<Tbl_BuyerProfile> list = db.Tbl_BuyerProfile.ToList();
            //ViewBag.NidTypelist = new SelectList(list, "NidTypeId", "NidType");

            return View();
        }
        //Registration Post Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BuyerRegister([Bind(Exclude = "BIsEmailVarified,BActivationCode")] Tbl_BuyerProfile model, HttpPostedFileBase file)
        {
            bool Status = false;
            string message = "";

            //Model validation
            if (ModelState.IsValid)
            {

                #region //Email is already Exist 
                var isExist = IsEmailExist(model.BEmailId);
                if (isExist)
                {
                    ModelState.AddModelError("EmailExist", "Email already exist");
                    return View(model);
                }
                #endregion
                #region Generate Activation Code 
                model.BActivationCode = Guid.NewGuid();
                #endregion
                #region  Password Hashing 
                model.BPassword = Crypto.Hash(model.BPassword);
                model.BConfirmPassword = Crypto.Hash(model.BConfirmPassword); //
                #endregion

                model.BIsEmailVarified = false;
                model.NidTypeId = 1;
                model.DistrictId = 1;
                model.BCreatedDate = DateTime.Now;
                model.IsBuyerActivated = false;
                model.IsSellerActivated = false;
                string fileName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
                string extension = Path.GetExtension(model.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                model.BImage = "~/BImage/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/BImage/"), fileName);
                model.ImageFile.SaveAs(fileName);

                #region Save to Database
                using (dbMyEFarmingProjectEntities dc = new dbMyEFarmingProjectEntities())
                {
                    dc.Tbl_BuyerProfile.Add(model);
                    try
                    {
                        dc.SaveChanges();
                    }
                    catch (DbEntityValidationException e)
                    {
                        foreach (var eve in e.EntityValidationErrors)
                        {
                            string t = ("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:" + 
                                eve.Entry.Entity.GetType().Name + eve.Entry.State);
                            foreach (var ve in eve.ValidationErrors)
                            {
                                t = ("- Property: \"{0}\"+ Error: \"{1}\""+
                                    ve.PropertyName +  ve.ErrorMessage);
                            }
                        }
                        throw;
                    }

                    //Send Email to User
                    SendVerificationLinkEmail(model.BEmailId, model.BActivationCode.ToString());
                    message = "Registration successfully done. Account activation link " +
                        " has been sent to your email id:" + model.BEmailId;
                    Status = true;
                }
                #endregion

            }
            else
            {
                message = "Invalid Request";
            }
            ViewBag.Message = message;
            ViewBag.Status = Status;

            return View(model);
        }
        //Verify Account
        [HttpGet]
        public ActionResult VerifyAccount(string id)
        {
            bool Status = false;
            using (dbMyEFarmingProjectEntities dc = new dbMyEFarmingProjectEntities())
            {
                dc.Configuration.ValidateOnSaveEnabled = false; // This line I have added here to avoid 
                                                                // Confirm password does not match issue on save changes
                var v = dc.Tbl_BuyerProfile.Where(a => a.BActivationCode == new Guid(id)).FirstOrDefault();
                if (v != null)
                {
                    v.BIsEmailVarified = true;
                    dc.SaveChanges();
                    Status = true;
                }
                else
                {
                    ViewBag.Message = "Invalid Request";
                }
            }
            ViewBag.Status = Status;
            return View();
        }

        //LogIn Action
        [HttpGet]
        public ActionResult BuyerLogin()
        {
            return View();
        }
        //LogIn Post Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BuyerLogin(BuyerLogin login, string ReturnUrl = "")
        {
            string message = "";
            using (dbMyEFarmingProjectEntities dc = new dbMyEFarmingProjectEntities())
            {
                var v = dc.Tbl_BuyerProfile.Where(a => a.BEmailId == login.BEmailId).FirstOrDefault();
                Session["BId"] = v.BId;
                Session["RoleAsBuyer"] = v.IsBuyerActivated;
                Session["RoleAsSeller"] = v.IsSellerActivated;
                if (v != null)
                {
                    if (!v.BIsEmailVarified)
                    {
                        ViewBag.Message = "Please verify your email first";
                        return View();
                    }
                    if (string.Compare(Crypto.Hash(login.BPassword), v.BPassword) == 0)
                    {
                        int timeout = login.RememberMe ? 525600 : 20; // 525600 min = 1 year
                        var ticket = new FormsAuthenticationTicket(login.BEmailId, login.RememberMe, timeout);
                        string encrypted = FormsAuthentication.Encrypt(ticket);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                        cookie.Expires = DateTime.Now.AddMinutes(timeout);
                        cookie.HttpOnly = true;
                        Response.Cookies.Add(cookie);


                        if (Url.IsLocalUrl(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "BuyerProduct");
                        }
                    }
                    else
                    {
                        message = "Invalid credential provided";
                    }
                }
                else
                {
                    message = "Invalid credential provided";
                }
            }
            ViewBag.Message = message;
            return View();
        }

        //LogOut
        [Authorize]
        [HttpPost]
        public ActionResult BuyerLogout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("BuyerLogin", "Buyer");
        }

        [NonAction]
        public bool IsEmailExist(string emailID)
        {
            using (dbMyEFarmingProjectEntities db = new dbMyEFarmingProjectEntities())
            {
                var v = db.Tbl_BuyerProfile.Where(a => a.BEmailId == emailID).FirstOrDefault();
                return v != null;
            }
        }

        [NonAction]
        public void SendVerificationLinkEmail(string emailID, string activationCode)
        {
            var verifyUrl = "/Buyer/VerifyAccount/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("ppghose0@gmail.com", "Partha Proteem Ghose");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "partha150130"; // Replace with actual password
            string subject = "Your account is successfully created!";

            string body = "<br/><br/>We are excited to tell you that your Dotnet Awesome account is" +
                " successfully created. Please click on the below link to verify your account" +
                " <br/><br/><a href='" + link + "'>" + link + "</a> ";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [NonAction]
        public void SendVerificationLinkEmail(string emailID, string activationCode, string emailFor = "VerifyAccount")
        {
            var verifyUrl = "/User/" + emailFor + "/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            //var fromEmail = new MailAddress("dotnetawesome@gmail.com", "Dotnet Awesome");
            //var toEmail = new MailAddress(emailID);
            //var fromEmailPassword = "******"; // Replace with actual password

            var fromEmail = new MailAddress("ppghose0@gmail.com", "Partha Proteem Ghose");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "partha150130"; // Replace with actual password

            string subject = "";
            string body = "";
            if (emailFor == "VerifyAccount")
            {
                subject = "Your account is successfully created!";
                body = "<br/><br/>We are excited to tell you that your E-Farming account is" +
                    " successfully created. Please click on the below link to verify your account" +
                    " <br/><br/><a href='" + link + "'>" + link + "</a> ";
            }
            else if (emailFor == "ResetPassword")
            {
                subject = "Reset Password";
                body = "Hi,<br/>br/>We got request for reset your account password. Please click on the below link to reset your password" +
                    "<br/><br/><a href=" + link + ">Reset Password link</a>";
            }


            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }

        [HttpPost]
        public ActionResult ForgotPassword(string EmailID)
        {
            //Verify Email ID
            //Generate Reset password link 
            //Send Email 
            string message = "";
            bool status = false;

            using (dbMyEFarmingProjectEntities dc = new dbMyEFarmingProjectEntities())
            {
                var account = dc.Tbl_BuyerProfile.Where(a => a.BEmailId == EmailID).FirstOrDefault();
                if (account != null)
                {
                    //Send email for reset password
                    string resetCode = Guid.NewGuid().ToString();
                    SendVerificationLinkEmail(account.BEmailId, resetCode, "ResetPassword");
                    account.ResetPasswordCode = resetCode;
                    //This line I have added here to avoid confirm password not match issue , as we had added a confirm password property 
                    //in our model class in part 1
                    dc.Configuration.ValidateOnSaveEnabled = false;
                    dc.SaveChanges();
                    message = "Reset password link has been sent to your email id.";
                }
                else
                {
                    message = "Account not found";
                }
            }
            ViewBag.Message = message;
            return View();
        }

        public ActionResult ResetPassword(string id)
        {
            //Verify the reset password link
            //Find account associated with this link
            //redirect to reset password page
            if (string.IsNullOrWhiteSpace(id))
            {
                return HttpNotFound();
            }

            using (dbMyEFarmingProjectEntities dc = new dbMyEFarmingProjectEntities())
            {
                var user = dc.Tbl_BuyerProfile.Where(a => a.ResetPasswordCode == id).FirstOrDefault();
                if (user != null)
                {
                    ResetPasswordModel model = new ResetPasswordModel();
                    model.ResetCode = id;
                    return View(model);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordModel model)
        {
            var message = "";
            if (ModelState.IsValid)
            {
                using (dbMyEFarmingProjectEntities dc = new dbMyEFarmingProjectEntities())
                {
                    var user = dc.Tbl_BuyerProfile.Where(a => a.ResetPasswordCode == model.ResetCode).FirstOrDefault();
                    if (user != null)
                    {
                        user.BPassword = Crypto.Hash(model.NewPassword);
                        user.ResetPasswordCode = "";
                        dc.Configuration.ValidateOnSaveEnabled = false;
                        dc.SaveChanges();
                        message = "New password updated successfully";
                    }
                }
            }
            else
            {
                message = "Something invalid";
            }
            ViewBag.Message = message;
            return View(model);
        }


    }
}