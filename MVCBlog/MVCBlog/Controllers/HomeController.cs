using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCBlog.Models;

namespace MVCBlog.Controllers
{
    public class HomeController : Controller
    {
        List<MessageEntity> messageEntities;
        const int pageSize = 8;
        public HomeController()
        {
              var messageContext = new MessageContext();
             // messageContext.MessageEntities.Load();
            messageEntities = messageContext.MessageEntities.ToList();

        }
        public ActionResult Index(int? id)
        {
            int page = id ?? 0;
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Items", GetItemsPage(page));
            }
            return View(GetItemsPage(page));
        }

        private List<MessageEntity> GetItemsPage(int page = 1)
        {
            var itemsToSkip = page * pageSize;

            return messageEntities.OrderBy(t => t.Id).Skip(itemsToSkip).
                Take(pageSize).ToList();
        }
    }

}