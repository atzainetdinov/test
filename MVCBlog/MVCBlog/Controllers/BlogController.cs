using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MVCBlog.Models;

namespace MVCBlog.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {
        // GET: Blog
     
        private MessageContext messageContext;
        private List<MessageEntity> messageEntities;
        const int pageSize = 8;
        public BlogController()
        {
            try
            {
                messageContext = new MessageContext();
                messageEntities = messageContext.MessageEntities.ToList();
            }
            catch (Exception ex)
            {
              HelpFunction.GenerateEx(ex);
            }
        }

        public void AddMessage(string text)
        {
            try
            {
                messageContext = new MessageContext();
                MessageEntity message = new MessageEntity();
                message.Login = User.Identity.GetUserName();
                message.Message = text;
                message.Date = DateTime.Now;
                messageContext.MessageEntities.Load();
                messageContext.MessageEntities.Add(message);
                messageContext.SaveChanges();
            }
            catch (Exception ex)
            {
                HelpFunction.GenerateEx(ex);
            }
        }
        public ActionResult Index(int? id)
        {                                    

            try
            {
                int page = id ?? 0;
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_Items", GetItemsPage(page));
                }
                return View(GetItemsPage(page));
            }
            catch (Exception ex)
            {
                HelpFunction.GenerateEx(ex);
                return null;
            }
        }

        public List<MessageEntity> GetItemsPage(int page = 1)
        {
            try
            {
                //для скролинга которого не будет
                var itemsToSkip = page * pageSize;

                //return messageEntities.OrderByDescending(t => t.Id).Skip(itemsToSkip).
                //    Take(pageSize).ToList();
                return messageEntities.OrderByDescending(t => t.Id).ToList();
            }
            catch (Exception ex)
            {
                HelpFunction.GenerateEx(ex);
                return null;
            }

        }
    }
}