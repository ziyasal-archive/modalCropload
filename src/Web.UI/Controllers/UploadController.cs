using System;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Web.UI.Infrastructure;

namespace Web.UI.Controllers
{
    public class UploadController : Controller
    {
        [HttpPost]
        public JsonResult UploadNewscastImage()
        {
            if (Request.Files == null || Request.Files.Count == 0)
            {
                return Json("Not supported.");
            }

            JsonResult result = new JsonResult();
            HttpPostedFileBase file = Request.Files[0];

            if (file.ContentLength > (5 * 1024 * 1024))
            {
                result.Data = new { success = false, message = "5 Mb 'den büyük olamaz" };
            }
            else if (!ImageHelper.IsImage(file))
            {
                result.Data = new { success = false, message = "Desteklenen dosya tipleri: '.jpg, .png, .jpeg'" };
            }
            else
            {
                Image image = Image.FromStream(file.InputStream);
                if (image.Width < 750)
                {
                    result.Data = new { success = false, message = "Minimum fotoğraf genişliği: 750px olmalıdır." };
                }
                else
                {
                    var extension = Path.GetExtension(file.FileName);
                    var fileName = string.Format("{0}{1}", Guid.NewGuid().ToString("D"), extension);
                    string folderPath = Server.MapPath("~/Uploads/Temp");

                    var path = Path.Combine(folderPath, fileName);

                    file.SaveAs(path);

                    result.Data = new
                    {
                        imageUrl = string.Format("/Uploads/Temp/{0}", fileName),
                        success = true,
                        tempImageKey = fileName,
                        dimension = new { w = image.Width, h = image.Height }
                    };
                }
            }

            result.ContentType = "text/plain";
            return result;
        }

        [HttpPost]
        public JsonResult UploadPortfolioImage()
        {
            if (Request.Files == null || Request.Files.Count == 0)
            {
                return Json("Not supported.");
            }

            JsonResult result = new JsonResult();
            HttpPostedFileBase file = Request.Files[0];

            if (file.ContentLength > (5 * 1024 * 1024))
            {
                result.Data = new { success = false, message = "5 Mb 'den büyük olamaz" };
            }
            else if (!ImageHelper.IsImage(file))
            {
                result.Data = new { success = false, message = "Desteklenen dosya tipleri: '.jpg, .png, .jpeg'" };
            }
            else
            {
                Image image = Image.FromStream(file.InputStream);
                if (image.Width < 750)
                {
                    result.Data = new { success = false, message = "Minimum fotoğraf genişliği: 750px olmalıdır." };
                }
                else if (image.Height < 480)
                {
                    result.Data = new { success = false, message = "Minimum fotoğraf yüksekliği: 480px olmalıdır." };
                }
                else
                {
                    var extension = Path.GetExtension(file.FileName);
                    var fileName = string.Format("{0}{1}", Guid.NewGuid().ToString("D"), extension);
                    var path = Path.Combine(Server.MapPath("~/Uploads/Temp"), fileName);
                    file.SaveAs(path);

                    result.Data = new
                    {
                        imageUrl = string.Format("/Uploads/Temp/{0}", fileName),
                        success = true,
                        tempImageKey = fileName,
                        dimension = new { w = image.Width, h = image.Height }
                    };
                }
            }

            result.ContentType = "text/plain";
            return result;
        }

        [HttpPost]
        public JsonResult UploadServiceImage()
        {
            if (Request.Files == null || Request.Files.Count == 0) {
                return Json("Not supported.");
            }

            JsonResult result = new JsonResult();
            HttpPostedFileBase file = Request.Files[0];

            if (file.ContentLength > (5 * 1024 * 1024)) {
                result.Data = new { success = false, message = "5 Mb 'den büyük olamaz" };
            } else if (!ImageHelper.IsImage(file)) {
                result.Data = new { success = false, message = "Desteklenen dosya tipleri: '.jpg, .png, .jpeg'" };
            } else {
                Image image = Image.FromStream(file.InputStream);
                if (image.Width < 750) {
                    result.Data = new { success = false, message = "Minimum fotoğraf genişliği: 750px olmalıdır." };
                } else if (image.Height < 480) {
                    result.Data = new { success = false, message = "Minimum fotoğraf yüksekliği: 480px olmalıdır." };
                } else {
                    var extension = Path.GetExtension(file.FileName);
                    var fileName = string.Format("{0}{1}", Guid.NewGuid().ToString("D"), extension);
                    var path = Path.Combine(Server.MapPath("~/Uploads/Temp"), fileName);
                    file.SaveAs(path);

                    result.Data = new {
                        imageUrl = string.Format("/Uploads/Temp/{0}", fileName),
                        success = true,
                        tempImageKey = fileName,
                        dimension = new { w = image.Width, h = image.Height }
                    };
                }
            }

            result.ContentType = "text/plain";
            return result;
        }

    }
}