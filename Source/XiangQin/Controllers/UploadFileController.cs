using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XiangQin.Controllers
{
    public class UploadFileController : Controller
    {
        //
        // GET: /UploadFile/

        [HttpPost]
      public ActionResult Upload(HttpPostedFileBase file)
      {
          if (null != file && file.ContentLength>0)
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + "uploads";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmss");
                fileName += Path.GetFileName(file.FileName);
                file.SaveAs(Path.Combine(path, fileName));
                return Json(new { filePath = "/uploads/" + fileName, code = 1 });
            }
    return Json(new { filePath = "请选择需要上传的文件", code = 0 });
　　　　      }

　　　　     

       }

    }

