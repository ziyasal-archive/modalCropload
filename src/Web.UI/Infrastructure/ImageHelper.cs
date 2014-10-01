using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;
using ImageResizer.Configuration;

namespace Web.UI.Infrastructure
{
    public static class ImageHelper
    {
        public static bool IsImage(HttpPostedFileBase file) {
            if (file.ContentType.Contains("image")) {
                return true;
            }

            var formats = new[] { ".jpg", ".png", ".jpeg" };

            // linq from Henrik Stenbæk
            return formats.Any(item => file.FileName.EndsWith(item, StringComparison.OrdinalIgnoreCase));
        }

        public static Bitmap Crop(Image photo, int xCoordinate, int yCoordinate, int width, int height) {
            var result = new Bitmap(width, height, photo.PixelFormat);

            result.SetResolution(photo.HorizontalResolution, photo.VerticalResolution);

            using (var g = Graphics.FromImage(result)) {
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                g.DrawImage(photo, new Rectangle(0, 0, width, height), new Rectangle(xCoordinate, yCoordinate, width, height), GraphicsUnit.Pixel);

                return result;
            }

        }

        public static string CropImageAndSaveToFile(ImageCroppingSettings settings) {
            var sourcePath = settings.OriginalImagePath;
            var destFileName = string.Format("{0}_{1}{2}", settings.ImageNamePrefix, settings.UniqueKey, settings.OutputExtension);
            var destPath = Path.Combine(settings.OutputFolderPath, destFileName);

            var config = new Config();

            config.BuildImage(sourcePath, destPath, settings.CropParameters);
            return destFileName;
        }
    }
}