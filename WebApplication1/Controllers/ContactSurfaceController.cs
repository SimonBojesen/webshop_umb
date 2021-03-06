﻿using WebApplication1.Models;
using Umbraco.Web.Mvc;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;

namespace WebApplication1.Controllers
{
    public class ContactSurfaceController : SurfaceController
    {
        public const string PARTIAL_VIEW_FOLDER = "~/Views/Partials/Contact/";
        public ActionResult RenderForm()
        {
            return PartialView($"{PARTIAL_VIEW_FOLDER}_Contact.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(ContactModel model)
        {
            if (ModelState.IsValid)
            {
                SendEmail(model);
                TempData["ContactSuccess"] = true;
                return RedirectToCurrentUmbracoPage();
            }
            return CurrentUmbracoPage();
        }

        private void SendEmail(ContactModel model)
        {
            MailMessage message = new MailMessage(model.EmailAddress, "987test32123@gmail.com");
            message.Subject = string.Format("Enquiry from {0} {1} - {2}", model.FirstName, model.LastName, model.EmailAddress);
            message.Body = model.Message;
            SmtpClient client = new SmtpClient();

            client.Send(message);
            
        }
    }
}