using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ModalCropload.Infrastructure;
using ModalCropload.Models;
using Newtonsoft.Json;

namespace ModalCropload.Controllers
{
    public class NewscastController : Controller
    {
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(NewscastCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (!string.IsNullOrWhiteSpace(model.ImageUploadDetail))
            {
                IEnumerable<TempImageData> uploadedImages = ProcessUploadedImageDetails(model.ImageUploadDetail);
            }


            return RedirectToAction("Create");
        }

        [HttpPost]
        public JsonResult CreateCroppedImage(UploadedImageDetail model)
        {
            string folderPath = Server.MapPath("~/Uploads/Temp/");
            var tempImagePath = string.Format("{0}{1}", folderPath, model.ImgKey);
            string uniqueKey = Guid.NewGuid().ToString("D");

            try
            {
                #region Create Original Image
                var orgImgSettings = new ImageCroppingSettings(tempImagePath)
                {
                    UniqueKey = uniqueKey,
                    ImageNamePrefix = "Com-X-News",
                    OutputExtension = ".jpg",
                    OutputFolderPath = folderPath,
                    ScaleWidth = 750,
                    CropParameters = string.Format("format=jpg&colors=128&Bgcolor=ffffff&crop=({0},{1},{2},{3})&cropxunits={4}&cropyunits={5}",
                                                    model.X,
                                                    model.Y,
                                                    model.Width,
                                                    model.Height,
                                                    model.PreviewImageWidth,
                                                    model.PreviewImageHeight)
                };

                #endregion

                string imageFileName = ImageHelper.CropImageAndSaveToFile(orgImgSettings);

                return Json(new
                {
                    success = true,
                    data = new { imagePath = string.Format("/Uploads/Temp/{0}", imageFileName) }
                });
            }
            catch
            {
                return Json(new
                {
                    success = false
                });
            }
        }

        private IEnumerable<TempImageData> ProcessUploadedImageDetails(string json)
        {
            return JsonConvert.DeserializeObject<List<TempImageData>>(json);
        }
    }
}