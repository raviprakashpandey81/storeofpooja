using poojastore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace poojastore.Controller
{
    /// <summary>
    /// 
    /// </summary>
    public class PSContactController : SurfaceController
    {
        public const string PARTIAL_VIEW_FOLDER = "~/Views/Partials/";
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// 
        public ActionResult RenderForm()
        {
            return PartialView(PARTIAL_VIEW_FOLDER + "_pscontact.cshtml");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitPSForm(contact model)
        {
            if (ModelState.IsValid)
            {
                SendEmail(model);
                TempData["ContactSuccess"] = true;
                return RedirectToCurrentUmbracoPage();
            }
            return CurrentUmbracoPage();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        private void SendEmail(contact model)
        {
            MailMessage message = new MailMessage(model.EmailAddress, "");
            message.Subject = string.Format("Enquiry from {0} {1} - {2}", model.FirstName, model.LastName, model.EmailAddress);
            message.Body = model.Message;
            SmtpClient client = new SmtpClient();
            client.Send(message);
        }


    }
}