using System;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Mvc;
using ModalCropload.Infrastructure;

namespace ModalCropload.Controllers
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
                result.Data = new { success = false, message = "Max 5 Mb" };
            }
            else if (!ImageHelper.IsImage(file))
            {
                result.Data = new { success = false, message = "File Types: '.jpg, .png, .jpeg'" };
            }
            else
            {
                Image image = Image.FromStream(file.InputStream);
                if (image.Width < 750)
                {
                    result.Data = new { success = false, message = "Min width: 750px." };
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
    }
}