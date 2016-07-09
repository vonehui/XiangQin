using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XiangQin.BLL;
using XiangQin.Models;

namespace XiangQin.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        public ActionResult Index()
        {
            if (Request.Cookies["userName"] != null)
            {
                string cookieUserName = Server.HtmlEncode(Request.Cookies["userName"].Value);
                if (cookieUserName.Length > 0)
                {
                    BaseUserInfoBLL baseInfo = new BaseUserInfoBLL();
                    ViewData.Model = baseInfo.getUserList();
                    return View("List");
                }
            }
                return View("Index");
        }
        [HttpPost]
        public ActionResult List(Admin admin)
        {
            AdminBLL adminBLL = new AdminBLL();

            if (adminBLL.Login(admin.aUserName, admin.aPassword))
            {
                BaseUserInfoBLL baseInfo = new BaseUserInfoBLL();
                ViewData.Model = baseInfo.getUserList();
                Response.Cookies["userName"].Value = admin.aUserName;
                Response.Cookies["userName"].Expires = DateTime.Now.AddDays(7);
                return View("List");
            }
            else
            {
                return Content("登录失败");
            }
            
        }
        public ActionResult List()
        {
            if (Request.Cookies["userName"] != null)
            {
                string cookieUserName = Server.HtmlEncode(Request.Cookies["userName"].Value);
                if (cookieUserName.Length>0)
                {
                    BaseUserInfoBLL baseInfo = new BaseUserInfoBLL();
                    ViewData.Model = baseInfo.getUserList();
                    return View("List");
                }
            }

            return RedirectToAction("Index");
        }
        public ActionResult Create()
        {
            if (Request.Cookies["userName"] == null)
            {
                return RedirectToAction("Index");

            }
            string cookieUserName = Server.HtmlEncode(Request.Cookies["userName"].Value);
            if (cookieUserName.Length < 1)
            {
                return RedirectToAction("Index");
            }
            //创建年龄选择的对象从1岁到 119岁
            var selectYearList = new List<SelectListItem>();
            int nowYear = DateTime.Now.Year;
            for (int i = (nowYear - 100); i < nowYear; i++)
            {
                SelectListItem tempSel = new SelectListItem() { Value = i.ToString(), Text = i.ToString()+"年" };
                if (i == (nowYear - 50))
                {
                    tempSel.Selected = true;
                }
                selectYearList.Add(tempSel);
            }
            ViewBag.selectAgeList = selectYearList;
            //穿件月份
            var selectMonthList = new List<SelectListItem>();
            for (int i = 1; i <= 12; i++)
            {
                SelectListItem tempSel = new SelectListItem() { Value = i.ToString(), Text = i.ToString()+"月" };
                selectMonthList.Add(tempSel);
            }
            

            ViewBag.selectMonthList = selectMonthList;
            //创建日
            var selectDateList = new List<SelectListItem>();
            for (int i = 1; i <= 31; i++)
            {
                SelectListItem tempSel = new SelectListItem() { Value = i.ToString(), Text = i.ToString()+"日" };
                selectDateList.Add(tempSel);
            }


            ViewBag.selectDateList = selectDateList;
            //创建年龄选择的对象从1岁到 119岁
            //var selectAgeList = new List<SelectListItem>();
            //for (int i = 1; i < 120; i++)
            //{
            //    selectAgeList.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString() });
            //}
            //ViewBag.selectAgeList = selectAgeList;
            //创建一个身高对象 从100cm 到 250 cm;
            var selectHeightList = new List<SelectListItem>();
            for (int i = 100; i < 250; i++)
            {
                selectHeightList.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString()+"cm" });
            }
            ViewBag.selectHeightList = selectHeightList;
            //创建血型选择对象 A型、B型、O型、AB型
            var selectBloodList = new List<SelectListItem>();
            selectBloodList.Add(new SelectListItem() { Value = "A型", Text = "A型" });
            selectBloodList.Add(new SelectListItem() { Value = "B型", Text = "B型" });
            selectBloodList.Add(new SelectListItem() { Value = "O型", Text = "O型" });
            selectBloodList.Add(new SelectListItem() { Value = "AB型", Text = "AB型" });
            ViewBag.selectBloodList = selectBloodList;

            //创建学历选择对象 
            var selectEduList = new List<SelectListItem>();
            selectEduList.Add(new SelectListItem() { Value = "初中及以下", Text = "初中及以下" });
            selectEduList.Add(new SelectListItem() { Value = "高中及中专", Text = "高中及中专" });
            selectEduList.Add(new SelectListItem() { Value = "大专", Text = "大专" });
            selectEduList.Add(new SelectListItem() { Value = "本科", Text = "本科" });
            selectEduList.Add(new SelectListItem() { Value = "硕士及以上", Text = "硕士及以上" });
            ViewBag.selectEduList = selectEduList;
            //创建职业选择对象
            var selectProList = new List<SelectListItem>();
            selectProList.Add(new SelectListItem() { Value = "在校学生", Text = "在校学生" });
            selectProList.Add(new SelectListItem() { Value = "军人", Text = "军人" });
            selectProList.Add(new SelectListItem() { Value = "私营业主", Text = "私营业主" });
            selectProList.Add(new SelectListItem() { Value = "企业职工", Text = "企业职工" });
            selectProList.Add(new SelectListItem() { Value = "农业劳动者", Text = "农业劳动者" });
            selectProList.Add(new SelectListItem() { Value = "政府籍贯/事业单位工作者", Text = "政府籍贯/事业单位工作者" });
            selectProList.Add(new SelectListItem() { Value = "其他", Text = "其他" });
            ViewBag.selectProList = selectProList;
            //创建收入选择对象
            var selectMoneyList = new List<SelectListItem>();
            selectMoneyList.Add(new SelectListItem() { Value = "2000元以下", Text = "2000元以下" });
            selectMoneyList.Add(new SelectListItem() { Value = "2000-5000", Text = "2000-5000" });
            selectMoneyList.Add(new SelectListItem() { Value = "5000-10000", Text = "5000-10000" });
            selectMoneyList.Add(new SelectListItem() { Value = "10000-20000", Text = "10000-20000" });
            selectMoneyList.Add(new SelectListItem() { Value = "20000以上", Text = "20000以上" });
            ViewBag.selectMoneyList = selectMoneyList;
            //创建婚姻状况选择对象
            var selectMarriageList = new List<SelectListItem>();
            selectMarriageList.Add(new SelectListItem() { Value = "未婚", Text = "未婚" });
            selectMarriageList.Add(new SelectListItem() { Value = "离异", Text = "离异" });
            selectMarriageList.Add(new SelectListItem() { Value = "丧偶", Text = "丧偶" });
            ViewBag.selectMarriageList = selectMarriageList;
            //创建住房情况对象
            var selectHouseList = new List<SelectListItem>();
            selectHouseList.Add(new SelectListItem() { Value = "已购房", Text = "已购房" });
            selectHouseList.Add(new SelectListItem() { Value = "与父母同住", Text = "与父母同住" });
            selectHouseList.Add(new SelectListItem() { Value = "租房", Text = "租房" });
            selectHouseList.Add(new SelectListItem() { Value = "其他", Text = "其他" });
            ViewBag.selectHouseList = selectHouseList;
            //创建爱好情况对象
            var selectHobbyList = new List<SelectListItem>();
            selectHobbyList.Add(new SelectListItem() { Value = "上网", Text = "上网" });
            selectHobbyList.Add(new SelectListItem() { Value = "研究汽车", Text = "研究汽车" });
            selectHobbyList.Add(new SelectListItem() { Value = "养小动物", Text = "养小动物" });
            selectHobbyList.Add(new SelectListItem() { Value = "摄影", Text = "摄影" });
            selectHobbyList.Add(new SelectListItem() { Value = "看电影", Text = "看电影" });
            selectHobbyList.Add(new SelectListItem() { Value = "听音乐", Text = "听音乐" });
            selectHobbyList.Add(new SelectListItem() { Value = "写作", Text = "写作" });
            selectHobbyList.Add(new SelectListItem() { Value = "购物", Text = "购物" });
            selectHobbyList.Add(new SelectListItem() { Value = "做手工艺", Text = "做手工艺" });
            selectHobbyList.Add(new SelectListItem() { Value = "做园艺", Text = "做园艺" });
            selectHobbyList.Add(new SelectListItem() { Value = "跳舞", Text = "跳舞" });
            selectHobbyList.Add(new SelectListItem() { Value = "看展览", Text = "看展览" });
            selectHobbyList.Add(new SelectListItem() { Value = "烹饪", Text = "烹饪" });
            selectHobbyList.Add(new SelectListItem() { Value = "读书", Text = "读书" });
            selectHobbyList.Add(new SelectListItem() { Value = "绘画", Text = "绘画" });
            selectHobbyList.Add(new SelectListItem() { Value = "研究计算机", Text = "研究计算机" });
            selectHobbyList.Add(new SelectListItem() { Value = "做运动", Text = "做运动" });
            selectHobbyList.Add(new SelectListItem() { Value = "旅游", Text = "旅游" });
            selectHobbyList.Add(new SelectListItem() { Value = "玩电子游戏", Text = "玩电子游戏" });
            selectHobbyList.Add(new SelectListItem() { Value = "其他", Text = "其他" });
            ViewBag.selectHobbyList = selectHobbyList;
            return View();
        }
        [HttpPost]
        public ActionResult Create(BaseUserInfo user)
        {
            BaseUserInfoBLL baseInfo = new BaseUserInfoBLL();
            if ( baseInfo.createUser(user))
	        {
                return RedirectToAction("List", "Admin");
            }
            else
            {
                return Content("创建失败");
            }     
            
        }
        public ActionResult Edit(int id)
        {
            if (Request.Cookies["userName"] == null)
            {
                return RedirectToAction("Index");

            }
            string cookieUserName = Server.HtmlEncode(Request.Cookies["userName"].Value);
            if (cookieUserName.Length < 1)
            {
                return RedirectToAction("Index");
            }
            
            //创建年龄选择的对象从1岁到 119岁
            var selectYearList = new List<SelectListItem>();
            int nowYear = DateTime.Now.Year;
            for (int i = (nowYear - 100); i < nowYear; i++)
            {
                SelectListItem tempSel = new SelectListItem() { Value = i.ToString(), Text = i.ToString() + "年" };
                if (i == (nowYear - 50))
                {
                    tempSel.Selected = true;
                }
                selectYearList.Add(tempSel);
            }
            ViewBag.selectAgeList = selectYearList;
            //穿件月份
            var selectMonthList = new List<SelectListItem>();
            for (int i = 1; i <= 12; i++)
            {
                SelectListItem tempSel = new SelectListItem() { Value = i.ToString(), Text = i.ToString() + "月" };
                selectMonthList.Add(tempSel);
            }


            ViewBag.selectMonthList = selectMonthList;
            //创建日
            var selectDateList = new List<SelectListItem>();
            for (int i = 1; i <= 31; i++)
            {
                SelectListItem tempSel = new SelectListItem() { Value = i.ToString(), Text = i.ToString() + "日" };
                selectDateList.Add(tempSel);
            }


            ViewBag.selectDateList = selectDateList;
            //创建一个身高对象 从100cm 到 250 cm;
            var selectHeightList = new List<SelectListItem>();
            for (int i = 100; i < 250; i++)
            {
                selectHeightList.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString() + "cm" });
            }
            ViewBag.selectHeightList = selectHeightList;
            //创建血型选择对象 A型、B型、O型、AB型
            var selectBloodList = new List<SelectListItem>();
            selectBloodList.Add(new SelectListItem() { Value = "A型", Text = "A型" });
            selectBloodList.Add(new SelectListItem() { Value = "B型", Text = "B型" });
            selectBloodList.Add(new SelectListItem() { Value = "O型", Text = "O型" });
            selectBloodList.Add(new SelectListItem() { Value = "AB型", Text = "AB型" });
            ViewBag.selectBloodList = selectBloodList;

            //创建学历选择对象 
            var selectEduList = new List<SelectListItem>();
            selectEduList.Add(new SelectListItem() { Value = "初中及以下", Text = "初中及以下" });
            selectEduList.Add(new SelectListItem() { Value = "高中及中专", Text = "高中及中专" });
            selectEduList.Add(new SelectListItem() { Value = "大专", Text = "大专" });
            selectEduList.Add(new SelectListItem() { Value = "本科", Text = "本科" });
            selectEduList.Add(new SelectListItem() { Value = "硕士及以上", Text = "硕士及以上" });
            ViewBag.selectEduList = selectEduList;
            //创建职业选择对象
            var selectProList = new List<SelectListItem>();
            selectProList.Add(new SelectListItem() { Value = "在校学生", Text = "在校学生" });
            selectProList.Add(new SelectListItem() { Value = "军人", Text = "军人" });
            selectProList.Add(new SelectListItem() { Value = "私营业主", Text = "私营业主" });
            selectProList.Add(new SelectListItem() { Value = "企业职工", Text = "企业职工" });
            selectProList.Add(new SelectListItem() { Value = "农业劳动者", Text = "农业劳动者" });
            selectProList.Add(new SelectListItem() { Value = "政府籍贯/事业单位工作者", Text = "政府籍贯/事业单位工作者" });
            selectProList.Add(new SelectListItem() { Value = "其他", Text = "其他" });
            ViewBag.selectProList = selectProList;
            //创建收入选择对象
            var selectMoneyList = new List<SelectListItem>();
            selectMoneyList.Add(new SelectListItem() { Value = "2000元以下", Text = "2000元以下" });
            selectMoneyList.Add(new SelectListItem() { Value = "2000-5000", Text = "2000-5000" });
            selectMoneyList.Add(new SelectListItem() { Value = "5000-10000", Text = "5000-10000" });
            selectMoneyList.Add(new SelectListItem() { Value = "10000-20000", Text = "10000-20000" });
            selectMoneyList.Add(new SelectListItem() { Value = "20000以上", Text = "20000以上" });
            ViewBag.selectMoneyList = selectMoneyList;
            //创建婚姻状况选择对象
            var selectMarriageList = new List<SelectListItem>();
            selectMarriageList.Add(new SelectListItem() { Value = "未婚", Text = "未婚" });
            selectMarriageList.Add(new SelectListItem() { Value = "离异", Text = "离异" });
            selectMarriageList.Add(new SelectListItem() { Value = "丧偶", Text = "丧偶" });
            ViewBag.selectMarriageList = selectMarriageList;
            //创建住房情况对象
            var selectHouseList = new List<SelectListItem>();
            selectHouseList.Add(new SelectListItem() { Value = "已购房", Text = "已购房" });
            selectHouseList.Add(new SelectListItem() { Value = "与父母同住", Text = "与父母同住" });
            selectHouseList.Add(new SelectListItem() { Value = "租房", Text = "租房" });
            selectHouseList.Add(new SelectListItem() { Value = "其他", Text = "其他" });
            ViewBag.selectHouseList = selectHouseList;
            //BaseUserInfo user = new BaseUserInfo();
            BaseUserInfoBLL userBLL = new BaseUserInfoBLL();
            //创建爱好情况对象
            var selectHobbyList = new List<SelectListItem>();
            selectHobbyList.Add(new SelectListItem() { Value = "上网", Text = "上网" });
            selectHobbyList.Add(new SelectListItem() { Value = "研究汽车", Text = "研究汽车" });
            selectHobbyList.Add(new SelectListItem() { Value = "养小动物", Text = "养小动物" });
            selectHobbyList.Add(new SelectListItem() { Value = "摄影", Text = "摄影" });
            selectHobbyList.Add(new SelectListItem() { Value = "看电影", Text = "看电影" });
            selectHobbyList.Add(new SelectListItem() { Value = "听音乐", Text = "听音乐" });
            selectHobbyList.Add(new SelectListItem() { Value = "写作", Text = "写作" });
            selectHobbyList.Add(new SelectListItem() { Value = "购物", Text = "购物" });
            selectHobbyList.Add(new SelectListItem() { Value = "做手工艺", Text = "做手工艺" });
            selectHobbyList.Add(new SelectListItem() { Value = "做园艺", Text = "做园艺" });
            selectHobbyList.Add(new SelectListItem() { Value = "跳舞", Text = "跳舞" });
            selectHobbyList.Add(new SelectListItem() { Value = "看展览", Text = "看展览" });
            selectHobbyList.Add(new SelectListItem() { Value = "烹饪", Text = "烹饪" });
            selectHobbyList.Add(new SelectListItem() { Value = "读书", Text = "读书" });
            selectHobbyList.Add(new SelectListItem() { Value = "绘画", Text = "绘画" });
            selectHobbyList.Add(new SelectListItem() { Value = "研究计算机", Text = "研究计算机" });
            selectHobbyList.Add(new SelectListItem() { Value = "做运动", Text = "做运动" });
            selectHobbyList.Add(new SelectListItem() { Value = "旅游", Text = "旅游" });
            selectHobbyList.Add(new SelectListItem() { Value = "玩电子游戏", Text = "玩电子游戏" });
            selectHobbyList.Add(new SelectListItem() { Value = "其他", Text = "其他" });
            ViewBag.selectHobbyList = selectHobbyList;
            return View(userBLL.getUser(id));
        }
        [HttpPost]
        public ActionResult Edit(BaseUserInfo user)
        {
            BaseUserInfoBLL userBLL = new BaseUserInfoBLL();
            if (userBLL.editUser(user))
            {
                return RedirectToAction("List", "Admin");
            }
            else
            {
                return Content("修改失败");
            }
            
        }
        public ActionResult Delete(int id)
        {
            if (Request.Cookies["userName"] == null)
            {
                return RedirectToAction("Index");

            }
            string cookieUserName = Server.HtmlEncode(Request.Cookies["userName"].Value);
            if (cookieUserName.Length < 1)
            {
                return RedirectToAction("Index");
            }
            BaseUserInfoBLL userBLL = new BaseUserInfoBLL();
            if (userBLL.deleteUser(id))
            {
                return RedirectToAction("List", "Admin");
            }
            else
            {
                return Content("删除失败");
            }

        }
        public ActionResult AD()
        {
            if (Request.Cookies["userName"] == null)
            {
                return RedirectToAction("Index");

            }
            string cookieUserName = Server.HtmlEncode(Request.Cookies["userName"].Value);
            if (cookieUserName.Length < 1)
            {
                return RedirectToAction("Index");
            }
            ADBLL ad = new ADBLL();
            return View(ad.getADList());
        }
        public ActionResult ADEdit(int id)
        {
            if (Request.Cookies["userName"] == null)
            {
                return RedirectToAction("Index");

            }
            string cookieUserName = Server.HtmlEncode(Request.Cookies["userName"].Value);
            if (cookieUserName.Length < 1)
            {
                return RedirectToAction("Index");
            }
            ADBLL ad = new ADBLL();
            return View(ad.getAD(id));
        }
        [HttpPost]
        public ActionResult ADEdit(AD ad)
        {
            ADBLL adBLL = new ADBLL();
            if (adBLL.EditAD(ad))
            {
                return RedirectToAction("AD");
            }
            else
            {
                return Content("修改失败");
            }
        }
        public ActionResult deleteAD(int id)
        {
            if (Request.Cookies["userName"] == null)
            {
                return RedirectToAction("Index");

            }
            string cookieUserName = Server.HtmlEncode(Request.Cookies["userName"].Value);
            if (cookieUserName.Length < 1)
            {
                return RedirectToAction("Index");
            }
            ADBLL adBLL = new ADBLL();
            if (adBLL.deleteAD(id))
            {
                return RedirectToAction("AD");
            }
            else
            {
                return Content("删除失败");
            }
        }
        public ActionResult createAD()
        {
            return View();
        }
        [HttpPost]
        public ActionResult createAD(AD ad)
        {
            if (Request.Cookies["userName"] == null)
            {
                return RedirectToAction("Index");

            }
            string cookieUserName = Server.HtmlEncode(Request.Cookies["userName"].Value);
            if (cookieUserName.Length < 1)
            {
                return RedirectToAction("Index");
            }

            ADBLL adBLL = new ADBLL();
            if (adBLL.createAD(ad))
            {
                return RedirectToAction("AD");
            }
            else
            {
                return Content("创建失败");
            }
        }
        public ActionResult loginOut()
        {
            Response.Cookies["userName"].Expires = DateTime.Now;
            return RedirectToAction("Index");
        }
        public ActionResult editAdmin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult editAdmin(ChangeAdmin admin)
        {
            AdminBLL adminBLL = new AdminBLL();

            if (!adminBLL.Login(admin.aUserName,admin.aPassword))
            {
                return Content("修改失败,原密码错误");
            }
            if (admin.aNewPassword!=admin.aReNewPassword)
            {
                return Content("修改失败,新密码两次输入不一致");
            }
            if (!adminBLL.editAdmin(admin))
            {
                return Content("修改失败");
            }
            Response.Cookies["userName"].Expires = DateTime.Now;
            return RedirectToAction("Index");
        }
    }
}
