using DBBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XiangQin.BLL;
using XiangQin.Models;

namespace XiangQin.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            ADBLL ad = new ADBLL();
            BaseUserInfoBLL user =new BaseUserInfoBLL();
            ViewBag.adList = ad.getTopAD(5);
            ViewBag.userList = user.getUserList();
            return View();
        }
        public ActionResult UserInfo(int id)
        {
            BaseUserInfoBLL user = new BaseUserInfoBLL();
            return View(user.getUser(id));
        }
        public ActionResult FilterUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FilterUser(FilterUser u)
        {
            FilterBLL filter = new FilterBLL();
            ADBLL ad = new ADBLL();
            ViewBag.adList = ad.getTopAD(5);
            ViewBag.userList = filter.getFiltedList(u);
            return View("Index");

        }
    }
}
