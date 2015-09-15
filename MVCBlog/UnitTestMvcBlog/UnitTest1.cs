using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVCBlog.Controllers;
using MVCBlog.Models;

namespace UnitTestMvcBlog
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// проверка наличия элементов в БД
        /// </summary>
        [TestMethod]
        public void TestMethod1()
        {
            //может упасть если не прписать путь к базе данных
            BlogController controller=new BlogController();
            List<MessageEntity> messageEntities = controller.GetItemsPage();
            if (!(messageEntities.Count > 0))
            {
                throw new Exception("В базе нет сообщений");
            }
        }
        /// <summary>
        /// Проверяет возможность отправки сообщений авторизованных пользователей
        /// должен падать всегда
        /// </summary>
        [TestMethod]
        public void TestAddMessage()
        {
            BlogController controller = new BlogController();
            string text = "test " + DateTime.Now;
            controller.AddMessage(text);
        }

    }
}
